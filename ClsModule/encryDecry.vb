Module encryDecry
    Public Function enCry(ByRef strrectext As String) As String
        Try
            Dim strpwd As String
            Dim strtext As String
            strpwd = ""
            strtext = strrectext
            Dim str As String = ""
            If (Strings.Len(strpwd) = 0) Then
                strpwd = "p@ssw0rd05Ruthr@m1986"
            End If
            strpwd = Strings.UCase(strpwd)
            If (Strings.Len(strpwd) > 0) Then
                Dim num3 As Integer = Strings.Len(strtext)
                Dim i As Integer = 1
                Do While (i <= num3)
                    Dim num As Integer = (Strings.Asc(Strings.Mid(strtext, i, 1)) + Strings.Asc(Strings.Mid(strpwd, ((i Mod Strings.Len(strpwd)) + 1), 1)))
                    str = (str & Strings.Chr((num And &HFF)).ToString)
                    i += 1
                Loop

                'txtres.Text = str.ToString()
                'txtdec.Text = str.ToString()

            End If
            Return str
        Catch ex As Exception
            Return "0"
        End Try
    End Function
    Public Function deCry(ByRef strrectext As String) As String
        Try
            Dim str As String = ""
            Dim strpwd As String
            Dim strtext As String
            strpwd = ""
            strtext = strrectext
            If (strpwd = "") Then
                strpwd = "p@ssw0rd05Ruthr@m1986"
            End If
            strpwd = Strings.UCase(strpwd)
            If (Strings.Len(strpwd) > 0) Then
                Dim num3 As Integer = Strings.Len(strtext)
                Dim i As Integer = 1
                Do While (i <= num3)
                    Dim num As Integer = (Strings.Asc(Strings.Mid(strtext, i, 1)) - Strings.Asc(Strings.Mid(strpwd, ((i Mod Strings.Len(strpwd)) + 1), 1)))
                    str = (str & Convert.ToString(Strings.ChrW((num And &HFF))))
                    i += 1
                Loop
                Return str
                ' txtresdec.Text = str.ToString()
            End If
            Return strtext
        Catch ex As Exception
            Return "0"
        End Try
    End Function
End Module

