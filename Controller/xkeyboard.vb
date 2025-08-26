Imports System.Speech.Synthesis
Imports PosRetailWebBilling.clssalesProperty

Public Class xkeyboard
    Dim Yes, CtrlYesNo As Boolean, ReadTXT As New SpeechSynthesizer, svfile As New SaveFileDialog, Opdlg As New OpenFileDialog
    Private Sub BackSpace_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BackSpace.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        If Rtext.SelectedText <> "" Then
            Rtext.SelectedText = ""
        ElseIf Rtext.Text <> "" Then
            Rtext.Text = Rtext.Text.Remove(Rtext.Text.Length - 1, 1)
            Yes = True
        End If
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
        Rtext.Refresh()
    End Sub

    Private Sub Num1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Num1.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        If Symbol.ForeColor = Color.Blue And Yes = True Then
            Rtext.Text = Rtext.Text & "!"
        ElseIf Yes = True Then
            Rtext.Text = Rtext.Text & "1"
        ElseIf Symbol.ForeColor = Color.Blue Then
            Rtext.SelectedText = "!"
        Else
            Rtext.SelectedText = "1"
        End If
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub

    Private Sub Num2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Num2.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        If Symbol.ForeColor = Color.Blue And Yes = True Then
            Rtext.Text = Rtext.Text & "@"
        ElseIf Yes = True Then
            Rtext.Text = Rtext.Text & "2"
        ElseIf Symbol.ForeColor = Color.Blue Then
            Rtext.SelectedText = "@"
        Else
            Rtext.SelectedText = "2"
        End If
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub

    Private Sub Num3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Num3.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        If Symbol.ForeColor = Color.Blue And Yes = True Then
            Rtext.Text = Rtext.Text & "#"
        ElseIf Yes = True Then
            Rtext.Text = Rtext.Text & "3"
        ElseIf Symbol.ForeColor = Color.Blue Then
            Rtext.SelectedText = "#"
        Else
            Rtext.SelectedText = "3"
        End If
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub

    Private Sub Num4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Num4.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        If Symbol.ForeColor = Color.Blue And Yes = True Then
            Rtext.Text = Rtext.Text & "$"
        ElseIf Yes = True Then
            Rtext.Text = Rtext.Text & "4"
        ElseIf Symbol.ForeColor = Color.Blue Then
            Rtext.SelectedText = "$"
        Else
            Rtext.SelectedText = "4"
        End If
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub

    Private Sub Num5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Num5.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        If Symbol.ForeColor = Color.Blue And Yes = True Then
            Rtext.Text = Rtext.Text & "%"
        ElseIf Yes = True Then
            Rtext.Text = Rtext.Text & "5"
        ElseIf Symbol.ForeColor = Color.Blue Then
            Rtext.SelectedText = "%"
        Else
            Rtext.SelectedText = "5"
        End If
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub

    Private Sub Num6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Num6.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        If Symbol.ForeColor = Color.Blue And Yes = True Then
            Rtext.Text = Rtext.Text & "^"
        ElseIf Yes = True Then
            Rtext.Text = Rtext.Text & "6"
        ElseIf Symbol.ForeColor = Color.Blue Then
            Rtext.SelectedText = "^"
        Else
            Rtext.SelectedText = "6"
        End If
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub

    Private Sub Num7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Num7.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        If Symbol.ForeColor = Color.Blue And Yes = True Then
            Rtext.Text = Rtext.Text & "&"
        ElseIf Yes = True Then
            Rtext.Text = Rtext.Text & "7"
        ElseIf Symbol.ForeColor = Color.Blue Then
            Rtext.SelectedText = "&"
        Else
            Rtext.SelectedText = "7"
        End If
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub

    Private Sub Num8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Num8.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        If Symbol.ForeColor = Color.Blue And Yes = True Then
            Rtext.Text = Rtext.Text & "*"
        ElseIf Yes = True Then
            Rtext.Text = Rtext.Text & "8"
        ElseIf Symbol.ForeColor = Color.Blue Then
            Rtext.SelectedText = "*"
        Else
            Rtext.SelectedText = "8"
        End If
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub

    Private Sub Num9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Num9.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        If Symbol.ForeColor = Color.Blue And Yes = True Then
            Rtext.Text = Rtext.Text & "("
        ElseIf Yes = True Then
            Rtext.Text = Rtext.Text & "9"
        ElseIf Symbol.ForeColor = Color.Blue Then
            Rtext.SelectedText = "("
        Else
            Rtext.SelectedText = "9"
        End If
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub

    Private Sub Num0_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Num0.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        If Symbol.ForeColor = Color.Blue And Yes = True Then
            Rtext.Text = Rtext.Text & ")"
        ElseIf Yes = True Then
            Rtext.Text = Rtext.Text & "0"
        ElseIf Symbol.ForeColor = Color.Blue Then
            Rtext.SelectedText = ")"
        Else
            Rtext.SelectedText = "0"
        End If
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub

    Private Sub Num10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Num10.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        If Symbol.ForeColor = Color.Blue And Yes = True Then
            Rtext.Text = Rtext.Text & "_"
        ElseIf Yes = True Then
            Rtext.Text = Rtext.Text & "-"
        ElseIf Symbol.ForeColor = Color.Blue Then
            Rtext.SelectedText = "_"
        Else
            Rtext.SelectedText = "-"
        End If
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub

    Private Sub Num11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Num11.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        If Symbol.ForeColor = Color.Blue And Yes = True Then
            Rtext.Text = Rtext.Text & "+"
        ElseIf Yes = True Then
            Rtext.Text = Rtext.Text & "="
        ElseIf Symbol.ForeColor = Color.Blue Then
            Rtext.SelectedText = "+"
        Else
            Rtext.SelectedText = "="
        End If
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub

    Private Sub Tab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Tab.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        If Yes = True Then
            Rtext.Text = Rtext.Text & "    "
        ElseIf Yes = False Then
            Rtext.SelectedText = "    "
        End If
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub

    Private Sub Q_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Q.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        If Yes = True And CapsLock.ForeColor = Color.Blue Then
            Rtext.Text = Rtext.Text & "Q"
        ElseIf Yes = True Then
            Rtext.Text = Rtext.Text & "q"
        ElseIf CapsLock.ForeColor = Color.Blue Then
            Rtext.SelectedText = "Q"
        Else
            Rtext.SelectedText = "q"
        End If
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub

    Private Sub W_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles W.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        If Yes = True And CapsLock.ForeColor = Color.Blue Then
            Rtext.Text = Rtext.Text & "W"
        ElseIf Yes = True Then
            Rtext.Text = Rtext.Text & "w"
        ElseIf CapsLock.ForeColor = Color.Blue Then
            Rtext.SelectedText = "W"
        Else
            Rtext.SelectedText = "w"
        End If
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub



    Private Sub R_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles R.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        If Yes = True And CapsLock.ForeColor = Color.Blue Then
            Rtext.Text = Rtext.Text & "R"
        ElseIf Yes = True Then
            Rtext.Text = Rtext.Text & "r"
        ElseIf CapsLock.ForeColor = Color.Blue Then
            Rtext.SelectedText = "R"
        Else
            Rtext.SelectedText = "r"
        End If
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub

    Private Sub T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles T.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        If Yes = True And CapsLock.ForeColor = Color.Blue Then
            Rtext.Text = Rtext.Text & "T"
        ElseIf Yes = True Then
            Rtext.Text = Rtext.Text & "t"
        ElseIf CapsLock.ForeColor = Color.Blue Then
            Rtext.SelectedText = "T"
        Else
            Rtext.SelectedText = "t"
        End If
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub

    Private Sub Y_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Y.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        If Yes = True And CapsLock.ForeColor = Color.Blue Then
            Rtext.Text = Rtext.Text & "Y"
        ElseIf Yes = True Then
            Rtext.Text = Rtext.Text & "y"
        ElseIf CapsLock.ForeColor = Color.Blue Then
            Rtext.SelectedText = "Y"
        Else
            Rtext.SelectedText = "y"
        End If
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub

    Private Sub U_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles U.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        If Yes = True And CapsLock.ForeColor = Color.Blue Then
            Rtext.Text = Rtext.Text & "U"
        ElseIf Yes = True Then
            Rtext.Text = Rtext.Text & "u"
        ElseIf CapsLock.ForeColor = Color.Blue Then
            Rtext.SelectedText = "U"
        Else
            Rtext.SelectedText = "u"
        End If
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub

    Private Sub I_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles I.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        If Yes = True And CapsLock.ForeColor = Color.Blue Then
            Rtext.Text = Rtext.Text & "I"
        ElseIf Yes = True Then
            Rtext.Text = Rtext.Text & "i"
        ElseIf CapsLock.ForeColor = Color.Blue Then
            Rtext.SelectedText = "I"
        Else
            Rtext.SelectedText = "i"
        End If
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub

    Private Sub O_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles O.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        If Yes = True And CapsLock.ForeColor = Color.Blue Then
            Rtext.Text = Rtext.Text & "O"
        ElseIf Yes = True Then
            Rtext.Text = Rtext.Text & "o"
        ElseIf CapsLock.ForeColor = Color.Blue Then
            Rtext.SelectedText = "O"
        Else
            Rtext.SelectedText = "o"
        End If
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub

    Private Sub P_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles P.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        If Yes = True And CapsLock.ForeColor = Color.Blue Then
            Rtext.Text = Rtext.Text & "P"
        ElseIf Yes = True Then
            Rtext.Text = Rtext.Text & "p"
        ElseIf CapsLock.ForeColor = Color.Blue Then
            Rtext.SelectedText = "P"
        Else
            Rtext.SelectedText = "p"
        End If
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub

    Private Sub N10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles N10.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        If Symbol.ForeColor = Color.Blue And Yes = True Then
            Rtext.Text = Rtext.Text & "{"
        ElseIf Yes = True Then
            Rtext.Text = Rtext.Text & "("
        ElseIf Symbol.ForeColor = Color.Blue Then
            Rtext.SelectedText = "{"
        Else
            Rtext.SelectedText = "("
        End If
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub

    Private Sub Ri_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Ri.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        If Symbol.ForeColor = Color.Blue And Yes = True Then
            Rtext.Text = Rtext.Text & "}"
        ElseIf Yes = True Then
            Rtext.Text = Rtext.Text & ")"
        ElseIf Symbol.ForeColor = Color.Blue Then
            Rtext.SelectedText = "}"
        Else
            Rtext.SelectedText = ")"
        End If
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub

    Private Sub BackSlash_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BackSlash.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        If Symbol.ForeColor = Color.Blue And Yes = True Then
            Rtext.Text = Rtext.Text & "|"
        ElseIf Yes = True Then
            Rtext.Text = Rtext.Text & "\"
        ElseIf Symbol.ForeColor = Color.Blue Then
            Rtext.SelectedText = "|"
        Else
            Rtext.SelectedText = "\"
        End If
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub

    Private Sub CapsLock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CapsLock.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        If CapsLock.ForeColor = Color.Black Then
            CapsLock.ForeColor = Color.Blue
            Q.Text = "Q"
            W.Text = "W"
            E1.Text = "E"
            R.Text = "R"
            T.Text = "T"
            Y.Text = "Y"
            U.Text = "U"
            I.Text = "I"
            O.Text = "O"
            P.Text = "P"
            A.Text = "A"
            S.Text = "S"
            D.Text = "D"
            F.Text = "F"
            G.Text = "G"
            H.Text = "H"
            J.Text = "J"
            K.Text = "K"
            L.Text = "L"
            Z.Text = "Z"
            X.Text = "X"
            C.Text = "C"
            V.Text = "V"
            B.Text = "B"
            N.Text = "N"
            M.Text = "M"
        Else
            CapsLock.ForeColor = Color.Black
            Q.Text = "q"
            W.Text = "w"
            E1.Text = "e"
            R.Text = "r"
            T.Text = "t"
            Y.Text = "y"
            U.Text = "u"
            I.Text = "i"
            O.Text = "o"
            P.Text = "p"
            A.Text = "a"
            S.Text = "s"
            D.Text = "d"
            F.Text = "f"
            G.Text = "g"
            H.Text = "h"
            J.Text = "j"
            K.Text = "k"
            L.Text = "l"
            Z.Text = "z"
            X.Text = "x"
            C.Text = "c"
            V.Text = "v"
            B.Text = "b"
            N.Text = "n"
            M.Text = "m"
        End If
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub

    Private Sub A_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles A.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        If Yes = True And CapsLock.ForeColor = Color.Blue Then
            Rtext.Text = Rtext.Text & "A"
        ElseIf Yes = True Then
            Rtext.Text = Rtext.Text & "a"
        ElseIf CapsLock.ForeColor = Color.Blue Then
            Rtext.SelectedText = "A"
        Else
            Rtext.SelectedText = "a"
        End If
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub

    Private Sub S_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles S.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        If Yes = True And CapsLock.ForeColor = Color.Blue Then
            Rtext.Text = Rtext.Text & "S"
        ElseIf Yes = True Then
            Rtext.Text = Rtext.Text & "s"
        ElseIf CapsLock.ForeColor = Color.Blue Then
            Rtext.SelectedText = "S"
        Else
            Rtext.SelectedText = "s"
        End If
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub

    Private Sub D_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles D.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        If Yes = True And CapsLock.ForeColor = Color.Blue Then
            Rtext.Text = Rtext.Text & "D"
        ElseIf Yes = True Then
            Rtext.Text = Rtext.Text & "d"
        ElseIf CapsLock.ForeColor = Color.Blue Then
            Rtext.SelectedText = "D"
        Else
            Rtext.SelectedText = "d"
        End If
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub

    Private Sub F_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles F.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        If Yes = True And CapsLock.ForeColor = Color.Blue Then
            Rtext.Text = Rtext.Text & "F"
        ElseIf Yes = True Then
            Rtext.Text = Rtext.Text & "f"
        ElseIf CapsLock.ForeColor = Color.Blue Then
            Rtext.SelectedText = "F"
        Else
            Rtext.SelectedText = "f"
        End If
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub

    Private Sub G_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles G.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        If Yes = True And CapsLock.ForeColor = Color.Blue Then
            Rtext.Text = Rtext.Text & "G"
        ElseIf Yes = True Then
            Rtext.Text = Rtext.Text & "g"
        ElseIf CapsLock.ForeColor = Color.Blue Then
            Rtext.SelectedText = "G"
        Else
            Rtext.SelectedText = "g"
        End If
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub

    Private Sub H_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles H.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        If Yes = True And CapsLock.ForeColor = Color.Blue Then
            Rtext.Text = Rtext.Text & "H"
        ElseIf Yes = True Then
            Rtext.Text = Rtext.Text & "h"
        ElseIf CapsLock.ForeColor = Color.Blue Then
            Rtext.SelectedText = "H"
        Else
            Rtext.SelectedText = "h"
        End If
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub

    Private Sub J_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles J.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        If Yes = True And CapsLock.ForeColor = Color.Blue Then
            Rtext.Text = Rtext.Text & "J"
        ElseIf Yes = True Then
            Rtext.Text = Rtext.Text & "j"
        ElseIf CapsLock.ForeColor = Color.Blue Then
            Rtext.SelectedText = "J"
        Else
            Rtext.SelectedText = "j"
        End If
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub

    Private Sub K_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles K.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        If Yes = True And CapsLock.ForeColor = Color.Blue Then
            Rtext.Text = Rtext.Text & "K"
        ElseIf Yes = True Then
            Rtext.Text = Rtext.Text & "k"
        ElseIf CapsLock.ForeColor = Color.Blue Then
            Rtext.SelectedText = "K"
        Else
            Rtext.SelectedText = "k"
        End If
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub

    Private Sub L_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles L.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        If Yes = True And CapsLock.ForeColor = Color.Blue Then
            Rtext.Text = Rtext.Text & "L"
        ElseIf Yes = True Then
            Rtext.Text = Rtext.Text & "l"
        ElseIf CapsLock.ForeColor = Color.Blue Then
            Rtext.SelectedText = "L"
        Else
            Rtext.SelectedText = "l"
        End If
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub

    Private Sub n2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles n2.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        If Symbol.ForeColor = Color.Blue And Yes = True Then
            Rtext.Text = Rtext.Text & ":"
        ElseIf Yes = True Then
            Rtext.Text = Rtext.Text & ";"
        ElseIf Symbol.ForeColor = Color.Blue Then
            Rtext.SelectedText = ":"
        Else
            Rtext.SelectedText = ";"
        End If
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub

    Private Sub n1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles n1.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        If Symbol.ForeColor = Color.Blue And Yes = True Then
            Rtext.Text = Rtext.Text & """"
        ElseIf Yes = True Then
            Rtext.Text = Rtext.Text & ","
        ElseIf Symbol.ForeColor = Color.Blue Then
            Rtext.SelectedText = """"
        Else
            Rtext.SelectedText = ","
        End If
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub

    Private Sub Enter1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Enter1.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        properClass.R_TextNumKey = Rtext.Text.ToUpper
        Me.Close()
        'If Yes = True Then
        '    Rtext.Text = Rtext.Text & vbCrLf & ""
        'ElseIf Yes = False Then
        '    Rtext.SelectedText = vbCrLf & ""
        'End If
        'CtrlYesNo = False
        'Ctrl.ForeColor = Color.Black
    End Sub


    Private Sub Z_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Z.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        If Yes = True And CapsLock.ForeColor = Color.Blue Then
            Rtext.Text = Rtext.Text & "Z"
        ElseIf Yes = True Then
            Rtext.Text = Rtext.Text & "z"
        ElseIf CapsLock.ForeColor = Color.Blue Then
            Rtext.SelectedText = "Z"
        Else
            Rtext.SelectedText = "z"
        End If
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub

    Private Sub X_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles X.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        If Yes = True And CapsLock.ForeColor = Color.Blue Then
            Rtext.Text = Rtext.Text & "X"
        ElseIf Yes = True Then
            Rtext.Text = Rtext.Text & "x"
        ElseIf CapsLock.ForeColor = Color.Blue Then
            Rtext.SelectedText = "X"
        ElseIf CtrlYesNo = True And Rtext.SelectedText <> "" Then
            Clipboard.SetText(Rtext.SelectedText)
            Rtext.SelectedText = ""
        Else
            Rtext.SelectedText = "x"
        End If
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub

    Private Sub C_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles C.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        If Yes = True And CapsLock.ForeColor = Color.Blue Then
            Rtext.Text = Rtext.Text & "C"
        ElseIf Yes = True Then
            Rtext.Text = Rtext.Text & "c"
        ElseIf CapsLock.ForeColor = Color.Blue Then
            Rtext.SelectedText = "C"
        ElseIf CtrlYesNo = True And Rtext.SelectedText <> "" Then
            Clipboard.SetText(Rtext.SelectedText)
        Else
            Rtext.SelectedText = "c"
        End If
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub

    Private Sub V_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles V.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        If Yes = True And CapsLock.ForeColor = Color.Blue Then
            Rtext.Text = Rtext.Text & "V"
        ElseIf Yes = True Then
            Rtext.Text = Rtext.Text & "v"
        ElseIf CapsLock.ForeColor = Color.Blue Then
            Rtext.SelectedText = "V"
        ElseIf CtrlYesNo = True And Clipboard.GetText <> "" Then
            Rtext.SelectedText = Clipboard.GetText()
        Else
            Rtext.SelectedText = "v"
        End If
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub

    Private Sub B_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        If Yes = True And CapsLock.ForeColor = Color.Blue Then
            Rtext.Text = Rtext.Text & "B"
        ElseIf Yes = True Then
            Rtext.Text = Rtext.Text & "b"
        ElseIf CapsLock.ForeColor = Color.Blue Then
            Rtext.SelectedText = "B"
        Else
            Rtext.SelectedText = "b"
        End If
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub

    Private Sub N_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles N.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        If Yes = True And CapsLock.ForeColor = Color.Blue Then
            Rtext.Text = Rtext.Text & "N"
        ElseIf Yes = True Then
            Rtext.Text = Rtext.Text & "n"
        ElseIf CapsLock.ForeColor = Color.Blue Then
            Rtext.SelectedText = "N"
        Else
            Rtext.SelectedText = "n"
        End If
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub

    Private Sub M_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles M.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        If Yes = True And CapsLock.ForeColor = Color.Blue Then
            Rtext.Text = Rtext.Text & "M"
        ElseIf Yes = True Then
            Rtext.Text = Rtext.Text & "m"
        ElseIf CapsLock.ForeColor = Color.Blue Then
            Rtext.SelectedText = "M"
        Else
            Rtext.SelectedText = "m"
        End If
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub

    Private Sub N3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles N3.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        If Symbol.ForeColor = Color.Blue And Yes = True Then
            Rtext.Text = Rtext.Text & "<"
        ElseIf Yes = True Then
            Rtext.Text = Rtext.Text & "+"
        ElseIf Symbol.ForeColor = Color.Blue Then
            Rtext.SelectedText = "<"
        Else
            Rtext.SelectedText = "+"
        End If
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub

    Private Sub N4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles N4.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        If Symbol.ForeColor = Color.Blue And Yes = True Then
            Rtext.Text = Rtext.Text & ">"
        ElseIf Yes = True Then
            Rtext.Text = Rtext.Text & "."
        ElseIf Symbol.ForeColor = Color.Blue Then
            Rtext.SelectedText = ">"
        Else
            Rtext.SelectedText = "."
        End If
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub

    Private Sub SLASH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SLASH.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        If Symbol.ForeColor = Color.Blue And Yes = True Then
            Rtext.Text = Rtext.Text & "?"
        ElseIf Yes = True Then
            Rtext.Text = Rtext.Text & "/"
        ElseIf Symbol.ForeColor = Color.Blue Then
            Rtext.SelectedText = "?"
        Else
            Rtext.SelectedText = "/"
        End If
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub


    Private Sub Ctrl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Ctrl.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        If Ctrl.ForeColor = Color.Black Then
            Ctrl.ForeColor = Color.Blue
            CtrlYesNo = True
        ElseIf Ctrl.ForeColor = Color.Blue Then
            Ctrl.ForeColor = Color.Black
            CtrlYesNo = False
        End If
    End Sub

    Private Sub Space_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Space.Click
        'If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
        '    My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        'End If
        If Yes = True Then
            Rtext.Text = Rtext.Text & " "
        Else
            Rtext.SelectedText = " "
        End If
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub

    Private Sub Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Save.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        svfile.FileName = "XKeyboard"
        svfile.Filter = "Rich Text Format|*.rtf|Text File|*.txt"
        svfile.Title = "Save File (Xkeyboard)"
        svfile.ShowDialog()
        If svfile.FileName <> "" Then
            Dim WriteTxT As New System.IO.StreamWriter(svfile.FileName)
            WriteTxT.Write(Rtext.Text)
            WriteTxT.Close()
        End If
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub
    Private Sub Paste_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Paste.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        If Yes = True And Clipboard.GetText <> "" Then
            Rtext.Text = Rtext.Text & Clipboard.GetText
        ElseIf Clipboard.GetText <> "" Then
            Rtext.SelectedText = Clipboard.GetText
        End If
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub

    Private Sub Copy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Copy.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        If Rtext.SelectedText <> "" Then
            Clipboard.SetText(Rtext.SelectedText)
        End If
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub

    Private Sub Cut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cut.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        If Rtext.SelectedText <> "" Then
            Clipboard.SetText(Rtext.SelectedText)
            Rtext.SelectedText = ""
        End If
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub

    Private Sub Rtext_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Rtext.MouseClick
        Yes = False
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub

    Private Sub E1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles E1.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        If Yes = True And CapsLock.ForeColor = Color.Blue Then
            Rtext.Text = Rtext.Text & "E"
        ElseIf Yes = True Then
            Rtext.Text = Rtext.Text & "e"
        ElseIf CapsLock.ForeColor = Color.Blue Then
            Rtext.SelectedText = "E"
        Else
            Rtext.SelectedText = "e"
        End If
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub


    Private Sub Symbol_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Symbol.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        If Symbol.ForeColor = Color.Black Then
            Symbol.ForeColor = Color.Blue
            Num1.Text = "!"
            Num2.Text = "@"
            Num3.Text = "#"
            Num4.Text = "$"
            Num5.Text = "%"
            Num6.Text = "^"
            Num7.Text = "&&"
            Num8.Text = "*"
            Num9.Text = "("
            Num0.Text = ")"
            Num10.Text = "_"
            Num11.Text = "+"
            N10.Text = "{"
            Ri.Text = "}"
            BackSlash.Text = "|"
            n2.Text = ":"
            n1.Text = """"
            N3.Text = "<"
            N4.Text = ">"
            SLASH.Text = "?"
        Else
            Symbol.ForeColor = Color.Black
            Num1.Text = "1"
            Num2.Text = "2"
            Num3.Text = "3"
            Num4.Text = "4"
            Num5.Text = "5"
            Num6.Text = "6"
            Num7.Text = "7"
            Num8.Text = "8"
            Num9.Text = "9"
            Num0.Text = "0"
            Num10.Text = "-"
            Num11.Text = "="
            N10.Text = "["
            Ri.Text = "]"
            BackSlash.Text = "\"
            n2.Text = ";"
            n1.Text = "'"
            N3.Text = ","
            N4.Text = "."
            SLASH.Text = "/"
        End If
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub

    Private Sub RDT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RDT.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        Me.Width = 803
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub


    Private Sub Back_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Back.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
            My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
        End If
        Me.Width = 645
        ReadTXT.SpeakAsyncCancelAll()
    End Sub

    Private Sub Read_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Read.Click
        If Rtext.SelectedText <> "" Then
            ReadTXT.SpeakAsync(Rtext.SelectedText)
        Else
            ReadTXT.SpeakAsync(Rtext.Text)
        End If
    End Sub

    Private Sub RRead_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RRead.ValueChanged
        ReadTXT.Rate = RRead.Value
    End Sub

    'Private Sub xkeyboard_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    '    ReadTXT.SpeakAsyncCancelAll()
    '    If MsgBox("Do You Want To Exit The Program?", vbYesNo + MsgBoxStyle.Critical) = vbYes Then
    '        End
    '    Else
    '        e.Cancel = True
    '    End If
    'End Sub
    Private Sub VolBar_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles VolBar.ValueChanged
        ReadTXT.Volume = VolBar.Value
    End Sub

    Private Sub SPbtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SPbtn.Click
        ReadTXT.SpeakAsyncCancelAll()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        If MsgBox("Do You Want To Exit The Program?", vbYesNo + MsgBoxStyle.Critical) = vbYes Then
            End
        End If
    End Sub

    Private Sub SaveAs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveAs.Click
        svfile.FileName = "XKeyboard"
        svfile.Filter = "Rich Text Format|*.rtf|Text File|*.txt"
        svfile.Title = "Save File (Xkeyboard)"
        svfile.ShowDialog()
        If svfile.FileName <> "" Then
            Dim WriteTxT As New System.IO.StreamWriter(svfile.FileName)
            WriteTxT.Write(Rtext.Text)
            WriteTxT.Close()
        End If
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub

    Private Sub Open1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Open1.Click
        Opdlg.FileName = ""
        Opdlg.Title = "Open File"
        Opdlg.Filter = "Text File|*.txt"
        Opdlg.ShowDialog()
        If Opdlg.FileName <> "" Then
            Dim OPW As New System.IO.StreamReader(Opdlg.FileName)
            Rtext.Text = OPW.ReadToEnd
            OPW.Close()
        End If
    End Sub

    Private Sub ReadTextToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReadTextToolStripMenuItem.Click
        Me.Width = 803
        CtrlYesNo = False
        Ctrl.ForeColor = Color.Black
    End Sub

    Private Sub AboutUsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutUsToolStripMenuItem.Click
        ' AboutBox1.Show()
    End Sub

    Private Sub btnEnd_Click(sender As Object, e As EventArgs) Handles btnEnd.Click
        Me.Close()

    End Sub

    Private Sub xkeyboard_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            CapsLock.ForeColor = Color.Black
            CapsLock_Click(Nothing, Nothing)
            Rtext.Text = properClass.R_TextNumKey
            Rtext.SelectAll()
            properClass.R_TextNumKey = ""
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnclear_Click(sender As Object, e As EventArgs) Handles btnclear.Click
        Try
            Rtext.Text = ""
            Rtext.Focus()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Rtext_KeyDown(sender As Object, e As KeyEventArgs) Handles Rtext.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If My.Computer.FileSystem.FileExists(Application.StartupPath & "\Click.WAV") Then
                    My.Computer.Audio.Play(Application.StartupPath & "\Click.WAV")
                End If
                properClass.R_TextNumKey = Rtext.Text.ToUpper
                Me.Close()
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class