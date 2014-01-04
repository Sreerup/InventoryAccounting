
Public Class frm_bedit
    Dim editname As Boolean
    Private Sub bedit_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        editname = False
        If Module1.flag = 2 Then
            If Module1.count = 1 Or Module1.count = 2 Or Module1.count = 4 Then
                TextBox1.Text = Module1.col2
            End If
            If Module1.count = 6 Then
                TextBox1.Text = itemmstmodule.strengthname
            End If
            Button1.Text = "&Update"
        End If
        If Module1.count = 1 Then
            Me.Text = "Brand Master"
            Label1.Text = "Brand Name"
        ElseIf Module1.count = 2 Then
            Me.Text = "KFL Master"
            Label1.Text = "KFL Name"
        ElseIf Module1.count = 4 Then
            Me.Text = "Category Master"
            Label1.Text = "Category Name"
        ElseIf Module1.count = 6 Then
            Me.Text = "Strength Master"
            Label1.Text = "Strength"
        End If
        TextBox1.Select()
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Trim(TextBox1.Text) = Nothing Or Trim(TextBox1.Text) = "0" Then
            Exit Sub
        End If

        editcheck()
        If editname = True Then
            TextBox1.Select()
            Exit Sub
        End If

        Dim s As String
        Dim ob As New Class1
        s = Nothing
        If Module1.flag = 1 Then
            Dim s1 As String
            Dim code As String
            If Module1.count = 1 Then
                s1 = "select top 1 categorycode from CategoryMst where categorycode like '" & TextBox1.Text.Chars(0) & "%'order by categorycode desc "
                code = ob.getcode(s1, TextBox1.Text)
                s = "insert into CategoryMst (companycode,categoryCode,CategoryName) values ('" & Module1.companycode & "','" & code & "','" & TextBox1.Text.ToUpper & "') "
            ElseIf Module1.count = 2 Then
                s1 = "select top 1 kflcode from kflmst where kflcode like '" & TextBox1.Text.Chars(0) & "%'order by kflcode desc "
                code = ob.getcode(s1, TextBox1.Text)
                s = "insert into KflMst(companycode,kflcode,kflname )values('" & Module1.companycode & "','" & code & "','" & TextBox1.Text.ToUpper & "') "
            ElseIf Module1.count = 4 Then
                s1 = "select top 1 groupcode from groupmst where groupcode like '" & TextBox1.Text.Chars(0) & "%'order by groupcode desc "
                code = ob.getcode(s1, TextBox1.Text)
                s = "insert into groupMst(companycode,groupcode,groupname )values('" & Module1.companycode & "','" & code & "','" & TextBox1.Text.ToUpper & "') "
            ElseIf Module1.count = 6 Then
                's1 = "select top 1 strengthcode from strength where strengthcode like '" & TextBox1.Text.Chars(0) & "%'order by strengthcode desc "
                'code = ob.getcode(s1, TextBox1.Text)
                s = "insert into strength(strengthname )values('" & TextBox1.Text.ToUpper & "') "
            End If
        ElseIf Module1.flag = 2 Then
            If Module1.count = 1 Then
                s = "update categorymst set categoryname='" & TextBox1.Text.ToUpper & "' where categorycode='" & Module1.col1 & "'"
            ElseIf Module1.count = 2 Then
                s = "update kflmst set kflname='" & TextBox1.Text.ToUpper & "' where kflcode='" & Module1.col1 & "'"
            ElseIf Module1.count = 4 Then
                s = "update groupmst set groupname='" & TextBox1.Text.ToUpper & "' where groupcode='" & Module1.col1 & "'"
            ElseIf Module1.count = 6 Then
                s = "update strength set strengthname='" & TextBox1.Text.ToUpper & "' where strengthname='" & itemmstmodule.strengthname & "'"
            End If
        End If
        ob.insert(s)
        '--------code for refreshing the datadrig view-------------->

        frm_MainForm.mainformload()
        If Module1.flag = 0 Then
            TextBox1.Select()
            Exit Sub
        ElseIf Module1.flag = 1 Then
            TextBox1.Text = ""
            TextBox1.Select()
        ElseIf Module1.flag = 2 Then
            Me.Close()
            frm_MainForm.Enabled = True
        End If
    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
        frm_MainForm.Enabled = True
    End Sub
    Private Sub bedit_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        frm_MainForm.Enabled = True
    End Sub
    Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyData = Keys.Enter And Not TextBox1.Text = "" Then
            editcheck()
            If editname = True Then
                TextBox1.Select()
                Exit Sub
            Else
                Button1.Select()
            End If
        End If
    End Sub

    Private Sub editcheck()
        Dim s As String
        Dim ob As New Class1
        s = Nothing
        If Module1.flag = 1 Then
            If Module1.count = 1 Then
                s = "select categoryname from categorymst where companycode='" & Module1.companycode & "' and categoryname='" & TextBox1.Text & "'"
            ElseIf Module1.count = 2 Then
                s = "select kflname from kflmst where companycode='" & Module1.companycode & "' and kflname='" & TextBox1.Text & "'"
            ElseIf Module1.count = 4 Then
                s = "select groupname from groupmst where companycode='" & Module1.companycode & "' and groupname='" & TextBox1.Text & "'"
            ElseIf Module1.count = 6 Then
                s = "select strengthname from strength where strengthname='" & TextBox1.Text & "'"
            End If
            s = ob.executereader(s)
            If s = Nothing Then
                editname = False
            Else
                GoTo msg
            End If
        ElseIf Module1.flag = 2 Then
            If Module1.col2 <> TextBox1.Text Then

                If Module1.count = 1 Then
                    s = "select categoryname from categorymst where companycode='" & Module1.companycode & "' and categoryname='" & TextBox1.Text & "'"
                ElseIf Module1.count = 2 Then
                    s = "select kflname from kflmst where companycode='" & Module1.companycode & "' and kflname='" & TextBox1.Text & "'"
                ElseIf Module1.count = 4 Then
                    s = "select groupname from groupmst where companycode='" & Module1.companycode & "' and groupname='" & TextBox1.Text & "'"
                ElseIf Module1.count = 6 Then
                    s = "select strengthname from strength where companycode='" & Module1.companycode & "' and strengthname='" & TextBox1.Text & "'"
                End If
                s = ob.executereader(s)
                If s = Nothing Then
                    editname = False
                Else
msg:                MsgBox("Duplicate entry, please enter distinct data", MsgBoxStyle.Information, "Duplicate")
                    editname = True
                    Exit Sub
                End If
            Else
            End If
        End If

    End Sub
End Class