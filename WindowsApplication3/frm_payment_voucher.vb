
Imports System.Linq


Public Class frm_payment_voucher

    Dim s As String
    Dim ds As New DataSet
    Dim ds1 As New DataSet
    Dim ds2 As New DataSet
    Dim ob As New Class1
    Dim row As Integer
    Dim sum As Double
    Dim accode As String
    Dim cash_bank_code As String
    Dim ref_bill_no As String



    Private Sub gettrn()
        '----calculating the voucher number for the perticular transaction----->
        s = "select top 1 vchno from payment_detail where companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "' order by vchno desc"
        s = ob.executereader(s)
        If s = Nothing Then
            s = 1
        Else
            s = (Convert.ToInt32(s) + 1).ToString
        End If
        TextBox1.Text = s
    End Sub

    '---main form load--->
    Private Sub voucher_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '---disabeling the form1-->
        frm_MainForm.Enabled = False
        '---assigning the readonly properties-->
        TextBox1.ReadOnly = True
        TextBox3.ReadOnly = True
        TextBox10.ReadOnly = True
        TextBox13.ReadOnly = True
        '----assigning the color to the grid--->
        DataGridView1.BackgroundColor = Color.Ivory
        DataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.SandyBrown
        DataGridView1.DefaultCellStyle.BackColor = Color.NavajoWhite
        DataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Ivory
        '---assing the properties to the datagridview---->
        DataGridView1.RowHeadersVisible = False
        DataGridView1.ColumnHeadersVisible = True
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        '---the dataset ds1 ids for filling the cmb_cashbank combobox--->
        s = "select ledcode,name from ledger where companycode='" & Module1.companycode & "' and ledcode='" & Module1.comcashacc & "' or ledcode='" & Module1.combankacc & "'"
        ds1 = ob.populate(s)
        '--the dataset ds2 is for filling the cmb_party combobox-->
        s = "select ledcode,name from ledger where companycode='" & Module1.companycode & "'"
        ds2 = ob.populate(s)
        '---filling up the combboxes with the respective ledger codes------>
        ob.combofill(ds1, cmb_cashbank)
        ob.combofill(ds2, cmb_party)
        '--loading the default values in the comboboxes while adding--->
        If Module1.flag = 1 Then
            cmb_cashbank.Text = ds1.Tables(0).Rows(0).Item(1)
            cmb_party.Text = ds2.Tables(0).Rows(0).Item(1)
            '--filling the comboboxes during the edit mode--->
        ElseIf Module1.flag = 2 Then
            cmb_party.Text = Module1.vch_head_name
            cmb_cashbank.Text = Module1.vch_client_name
        End If
        '--calling the grid_fill function->
        refresh_grid()
    End Sub
    '---form cloasing event--->
    Private Sub payment_voucher_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        '---enabeling the form1--->
        frm_MainForm.Enabled = True
    End Sub
    '--function for fillint the grid----->
    Private Sub refresh_grid()
        '---regreshing the datagridview--->
        DataGridView1.Rows.Clear()
        DataGridView1.Columns.Clear()
        '---assigning the columns in the datagridview--->
        DataGridView1.Columns.Add("BILL NO.", "BLLNO")
        DataGridView1.Columns.Add("AMOUNT", "AMOUNT")
        DataGridView1.Columns.Add("PAYMENT", "PAYMENT")
        '---checking if the form has been opened in the add mode or edit mode-->
        If Module1.flag = 1 Then
            '--calling the gettrn function to get the voucher no.-->
            gettrn()
            '----populating the dataset------------->
            s = "select billno,sum(credit - debit),0 as amount from vw_payment_detail where companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "' and ledgercode='" & accode & "' and date>='" & Module1.comstdate & "' and date<='" & DateTimePicker1.Value.Date & "' group by companycode,yearcode,ledgercode,billno having sum(credit-debit)>0 and sum(credit-debit)<>0"
            ds = ob.populate(s)
            '---setting the default values of the text boxes to nothing--->
            TextBox2.Text = Nothing
            TextBox3.Text = Nothing
            TextBox8.Text = Nothing
            TextBox10.Text = Nothing
            TextBox12.Text = Nothing
            TextBox13.Text = Nothing
            '----gettin gthe total due amount in the text box 10--->
            '---getting the due amount--->
            s = "select sum(credit - debit) from vw_payment_detail where companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "' and ledgercode='" & accode & "' and date>='" & Module1.comstdate & "' and date<='" & DateTimePicker1.Value.Date & "' group by companycode,yearcode,ledgercode"
            TextBox10.Text = ob.executereader(s)
            '---logic for opening the form in the edit mode---->
        ElseIf Module1.flag = 2 Then
            '-----populating the dataset-------------->
            s = "select trnno,due_amount,debit-credit from payment_detail where vchno='" & Module1.vchno & "' and companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "'"
            ds = ob.populate(s)
            '--putting the editing vaues in the text boxes---->
            TextBox1.Text = Module1.vchno
            TextBox2.Text = Module1.vch_cheque_no
            TextBox8.Text = Module1.vch_narration
            TextBox3.Text = Module1.vch_amt_paid
            TextBox10.Text = Module1.vch_due
            TextBox12.Text = Module1.vch_discount
            TextBox13.Text = Module1.vch_net_due
            DateTimePicker1.Value = Module1.vch_date
        End If
        '----loop for filling up the datagridview with the data---->
        If Module1.flag = 1 Then
            If ds.Tables(0).Rows.Count > 0 And Val(TextBox10.Text) <> 0 Then
                DataGridView1.Rows.Add(ds.Tables(0).Rows.Count)
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    DataGridView1.Item(0, i).Value = ds.Tables(0).Rows(i).Item(0)
                    DataGridView1.Item(1, i).Value = ds.Tables(0).Rows(i).Item(1)
                    If Module1.flag = 2 Then
                        DataGridView1.Item(2, i).Value = ds.Tables(0).Rows(i).Item(2)
                    End If
                Next '---end of for loop ----->
            End If '--- end of the if loop --->
            '---populating teh datagrid view for the edit part of the form ----->
        ElseIf Module1.flag = 2 Then
            If ds.Tables(0).Rows.Count > 0 Then
                DataGridView1.Rows.Add(ds.Tables(0).Rows.Count)
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    DataGridView1.Item(0, i).Value = ds.Tables(0).Rows(i).Item(0)
                    DataGridView1.Item(1, i).Value = ds.Tables(0).Rows(i).Item(1)
                    If Module1.flag = 2 Then
                        DataGridView1.Item(2, i).Value = ds.Tables(0).Rows(i).Item(2)
                    End If
                Next '---end of for loop ----->
            End If '--- end of the if loop --->
        End If

    End Sub
    '---keyup event for the datagridview--->
    Private Sub DataGridView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyUp
        sum = 0
        For i = 0 To DataGridView1.Rows.Count - 1
            sum = sum + DataGridView1.Item(2, i).Value
        Next
        TextBox3.Text = sum
    End Sub
    '-----selecting the code of the ledger which the user selects in the combobox---->
    Private Sub cmb_cashbank_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmb_cashbank.SelectedIndexChanged
        Dim query = From p As DataRow In ds1.Tables(0) Where p.Item(1) = cmb_cashbank.Text Select p.Item(0)
        cash_bank_code = query(0).ToString
        DataGridView1.Rows.Clear()
        refresh_grid()
    End Sub
    '-----selecting the code of the ledger which the user selects in the combobox---->
    Private Sub cmb_party_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmb_party.SelectedIndexChanged
        Dim query = From p As DataRow In ds2.Tables(0) Where p.Item(1) = cmb_party.Text Select p.Item(0)
        accode = query(0).ToString
        DataGridView1.Rows.Clear()
        refresh_grid()
    End Sub
    '--event to call when the user presses the save button--->
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        '---if the user wishes to save without inputting the cheque number when he\she has selected the clint as bank account
        '---the software stops from promoting it---->
        s = "select acname from ledger join acountname on acountname.accode=ledger.accode where ledcode='" & cash_bank_code & "' and ledger.companycode='" & Module1.companycode & "' "
        s = ob.executereader(s)
        If s = "BANK ACCOUNTS" And TextBox2.Text = Nothing Then
            MsgBox("Plz input a cheque number to continue")
            Exit Sub
        End If
        '-----inserting value-----------by 1st deleting and then inseerting----->
        s = "delete from payment_detail where companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "' and vchno='" & TextBox1.Text & "'"
        ob.insert(s)
        s = "delete from payment_main where companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "' and vchno='" & TextBox1.Text & "'"
        ob.insert(s)

        '---inserting into the payment_details ----------------------------->
        '---running the for loop for getting the values from the datagrid--->
        For i = 0 To DataGridView1.Rows.Count - 2
            If Module1.flag = 1 Then
                If Not DataGridView1.Item(2, i).Value = "" Then
                    s = "insert into payment_detail(companycode,yearcode                  ,vchno                  ,trnno                                   ,due_amount                              ,debit                                   ,credit,vchdate)" & _
                         " values('" & Module1.companycode & "','" & Module1.yearcode & "','" & TextBox1.Text & "','" & DataGridView1.Item(0, i).Value & "','" & DataGridView1.Item(1, i).Value & "','" & DataGridView1.Item(2, i).Value & "','0'   ,'" & DateTimePicker1.Value.Date & "')"
                    ob.insert(s)
                End If
            ElseIf Module1.flag = 2 Then
                s = "insert into payment_detail(companycode,yearcode                  ,vchno                  ,trnno                                   ,due_amount                              ,debit                                   ,credit,vchdate)" & _
                     " values('" & Module1.companycode & "','" & Module1.yearcode & "','" & TextBox1.Text & "','" & DataGridView1.Item(0, i).Value & "','" & DataGridView1.Item(1, i).Value & "','" & DataGridView1.Item(2, i).Value & "','0'   ,'" & DateTimePicker1.Value.Date & "')"
                ob.insert(s)
            End If
        Next
        '---code for inserting into the payment_main ------------------->
        s = "insert into payment_main(companycode,yearcode                  ,vchno                  ,head_account    ,client_account          ,vchdate                             ,narration              ,cheque_no              ,due                     ,discount                ,net_due              ,amount_paid               ,discount_account_head)" & _
           " values('" & Module1.companycode & "','" & Module1.yearcode & "','" & TextBox1.Text & "','" & accode & "','" & cash_bank_code & "','" & DateTimePicker1.Value.Date & "','" & TextBox8.Text & "','" & TextBox2.Text & "','" & TextBox10.Text & "','" & TextBox12.Text & "','" & TextBox13.Text & "','" & TextBox3.Text & "','" & Module1.comdiscacc & "')"
        ob.insert(s)



        '---code for refreshing the form1--->
        frm_MainForm.mainformload()
        '----refreshing the form or closing the from--->
        If Module1.flag = 1 Then
            '---clearing the datagrid view before loading the form----->
            refresh_grid()
        ElseIf Module1.flag = 2 Then
            Me.Close()
        End If
    End Sub
    '---event to handel when the user presses the canscel button--->
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
    '---text change event for amount paid--->
    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        TextBox13.Text = Val(TextBox10.Text) - Val(TextBox12.Text) - Val(TextBox3.Text)
    End Sub
    '---text change event for handling discount-->
    Private Sub TextBox12_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox12.TextChanged
        TextBox13.Text = Val(TextBox10.Text) - Val(TextBox12.Text) - Val(TextBox3.Text)
    End Sub
End Class