Public Class frm_companyparameter
    '--initialising the instance variables--->
    Dim data_set As New DataSet
    Dim ds As New DataSet
    Dim ds1 As New DataSet
    Dim ds2 As New DataSet
    Dim s As String
    Public shoptype As String
    Public printmode As String
    Dim ob As New Class1
    Private Sub companyparameter_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '---initialising the variables----->
        Module1.comsaveprint = "0"
        Module1.comsaleref = "1"
        Module1.compos = "1"
        '----filling up the dataset data_set--->
        s = "select ledcode,name from ledger join acountname on ledger.accode = acountname.accode where companycode='" & Module1.companycode & "' order by name"
        data_set = ob.populate(s)
        '---filling the comboboxes with the data_set----->
        ob.combofill(data_set, cmbsalacc_hd)
        ob.combofill(data_set, cmbbankacc)
        ob.combofill(data_set, cmbcashhd)
        ob.combofill(data_set, cmbbreakacc)
        ob.combofill(data_set, cmb_cash_discount)
        ob.combofill(data_set, cmb_trade_discount)
        ob.combofill(data_set, cmbexpenceacc)
        ob.combofill(data_set, cmbrndoffacc)
        ob.combofill(data_set, cmbsaleretrnacc)
        ob.combofill(data_set, cmbpurchaseacc_hd)
        ob.combofill(data_set, cmb_surcharge_acc)
        '---filling up the datset data_set and the combobox --->
        s = "select shopcode,shopname from storage where companycode='" & Module1.companycode & "'order by shopname"
        ds1 = ob.populate(s)
        ob.combofill(ds1, cmbdefshpgdn)
        '---filling up the dataset and the combobox ------>
        s = "select ratecode,ratename from itemrateinfo where companycode='" & Module1.companycode & "'"
        ds2 = ob.populate(s)
        ob.combofill(ds2, cmbdefrate)
        '---selecting the installed default printers ----->
        Dim prt As String
        For Each prt In Printing.PrinterSettings.InstalledPrinters : cmbprinter.Items.Add(prt) : Next
        '---loading the companyparametrer values if there are values already present in it --->
        s = "select companycode,csalebillno,saleAccount_head,bankacc,cashparty,crcashparty,breakacc,saleretrnacc,csalelmt,printbill,printer,purchaseacchd,cashhd,discntacc,defshpgdn,ratecode,expenceacc,rndoffacc,autorefcurrsal,actposmode,bill_footer,bill_footer2,bill_footer3,bill_footer4,on_off_shop_type,back_up_restore_path,print_mode,surcharge_account_head,Trade_discount_head,surcharge_percent from companyparamtr where companycode= '" & Module1.companycode & "'"
        ds = ob.populate(s)
        '---if the ds variable contains more than one row ---->
        If ds.Tables(0).Rows.Count > 0 Then
            txtcsalebillno.Text = ds.Tables(0).Rows(0).Item(1).ToString
            txtcsalelmt.Text = ds.Tables(0).Rows(0).Item(8).ToString
            txt_billfooter.Text = ds.Tables(0).Rows(0).Item(20).ToString
            txt_billfooter2.Text = ds.Tables(0).Rows(0).Item(21).ToString
            txt_billfooter3.Text = ds.Tables(0).Rows(0).Item(22).ToString
            txt_billfooter4.Text = ds.Tables(0).Rows(0).Item(23).ToString
            cmbprinter.Text = ds.Tables(0).Rows(0).Item(10).ToString
            back_up_path_textbox.Text = ds.Tables(0).Rows(0).Item(25).ToString
            '---code for checking or unchecking the check boxes----->
            print_bill_on_save.Checked = ds.Tables(0).Rows(0).Item(9).ToString
            auto_refresh_current_sale.Checked = ds.Tables(0).Rows(0).Item(18).ToString
            active_pos_mode.Checked = ds.Tables(0).Rows(0).Item(19).ToString
            '---link query for fetching the data from the company parameter------>
            If Not ds.Tables(0).Rows(0).Item(2).ToString = Nothing Then : Dim query1 = From p As DataRow In data_set.Tables(0) Where p(0) = ds.Tables(0).Rows(0).Item(2) Select p(1) : If query1.Count > 0 Then : cmbsalacc_hd.Text = query1(0).ToString : End If : End If
            If Not ds.Tables(0).Rows(0).Item(3).ToString = Nothing Then : Dim query4 = From p As DataRow In data_set.Tables(0) Where p(0) = ds.Tables(0).Rows(0).Item(3) Select p(1) : If query4.Count > 0 Then : cmbbankacc.Text = query4(0).ToString : End If : End If
            If Not ds.Tables(0).Rows(0).Item(6).ToString = Nothing Then : Dim query7 = From p As DataRow In data_set.Tables(0) Where p(0) = ds.Tables(0).Rows(0).Item(6) Select p(1) : If query7.Count > 0 Then : cmbbreakacc.Text = query7(0).ToString : End If : End If
            If Not ds.Tables(0).Rows(0).Item(7).ToString = Nothing Then : Dim query8 = From p As DataRow In data_set.Tables(0) Where p(0) = ds.Tables(0).Rows(0).Item(7) Select p(1) : If query8.Count > 0 Then : cmbsaleretrnacc.Text = query8(0).ToString : End If : End If
            If Not ds.Tables(0).Rows(0).Item(11).ToString = Nothing Then : Dim query2 = From p As DataRow In data_set.Tables(0) Where p(0) = ds.Tables(0).Rows(0).Item(11) Select p(1) : If query2.Count > 0 Then : cmbpurchaseacc_hd.Text = query2(0).ToString : End If : End If
            If Not ds.Tables(0).Rows(0).Item(12).ToString = Nothing Then : Dim query3 = From p As DataRow In data_set.Tables(0) Where p(0) = ds.Tables(0).Rows(0).Item(12) Select p(1) : If query3.Count > 0 Then : cmbcashhd.Text = query3(0).ToString : End If : End If
            If Not ds.Tables(0).Rows(0).Item(13).ToString = Nothing Then : Dim query5 = From p As DataRow In data_set.Tables(0) Where p(0) = ds.Tables(0).Rows(0).Item(13) Select p(1) : If query5.Count > 0 Then : cmb_cash_discount.Text = query5(0).ToString : End If : End If
            If Not ds.Tables(0).Rows(0).Item(14).ToString = Nothing Then : Dim query10 = From p As DataRow In ds1.Tables(0) Where p(0) = ds.Tables(0).Rows(0).Item(14) Select p(1) : If query10.Count > 0 Then : cmbdefshpgdn.Text = query10(0).ToString : End If : End If
            If Not ds.Tables(0).Rows(0).Item(15).ToString = Nothing Then : Dim query11 = From p As DataRow In ds2.Tables(0) Where p(0) = ds.Tables(0).Rows(0).Item(15) Select p(1) : If query11.Count > 0 Then : cmbdefrate.Text = query11(0).ToString : End If : End If
            If Not ds.Tables(0).Rows(0).Item(16).ToString = Nothing Then : Dim query6 = From p As DataRow In data_set.Tables(0) Where p(0) = ds.Tables(0).Rows(0).Item(16) Select p(1) : If query6.Count > 0 Then : cmbexpenceacc.Text = query6(0).ToString : End If : End If
            If Not ds.Tables(0).Rows(0).Item(17).ToString = Nothing Then : Dim query9 = From p As DataRow In data_set.Tables(0) Where p(0) = ds.Tables(0).Rows(0).Item(17) Select p(1) : If query9.Count > 0 Then : cmbrndoffacc.Text = query9(0).ToString : End If : End If
            If Not ds.Tables(0).Rows(0).Item(27).ToString = Nothing Then : Dim query12 = From p As DataRow In data_set.Tables(0) Where p(0) = ds.Tables(0).Rows(0).Item(27) Select p(1) : If query12.Count > 0 Then : cmb_surcharge_acc.Text = query12(0).ToString : End If : End If
            If Not ds.Tables(0).Rows(0).Item(28).ToString = Nothing Then : Dim query13 = From p As DataRow In data_set.Tables(0) Where p(0) = ds.Tables(0).Rows(0).Item(28) Select p(1) : If query13.Count > 0 Then : cmb_trade_discount.Text = query13(0).ToString : End If : End If
            TextBox1.Text = Convert.ToString(ds.Tables(0).Rows(0).Item(29))
            '--check box selection fro off-shop or on shop ---->
            If ds.Tables(0).Rows(0).Item(24).ToString = "ON" Then
                chk_on.Checked = True
                chk_off.Checked = False
            ElseIf ds.Tables(0).Rows(0).Item(24).ToString = "OFF" Then
                chk_off.Checked = True
                chk_on.Checked = False
            End If
            '----check box selection for dos windows mode selectin of printing ----->
            If ds.Tables(0).Rows(0).Item(26).ToString = "dos" Then
                chk_dos.Checked = True
                chk_win.Checked = False
            ElseIf ds.Tables(0).Rows(0).Item(26).ToString = "windows" Then
                chk_win.Checked = True
                chk_dos.Checked = False
            End If

        End If
    End Sub
    '----event for the save button -->
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        '--code for updating or inserting into the companyparameter ----->
        s = "delete from companyparamtr where companycode='" & Module1.companycode & "'"
        ob.insert(s)
        s = "insert into companyparamtr(companycode,csalebillno                          ,saleAccount_head            ,bankacc                     ,breakacc                     ,saleretrnacc                   ,purchaseacchd              ,cashhd                      ,discntacc                   ,defshpgdn                    ,ratecode                    ,expenceacc                     ,rndoffacc                  ,printer                  ,autorefcurrsal              ,csalelmt                          ,actposmode              ,printbill                     ,on_off_shop_type  ,bill_footer                  ,bill_footer2                  ,bill_footer3                  ,bill_footer4                  ,back_up_restore_path                       ,print_mode         ,surcharge_account_head             ,Trade_discount_head                 ,surcharge_percent)" & _
              "values('" & Module1.companycode & "','" & txtcsalebillno.Text.ToUpper & "','" & Module1.comsaleacc & "','" & Module1.combankacc & "','" & Module1.combreakacc & "','" & Module1.comsaleretacc & "','" & Module1.compuracc & "','" & Module1.comcashacc & "','" & Module1.comdiscacc & "','" & Module1.comdefstore & "','" & Module1.comdefrate & "','" & Module1.comexpenceacc & "','" & Module1.comrndacc & "','" & cmbprinter.Text & "','" & Module1.comsaleref & "','" & txtcsalelmt.Text.ToUpper & "','" & Module1.compos & "','" & Module1.comsaveprint & "','" & shoptype & "','" & txt_billfooter.Text & "','" & txt_billfooter2.Text & "','" & txt_billfooter3.Text & "','" & txt_billfooter4.Text & "','" & back_up_path_textbox.Text.ToUpper & "','" & printmode & "','" & Module1.com_surcharge_acc & "','" & Module1.com_trade_discount & "','" & Module1.com_surcharge_percent & "')"
        ob.insert(s)
        frm_ContainerForm.ToolStripLabel1.Text = ""
        frm_login.comparamtr()
        Me.Close()
    End Sub
    '---event for the cancel button ---->
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
    '--selected index change for the combox of sale account ----->
    Private Sub cmbcsalacc_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbsalacc_hd.SelectedIndexChanged
        If Not cmbsalacc_hd.Text = Nothing Then : Dim query = From p As DataRow In data_set.Tables(0) Where p(1) = cmbsalacc_hd.Text Select p(0) : Module1.comsaleacc = query(0).ToString : End If
    End Sub
    '--selected index change of the combobox of purchase accont--->
    Private Sub cmbpurchaseacchd_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbpurchaseacc_hd.SelectedIndexChanged
        If Not cmbpurchaseacc_hd.Text = Nothing Then : Dim query = From p As DataRow In data_set.Tables(0) Where p(1) = cmbpurchaseacc_hd.Text Select p(0) : Module1.compuracc = query(0).ToString : End If
    End Sub
    '--selected index change of the combox of cash account ----->
    Private Sub cmbcashhd_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbcashhd.SelectedIndexChanged
        If Not cmbcashhd.Text = Nothing Then : Dim query = From p As DataRow In data_set.Tables(0) Where p(1) = cmbcashhd.Text Select p(0) : Module1.comcashacc = query(0).ToString : End If
    End Sub
    '--selected index change of the bank account combo box--->
    Private Sub cmbbankacc_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbbankacc.SelectedIndexChanged
        If Not cmbbankacc.Text = Nothing Then : Dim query = From p As DataRow In data_set.Tables(0) Where p(1) = cmbbankacc.Text Select p(0) : Module1.combankacc = query(0).ToString : End If
    End Sub
    '----selected index change of the cash discount account combobox ----->
    Private Sub cmbdiscntacc_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmb_cash_discount.SelectedIndexChanged
        If Not cmb_cash_discount.Text = Nothing Then : Dim query = From p As DataRow In data_set.Tables(0) Where p(1) = cmb_cash_discount.Text Select p(0) : Module1.comdiscacc = query(0).ToString : End If
    End Sub

    '---selected index change for trade discount account combobox--->
    Private Sub cmb_trade_discount_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmb_trade_discount.SelectedIndexChanged
        If Not cmb_trade_discount.Text = Nothing Then : Dim query = From p As DataRow In data_set.Tables(0) Where p(1) = cmb_trade_discount.Text Select p(0) : Module1.com_trade_discount = query(0).ToString : End If
    End Sub

    '--selected index change of the expence account combobox----->
    Private Sub cmbexpenceacc_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbexpenceacc.SelectedIndexChanged
        If Not cmbexpenceacc.Text = Nothing Then : Dim query = From p As DataRow In data_set.Tables(0) Where p(1) = cmbexpenceacc.Text Select p(0) : Module1.comexpenceacc = query(0).ToString : End If
    End Sub
    '---selected index change of the combobox of breakage account--->
    Private Sub cmbbreakacc_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbbreakacc.SelectedIndexChanged
        If Not cmbbreakacc.Text = Nothing Then : Dim query = From p As DataRow In data_set.Tables(0) Where p(1) = cmbbreakacc.Text Select p(0) : Module1.combreakacc = query(0).ToString : End If
    End Sub
    '---selected index change of the sale return account ---->
    Private Sub cmbsaleretrnacc_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbsaleretrnacc.SelectedIndexChanged
        If Not cmbsaleretrnacc.Text = Nothing Then : Dim query = From p As DataRow In data_set.Tables(0) Where p(1) = cmbsaleretrnacc.Text Select p(0) : Module1.comsaleretacc = query(0).ToString : End If
    End Sub
    '---selected index change of the combobox of the round off acocunt --->
    Private Sub cmbrndoffacc_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbrndoffacc.SelectedIndexChanged
        If Not cmbrndoffacc.Text = Nothing Then : Dim query = From p As DataRow In data_set.Tables(0) Where p(1) = cmbrndoffacc.Text Select p(0) : Module1.comrndacc = query(0).ToString : End If
    End Sub
    '----select index for surcharge account combo box ---->
    Private Sub cmb_surcharge_acc_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmb_surcharge_acc.SelectedIndexChanged
        If Not cmb_surcharge_acc.Text = Nothing Then : Dim query = From p As DataRow In data_set.Tables(0) Where p(1) = cmb_surcharge_acc.Text Select p(0) : Module1.com_surcharge_acc = query(0).ToString : End If
    End Sub
    '---selected index change of the combobox for the default godown--->
    Private Sub cmbdefshpgdn_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbdefshpgdn.SelectedIndexChanged
        If Not cmbdefshpgdn.Text = Nothing Then : Dim query = From p As DataRow In ds1.Tables(0) Where p(1) = cmbdefshpgdn.Text Select p(0) : Module1.comdefstore = query(0).ToString : End If
    End Sub
    '--selected index change of the combobox for the default rate---->
    Private Sub cmbratecode_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbdefrate.SelectedIndexChanged
        If Not cmbdefrate.Text = Nothing Then : Dim query = From p As DataRow In ds2.Tables(0) Where p(1) = cmbdefrate.Text Select p(0) : Module1.comdefrate = query(0).ToString : End If
    End Sub
    '---check change event for print bill directly on save------>
    Private Sub chk1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles print_bill_on_save.CheckedChanged
        If print_bill_on_save.Checked = True Then : Module1.comsaveprint = "1" : Else : Module1.comsaveprint = "0" : End If
    End Sub
    '--check change event for auto refresh current sale on save for stock and sale--->
    Private Sub chk2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles auto_refresh_current_sale.CheckedChanged
        If auto_refresh_current_sale.Checked = True Then : Module1.comsaleref = "1" : Else : Module1.comsaleref = "0" : End If
    End Sub
    '---check change event for active pos mode--->
    Private Sub chk3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles active_pos_mode.CheckedChanged
        If active_pos_mode.Checked = True Then : Module1.compos = "1" : Else : Module1.compos = "0" : End If
    End Sub
    '---check change for on off shop---->
    Private Sub chk_off_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_off.CheckedChanged
        If chk_off.Checked = True Then : shoptype = "OFF" : chk_on.Checked = False : End If
    End Sub
    '---check change for on off shop--->
    Private Sub chk_on_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_on.CheckedChanged
        If chk_on.Checked = True Then : shoptype = "ON" : chk_off.Checked = False : End If
    End Sub
    '---check change for print mode--->
    Private Sub chk_dos_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_dos.CheckedChanged
        If chk_dos.Checked = True Then : printmode = "dos" : chk_win.Checked = False : End If
    End Sub
    '---check change for print mode--->
    Private Sub chk_win_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_win.CheckedChanged
        If chk_win.Checked = True Then : printmode = "windows" : chk_dos.Checked = False : End If
    End Sub
    '---key events for comboboxes---->
    Private Sub cmbcsalacc_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbsalacc_hd.KeyDown
        If e.KeyData = Keys.Enter Then
            If cmbsalacc_hd.SelectedIndex >= 0 Then
                cmbbankacc.Select()
            End If
        End If
        If e.KeyData = Keys.Down Then
            cmbsalacc_hd.DroppedDown = True
        End If
    End Sub
    Private Sub cmbbankacc_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbbankacc.KeyDown
        If e.KeyData = Keys.Enter Then
            If cmbbankacc.SelectedIndex >= 0 Then
                cmbbreakacc.Select()
            End If
        End If
        If e.KeyData = Keys.Down Then
            cmbbankacc.DroppedDown = True
        End If
    End Sub
    Private Sub cmbbreakacc_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbbreakacc.KeyDown
        If e.KeyData = Keys.Enter Then
            If cmbbreakacc.SelectedIndex >= 0 Then
                cmbsaleretrnacc.Select()
            End If
        End If
        If e.KeyData = Keys.Down Then
            cmbbreakacc.DroppedDown = True
        End If
    End Sub
    Private Sub cmbcashhd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbcashhd.KeyDown
        If e.KeyData = Keys.Enter Then
            If cmbcashhd.SelectedIndex >= 0 Then
                cmb_cash_discount.Select()
            End If
        End If
        If e.KeyData = Keys.Down Then
            cmbcashhd.DroppedDown = True
        End If
    End Sub
    Private Sub cmbratecode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbdefrate.KeyDown
        If e.KeyData = Keys.Enter Then
            If cmbdefrate.SelectedIndex >= 0 Then
                cmbexpenceacc.Select()
            End If
        End If
        If e.KeyData = Keys.Down Then
            cmbdefrate.DroppedDown = True
        End If
    End Sub
    Private Sub cmbdiscntacc_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmb_cash_discount.KeyDown
        If e.KeyData = Keys.Enter Then
            If cmb_cash_discount.SelectedIndex >= 0 Then
                cmbdefshpgdn.Select()
            End If
        End If
        If e.KeyData = Keys.Down Then
            cmb_cash_discount.DroppedDown = True
        End If
    End Sub
    Private Sub cmbexpenceacc_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbexpenceacc.KeyDown
        If e.KeyData = Keys.Enter Then
            If cmbexpenceacc.SelectedIndex >= 0 Then
                cmbrndoffacc.Select()
            End If
        End If
        If e.KeyData = Keys.Down Then
            cmbexpenceacc.DroppedDown = True
        End If
    End Sub
    Private Sub cmbrndoffacc_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbrndoffacc.KeyDown
        If e.KeyData = Keys.Enter Then
            If cmbrndoffacc.SelectedIndex >= 0 Then
                Button1.Select()
            End If
        End If
        If e.KeyData = Keys.Down Then
            cmbrndoffacc.DroppedDown = True
        End If
    End Sub
    Private Sub cmbsaleretrnacc_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbsaleretrnacc.KeyDown
        If e.KeyData = Keys.Enter Then
            If cmbsaleretrnacc.SelectedIndex >= 0 Then
                txtcsalelmt.Select()
            End If
        End If
        If e.KeyData = Keys.Down Then
            cmbsaleretrnacc.DroppedDown = True
        End If
    End Sub
    Private Sub cmbpurchaseacchd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbpurchaseacc_hd.KeyDown
        If e.KeyData = Keys.Enter Then
            If cmbpurchaseacc_hd.SelectedIndex >= 0 Then
                cmbcashhd.Select()
            End If
        End If
        If e.KeyData = Keys.Down Then
            cmbpurchaseacc_hd.DroppedDown = True
        End If
    End Sub
    Private Sub cmbdefshpgdn_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbdefshpgdn.KeyDown
        If e.KeyData = Keys.Enter Then
            If cmbdefshpgdn.SelectedIndex >= 0 Then
                cmbdefrate.Select()
            End If
        End If
        If e.KeyData = Keys.Down Then
            cmbdefshpgdn.DroppedDown = True
        End If
    End Sub
    Private Sub companyparameter_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        frm_ContainerForm.ToolStripLabel1.Text = ""
    End Sub
    Private Sub txtcsalelmt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtcsalelmt.KeyPress
        Select Case e.KeyChar
            Case "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", ".", vbBack
                e.Handled = False
            Case Else
                e.Handled = True
        End Select
    End Sub
    Private Sub txtcsalelmt_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtcsalelmt.KeyDown
        If e.KeyData = Keys.Enter Then
            cmbprinter.DroppedDown = True
            'Button3.Select()
        End If
    End Sub
    Private Sub txtcsalebillno_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtcsalebillno.KeyDown
        If e.KeyData = Keys.Enter And Not txtcsalebillno.Text = "" Then
            cmbsalacc_hd.Select()
        End If
    End Sub
    Private Sub txt_billfooter_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_billfooter.KeyDown
        If e.KeyData = Keys.Enter Then
            txt_billfooter2.Select()
        End If
    End Sub
    Private Sub txt_billfooter2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_billfooter2.KeyDown
        If e.KeyData = Keys.Enter Then
            txt_billfooter3.Select()
        End If
    End Sub
    Private Sub txt_billfooter3_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_billfooter3.KeyDown
        If e.KeyData = Keys.Enter Then
            txt_billfooter4.Select()
        End If
    End Sub
    Private Sub txt_billfooter4_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_billfooter4.KeyDown
        If e.KeyData = Keys.Enter Then
            cmbprinter.Select()
        End If
    End Sub
    Private Sub txtcrcashparty_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyData = Keys.Enter Then
            cmbbreakacc.Select()
        End If
    End Sub
    Private Sub cmbprinter_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyData = Keys.Enter Then
            If cmbprinter.SelectedIndex >= 0 Then
                Button1.Select()
            End If
        End If
        If e.KeyData = Keys.Down Then
            cmbprinter.DroppedDown = True
        End If
    End Sub
    '---button for serialisint the sale transaction ---->
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        s = "exec dbo.serialise_n_reference_sale_bills '" & Module1.yearcode & "','" & Module1.companycode & "'"
        ob.insert(s)
        MsgBox("Sale transaction Serialised")
        active_pos_mode.Select()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles but_browse.Click
        folder_browse.ShowDialog()
        back_up_path_textbox.Text = folder_browse.SelectedPath
        Button1.Select()
    End Sub

    Private Sub active_pos_mode_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles active_pos_mode.Enter
        active_pos_mode.Checked = True
        auto_refresh_current_sale.Select()
    End Sub

    Private Sub auto_refresh_current_sale_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles auto_refresh_current_sale.Enter
        auto_refresh_current_sale.Checked = True
        Button1.Select()
    End Sub

    Private Sub TabPage1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage1.Click
        txtcsalebillno.Select()
    End Sub

    Private Sub TabPage2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage2.Click
        cmbsalacc_hd.Select()
    End Sub

    Private Sub TabPage4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage4.Click
        chk_off.Select()
    End Sub

    Private Sub TabPage5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage5.Click
        but_browse.Select()
    End Sub

    Private Sub print_bill_on_save_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles print_bill_on_save.CheckedChanged
        Button1.Select()
    End Sub
    '---text change event for the surcharger percentage--->
    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        Module1.com_surcharge_percent = TextBox1.Text
    End Sub



End Class