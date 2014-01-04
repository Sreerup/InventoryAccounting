Imports System.Configuration
Imports System.Data.SqlClient

Public Class frm_ContainerForm

    Public flgout As Boolean = False
    Public flpos As Boolean

    Dim check As Boolean
    Dim ds As New DataSet
    Dim s As String
    Dim ob As New Class1

    Private Sub ContainerForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ToolStripStatusLabel6.Width = Me.Width - 520
        logout()
        comcheck()
        '----calling the check registration function -------------->
        check_registration()
        '---end of calling the check registration function -------->
    End Sub
    '---this function is for checking the registration period ----------->
    Private Sub check_registration()
        '---checking if the registration date matches the date for today ------->
        'If Date.Today >= "2012-09-30" Then
        '    MsgBox("System file corrupted")
        '    flgout = True
        '    Me.Close()
        'End If
    End Sub

    Private Sub create_company()
        Dim com_nos = MsgBox("No Company Exist, If you want to import Company Data from old Database then click on 'Yes', If you want to create new Company then click on 'No'.", MsgBoxStyle.YesNoCancel, "No Company")
        logout()
        If com_nos = 6 Then 'this is for the true condition of the msg box
            frm_data_fetch.MdiParent = Me
            frm_data_fetch.Show()
        ElseIf com_nos = 7 Then 'this is for the false condition of the msg box
            Module1.flag1 = 1
            Module1.flag = 1
            Dim frm1 As New frm_createcompany
            frm1.MdiParent = Me
            frm1.Show()
        End If
    End Sub

    Private Function check_company() As Boolean
        check = False
        s = "select  count(*) companyname from companymst"
        If Convert.ToInt32(ob.executereader(s)) > 0 Then
            check = True
        End If
        Return check
    End Function


    Private Function check_database() As Boolean
        check = False
        s = "select count(*) from sys.databases where name='barmanager'"
        If Convert.ToInt32(ob.check_database_present(s)) = 1 Then
            check = True
        End If
        Return check
    End Function

    Public Function check_connection() As Boolean
        check = True
        Try
            Module1.openmaster()
            Module1.closecon()
        Catch ex As SqlException
            check = False
        End Try
        Return check
    End Function


    Public Sub comcheck()
        Module1.server = ConfigurationManager.AppSettings("server_name")
        If (Not ConfigurationManager.AppSettings("server_name") = "" And check_connection()) Then
            If (Not check_database()) Then
                Module1.openmaster()
                Dim attach As New SqlCommand("EXEC sp_attach_db @dbname = 'barmanager', @filename1 = '" & Application.StartupPath & "\barmanager.mdf', @filename2 = '" & Application.StartupPath & "\barmanager_log.ldf'", Module1.con)
                Try
                    attach.ExecuteNonQuery()
                Catch ex As SqlException
                    MsgBox("database cannot be attached to the server " & Module1.server)
                End Try
                Module1.closecon()
                Exit Sub
            End If
            If (check_company()) Then
                frm_login.MdiParent = Me
                frm_login.Show()
            Else
                create_company()
            End If
        Else
            frm_sign_in.MdiParent = Me
            frm_sign_in.Show()
        End If
    End Sub


    Public Sub logout()

        Try
            For Each openoform In Application.OpenForms
                If openoform.Equals(Me) = False Then
                    openoform.Close()
                End If
            Next
        Catch ex As Exception
        End Try
        Try
            Module1.companycode = Nothing
            Module1.companyname = Nothing
            Module1.usercode = Nothing
            Module1.username = Nothing
            Module1.accesslevel = Nothing
            Module1.yearcode = Nothing
            Module1.yearrange = Nothing

            AdminToolStripMenuItem.Enabled = False

            MasterToolStripMenuItem.Enabled = False
            AccountCreationToolStripMenuItem.Enabled = False
            AcountSubGroupToolStripMenuItem.Enabled = False
            LedgerToolStripMenuItem.Enabled = False
            BrandMasterToolStripMenuItem.Enabled = False
            ItemCateToolStripMenuItem.Enabled = False
            KindOfForeignLoquoToolStripMenuItem.Enabled = False
            MeasurePackingToolStripMenuItem.Enabled = False
            StrengthToolStripMenuItem.Enabled = False
            ItemMasterToolStripMenuItem.Enabled = False
            SalesRateToolStripMenuItem.Enabled = False
            TaxShemesToolStripMenuItem.Enabled = False
            StorageLocationToolStripMenuItem.Enabled = False

            TransactionsToolStripMenuItem.Enabled = False
            PurchaseBillToolStripMenuItem.Enabled = False
            PaymentVoucherToolStripMenuItem.Enabled = False
            CounterSaleToolStripMenuItem.Enabled = False
            OpeningStockToolStripMenuItem1.Enabled = False
            StockTransferToolStripMenuItem.Enabled = False
            BeakageEntryToolStripMenuItem.Enabled = False

            REPORTToolStripMenuItem.Enabled = False
            SETTINGSToolStripMenuItem.Enabled = False
            LogOutToolStripMenuItem.Enabled = False
            ChangeCompanyToolStripMenuItem.Enabled = False
            LogToolStripMenuItem.Enabled = True
            ExitToolStripMenuItem2.Enabled = True
            ToolStripButton1.Enabled = True
            ToolStripButton3.Enabled = True
            ToolStripButton2.Enabled = False

            AdminToolStripMenuItem.Visible = False

            MasterToolStripMenuItem.Visible = False
            AccountCreationToolStripMenuItem.Visible = False
            AcountSubGroupToolStripMenuItem.Visible = False
            LedgerToolStripMenuItem.Visible = False
            BrandMasterToolStripMenuItem.Visible = False
            ItemCateToolStripMenuItem.Visible = False
            KindOfForeignLoquoToolStripMenuItem.Visible = False
            MeasurePackingToolStripMenuItem.Visible = False
            StrengthToolStripMenuItem.Visible = False
            ItemMasterToolStripMenuItem.Visible = False
            SalesRateToolStripMenuItem.Visible = False
            TaxShemesToolStripMenuItem.Visible = False
            StorageLocationToolStripMenuItem.Visible = False

            TransactionsToolStripMenuItem.Visible = False
            PurchaseBillToolStripMenuItem.Visible = False
            PaymentVoucherToolStripMenuItem.Visible = False
            CounterSaleToolStripMenuItem.Visible = False
            OpeningStockToolStripMenuItem1.Visible = False
            StockTransferToolStripMenuItem.Visible = False
            BeakageEntryToolStripMenuItem.Visible = False

            REPORTToolStripMenuItem.Visible = False
            SETTINGSToolStripMenuItem.Visible = False
            ExitToolStripMenuItem2.Visible = True
            ToolStripButton1.Visible = True
            ToolStripButton2.Visible = False
            ToolStripButton3.Visible = False

            mnu_closing_stock.Text = ""
            ToolStripLabel2.Text = Nothing
            mnu_closing_stock.Text = Nothing
            ToolStripStatusLabel1.Text = ""
            year_drop.DropDownItems.Clear()
            year_drop.Text = ""
            flgout = True
        Catch ex As Exception
        End Try
    End Sub
    Private Sub KindOfForeignLoquoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KindOfForeignLoquoToolStripMenuItem.Click
        Module1.count = 2
        ToolStripLabel1.Text = "KFL Master"
        frmload()
    End Sub
    Private Sub BrandMasterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BrandMasterToolStripMenuItem.Click

        Module1.count = 1
        ToolStripLabel1.Text = "Brand Master"
        frmload()
    End Sub
    Private Sub MeasurePackingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MeasurePackingToolStripMenuItem.Click

        Module1.count = 3
        ToolStripLabel1.Text = "Measure & Packing Master"
        frmload()

    End Sub
    Private Sub ItemCateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ItemCateToolStripMenuItem.Click
        Module1.count = 4
        ToolStripLabel1.Text = "Category Master"
        frmload()
    End Sub
    Private Sub ItemMasterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ItemMasterToolStripMenuItem.Click
        Module1.count = 5
        ToolStripLabel1.Text = "Item Master"
        frmload()
    End Sub

    Private Sub StrengthToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StrengthToolStripMenuItem.Click
        Module1.count = 6
        ToolStripLabel1.Text = "Strength Master"
        frmload()
    End Sub

    Private Sub SalesRateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SalesRateToolStripMenuItem.Click
        Module1.count = 7
        ToolStripLabel1.Text = "Sales Rate"
        frmload()
    End Sub

    Private Sub StorageLocationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StorageLocationToolStripMenuItem.Click
        Module1.count = 10
        ToolStripLabel1.Text = "Store Master"
        frmload()
    End Sub

    Private Sub CalculatorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CalculatorToolStripMenuItem.Click
        Call Shell("calc.exe")
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Dim l = MsgBox("Do you want to log-out from the current company?", MsgBoxStyle.YesNo, "Log-Out")
        If l = 6 Then
            logout()
        End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        ToolStripStatusLabel3.Text = TimeOfDay
        ToolStripStatusLabel5.Text = Format(Date.Now, "dd/MM/yyyy")
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        logout()
        comcheck()
    End Sub

    Private Sub MasterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MasterToolStripMenuItem.Click, MasterToolStripMenuItem.DropDownOpened
        ToolStripLabel2.Text = "Masters"
    End Sub

    Private Sub AdminToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AdminToolStripMenuItem.Click, AdminToolStripMenuItem.DropDownOpened
        ToolStripLabel2.Text = "Admin"
    End Sub

    Private Sub CREATECOMPANYToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CREATECOMPANYToolStripMenuItem.Click
        Module1.count = 8
        ToolStripLabel1.Text = "Company Information"
        Call frmload()
    End Sub

    Private Sub CompanyParameterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CompanyParameterToolStripMenuItem.Click
        Module1.count = 0
        frmload()
        Dim frm As New frm_companyparameter
        frm.MdiParent = Me
        ToolStripLabel1.Text = "Company Parameter"
        frm.Show()
    End Sub

    Private Sub LogOutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LogOutToolStripMenuItem.Click
        Dim l = MsgBox("Do you want to log-out from the current company?", MsgBoxStyle.YesNo, "Log-Out")
        If l = 6 Then
            logout()
        End If
    End Sub

    Private Sub LogToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LogToolStripMenuItem.Click
        ToolStripButton2.Visible = True
        ToolStripButton1.Visible = False
        ToolStripButton3.Visible = False
        ToolStripButton2.Enabled = False
        comcheck()
    End Sub

    Private Sub UserInformationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UserInformationToolStripMenuItem.Click
        Module1.count = 21
        ToolStripLabel1.Text = "User Information"
        Call frmload()
    End Sub

    Private Sub LedgerToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LedgerToolStripMenuItem.Click
        Module1.count = 13
        ToolStripLabel1.Text = "Ledger Master"
        frmload()
    End Sub

    Private Sub AcountSubGroupToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AcountSubGroupToolStripMenuItem.Click
        Module1.count = 12
        ToolStripLabel1.Text = "Account Sub Group"
        frmload()
    End Sub

    Private Sub AccountCreationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AccountCreationToolStripMenuItem.Click
        Module1.count = 11
        ToolStripLabel1.Text = "Account Main Group"
        frmload()
    End Sub

    Private Sub TransactionsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TransactionsToolStripMenuItem.Click, TransactionsToolStripMenuItem.DropDownOpened
        ToolStripLabel2.Text = "Transactions"
    End Sub

    Private Sub REPORTToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles REPORTToolStripMenuItem.Click, REPORTToolStripMenuItem.DropDownOpened
        ToolStripLabel2.Text = "Reports"
    End Sub

    Private Sub SETTINGSToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SETTINGSToolStripMenuItem.Click, SETTINGSToolStripMenuItem.DropDownOpened
        ToolStripLabel2.Text = "Settings"
    End Sub

    Private Sub UserRightsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UserRightsToolStripMenuItem.Click
        Module1.count = 22
        ToolStripLabel1.Text = "User Rights"
        Call frmload()
    End Sub

    Private Sub CounterSaleToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CounterSaleToolStripMenuItem.Click
        Module1.count = 15
        ToolStripLabel1.Text = "Counter Sale"
        Call frmload()
    End Sub

    Private Sub OpeningStockToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpeningStockToolStripMenuItem1.Click
        Module1.count = 14
        ToolStripLabel1.Text = "Opening Stock"
        Call frmload()
    End Sub

    Private Sub CHANGEIDToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CHANGEIDToolStripMenuItem.Click
        Module1.count = 20
        Dim frm As New frm_userinfo
        frm.MdiParent = Me
        ToolStripLabel1.Text = "Change Password"
        frm.Show()
    End Sub

    Public Sub frmload()
        For Each openoform In Me.MdiChildren
            If openoform.Equals(Me) = False Then
                openoform.Close()
            End If
        Next
        If Module1.count <> 0 Then
            If Button1.Text = "<" Then
                accessbarlayout2()
            Else
                accessbarlayout1()
            End If
            frm_MainForm.DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            frm_MainForm.MdiParent = Me
            frm_MainForm.Show()
        End If
    End Sub
    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem2.Click
        Me.Close()
    End Sub

    Private Sub BeakageEntryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BeakageEntryToolStripMenuItem.Click
        Module1.count = 16
        ToolStripLabel1.Text = "Breakage Entry"
        Call frmload()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Button1.Text = "<" Then
            accessbarlayout1()
            Button1.Text = ">"
        Else
            accessbarlayout2()
            Button1.Text = "<"
        End If
        frm_MainForm.DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        Me.Refresh()
    End Sub

    Public Sub accessbarlayout1()
        ToolStrip2.Visible = False
        Button1.Left = 0
        frm_MainForm.Width = Me.Width - 31
        frm_MainForm.ToolStrip1.Width = frm_MainForm.Width
        frm_MainForm.ToolStripLabel1.Width = (Me.Width / 2.6)
        frm_MainForm.Button1.Left = (Me.Width / 8.5)
        frm_MainForm.Button2.Left = (Me.Width / 4.8)
        frm_MainForm.Button3.Left = (Me.Width / 3.35)
        frm_MainForm.TextBox3.Left = (Me.Width / 2)
        frm_MainForm.Button5.Left = (Me.Width / 1.28)
        frm_MainForm.DataGridView1.Width = Me.Width / 1.08936
    End Sub
    Public Sub accessbarlayout2()
        ToolStrip2.Visible = True
        Button1.Left = ToolStrip2.Width
        frm_MainForm.Width = Me.Width - 181
        frm_MainForm.ToolStrip1.Width = frm_MainForm.Width
        frm_MainForm.ToolStripLabel1.Width = (Me.Width / 3.24)
        frm_MainForm.Button1.Left = (Me.Width / 22.75)
        frm_MainForm.Button2.Left = (Me.Width / 7.42)
        frm_MainForm.Button3.Left = (Me.Width / 4.43)
        frm_MainForm.TextBox3.Left = (Me.Width / 2.39)
        frm_MainForm.Button5.Left = (Me.Width / 1.41)
        frm_MainForm.DataGridView1.Width = Me.Width / 1.2962
    End Sub


    Private Sub PurchaseBillToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PurchaseBillToolStripMenuItem.Click
        Module1.count = 23
        ToolStripLabel1.Text = "Purchase Bill"
        Call frmload()
    End Sub

    Private Sub HelpToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HelpToolStripMenuItem.Click
        frm_AboutBox1.Show()
    End Sub

    Private Sub TaxShemesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TaxShemesToolStripMenuItem.Click
        Module1.count = 24
        ToolStripLabel1.Text = "Tax Scheme"
        Call frmload()
    End Sub

    Private Sub NeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NeToolStripMenuItem.Click
        frm_yearcreation.Show()
    End Sub


    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
        Dim c = MsgBox("Do you want to log-out from the current company?", MsgBoxStyle.YesNo, "Log-Out")
        If c = 6 Then
            logout()
            comcheck()
        End If
    End Sub

    Private Sub ToolStripLabel23_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripLabel23.Click
        frm_AboutBox1.Show()
        Me.Enabled = False
    End Sub

    Private Sub year_drop_DropDownItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles year_drop.DropDownItemClicked
        year_drop.Text = e.ClickedItem.Text
        s = "select * from yearmst where companycode='" & Module1.companycode & "' and yearrange='" & year_drop.Text & "'"
        ds = ob.populate(s)
        Module1.yearcode = ds.Tables(0).Rows(0).Item(0)
        Module1.comstdate = ds.Tables(0).Rows(0).Item(2)
        Module1.comenddate = ds.Tables(0).Rows(0).Item(3)
        For Each openoform In Me.MdiChildren
            If openoform.Equals(Me) = False Then
                openoform.Close()
            End If
        Next
    End Sub

    Private Sub StockTransferToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StockTransferToolStripMenuItem.Click
        Module1.count = 25
        frmload()
    End Sub
    Private Sub ContainerForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If flgout = False Then
            Dim q = MsgBox("Do you want to close the system?.", MsgBoxStyle.YesNo, "Close")
            If q = 6 Then
                logout()
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub ReconnectToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        frm_sign_in.Show()
    End Sub

    Private Sub ChangeCompanyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChangeCompanyToolStripMenuItem.Click
        Dim c = MsgBox("Do you want to log-out from the current company?", MsgBoxStyle.YesNo, "Log-Out")
        If c = 6 Then
            logout()
            comcheck()
        End If
    End Sub

    Private Sub DisconnectServerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        con = New SqlConnection("Data Source=" & Module1.server & ";Initial Catalog=master;Integrated Security=True;Network Library=dbnmpntw")
        con.Open()
        Dim cmd As New SqlCommand("exec sp_detach_db @dbname='barmanager'", con)
        con.Close()
        logout()
        frm_sign_in.Show()
    End Sub

    Private Sub ChangeServerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChangeServerToolStripMenuItem.Click
        frm_sign_in.MdiParent = Me
        frm_sign_in.Show()
    End Sub
    Private Sub PaymentVoucherToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PaymentVoucherToolStripMenuItem.Click
        Module1.count = 26
        ToolStripLabel1.Text = "Receipt Voucher Entry"
        Call frmload()
    End Sub

    Private Sub PaymentVoucherToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PaymentVoucherToolStripMenuItem1.Click
        Module1.count = 27
        ToolStripLabel1.Text = "PayMent Voucher Entry"
        Call frmload()
    End Sub

    Private Sub BackUpToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BackUpToolStripMenuItem.Click
        'Try
        s = "backup database barmanager to disk='" & Module1.back_up_path & "\barmanager.bak'"
        ob.insert(s)
        'Catch ex As SqlException
        '    If ex.ToString = '& Module1.back_up_path & "\barmanager.bak'". Operating system error 3(The system cannot find the path specified.). BACKUP DATABASE is terminating abnormally" Then
        '        MsgBox("bck up failed to create")
        'Finally
        MsgBox("back up created successfully")
        'End Try




    End Sub



    Private Sub DataFetchToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataFetchToolStripMenuItem.Click
        frm_data_fetch.Show()
    End Sub
    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click
        Module1.report_no = 1
        ToolStripLabel1.Text = "Daily Stock Brand Wise"
        frm_ReportForm.Text = "Daily Stock Brand Wise"
        frm_ReportForm.Show()
    End Sub

    Private Sub ToolStripMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem3.Click
        Module1.report_no = 2
        ToolStripLabel1.Text = "Monthly Stock Brand Wise"
        frm_ReportForm.Text = "Monthly Stock Brand Wise"
        frm_ReportForm.Show()
    End Sub

    Private Sub ToolStripMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem4.Click
        Module1.report_no = 3
        ToolStripLabel1.Text = "Day to Day Stock Brand Wise"
        frm_ReportForm.Text = "Day to Day Stock Brand Wise"
        frm_ReportForm.Show()
    End Sub

    Private Sub StocksByBrandToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StocksByBrandToolStripMenuItem.Click
        Module1.report_no = 3
        ToolStripLabel1.Text = "Stock Report - Brand Wise"
        frm_ReportForm.Text = "Stock Report - Brand Wise"
        frm_ReportForm.Show()
    End Sub

    Private Sub mnu_daily_stock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_daily_stock.Click
        Module1.report_no = 4
        ToolStripLabel1.Text = "Daily Stock Category Wise"
        frm_ReportForm.Text = "Daily Stock Category Wise"
        frm_ReportForm.Show()
    End Sub

    Private Sub mnu_monthly_stock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_monthly_stock.Click
        Module1.report_no = 5
        ToolStripLabel1.Text = "Monthly Stock Category Wise"
        frm_ReportForm.Text = "Monthly Stock Category Wise"
        frm_ReportForm.Show()
    End Sub

    Private Sub mnu_d2d_stock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_d2d_stock.Click
        Module1.report_no = 6
        ToolStripLabel1.Text = "Day to Day Srock Category Wise"
        frm_ReportForm.Text = "Day to Day Srock Category Wise"
        frm_ReportForm.Show()
    End Sub

    Private Sub StockReportsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StockReportsToolStripMenuItem.Click
        Module1.report_no = 6
        ToolStripLabel1.Text = "Srock Report - Category Wise"
        frm_ReportForm.Text = "Srock Category Wise"
        frm_ReportForm.Show()
    End Sub


    Private Sub mnu_daily_sale_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_daily_sale.Click
        Module1.report_no = 7
        ToolStripLabel1.Text = "Daily Sales Statement"
        frm_ReportForm.Text = "Daily Sales Statement"
        frm_ReportForm.Show()
    End Sub

    Private Sub mnu_monthly_sale_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_monthly_sale.Click
        Module1.report_no = 8
        ToolStripLabel1.Text = "Monthly Sales Statement"
        frm_ReportForm.Text = "Monthly Sales Statement"
        frm_ReportForm.Show()
    End Sub

    Private Sub mnu_d2d_sale_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_d2d_sale.Click
        Module1.report_no = 9
        ToolStripLabel1.Text = "Day to Day Sales Statement"
        frm_ReportForm.Text = "Day to Day Sales Statement"
        frm_ReportForm.Show()
    End Sub

    Private Sub SaleReportsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaleReportsToolStripMenuItem.Click
        Module1.report_no = 9
        ToolStripLabel1.Text = "Sales Statement"
        frm_ReportForm.Text = "Sales Statement"
        frm_ReportForm.Show()
    End Sub

    Private Sub mnu_multybill_print_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_multybill_print.Click
        Module1.report_no = 10
        ToolStripLabel1.Text = "Multy Bill Print"
        frm_ReportForm.Text = "Multy Bill Print"
        frm_ReportForm.Show()
    End Sub

    Private Sub ToolStripMenuItem8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem8.Click
        Module1.report_no = 11
        ToolStripLabel1.Text = "Daily Stock Item Wise"
        frm_ReportForm.Text = "Daily Stock Item Wise"
        frm_ReportForm.Show()
    End Sub

    Private Sub ToolStripMenuItem12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem12.Click
        Module1.report_no = 12
        ToolStripLabel1.Text = "Monthly Stock Item Wise"
        frm_ReportForm.Text = "Monthly Stock Item Wise"
        frm_ReportForm.Show()
    End Sub

    Private Sub ToolStripMenuItem13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem13.Click
        Module1.report_no = 13
        ToolStripLabel1.Text = "Day to Day Srock Item Wise"
        frm_ReportForm.Text = "Day to Day Srock Item Wise"
        frm_ReportForm.Show()
    End Sub

    Private Sub StockByItemsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StockByItemsToolStripMenuItem.Click
        Module1.report_no = 13
        ToolStripLabel1.Text = "Srock Report - Item Wise"
        frm_ReportForm.Text = "Srock Report - Item Wise"
        frm_ReportForm.Show()
    End Sub

    Private Sub ToolStripMenuItem11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem11.Click
        Module1.report_no = 14
        ToolStripLabel1.Text = "Daily Stock ML Wise"
        frm_ReportForm.Text = "Daily Stock ML Wise"
        frm_ReportForm.Show()
    End Sub

    Private Sub ToolStripMenuItem15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem15.Click
        Module1.report_no = 15
        ToolStripLabel1.Text = "Monthly Stock ML Wise"
        frm_ReportForm.Text = "Monthly Stock ML Wise"
        frm_ReportForm.Show()
    End Sub

    Private Sub ToolStripMenuItem16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem16.Click
        Module1.report_no = 16
        ToolStripLabel1.Text = "Day to Day  Stock ML Wise"
        frm_ReportForm.Text = "Day to Day  Stock ML Wise"
        frm_ReportForm.Show()
    End Sub

    Private Sub StockByMLToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StockByMLToolStripMenuItem.Click
        Module1.report_no = 16
        ToolStripLabel1.Text = "Stock Report - ML Wise"
        frm_ReportForm.Text = "Stock Report - ML Wise"
        frm_ReportForm.Show()
    End Sub

    Private Sub mnu_daily_purchase_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_daily_purchase.Click
        Module1.report_no = 17
        ToolStripLabel1.Text = "Daily Purchase Report - Suupplier Wise Details"
        frm_ReportForm.Text = "Daily Purchase Report - Suupplier Wise Details"
        frm_ReportForm.Show()
    End Sub

    Private Sub mnu_monthly_purchase_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_monthly_purchase.Click
        Module1.report_no = 18
        ToolStripLabel1.Text = "Monthly Purchase Report - Suupplier Wise Details"
        frm_ReportForm.Text = "Monthly Purchase Report - Suupplier Wise Details"
        frm_ReportForm.Show()
    End Sub

    Private Sub mnu_d2d_purchase_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_d2d_purchase.Click
        Module1.report_no = 19
        ToolStripLabel1.Text = "Day to Day Purchase Report - Suupplier Wise Details"
        frm_ReportForm.Text = "Day to Day Purchase Report - Suupplier Wise Details"
        frm_ReportForm.Show()
    End Sub


    Private Sub PurchaseReportsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PurchaseReportsToolStripMenuItem.Click
        Module1.report_no = 19
        ToolStripLabel1.Text = "Purchase Report - Suupplier Wise Details"
        frm_ReportForm.Text = "Purchase Report - Suupplier Wise Details"
        frm_ReportForm.Show()
    End Sub

    Private Sub ToolStripMenuItem14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem14.Click
        Module1.report_no = 20
        ToolStripLabel1.Text = "Daily Purchase Report - Bill No Wise Details"
        frm_ReportForm.Text = "Daily Purchase Report - Bill No Wise Details"
        frm_ReportForm.Show()
    End Sub

    Private Sub ToolStripMenuItem18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem18.Click
        Module1.report_no = 21
        ToolStripLabel1.Text = "Monthly Purchase Report - Bill No Wise Details"
        frm_ReportForm.Text = "Monthly Purchase Report - Bill No Wise Details"
        frm_ReportForm.Show()
    End Sub

    Private Sub ToolStripMenuItem19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem19.Click
        Module1.report_no = 22
        ToolStripLabel1.Text = "Day to Day Purchase Report - Bill No Wise Details"
        frm_ReportForm.Text = "Day to Day Purchase Report - Bill No Wise Details"
        frm_ReportForm.Show()
    End Sub

    Private Sub PurchaseByBillNoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PurchaseByBillNoToolStripMenuItem.Click
        Module1.report_no = 22
        ToolStripLabel1.Text = "Purchase Report - Bill No Wise Details"
        frm_ReportForm.Text = "Purchase Report - Bill No Wise Details"
        frm_ReportForm.Show()
    End Sub

    Private Sub ToolStripMenuItem17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem17.Click
        Module1.report_no = 23
        ToolStripLabel1.Text = "Daily Purchase Report - Item Wise Summary"
        frm_ReportForm.Text = "Daily Purchase Report - Item Wise Summary"
        frm_ReportForm.Show()
    End Sub

    Private Sub ToolStripMenuItem21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem21.Click
        Module1.report_no = 24
        ToolStripLabel1.Text = "Monthly Purchase Report - Item Wise Summary"
        frm_ReportForm.Text = "Monthly Purchase Report - Item Wise Summary"
        frm_ReportForm.Show()
    End Sub

    Private Sub ToolStripMenuItem22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem22.Click
        Module1.report_no = 25
        ToolStripLabel1.Text = "Day to Day Purchase Report - Item Wise Summary"
        frm_ReportForm.Text = "Day to Day Purchase Report - Item Wise Summary"
        frm_ReportForm.Show()
    End Sub

    Private Sub PurchaseByItemToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PurchaseByItemToolStripMenuItem.Click
        Module1.report_no = 25
        ToolStripLabel1.Text = "Purchase Report - Item Wise Summary"
        frm_ReportForm.Text = "Purchase Report - Item Wise Summary"
        frm_ReportForm.Show()
    End Sub


    Private Sub ToolStripMenuItem20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem20.Click
        Module1.report_no = 26
        ToolStripLabel1.Text = "Day to Day Purchase Report - Bill No Wise Summary"
        frm_ReportForm.Text = "Day to Day Purchase Report - Bill No Wise Summary"
        frm_ReportForm.Show()
    End Sub

    Private Sub ToolStripMenuItem24_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem24.Click
        Module1.report_no = 27
        ToolStripLabel1.Text = "Monthly Purchase Report - Bill No Wise Summary"
        frm_ReportForm.Text = "Monthly Purchase Report - Bill No Wise Summary"
        frm_ReportForm.Show()
    End Sub

    Private Sub ToolStripMenuItem25_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem25.Click
        Module1.report_no = 28
        ToolStripLabel1.Text = "Day to Day Purchase Report - Bill No Wise Summary"
        frm_ReportForm.Text = "Day to Day Purchase Report - Bill No Wise Summary"
        frm_ReportForm.Show()
    End Sub

    Private Sub PurchaseSummaryByBillNoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PurchaseSummaryByBillNoToolStripMenuItem.Click
        Module1.report_no = 28
        ToolStripLabel1.Text = "Purchase Report - Bill No Wise Summary"
        frm_ReportForm.Text = "Purchase Report - Bill No Wise Summary"
        frm_ReportForm.Show()
    End Sub


    '---mouse click event for the accounts report----->
    Private Sub AccountsReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AccountsReportToolStripMenuItem.Click
        Module1.report_no = 35
        ToolStripLabel1.Text = "Accounts Report"
        frm_ReportForm.Text = "Accounts Report"
        frm_ReportForm.Show()
    End Sub


    Private Sub ToolStripMenuItem42_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem42.Click
        Module1.report_no = 36
        ToolStripLabel1.Text = "Daily Sales Report - ML Wise"
        frm_ReportForm.Text = "Daily Sales Report - ML Wise"
        frm_ReportForm.Show()
    End Sub

    Private Sub ToolStripMenuItem43_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem43.Click
        Module1.report_no = 37
        ToolStripLabel1.Text = "Monthly Sales Report - ML Wise"
        frm_ReportForm.Text = "Monthly Sales Report - ML Wise"
        frm_ReportForm.Show()
    End Sub

    '----------sales report ml wise --------->
    Private Sub ToolStripMenuItem26_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem26.Click
        Module1.report_no = 38
        ToolStripLabel1.Text = "Sales Report - ML Wise"
        frm_ReportForm.Text = "Sales Report - ML Wise"
        frm_ReportForm.Show()
    End Sub

    Private Sub ToolStripMenuItem44_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem44.Click
        Module1.report_no = 38
        ToolStripLabel1.Text = "Day to Day Sales Report - ML Wise"
        frm_ReportForm.Text = "Day to Day Sales Report - ML Wise"
        frm_ReportForm.Show()
    End Sub

    Private Sub ToolStripMenuItem36_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem36.Click
        Module1.report_no = 39
        ToolStripLabel1.Text = "Daily Sales Report - Item Wise"
        frm_ReportForm.Text = "Daily Sales Report - Item Wise"
        frm_ReportForm.Show()
    End Sub

    Private Sub ToolStripMenuItem37_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem37.Click
        Module1.report_no = 40
        ToolStripLabel1.Text = "Monthly Sales Report - Item Wise"
        frm_ReportForm.Text = "Monthly Sales Report - Item Wise"
        frm_ReportForm.Show()
    End Sub

    Private Sub ToolStripMenuItem38_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem38.Click
        Module1.report_no = 41
        ToolStripLabel1.Text = "Day to Day Sales Report - Item Wise"
        frm_ReportForm.Text = "Day to Day Sales Report - Item Wise"
        frm_ReportForm.Show()
    End Sub

    '----sale report itemwise --------->
    Private Sub ToolStripMenuItem35_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem35.Click
        Module1.report_no = 41
        ToolStripLabel1.Text = "Sales and Closing Report"
        frm_ReportForm.Text = "Sales and Closing Report"
        frm_ReportForm.Show()
    End Sub

    Private Sub ToolStripMenuItem32_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem32.Click
        Module1.report_no = 42
        ToolStripLabel1.Text = "Daily Sales Report - Bill Wise"
        frm_ReportForm.Text = "Daily Sales Report - Bill Wise"
        frm_ReportForm.Show()
    End Sub

    Private Sub ToolStripMenuItem33_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem33.Click
        Module1.report_no = 43
        ToolStripLabel1.Text = "Monthly Sales Report - Bill Wise"
        frm_ReportForm.Text = "Monthly Sales Report - Bill Wise"
        frm_ReportForm.Show()
    End Sub

    Private Sub ToolStripMenuItem31_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem31.Click
        Module1.report_no = 44
        ToolStripLabel1.Text = "Sales Report - Bill Wise"
        frm_ReportForm.Text = "Sales Report - Bill Wise"
        frm_ReportForm.Show()
    End Sub

    Private Sub ToolStripMenuItem34_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem34.Click
        Module1.report_no = 44
        ToolStripLabel1.Text = "Day to Day Sales Report - Bill Wise"
        frm_ReportForm.Text = "Day to Day Sales Report - Bill Wise"
        frm_ReportForm.Show()
    End Sub

    Private Sub ToolStripMenuItem39_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem39.Click
        Module1.report_no = 45
        ToolStripLabel1.Text = "Daily Sales Report - Party Wise"
        frm_ReportForm.Text = "Daily Sales Report - Party Wise"
        frm_ReportForm.Show()
    End Sub

    Private Sub ToolStripMenuItem40_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem40.Click
        Module1.report_no = 46
        ToolStripLabel1.Text = "Monthly Sales Report - Party Wise"
        frm_ReportForm.Text = "Monthly Sales Report - Party Wise"
        frm_ReportForm.Show()
    End Sub

    Private Sub ToolStripMenuItem41_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem41.Click
        Module1.report_no = 47
        ToolStripLabel1.Text = "Day to Day Sales Report - Party Wise"
        frm_ReportForm.Text = "Day to Day Sales Report - Party Wise"
        frm_ReportForm.Show()
    End Sub


    Private Sub ToolStripMenuItem23_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem23.Click
        Module1.report_no = 47
        ToolStripLabel1.Text = "Sales Report - Party Wise"
        frm_ReportForm.Text = "Sales Report - Party Wise"
        frm_ReportForm.Show()
    End Sub

    Private Sub ToolStripMenuItem28_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem28.Click
        Module1.report_no = 48
        ToolStripLabel1.Text = "Daily Sales Report - Amount Wise"
        frm_ReportForm.Text = "Daily Sales Report - Amount Wise"
        frm_ReportForm.Show()
    End Sub

    Private Sub ToolStripMenuItem29_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem29.Click
        Module1.report_no = 49
        ToolStripLabel1.Text = "Monthly Sales Report - Amount Wise"
        frm_ReportForm.Text = "Monthly Sales Report - Amount Wise"
        frm_ReportForm.Show()
    End Sub

    Private Sub ToolStripMenuItem30_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem30.Click
        Module1.report_no = 50
        ToolStripLabel1.Text = "Day to Day Sales Report - Amount Wise"
        frm_ReportForm.Text = "Day to Day Sales Report - Amount Wise"
        frm_ReportForm.Show()
    End Sub


    Private Sub ToolStripMenuItem27_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem27.Click
        Module1.report_no = 50
        ToolStripLabel1.Text = "Sales Report - Amount Wise"
        frm_ReportForm.Text = "Sales Report - Amount Wise"
        frm_ReportForm.Show()
    End Sub

    Private Sub BreakageReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BreakageReportToolStripMenuItem.Click
        Module1.report_no = 51
        ToolStripLabel1.Text = "Breakage Report"
        frm_ReportForm.Text = "Breakage Report"
        frm_ReportForm.Show()
    End Sub

    Private Sub SalesReturnToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SalesReturnToolStripMenuItem.Click
        Module1.report_no = 52
        ToolStripLabel1.Text = "Sales Return Report"
        frm_ReportForm.Text = "Sales Return Report"
        frm_ReportForm.Show()
    End Sub


    Private Sub StockReportValueWiseDetailToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StockReportValueWiseDetailToolStripMenuItem.Click
        Module1.report_no = 53
        ToolStripLabel1.Text = "Stock Report - Value Wise Detail"
        frm_ReportForm.Text = "Stock Report - Value Wise Detail"
        frm_ReportForm.Show()
    End Sub

    Private Sub StockReportValueWiseClosingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StockReportValueWiseClosingToolStripMenuItem.Click
        Module1.report_no = 54
        ToolStripLabel1.Text = "Stock Report - Value Wise Closing"
        frm_ReportForm.Text = "Stock Report - Value Wise Closing"
        frm_ReportForm.Show()
    End Sub

    '---bulk liter sales report---->
    Private Sub BulkLtrToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BulkLtrToolStripMenuItem.Click
        Module1.report_no = 55
        ToolStripLabel1.Text = "Bulk Ltr Sale Report"
        frm_ReportForm.Text = "Bulk Ltr Sale Report"
        frm_ReportForm.Show()
    End Sub

    '---the new excise report---------->
    Private Sub ExciseReportsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExciseReportsToolStripMenuItem.Click
        Module1.report_no = 56
        ToolStripLabel1.Text = "Excise Report"
        frm_ReportForm.Text = "Excise Report"
        frm_ReportForm.Show()
    End Sub



    '---sales import to tally---->
    Private Sub ImportSalesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImportSalesToolStripMenuItem.Click
        Module1.count = 100
        frm_tally_Import.Show()
    End Sub
    '---purchase import to tally--->
    Private Sub ImportPurchaseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImportPurchaseToolStripMenuItem.Click
        Module1.count = 101
        frm_tally_Import.Show()
    End Sub




    Private Sub UpgradeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UpgradeToolStripMenuItem.Click
        '======command to upgrade the databse ========
        s = "IF EXISTS (SELECT * FROM information_schema.columns WHERE table_name='ledger' and column_name='yearcode') alter table ledger drop column yearcode "
        ob.insert(s)
        s = "IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'acountname') AND name = N'compo') alter table acountname drop constraint compo"
        ob.insert(s)
        s = "IF  EXISTS (SELECT * FROM information_schema.columns WHERE table_name='acountname' AND column_name = 'companycode')alter table acountname drop column companycode"
        ob.insert(s)
        s = "IF  EXISTS (SELECT * FROM information_schema.columns WHERE table_name='acountname' AND column_name='accode') alter table acountname alter column accode varchar(6) not null"
        ob.insert(s)
        s = "IF  NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'acountname') AND name = N'PK_acountname')alter table acountname add constraint PK_acountname primary key(accode)"
        ob.insert(s)
        s = "IF  EXISTS (SELECT * FROM information_schema.columns WHERE table_name='companyparamtr' AND column_name = 'csaleacc') exec sp_rename 'companyparamtr.csaleacc','SaleAccount_Head','COLUMN' "
        ob.insert(s)
        s = "IF NOT EXISTS (SELECT * FROM information_schema.columns WHERE table_name='companyparamtr' AND column_name = 'print_mode') alter table companyparamtr add print_mode varchar(7) null"
        ob.insert(s)
        s = "IF NOT EXISTS (SELECT * FROM information_schema.columns WHERE table_name='companyparamtr' AND column_name = 'surcharge_account_head') alter table companyparamtr add surcharge_account_head varchar(6) null"
        ob.insert(s)
        s = "IF NOT EXISTS (SELECT * FROM information_schema.columns WHERE table_name='companyparamtr' AND column_name = 'back_up_restore_path') alter table companyparamtr add  back_up_restore_path varchar(max) null"
        ob.insert(s)
        s = "IF EXISTS (SELECT * FROM information_schema.tables WHERE table_name='defacgroup') DROP TABLE defacgroup"
        ob.insert(s)
        s = "IF EXISTS (SELECT * FROM information_schema.tables WHERE table_name='defgroupmst') DROP TABLE defgroupmst"
        ob.insert(s)
        s = "IF EXISTS (SELECT * FROM information_schema.tables WHERE table_name='defledger') DROP TABLE defledger"
        ob.insert(s)
        s = "if not exists (SELECT * FROM information_schema.columns WHERE table_name='itemmst' and column_name='purchaseRate') alter table itemmst add purchaseRate float null"
        ob.insert(s)
        s = "IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'itemratemst') AND name = N'fk3') alter table itemratemst drop constraint fk3"
        ob.insert(s)
        s = "IF EXISTS (SELECT * FROM information_schema.tables WHERE table_name='misc_acc') DROP TABLE misc_acc"
        ob.insert(s)
        s = "IF EXISTS (SELECT * FROM information_schema.tables WHERE table_name='party_acc') DROP TABLE party_acc"
        ob.insert(s)
        s = "IF EXISTS (SELECT * FROM information_schema.tables WHERE table_name='receiptpayment_acc') DROP TABLE receiptpayment_acc"
        ob.insert(s)
        s = "IF EXISTS (SELECT * FROM information_schema.tables WHERE table_name='salepurchase_acc') DROP TABLE salepurchase_acc"
        ob.insert(s)
        '---creating the tables payment detail and payment main incase it doesnot exist--->
        s = "IF NOT EXISTS (SELECT * FROM information_schema.tables WHERE table_name='payment_main') CREATE TABLE payment_main(companycode int,yearcode int,vchno int,head_account varchar(6),client_account varchar(6),narration varchar(50),vchdate datetime,cheque_no varchar,due float,amount_paid float,discount float,net_due float,discount_account_head varchar(6))"
        ob.insert(s)
        s = "IF Not EXISTS (SELECT * FROM information_schema.tables WHERE table_name='payment_detail') CREATE TABLE payment_detail(companycode int,yearcode int,vchno int,trnno int,due_amount float,debit float,credit float,vchdate datetime)"
        ob.insert(s)
        '---altering the table payment main--->
        s = "IF EXISTS (SELECT * FROM information_schema.columns WHERE table_name='payment_main' and column_name='due_on_total_bill') EXEC sp_rename 'payment_main.due_on_total_bill','due','COLUMN' "
        ob.insert(s)
        s = "IF EXISTS (SELECT * FROM information_schema.columns WHERE table_name='payment_main' and column_name='total_ad_or_less') EXEC sp_rename 'payment_main.total_ad_or_less','discount','COLUMN'"
        ob.insert(s)
        s = "IF EXISTS (SELECT * FROM information_schema.columns WHERE table_name='payment_main' and column_name='net_amount') EXEC sp_rename  'payment_main.net_amount','net_due','COLUMN'"
        ob.insert(s)
        s = "IF not EXISTS (SELECT * FROM information_schema.columns WHERE table_name='payment_main' and column_name='Discount_account_head') alter table payment_main add  Discount_account_head varchar(6) null"
        ob.insert(s)
        s = "IF EXISTS (SELECT * FROM information_schema.tables WHERE table_name='paymentreceive') DROP TABLE paymentreceive"
        ob.insert(s)
        s = "IF not EXISTS (SELECT * FROM information_schema.columns WHERE table_name='salesbillmain' and column_name='account_head') alter table salesbillmain add account_head varchar(6) null"
        ob.insert(s)
        s = "IF not EXISTS (SELECT * FROM information_schema.columns WHERE table_name='salesbillmain' and column_name='discount_account_head') alter table salesbillmain add discount_account_head varchar(6) null"
        ob.insert(s)
        s = "IF EXISTS (SELECT * FROM information_schema.columns WHERE table_name='salesbilldetail' and column_name='billno') alter table salesbilldetail drop column billno"
        ob.insert(s)
        s = "IF EXISTS (SELECT * FROM information_schema.columns WHERE table_name='receipt_main' and column_name='due_on_total_bill') EXEC sp_rename 'receipt_main.due_on_total_bill','due','COLUMN' "
        ob.insert(s)
        s = "IF EXISTS (SELECT * FROM information_schema.columns WHERE table_name='receipt_main' and column_name='total_ad_or_less') EXEC sp_rename 'receipt_main.total_ad_or_less','discount','COLUMN'"
        ob.insert(s)
        s = "IF EXISTS (SELECT * FROM information_schema.columns WHERE table_name='receipt_main' and column_name='net_amount') EXEC sp_rename  'receipt_main.net_amount','net_due','COLUMN'"
        ob.insert(s)
        '---creating the table receipt_main and receipt_details if ti doesnot esists-->
        s = "IF NOT EXISTS (SELECT * FROM information_schema.tables WHERE table_name='receipt_main') CREATE TABLE receipt_main(companycode int,yearcode int,vchno int,head_account varchar(6),client_account varchar(6),narration varchar(50),vchdate datetime,cheque_no varchar(20),due float,amount_paid float,discount float,net_due float,Discount_account_head varchar(6),receipt_type varchar(15))"
        ob.insert(s)
        s = "IF NOT EXISTS (SELECT * FROM information_schema.tables WHERE table_name='receipt_detail') CREATE TABLE receipt_detail(companycode int,yearcode int,vchno int,trnno int,due_amount float,debit float,credit float,vchdate datetime)"
        ob.insert(s)
        '--altering the table receipt_main--->
        s = "IF not EXISTS (SELECT * FROM information_schema.columns WHERE table_name='receipt_main' and column_name='Discount_account_head') alter table receipt_main add  Discount_account_head varchar(6) null"
        ob.insert(s)
        s = "IF not EXISTS (SELECT * FROM information_schema.columns WHERE table_name='receipt_main' and column_name='receipt_type') alter table receipt_main add receipt_type varchar(15) null"
        ob.insert(s)
        s = "if exists(select * from sys.triggers where name='companydel') DROP TRIGGER companydel"
        ob.insert(s)
        s = "if exists(select * from sys.triggers where name='create_companyparamtr') DROP TRIGGER create_companyparamtr"
        ob.insert(s)
        s = "if exists(select * from sys.triggers where name='def_itemrateinfo_create') DROP TRIGGER def_itemrateinfo_create"
        ob.insert(s)
        s = "if exists(select * from sys.triggers where name='def_ledger_create') DROP TRIGGER def_ledger_create"
        ob.insert(s)
        s = "if exists(select * from sys.triggers where name='def_storage_create') DROP TRIGGER def_storage_create"
        ob.insert(s)
        s = "if exists(select * from sys.triggers where name='defacgroupcreate') DROP TRIGGER defacgroupcreate"
        ob.insert(s)
        s = "if exists(select * from sys.triggers where name='defgroupcreate') DROP TRIGGER defgroupcreate"
        ob.insert(s)
        s = "if exists(select * from sys.triggers where name='itemdel_on_sale_purchase') DROP TRIGGER itemdel_on_sale_purchase"
        ob.insert(s)
        s = "if exists(select * from sys.triggers where name='itemrate_mst_create') DROP TRIGGER itemrate_mst_create"
        ob.insert(s)
        s = "if exists(select * from sys.triggers where name='itemrate_mst_create2') DROP TRIGGER itemrate_mst_create2"
        ob.insert(s)
        s = "if exists(select * from sys.triggers where name='itemmstupdate') DROP TRIGGER itemmstupdate"
        ob.insert(s)
        s = "if exists(select * from sys.triggers where name='purchasemain_del') DROP TRIGGER purchasemain_del"
        ob.insert(s)
        s = "if exists(select * from sys.triggers where name='acontitng_part_delete') DROP TRIGGER acontitng_part_delete"
        ob.insert(s)
        s = "if exists(select * from sys.triggers where name='salesbilldetail_del') DROP TRIGGER salesbilldetail_del"
        ob.insert(s)
        s = "if exists(select * from sys.triggers where name='store_del') DROP TRIGGER store_del"
        ob.insert(s)
        s = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'receipt_1') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT')) DROP FUNCTION receipt_1"
        ob.insert(s)
        s = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'receipt_2') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT')) DROP FUNCTION receipt_2"
        ob.insert(s)
        s = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'accounts') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT')) DROP FUNCTION accounts"
        ob.insert(s)
        s = "IF  EXISTS (SELECT * FROM sys.views WHERE name='vw_receipt') DROP VIEW vw_receipt"
        ob.insert(s)
        s = "IF  EXISTS (SELECT * FROM sys.views WHERE name='vw_payment') DROP VIEW vw_payment"
        ob.insert(s)
        s = "IF  EXISTS (SELECT * FROM sys.views WHERE name='vw_due_receipt_amount') DROP VIEW vw_due_receipt_amount"
        ob.insert(s)
        s = "IF  EXISTS (SELECT * FROM sys.views WHERE name='vw_due_payment_amount') DROP VIEW vw_due_payment_amount"
        ob.insert(s)
        s = "IF  EXISTS (SELECT * FROM sys.views WHERE name='discount_available_on_receipt') DROP VIEW discount_available_on_receipt"
        ob.insert(s)
        s = "IF EXISTS (SELECT * FROM information_schema.columns WHERE table_name='companyparamtr' and column_name='back_up_restore_path') alter table companyparamtr alter column back_up_restore_path varchar(max)"
        ob.insert(s)
        '---change this command reader when you will remove the trade discount--->
        s = "IF NOT EXISTS (SELECT * FROM information_schema.columns WHERE table_name='companyparamtr' AND column_name = 'Trade_discount_head') alter table companyparamtr add Trade_discount_head varchar(6) null"
        ob.insert(s)
        '---creating the view for bulk liter sale---->
        s = "IF  EXISTS (SELECT * FROM sys.views WHERE  name='vw_ltr_sale') drop view vw_ltr_sale"
        ob.insert(s)
        s = "create  view vw_ltr_sale as select liquor,companycode,yearcode,trndate,groupname,kflname,sum(ltr_sale) as ltr_sale from(select 'SPIRIT' as liquor,salesbilldetail.companycode,salesbilldetail.yearcode,salesbillmain.trndate,groupname,kflname,convert(float,convert(float,ml*qnty)/1000) as Ltr_sale from salesbilldetail join itemmst on itemmst.itemcode=salesbilldetail.itemcode and itemmst.companycode=salesbilldetail.companycode join groupmst on itemmst.groupcode=groupmst.groupcode and itemmst.companycode=groupmst.companycode join kflmst on itemmst.kflcode=kflmst.kflcode and itemmst.companycode=kflmst.companycode join salesbillmain on salesbillmain.companycode=salesbilldetail.companycode and salesbillmain.yearcode=salesbilldetail.yearcode and salesbillmain.trnno=salesbilldetail.trnno where groupname  not like '%BEER' and   groupname  not like 'BEER%' and   groupname  not like '%LAB' and   groupname  not like 'LAB%' and salestype<>'RETURN' union all select 'BEER'as liquor,salesbilldetail.companycode,salesbilldetail.yearcode,salesbillmain.trndate,groupname,kflname,convert(float,convert(float,ml*qnty)/1000) as Ltr_sale from salesbilldetail join itemmst on itemmst.itemcode=salesbilldetail.itemcode and itemmst.companycode=salesbilldetail.companycode join groupmst on itemmst.groupcode=groupmst.groupcode and itemmst.companycode=groupmst.companycode join kflmst on itemmst.kflcode=kflmst.kflcode and itemmst.companycode=kflmst.companycode join salesbillmain on salesbillmain.companycode=salesbilldetail.companycode and salesbillmain.yearcode=salesbilldetail.yearcode and salesbillmain.trnno=salesbilldetail.trnno where groupname like '%BEER' and salestype<>'RETURN' or  groupname  like 'BEER%' and salestype<>'RETURN' or    groupname  like '%LAB' and salestype<>'RETURN' or groupname  like 'LAB%' and salestype<>'RETURN' union all select 'SPIRIT' as liquor,salesbilldetail.companycode,salesbilldetail.yearcode,salesbillmain.trndate,groupname,kflname,-1*convert(float,convert(float,ml*qnty)/1000) as Ltr_sale from salesbilldetail join itemmst on itemmst.itemcode=salesbilldetail.itemcode and itemmst.companycode=salesbilldetail.companycode join groupmst on itemmst.groupcode=groupmst.groupcode and itemmst.companycode=groupmst.companycode join kflmst on itemmst.kflcode=kflmst.kflcode and itemmst.companycode=kflmst.companycode join salesbillmain on salesbillmain.companycode=salesbilldetail.companycode and salesbillmain.yearcode=salesbilldetail.yearcode and salesbillmain.trnno=salesbilldetail.trnno where groupname  not like '%BEER' and   groupname  not like 'BEER%' and   groupname  not like '%LAB' and   groupname  not like 'LAB%' and salestype='RETURN' union all select 'BEER'as liquor,salesbilldetail.companycode,salesbilldetail.yearcode,salesbillmain.trndate,groupname,kflname,-1*convert(float,convert(float,ml*qnty)/1000) as Ltr_sale from salesbilldetail join itemmst on itemmst.itemcode=salesbilldetail.itemcode and itemmst.companycode=salesbilldetail.companycode join groupmst on itemmst.groupcode=groupmst.groupcode and itemmst.companycode=groupmst.companycode join kflmst on itemmst.kflcode=kflmst.kflcode and itemmst.companycode=kflmst.companycode join salesbillmain on salesbillmain.companycode=salesbilldetail.companycode and salesbillmain.yearcode=salesbilldetail.yearcode and salesbillmain.trnno=salesbilldetail.trnno where groupname like '%BEER' and salestype='RETURN' or  groupname  like 'BEER%' and salestype='RETURN' or    groupname  like '%LAB' and salestype='RETURN' or groupname  like 'LAB%' and salestype='RETURN' ) as x group by liquor,companycode,yearcode,trndate,groupname,kflname"
        ob.insert(s)
        '--creating the view vw_payment_detail--->
        s = "IF  EXISTS (SELECT * FROM sys.views WHERE  name='vw_payment_detail') drop view vw_payment_detail"
        ob.insert(s)
        s = "create view vw_payment_detail as select trndate as date,purchasemain.companycode,yearcode,suppliercode as ledgercode,name,convert(varchar(max),trnno) as billno,0 as debit,totnetamt as credit from purchasemain join ledger on ledger.ledcode=purchasemain.suppliercode and ledger.companycode=purchasemain.companycode where ptype='PURCHASE' union all select payment_detail.vchdate as date,payment_detail.companycode,payment_detail.yearcode,head_account as ledgercode,name,convert(varchar(max),trnno) as billno,payment_detail.debit as debit,0 as credit from payment_detail join  payment_main on payment_detail.companycode=payment_main.companycode and payment_detail.yearcode=payment_main.yearcode and payment_detail.vchno=payment_main.vchno join ledger on ledger.ledcode=payment_main.head_account and ledger.companycode=payment_main.companycode union all select vchdate as date,payment_main.companycode,yearcode,head_account as ledgercode,name,'Voucher no.'+convert(varchar(max),vchno) as billno,discount as debit,0 as credit from payment_main join ledger on ledger.ledcode=payment_main.head_account and ledger.companycode=payment_main.companycode where discount<>0"
        ob.insert(s)
        '----crearing the view vw_receipt_detail ---->
        s = "IF  EXISTS (SELECT * FROM sys.views WHERE  name='vw_receipt_detail') drop view vw_receipt_detail"
        ob.insert(s)
        s = "create view vw_receipt_detail as select trndate as date,salesbillmain.companycode,yearcode,ledgercode as ledgercode,convert(varchar(max),trnno) as billno,name,amount as debit,0 as credit from salesbillmain join ledger  on ledger.ledcode=salesbillmain.ledgercode and ledger.companycode=salesbillmain.companycode where  salestype='CREDIT ACCOUNT' union all select receipt_main.vchdate as date,receipt_main.companycode,receipt_main.yearcode,head_account as ledegercode,convert(varchar(max),trnno) as billno,name,0 as debit,receipt_detail.credit as credit from receipt_main join receipt_detail on receipt_main.vchno=receipt_detail.vchno and receipt_main.companycode=receipt_detail.companycode and receipt_main.yearcode=receipt_detail.yearcode join ledger on ledger.companycode=receipt_main.companycode and ledger.ledcode=receipt_main.head_account where receipt_type='CREDIT ACCOUNT' union all select vchdate as date,receipt_main.companycode,yearcode,head_account as ledgercode,'Voucher no.'+convert(varchar(max),vchno) as billno,name,0 as debit,discount as credit from receipt_main join ledger on ledger.ledcode=receipt_main.head_account and ledger.companycode=receipt_main.companycode where receipt_type='CREDIT ACCOUNT' and discount<>0"
        ob.insert(s)
        '--creating the table salestax---->
        s = "IF NOT EXISTS (SELECT * FROM information_schema.tables WHERE table_name='sales_tax') create table sales_tax(trnno int,companycode int,yearcode int,cash_bank_book varchar(6),tax_book varchar(6),tax_amount float)"
        ob.insert(s)
        '---creating the view vw_receipt_Accounts---->
        s = "IF  EXISTS (SELECT * FROM sys.views WHERE  name='vw_receipt_accounts') drop view vw_receipt_accounts"
        ob.insert(s)
        's = "create view vw_receipt_accounts as select vchdate as date,receipt_main.companycode,receipt_main.yearcode,head_account as ledgercode,'Receipt' as Vchtype,vchno as billno,name,0 as debit,amount_paid+isnull(tax_amount,0) as credit from receipt_main join ledger on ledger.ledcode=receipt_main.head_account left join sales_tax on sales_tax.companycode=receipt_main.companycode and sales_tax.yearcode=receipt_main.yearcode and sales_tax.trnno=receipt_main.vchno union all select vchdate as date,receipt_main.companycode,receipt_main.yearcode,client_account as ledgercode,'Receipt' as Vchtype,vchno as billno,name,amount_paid+isnull(tax_amount,0) as debit,0 as credit from receipt_main join sales_tax on sales_tax.companycode=receipt_main.companycode and sales_tax.yearcode=receipt_main.yearcode and sales_tax.trnno=receipt_main.vchno join ledger on ledger.ledcode=receipt_main.client_Account union all select vchdate as date,receipt_main.companycode,yearcode,head_account as ledgercode,'Receipt' as Vchtype,vchno as billno,name,0 as debit,discount as credit from receipt_main join ledger on ledger.ledcode=receipt_main.head_account and ledger.companycode=receipt_main.companycode where discount<>0 union all select vchdate as date,receipt_main.companycode,yearcode,discount_account_head as ledgercode,'Receipt' as Vchtype,vchno as billno,name,discount as debit,0 as credit from receipt_main join ledger on ledger.ledcode=receipt_main.discount_account_head and ledger.companycode=receipt_main.companycode where discount<>0 union all select vchdate as date,receipt_main.companycode,receipt_main.yearcode,cash_bank_book as ledgercode,'Sale' as Vchtype,receipt_main.vchno as billno,name,0 as debit,tax_amount as credit from sales_tax join receipt_main on receipt_main.companycode=sales_tax.companycode and receipt_main.yearcode=sales_tax.yearcode and receipt_main.vchno=sales_tax.trnno join ledger on ledger.companycode=sales_tax.companycode and ledger.ledcode=sales_tax.cash_bank_book union all select vchdate as date,receipt_main.companycode,receipt_main.yearcode,tax_book as ledgercode,'Sale' as Vchtype,receipt_main.vchno as billno,name,tax_amount as debit,0 as credit from sales_tax join receipt_main on receipt_main.companycode=sales_tax.companycode and receipt_main.yearcode=sales_tax.yearcode and receipt_main.vchno=sales_tax.trnno join ledger on ledger.companycode=sales_tax.companycode and ledger.ledcode=sales_tax.tax_book union all select trndate as date,salesbillmain.companycode,yearcode,account_head as ledgercode,'Sale' as Vchtype,trnno as billno,name,0 as debit,netamount as credit from salesbillmain join ledger on ledger.ledcode=salesbillmain.account_head and ledger.companycode=salesbillmain.companycode where salestype='CREDIT ACCOUNT' union all select trndate as date,salesbillmain.companycode,yearcode,ledgercode as ledgercode,'Sale' as Vchtype,trnno as billno,name,netamount as debit,0 as credit from salesbillmain join ledger  on ledger.ledcode=salesbillmain.ledgercode and ledger.companycode=salesbillmain.companycode where  salestype='CREDIT ACCOUNT' union all select trndate as date,salesbillmain.companycode,yearcode,account_head as ledgercode,'Sale' as Vchtype,trnno as billno,name,0 as debit,discamount as credit from salesbillmain join ledger on ledger.ledcode=salesbillmain.account_head and ledger.companycode=salesbillmain.companycode where salestype='CREDIT ACCOUNT' and discamount<>0 union all select trndate as date,salesbillmain.companycode,yearcode,discount_account_head as ledgercode,'Sale' as Vchtype,trnno as billno,name,discamount as debit,0 as credit from salesbillmain join ledger on ledger.ledcode=salesbillmain.discount_account_head and ledger.companycode=salesbillmain.companycode where salestype='CREDIT ACCOUNT' and discamount<>0 union all select trndate as date,salesbillmain.companycode,yearcode,account_head as ledgercode,'Return' as Vchtype,trnno as billno,name,netamount as debit,0 as credit from salesbillmain join ledger on ledger.ledcode=salesbillmain.account_head and ledger.companycode=salesbillmain.companycode where salestype='RETURN' union all select trndate as date,salesbillmain.companycode,yearcode,ledgercode as ledgercode,'Return' as Vchtype,trnno as billno,name,0 as debit,netamount as credit from salesbillmain join ledger on ledger.ledcode=salesbillmain.ledgercode and ledger.companycode=salesbillmain.companycode where salestype='RETURN' union all select trndate as date,salesbillmain.companycode,yearcode,discount_account_head as ledgercode,'Return' as Vchtype,trnno as billno,name,0 as debit,discamount as credit from salesbillmain join ledger on ledger.ledcode=salesbillmain.discount_account_head and ledger.companycode=salesbillmain.companycode where salestype='RETURN' and discamount<>0 union all select trndate as date,salesbillmain.companycode,yearcode,account_head as ledgercode,'Return' as Vchtype,trnno as billno,name,discamount as debit,0 as credit from salesbillmain join ledger on ledger.ledcode=salesbillmain.account_head and ledger.companycode=salesbillmain.companycode where salestype='RETURN' and discamount<>0 union all select trndate as date,purchasemain.companycode,yearcode,purchaseacccode as ledgercode,'Purchase' as Vchtype,trnno as billno,name,totnetamt as debit,0 as credit from purchasemain join ledger on ledger.ledcode=purchasemain.purchaseacccode and ledger.companycode=purchasemain.companycode where ptype='PURCHASE' union all select trndate as date,purchasemain.companycode,yearcode,suppliercode as ledgercode,'Purchase' as Vchtype,trnno as billno,name,0 as debit,totnetamt as credit from purchasemain join ledger on ledger.ledcode=purchasemain.suppliercode and ledger.companycode=purchasemain.companycode where ptype='PURCHASE' union all select docdate as date,purchasetaxdetail.companycode,purchasemain.yearcode,taxdetail.ledcode as ledgercode,'purchase' as vchtype,purchasemain.trnno as billno,name,0 as debit,purchasetaxdetail.taxamount as credit from purchasetaxdetail join taxdetail on taxdetail.companycode=purchasetaxdetail.companycode and taxdetail.schemecode=purchasetaxdetail.schemecode and taxdetail.taxcode=purchasetaxdetail.taxcode join purchasemain on purchasemain.companycode=purchasetaxdetail.companycode and purchasemain.yearcode=purchasetaxdetail.yearcode and purchasemain.trnno=purchasetaxdetail.trnno join ledger on ledger.companycode=taxdetail.companycode and ledger.ledcode=taxdetail.ledcode where sig='-' union all select docdate as date,purchasetaxdetail.companycode,purchasemain.yearcode,purchaseacccode as ledgercode,'purchase' as vchtype,purchasemain.trnno as billno,name,purchasetaxdetail.taxamount as debit,0 as credit from purchasetaxdetail join taxdetail on taxdetail.companycode=purchasetaxdetail.companycode and taxdetail.schemecode=purchasetaxdetail.schemecode and taxdetail.taxcode=purchasetaxdetail.taxcode join purchasemain on purchasemain.companycode=purchasetaxdetail.companycode and purchasemain.yearcode=purchasetaxdetail.yearcode and purchasemain.trnno=purchasetaxdetail.trnno join ledger on ledger.companycode=purchasemain.companycode and ledger.ledcode=purchasemain.purchaseacccode where sig='-' union all select docdate as date,purchasetaxdetail.companycode,purchasemain.yearcode,taxdetail.ledcode as ledgercode,'purchase' as vchtype,purchasemain.trnno as billno,name,purchasetaxdetail.taxamount as debit,0 as credit from purchasetaxdetail join taxdetail on taxdetail.companycode=purchasetaxdetail.companycode and taxdetail.schemecode=purchasetaxdetail.schemecode and taxdetail.taxcode=purchasetaxdetail.taxcode join purchasemain  on purchasemain.companycode=purchasetaxdetail.companycode and purchasemain.yearcode=purchasetaxdetail.yearcode and purchasemain.trnno=purchasetaxdetail.trnno join ledger on ledger.companycode=taxdetail.companycode and ledger.ledcode=taxdetail.ledcode where sig='+' union all select docdate as date,purchasetaxdetail.companycode,purchasemain.yearcode,purchaseacccode as ledgercode,'purchase' as vchtype,purchasemain.trnno as billno,name,0 as debit,purchasetaxdetail.taxamount as credit from purchasetaxdetail join taxdetail on taxdetail.companycode=purchasetaxdetail.companycode and taxdetail.schemecode=purchasetaxdetail.schemecode and taxdetail.taxcode=purchasetaxdetail.taxcode join purchasemain on purchasemain.companycode=purchasetaxdetail.companycode and purchasemain.yearcode=purchasetaxdetail.yearcode and purchasemain.trnno=purchasetaxdetail.trnno join ledger on ledger.companycode=purchasemain.companycode and ledger.ledcode=purchasemain.purchaseacccode where sig='+' union all select vchdate as date,payment_main.companycode,yearcode,head_account as ledgercode,'Payment' as Vchtype,vchno as billno,name,amount_paid as debit,0 as credit from payment_main join ledger on ledger.ledcode=payment_main.head_account and ledger.companycode=payment_main.companycode union all select vchdate as date,payment_main.companycode,yearcode,client_account as ledgercode,'Payment' as Vchtype,vchno as billno,name,0 as debit,amount_paid as credit from payment_main join ledger on ledger.ledcode=payment_main.client_account and ledger.companycode=payment_main.companycode union all select vchdate as date,payment_main.companycode,yearcode,discount_account_head as ledgercode,'Payment' as Vchtype,vchno as billno,name,0 as debit,discount as credit from payment_main join ledger on ledger.ledcode=payment_main.discount_account_head and ledger.companycode=payment_main.companycode where discount<>0 union all select vchdate as date,payment_main.companycode,yearcode,head_account as ledgercode,'Payment' as Vchtype,vchno as billno,name,discount as debit,0 as credit from payment_main join ledger on ledger.ledcode=payment_main.head_account and ledger.companycode=payment_main.companycode where discount<>0"
        s = "create view vw_receipt_accounts as select vchdate as date,receipt_main.companycode,receipt_main.yearcode,head_account as ledgercode,'Receipt' as Vchtype,vchno as billno,name,0 as debit,amount_paid+isnull(tax_amount,0) as credit from receipt_main join ledger on ledger.ledcode=receipt_main.head_account left join sales_tax on sales_tax.companycode=receipt_main.companycode and sales_tax.yearcode=receipt_main.yearcode and sales_tax.trnno=receipt_main.vchno union all select vchdate as date,receipt_main.companycode,receipt_main.yearcode,client_account as ledgercode,'Receipt' as Vchtype,vchno as billno,name,amount_paid+isnull(tax_amount,0) as debit,0 as credit from receipt_main join ledger on ledger.ledcode=receipt_main.client_Account left join sales_tax on sales_tax.companycode=receipt_main.companycode and sales_tax.yearcode=receipt_main.yearcode and sales_tax.trnno=receipt_main.vchno union all select vchdate as date,receipt_main.companycode,yearcode,head_account as ledgercode,'Receipt' as Vchtype,vchno as billno,name,0 as debit,discount as credit from receipt_main join ledger on ledger.ledcode=receipt_main.head_account and ledger.companycode=receipt_main.companycode where discount<>0 union all select vchdate as date,receipt_main.companycode,yearcode,discount_account_head as ledgercode,'Receipt' as Vchtype,vchno as billno,name,discount as debit,0 as credit from receipt_main join ledger on ledger.ledcode=receipt_main.discount_account_head and ledger.companycode=receipt_main.companycode where discount<>0  union all select vchdate as date,receipt_main.companycode,receipt_main.yearcode,cash_bank_book as ledgercode,'Sale' as Vchtype,receipt_main.vchno as billno,name,0 as debit,tax_amount as credit from sales_tax join receipt_main on receipt_main.companycode=sales_tax.companycode and receipt_main.yearcode=sales_tax.yearcode and receipt_main.vchno=sales_tax.trnno join ledger on ledger.companycode=sales_tax.companycode and ledger.ledcode=sales_tax.cash_bank_book union all select vchdate as date,receipt_main.companycode,receipt_main.yearcode,tax_book as ledgercode,'Sale' as Vchtype,receipt_main.vchno as billno,name,tax_amount as debit,0 as credit from sales_tax join receipt_main on receipt_main.companycode=sales_tax.companycode and receipt_main.yearcode=sales_tax.yearcode and receipt_main.vchno=sales_tax.trnno join ledger on ledger.companycode=sales_tax.companycode and ledger.ledcode=sales_tax.tax_book union all select trndate as date,salesbillmain.companycode,yearcode,account_head as ledgercode,'Sale' as Vchtype,trnno as billno,name,0 as debit,netamount as credit  from salesbillmain  join ledger on ledger.ledcode=salesbillmain.account_head and ledger.companycode=salesbillmain.companycode where salestype='CREDIT ACCOUNT' union all select trndate as date,salesbillmain.companycode,yearcode,ledgercode as ledgercode,'Sale' as Vchtype,trnno as billno,name,netamount as debit,0 as credit  from salesbillmain  join ledger  on ledger.ledcode=salesbillmain.ledgercode and ledger.companycode=salesbillmain.companycode where  salestype='CREDIT ACCOUNT' union all select trndate as date,salesbillmain.companycode,yearcode,account_head as ledgercode,'Sale' as Vchtype,trnno as billno,name,0 as debit,discamount as credit  from salesbillmain  join ledger  on ledger.ledcode=salesbillmain.account_head  and ledger.companycode=salesbillmain.companycode where salestype='CREDIT ACCOUNT' and discamount<>0 union all select trndate as date,salesbillmain.companycode,yearcode,discount_account_head as ledgercode,'Sale' as Vchtype,trnno as billno,name,discamount as debit,0 as credit  from salesbillmain  join ledger on ledger.ledcode=salesbillmain.discount_account_head and ledger.companycode=salesbillmain.companycode where salestype='CREDIT ACCOUNT' and discamount<>0 union all select trndate as date,salesbillmain.companycode,yearcode,account_head as ledgercode,'Return' as Vchtype,trnno as billno,name,netamount as debit,0 as credit  from salesbillmain  join ledger  on ledger.ledcode=salesbillmain.account_head  and ledger.companycode=salesbillmain.companycode where salestype='RETURN' union all select trndate as date,salesbillmain.companycode,yearcode,ledgercode as ledgercode,'Return' as Vchtype,trnno as billno,name,0 as debit,netamount as credit  from salesbillmain  join ledger on  ledger.ledcode=salesbillmain.ledgercode  and ledger.companycode=salesbillmain.companycode where salestype='RETURN' union all select trndate as date,salesbillmain.companycode,yearcode,discount_account_head as ledgercode,'Return' as Vchtype,trnno as billno,name,0 as debit,discamount as credit  from salesbillmain  join ledger  on ledger.ledcode=salesbillmain.discount_account_head  and ledger.companycode=salesbillmain.companycode where salestype='RETURN' and discamount<>0 union all select trndate as date,salesbillmain.companycode,yearcode,account_head as ledgercode,'Return' as Vchtype,trnno as billno,name,discamount as debit,0 as credit  from salesbillmain  join ledger  on ledger.ledcode=salesbillmain.account_head  and ledger.companycode=salesbillmain.companycode where salestype='RETURN' and discamount<>0 union all  select trndate as date,purchasemain.companycode,yearcode,purchaseacccode as ledgercode,'Purchase' as Vchtype,trnno as billno,name,totnetamt as debit,0 as credit from purchasemain join ledger  on ledger.ledcode=purchasemain.purchaseacccode and ledger.companycode=purchasemain.companycode where ptype='PURCHASE' union all select trndate as date,purchasemain.companycode,yearcode,suppliercode as ledgercode,'Purchase' as Vchtype,trnno as billno,name,0 as debit,totnetamt as credit from purchasemain join ledger  on ledger.ledcode=purchasemain.suppliercode and ledger.companycode=purchasemain.companycode where ptype='PURCHASE' union all select docdate as date,purchasetaxdetail.companycode,purchasemain.yearcode,taxdetail.ledcode as ledgercode,'purchase' as vchtype,purchasemain.trnno as billno,name,0 as debit,purchasetaxdetail.taxamount as credit from purchasetaxdetail join taxdetail  on taxdetail.companycode=purchasetaxdetail.companycode and taxdetail.schemecode=purchasetaxdetail.schemecode and taxdetail.taxcode=purchasetaxdetail.taxcode join purchasemain  on purchasemain.companycode=purchasetaxdetail.companycode and purchasemain.yearcode=purchasetaxdetail.yearcode and purchasemain.trnno=purchasetaxdetail.trnno join ledger  on ledger.companycode=taxdetail.companycode and ledger.ledcode=taxdetail.ledcode where sig='-' union all select docdate as date,purchasetaxdetail.companycode,purchasemain.yearcode,purchaseacccode as ledgercode,'purchase' as vchtype,purchasemain.trnno as billno,name,purchasetaxdetail.taxamount as debit,0 as credit from purchasetaxdetail join taxdetail  on taxdetail.companycode=purchasetaxdetail.companycode and taxdetail.schemecode=purchasetaxdetail.schemecode and taxdetail.taxcode=purchasetaxdetail.taxcode join purchasemain  on purchasemain.companycode=purchasetaxdetail.companycode and purchasemain.yearcode=purchasetaxdetail.yearcode and purchasemain.trnno=purchasetaxdetail.trnno join ledger  on ledger.companycode=purchasemain.companycode and ledger.ledcode=purchasemain.purchaseacccode where sig='-' union all select docdate as date,purchasetaxdetail.companycode,purchasemain.yearcode,taxdetail.ledcode as ledgercode,'purchase' as vchtype,purchasemain.trnno as billno, name, purchasetaxdetail.taxamount as debit,0 as credit from purchasetaxdetail join taxdetail  on taxdetail.companycode=purchasetaxdetail.companycode and taxdetail.schemecode=purchasetaxdetail.schemecode and taxdetail.taxcode=purchasetaxdetail.taxcode join purchasemain  on purchasemain.companycode=purchasetaxdetail.companycode and purchasemain.yearcode=purchasetaxdetail.yearcode and purchasemain.trnno=purchasetaxdetail.trnno join ledger  on ledger.companycode=taxdetail.companycode and ledger.ledcode=taxdetail.ledcode where sig='+' union all select docdate as date,purchasetaxdetail.companycode,purchasemain.yearcode,purchaseacccode as ledgercode,'purchase' as vchtype,purchasemain.trnno as billno,name,0 as debit,purchasetaxdetail.taxamount as credit from purchasetaxdetail join taxdetail  on taxdetail.companycode=purchasetaxdetail.companycode and taxdetail.schemecode=purchasetaxdetail.schemecode and taxdetail.taxcode=purchasetaxdetail.taxcode join purchasemain  on purchasemain.companycode=purchasetaxdetail.companycode and purchasemain.yearcode=purchasetaxdetail.yearcode and purchasemain.trnno=purchasetaxdetail.trnno join ledger  on ledger.companycode=purchasemain.companycode and ledger.ledcode=purchasemain.purchaseacccode where sig='+' union all select vchdate as date,payment_main.companycode,yearcode,head_account as ledgercode,'Payment' as Vchtype,vchno as billno,name,amount_paid as debit,0 as credit from payment_main join ledger on ledger.ledcode=payment_main.head_account and ledger.companycode=payment_main.companycode union all select vchdate as date,payment_main.companycode,yearcode,client_account as ledgercode,'Payment' as Vchtype,vchno as billno,name,0 as debit,amount_paid as credit from payment_main join ledger on ledger.ledcode=payment_main.client_account and ledger.companycode=payment_main.companycode union all select vchdate as date,payment_main.companycode,yearcode,discount_account_head as ledgercode,'Payment' as Vchtype,vchno as billno,name,0 as debit,discount as credit from payment_main join ledger on ledger.ledcode=payment_main.discount_account_head and ledger.companycode=payment_main.companycode where discount<>0 union all select vchdate as date,payment_main.companycode,yearcode,head_account as ledgercode,'Payment' as Vchtype,vchno as billno,name,discount as debit,0 as credit from payment_main join ledger on ledger.ledcode=payment_main.head_account and ledger.companycode=payment_main.companycode where discount<>0"
        ob.insert(s)
        '-----------adding the tppassno and billno column to the breakage main-->
        s = "IF NOT  EXISTS (SELECT * FROM information_schema.columns WHERE table_name='breakagemain' AND column_name = 'billno') alter table breakagemain add billno varchar(max) null"
        ob.insert(s)
        s = "IF NOT  EXISTS (SELECT * FROM information_schema.columns WHERE table_name='breakagemain' AND column_name = 'TP_pass_no') alter table breakagemain add TP_pass_no varchar(max) null"
        ob.insert(s)
        '---adding the surcharge percentage column to the companyparamtr-->
        s = "IF NOT EXISTS (SELECT * FROM information_schema.columns WHERE table_name='companyparamtr' AND column_name = 'surcharge_percent')alter table companyparamtr add surcharge_percent float null"
        ob.insert(s)
        '---creating the view for salesbillmain--->
        s = "IF  EXISTS (SELECT * FROM sys.views WHERE  name='vw_salesbill_main') drop view vw_salesbill_main"
        ob.insert(s)
        s = "create view vw_salesbill_main as select convert(varchar(10),trndate,103) as trndate,name,salesbillmain.trnno,amount,discamount,isnull(tax_amount,0) as 'surcharge_amount',amount-isnull(discamount,0)+isnull(tax_amount,0) as netamount,salestype,ratename,shopname,salesbillmain.ledgercode,salesbillmain.storecode,salesbillmain.ratecode,salesbillmain.yearcode,salesbillmain.companycode,trndate as date from salesbillmain join ledger on salesbillmain.ledgercode=ledger.ledcode and salesbillmain.companycode=ledger.companycode join itemrateinfo on salesbillmain.ratecode=itemrateinfo.ratecode and salesbillmain.companycode=itemrateinfo.companycode join storage on storage.shopcode=salesbillmain.storecode and salesbillmain.companycode=storage.companycode left join sales_tax on sales_tax.companycode=salesbillmain.companycode and sales_tax.yearcode=salesbillmain.yearcode and sales_tax.trnno=salesbillmain.trnno"
        ob.insert(s)
        '---creating the view vw_debtors--->
        s = "IF  EXISTS (SELECT * FROM sys.views WHERE  name='vw_detors') drop view vw_detors"
        ob.insert(s)
        s = "create view vw_detors as select ledcode, name,ledger.companycode from ledger join acountname on acountname.accode=ledger.accode where acountname.acname like '%DEBTORS'  or acountname.acname like 'DEBTORS%'"
        ob.insert(s)
        '--creating the view vw_creditors--->
        s = "IF  EXISTS (SELECT * FROM sys.views WHERE  name='vw_creditors') drop view vw_creditors"
        ob.insert(s)
        s = "create view vw_creditors as select ledcode, name,ledger.companycode from ledger join acountname on acountname.accode=ledger.accode where acountname.acname like '%CREDITORS'  or acountname.acname like 'CREDITORS%'"
        ob.insert(s)
        '--creating view vw_sale---->
        s = "IF  EXISTS (SELECT * FROM sys.views WHERE  name='vw_sale') drop view vw_sale"
        ob.insert(s)
        s = "CREATE view vw_sale as select salestype,sum(bottles_sold) as bottles_sold,sum(total_amount)as total_amount,companycode,yearcode,trndate from(select salestype,qnty as BOTTLES_SOLD,itemamount as TOTAL_AMOUNT,salesbillmain.companycode,salesbillmain.yearcode,salesbillmain.trndate from salesbillmain join salesbilldetail  on salesbillmain.companycode=salesbilldetail.companycode  and salesbillmain.trnno=salesbilldetail.trnno and salesbillmain.yearcode=salesbilldetail.yearcode join itemmst on itemmst.itemcode=salesbilldetail.itemcode and salesbilldetail.companycode=itemmst.companycode union all select 'DISCOUNT' as saletype,'0' as bottles_sold,discamount as total_amount,companycode,yearcode,trndate from salesbillmain where salestype<>'RETURN' and discamount<>0 union all select 'DISCOUNT' as saletype,'0' as bottles_sold,(-1)*discamount as total_amount,companycode,yearcode,trndate from salesbillmain where salestype='RETURN' and discamount<>0 union all select 'EXPENCE' as saletype,'0' as BOTTLES_SOLD,debit as total_amount,payment_main.companycode as companycode,payment_main.yearcode as yearcode,payment_main.vchdate as trndate from payment_main join payment_detail on payment_main.companycode=payment_detail.companycode and payment_main.yearcode=payment_detail.yearcode and payment_main.vchno=payment_detail.vchno join companyparamtr on companyparamtr.companycode=payment_main.companycode and companyparamtr.expenceacc=payment_main.head_account )as x group by salestype,companycode,yearcode,trndate"
        ob.insert(s)
        '---creating all the views and the tables required for the tally data pull-->
        s = "if exists(select * from sys.views where name='vw_tally_data') drop view vw_tally_data"
        ob.insert(s)
        s = "create view vw_tally_data as select saletype,x.companycode,x.yearcode,x.trnno,date,head,client,itemname,itemqty,itemrate,itemamount,tottaxamt,totnetamt from(select salestype as saletype,salesbillmain.companycode,salesbillmain.yearcode,salesbillmain.trnno,salesbillmain.trndate as date,name as head,itemname,qnty as itemqty,rate as itemrate,itemamount,discamount as tottaxamt,itemamount-discamount as totnetamt from salesbillmain join ledger on ledger.ledcode=salesbillmain.account_head and ledger.companycode=salesbillmain.companycode join salesbilldetail on salesbilldetail.companycode=salesbillmain.companycode and salesbilldetail.yearcode=salesbillmain.yearcode and salesbilldetail.trnno=salesbillmain.trnno join itemmst on itemmst.companycode=salesbilldetail.companycode and itemmst.itemcode=salesbilldetail.itemcode ) as x join( select salesbillmain.companycode,salesbillmain.yearcode,salesbillmain.trnno,name as client from salesbillmain join ledger on ledger.ledcode=salesbillmain.ledgercode and ledger.companycode=salesbillmain.companycode join salesbilldetail on salesbilldetail.companycode=salesbillmain.companycode and salesbilldetail.yearcode=salesbillmain.yearcode and salesbilldetail.trnno=salesbillmain.trnno join itemmst on itemmst.companycode=salesbilldetail.companycode and itemmst.itemcode=salesbilldetail.itemcode group by salesbillmain.companycode,salesbillmain.yearcode,salesbillmain.trnno,name) as y on  x.companycode = y.companycode and x.yearcode = y.yearcode and x.trnno = y.trnno"
        ob.insert(s)
        '---creating the function for the opening_closing_stock--->
        s = "IF EXISTS(SELECT name FROM sys.objects WHERE name = N'opening_closing_stock') DROP FUNCTION opening_closing_stock"
        ob.insert(s)
        s = "create function opening_closing_stock(@day1 datetime,@day2 datetime,@day3 datetime) returns table as return select itemcode,itemname,sum(opening_stock) as opening_stock,sum(bottles_purchased) as bottles_purchased,sum(bottles_sold) as bottles_sold, sum(stock)+sum(opening_stock)+ sum(bottles_purchased)-sum(bottles_sold)as stock,companycode,storecode,yearcode from(select itemmst.itemcode,itemname,sum(qnty) as opening_stock,0 as bottles_purchased,0 as bottles_sold,0 as stock,itemmst.companycode,shopcode as storecode,yearcode from openingstockmst join itemmst on openingstockmst.itemcode=itemmst.itemcode and openingstockmst.companycode=itemmst.companycode where openingstockmst.trndate=@day1 group by itemmst.itemcode,itemmst.itemname,itemmst.companycode,shopcode,openingstockmst.yearcode union all select itemmst.itemcode,itemmst.itemname,(-1)*sum(qnty) as opening_stock,0 as bottles_purchased,0 as bottles_sold,0 as stock,salesbillmain.companycode,storecode,salesbillmain.yearcode from salesbillmain join salesbilldetail on salesbillmain.companycode=salesbilldetail.companycode and salesbillmain.yearcode=salesbilldetail.yearcode and salesbillmain.trnno=salesbilldetail.trnno join itemmst on itemmst.itemcode=salesbilldetail.itemcode and salesbilldetail.companycode=itemmst.companycode where salestype<>'RETURN' and salesbillmain.trndate >=@day1 and salesbillmain.trndate <@day2 group by itemmst.itemcode,itemmst.itemname,salesbillmain.companycode,storecode,salesbillmain.yearcode union all select itemmst.itemcode,itemmst.itemname,sum(qnty) as opening_stock,0 as bottles_purchased,0 as bottles_sold,0 as stock,salesbillmain.companycode,storecode,salesbillmain.yearcode from salesbillmain join salesbilldetail on salesbillmain.companycode=salesbilldetail.companycode and salesbillmain.yearcode=salesbilldetail.yearcode and salesbillmain.trnno=salesbilldetail.trnno join itemmst on itemmst.itemcode=salesbilldetail.itemcode and salesbilldetail.companycode=itemmst.companycode where salestype='RETURN' and salesbillmain.trndate >=@day1 and salesbillmain.trndate <@day2 group by itemmst.itemcode,itemmst.itemname,salesbillmain.companycode,storecode,salesbillmain.yearcode union all select itemmst.itemcode,itemmst.itemname,sum(itemquantity) as opening_stock,0 as bottles_purchased,0 as bottles_sold,0 as stock,purchasemain.companycode,shopcode as storecode,purchasemain.yearcode from purchasemain join purchasedetail on purchasedetail.companycode=purchasemain.companycode and purchasedetail.trnno=purchasemain.trnno and purchasedetail.yearcode=purchasemain.yearcode join itemmst on itemmst.itemcode=purchasedetail.itemcode and itemmst.companycode=purchasedetail.companycode where ptype='PURCHASE' and purchasemain.trndate >=@day1 and purchasemain.trndate <@day2 group by itemmst.itemcode,itemmst.itemname,purchasemain.companycode,shopcode,purchasemain.yearcode union all select itemmst.itemcode,itemmst.itemname,-1*sum(itemquantity) as opening_stock,0 as bottles_purchased,0 as bottles_sold,0 as stock,purchasemain.companycode,shopcode as storecode,purchasemain.yearcode from purchasemain join purchasedetail on purchasedetail.companycode=purchasemain.companycode and purchasedetail.trnno=purchasemain.trnno and purchasedetail.yearcode=purchasemain.yearcode join itemmst on itemmst.itemcode=purchasedetail.itemcode and itemmst.companycode=purchasedetail.companycode where ptype='PURCHASE RETURN' and purchasemain.trndate >=@day1 and purchasemain.trndate <@day2 group by itemmst.itemcode,itemmst.itemname,purchasemain.companycode,shopcode,purchasemain.yearcode union all select itemmst.itemcode,itemname,(-1)*sum(qnty) as opening_stock,0 as bottles_purchased,0 as bottles_sold,0 as stock,stk_transfer_detail.companycode,shopcode_frm as storecode,stk_transfer_detail.yearcode from stk_transfer_detail join itemmst on itemmst.itemcode=stk_transfer_detail.itemcode and stk_transfer_detail.companycode=itemmst.companycode join stk_transfer_main on stk_transfer_main.companycode=stk_transfer_detail.companycode and stk_transfer_main.yearcode=stk_transfer_detail.yearcode and stk_transfer_main.trnno=stk_transfer_detail.trnno where stk_transfer_main.trndate>=@day1 and stk_transfer_main.trndate<@day2 group by itemmst.itemcode,itemmst.itemname,stk_transfer_detail.companycode,shopcode_frm,stk_transfer_detail.yearcode union all select itemmst.itemcode,itemname,sum(qnty) as opening_stock,0 as bottles_purchased,0 as bottles_sold,0 as stock,stk_transfer_detail.companycode,shopcode_to as storecode,stk_transfer_detail.yearcode from stk_transfer_detail join itemmst on itemmst.itemcode=stk_transfer_detail.itemcode and stk_transfer_detail.companycode=itemmst.companycode join stk_transfer_main on stk_transfer_main.companycode=stk_transfer_detail.companycode and stk_transfer_main.yearcode=stk_transfer_detail.yearcode and stk_transfer_main.trnno=stk_transfer_detail.trnno where stk_transfer_main.trndate>=@day1 and stk_transfer_main.trndate<@day2 group by itemmst.itemcode,itemmst.itemname,stk_transfer_detail.companycode,shopcode_to,stk_transfer_detail.yearcode union all select breakagedetail.itemcode,itemname,-1*sum(quantity) as opening_stock,0 as bottles_purchased,0 as bottles_sold,0 as stock,breakagedetail.companycode,shopcode,breakagedetail.yearcode from breakagedetail join breakagemain on breakagedetail.companycode=breakagemain.companycode and breakagedetail.yearcode=breakagemain.yearcode and breakagedetail.trnno=breakagemain.trnno join itemmst on itemmst.itemcode=breakagedetail.itemcode and itemmst.companycode=breakagedetail.companycode where receivd='False' and breakagemain.trndate>=@day1 and breakagemain.trndate<@day2 group by breakagedetail.itemcode,itemname,breakagedetail.companycode,shopcode,breakagedetail.yearcode union all select itemmst.itemcode,itemmst.itemname,0 as opening_stock,0 as bottles_purchased,sum(qnty) as bottles_sold,0 as stock,salesbillmain.companycode,storecode,salesbillmain.yearcode from salesbillmain join salesbilldetail on salesbillmain.companycode=salesbilldetail.companycode and salesbillmain.yearcode=salesbilldetail.yearcode and salesbillmain.trnno=salesbilldetail.trnno join itemmst on itemmst.itemcode=salesbilldetail.itemcode and salesbilldetail.companycode=itemmst.companycode where salestype='cash' and salesbillmain.trndate >=@day2 and salesbillmain.trndate <=@day3 or salestype='credit card' and salesbillmain.trndate >=@day2 and salesbillmain.trndate <=@day3 or  salestype ='credit account' and salesbillmain.trndate >=@day2 and salesbillmain.trndate <=@day3 group by itemmst.itemcode,itemmst.itemname,salesbillmain.companycode,storecode,salesbillmain.yearcode union all select itemmst.itemcode,itemmst.itemname,0 as opening_stock,0 as bottles_purchased,(-1)*sum(qnty) as bottles_sold,0 as stock,salesbillmain.companycode,storecode,salesbillmain.yearcode from salesbillmain join salesbilldetail on salesbillmain.companycode=salesbilldetail.companycode and salesbillmain.yearcode=salesbilldetail.yearcode and salesbillmain.trnno=salesbilldetail.trnno join itemmst on itemmst.itemcode=salesbilldetail.itemcode and salesbilldetail.companycode=itemmst.companycode where salestype='return' and salesbillmain.trndate >=@day2 and salesbillmain.trndate <=@day3 group by itemmst.itemcode,itemmst.itemname,salesbillmain.companycode,storecode,salesbillmain.yearcode union all select itemmst.itemcode,itemmst.itemname,0 as opening_stock,sum(itemquantity) as bottles_purchased,0 as bottles_sold,0 as stock,purchasemain.companycode,shopcode as storecode,purchasemain.yearcode from purchasemain join purchasedetail on purchasedetail.companycode=purchasemain.companycode and purchasedetail.trnno=purchasemain.trnno and purchasedetail.yearcode=purchasemain.yearcode join itemmst on itemmst.itemcode=purchasedetail.itemcode and itemmst.companycode=purchasedetail.companycode where ptype='PURCHASE' and purchasemain.trndate >=@day2 and purchasemain.trndate <=@day3 group by itemmst.itemcode,itemmst.itemname,purchasemain.companycode,shopcode,purchasemain.yearcode union all select itemmst.itemcode,itemmst.itemname,0 as opening_stock,(-1)*sum(itemquantity) as bottles_purchased,0 as bottles_sold,0 as stock,purchasemain.companycode,shopcode as storecode,purchasemain.yearcode from purchasemain join purchasedetail on purchasedetail.companycode=purchasemain.companycode and purchasedetail.trnno=purchasemain.trnno and purchasedetail.yearcode=purchasemain.yearcode join itemmst on itemmst.itemcode=purchasedetail.itemcode and itemmst.companycode=purchasedetail.companycode where ptype='PURCHASE RETURN' and purchasemain.trndate >=@day2 and purchasemain.trndate <=@day3 group by itemmst.itemcode,itemmst.itemname,purchasemain.companycode,shopcode,purchasemain.yearcode union all select itemmst.itemcode,itemname,0 as opening_stock,0 as bottles_purchased,0 as bottles_sold,(-1)*sum(qnty) as stock,stk_transfer_detail.companycode,shopcode_frm as storecode,stk_transfer_detail.yearcode from stk_transfer_detail join itemmst on itemmst.itemcode=stk_transfer_detail.itemcode and stk_transfer_detail.companycode=itemmst.companycode join stk_transfer_main on stk_transfer_main.companycode=stk_transfer_detail.companycode and stk_transfer_main.yearcode=stk_transfer_detail.yearcode and stk_transfer_main.trnno=stk_transfer_detail.trnno where stk_transfer_main.trndate>=@day2 and stk_transfer_main.trndate<=@day3 group by itemmst.itemcode,itemmst.itemname,stk_transfer_detail.companycode,shopcode_frm,stk_transfer_detail.yearcode union all select itemmst.itemcode,itemname,0 as opening_stock,0 as bottles_purchased,0 as bottles_sold,sum(qnty) as stock,stk_transfer_detail.companycode,shopcode_to as storecode,stk_transfer_detail.yearcode from stk_transfer_detail join itemmst on itemmst.itemcode=stk_transfer_detail.itemcode and stk_transfer_detail.companycode=itemmst.companycode join stk_transfer_main on stk_transfer_main.companycode=stk_transfer_detail.companycode and stk_transfer_main.yearcode=stk_transfer_detail.yearcode and stk_transfer_main.trnno=stk_transfer_detail.trnno where stk_transfer_main.trndate>=@day2 and stk_transfer_main.trndate<=@day3 group by itemmst.itemcode,itemmst.itemname,stk_transfer_detail.companycode,shopcode_to,stk_transfer_detail.yearcode union all select breakagedetail.itemcode,itemname,0 as opening_stock,0 as bottles_purchased,0 as bottles_sold,-1*sum(quantity) as stock,breakagedetail.companycode,shopcode,breakagedetail.yearcode from breakagedetail join breakagemain on breakagedetail.companycode=breakagemain.companycode and breakagedetail.yearcode=breakagemain.yearcode and breakagedetail.trnno=breakagemain.trnno join itemmst on itemmst.itemcode=breakagedetail.itemcode and itemmst.companycode=breakagedetail.companycode where receivd='False' and breakagemain.trndate>=@day2 and breakagemain.trndate<=@day3 group by breakagedetail.itemcode,itemname,breakagedetail.companycode,shopcode,breakagedetail.yearcode)x group by itemcode,itemname,companycode,storecode,yearcode"
        ob.insert(s)
        '--creating the function for the excise report---->
        s = "IF EXISTS(SELECT name FROM sys.objects WHERE name = N'xcise_statement') DROP FUNCTION xcise_statement"
        ob.insert(s)
        s = "CREATE function xcise_statement(@day1 as datetime,@day2 as datetime,@day3 as datetime) returns table as return select companycode,yearcode,type,groupname,ml,sum(qnty) as 'qnty' from(select itemmst.companycode,yearcode,'0-Opening' as type,groupname,ml,opening_stock as 'qnty' from  opening_closing_stock(@day1,@day2,@day3) join itemmst on itemmst.itemcode=opening_closing_stock.itemcode and itemmst.companycode=opening_closing_stock.companycode join groupmst on groupmst.groupcode=itemmst.groupcode and groupmst.companycode=itemmst.companycode union all select itemmst.companycode,yearcode,'1-Purchase' as type,groupname,ml,bottles_purchased as 'qnty' from  opening_closing_stock(@day1,@day2,@day3) join itemmst on itemmst.itemcode=opening_closing_stock.itemcode and itemmst.companycode=opening_closing_stock.companycode join groupmst on groupmst.groupcode=itemmst.groupcode and groupmst.companycode=itemmst.companycode union all select itemmst.companycode,yearcode,'2-Total' as type,groupname,ml,bottles_purchased+opening_stock as 'qnty' from  opening_closing_stock(@day1,@day2,@day3) join itemmst on itemmst.itemcode=opening_closing_stock.itemcode and itemmst.companycode=opening_closing_stock.companycode join groupmst on groupmst.groupcode=itemmst.groupcode and groupmst.companycode=itemmst.companycode union all select itemmst.companycode,yearcode,'3-Closing' as type,groupname,ml,stock as 'qnty' from  opening_closing_stock(@day1,@day2,@day3) join itemmst on itemmst.itemcode=opening_closing_stock.itemcode and itemmst.companycode=opening_closing_stock.companycode join groupmst on groupmst.groupcode=itemmst.groupcode and groupmst.companycode=itemmst.companycode union all select itemmst.companycode,yearcode,'4-Difference' as type,groupname,ml,bottles_sold as 'qnty' from  opening_closing_stock(@day1,@day2,@day3) join itemmst on itemmst.itemcode=opening_closing_stock.itemcode and itemmst.companycode=opening_closing_stock.companycode join groupmst on groupmst.groupcode=itemmst.groupcode and groupmst.companycode=itemmst.companycode union all select itemmst.companycode,yearcode,'5-Difference In BL' as type,groupname,ml,convert(float,convert(float,ml*bottles_sold)/1000) as 'qnty' from  opening_closing_stock(@day1,@day2,@day3) join itemmst on itemmst.itemcode=opening_closing_stock.itemcode and itemmst.companycode=opening_closing_stock.companycode join groupmst on groupmst.groupcode=itemmst.groupcode and groupmst.companycode=itemmst.companycode) as x group by companycode,yearcode,type,groupname,ml"
        ob.insert(s)
        '---dropping the view view tally purchase in case it exists and then creating it--->
        s = "if exists(select * from sys.views where name='vw_tally_purchase') drop view vw_tally_purchase"
        ob.insert(s)
        '--creating the view tally purchase--->
        s = "create view vw_tally_purchase as select x.trnno as no,x.companycode,x.yearcode,date,achead,supplier,itempurchased,loose,box,itemqty,itemrate,itemamount,chno,chdate,tpassno,batchno from (select purchasemain.trnno,purchasemain.companycode,purchasemain.yearcode,trndate as date,name as achead,itemname as itempurchased,itemloose as loose,itembox as box,itemquantity as itemqty,isnull(itemrate,0) as itemrate,isnull(itemamount,0) as itemamount,docno as chno,docdate as chdate,tppassno as tpassno,batchno from purchasemain join ledger on ledger.ledcode=purchasemain.purchaseacccode and ledger.companycode=purchasemain.companycode join purchasedetail on purchasedetail.companycode=purchasemain.companycode and purchasedetail.yearcode=purchasemain.yearcode and purchasedetail.trnno=purchasemain.trnno join itemmst on itemmst.itemcode=purchasedetail.itemcode and itemmst.companycode=purchasedetail.companycode ) as x join( select trnno,purchasemain.companycode,yearcode,name as supplier from purchasemain join ledger  on ledger.companycode=purchasemain.companycode and ledger.ledcode=purchasemain.suppliercode )as y on y.companycode=x.companycode and y.yearcode=x.yearcode and y.trnno=x.trnno"
        ob.insert(s)
        '--creating the tables tempitem and templedger--->
        s = "IF NOT EXISTS (SELECT * FROM information_schema.tables WHERE table_name='tempitem') create table tempitem(item nvarchar(225))"
        ob.insert(s)
        s = "IF NOT EXISTS (SELECT * FROM information_schema.tables WHERE table_name='templedger') create table templedger(ledname nvarchar(225))"
        ob.insert(s)
        MsgBox("Data Base updated")
    End Sub





End Class
