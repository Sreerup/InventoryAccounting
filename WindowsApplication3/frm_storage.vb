Public Class frm_storage
    Dim ds As New DataSet


    Private Sub storage_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load



        If Module1.flag = 2 Then
            If Module1.count = 3 Then
                TextBox1.Text = itemmstmodule.ml.ToString
                TextBox2.Text = itemmstmodule.packing.ToString
            ElseIf Module1.count = 10 Then
                TextBox1.Text = Module1.col1
                TextBox2.Text = Module1.col2
            End If
        End If

        If Module1.count = 3 Then
            TextBox1.MaxLength = 4
            TextBox2.MaxLength = 2
            TextBox2.Multiline = False
            TextBox2.Height = 25
            Label1.Text = "ML"
            Label2.Text = "Packing"
        End If
        TextBox1.Select()

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim s As String
        Dim code As String
        Dim ob As New Class1
        s = Nothing




        If Module1.count = 3 Then
            If Not TextBox1.Text = Nothing And Not TextBox2.Text = Nothing Then
                If flag = 1 Then
                    's = "select top 1 mlcode from itemgroupml where mlcode like '" & TextBox1.Text.Chars(0) & "%'order by mlcode desc "
                    'mlcode = ob.getcode(s, TextBox1.Text)
                    s = "insert into itemgroupml(ml,packing )values('" & TextBox1.Text & "','" & TextBox2.Text & "')"
                ElseIf flag = 2 Then
                    s = "update itemgroupml set ml='" & TextBox1.Text & "',packing ='" & TextBox2.Text & "' where ml='" & itemmstmodule.ml & "'"
                End If
            Else
                MsgBox("Fields can not be blank", MsgBoxStyle.Critical, "Blank Fields")
                TextBox1.Select()
                Exit Sub
            End If
        ElseIf Module1.count = 10 Then
            If Not TextBox1.Text = Nothing Then
                If Module1.flag = 1 Then
                    s = "select top 1 shopcode from storage where shopcode like '" & TextBox1.Text.ToUpper.Chars(0) & "%'order by shopcode desc "
                    code = ob.getcode(s, TextBox1.Text.ToUpper)
                    s = "insert into storage (companycode,shopcode,shopname,address) values ('" & Module1.companycode.ToUpper & "','" & code.ToUpper & "','" & TextBox1.Text.ToUpper & "','" & TextBox2.Text.ToUpper & "') "
                ElseIf Module1.flag = 2 Then
                    s = "update storage set shopname='" & TextBox1.Text.ToUpper & "',address='" & TextBox2.Text.ToUpper & "' where shopcode='" & Module1.shopcode.ToUpper & "' and companycode='" & Module1.companycode & "'"
                End If
            Else
                MsgBox("Fields can not be blank", MsgBoxStyle.Critical, "Blank Fields")
                TextBox1.Select()
                Exit Sub
            End If
        End If
        ob.insert(s)


        frm_MainForm.mainformload()
        If Module1.flag = 0 Then
            TextBox1.Select()
            Exit Sub
        ElseIf Module1.flag = 1 Then
            TextBox1.Text = ""
            TextBox2.Text = ""
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

    Private Sub TextBox1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyData = Keys.Enter Then
            TextBox2.Focus()
        End If
    End Sub

    Private Sub TextBox2_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyData = Keys.Enter Then
            Button1.Focus()
        End If
    End Sub
    Private Sub storage_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        frm_MainForm.Enabled = True
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If Module1.count = 3 Then

            Select Case e.KeyChar
                Case "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", vbBack
                    e.Handled = False
                Case Else
                    e.Handled = True
            End Select
        ElseIf Module1.count = 10 And TextBox1.Text = "DEFAULT STORE" And Module1.flag = 2 Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        If Module1.count = 3 Then
            Select Case e.KeyChar
                Case "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", vbBack
                    e.Handled = False
                Case Else
                    e.Handled = True
            End Select
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub
End Class