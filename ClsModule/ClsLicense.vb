Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports System.Net
Imports System.Security.Cryptography
Imports System.Text
Imports System.Threading
Imports System.Text.RegularExpressions
Imports Microsoft.Win32
Imports Newtonsoft.Json.Linq
Imports Org.BouncyCastle.Crypto.Parameters
Imports Org.BouncyCastle.Crypto.Signers

Public Enum LicenseState
    Valid
    Expiring
    Grace
    Expired
    NotYetValid
    Invalid
    WrongDevice
    ProductMissing
End Enum

''' <summary>Outcome of checking a license token.</summary>
Public Class LicenseResult
    Public Property State As LicenseState = LicenseState.Invalid
    Public Property Message As String = ""
    Public Property Serial As String = ""
    Public Property ClientName As String = ""
    Public Property Plan As String = ""
    Public Property FromDate As Date = Date.MinValue
    Public Property UntilDate As Date? = Nothing
    Public Property GraceDays As Integer = 0
    Public Property DaysLeft As Integer? = Nothing
    Public Property Products As New Dictionary(Of String, Integer)

    ''' <summary>The app may run normally (a warning may still be shown).</summary>
    Public ReadOnly Property AllowsFullUse As Boolean
        Get
            Return State = LicenseState.Valid OrElse State = LicenseState.Expiring OrElse State = LicenseState.Grace
        End Get
    End Property
End Class

