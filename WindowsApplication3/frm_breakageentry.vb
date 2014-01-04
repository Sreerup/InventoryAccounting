Public Class frm_breakageentry
    '--variable description for breakage--->
    Dim ds As New DataSet
    Dim ds1 As New DataSet
    Dim ds2 As New DataSet
    Dim dset As New DataSet
    '--dataset for puttin in the bill no and the tppassno-->
    Dim dset_bill_no As New DataSet
    Dim dset_tppass_no As New DataSet
    Dim ob As New Class1
    Dim s As String
    Dim loose As Integer
    Dim packing As Integer
    Dim box As Integer
    Dim quantity As Integer
    Dim column As Integer
    Dim breakage_ledgercode As String
    Dim breakage_store_code As String

    Dim billno As String
    Dim pass_no As String


    Private Sub gettrn()
        s = "select top 1 trnno from breakagemain where companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "' order by trnno desc"
        TextBox2.Text = ob.executereader(s)
        If TextBox2.Text = Nothing Then
            TextBox2.Text = "1"
        Else
            TextBox2.Text = Val(TextBox2.Text) + 1
        End If
    End Sub
    '---selected index change for the shop selection combobox--->
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim query = From p As DataRow In ds1.Tables(0) Where p(1) = ComboBox1.Text Select p(0)
        breakage_store_code = query(0).ToString
    End Sub
    '---selected index change for the ledgercode combobox--->
    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged

        ComboBox3.Items.Clear()
        ComboBox4.Items.Clear()


        '---query to select the ledgercode form the combobox--->
        Dim query = From p In ds2.Tables(0) Where p(1) = ComboBox2.Text Select p(0)
        breakage_ledgercode = query(0).ToString
        '----populating the dataset for the purchase bill no for those breakages--->
        If dset_bill_no.Tables.Count > 0 Then
            dset_bill_no.Tables.Clear()
        End If
        s = "select '0',docno from purchasemain where companycode='" & Module1.companycode & "' and yearcode ='" & Module1.yearcode & "' and suppliercode='" & breakage_ledgercode & "' and ptype<>'RETURN'"
        dset_bill_no = ob.populate(s)
        '---populating the dataset for the purchase tp pass no for that breakage---->
        If dset_tppass_no.Tables.Count > 0 Then
            dset_tppass_no.Tables.Clear()
        End If
        s = "select '0',tppassno from purchasemain where companycode='" & Module1.companycode & "' and yearcode ='" & Module1.yearcode & "' and suppliercode='" & breakage_ledgercode & "' and ptype<>'RETURN'"
        dset_tppass_no = ob.populate(s)
        '---filling the combobox for the bill no---->
        ob.combofill(dset_bill_no, ComboBox3)
        '---filling the combobox for the tppassno---->
        ob.combofill(dset_tppass_no, ComboBox4)
        '--keeping the first row of the table selected in the combobox--->
        If dset_bill_no.Tables(0).Rows.Count > 0 Then
            ComboBox3.Text = dset_bill_no.Tables(0).Rows(0).Item(1)
        ElseIf dset_tppass_no.Tables(0).Rows.Count > 0 Then
            ComboBox4.Text = dset_tppass_no.Tables(0).Rows(0).Item(1)
        End If
    End Sub


    '----selected index change for combobox3 to select the tppassno for the selected billno
    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        billno = ComboBox3.Text
        '---selecting the respective tppassno for the billno selected--->
        s = "select tppassno from purchasemain where yearcode='" & Module1.yearcode & "' and companycode ='" & Module1.companycode & "' and docno='" & ComboBox3.Text & "' and suppliercode='" & breakage_ledgercode & "' and ptype<>'RETURN'"
        s = ob.executereader(s)
        If Not ComboBox4.Text = s Then
            ComboBox4.Text = s
        End If
    End Sub

    '---selected index change for combobox4 to select the billno for the change in the tppassno--->
    Private Sub ComboBox4_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox4.SelectedIndexChanged
        pass_no = ComboBox4.Text
        '---selecting teh respective billno for the passno selected--->
        s = "select docno from purchasemain where yearcode='" & Module1.yearcode & "' and companycode ='" & Module1.companycode & "' and tppassno='" & ComboBox4.Text & "' and suppliercode='" & breakage_ledgercode & "' and ptype<>'RETURN'"
        s = ob.executereader(s)
        '--if the correspoding billno for that tppassno is not selected in the combobox-->
        If Not ComboBox3.Text = s Then
            ComboBox3.Text = s
        End If
    End Sub





    '---breakage form load event--->
    Private Sub breakageentry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '---disabeling the form1 when the breakage form is opened--->
        frm_MainForm.Enabled = False
        '---assingning the readonly property to the text box1-->
        TextBox2.ReadOnly = True
        '--loasding the datasets--->
        s = "select shopcode,shopname from storage where companycode='" & Module1.companycode & "'"
        ds1 = ob.populate(s)
        s = "select ledcode,name from ledger where companycode='" & Module1.companycode & "'"
        ds2 = ob.populate(s)
        '---filling the comboxes---->
        ob.combofill(ds1, ComboBox1)
        ob.combofill(ds2, ComboBox2)
        '---calling the function for creating columns in the datagrid view1--->
        refresh_breakage_grid()
    End Sub
    '---form closing event of the breakage form-->
    Private Sub breakageentry_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        frm_MainForm.Enabled = True
    End Sub

    '---function got creating the columns anfd filling it with datas during the edit mode-->
    Private Sub refresh_breakage_grid()
        DataGridView1.Columns.Clear()
        '--creting the columns for the datagridview--->
        Dim Ob1 As New DataGridViewCheckBoxColumn
        DataGridView1.Columns.Add("BARCODE", "BARCODE")
        DataGridView1.Columns.Add("ITEM NAME", "ITEM NAME")
        DataGridView1.Columns.Add("ML", "ML")
        DataGridView1.Columns.Add("LOOSE", "LOOSE")
        DataGridView1.Columns.Add("BOX", "BOX")
        DataGridView1.Columns.Add("QUANTITY", "QUANTITY")
        DataGridView1.Columns.Add("ITEM CODE", "ITEM CODE")
        DataGridView1.Columns.Add("PACKING", "PACKING")
        DataGridView1.Columns.Add(Ob1)
        DataGridView1.Columns(8).HeaderText = "RECEIVED"
        DataGridView1.Columns(8).Name = "RCVD"
        '---assigning the property of the datagridview--->
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        '---assigning the color for the datagridview--->
        DataGridView1.BackgroundColor = Color.Ivory
        DataGridView1.DefaultCellStyle.BackColor = Color.NavajoWhite
        DataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Ivory
        '---enabeling the visibility of the header--->
        DataGridView1.RowHeadersVisible = False
        DataGridView1.ColumnHeadersVisible = True
        '---hidding some columns of the datgrid view-->
        DataGridView1.Columns(6).Visible = False
        DataGridView1.Columns(7).Visible = False
        '--if the form is opened in the add mode--->
        If Module1.flag = 1 Then
            gettrn()
            ComboBox1.Text = ds1.Tables(0).Rows(0).Item(1)
            ComboBox2.Text = ds2.Tables(0).Rows(0).Item(1)
            TextBox3.Text = Nothing
            '--if the form is opened in the edit mode--->
        ElseIf Module1.flag = 2 Then
            '---filling the controls with the edited vales---->
            TextBox2.Text = Module1.breakage_trn
            TextBox3.Text = Module1.breakage_narration
            ComboBox1.Text = Module1.breakage_store_name
            ComboBox2.Text = Module1.breakage_party_name
            ComboBox3.Text = Module1.breakage_bill_no
            ComboBox4.Text = Module1.breakage_tp_pass_no
            DateTimePicker1.Value = Module1.breakage_trndate
            '---feelimg the dataset--->
            s = "select barcode,itemname,ml,loose,box,quantity,itemmst.itemcode,packing,receivd from breakagedetail join itemmst on itemmst.itemcode=breakagedetail.itemcode and itemmst.companycode=breakagedetail.companycode where breakagedetail.yearcode='" & Module1.yearcode & "' and breakagedetail.companycode='" & Module1.companycode & "' and breakagedetail.trnno='" & TextBox2.Text & "'"
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
            Next
        End If
    End Sub
    '---datagrid view1 key up event--->
    Private Sub DataGridView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyUp
        '-----getting the value for the current row and the current column------->
        Module1.breakage_row = DataGridView1.CurrentCell.RowIndex - 1
        column = DataGridView1.CurrentCell.ColumnIndex
        If e.KeyData = Keys.Enter Then
            '-----checking if the user has entered the itemname or barcode
            If column >= 0 And column <= 1 Then
                s = DataGridView1.Item(column, Module1.breakage_row).Value.ToString
                s = "select itemcode,itemname,barcode,packing,ml from itemmst where barcode ='" & s & "' and companycode='" & Module1.companycode & "' or itemname ='" & s & "' and companycode='" & Module1.companycode & "'"
                dset = ob.populate(s)
                If dset.Tables(0).Rows.Count > 0 Then
                    DataGridView1.Item(0, Module1.breakage_row).Value = dset.Tables(0).Rows(0).Item(2).ToString
                    DataGridView1.Item(1, Module1.breakage_row).Value = dset.Tables(0).Rows(0).Item(1).ToString
                    DataGridView1.Item(2, Module1.breakage_row).Value = dset.Tables(0).Rows(0).Item(4).ToString
                    DataGridView1.Item(3, Module1.breakage_row).Value = 1
                    DataGridView1.Item(4, Module1.breakage_row).Value = 0
                    DataGridView1.Item(5, Module1.breakage_row).Value = 1
                    DataGridView1.Item(6, Module1.breakage_row).Value = dset.Tables(0).Rows(0).Item(0).ToString
                    DataGridView1.Item(7, Module1.breakage_row).Value = dset.Tables(0).Rows(0).Item(3).ToString
                    DataGridView1.Item(8, Module1.breakage_row).Value = 0 '---kepping the default value for the check box false
                Else
                    Dim frm As New frm_breakage_gridsearch
                    s = DataGridView1.Item(column, Module1.breakage_row).Value.ToString
                    frm.TextBox1.Text = s
                    frm.Show()
                End If
                '--checking if the user has changed the loose or the box--->
            ElseIf column >= 3 And column <= 4 Then
                packing = DataGridView1.Item(7, Module1.breakage_row).Value
                loose = DataGridView1.Item(3, Module1.breakage_row).Value
                box = DataGridView1.Item(4, Module1.breakage_row).Value
                quantity = loose + box * packing
                DataGridView1.Item(5, Module1.breakage_row).Value = quantity
            End If
            '----this for deleting that perticular row that on which the user has pressed delete
        ElseIf e.KeyData = Keys.Delete Then
            DataGridView1.Rows.Remove(DataGridView1.CurrentRow)
        End If

    End Sub

    '----event handling when the user presses the save button---->
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        '--checking if any quantity exisists int the grid-->
        Dim sum = 0
        For i = 0 To DataGridView1.RowCount - 2
            sum = sum + DataGridView1.Item(5, i).Value
        Next
        '----allowing the save only when the there are some quantities in the datagridview1--->
        If Not sum = 0 Then
            '---first deleting those datas in the table which mathches the transation number,the companycode and the yearcode--->
            s = "delete from breakagedetail where trnno='" & TextBox2.Text & "' and companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "'"
            ob.insert(s)
            s = "delete from breakagemain where trnno='" & TextBox2.Text & "' and companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "'"
            ob.insert(s)
            For i = 0 To DataGridView1.Rows.Count - 2
                s = "insert into breakagedetail(companycode,trnno                  ,itemcode                                         ,quantity                                         ,receivd                                                    ,yearcode                  ,loose                                            ,box                                              ,trndate) " & _
                      "values('" & Module1.companycode & "','" & TextBox2.Text & "','" & DataGridView1.Item(6, i).Value.ToString & "','" & DataGridView1.Item(5, i).Value.ToString & "','" & Convert.ToBoolean(DataGridView1.Item(8, i).Value) & "','" & Module1.yearcode & "','" & DataGridView1.Item(3, i).Value.ToString & "','" & DataGridView1.Item(4, i).Value.ToString & "','" & DateTimePicker1.Value.Date & "')"
                ob.insert(s)
            Next
            s = "insert into breakagemain(companycode,yearcode                  ,trnno                  ,narration              ,shopcode                     ,trndate                             ,ledgercode                   ,billno                ,tp_pass_no) " & _
                "values('" & Module1.companycode & "','" & Module1.yearcode & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & breakage_store_code & "','" & DateTimePicker1.Value.Date & "','" & breakage_ledgercode & "'," & ComboBox3.Text & "," & ComboBox4.Text & ")"
            ob.insert(s)
            '-----code for refreshing the form1 and the breakage form-->
            frm_MainForm.mainformload()
            If Module1.flag = 1 Then
                ds.Tables.Clear()
                refresh_breakage_grid()
                DataGridView1.Select()
            ElseIf Module1.flag = 2 Then
                Me.Close()
            End If
            '---msg to user to put some quantity in the datagridview1-->
        Else
            MsgBox("You cannot save a with zero quantity")
        End If
    End Sub
    '---event handling when the user presses the close button--->
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub






End Class