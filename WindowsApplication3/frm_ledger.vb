Public Class frm_ledger

    Dim ds As New DataSet
    Dim actype As String

    Private Sub ledger_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        frmledgerload()
    End Sub

    Private Sub frmledgerload()

        actype = Nothing
        '-----populating a dataset--------------------------------->
        Dim ob As New Class1
        Dim s As String
        s = "select accode,acname from  acountname"
        ds = ob.populate(s)
        '-------populating the combobox with the company names
        ComboBox1.Items.Clear()
        ob.combofill(ds, ComboBox1)

        '---code to run when the user opens the form in edit mode ----->
        If Module1.flag = 2 Then
            TextBox1.Text = Module1.ledgername
            TextBox3.Text = Module1.address2
            TextBox4.Text = Module1.contactperson
            TextBox5.Text = Module1.panno
            TextBox6.Text = Module1.address1
            TextBox7.Text = Module1.city
            TextBox8.Text = Module1.vatno
            TextBox9.Text = Module1.area
            TextBox10.Text = Module1.district
            TextBox11.Text = Module1.state
            TextBox12.Text = Module1.phone
            TextBox13.Text = Module1.email
            TextBox14.Text = Module1.www
            ComboBox1.Text = Module1.acountname

            If Not Module1.credit = "0" Then
                TextBox2.Text = Module1.credit.ToString
                actype = "CREDIT"
            Else
                TextBox2.Text = Module1.debit.ToString
                actype = "DEBIT"
            End If
        Else
            ComboBox1.Text = ds.Tables(0).Rows(0).Item(1).ToString
            TextBox1.Text = ""
            TextBox2.Text = "0"
            TextBox3.Text = ""
            TextBox4.Text = ""
            TextBox5.Text = ""
            TextBox6.Text = ""
            TextBox7.Text = ""
            TextBox8.Text = ""
            TextBox9.Text = ""
            TextBox10.Text = ""
            TextBox11.Text = ""
            TextBox12.Text = ""
            TextBox13.Text = ""

        End If
        TextBox1.Select()

    End Sub



    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim s As String
        Dim ob As New Class1
        Dim code As String
        s = Nothing
        If Module1.flag = 1 Then
            s = "select top 1 ledcode from ledger where name like '" & TextBox1.Text.Chars(0).ToString & "%' order by ledcode desc"
            code = ob.getcode(s, TextBox1.Text)
            If RadioButton1.Checked = True Then
                s = "insert into ledger (name,accode,contperson,address1,area,city,district,state,phone,email,www,address2,debit,vatno,panno,ledcode,companycode,credit,type) values('" & TextBox1.Text.ToUpper & "','" & Module1.acountcode.ToUpper & "','" & TextBox4.Text.ToUpper & "','" & TextBox6.Text.ToUpper & "','" & TextBox9.Text.ToUpper & "','" & TextBox7.Text.ToUpper & "','" & TextBox10.Text.ToUpper & "','" & TextBox11.Text.ToUpper & "','" & TextBox12.Text.ToUpper & "','" & TextBox13.Text.ToUpper & "','" & TextBox14.Text.ToUpper & "','" & TextBox3.Text.ToUpper & "', " & TextBox2.Text & " ,'" & TextBox8.Text.ToUpper & "','" & TextBox5.Text.ToUpper & "','" & code.ToUpper & "','" & Module1.companycode.ToUpper & "','','" & actype & "')"
            Else
                s = "insert into ledger (name,accode,contperson,address1,area,city,district,state,phone,email,www,address2,credit,vatno,panno,ledcode,companycode,debit,type) values('" & TextBox1.Text.ToUpper & "','" & Module1.acountcode.ToUpper & "','" & TextBox4.Text.ToUpper & "','" & TextBox6.Text.ToUpper & "','" & TextBox9.Text.ToUpper & "','" & TextBox7.Text.ToUpper & "','" & TextBox10.Text.ToUpper & "','" & TextBox11.Text.ToUpper & "','" & TextBox12.Text.ToUpper & "','" & TextBox13.Text.ToUpper & "','" & TextBox14.Text.ToUpper & "','" & TextBox3.Text.ToUpper & "', " & TextBox2.Text & " ,'" & TextBox8.Text.ToUpper & "','" & TextBox5.Text.ToUpper & "','" & code.ToUpper & "','" & Module1.companycode.ToUpper & "','','" & actype & "')"
            End If
        ElseIf Module1.flag = 2 Then
            If RadioButton1.Checked = True Then
                s = "update ledger set name='" & TextBox1.Text.ToUpper & "',accode='" & Module1.acountcode & "',address1='" & TextBox6.Text.ToUpper & "',area='" & TextBox9.Text.ToUpper & "',city='" & TextBox7.Text.ToUpper & "',district='" & TextBox10.Text.ToUpper & "',state='" & TextBox11.Text.ToUpper & "',phone='" & TextBox12.Text.ToUpper & "',email='" & TextBox13.Text.ToUpper & "',www='" & TextBox14.Text.ToUpper & "',address2='" & TextBox3.Text.ToUpper & "',debit='" & TextBox2.Text & "',vatno='" & TextBox8.Text.ToUpper & "',panno='" & TextBox5.Text.ToUpper & "', contperson='" & TextBox4.Text.ToUpper & "',credit='',type='" & actype & "' where ledcode='" & Module1.ledgercode & "' and companycode='" & Module1.companycode.ToUpper & "'"
            Else
                s = "update ledger set name='" & TextBox1.Text.ToUpper & "',accode='" & Module1.acountcode & "',address1='" & TextBox6.Text.ToUpper & "',area='" & TextBox9.Text.ToUpper & "',city='" & TextBox7.Text.ToUpper & "',district='" & TextBox10.Text.ToUpper & "',state='" & TextBox11.Text.ToUpper & "',phone='" & TextBox12.Text.ToUpper & "',email='" & TextBox13.Text.ToUpper & "',www='" & TextBox14.Text.ToUpper & "',address2='" & TextBox3.Text.ToUpper & "',credit='" & TextBox2.Text & "',vatno='" & TextBox8.Text.ToUpper & "',panno='" & TextBox5.Text.ToUpper & "', contperson='" & TextBox4.Text.ToUpper & "',debit='',type='" & actype & "' where ledcode='" & Module1.ledgercode & "' and companycode='" & Module1.companycode.ToUpper & "'"
            End If
        End If
        ob.insert(s)

        frm_MainForm.mainformload()
        If Module1.flag = 1 Then
            frmledgerload()
        ElseIf Module1.flag = 2 Then
            Me.Close()
            frm_MainForm.Enabled = True
        End If

    End Sub


    '----selcted index change of the combobox ------>
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedIndex >= 0 Then
            Dim quey = From p As DataRow In ds.Tables(0) Where p(1) = ComboBox1.Text Select p(0)
            Module1.acountcode = quey(0).ToString
        End If
    End Sub

    Private Sub ComboBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox1.KeyDown
        If e.KeyData = Keys.Enter Then
            If ComboBox1.SelectedIndex >= 0 Then
                Dim l As Integer = Len(Trim(ComboBox1.Text.ToUpper))
                Dim strled As String = Mid(Trim(ComboBox1.Text.ToUpper), l - 8, 9)
                If strled = "CREDITORS" Then
                    Label4.Enabled = True
                    Label5.Enabled = True
                    Label6.Enabled = True
                    Label7.Enabled = True
                    Label8.Enabled = True
                    Label9.Enabled = True
                    Label10.Enabled = True
                    Label11.Enabled = True
                    Label12.Enabled = True
                    Label13.Enabled = True
                    Label14.Enabled = True
                    TextBox3.Enabled = True
                    TextBox4.Enabled = True
                    TextBox5.Enabled = True
                    TextBox6.Enabled = True
                    TextBox7.Enabled = True
                    TextBox8.Enabled = True
                    TextBox9.Enabled = True
                    TextBox10.Enabled = True
                    TextBox11.Enabled = True
                    TextBox12.Enabled = True
                    TextBox13.Enabled = True
                    TextBox14.Enabled = True
                Else
                    Label4.Enabled = False
                    Label5.Enabled = False
                    Label6.Enabled = False
                    Label7.Enabled = False
                    Label8.Enabled = False
                    Label9.Enabled = False
                    Label10.Enabled = False
                    Label11.Enabled = False
                    Label12.Enabled = False
                    Label13.Enabled = False
                    Label14.Enabled = False
                    TextBox3.Enabled = False
                    TextBox4.Enabled = False
                    TextBox5.Enabled = False
                    TextBox6.Enabled = False
                    TextBox7.Enabled = False
                    TextBox8.Enabled = False
                    TextBox9.Enabled = False
                    TextBox10.Enabled = False
                    TextBox11.Enabled = False
                    TextBox12.Enabled = False
                    TextBox13.Enabled = False
                    TextBox14.Enabled = False
                End If
                TextBox2.Select()
            End If
        End If
        If e.KeyData = Keys.Down Then
            ComboBox1.DroppedDown = True
        End If
    End Sub

    '----key down event for textbox1---->
    Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyData = Keys.Enter And Not TextBox1.Text = "" Then
            ComboBox1.Select()
        End If
    End Sub

    '---keydown event for textbox2 ----->
    Private Sub TextBox2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyData = Keys.Enter Then
            If TextBox4.Enabled = True Then
                TextBox4.Select()
            Else
                Button1.Select()
            End If
        End If
    End Sub

    '---keydown event for textbox3--->
    Private Sub TextBox3_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox3.KeyDown
        If e.KeyData = Keys.Enter Then
            TextBox9.Select()
        End If
    End Sub

    '----keydown event for textbox4---->
    Private Sub TextBox4_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox4.KeyDown
        If e.KeyData = Keys.Enter Then
            TextBox6.Select()
        End If
    End Sub
    '---keydown event for textbox6----->
    Private Sub TextBox6_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox6.KeyDown
        If e.KeyData = Keys.Enter Then
            TextBox3.Select()
        End If
    End Sub

    Private Sub TextBox9_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox9.KeyDown
        If e.KeyData = Keys.Enter Then
            TextBox7.Select()
        End If
    End Sub

    Private Sub TextBox7_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox7.KeyDown
        If e.KeyData = Keys.Enter Then
            TextBox10.Select()
        End If
    End Sub

    Private Sub TextBox10_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox10.KeyDown
        If e.KeyData = Keys.Enter Then
            TextBox11.Select()
        End If
    End Sub

    Private Sub TextBox11_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox11.KeyDown
        If e.KeyData = Keys.Enter Then
            TextBox12.Select()
        End If
    End Sub

    Private Sub TextBox12_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox12.KeyDown
        If e.KeyData = Keys.Enter Then
            TextBox13.Select()
        End If
    End Sub

    Private Sub TextBox13_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox13.KeyDown
        If e.KeyData = Keys.Enter Then
            TextBox14.Select()
        End If
    End Sub

    Private Sub TextBox14_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox14.KeyDown
        If e.KeyData = Keys.Enter Then
            TextBox5.Select()
        End If
    End Sub

    Private Sub TextBox5_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox5.KeyDown
        If e.KeyData = Keys.Enter Then
            TextBox8.Select()
        End If
    End Sub

    Private Sub TextBox8_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox8.KeyDown
        If e.KeyData = Keys.Enter Then
            Button1.Select()
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Close()
        frm_MainForm.Enabled = True
    End Sub

    Private Sub ledger_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        frm_MainForm.Enabled = True
    End Sub

    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        Select Case e.KeyChar
            Case "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", ".", vbBack
                e.Handled = False
            Case Else
                e.Handled = True
        End Select
    End Sub

    Private Sub TextBox12_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox12.KeyPress
        Select Case e.KeyChar
            Case "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", vbBack
                e.Handled = False
            Case Else
                e.Handled = True
        End Select
    End Sub


End Class

