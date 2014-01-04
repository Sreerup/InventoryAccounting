Public Class frm_userinfo
    Dim ds As New DataSet
    'Public changpass As Boolean
    Dim dsuser As DataSet
    Dim su As String
    Dim uname As Boolean

    Private Sub userinfo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ComboBox2.Items.Add("Admin")
        ComboBox2.Items.Add("Normal")
        Dim s As String
        Dim ob As New Class1
        If Module1.count = 20 Then
            Me.Text = "Change Password"
            TextBox1.Visible = False
            ComboBox2.Visible = False
            Label1.Visible = False
            Label6.Visible = False
            s = "select * from id where usercode='" & Module1.usercode & "'"
            ds = ob.populate(s)

        ElseIf Module1.count = 21 Then
            If Module1.flag = 1 Then
                Me.Text = "New User Information"
                TextBox1.Text = ""
                TextBox2.Text = ""
                TextBox3.Text = ""
                TextBox4.Visible = False
                Label4.Visible = False
                ComboBox2.Text = "Normal"
            ElseIf Module1.flag = 2 Then
                Me.Text = "Edit User Information"
                s = "select * from id where usercode='" & frm_MainForm.dsload.Tables(0).Rows(row).Item(0).ToString & "'"
                ds = ob.populate(s)
                TextBox1.Text = ds.Tables(0).Rows(0).Item(1).ToString
                ComboBox2.Text = ds.Tables(0).Rows(0).Item(2).ToString
            End If
            TextBox1.Select()
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        uname = False
        Dim s1 As String
        Dim code As String
        Dim ob As New Class1
        usercheck()
        If uname = True Then
            Exit Sub
        End If

        If TextBox2.Text = "" Or TextBox3.Text = "" Or Not TextBox2.Text = TextBox3.Text Then
            MsgBox("New password does not match.", MsgBoxStyle.Information, "Password")
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox2.Select()
            Exit Sub
        End If

        If Module1.count = 20 Then
            If TextBox4.Text = "" Or Not TextBox4.Text = ds.Tables(0).Rows(0).Item(3).ToString Then
                MsgBox("Wrong password, Please enter correct existing password.", MsgBoxStyle.Critical, "Password")
                TextBox4.Text = ""
                TextBox4.Select()
                Exit Sub
            End If
            s1 = "update id set password='" & TextBox3.Text & "' where usercode='" & Module1.usercode & "'"
            ob.insert(s1)
            Me.Close()
            frm_ContainerForm.ToolStripLabel1.Text = ""
        End If


        If Module1.count = 21 Then
            If Not ComboBox2.Text = "Admin" And Not ComboBox2.Text = "Normal" Then
                MsgBox("Please select user type.", MsgBoxStyle.Information, "User")
                ComboBox2.Select()
                Exit Sub
            End If

            If TextBox1.Text = "" Then
                MsgBox("Please enter username", MsgBoxStyle.Information, "User")
                TextBox1.Select()
                Exit Sub
            End If

            If Module1.flag = 1 Then
                s1 = "select top 1 usercode from id where usercode like '" & TextBox1.Text.Chars(0) & "%'order by usercode  desc  "
                code = ob.getcode(s1, TextBox1.Text)

                s1 = "insert into id (usercode,username,accesslevel,password) values('" & code & "','" & TextBox1.Text.ToUpper & "','" & ComboBox2.Text & "','" & TextBox3.Text & "')"
                ob.insert(s1)
            End If
        End If
        If Module1.flag = 2 Then
            If TextBox4.Text = "" Or Not TextBox4.Text = ds.Tables(0).Rows(0).Item(3).ToString Then
                MsgBox("Wrong password, Please enter correct existing password.", MsgBoxStyle.Critical, "Password")
                TextBox4.Text = ""
                TextBox4.Select()
                Exit Sub
            Else
                s1 = "update id  set username='" & TextBox1.Text.ToUpper & "',accesslevel='" & ComboBox2.Text & "',password='" & TextBox3.Text & "' where usercode='" & frm_MainForm.dsload.Tables(0).Rows(row).Item(0).ToString & "'"
                ob.insert(s1)
                Me.Close()
            End If
        End If

        ''--------code for refreshing the datadrig view-------------->
        'Module1.frm.Close()
        'Dim frm As New Form1
        'frm.Size = New Size(915, 660)
        'frm.MdiParent = ContainerForm
        'frm.Show()
        'Module1.frm = frm
        'Dim frm1 As New userinfo
        'Module1.userinfo.Close()
        'frm1.MdiParent = ContainerForm

        'If Module1.flag = 1 Then
        '    frm1.Show()
        '    Module1.userinfo = frm1
        'ElseIf Module1.flag = 2 Then
        '    frm1.Close()
        'End If

        frm_MainForm.mainformload()
        'If Module1.flag = 0 Then
        '    TextBox1.Select()
        '    Exit Sub
        'Else
        If Module1.flag = 1 Then
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            ComboBox2.Text = "Normal"
            TextBox1.Select()
            flag = 1
        ElseIf Module1.flag = 2 Then
            Me.Close()
            frm_MainForm.Enabled = True
            flag = 0
        End If

    End Sub

    Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        If Not TextBox1.Text = "" And e.KeyData = Keys.Enter Then
            If Module1.flag = 1 Then
                TextBox2.Select()
            Else
                TextBox4.Select()
            End If
        End If
    End Sub

    Private Sub TextBox2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyDown
        If Not TextBox2.Text = "" And e.KeyData = Keys.Enter Then
            TextBox3.Select()
        End If

    End Sub

    Private Sub TextBox3_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox3.KeyDown
        If Not TextBox3.Text = "" And e.KeyData = Keys.Enter Then
            If TextBox2.Text = TextBox3.Text Then
                If ComboBox2.Visible = True Then
                    ComboBox2.Select()
                Else
                    Button1.Select()
                End If
            Else
                MsgBox("Password does not match, Please enter again", MsgBoxStyle.Critical, "Wrong Password")
                TextBox2.Select()
            End If
        End If

    End Sub



    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
        frm_MainForm.Enabled = True
        If Module1.count = 20 Then
            frm_ContainerForm.ToolStripLabel1.Text = ""
        End If
    End Sub

    Private Sub TextBox4_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox4.KeyDown
        If e.KeyData = Keys.Enter And Not TextBox4.Text = "" Then
            If TextBox4.Text = ds.Tables(0).Rows(0).Item(3).ToString Then
                TextBox2.Select()
            Else
                MsgBox("Wrong assword, Please enter correct existing password.", MsgBoxStyle.Critical, "Wrong Password")
                TextBox4.Text = ""
                TextBox4.Select()
            End If
        End If
    End Sub


    Private Sub ComboBox2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox2.KeyDown
        If e.KeyData = Keys.Enter Then
            If ComboBox2.Text = "" Or Not ComboBox2.SelectedIndex >= 0 Then
                ComboBox2.DroppedDown = True
            Else
                Button1.Select()
            End If
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        usercheck()
    End Sub
    Private Sub usercheck()
        Dim ob As New Class1
        su = "select * from id"
        dsuser = ob.populate(su)
        If dsuser.Tables(0).Rows.Count > 0 Then
            For i = 0 To dsuser.Tables(0).Rows.Count - 1
                If TextBox1.Text.ToUpper = dsuser.Tables(0).Rows(i).Item(1).ToString.ToUpper Then
                    If Module1.flag = 1 Then
                        MsgBox("User already exist, Please enter new User", MsgBoxStyle.Information, "User Exist")
                        TextBox1.Select()
                        uname = True
                    ElseIf Module1.flag = 2 And Not TextBox1.Text.ToUpper = ds.Tables(0).Rows(0).Item(1).ToString.ToUpper Then
                        MsgBox("User already exist, Please enter new User", MsgBoxStyle.Information, "User Exist")
                        TextBox1.Select()
                        uname = True
                    End If
                End If
            Next
        End If
    End Sub
    Private Sub userinfo_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        frm_MainForm.Enabled = True
    End Sub

End Class