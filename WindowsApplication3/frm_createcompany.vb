Public Class frm_createcompany

    Dim ds As New DataSet

    Dim check As Boolean
    Dim ccode As String
    Dim ycode As String

    Dim ob As New Class1
    Dim s As String

    Private Sub createcompany_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        frmcompanyload()
        If Module1.flag = 2 Then
            Label11.Visible = False
            Label17.Visible = False
            start_text.Visible = False
            end_text.Visible = False
            Button1.Text = "&Update"
            initialise()
        End If
        cname.Select()
    End Sub

    Private Sub frmcompanyload()
        cname.Text = Nothing
        caddress1.Text = Nothing
        caddress2.Text = Nothing
        city.Text = Nothing
        district.Text = Nothing
        state.Text = Nothing
        pin.Text = Nothing
        phn.Text = Nothing
        faxno.Text = Nothing
        email.Text = Nothing
        website.Text = Nothing
        start_text.Text = Nothing
        end_text.Text = Nothing

        txtpan.Text = Nothing
        txtvat.Text = Nothing
        txtlst.Text = Nothing
        txtcst.Text = Nothing
        txtst.Text = Nothing

        check = False

        frm_MainForm.Enabled = False
    End Sub



    Private Sub initialise()


        cname.Text = Module1.companyname
        caddress1.Text = Module1.address1
        caddress2.Text = Module1.address2
        city.Text = Module1.city
        district.Text = Module1.district
        state.Text = Module1.state
        pin.Text = Module1.pin
        phn.Text = Module1.phone
        faxno.Text = Module1.fax
        email.Text = Module1.email
        website.Text = Module1.www

        txtpan.Text = Module1.panno
        txtvat.Text = Module1.vatno
        txtlst.Text = Module1.lstno
        txtcst.Text = Module1.cstno
        txtst.Text = Module1.stno


    End Sub



    Private Sub comexist()

        s = "select count(*) from companymst where companyname='" & cname.Text & "'"
        s = ob.executereader(s)
        If Convert.ToInt32(s) > 0 Then
            '----this is for checking while creting a company
            If Module1.flag = 1 Then
                MsgBox("Company already exist, Please enter new Company", MsgBoxStyle.Information, "Company Exist")
                check = True
                '---this is for checking while editing a company
            ElseIf Module1.flag = 2 And Not cname.Text.ToUpper = Module1.comname.ToUpper Then
                MsgBox("Company already exist, Please enter new Company", MsgBoxStyle.Information, "Company Exist")
                check = True
            End If
            cname.Select()
        End If

    End Sub


    Private Sub get_company_code()
        s = "select top 1 companycode from companymst order by companycode desc"
        s = ob.executereader(s)
        If Not s = Nothing Then
            ccode = Convert.ToInt32(s) + 1
        Else
            ccode = 1
        End If
    End Sub


    Private Sub get_yearcode()
        s = "select top 1 yearcode from yearmst where companycode='" & ccode & "' order by yearcode desc"
        s = ob.executereader(s)
        If Not s = Nothing Then
            ycode = Convert.ToInt32(s) + 1
        Else
            ycode = 1
        End If
    End Sub



    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        check = False
        comexist()

        If check = False Then
            If flag = 1 And Not start_text.Text = Nothing And Not end_text.Text = Nothing Then
                '---creatint the company----------------->
                get_company_code()
                s = "insert into companymst (companycode,companyname,address1,city,pin,stat,phn,email,website,address2,district,faxno,lstno,cstno,panno,vatno,stno ) values('" & ccode & "','" & cname.Text.ToUpper & "','" & caddress1.Text.ToUpper & "','" & city.Text.ToUpper & "','" & pin.Text & "','" & state.Text.ToUpper & "','" & phn.Text & "','" & email.Text & "','" & website.Text & "','" & caddress2.Text.ToUpper & "','" & district.Text.ToUpper & "','" & faxno.Text.ToUpper & "', '" & txtlst.Text.ToUpper & "', '" & txtcst.Text.ToUpper & "', '" & txtpan.Text.ToUpper & "', '" & txtvat.Text.ToUpper & "', '" & txtst.Text.ToUpper & "')"
                ob.insert(s)
                '-----creatting the yearcode from the company------->
                get_yearcode()
                s = "insert into yearmst(yearcode,yearrange,stdate,enddate,companycode) values('" & ycode & "','" & start_text.Text & "-" & end_text.Text & "','" & start_text.Text & "-04-01','" & end_text.Text & "-03-31','" & ccode & "')"
                ob.insert(s)
                s = "insert into itemrateinfo(ratecode,ratename,companycode) values('G00001','GENERAL','" & Module1.companycode & "')"
                ob.insert(s)
            ElseIf flag = 2 Then
                s = "update companymst  set companyname='" & cname.Text.ToUpper & "',  address1='" & caddress1.Text.ToUpper & "',city='" & city.Text.ToUpper & "',pin='" & pin.Text & "',stat='" & state.Text.ToUpper & "',phn='" & phn.Text & "',email='" & email.Text & "',website='" & website.Text & "',address2='" & caddress2.Text.ToUpper & "',district='" & district.Text.ToUpper & "',faxno='" & faxno.Text & "',lstno='" & txtlst.Text.ToUpper & "',cstno='" & txtcst.Text.ToUpper & "',panno='" & txtpan.Text.ToUpper & "',vatno='" & txtvat.Text.ToUpper & "',stno ='" & txtst.Text.ToUpper & "' where companycode ='" & Module1.companycode & "'"
                ob.insert(s)
                frm_ContainerForm.mnu_closing_stock.Text = cname.Text.ToUpper
            End If
        Else
            If Module1.flag = 1 Then
                MsgBox("company by this name already exists or you hav'nt inputed the starting and the ending year")
            ElseIf Module1.flag = 2 Then
                MsgBox("company already exists")
            End If
        End If





        '--------code for refreshing the datadrig view-------------->
        '---module1.flag1 is set up to be 1 when no company exists-->
        If Module1.flag1 = 1 Then

            Dim frm As New frm_login
            frm.MdiParent = frm_ContainerForm
            frm.Show()

            Module1.flag1 = 0
            Me.Close()

        Else

            If Module1.flag = 1 Then
                frm_MainForm.mainformload()
                frmcompanyload()
            ElseIf Module1.flag = 2 Then
                frm_MainForm.mainformload()
                Me.Close()
            End If


        End If



    End Sub

    Private Sub caddress1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles caddress1.KeyDown
        If e.KeyData = Keys.Enter Then
            caddress2.Select()
        End If
    End Sub

    Private Sub caddress2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles caddress2.KeyDown
        If e.KeyData = Keys.Enter Then
            city.Select()
        End If
    End Sub

    Private Sub city_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles city.KeyDown
        If e.KeyData = Keys.Enter Then
            district.Select()
        End If
    End Sub

    Private Sub district_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles district.KeyDown
        If e.KeyData = Keys.Enter Then
            state.Select()
        End If
    End Sub

    Private Sub state_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles state.KeyDown
        If e.KeyData = Keys.Enter Then
            pin.Select()
        End If
    End Sub

    Private Sub pin_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles pin.KeyPress
        Select Case e.KeyChar
            Case "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", vbBack
                e.Handled = False
            Case Else
                e.Handled = True
        End Select
    End Sub

    Private Sub pin_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles pin.KeyDown
        If e.KeyData = Keys.Enter Then
            phn.Select()
        End If
    End Sub

    Private Sub phn_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles phn.KeyPress
        Select Case e.KeyChar
            Case "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", vbBack
                e.Handled = False
            Case Else
                e.Handled = True
        End Select
    End Sub

    Private Sub phn_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles phn.KeyDown
        If e.KeyData = Keys.Enter Then
            faxno.Select()
        End If
    End Sub

    Private Sub faxno_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles faxno.KeyPress
        Select Case e.KeyChar
            Case "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", vbBack
                e.Handled = False
            Case Else
                e.Handled = True
        End Select
    End Sub

    Private Sub faxno_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles faxno.KeyDown
        If e.KeyData = Keys.Enter Then
            email.Select()
        End If
    End Sub

    Private Sub email_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles email.KeyDown
        If e.KeyData = Keys.Enter Then
            website.Select()
        End If
    End Sub

    Private Sub website_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles website.KeyDown
        If e.KeyData = Keys.Enter Then
            If Module1.flag = 1 Then
                start_text.Select()
            Else
                Button1.Select()
            End If
        End If
    End Sub

    Private Sub finyear_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Select Case e.KeyChar
            Case "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "-", vbBack
                e.Handled = False
            Case Else
                e.Handled = True
        End Select
    End Sub


    Private Sub TabPage2_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabPage2.GotFocus
        txtpan.Select()
    End Sub

    Private Sub txtpan_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtpan.KeyDown
        If e.KeyData = Keys.Enter Then
            txtvat.Select()
        End If
    End Sub

    Private Sub txtvat_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtvat.KeyDown
        If e.KeyData = Keys.Enter Then
            txtlst.Select()
        End If
    End Sub

    Private Sub txtlst_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtlst.KeyDown
        If e.KeyData = Keys.Enter Then
            txtcst.Select()
        End If
    End Sub

    Private Sub txtcst_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtcst.KeyDown
        If e.KeyData = Keys.Enter Then
            txtst.Select()
        End If
    End Sub

    Private Sub txtst_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtst.KeyDown
        If e.KeyData = Keys.Enter Then
            Button1.Select()
        End If
    End Sub

    Private Sub Button1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Button1.KeyDown
        If e.KeyData = Keys.Enter Then
            Button2.Select()
        End If
    End Sub

    Private Sub cname_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cname.KeyDown
        If Not cname.Text = "" And e.KeyData = Keys.Enter Then
            comexist()
            caddress1.Select()
        End If
    End Sub

    Private Sub createcompany_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Module1.flag1 = 1 Then
            e.Cancel = True
        Else
            frm_MainForm.Enabled = True
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
        frm_MainForm.Enabled = True
    End Sub

    Private Sub start_text_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)

    End Sub

    Private Sub end_text_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)

    End Sub

    Private Sub start_text_KeyDown1(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles start_text.KeyDown
        If e.KeyData = Keys.Enter Then
            end_text.Select()
        End If
    End Sub

    Private Sub end_text_KeyDown1(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles end_text.KeyDown
        If e.KeyData = Keys.Enter Then
            Button1.Select()
        End If
    End Sub
End Class