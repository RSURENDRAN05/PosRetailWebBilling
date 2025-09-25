Imports System.Net
Imports System.Text
Imports System.IO
Imports DPUruNet
Imports DPUruNet.Constants
Imports System.Collections.Specialized
Imports Newtonsoft.Json.Linq
Imports Newtonsoft.Json
Imports DPXUru
Public Class FingerprintHelper
    Dim FingerPrintReader As New Form_Main
    ' ============================
    ' Enroll and Save Fingerprint
    ' ============================
 
    Public Function EnrollAndSave(empId As Integer, selectedFingerType As String, selectedFingerName As String, fmdData As Fmd) As Boolean
        Dim dialog As New DevExpress.Utils.WaitDialogForm()
        Try
           
            Dim fmdstr As String = Fmd.SerializeXml(fmdData)
            Dim templateBase64 As String = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(fmdstr))
            dialog.Caption = "Image Data Received"
            ' Call PHP API
            dialog.Caption = "Connecting To Server"
            Dim success As Boolean = SaveFingerprintToServer(empId, templateBase64, selectedFingerType, selectedFingerName)
            ' Basic check
            If success Then
                dialog.Caption = "Data Saved"
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            dialog.Close()
            MessageBox.Show("EnrollAndSave error: " & ex.Message)
            Return False
        Finally
            dialog.Close()
        End Try
    End Function

    Private Function SaveFingerprintToServer(empId As Integer, templateBase64 As String, fingerType As String, fingerName As String) As Boolean
        Try
            ' Validate input parameters
            If empId <= 0 Then
                MessageBox.Show("Invalid employee ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If

            If String.IsNullOrEmpty(templateBase64) Then
                MessageBox.Show("Template data is empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If

            ' Build JSON safely
            Dim jsonData As JObject = New JObject From {
                {"EmpId", empId},
                {"Template", templateBase64},
                {"FingerType", fingerType},
                {"FingerName", fingerName}
            }

            Using client As New WebClient()
                client.Headers(HttpRequestHeader.ContentType) = "application/json"

                ' Upload JSON string
                Dim response As String = client.UploadString(M_Details.LinkAjaxRequest & "AttRequest=1", jsonData.ToString())

                If String.IsNullOrEmpty(response) Then
                    MessageBox.Show("Empty response from server.", "Server Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return False
                End If

                ' Parse JSON response
                Dim parsedResponse As JObject = JObject.Parse(response)
                Dim success As Boolean = parsedResponse("Success").ToObject(Of Boolean)()

                If Not success Then
                    Dim errorMsg As String = If(parsedResponse("Msg") IsNot Nothing, parsedResponse("Msg").ToString(), "Unknown error")
                    MessageBox.Show("Failed to save fingerprint: " & errorMsg, "Server Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return False
                End If

                ' Success
                Return True
            End Using

        Catch ex As Exception
            MessageBox.Show("Network error saving fingerprint: " & ex.Message, "Network Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Private Function GetFingerprintToServer(empId As Integer, templateBase64 As String, fingerType As String, fingerName As String) As Boolean
        Try
            ' Validate input parameters
            If empId <= 0 Then
                MessageBox.Show("Invalid employee ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If

            If String.IsNullOrEmpty(templateBase64) Then
                MessageBox.Show("Template data is empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If


            Dim postData As String = "{""EmpId"":" & empId.ToString() & ",""Template"":""" & templateBase64 & """,""FingerType"":""" & fingerType & """,""FingerName"":""" & fingerName & """}"

            Using client As New WebClient()
                client.Headers(HttpRequestHeader.ContentType) = "application/json"

                Dim response As String = client.UploadString(M_Details.LinkAjaxRequest & "AttRequest=1", postData)

                If String.IsNullOrEmpty(response) Then
                    MessageBox.Show("Empty response from server.", "Server Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return False
                End If

                Dim parsedResponse As JObject = JObject.Parse(response)
                Dim success As Boolean = parsedResponse("Success").ToString().ToLower() = "true"

                If Not success Then
                    Dim errorMsg As String = "Unknown error"
                    If parsedResponse("Msg") IsNot Nothing Then
                        errorMsg = parsedResponse("Msg").ToString()
                    End If
                End If

                Return success
            End Using
        Catch ex As Exception
            MessageBox.Show("Network error saving fingerprint: " & ex.Message, "Network Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    ' ============================
    ' Verify Against All Employees
    ' ============================
    ' 🔹 VerifyFingerprint using probeFmd (already captured)
    Public Function VerifyFingerprint(empId As String, probeFmd As Fmd, fingerType As String, fingerName As String) As Boolean
        Try
            ' ✅ Ensure probeFmd is valid
            If probeFmd Is Nothing Then
                MessageBox.Show("No probe fingerprint provided.")
                Return -1
            End If

            Using client As New WebClient()
                client.Headers(HttpRequestHeader.ContentType) = "application/json"
                Dim postData As String = "{""EmpId"":" & empId.ToString() & ",""FingerName"":""" & fingerName & """,""FingerType"":""" & fingerType & """}"
                Dim response As String = client.UploadString(M_Details.LinkAjaxRequest & "AttRequest=2", postData)
                If String.IsNullOrEmpty(response) Then
                    MessageBox.Show("Empty response from server.", "Server Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return False
                End If

                Dim parsedResponse As JObject = JObject.Parse(response)
                Dim success As Boolean = parsedResponse("Success").ToString().ToLower() = "true"
                If success Then
                    Dim fingerprints As List(Of StoredFmd) = ParseFingerprints(parsedResponse)

                    If fingerprints Is Nothing OrElse fingerprints.Count = 0 Then
                        MessageBox.Show("No fingerprints stored for empId=" & empId)
                        Return -1
                    End If

                    ' ✅ Compare probeFmd with each stored fingerprint
                    For Each sf As StoredFmd In fingerprints
                        ' Decode Base64 → XML → FMD
                        Dim xml As String = Encoding.UTF8.GetString(Convert.FromBase64String(sf.FingerData))
                        Dim storedFmd As Fmd = Fmd.DeserializeXml(xml)

                        Dim compareResult = Comparison.Compare(probeFmd, 0, storedFmd, 0)

                        If compareResult.ResultCode = Constants.ResultCode.DP_SUCCESS Then
                            ' 🔹 Adjust threshold (20000–25000 is common)
                            If compareResult.Score < 20000 Then
                                MessageBox.Show("✅ Match found! Employee ID = " & sf.EmpId)
                                Return True 'sf.EmpId
                            End If
                        End If
                    Next
                Else
                    MessageBox.Show("✅No ID Match found! Employee ID = " & empId)
                    Return False
                End If

            End Using



            MessageBox.Show("❌ No match found for empId=" & empId)
            Return -1

        Catch ex As Exception
            MessageBox.Show("Error verifying fingerprint: " & ex.Message)
            Return -1
        End Try
    End Function


    ' ============================
    ' HTTP Helpers
    ' ============================
    Private Function HttpPost(url As String, postData As String) As String
        Dim data As Byte() = Encoding.UTF8.GetBytes(postData)
        Dim request As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
        request.Method = "POST"
        request.ContentType = "application/x-www-form-urlencoded"
        request.ContentLength = data.Length

        Using stream = request.GetRequestStream()
            stream.Write(data, 0, data.Length)
        End Using

        Using response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
            Using reader As New StreamReader(response.GetResponseStream())
                Return reader.ReadToEnd()
            End Using
        End Using
    End Function

    Private Function HttpGet(url As String) As String
        Dim request As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
        request.Method = "GET"
        Using response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
            Using reader As New StreamReader(response.GetResponseStream())
                Return reader.ReadToEnd()
            End Using
        End Using
    End Function

    ' ============================
    ' Model for JSON Parse
    ' ============================
    Private Class StoredFmd
        Public Property EmpId As Integer
        Public Property FingerData As String
    End Class

    Private Function ParseFingerprints(json As String) As List(Of StoredFmd)
        Dim result As New List(Of StoredFmd)
        Dim arr = Newtonsoft.Json.Linq.JArray.Parse(json)
        For Each item In arr
            result.Add(New StoredFmd With {
                .EmpId = CInt(item("emp_id")),
                .FingerData = item("finger_template").ToString()
            })
        Next
        Return result
    End Function

End Class
