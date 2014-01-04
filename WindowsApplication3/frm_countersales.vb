Imports System.IO
Imports System.Diagnostics
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports System.Linq


Public Class frm_countersales
    '=========================
    Dim ds As New DataSet
    Dim ds1 As New DataSet
    Dim ds2 As New DataSet
    Dim ds3 As New DataSet
    Dim ds4 As New DataSet
    '==========================
    Dim data_table As New DataTable
    Dim stock_data As New DataSet
    Dim dv As New DataView
    '==========================
    Dim packing As Integer
    Dim rate As Double
    Dim loose As Integer
    Dim box As Integer
    Dim quantity As Integer
    Dim amnt As Double
    Dim liter As Double
    '============================
    Dim ob As New Class1
    Dim obj As New Print_Class
    '============================
    Dim column As Integer
    Dim row As Integer
    Public position As Integer
    Public searchflag As Boolean
    '==========================
    Dim s As String


    Private Sub countersales_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '----putting on the key preview------------------>
        Me.KeyPreview = False
        ''---default seetings to adjust during the screen change resolution---->
        ''--aliigning the panel--->
        'Panel1.Width = Me.Width / 1.42
        ''--alligning the datagrid view--->
        'DataGridView1.Width = Me.Width / 1.68
        'DataGridView2.Left = Me.Width / 1.38
        'DataGridView2.Left = Me.Width / 1.38
        'Label6.Width = DataGridView1.Width + Button9.Width
        ''--alligning the text boxes---->
        'TextBox2.Left = Me.Width / 1.77
        'TextBox3.Left = Me.Width / 1.77
        'TextBox4.Left = TextBox3.Left + 47
        'TextBox5.Left = Me.Width / 1.77
        ''textbox6.text=
        'TextBox7.Left = Me.Width / 1.77
        ''TextBox8.Left = Label15.Left + 51
        'TextBox8.Left = Me.Width - 229
        'TextBox9.Left = Me.Width / 1.77
        ''---alligning teh lables----->
        'Label3.Left = TextBox2.Left - 101
        'Label5.Left = Me.Width / 1.38
        'Label8.Left = Label3.Left
        'Label9.Left = Label3.Left
        'Label10.Left = Label3.Left
        'Label11.Left = Label3.Left
        'Label12.Left = Label3.Left
        'Label15.Left = Me.Width / 1.38
        ''--alligning the comboboxes----->
        'ComboBox1.Left = Me.Width / 1.77
        'ComboBox2.Left = Me.Width / 1.77
        'ComboBox3.Left = Me.Width / 1.77
        ''--alligning the buttons---->
        'Button1.Left = TextBox2.Left - 117
        'Button2.Left = DataGridView2.Left + 161
        'Button5.Left = TextBox2.Left - 117
        'Button3.Left = Me.Width / 1.7
        'Button10.Left = Me.Width / 1.7
        '----disabeling the view for the surcharge % and the surcharge amount --->





        If Module1.flag = 1 Then
            TextBox7.Visible = False
            TextBox9.Visible = False
            Label7.Visible = False
            Label13.Visible = False
        ElseIf Module1.salestype = "CREDIT CARD" And Module1.flag = 2 Then
            TextBox7.Visible = True
            TextBox9.Visible = True
            Label7.Visible = True
            Label13.Visible = True
        ElseIf Module1.salestype = "CREDIT CARD" And Module1.flag = 2 Then
            TextBox7.Visible = False
            TextBox9.Visible = False
            Label7.Visible = False
            Label13.Visible = False
        End If


        '--making the default settings---->
        pos = False
        TextBox1.ReadOnly = True
        TextBox2.ReadOnly = True
        TextBox5.ReadOnly = True
        TextBox6.ReadOnly = True
        '----creating dataset for filling the combo boxes------->
        s = "select ledcode,name from ledger where companycode='" & Module1.companycode & "'"
        ds1 = ob.populate(s)
        s = "select shopcode,shopname from storage where companycode='" & Module1.companycode & "'"
        ds2 = ob.populate(s)
        s = "select ratecode,ratename from itemrateinfo where companycode='" & Module1.companycode & "'"
        ds3 = ob.populate(s)
        '----filling the combobox3 and combobox3 with all the shopnames and the ratenames-->
        ob.combofill(ds2, ComboBox2)
        ob.combofill(ds3, ComboBox3)
        '----calling the function-------------------------------->
        create_dataset_for_datagrid()
    End Sub

    '---getting the voucher number---->
    Private Sub get_voucher_number()
        If Module1.flag = 1 Then
            s = "select top 1 vchno from receipt_main where companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "' order by vchno desc"
            s = ob.executereader(s)
            If s = Nothing Then
                Module1.voucher_number = 1
            Else
                Module1.voucher_number = (Convert.ToInt32(s) + 1).ToString
            End If
        End If
    End Sub

    '---function to call when the user presses the save button----->
    Private Sub save()
        '---getting the fresh transaction number incase a bill has been already saved --->
        If Module1.flag = 1 Then
            gettrn()
        End If

        If Not Val(TextBox5.Text) = 0 And Module1.salestype <> "EXPENCES" Then
            '---getting the new voucher number for inserting into the voucher--->
            get_voucher_number()
            '---deleting from salesbilldetail------>
            s = "delete from salesbilldetail where trnno='" & TextBox1.Text & "' and companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "'"
            ob.insert(s)
            '--deleting form salesbillmain---->
            s = "delete from salesbillmain where trnno='" & TextBox1.Text & "' and companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "'"
            ob.insert(s)
            '----inserting inot salesbilldetail----->
            For i = 0 To DataGridView1.Rows.Count - 2
                s = "insert into salesbilldetail(companycode,itemcode                                ,tot_box                                 ,loose                                   ,qnty                                    ,itemamount                              ,trnno                  ,rate                                    ,trndate                             ,yearcode) " & _
                       "values('" & Module1.companycode & "','" & DataGridView1.Item(7, i).Value & "','" & DataGridView1.Item(4, i).Value & "','" & DataGridView1.Item(3, i).Value & "','" & DataGridView1.Item(5, i).Value & "','" & DataGridView1.Item(6, i).Value & "','" & TextBox1.Text & "','" & DataGridView1.Item(2, i).Value & "','" & DateTimePicker1.Value.Date & "','" & Module1.yearcode & "')"
                ob.insert(s)
            Next
            '---inserting into salesbillmain-------->
            s = "insert into salesbillmain(companycode,trnno                  ,trndate                             ,ledgercode                  ,amount                 ,salestype                  ,ratecode                  ,storecode                 ,yearcode                  ,discamount             ,netamount              ,account_head                    ,discount_account_head)" & _
                    " values('" & Module1.companycode & "','" & TextBox1.Text & "','" & DateTimePicker1.Value.Date & "','" & Module1.ledgercode & "','" & TextBox2.Text & "','" & Module1.salestype & "','" & Module1.ratecode & "','" & Module1.shopcode & "','" & Module1.yearcode & "','" & TextBox4.Text & "','" & TextBox5.Text & "','" & Module1.comsaleacc & "','" & Module1.comdiscacc & "')"
            ob.insert(s)





            '---inserting to the receipt_voucher if sales type not credit----->
            If (Module1.flag = 1 And Module1.salestype = "CASH") Or (Module1.flag = 1 And Module1.salestype = "CREDIT CARD") Then
                update_voucher()
            End If
            '----if the billprint on save in the companyparameter is on then the billprint sub is called-->
            If Module1.comsaveprint = "1" Then
                billprint()
            End If
            '---refreshing the form------------>
            If Module1.flag = 1 Then
                '---the create_dataset_for_datagrid() method is called--------->
                create_dataset_for_datagrid()
            ElseIf Module1.flag = 2 Then
                Me.Close()
            End If
        ElseIf Val(TextBox5.Text) = 0 And Module1.salestype <> "EXPENCES" Then
            MsgBox("You cannot save a bill with zero amount")
        End If
    End Sub

    '----updating the receipt voucher---->
    Private Sub update_voucher()
        '---deleting form the voucher that specific voucher number---->
        s = "delete from receipt_main where vchno='" & Module1.voucher_number & "' and companycode='" & Module1.companycode & "' and yearcode ='" & Module1.yearcode & "'"
        ob.insert(s)
        s = "delete from receipt_detail where vchno='" & Module1.voucher_number & "' and companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "'"
        ob.insert(s)
        '--inserting a new row in the voucher ----->
        s = "insert into receipt_detail(companycode,yearcode                  ,vchno                           ,trnno                  ,due_amount             ,debit,credit                 ,vchdate)" & _
              "values('" & Module1.companycode & "','" & Module1.yearcode & "','" & Module1.voucher_number & "','" & TextBox1.Text & "','" & TextBox5.Text & "','0'  ,'" & TextBox5.Text & "','" & DateTimePicker1.Value.Date & "')"
        ob.insert(s)
        s = "insert into receipt_main(companycode,yearcode                  ,vchno                           ,head_account                ,client_account              ,vchdate                             ,narration,cheque_no,due                    ,Discount               ,amount_paid            ,net_due,discount_account_head       ,receipt_type)" & _
           " values('" & Module1.companycode & "','" & Module1.yearcode & "','" & Module1.voucher_number & "','" & Module1.comsaleacc & "','" & Module1.ledgercode & "','" & DateTimePicker1.Value.Date & "','Sales'  ,' '      ,'" & TextBox2.Text & "','" & TextBox4.Text & "','" & TextBox5.Text & "','0'    ,'" & Module1.comdiscacc & "','" & Module1.salestype & "')"
        ob.insert(s)
        '---inserting into the sales_tax if there is any surcharge amount --->
        If Module1.salestype = "CREDIT CARD" Then
            s = "insert into sales_tax(trnno                        ,companycode                  ,yearcode                  ,cash_bank_book              ,tax_book                           ,tax_amount) " & _
                              "values(" & Module1.voucher_number & ",'" & Module1.companycode & "','" & Module1.yearcode & "','" & Module1.ledgercode & "','" & Module1.com_surcharge_acc & "','" & TextBox9.Text & "')"
            ob.insert(s)
        End If


    End Sub
    '----this is the part that the program runs every time the form is being loaded--------->
    Private Sub create_dataset_for_datagrid()
        '--clearing the rows from the grid if there is any information in there--->
        DataGridView1.Columns.Clear()
        '--------creating columns for the datagridview1--------------->
        DataGridView1.Columns.Add("BAR CODE", "BAR CODE")
        DataGridView1.Columns.Add("ITEM NAME", "ITEM NAME")
        DataGridView1.Columns.Add("RATE", "RATE")
        DataGridView1.Columns.Add("LOOSE", "LOOSE")
        DataGridView1.Columns.Add("BOX", "BOX")
        DataGridView1.Columns.Add("QNTY", "QNTY")
        DataGridView1.Columns.Add("TOTAL AMOUNT", "TOTAL AMOUNT")
        DataGridView1.Columns.Add("ITEM CODE", "ITEM CODE")
        DataGridView1.Columns.Add("PACKING", "PACKING")
        DataGridView1.Columns.Add("ML", "ML")
        '------declaring the column size ---------------------------->
        DataGridView1.Columns("BAR CODE").Width = 114
        DataGridView1.Columns("ITEM NAME").Width = 190
        DataGridView1.Columns("RATE").Width = 60
        DataGridView1.Columns("LOOSE").Width = 60
        DataGridView1.Columns("BOX").Width = 60
        DataGridView1.Columns("QNTY").Width = 60
        DataGridView1.Columns("TOTAL AMOUNT").Width = 60
        '---defining the read only property for the qnty column------------>
        DataGridView1.Columns(5).ReadOnly = True
        '---setting the values when the form is opened for adding items---->
        If Module1.flag = 1 Then
            '--getting the default cash accont code in the ledger------>
            Module1.ledgercode = Module1.comcashacc
            '----getting the shopcode of the companyparameter--------->
            Module1.shopcode = Module1.comdefstore
            '---getting the default rate of the companyparameter----->
            Module1.ratecode = Module1.comdefrate
            '---setting the default values when the form is being loaded---->
            gettrn()
            Module1.salestype = "CASH"
            TextBox2.Text = "0"
            TextBox3.Text = "0"
            TextBox4.Text = "0"
            TextBox5.Text = "0"
            '------setting the values when the form is opened in editing mode---->
        ElseIf Module1.flag = 2 Then
            TextBox1.Text = Module1.transaction
            TextBox2.Text = Module1.amount
            TextBox4.Text = Module1.discount
            TextBox9.Text = Module1.surcharge_amount
            DateTimePicker1.Value = Module1.transaction_date
            '-----making the dataset---------------->
            s = "select barcode,itemname,rate,loose,tot_box,qnty,itemamount,itemmst.itemcode,packing,ml from salesbilldetail join itemmst on itemmst.itemcode=salesbilldetail.itemcode and itemmst.companycode=salesbilldetail.companycode where trnno='" & Module1.transaction & "' and salesbilldetail.companycode='" & Module1.companycode & "' and salesbilldetail.yearcode='" & Module1.yearcode & "'"
            ds = ob.populate(s)
            DataGridView1.Rows.Add(ds.Tables(0).Rows.Count)
            For i = 0 To ds.Tables(0).Rows.Count - 1
                DataGridView1.Item(0, i).Value = ds.Tables(0).Rows(i).Item(0).ToString
                DataGridView1.Item(1, i).Value = ds.Tables(0).Rows(i).Item(1).ToString
                DataGridView1.Item(2, i).Value = ds.Tables(0).Rows(i).Item(2).ToString
                DataGridView1.Item(3, i).Value = ds.Tables(0).Rows(i).Item(3).ToString
                DataGridView1.Item(4, i).Value = ds.Tables(0).Rows(i).Item(4).ToString
                DataGridView1.Item(5, i).Value = ds.Tables(0).Rows(i).Item(5).ToString
                DataGridView1.Item(6, i).Value = ds.Tables(0).Rows(i).Item(6).ToString
                DataGridView1.Item(7, i).Value = ds.Tables(0).Rows(i).Item(7).ToString
                DataGridView1.Item(8, i).Value = ds.Tables(0).Rows(i).Item(8).ToString
                DataGridView1.Item(9, i).Value = ds.Tables(0).Rows(i).Item(9).ToString
            Next
        End If
        '-----filling the comboboxes with the respective codes------>
        ob.combo_fill_by_code(ds1, ComboBox1, Module1.ledgercode)
        '---selecting the texts in combobox 2 and combobox3-------->
        Dim query_for_shop_name = From p As DataRow In ds2.Tables(0) Where p.Item(0) = Module1.shopcode Select p.Item(1)
        Dim query_for_rate_name = From p As DataRow In ds3.Tables(0) Where p.Item(0) = Module1.ratecode Select p.Item(1)
        ComboBox2.Text = query_for_shop_name(0).ToString
        ComboBox3.Text = query_for_rate_name(0).ToString
        '---assigning colors to the buttons according to salestype------>
        If Module1.salestype = "CASH" Then
            give_button_colour(Button9, Button8, Button7, Button6, Button11)
        ElseIf Module1.salestype = "CREDIT CARD" Then
            give_button_colour(Button8, Button9, Button7, Button6, Button11)
        ElseIf Module1.salestype = "CREDIT ACCOUNT" Then
            give_button_colour(Button7, Button8, Button9, Button6, Button11)
        ElseIf Module1.salestype = "RETURN" Then
            give_button_colour(Button6, Button8, Button7, Button9, Button11)
        ElseIf Module1.salestype = "EXPENCES" Then
            give_button_colour(Button11, Button8, Button7, Button6, Button9)
        End If
        '---hiding columns of datagridview --------------->
        DataGridView1.Columns(7).Visible = False
        DataGridView1.Columns(8).Visible = False
        DataGridView1.Columns(9).Visible = False
        '---selecting the default 0,0 cell of the grid----->
        DataGridView1.ClearSelection()
        DataGridView1.CurrentCell = DataGridView1.Item(0, 0)
        DataGridView1.CurrentCell.Selected = True
        DataGridView1.Select()
        '---showing the stock and the sale by the selection of the companyparameter----------->
        If Module1.comsaleref = "1" Then
            show_stock()
            show_sale()
        End If
    End Sub

    '---selected index change for selecting the ledger account --->
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim query = From p In ds1.Tables(0) Where p.Item(1) = ComboBox1.Text Select p.Item(0)
        Module1.ledgercode = query(0).ToString
        '---selecting back the datagridview1 after every drop down close event of the combo box---->
        DataGridView1.Select()
    End Sub

    '---selecting the perticular shopcode---------->
    Private Sub ComboBox2_DropDownClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox2.DropDownClosed
        Dim query = From p In ds2.Tables(0) Where p.Item(1) = ComboBox2.Text Select p.Item(0)
        Module1.shopcode = query(0).ToString
        '---selecting back the datagridview1 after every drop down close event of the combo box---->
        DataGridView1.Select()
    End Sub
    '---selecting the perticular ratecode---------->
    Private Sub ComboBox3_DropDownClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox3.DropDownClosed
        Dim query = From p In ds3.Tables(0) Where p.Item(1) = ComboBox3.Text Select p.Item(0)
        Module1.ratecode = query(0).ToString
        '---selecting back the datagridview1 after every drop down close event of the combo box---->
        DataGridView1.Select()
    End Sub
    '------function for bill printing---------------------------------->
    Private Sub billprint()
        If Module1.comprintmode = "dos" Then
            '--------bill printing for the dos mode ---->
            Dim fwriter As StreamWriter
            fwriter = File.CreateText(Application.StartupPath + "\Bill.txt")
            fwriter.WriteLine(obj.centre_alignment(Module1.companyname, 31) + Module1.companyname)
            fwriter.WriteLine(obj.centre_alignment(Module1.comaddress1, 31) + Module1.comaddress1)
            fwriter.WriteLine(obj.centre_alignment(Module1.comaddress2, 31) + Module1.comaddress2)
            fwriter.WriteLine(obj.centre_alignment(Module1.comcity + " " + Module1.comstate + "-" + Module1.compin, 31) + Module1.comcity + " " + Module1.comstate + "-" + Module1.compin)
            fwriter.WriteLine(obj.centre_alignment("PHONE: " + Module1.comphone, 31) + "PHONE: " + Module1.comphone)
            fwriter.WriteLine(obj.centre_alignment("email: " + Module1.comemail, 31) + "email: " + Module1.comemail)
            fwriter.WriteLine("-------------------------------")
            If Module1.flag = 1 Then
                fwriter.WriteLine(Module1.salestype + " SALE" + vbTab + "ORIGINAL")
            ElseIf Module1.flag = 2 Then
                fwriter.WriteLine(Module1.salestype + " SALE" + vbTab + "DUPLICATE")
            End If
            fwriter.WriteLine(" ")
            fwriter.WriteLine("Bill No " + TextBox1.Text.ToUpper + vbTab + "DATE: " + DateTimePicker1.Value.Date)
            fwriter.WriteLine("PARTYCULARS" + vbTab + "QTY" + " " + "RATE" + " " + "AMOUNT")
            fwriter.WriteLine("-------------------------------")
            For i = 0 To DataGridView1.Rows.Count - 2
                Dim stritem As String
                Dim strqty As String
                Dim strrate As String
                Dim stramount As String
                stritem = Strings.Left(DataGridView1.Item(1, i).Value.ToString, 13)
                strqty = DataGridView1.Item(5, i).Value.ToString
                strrate = DataGridView1.Item(2, i).Value.ToString
                stramount = DataGridView1.Item(6, i).Value.ToString
                fwriter.WriteLine(stritem + obj.space_cal(stritem, 13) + obj.space_cal(strqty, 4) + strqty + obj.space_cal(strrate, 6) + strrate + obj.space_cal(stramount, 8) + stramount)
            Next
            fwriter.WriteLine("-------------------------------")
            fwriter.WriteLine("TOTAL AMOUNT" + vbTab + obj.space_cal(TextBox2.Text, 15) + TextBox2.Text)
            fwriter.WriteLine("DISCOUNT" + vbTab + obj.space_cal(TextBox4.Text, 15) + TextBox4.Text)
            fwriter.WriteLine("-------------------------------")
            fwriter.WriteLine("NET AMOUNT" + vbTab + obj.space_cal(TextBox5.Text, 15) + TextBox5.Text)
            fwriter.WriteLine(" ")
            fwriter.WriteLine(" ")
            fwriter.WriteLine(Module1.username)
            fwriter.WriteLine(obj.space_cal("FOR " + Module1.companyname, 31) + "FOR " + Module1.companyname)
            fwriter.WriteLine(" ")
            fwriter.WriteLine(obj.centre_alignment(Module1.combillfooter, 31) + Module1.combillfooter)
            fwriter.WriteLine(obj.centre_alignment(Module1.combillfooter2, 31) + Module1.combillfooter2)
            fwriter.WriteLine(obj.centre_alignment(Module1.combillfooter3, 31) + Module1.combillfooter3)
            fwriter.WriteLine(obj.centre_alignment(Module1.combillfooter4, 31) + Module1.combillfooter4)
            fwriter.WriteLine("-------------------------------")
            fwriter.Flush()
            fwriter.Close()

            Dim prtStart As New Process
            Dim prtinfo As New ProcessStartInfo("cmd.exe")
            With prtinfo
                .FileName = Application.StartupPath + "\Bill.txt"
                .Verb = "print"
                .WindowStyle = ProcessWindowStyle.Hidden
            End With
            prtStart = Process.Start(prtinfo)

            '----bill printing for the windows ---->
        ElseIf Module1.comprintmode = "windows" Then

            If DataGridView1.Rows.Count < 2 Then
                Exit Sub
            End If
            Dim Rpt_Bill As New rep_Sales_Bill
            Dim dsbill As New ds_sales_statement
            Dim gross As Double = 0
            Dim tot_qnty As Integer = 0
            Dim dr As DataRow
            For i = 0 To DataGridView1.Rows.Count - 2
                dr = dsbill.Tables(0).NewRow
                dr("itemname") = DataGridView1.Item(1, i).Value
                dr("qnty_sold") = DataGridView1.Item(5, i).Value
                tot_qnty = tot_qnty + DataGridView1.Item(5, i).Value
                dr("tot_qnty") = tot_qnty
                dr("rate") = DataGridView1.Item(2, i).Value
                dr("amt") = DataGridView1.Item(6, i).Value
                gross = gross + DataGridView1.Item(6, i).Value
                dr("gross") = gross
                dr("companyname") = Module1.companyname
                dr("trndate1") = Format(DateTimePicker1.Value, "MM/dd/yyyy").ToString
                dr("trnno") = Val(TextBox1.Text)
                dr("discount") = Val(TextBox4.Text)
                dr("netamount") = Val(TextBox5.Text)
                dr("address1") = Module1.comaddress1
                dr("address2") = Module1.comaddress2
                dr("city") = Module1.comcity + " " + Module1.comstate + "-" + Module1.compin
                dr("phone") = "PHONE: " + Module1.comphone
                dr("saletype") = Module1.salestype + " SALE"
                dr("user") = Module1.username
                If Module1.flag = 1 Then
                    dr("original") = "ORIGINAL"
                ElseIf Module1.flag = 2 Then
                    dr("original") = "DUPLICATE"
                End If
                dr("footer") = Module1.combillfooter
                dr("email") = "email: " + Module1.comemail
                dr("footer2") = Module1.combillfooter2
                dr("footer3") = Module1.combillfooter3
                dr("footer4") = Module1.combillfooter4
                dsbill.Tables(0).Rows.Add(dr)
            Next
            If dsbill.Tables(0).Rows.Count > 0 Then
                Rpt_Bill.SetDataSource(dsbill)
                Rpt_Bill.PrintOptions.PrinterName = Module1.comprinter
                Rpt_Bill.PrintToPrinter(1, True, 0, 0)
            End If
        Else
            MsgBox("Please Select Print Mode In Company Parameter.", MsgBoxStyle.Information, "Print Mode")
        End If
    End Sub
    '---function for giving the colour of the buttons----->
    Private Sub give_button_colour(ByVal button1 As Button, ByVal button2 As Button, ByVal button3 As Button, ByVal button4 As Button, ByVal button5 As Button)
        button1.BackColor = Color.DodgerBlue
        button2.BackColor = Color.WhiteSmoke
        button3.BackColor = Color.WhiteSmoke
        button4.BackColor = Color.WhiteSmoke
        button5.BackColor = Color.WhiteSmoke
    End Sub
    '----getting the next transaction number-------------->
    Private Sub gettrn()
        s = "select top 1 trnno from salesbillmain where companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "' order by trnno desc"
        TextBox1.Text = ob.executereader(s)
        If TextBox1.Text = Nothing Then
            TextBox1.Text = 1
        Else
            TextBox1.Text = Val(TextBox1.Text) + 1
        End If
    End Sub
    '----searching if the itemcode is already present in the grid----->
    Public Function searchgrid(ByVal s) As Boolean
        searchflag = False
        For i = 0 To DataGridView1.Rows.Count - 2
            If DataGridView1.Item(7, i).Value = s Then
                searchflag = True
                position = i
            End If
        Next
        Return searchflag
    End Function
    '---calculating the total amount--------------------->
    Private Sub get_total_amount()
        amount = 0
        For i = 0 To DataGridView1.Rows.Count - 2
            If Not DataGridView1.Item(6, i).Value = Nothing Then
                amount = amount + DataGridView1.Item(6, i).Value
            End If
        Next
        TextBox2.Text = amount
    End Sub
    '----calculating the total liter-------------------->
    Private Sub calculate_ltr()
        liter = 0
        For i = 0 To DataGridView1.Rows.Count - 2
            liter = liter + (DataGridView1.Item(5, i).Value * DataGridView1.Item(9, i).Value) / 1000
        Next
        TextBox6.Text = liter
        '---creating a beep when the sale bill crosses the 36 liter -->
        'If liter >= 30 Then
        '    While True
        '        Console.Beep()
        '    End While
        'End If

    End Sub
    '--------calculation part for the datagridview1------------------------------->
    '--calculating the amount and liter everytime an user inputs somthing--->
    Private Sub DataGridView1_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        get_total_amount()
        calculate_ltr()
    End Sub
    '--calculating the amount and liter everytime an user removes somthing--->
    Private Sub DataGridView1_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles DataGridView1.RowsRemoved
        get_total_amount()
        calculate_ltr()
    End Sub
    '----code for the  datagridview1 key up event-------------->
    Private Sub DataGridView1_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyUp
        '---clearing allthe rows from the datatable before every new fetch---->
        data_table.Rows.Clear()
        '-----getting the value for the current row and the current column------->
        Module1.row = DataGridView1.CurrentCell.RowIndex - 1
        column = DataGridView1.CurrentCell.ColumnIndex
        '---if the user has pressed the enter key--->
        If e.KeyData = Keys.Enter And Module1.row >= 0 Then
            '--if the key entered is in between column 0 and 1----------->
            If column >= 0 And column <= 1 Then
                '---filling the dataset------->
                s = DataGridView1.Item(column, Module1.row).Value
                s = "select itemcode,itemname,barcode,packing,ml from itemmst where barcode ='" & s & "' and companycode='" & Module1.companycode & "' or itemname ='" & s & "' and companycode='" & Module1.companycode & "'"
                data_table = ob.populate2(s)
                '--if an item is found by that barcode or name------------>
                If data_table.Rows.Count > 0 Then
                    '---checking if the item to be inputed is already present in the grid or not---->
                    s = data_table.Rows(0).Item(0)
                    '---if an item by that barcode exists but not present in the grid---------------------->
                    If Not searchgrid(s) Then
                        '---getiing the rate for that item---------------->
                        s = "select salesrate from vw_ratemst where ratecode='" & Module1.ratecode & "' and itemcode='" & data_table.Rows(0).Item(0) & "' and companycode='" & Module1.companycode & "'"
                        rate = Convert.ToDouble(ob.executereader(s))
                        '---feeling up the gridview--------------------->
                        DataGridView1.Item(0, Module1.row).Value = data_table.Rows(0).Item(2).ToString
                        DataGridView1.Item(1, Module1.row).Value = data_table.Rows(0).Item(1).ToString
                        DataGridView1.Item(2, Module1.row).Value = rate
                        DataGridView1.Item(3, Module1.row).Value = 1
                        DataGridView1.Item(4, Module1.row).Value = 0
                        DataGridView1.Item(5, Module1.row).Value = 1
                        DataGridView1.Item(6, Module1.row).Value = rate
                        DataGridView1.Item(7, Module1.row).Value = data_table.Rows(0).Item(0).ToString
                        DataGridView1.Item(8, Module1.row).Value = data_table.Rows(0).Item(3).ToString
                        DataGridView1.Item(9, Module1.row).Value = data_table.Rows(0).Item(4).ToString
                        '---if an item by that itemcode is already present in the grid
                        '--but also checking if the user has not pressed enter in the black region---->
                    ElseIf searchgrid(s) And DataGridView1.Item(7, Module1.row).Value = Nothing Then
                        '----if an item with that itemcode already exists--------->
                        loose = DataGridView1.Item(3, position).Value + 1
                        packing = DataGridView1.Item(8, position).Value
                        rate = DataGridView1.Item(2, position).Value
                        box = DataGridView1.Item(4, position).Value
                        quantity = loose + box * packing
                        amnt = quantity * rate
                        '----putting the respective values in the datagridview1---->
                        DataGridView1.Item(3, position).Value = loose
                        DataGridView1.Item(5, position).Value = quantity
                        DataGridView1.Item(6, position).Value = amnt
                        '---code for selecting and removing the present row-------->
                        DataGridView1.ClearSelection()
                        DataGridView1.CurrentCell = DataGridView1.Item(0, Module1.row)
                        DataGridView1.CurrentCell.Selected = True
                        DataGridView1.Rows.Remove(DataGridView1.CurrentRow)
                        '--selecting the next entry portion of the grid------------>
                        DataGridView1.ClearSelection()
                        DataGridView1.CurrentCell = DataGridView1.Item(0, DataGridView1.Rows.Count - 1)
                        DataGridView1.CurrentCell.Selected = True
                    End If
                    '---if no items by that barcode or name is present-------->
                    '----then the grid search form is opened------------------>
                Else
                    '---if no item by that name or barcode exists-->
                    s = DataGridView1.Item(column, Module1.row).Value
                    frm_gridsearch.TextBox1.Text = s
                    frm_gridsearch.Show()
                End If
                '--if the user wishes to change the rate or loose---->
            ElseIf column >= 2 And column <= 4 Then
                loose = DataGridView1.Item(3, Module1.row).Value
                packing = DataGridView1.Item(8, Module1.row).Value
                rate = DataGridView1.Item(2, Module1.row).Value
                box = DataGridView1.Item(4, Module1.row).Value
                quantity = loose + box * packing
                amnt = quantity * rate
                DataGridView1.Item(3, Module1.row).Value = loose
                DataGridView1.Item(5, Module1.row).Value = quantity
                DataGridView1.Item(6, Module1.row).Value = amnt
                '--selecting the next entry portion of the grid--->
                DataGridView1.ClearSelection()
                DataGridView1.CurrentCell = DataGridView1.Item(0, DataGridView1.Rows.Count - 1)
                DataGridView1.CurrentCell.Selected = True
            End If
            '--removing the respective row where the user has pressed the delete key and the new row is not uncomitted--->
        ElseIf e.KeyData = Keys.Delete And Not DataGridView1.CurrentRow.IsNewRow Then
            '----code for removing the rows from the datagridview-------->
            DataGridView1.Rows.Remove(DataGridView1.CurrentRow)
            '--selecting the next entry portion of the grid--->
            DataGridView1.ClearSelection()
            DataGridView1.CurrentCell = DataGridView1.Item(0, DataGridView1.Rows.Count - 1)
            DataGridView1.CurrentCell.Selected = True
        End If
    End Sub
    '-----end of all the calculation part of the datagridview1-------------------->
    '----putting a default 0 in the discount percent box--------->
    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        If Val(TextBox2.Text) > 0 And Val(TextBox3.Text) <> 0 Then
            TextBox4.Text = Val(TextBox2.Text) * Val(TextBox3.Text) / 100
        Else
            TextBox5.Text = Val(TextBox2.Text) - Val(TextBox4.Text)
        End If
    End Sub
    '--calculating the discount on the discount percent selected by the user--->
    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        TextBox4.Text = Val(TextBox2.Text) * Val(TextBox3.Text) / 100
    End Sub
    '--calculating the net amount to be paid------->
    Private Sub TextBox4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.TextChanged
        TextBox5.Text = Val(TextBox2.Text) - Val(TextBox4.Text)
    End Sub
    '---calculating the surcharge amount if any--->
    Private Sub TextBox5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox5.TextChanged
        If Val(TextBox7.Text) > 0 Then : TextBox9.Text = Val(TextBox5.Text) * Val(TextBox7.Text) / 100 : End If
    End Sub
    '---calculating the surcharge amount on the percentage given by the user--->
    Private Sub TextBox7_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox7.TextChanged
        TextBox9.Text = Val(TextBox5.Text) * Val(TextBox7.Text) / 100
    End Sub
    '---letting only the integer values be put in the text box 3------->
    Private Sub TextBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        Select Case e.KeyChar
            Case "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", ".", vbBack
                e.Handled = False
            Case Else
                e.Handled = True
        End Select
    End Sub
    '----letting only theinteger values be put in the text box 4------>
    Private Sub TextBox4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox4.KeyPress
        Select Case e.KeyChar
            Case "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", ".", vbBack
                e.Handled = False
            Case Else
                e.Handled = True
        End Select
    End Sub
    '---function for showing the stock--------------->
    Private Sub show_stock()
        Dim yyyymmdd As String
        yyyymmdd = DateTimePicker1.Value.Year & "-" & DateTimePicker1.Value.Month & "-" & DateTimePicker1.Value.Day
        s = "select itemname,bottles_sold,stock from opening_closing_stock('" & Module1.comstdate & "','" & DateTimePicker1.Value.Date & "','" & DateTimePicker1.Value.Date & "') where companycode='" & Module1.companycode & "' and storecode='" & Module1.shopcode & "' and yearcode='" & Module1.yearcode & "' and stock<>0 or companycode='" & Module1.companycode & "' and storecode='" & Module1.shopcode & "' and yearcode='" & Module1.yearcode & "' and bottles_sold<>0"
        stock_data = ob.populate(s)
        DataGridView2.DataSource = stock_data.Tables(0)
        DataGridView2.Columns(0).HeaderText = "Item Name"
        DataGridView2.Columns(1).HeaderText = "Sales"
        DataGridView2.Columns(2).HeaderText = "Stock"
        DataGridView2.Columns(0).Width = 150
        DataGridView2.Columns(1).Width = 45
        DataGridView2.Columns(2).Width = 45
    End Sub
    '---function for showing the sales--------------->
    Private Sub show_sale()
        '---clearing teh datagridview --->
        DataGridView3.Columns.Clear()
        DataGridView3.Rows.Clear()
        '--declaring a variable for selecting the date ---->
        Dim yyyymmdd As String
        yyyymmdd = DateTimePicker1.Value.Year & "-" & DateTimePicker1.Value.Month & "-" & DateTimePicker1.Value.Day
        '---declaring the variables --->
        Dim b_sold1 As Integer
        Dim b_sold2 As Integer
        Dim amt1 As Double
        Dim amt2 As Double
        '---creating columns in the datagrid view 3----->
        DataGridView3.Columns.Add("SALESTYPE", "Sales Type")
        DataGridView3.Columns.Add("BOTTLES SOLD", "Bottles Sold")
        DataGridView3.Columns.Add("TOTAL AMOUNT", "Total Amount")
        '---populating the datagrid view with the required datas ------------>
        s = "select salestype,bottles_sold,total_amount from vw_sale where companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "' and trndate='" & yyyymmdd & "'"
        Dim datatable As New DataTable
        datatable = ob.populate2(s)
        '---creting rows in the datagrid view 3--->
        DataGridView3.Rows.Add(datatable.Rows.Count + 1)
        '---populating the datagrid view 3 -------->
        For i = 0 To datatable.Rows.Count - 1
            DataGridView3.Item(0, i).Value = datatable.Rows(i).Item(0)
            DataGridView3.Item(1, i).Value = datatable.Rows(i).Item(1)
            DataGridView3.Item(2, i).Value = datatable.Rows(i).Item(2)
            '---calculating the number of bottles sold and the cash amount ---->
            If DataGridView3.Item(0, i).Value = "RETURN" Or DataGridView3.Item(0, i).Value = "DISCOUNT" Or DataGridView3.Item(0, i).Value = "EXPENCE" Then
                amt2 = amt2 + DataGridView3.Item(2, i).Value
                b_sold2 = b_sold2 + DataGridView3.Item(1, i).Value
            Else
                amt1 = amt1 + DataGridView3.Item(2, i).Value
                b_sold1 = b_sold1 + DataGridView3.Item(1, i).Value
            End If
        Next
        '---creating the closing row in the datagrid view3-------------------->
        DataGridView3.Item(0, datatable.Rows.Count).Value = "CLOSING :"
        DataGridView3.Item(1, datatable.Rows.Count).Value = b_sold1 - b_sold2
        DataGridView3.Item(2, datatable.Rows.Count).Value = amt1 - amt2
        '---assing the property to the datagridview -------------------------->
        DataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub
    '--key up key down and key press events----------------->
    Private Sub TextBox8_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox8.KeyUp
        dv.Table = stock_data.Tables(0)
        dv.RowFilter = "itemname like '" & TextBox8.Text & "%'"
        DataGridView2.DataSource = dv
    End Sub
    '-------------end of all key up and key press events---------------->
    '----button click events------------------>
    '----filling the default ledegrs in the combo box and the button colour for this perticular select statement--->
    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        '----disabeling the view for the surcharge % and the surcharge amount --->
        TextBox7.Visible = False
        TextBox9.Visible = False
        Label7.Visible = False
        Label13.Visible = False

        ComboBox1.Items.Clear()
        Module1.salestype = "CASH"
        give_button_colour(Button9, Button8, Button7, Button6, Button11)
        Module1.ledgercode = Module1.comcashacc
        ob.combo_fill_by_code(ds1, ComboBox1, Module1.ledgercode)
        '---selecting back the datagrid after every change in the transaction mode--->
        DataGridView1.Select()
    End Sub
    '----filling the default ledegrs in the combo box and the button colour for this perticular select statement--->
    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        '----disabeling the view for the surcharge % and the surcharge amount --->
        TextBox7.Visible = True
        TextBox9.Visible = True
        Label7.Visible = True
        Label13.Visible = True
        TextBox7.Text = Module1.com_surcharge_percent

        ComboBox1.Items.Clear()
        Module1.salestype = "CREDIT CARD"
        give_button_colour(Button8, Button9, Button7, Button6, Button11)
        Module1.ledgercode = Module1.combankacc
        ob.combo_fill_by_code(ds1, ComboBox1, Module1.ledgercode)
        '---selecting back the datagrid after every change in the transaction mode--->
        DataGridView1.Select()
    End Sub
    '----filling the default ledegrs in the combo box and the button colour for this perticular select statement--->
    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        '----disabeling the view for the surcharge % and the surcharge amount --->
        TextBox7.Visible = False
        TextBox9.Visible = False
        Label7.Visible = False
        Label13.Visible = False

        ComboBox1.Items.Clear()
        Module1.salestype = "CREDIT ACCOUNT"
        give_button_colour(Button7, Button8, Button9, Button6, Button11)
        s = "select ledcode,name,companycode from vw_detors where companycode='" & Module1.companycode & "'"
        dataset = ob.populate(s)
        For i = 0 To dataset.Tables(0).Rows.Count - 1
            Module1.ledgercode = dataset.Tables(0).Rows(i).Item(0)
            ob.combo_fill_by_code(ds1, ComboBox1, Module1.ledgercode)
        Next
        '---selecting back the datagrid after every change in the transaction mode--->
        DataGridView1.Select()
    End Sub
    '----filling the default ledegrs in the combo box and the button colour for this perticular select statement--->
    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        '----disabeling the view for the surcharge % and the surcharge amount --->
        TextBox7.Visible = False
        TextBox9.Visible = False
        Label7.Visible = False
        Label13.Visible = False


        ComboBox1.Items.Clear()
        Module1.salestype = "RETURN"
        give_button_colour(Button6, Button8, Button7, Button9, Button11)
        Module1.ledgercode = Module1.comcashacc
        ob.combo_fill_by_code(ds1, ComboBox1, Module1.ledgercode)
        '---selecting back the datagrid after every change in the transaction mode--->
        DataGridView1.Select()
    End Sub
    '----filling the default ledegrs in the combo box and the button colour for this perticular select statement--->
    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        '----disabeling the view for the surcharge % and the surcharge amount --->
        TextBox7.Visible = False
        TextBox9.Visible = False
        Label7.Visible = False
        Label13.Visible = False

        Module1.salestype = "EXPENCES"
        give_button_colour(Button11, Button8, Button7, Button6, Button9)
        Module1.ledgercode = Module1.comexpenceacc
        '---assing the values for the expence from and showing it--->
        frm_ExpenceForm.voucher_date = DateTimePicker1.Value.Date
        frm_ExpenceForm.Show()
        '---selecting back the datagrid after every change in the transaction mode--->
        DataGridView1.Select()
    End Sub
    '---event for the save button--------->
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        save()
    End Sub
    '----event for the billprinting button------->
    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        billprint()
    End Sub
    '---event for the cancel button----------->
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
        frm_MainForm.Enabled = True
        frm_ContainerForm.Button1.Enabled = True
    End Sub
    '---event for the stock showing button------>
    Private Sub Button3_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        show_stock()
    End Sub
    '--event for the sale showing button------->
    Private Sub Button5_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        show_sale()
    End Sub
    '---end of all button click events--------->
    '----counter salesform closing event--------->
    Private Sub countersales_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            If frm_MainForm.Visible = True Then
                frm_MainForm.mainformload()
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        If frm_gridsearch.Visible = True Then
            frm_gridsearch.Close()
        End If
    End Sub



End Class