''' <summary>
''' MyPos signed license (Ed25519): MYPOS1.[payload].[signature]. Same token and rules as the Flutter apps.
''' The token is kept on this PC and re-verified on every start; online, it is refreshed from the server.
''' </summary>
Public NotInheritable Class ClsLicense
    Private Sub New()
    End Sub

    Public Const ServerUrl As String = "https://myposqr.com/sam/controller/getfunctionmgmt.php?AjaxRequest=116"
    Private Const RegPath As String = "Software\POSAPP"
    Private Const ExpiringDays As Integer = 7

    ''' <summary>Public keys by key id (kid). Add a new entry when the signing key is rotated.</summary>
    Private Shared ReadOnly PublicKeys As New Dictionary(Of Integer, Byte())() From {
        {1, Convert.FromBase64String("vokaIkFoBBsjJvYXJyP4mSJIN1hzh5gfyV4Z6xBrr8U=")}
    }

    ' ------------------------------------------------------------ roles / device
    ''' <summary>PosLogin (billing) is the RetailClient product; the back-office Login form passes "RETAILSERVER" for RetailServer.</summary>
    Public Shared Function ProductForRole(role As String) As String
        If (If(role, "")).Trim().ToUpperInvariant() = "RETAILSERVER" Then Return "RetailServer"
        Return "RetailClient"
    End Function

    ''' <summary>16 hex chars: SHA-256 of the Windows MachineGuid (salted). Same scheme as the Flutter apps.</summary>
    Public Shared Function Fingerprint() As String
        Dim id As String = ""
        Try
            Using hk = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64)
                Using k = hk.OpenSubKey("SOFTWARE\Microsoft\Cryptography")
                    If k IsNot Nothing Then id = Convert.ToString(k.GetValue("MachineGuid"))
                End Using
            End Using
        Catch
        End Try
        If String.IsNullOrWhiteSpace(id) Then id = Environment.MachineName
        Using sha As SHA256 = SHA256.Create()
            Dim h = sha.ComputeHash(Encoding.UTF8.GetBytes("mypos1|" & id.Trim().ToLowerInvariant()))
            Dim sb As New StringBuilder()
            For i = 0 To 7
                sb.Append(h(i).ToString("x2"))
            Next
            Return sb.ToString()
        End Using
    End Function

    ''' <summary>Code for offline activation: the owner pastes it into the CRM (Licenses - Issue file / code).</summary>
    Public Shared Function RequestCode(product As String) As String
        Dim o As New JObject()
        o("fp") = Fingerprint()
        o("product") = product
        o("ts") = CLng((DateTime.UtcNow - New DateTime(1970, 1, 1)).TotalSeconds)
        Return Convert.ToBase64String(Encoding.UTF8.GetBytes(o.ToString(Newtonsoft.Json.Formatting.None))).Replace("+"c, "-"c).Replace("/"c, "_"c)
    End Function

    ' ------------------------------------------------------------ verify
    Private Shared Function B64u(s As String) As Byte()
        Dim t = s.Replace("-"c, "+"c).Replace("_"c, "/"c)
        t = t.PadRight(t.Length + ((4 - t.Length Mod 4) Mod 4), "="c)
        Return Convert.FromBase64String(t)
    End Function

    Private Shared Function Bad(msg As String, Optional state As LicenseState = LicenseState.Invalid) As LicenseResult
        Return New LicenseResult With {.State = state, .Message = msg}
    End Function

    Private Shared Function Fmt(d As Date) As String
        Return d.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture)
    End Function

    ''' <summary>Checks the signature, device, dates and product. Never throws.</summary>
    Public Shared Function Verify(raw As String, fingerprintId As String, product As String, Optional nowDate As Date? = Nothing) As LicenseResult
        Const notValid As String = "This license is not valid."
        Try
            Dim parts = Regex.Replace(If(raw, ""), "\s", "").Split("."c)
            If parts.Length <> 3 OrElse parts(0) <> "MYPOS1" Then Return Bad(notValid)
            Dim payload = B64u(parts(1))
            Dim sig = B64u(parts(2))
            Dim j = JObject.Parse(Encoding.UTF8.GetString(payload))

            Dim kid = CInt(j("kid"))
            If Not PublicKeys.ContainsKey(kid) OrElse sig.Length <> 64 Then Return Bad(notValid)
            Dim signer As New Ed25519Signer()
            signer.Init(False, New Ed25519PublicKeyParameters(PublicKeys(kid), 0))
            signer.BlockUpdate(payload, 0, payload.Length) ' the signature covers exactly these bytes
            If Not signer.VerifySignature(sig) Then Return Bad(notValid)

            ' Genuine from here on.
            Dim r As New LicenseResult With {
                .Serial = CStr(j("lic")),
                .ClientName = CStr(j("name")),
                .Plan = CStr(j("plan")),
                .FromDate = DateTime.ParseExact(CStr(j("from")), "yyyy-MM-dd", CultureInfo.InvariantCulture),
                .GraceDays = If(j("grace_days") Is Nothing, 0, CInt(j("grace_days")))
            }
            Dim un = j("until")
            If un IsNot Nothing AndAlso un.Type <> JTokenType.Null AndAlso CStr(un) <> "" Then
                r.UntilDate = DateTime.ParseExact(CStr(un), "yyyy-MM-dd", CultureInfo.InvariantCulture)
            End If
            Dim prods = TryCast(j("products"), JObject)
            If prods IsNot Nothing Then
                For Each p In prods.Properties()
                    r.Products(p.Name) = CInt(p.Value)
                Next
            End If

            Dim dev = If(j("dev") Is Nothing, "", CStr(j("dev"))).ToLowerInvariant()
            If dev <> "" AndAlso fingerprintId IsNot Nothing AndAlso dev <> fingerprintId.ToLowerInvariant() Then
                r.State = LicenseState.WrongDevice
                r.Message = "This license was issued for a different computer."
                Return r
            End If
            Dim today = (If(nowDate, Date.Now)).Date
            If r.FromDate.Date > today Then
                r.State = LicenseState.NotYetValid
                r.Message = "This license starts on " & Fmt(r.FromDate) & "."
                Return r
            End If
            If product IsNot Nothing AndAlso (Not r.Products.ContainsKey(product) OrElse r.Products(product) <= 0) Then
                r.State = LicenseState.ProductMissing
                r.Message = product & " is not included in this license."
                Return r
            End If
            If Not r.UntilDate.HasValue Then
                r.State = LicenseState.Valid
                r.Message = "License is active."
                Return r
            End If
            Dim days = CInt((r.UntilDate.Value.Date - today).TotalDays) ' negative once past
            r.DaysLeft = days
            If days > ExpiringDays Then
                r.State = LicenseState.Valid
                r.Message = "License is active until " & Fmt(r.UntilDate.Value) & "."
            ElseIf days >= 0 Then
                r.State = LicenseState.Expiring
                r.Message = "License ends on " & Fmt(r.UntilDate.Value) & " (" & days & " day(s) left). Please renew."
            ElseIf -days <= r.GraceDays Then
                r.State = LicenseState.Grace
                r.Message = "License expired on " & Fmt(r.UntilDate.Value) & ". " & (r.GraceDays + days) & " grace day(s) left - renew now."
            Else
                r.State = LicenseState.Expired
                r.Message = "License expired on " & Fmt(r.UntilDate.Value) & ". Renew to continue."
            End If
            Return r
        Catch
            Return Bad(notValid)
        End Try
    End Function

    ' ------------------------------------------------------------ storage (file, with the registry as backup)
    Private Shared ReadOnly Property LicFile As String
        Get
            Return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "MyPos", "license.lic")
        End Get
    End Property

    Private Shared Function GetReg(name As String) As String
        Try
            Using k = Registry.CurrentUser.OpenSubKey(RegPath)
                If k IsNot Nothing Then Return Convert.ToString(k.GetValue(name))
            End Using
        Catch
        End Try
        Return ""
    End Function

    Private Shared Sub SetReg(name As String, value As String)
        Try
            Using k = Registry.CurrentUser.CreateSubKey(RegPath)
                If String.IsNullOrEmpty(value) Then
                    k.DeleteValue(name, False)
                Else
                    k.SetValue(name, value)
                End If
            End Using
        Catch
        End Try
    End Sub

    ''' <summary>The blocked flag is kept per product: two apps on one PC (e.g. MainServer and ClientPOS) must not block each other.</summary>
    Private Shared Function BlockedName(product As String) As String
        Return "LicenseBlocked_" & product
    End Function

    Public Shared Function StoredToken() As String
        Try
            If File.Exists(LicFile) Then
                Dim t = File.ReadAllText(LicFile).Trim()
                If t <> "" Then Return t
            End If
        Catch
        End Try
        Return GetReg("LicenseToken")
    End Function

    Public Shared Function StoredKey() As String
        Return GetReg("LicenseKey")
    End Function

    Private Shared Sub SaveToken(token As String)
        Dim clean = Regex.Replace(token, "\s", "")
        Try
            Directory.CreateDirectory(Path.GetDirectoryName(LicFile))
            File.WriteAllText(LicFile, clean)
        Catch
        End Try
        SetReg("LicenseToken", clean)
    End Sub

    ' ------------------------------------------------------------ clock guard
    Private Shared Function ClockWentBack() As Boolean
        Dim s = GetReg("LicenseLastSeen")
        Dim last As Long
        If Long.TryParse(s, last) Then
            Return DateTime.UtcNow.Ticks < last - TimeSpan.FromDays(1).Ticks
        End If
        Return False
    End Function

    Private Shared Sub TouchLastSeen()
        Dim s = GetReg("LicenseLastSeen")
        Dim last As Long
        Long.TryParse(s, last)
        SetReg("LicenseLastSeen", Math.Max(last, DateTime.UtcNow.Ticks).ToString(CultureInfo.InvariantCulture))
    End Sub

    ' ------------------------------------------------------------ server
    Private Shared Function PostActivate(key As String, product As String, timeoutMs As Integer) As JObject
        ServicePointManager.Expect100Continue = True
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
        Dim body = "key=" & Uri.EscapeDataString(key) & "&product=" & Uri.EscapeDataString(product) &
                   "&fp=" & Fingerprint() & "&name=" & Uri.EscapeDataString(Environment.MachineName)
        Dim data = Encoding.UTF8.GetBytes(body)
        Dim req = CType(WebRequest.Create(ServerUrl), HttpWebRequest)
        req.Method = "POST"
        req.ContentType = "application/x-www-form-urlencoded"
        req.ContentLength = data.Length
        req.Timeout = timeoutMs
        req.ReadWriteTimeout = timeoutMs
        req.Proxy.Credentials = CredentialCache.DefaultCredentials
        Using s = req.GetRequestStream()
            s.Write(data, 0, data.Length)
        End Using
        Using resp = req.GetResponse()
            Using rd As New StreamReader(resp.GetResponseStream())
                Return JObject.Parse(rd.ReadToEnd())
            End Using
        End Using
    End Function

    ''' <summary>Online activation with the license key from the owner. Returns "" on success, else a message.</summary>
    Public Shared Function ActivateOnline(key As String, product As String) As String
        Dim k = If(key, "").Trim().ToUpperInvariant()
        If k = "" Then Return "Enter the license key."
        Dim r As JObject
        Try
            r = PostActivate(k, product, 25000)
        Catch
            Return "Cannot reach the server. Check the internet, or use an offline code."
        End Try
        If CBool(r("Success")) = False Then Return If(r("Msg") Is Nothing, "Activation failed.", CStr(r("Msg")))
        Dim token = CStr(r("Token"))
        Dim chk = Verify(token, Fingerprint(), product)
        If Not chk.AllowsFullUse Then Return chk.Message
        SaveToken(token)
        SetReg("LicenseKey", k)
        SetReg(BlockedName(product), "")
        TouchLastSeen()
        Return ""
    End Function

    ''' <summary>Offline activation with a pasted code / license.lic text. Returns "" on success, else a message.</summary>
    Public Shared Function ApplyCode(code As String, product As String) As String
        Dim chk = Verify(code, Fingerprint(), product)
        If Not chk.AllowsFullUse Then Return chk.Message
        SaveToken(code)
        SetReg(BlockedName(product), "")
        TouchLastSeen()
        Return ""
    End Function

    ''' <summary>Quietly asks the server for a fresh token (a renewal arrives this way). Network trouble changes nothing.</summary>
    Private Shared Sub TryRefresh(product As String)
        Dim key = StoredKey()
        If key = "" Then Return
        Try
            Dim r = PostActivate(key, product, 4000)
            If CBool(r("Success")) Then
                SaveToken(CStr(r("Token")))
                SetReg(BlockedName(product), "")
            Else
                Dim code = If(r("Code") Is Nothing, "", CStr(r("Code")))
                If code = "suspended" OrElse code = "unknown_key" OrElse code = "product_missing" OrElse code = "device_removed" Then
                    SetReg(BlockedName(product), If(r("Msg") Is Nothing, code, CStr(r("Msg"))))
                End If
            End If
        Catch
            ' offline: keep the saved license
        End Try
    End Sub

    ' ------------------------------------------------------------ what the app calls
    ''' <summary>Verifies the saved license (no network).</summary>
    Public Shared Function CheckStored(product As String) As LicenseResult
        Dim blocked = GetReg(BlockedName(product))
        If blocked <> "" Then Return Bad(blocked)
        Dim token = StoredToken()
        If token = "" Then Return Bad("This system is not activated yet.")
        If ClockWentBack() Then Return Bad("The computer clock was set back. Connect to the internet or contact support to re-activate.")
        Dim r = Verify(token, Fingerprint(), product)
        If r.State <> LicenseState.Invalid Then TouchLastSeen()
        Return r
    End Function

    ''' <summary>At start: refresh from the server if possible, then verify.</summary>
    Public Shared Function Startup(product As String) As LicenseResult
        TryRefresh(product)
        Return CheckStored(product)
    End Function
End Class

''' <summary>Start-up gate: activation screen unless the license allows the app to run.</summary>
Public NotInheritable Class LicenseGuard
    Private Sub New()
    End Sub

    ''' <summary>True = go on. False = the app must close. [role] is SERVER / CLIENT / ORDER from SerSettings.xml.</summary>
    Public Shared Function Enforce(role As String) As Boolean
        Dim product = ClsLicense.ProductForRole(role)
        Dim r = ClsLicense.Startup(product)
        If Not r.AllowsFullUse Then
            Using f As New frmLicense(product, r.Message)
                f.ShowDialog()
            End Using
            r = ClsLicense.CheckStored(product)
            If Not r.AllowsFullUse Then Return False
        End If
        If r.State = LicenseState.Expiring OrElse r.State = LicenseState.Grace Then
            MessageBox.Show(r.Message, "License", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
        Return True
    End Function

    ' ---- top "License" button on the login screen + hourly re-check while the app is open ----
    Private Shared _btn As Button
    Private Shared _product As String
    Private Shared _timer As System.Threading.Timer
    Private Shared _ui As SynchronizationContext
    Private Shared _checking As Boolean
    Private Shared _lastWarnDay As Date = Date.MinValue

    ''' <summary>Adds a coloured "License" button at the top of [parent]; click opens the license details. Also starts the hourly re-check.</summary>
    Public Shared Sub AddTopButton(parent As Form, role As String)
        _product = ClsLicense.ProductForRole(role)
        Dim btn As New Button() With {
            .Name = "btnLicense",
            .FlatStyle = FlatStyle.Flat,
            .Size = New Size(130, 22),
            .Location = New Point(578, 12),
            .Anchor = AnchorStyles.Top Or AnchorStyles.Right,
            .Cursor = Cursors.Hand,
            .ForeColor = Color.White,
            .Font = New Font("Segoe UI", 8.5F, FontStyle.Bold),
            .TabStop = False
        }
        btn.FlatAppearance.BorderSize = 0
        AddHandler btn.Click, Sub()
                                  Using f As New frmLicenseInfo(_product)
                                      f.ShowDialog()
                                  End Using
                                  RefreshButton()
                              End Sub
        _btn = btn
        RefreshButton()
        ' Add a 30 px strip at the top for the button instead of covering a field.
        Dim bar As New Panel() With {.Dock = DockStyle.Top, .Height = 30}
        parent.ClientSize = New Size(parent.ClientSize.Width, parent.ClientSize.Height + 30)
        bar.Width = parent.ClientSize.Width
        btn.Location = New Point(bar.Width - btn.Width - 8, 4)
        btn.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        bar.Controls.Add(btn)
        parent.Controls.Add(bar)
        StartMonitor()
    End Sub

    Private Shared Sub RefreshButton()
        If _btn Is Nothing OrElse _btn.IsDisposed Then Return
        Dim r = ClsLicense.CheckStored(_product)
        Select Case r.State
            Case LicenseState.Valid
                _btn.Text = "License: Active"
                _btn.BackColor = Color.SeaGreen
            Case LicenseState.Expiring
                _btn.Text = "License: ends " & r.UntilDate.Value.ToString("dd-MM")
                _btn.BackColor = Color.DarkOrange
            Case LicenseState.Grace
                _btn.Text = "License: GRACE"
                _btn.BackColor = Color.DarkOrange
            Case LicenseState.Expired
                _btn.Text = "License: EXPIRED"
                _btn.BackColor = Color.Firebrick
            Case Else
                _btn.Text = "License: NOT VALID"
                _btn.BackColor = Color.Firebrick
        End Select
    End Sub

    ''' <summary>Every hour: ask the server (renewals arrive by themselves), then warn once a day. It never closes the app
    ''' in the middle of service; an expired or blocked license is enforced the next time the app starts.</summary>
    Private Shared Sub StartMonitor()
        If _timer IsNot Nothing Then Return
        _ui = SynchronizationContext.Current
        _timer = New System.Threading.Timer(AddressOf OnTick, Nothing, TimeSpan.FromHours(1), TimeSpan.FromHours(1))
    End Sub

    Private Shared Sub OnTick(state As Object)
        If _checking Then Return
        _checking = True
        Dim r As LicenseResult = Nothing
        Try
            r = ClsLicense.Startup(_product)
        Catch
        End Try
        Try
            If _ui IsNot Nothing Then
                _ui.Post(Sub(s) OnChecked(r), Nothing)
            Else
                _checking = False
            End If
        Catch
            _checking = False
        End Try
    End Sub

    Private Shared Sub OnChecked(r As LicenseResult)
        _checking = False
        RefreshButton()
        If r Is Nothing OrElse _lastWarnDay = Date.Today Then Return
        If r.AllowsFullUse Then
            If r.State = LicenseState.Valid Then Return
            _lastWarnDay = Date.Today
            MessageBox.Show(r.Message, "License", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            _lastWarnDay = Date.Today
            MessageBox.Show(r.Message & vbCrLf & vbCrLf & "You can keep working now, but the system will not open again until the license is renewed.",
                            "License", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub
End Class
