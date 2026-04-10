Imports System.Data
Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Imports System.Drawing.Printing
Imports System.String
Imports System.Text
Imports System.IO
Imports System.Runtime.InteropServices
Module BarcodePrint

    Dim info As New ProcessStartInfo()
    ReadOnly ErrorMsg As String = String.Empty
    Private RichTextBox1 As New RichTextBox
    Private RichTextBox2 As New RichTextBox
    Private Function BarcodeCreate(ByVal QtyTxt As String, ByRef ErrorMsg As String) As Boolean
        Try

            Dim _SqlParameter(0) As SqlParameter
            _SqlParameter(0) = New SqlParameter("@Customer", QtyTxt)
            ' dbcls._ExecuteNonQuery("Barcodegen_insert", _SqlParameter)
            Return True
        Catch ex As Exception
            ErrorMsg = "BarcodeCreate " & Environment.NewLine & ex.Message
            Return False
        End Try

    End Function

    Private Function BarcodeGetData(ByRef ErrorMsg As String, ByRef _DataSet As DataSet) As Boolean
        Try
            Dim ds As New DataSet
            'ds = dbcls._sqlDataAdapter("BarcodeData", Nothing)
            _DataSet = ds
            Return True
        Catch ex As Exception
            ErrorMsg = "BarcodeGetData " & Environment.NewLine & ex.Message
            Return False
        End Try
    End Function

    Private Function GetPRNtxt(ByVal BarcodeName As String, ByRef PRNtxt As String, ByRef ErrorMsg As String) As Boolean
        Try

            Dim Txt As String = String.Empty
            Dim dsbarcode As New DataSet
            Dim _SqlParameter(0) As SqlParameter
            _SqlParameter(0) = New SqlParameter("@BarcodeName", BarcodeName)
            '  dsbarcode = dbcls._sqlDataAdapter("[BarcodeLabel_select]", _SqlParameter)

            If dsbarcode IsNot Nothing Then

                Txt = dsbarcode.Tables(0).Rows(0).Item("Barcode")

                If Txt <> "" Then
                    PRNtxt = Txt
                    Return True
                Else
                    ErrorMsg = "PRN is Empty"
                    Return False
                End If


            Else

                ErrorMsg = "No Data in Database"
                Return False

            End If
            Return True
        Catch ex As Exception
            ErrorMsg = "DT2PRN " & Environment.NewLine & ex.Message
            Return False
        End Try
    End Function

    Public Function Print2PRN(ByVal MaterialList As String, ByVal LabelName As String, ByVal PrinterName As String, ByRef ErrorMsg As String, Optional ByVal PCKDate As String = "", Optional ByVal Expdate As String = "") As Boolean
        Try

            Dim barcodelabel As String = ""
            Dim barcodetxt As String = ""
            Dim ErMsg As String = String.Empty
            Dim str As String = String.Empty
            Dim _txt As New StringBuilder
            Dim _dataset As New DataSet
            Dim _PCKDate As String = PCKDate
            Dim _ExpDate As String = Expdate

            If BarcodeCreate(MaterialList, ErMsg) = False Then
                ErrorMsg = ErMsg
                Return False
            End If

            If BarcodeGetData(ErrorMsg, _dataset) = False Then
                ErrorMsg = ErMsg
                Return False
            End If

            If GetPRNtxt(LabelName, str, ErMsg) = False Then
                ErrorMsg = ErMsg
                Return False
            End If



            'Dim errstr As String = String.Empty

            RichTextBox1.Text = str
            Dim rch As New RichTextBox

            Dim i As Integer

            Dim j As Integer = 0
            Dim flag As String = "S"
            Dim jflag As String = "S"
            Dim barcodelabel1 As String = ""
            Dim r As Integer = 0
            Dim k As Integer = 0
            Dim BarTxt As String = String.Empty
            Dim printflag As String = "N"
            Dim expdatetxt As String = String.Empty
            For j = 0 To _dataset.Tables(0).Rows.Count - 1
                jflag = "S"
                For i = 0 To RichTextBox1.Lines.Length - 1
                    'MsgBox(RichTextBox1.Lines(i))
                    str = RichTextBox1.Lines(i)
                    'If jflag = "S" And j > 0 Then
                    '    j = j - 1
                    'End If

                    If str = "<New>" And flag = "NO" And jflag <> "S" Then

                        j = j + 1
                        If j > _dataset.Tables(0).Rows.Count - 1 Then

                            k = i
                            For k = i - 1 To RichTextBox1.Lines.Length - 1

                                str = RichTextBox1.Lines(k)
                                RichTextBox2.Text = RichTextBox2.Text & Environment.NewLine & str

                            Next


                            Exit For
                        End If

                    End If

                    If str = "<End>" Then
                        flag = "NO"
                    End If
                    If str = "<End>" Then
                        jflag = "N"
                    End If

                    If _dataset.Tables(0).Rows(j).Item("ExpDate") Is DBNull.Value Then
                        expdatetxt = _ExpDate
                    Else
                        expdatetxt = _dataset.Tables(0).Rows(j).Item("ExpDate")
                    End If

                    '.Replace("<S_Shortname>", _dataset.Tables(0).Rows(j).Item("S_Shortname").ToString) _
                    str = RichTextBox1.Lines(i).Replace("<Material_no>", _dataset.Tables(0).Rows(j).Item("MBarcode").ToString) _
                               .Replace("<MRPRate>", _dataset.Tables(0).Rows(j).Item("MRPRate").ToString) _
                               .Replace("<BatchCode>", _dataset.Tables(0).Rows(j).Item("BatchCode").ToString) _
                               .Replace("<ProductName>", _dataset.Tables(0).Rows(j).Item("MaterialName").ToString) _
                               .Replace("<S_ShortName>", _dataset.Tables(0).Rows(j).Item("ShortName").ToString) _
                               .Replace("<ProductPrice>", _dataset.Tables(0).Rows(j).Item("SalesRate").ToString) _
                               .Replace("<ProductColor>", _dataset.Tables(0).Rows(j).Item("C_Colorname").ToString) _
                               .Replace("<ProductShortColor>", _dataset.Tables(0).Rows(j).Item("ClShortName").ToString) _
                               .Replace("<BrandName>", _dataset.Tables(0).Rows(j).Item("BrandName").ToString) _
                               .Replace("<FixedPrice>", _dataset.Tables(0).Rows(j).Item("FixedPrice").ToString) _
                               .Replace("<BrandShortName>", _dataset.Tables(0).Rows(j).Item("BShortName").ToString) _
                               .Replace("<DesignName>", _dataset.Tables(0).Rows(j).Item("DesignName").ToString) _
                               .Replace("<DesignShortName>", _dataset.Tables(0).Rows(j).Item("DShortName").ToString) _
                               .Replace("<MaterialCategory>", _dataset.Tables(0).Rows(j).Item("MaterialCategory").ToString) _
                               .Replace("<CShortName>", _dataset.Tables(0).Rows(j).Item("CShortName").ToString) _
                               .Replace("<FixedPrice>", _dataset.Tables(0).Rows(j).Item("FixedPrice").ToString) _
                               .Replace("<PackingDate>", _PCKDate) _
                               .Replace("<ExpDate>", expdatetxt) _
                               .Replace("<NPrint>", _dataset.Tables(0).Rows(j).Item("B_QTY").ToString)
                    RichTextBox2.Text = RichTextBox2.Text & Environment.NewLine & str






                Next



                barcodelabel1 = RichTextBox2.Text
                RichTextBox2.Text = barcodelabel1.ToString.Replace("<End>", "").Replace("<New>", "")
                Dim txt As String = ""
                For r = 0 To RichTextBox2.Lines.Length - 1

                    str = RichTextBox2.Lines(r).ToString

                    If str <> "" Then
                        txt = txt & Environment.NewLine & str
                    End If

                Next

                RichTextBox2.Text = ""
                RichTextBox2.Text = txt.Replace("<Material_no>", 0) _
                                .Replace("<MRPRate>", 0) _
                               .Replace("<BatchCode>", 0) _
                               .Replace("<ProductName>", 0) _
                               .Replace("<S_Shortname>", 0) _
                               .Replace("<ProductPrice>", 0) _
                               .Replace("<ProductColor>", 0) _
                               .Replace("<ProductShortColor>", 0) _
                               .Replace("<BrandName>", 0) _
                               .Replace("<FixedPrice>", 0) _
                               .Replace("<BrandShortName>", 0) _
                               .Replace("<DesignName>", 0) _
                               .Replace("<DesignShortName>", 0) _
                               .Replace("<MaterialCategory>", 0) _
                               .Replace("<CShortName>", 0) _
                               .Replace("<DressSize>", 0) _
                               .Replace("<PackingDate>", 0) _
                               .Replace("<NPrint>", "1") & Environment.NewLine


                'rch.Text = RichTextBox1.Text
                RichTextBox2.Update()
                'rch.SaveFile("" & Application.StartupPath & "\barcode.txt", RichTextBoxStreamType.PlainText)
                BarTxt = RichTextBox2.Text
                'If Filewirte("" & Application.StartupPath & "\barcode.txt", RichTextBox2.Text, ErrorMsg) = False Then
                '    ' ErrorMsg = errstr
                '    Return False
                'End If

                'Dim kk = RichTextBox2.Text
                'RichTextBox2.Update()
                RichTextBox2.SaveFile(M_Details._appPath & "barcode.txt", RichTextBoxStreamType.PlainText)
                '' _txt.AppendLine(RichTextBox2.Text)
                Dim fname As String
                'Dim printname As String = String.Empty
                fname = M_Details._appPath & "barcode.txt"
                'If RawPrinterHelper.SendStringToPrinter(PrinterName, BarTxt, ErrorMsg) = False Then
                '    Return False
                'End If
                If File.Exists(M_Details._appPath & "barc.bat") Then
                    info.FileName = M_Details._appPath & "barc.bat"
                    info.WorkingDirectory = M_Details._appPath
                    Process.Start(info)
                End If
                RichTextBox2.Text = ""
                barcodelabel1 = ""
                Dim kkk = 0


                'Dim fname As String
                ''Dim printname As String = String.Empty
                'fname = AppPath & "\barcode.txt"
                'If RawPrinterHelper.SendStringToPrinter(PrinterName, BarTxt, ErrorMsg) = False Then
                '    Return False
                'End If
                'RichTextBox2.Text = ""
                'barcodelabel1 = ""
                'Dim kkk = 0
            Next

            Return True
        Catch ex As Exception
            ErrorMsg = ex.Message
            Return False
        End Try

    End Function

    Public Function Print2PRN(ByVal dt As DataTable, ByVal LabelName As String, ByVal PrinterName As String, ByRef ErrorMsg As String, Optional ByVal PCKDate As String = "", Optional ByVal Expdate As String = "") As Boolean
        Try

            Dim barcodelabel As String = ""
            Dim barcodetxt As String = ""
            Dim ErMsg As String = String.Empty
            Dim str As String = String.Empty
            Dim _txt As New StringBuilder
            Dim _dataset As New DataSet
            Dim _PCKDate As String = PCKDate
            Dim _ExpDate As String = Expdate

            _dataset.Tables.Add(dt)

            'If BarcodeCreate(MaterialList, ErMsg) = False Then
            '    ErrorMsg = ErMsg
            '    Return False
            'End If

            'If BarcodeGetData(ErrorMsg, _dataset) = False Then
            '    ErrorMsg = ErMsg
            '    Return False
            'End If

            If GetPRNtxt(LabelName, str, ErMsg) = False Then
                ErrorMsg = ErMsg
                Return False
            End If



            'Dim errstr As String = String.Empty

            RichTextBox1.Text = str
            Dim rch As New RichTextBox

            Dim i As Integer

            Dim j As Integer = 0
            Dim flag As String = "S"
            Dim jflag As String = "S"
            Dim barcodelabel1 As String = ""
            Dim r As Integer = 0
            Dim k As Integer = 0
            Dim BarTxt As String = String.Empty
            Dim printflag As String = "N"
            For j = 0 To _dataset.Tables(0).Rows.Count - 1
                jflag = "S"
                For i = 0 To RichTextBox1.Lines.Length - 1
                    'MsgBox(RichTextBox1.Lines(i))
                    str = RichTextBox1.Lines(i)
                    'If jflag = "S" And j > 0 Then
                    '    j = j - 1
                    'End If

                    If str = "<New>" And flag = "NO" And jflag <> "S" Then

                        j = j + 1
                        If j > _dataset.Tables(0).Rows.Count - 1 Then

                            k = i
                            For k = i - 1 To RichTextBox1.Lines.Length - 1

                                str = RichTextBox1.Lines(k)
                                RichTextBox2.Text = RichTextBox2.Text & Environment.NewLine & str

                            Next




                            Exit For
                        End If

                    End If

                    If str = "<End>" Then
                        flag = "NO"
                    End If
                    If str = "<End>" Then
                        jflag = "N"
                    End If
                    ''.Replace("<S_Shortname>", _dataset.Tables(0).Rows(j).Item("S_Shortname").ToString) _
                    'str = RichTextBox1.Lines(i).Replace("<Material_no>", _dataset.Tables(0).Rows(j).Item("MBarcode").ToString) _
                    '           .Replace("<MRPRate>", _dataset.Tables(0).Rows(j).Item("MRPRate").ToString) _
                    '           .Replace("<BatchCode>", _dataset.Tables(0).Rows(j).Item("BatchCode").ToString) _
                    '           .Replace("<ProductName>", _dataset.Tables(0).Rows(j).Item("MaterialName").ToString) _
                    '           .Replace("<S_ShortName>", _dataset.Tables(0).Rows(j).Item("ShortName").ToString) _
                    '           .Replace("<ProductPrice>", _dataset.Tables(0).Rows(j).Item("SalesRate").ToString) _
                    '           .Replace("<ProductColor>", _dataset.Tables(0).Rows(j).Item("C_Colorname").ToString) _
                    '           .Replace("<ProductShortColor>", _dataset.Tables(0).Rows(j).Item("ClShortName").ToString) _
                    '           .Replace("<BrandName>", _dataset.Tables(0).Rows(j).Item("BrandName").ToString) _
                    '           .Replace("<FixedPrice>", _dataset.Tables(0).Rows(j).Item("FixedPrice").ToString) _
                    '           .Replace("<BrandShortName>", _dataset.Tables(0).Rows(j).Item("BShortName").ToString) _
                    '           .Replace("<DesignName>", _dataset.Tables(0).Rows(j).Item("DesignName").ToString) _
                    '           .Replace("<DesignShortName>", _dataset.Tables(0).Rows(j).Item("DShortName").ToString) _
                    '           .Replace("<MaterialCategory>", _dataset.Tables(0).Rows(j).Item("MaterialCategory").ToString) _
                    '           .Replace("<CShortName>", _dataset.Tables(0).Rows(j).Item("CShortName").ToString) _
                    '           .Replace("<FixedPrice>", _dataset.Tables(0).Rows(j).Item("FixedPrice").ToString) _
                    '           .Replace("<PackingDate>", _PCKDate) _
                    '           .Replace("<ExpDate>", _ExpDate) _
                    '           .Replace("<DressSize>", _dataset.Tables(0).Rows(j).Item("MSize").ToString).Replace("<NPrint>", "1")
                    'RichTextBox2.Text = RichTextBox2.Text & Environment.NewLine & str



                    '.Replace("<S_Shortname>", _dataset.Tables(0).Rows(j).Item("S_Shortname").ToString) _
                    str = RichTextBox1.Lines(i).Replace("<ProductName>", _dataset.Tables(0).Rows(j).Item("MaterialName").ToString) _
                    .Replace("<MaterialCategory>", _dataset.Tables(0).Rows(j).Item("MaterialCategory").ToString) _
                    .Replace("<PackingDate>", _PCKDate) _
                    .Replace("<ExpDate>", _ExpDate) _
                    .Replace("<DressSize>", _dataset.Tables(0).Rows(j).Item("MSize").ToString).Replace("<NPrint>", "1")
                    RichTextBox2.Text = RichTextBox2.Text & Environment.NewLine & str

                    '.Replace("<MRPRate>", _dataset.Tables(0).Rows(j).Item("MRPRate").ToString) _
                    '.Replace("<BatchCode>", _dataset.Tables(0).Rows(j).Item("BatchCode").ToString) _
                    '.Replace("<ProductName>", _dataset.Tables(0).Rows(j).Item("MaterialName").ToString) _
                    '.Replace("<S_ShortName>", _dataset.Tables(0).Rows(j).Item("ShortName").ToString) _
                    '.Replace("<ProductPrice>", _dataset.Tables(0).Rows(j).Item("SalesRate").ToString) _
                    '.Replace("<ProductColor>", _dataset.Tables(0).Rows(j).Item("C_Colorname").ToString) _
                    '.Replace("<ProductShortColor>", _dataset.Tables(0).Rows(j).Item("ClShortName").ToString) _
                    '.Replace("<BrandName>", _dataset.Tables(0).Rows(j).Item("BrandName").ToString) _
                    '.Replace("<FixedPrice>", _dataset.Tables(0).Rows(j).Item("FixedPrice").ToString) _
                    '.Replace("<BrandShortName>", _dataset.Tables(0).Rows(j).Item("BShortName").ToString) _
                    '.Replace("<DesignName>", _dataset.Tables(0).Rows(j).Item("DesignName").ToString) _
                    '.Replace("<DesignShortName>", _dataset.Tables(0).Rows(j).Item("DShortName").ToString) _
                    '.Replace("<MaterialCategory>", _dataset.Tables(0).Rows(j).Item("MaterialCategory").ToString) _
                    '.Replace("<CShortName>", _dataset.Tables(0).Rows(j).Item("CShortName").ToString) _
                    '.Replace("<FixedPrice>", _dataset.Tables(0).Rows(j).Item("FixedPrice").ToString) _
                   



                Next



                barcodelabel1 = RichTextBox2.Text
                RichTextBox2.Text = barcodelabel1.ToString.Replace("<End>", "").Replace("<New>", "")
                Dim txt As String = ""
                For r = 0 To RichTextBox2.Lines.Length - 1

                    str = RichTextBox2.Lines(r).ToString

                    If str <> "" Then
                        txt = txt & Environment.NewLine & str
                    End If

                Next

                RichTextBox2.Text = ""
                RichTextBox2.Text = txt.Replace("<Material_no>", 0) _
                                .Replace("<MRPRate>", 0) _
                               .Replace("<BatchCode>", 0) _
                               .Replace("<ProductName>", 0) _
                               .Replace("<S_Shortname>", 0) _
                               .Replace("<ProductPrice>", 0) _
                               .Replace("<ProductColor>", 0) _
                               .Replace("<ProductShortColor>", 0) _
                               .Replace("<BrandName>", 0) _
                               .Replace("<FixedPrice>", 0) _
                               .Replace("<BrandShortName>", 0) _
                               .Replace("<DesignName>", 0) _
                               .Replace("<DesignShortName>", 0) _
                               .Replace("<MaterialCategory>", 0) _
                               .Replace("<CShortName>", 0) _
                               .Replace("<DressSize>", 0) _
                               .Replace("<PackingDate>", 0) _
                               .Replace("<NPrint>", "1") & Environment.NewLine


                'rch.Text = RichTextBox1.Text
                RichTextBox2.Update()
                'rch.SaveFile("" & Application.StartupPath & "\barcode.txt", RichTextBoxStreamType.PlainText)
                BarTxt = RichTextBox2.Text
                'If Filewirte("" & Application.StartupPath & "\barcode.txt", RichTextBox2.Text, ErrorMsg) = False Then
                '    ' ErrorMsg = errstr
                '    Return False
                'End If

                'Dim kk = RichTextBox2.Text
                'RichTextBox2.Update()
                'RichTextBox2.SaveFile("" & Application.StartupPath & "\barcode.txt", RichTextBoxStreamType.PlainText)
                '' _txt.AppendLine(RichTextBox2.Text)
                Dim fname As String
                'Dim printname As String = String.Empty
                fname = M_Details._appPath & "\barcode.txt"
                'If RawPrinterHelper.SendStringToPrinter(PrinterName, BarTxt, ErrorMsg) = False Then
                '    Return False
                'End If
                RichTextBox2.Text = ""
                barcodelabel1 = ""
                Dim kkk = 0
            Next

            Return True
        Catch ex As Exception
            ErrorMsg = ex.Message
            Return False
        End Try

    End Function

    Private Function Filewirte(ByVal FilePath As String, ByVal Txt As String, ByRef ErrMsg As String) As Boolean
        Try

            If File.Exists(FilePath) Then
                File.Delete(FilePath)
            End If

            Dim S As New FileStream(FilePath, FileMode.CreateNew, FileAccess.ReadWrite)

            Using writer As StreamWriter = New StreamWriter(S)
                writer.Write(Txt)
            End Using
            ' S.Flush()
            S.Close()
            S.Dispose()
            Return True
        Catch ex As Exception
            ErrMsg = ex.Message
            Return False
        End Try
    End Function


    'Public Function DT2PRN(ByRef ErrorMsg As String) As Boolean
    '    Try



    '        Dim HtmlDOC As New HtmlAgilityPack.HtmlDocument
    '        Dim htmltxt As String = String.Empty
    '        GetPRNtxt("label01", htmltxt, ErrorMsg)
    '        HtmlDOC.LoadHtml(htmltxt)
    '        Dim count As Integer = 0
    '        Dim Nodecount As Integer = 0
    '        Dim LabelNodeCollection As HtmlAgilityPack.HtmlNodeCollection = HtmlDOC.DocumentNode.SelectNodes("//label")

    '        If LabelNodeCollection IsNot Nothing Then

    '            Nodecount = LabelNodeCollection.Count

    '        End If




    '        'For Each node As HtmlAgilityPack.HtmlNode In LabelNodeCollection
    '        '    count += 1

    '        'Next


    '        Return True
    '    Catch ex As Exception
    '        Return False
    '    End Try
    'End Function




End Module


