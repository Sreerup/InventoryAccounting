
Public Class frm_login
    Dim ds As New DataSet
    Dim ds1 As New DataSet
    Dim ds3 As New DataSet
    Dim dslogin As New DataSet

    Dim s As String
    Dim ob As New Class1
    Dim dscomdef As New DataSet

    Private Sub login_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        uadmin = False
        uacc = False
        ComboBox3.Select()
        frm_ContainerForm.ToolStripButton1.Enabled = False
        frm_ContainerForm.ToolStripButton3.Enabled = False
        '---selecting all the companyinformation and filling the comboboxes--->
        s = "select companycode,companyname,address1,address2,city,district,stat,pin,phn,email,website,faxno,lstno,cstno,panno,vatno,stno from companymst order by companyname"
        ds = ob.populate(s)
        ob.combofill(ds, ComboBox1)
        ComboBox1.Text = ds.Tables(0).Rows(0).Item(1)
        '----selecting all the users from the id table and filling the comboboxes-->
        s = "select usercode,username,accesslevel,password from id order by username"
        ds1 = ob.populate(s)
        ob.combofill(ds1, ComboBox3)
        ComboBox3.Text = ds1.Tables(0).Rows(0).Item(1)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If ComboBox1.Text = "" Then
            MsgBox("Please select the Company", MsgBoxStyle.Exclamation, "Log-In")
            ComboBox1.Select()
            Exit Sub
        ElseIf ComboBox2.Text = "" Then
            MsgBox("Please select the Year", MsgBoxStyle.Exclamation, "Log-In")
            ComboBox2.Select()
            Exit Sub
        ElseIf ComboBox3.Text = "" Then
            MsgBox("Please select the User Name", MsgBoxStyle.Exclamation, "Log-In")
            ComboBox3.Select()
            Exit Sub
        ElseIf TextBox2.Text = "" Then
            MsgBox("Please enter The Password", MsgBoxStyle.Exclamation, "Log-In")
            TextBox2.Select()
            Exit Sub
        End If

        logincheck()
    End Sub

    Public Sub logincheck()
        Dim dspas As New DataSet
        s = "select * from userrights join id on id.usercode=userrights.usercode where id.usercode='" & Module1.usercode & "' and companycode='" & Module1.companycode & "'"
        dslogin = ob.populate(s)
        If dslogin.Tables(0).Rows.Count > 0 Then
            If TextBox2.Text = dslogin.Tables(0).Rows(0).Item(12).ToString Then
                If dslogin.Tables(0).Rows(0).Item(10).ToString = "Admin" Then
                    uadmin = True
                End If
                If dslogin.Tables(0).Rows(0).Item(2).ToString = "1" Then
                    loginacc()
                End If
                loginall()
                Me.Close()
            Else
                MsgBox("Wrong Username or Password.", MsgBoxStyle.Critical, "")
                TextBox2.Text = ""
                ComboBox3.Select()
            End If
        Else
            s = "select * from id where usercode='A00001'"
            dspas = ob.populate(s)
            If ComboBox3.Text = "Admin" And TextBox2.Text = dspas.Tables(0).Rows(0).Item(3).ToString Then
                loginadmin()
                loginacc()
                loginall()
                Me.Close()
            Else
                MsgBox("Wrong Username or Password.", MsgBoxStyle.Critical, "")
                TextBox2.Text = ""
                ComboBox3.Select()
            End If
        End If
    End Sub
    Private Sub loginall()
        If Module1.uadmin = True Then
            frm_ContainerForm.AdminToolStripMenuItem.Enabled = True
            frm_ContainerForm.MasterToolStripMenuItem.Enabled = True
            frm_ContainerForm.AccountCreationToolStripMenuItem.Enabled = True
            frm_ContainerForm.AcountSubGroupToolStripMenuItem.Enabled = True
            frm_ContainerForm.LedgerToolStripMenuItem.Enabled = True
            frm_ContainerForm.BrandMasterToolStripMenuItem.Enabled = True
            frm_ContainerForm.ItemCateToolStripMenuItem.Enabled = True
            frm_ContainerForm.KindOfForeignLoquoToolStripMenuItem.Enabled = True
            frm_ContainerForm.MeasurePackingToolStripMenuItem.Enabled = True
            frm_ContainerForm.StrengthToolStripMenuItem.Enabled = True
            frm_ContainerForm.ItemMasterToolStripMenuItem.Enabled = True
            frm_ContainerForm.SalesRateToolStripMenuItem.Enabled = True
            frm_ContainerForm.TaxShemesToolStripMenuItem.Enabled = True
            frm_ContainerForm.StorageLocationToolStripMenuItem.Enabled = True
            frm_ContainerForm.TransactionsToolStripMenuItem.Enabled = True
            frm_ContainerForm.PurchaseBillToolStripMenuItem.Enabled = True
            frm_ContainerForm.PaymentVoucherToolStripMenuItem.Enabled = True
            frm_ContainerForm.CounterSaleToolStripMenuItem.Enabled = True
            frm_ContainerForm.OpeningStockToolStripMenuItem1.Enabled = True
            frm_ContainerForm.StockTransferToolStripMenuItem.Enabled = True
            frm_ContainerForm.BeakageEntryToolStripMenuItem.Enabled = True
            frm_ContainerForm.REPORTToolStripMenuItem.Enabled = True
            frm_ContainerForm.AdminToolStripMenuItem.Visible = True
            frm_ContainerForm.MasterToolStripMenuItem.Visible = True
            frm_ContainerForm.AccountCreationToolStripMenuItem.Visible = True
            frm_ContainerForm.AcountSubGroupToolStripMenuItem.Visible = True
            frm_ContainerForm.LedgerToolStripMenuItem.Visible = True
            frm_ContainerForm.BrandMasterToolStripMenuItem.Visible = True
            frm_ContainerForm.ItemCateToolStripMenuItem.Visible = True
            frm_ContainerForm.KindOfForeignLoquoToolStripMenuItem.Visible = True
            frm_ContainerForm.MeasurePackingToolStripMenuItem.Visible = True
            frm_ContainerForm.StrengthToolStripMenuItem.Visible = True
            frm_ContainerForm.ItemMasterToolStripMenuItem.Visible = True
            frm_ContainerForm.SalesRateToolStripMenuItem.Visible = True
            frm_ContainerForm.TaxShemesToolStripMenuItem.Visible = True
            frm_ContainerForm.StorageLocationToolStripMenuItem.Visible = True
            frm_ContainerForm.TransactionsToolStripMenuItem.Visible = True
            frm_ContainerForm.PurchaseBillToolStripMenuItem.Visible = True
            frm_ContainerForm.PaymentVoucherToolStripMenuItem.Visible = True
            frm_ContainerForm.CounterSaleToolStripMenuItem.Visible = True
            frm_ContainerForm.OpeningStockToolStripMenuItem1.Visible = True
            frm_ContainerForm.StockTransferToolStripMenuItem.Visible = True
            frm_ContainerForm.BeakageEntryToolStripMenuItem.Visible = True
        Else
            If dslogin.Tables(0).Rows(0).Item(4).ToString = "True" Then
                frm_ContainerForm.AccountCreationToolStripMenuItem.Enabled = True
                frm_ContainerForm.AccountCreationToolStripMenuItem.Visible = True
            End If
            If dslogin.Tables(0).Rows(1).Item(4).ToString = "True" Then
                frm_ContainerForm.AcountSubGroupToolStripMenuItem.Enabled = True
                frm_ContainerForm.AcountSubGroupToolStripMenuItem.Visible = True
            End If
            If dslogin.Tables(0).Rows(2).Item(4).ToString = "True" Then
                frm_ContainerForm.LedgerToolStripMenuItem.Enabled = True
                frm_ContainerForm.LedgerToolStripMenuItem.Visible = True
            End If
            If dslogin.Tables(0).Rows(3).Item(4).ToString = "True" Then
                frm_ContainerForm.BrandMasterToolStripMenuItem.Enabled = True
                frm_ContainerForm.BrandMasterToolStripMenuItem.Visible = True
            End If
            If dslogin.Tables(0).Rows(4).Item(4).ToString = "True" Then
                frm_ContainerForm.ItemCateToolStripMenuItem.Enabled = True
                frm_ContainerForm.ItemCateToolStripMenuItem.Visible = True
            End If
            If dslogin.Tables(0).Rows(5).Item(4).ToString = "True" Then
                frm_ContainerForm.KindOfForeignLoquoToolStripMenuItem.Enabled = True
                frm_ContainerForm.KindOfForeignLoquoToolStripMenuItem.Visible = True
            End If
            If dslogin.Tables(0).Rows(6).Item(4).ToString = "True" Then
                frm_ContainerForm.MeasurePackingToolStripMenuItem.Enabled = True
                frm_ContainerForm.MeasurePackingToolStripMenuItem.Visible = True
            End If
            If dslogin.Tables(0).Rows(7).Item(4).ToString = "True" Then
                frm_ContainerForm.StrengthToolStripMenuItem.Enabled = True
                frm_ContainerForm.StrengthToolStripMenuItem.Visible = True
            End If
            If dslogin.Tables(0).Rows(8).Item(4).ToString = "True" Then
                frm_ContainerForm.ItemMasterToolStripMenuItem.Enabled = True
                frm_ContainerForm.ItemMasterToolStripMenuItem.Visible = True
            End If
            If dslogin.Tables(0).Rows(9).Item(4).ToString = "True" Then
                frm_ContainerForm.SalesRateToolStripMenuItem.Enabled = True
                frm_ContainerForm.SalesRateToolStripMenuItem.Visible = True
            End If
            If dslogin.Tables(0).Rows(10).Item(4).ToString = "True" Then
                frm_ContainerForm.TaxShemesToolStripMenuItem.Enabled = True
                frm_ContainerForm.TaxShemesToolStripMenuItem.Visible = True
            End If
            If dslogin.Tables(0).Rows(11).Item(4).ToString = "True" Then
                frm_ContainerForm.StorageLocationToolStripMenuItem.Enabled = True
                frm_ContainerForm.StorageLocationToolStripMenuItem.Visible = True
            End If
            If dslogin.Tables(0).Rows(12).Item(4).ToString = "True" Then
                frm_ContainerForm.PurchaseBillToolStripMenuItem.Enabled = True
                frm_ContainerForm.PurchaseBillToolStripMenuItem.Visible = True
            End If
            If dslogin.Tables(0).Rows(13).Item(4).ToString = "True" Then
                frm_ContainerForm.PaymentVoucherToolStripMenuItem.Enabled = True
                frm_ContainerForm.PaymentVoucherToolStripMenuItem.Visible = True
            End If
            If dslogin.Tables(0).Rows(15).Item(4).ToString = "True" Then
                frm_ContainerForm.CounterSaleToolStripMenuItem.Enabled = True
                frm_ContainerForm.CounterSaleToolStripMenuItem.Visible = True
            End If
            If dslogin.Tables(0).Rows(16).Item(4).ToString = "True" Then
                frm_ContainerForm.OpeningStockToolStripMenuItem1.Enabled = True
                frm_ContainerForm.OpeningStockToolStripMenuItem1.Visible = True
            End If
            If dslogin.Tables(0).Rows(17).Item(4).ToString = "True" Then
                frm_ContainerForm.StockTransferToolStripMenuItem.Enabled = True
                frm_ContainerForm.StockTransferToolStripMenuItem.Visible = True
            End If
            If dslogin.Tables(0).Rows(18).Item(4).ToString = "True" Then
                frm_ContainerForm.BeakageEntryToolStripMenuItem.Enabled = True
                frm_ContainerForm.BeakageEntryToolStripMenuItem.Visible = True
            End If
        End If
        frm_ContainerForm.SETTINGSToolStripMenuItem.Enabled = True
        frm_ContainerForm.SETTINGSToolStripMenuItem.Visible = True
        frm_ContainerForm.LogOutToolStripMenuItem.Enabled = True
        frm_ContainerForm.ChangeCompanyToolStripMenuItem.Enabled = True
        frm_ContainerForm.LogToolStripMenuItem.Enabled = False
        frm_ContainerForm.ExitToolStripMenuItem2.Enabled = True
        frm_ContainerForm.ToolStripButton1.Enabled = False
        frm_ContainerForm.ToolStripButton2.Enabled = True
        frm_ContainerForm.ToolStripButton3.Enabled = True
        frm_ContainerForm.ToolStripButton1.Visible = False
        frm_ContainerForm.ToolStripButton2.Visible = True
        frm_ContainerForm.ToolStripButton3.Visible = True
        frm_ContainerForm.mnu_closing_stock.Text = Module1.companyname
        frm_ContainerForm.ToolStripStatusLabel1.Text = Module1.username
        frm_ContainerForm.year_drop.DropDownItems.Clear()
        For i = 0 To ds3.Tables(0).Rows.Count - 1
            frm_ContainerForm.year_drop.DropDownItems.Add(ds3.Tables(0).Rows(i).Item(2).ToString)
        Next
        frm_ContainerForm.year_drop.Text = ds3.Tables(0).Rows(ComboBox2.SelectedIndex).Item(2)
        Module1.yearcode = ds3.Tables(0).Rows(ComboBox2.SelectedIndex).Item(0)
        Module1.comstdate = ds3.Tables(0).Rows(ComboBox2.SelectedIndex).Item(3)
        Module1.comenddate = ds3.Tables(0).Rows(ComboBox2.SelectedIndex).Item(4)
        comparamtr()
        frm_ContainerForm.flgout = False
    End Sub
    '----slecting the user defined values from the company parameter --------->
    Public Sub comparamtr()
        '---filling the dataset with the vaues if that company paramtr --->
        s = "select companycode,csalebillno,saleAccount_head,bankacc,cashparty,crcashparty,breakacc,saleretrnacc,csalelmt,printbill,printer,purchaseacchd,cashhd,discntacc,defshpgdn,ratecode,expenceacc,rndoffacc,autorefcurrsal,actposmode,bill_footer,bill_footer2,bill_footer3,bill_footer4,on_off_shop_type,back_up_restore_path,print_mode,surcharge_account_head,Trade_discount_head,surcharge_percent from companyparamtr where companycode= '" & Module1.companycode & "'"
        dscomdef = ob.populate(s)
        '--if there are some data then initialising the module variables with the values saves --->
        If dscomdef.Tables(0).Rows.Count > 0 Then
            Module1.combillno = dscomdef.Tables(0).Rows(0).Item(1).ToString
            Module1.comsaleacc = dscomdef.Tables(0).Rows(0).Item(2).ToString
            Module1.combankacc = dscomdef.Tables(0).Rows(0).Item(3).ToString
            Module1.combreakacc = dscomdef.Tables(0).Rows(0).Item(6).ToString
            Module1.comsaleretacc = dscomdef.Tables(0).Rows(0).Item(7).ToString
            Module1.comlimit = dscomdef.Tables(0).Rows(0).Item(8).ToString
            Module1.comsaveprint = dscomdef.Tables(0).Rows(0).Item(9).ToString
            Module1.comprinter = dscomdef.Tables(0).Rows(0).Item(10).ToString
            Module1.compuracc = dscomdef.Tables(0).Rows(0).Item(11).ToString
            Module1.comcashacc = dscomdef.Tables(0).Rows(0).Item(12).ToString
            Module1.comdiscacc = dscomdef.Tables(0).Rows(0).Item(13).ToString
            Module1.comdefstore = dscomdef.Tables(0).Rows(0).Item(14).ToString
            Module1.comdefrate = dscomdef.Tables(0).Rows(0).Item(15).ToString
            Module1.comexpenceacc = dscomdef.Tables(0).Rows(0).Item(16).ToString
            Module1.comrndacc = dscomdef.Tables(0).Rows(0).Item(17).ToString
            Module1.comsaleref = dscomdef.Tables(0).Rows(0).Item(18).ToString
            Module1.compos = dscomdef.Tables(0).Rows(0).Item(19).ToString
            Module1.combillfooter = dscomdef.Tables(0).Rows(0).Item(20).ToString
            Module1.combillfooter2 = dscomdef.Tables(0).Rows(0).Item(21).ToString
            Module1.combillfooter3 = dscomdef.Tables(0).Rows(0).Item(22).ToString
            Module1.combillfooter4 = dscomdef.Tables(0).Rows(0).Item(23).ToString
            Module1.back_up_path = dscomdef.Tables(0).Rows(0).Item(25).ToString
            Module1.comprintmode = dscomdef.Tables(0).Rows(0).Item(26).ToString
            Module1.com_surcharge_acc = dscomdef.Tables(0).Rows(0).Item(27).ToString
            Module1.com_trade_discount = dscomdef.Tables(0).Rows(0).Item(28).ToString
            Module1.com_surcharge_percent = dscomdef.Tables(0).Rows(0).Item(29).ToString
        End If
    End Sub
    Private Sub loginadmin()
        frm_ContainerForm.AdminToolStripMenuItem.Enabled = True
        frm_ContainerForm.AdminToolStripMenuItem.Visible = True
        Module1.uadmin = True
    End Sub

    Private Sub loginacc()
        frm_ContainerForm.MasterToolStripMenuItem.Enabled = True
        frm_ContainerForm.TransactionsToolStripMenuItem.Enabled = True
        frm_ContainerForm.REPORTToolStripMenuItem.Enabled = True
        frm_ContainerForm.REPORTToolStripMenuItem.Visible = True
        frm_ContainerForm.MasterToolStripMenuItem.Visible = True
        frm_ContainerForm.TransactionsToolStripMenuItem.Visible = True
        Module1.uacc = True
    End Sub
    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        If ComboBox3.SelectedIndex >= 0 Then
            Module1.usercode = ds1.Tables(0).Rows(ComboBox3.SelectedIndex).Item(0)
            Module1.username = ds1.Tables(0).Rows(ComboBox3.SelectedIndex).Item(1)
        End If
    End Sub

    Private Sub ComboBox3_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox3.KeyDown
        If e.KeyData = Keys.Enter Then
            If ComboBox3.Text = "" Or Not ComboBox3.SelectedIndex >= 0 Then
                ComboBox3.DroppedDown = True
            Else
                TextBox2.Select()
            End If
        End If
    End Sub

    Private Sub TextBox2_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyData = Keys.Enter And Not TextBox2.Text = "" Then
            ComboBox1.Select()
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedIndex >= 0 Then
            Module1.companycode = ds.Tables(0).Rows(ComboBox1.SelectedIndex).Item(0).ToString
            Module1.companyname = ds.Tables(0).Rows(ComboBox1.SelectedIndex).Item(1).ToString
            Module1.comaddress1 = ds.Tables(0).Rows(ComboBox1.SelectedIndex).Item(2).ToString
            Module1.comaddress2 = ds.Tables(0).Rows(ComboBox1.SelectedIndex).Item(3).ToString
            Module1.comcity = ds.Tables(0).Rows(ComboBox1.SelectedIndex).Item(4).ToString
            Module1.comdistrict = ds.Tables(0).Rows(ComboBox1.SelectedIndex).Item(5).ToString
            Module1.comstate = ds.Tables(0).Rows(ComboBox1.SelectedIndex).Item(6).ToString
            Module1.compin = ds.Tables(0).Rows(ComboBox1.SelectedIndex).Item(7).ToString
            Module1.comphone = ds.Tables(0).Rows(ComboBox1.SelectedIndex).Item(8).ToString
            Module1.comemail = ds.Tables(0).Rows(ComboBox1.SelectedIndex).Item(9).ToString
            ComboBox2.Items.Clear()
            s = "select yearcode,companycode,yearrange,stdate,enddate from yearmst where companycode='" & Module1.companycode & "' order by yearrange desc"
            ds3 = ob.populate(s)
            For i = 0 To ds3.Tables(0).Rows.Count - 1
                ComboBox2.Items.Add(ds3.Tables(0).Rows(i).Item(2).ToString)
            Next
            ComboBox2.SelectedItem = ds3.Tables(0).Rows(0).Item(2)
        End If
    End Sub

    Private Sub ComboBox1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox1.KeyDown
        If e.KeyData = Keys.Enter Then
            If ComboBox1.SelectedIndex >= 0 Then
                ComboBox2.Select()
            End If
        End If
    End Sub

    Private Sub ComboBox2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox2.KeyDown
        If e.KeyData = Keys.Enter Then
            If ComboBox2.SelectedIndex >= 0 Then
                Button1.Select()
            End If
        End If
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        'ContainerForm.logout()
        Me.Close()
    End Sub
    Private Sub login_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If frm_ContainerForm.flgout = True Then
            frm_ContainerForm.ToolStripButton1.Enabled = True
        End If
    End Sub

End Class