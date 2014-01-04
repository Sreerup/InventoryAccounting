Public Class frm_purchase
    Dim dsled As New DataSet
    Dim dssupp As New DataSet
    Dim dsstore As New DataSet
    Dim dsscheme As New DataSet
    Dim dstax As New DataSet
    Dim dsitem As New DataSet
    Dim dsbill As New DataSet
    Dim dsedit As New DataSet

    Dim pledcode As String
    Dim pstorecode As String
    Dim psuppledcode As String
    Dim pschemecode As String
    Dim pitemcode As String
    Dim type As String

    Dim purreturn As Boolean
    Dim editmode As Boolean
    Dim taxctrl As Boolean
    Dim billcheck As Boolean

    Dim r As Integer
    Dim c As Integer
    Dim irate() As Double
    Dim ibox() As Integer
    Dim iloose() As Integer
    Dim ipacking() As Integer
    Dim iquantity() As Integer
    Dim iamount() As Double
    Dim taxcount As Integer = 0
    Dim round As Double = 0

    Dim baselist() As String
    Dim ratelist() As Double
    Dim amountlist() As Double
    Dim glist() As Double

    Dim lbl1() As Label
    Dim lbl2() As Label
    Dim txt1() As TextBox
    Dim txt2() As TextBox
    Dim itemname As New DataGridViewComboBoxColumn
    Dim dgvcombo As New ComboBox
    Dim s As String
    Dim ob As New Class1

    Private Sub purchase_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DateTimePicker1.MaxDate = Date.Today

        taxctrl = False
        editmode = False

        s = "select ledcode, name from ledger where companycode='" & Module1.companycode & "' order by name"
        dsled = ob.populate(s)
        s = "select ledcode, name from ledger join acountname on acountname.accode=ledger.accode where ledger.companycode='" & Module1.companycode & "' and acountname.acname like '%SUPPLIERS' or ledger.companycode='" & Module1.companycode & "' and acountname.acname like '%CREDITORS' order by name"
        dssupp = ob.populate(s)
        s = "select shopcode,shopname from storage where companycode='" & Module1.companycode & "' order by shopname"
        dsstore = ob.populate(s)
        s = "select distinct schemecode,schemename from taxdetail where companycode='" & Module1.companycode & "' order by schemename"
        dsscheme = ob.populate(s)
        s = "select itemcode,itemname,packing from itemmst where companycode='" & Module1.companycode & "' order by itemname"
        dsitem = ob.populate(s)
        If Not dsled.Tables(0).Rows.Count > 0 Or Not dssupp.Tables(0).Rows.Count > 0 Or Not dsstore.Tables(0).Rows.Count > 0 Or Not dsitem.Tables(0).Rows.Count > 0 Then
            MsgBox("Masters Missing please check the related masters", MsgBoxStyle.Exclamation, "Master MIssings")
            Module1.flag = 0
            Exit Sub
        End If

        ob.combofill(dsled, cmb_puchaseacc)
        ob.combofill(dssupp, cmb_supplier)
        ob.combofill(dsstore, cmb_storage)
        ob.combofill(dsscheme, cmb_scheme)
        RadioButton1.Checked = True

    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        purreturn = False
        type = "PURCHASE"
        cmb_billnl.Visible = False
        DateTimePicker2.Enabled = True
        cmb_storage.Enabled = True
        txt_tppass.Enabled = True
        cmb_scheme.Enabled = True
        For i = 0 To dsled.Tables(0).Rows.Count - 1
            If Module1.compuracc = dsled.Tables(0).Rows(i).Item(0) Then
                cmb_puchaseacc.SelectedItem = dsled.Tables(0).Rows(i).Item(1).ToString
                Exit For
            End If
        Next
        If Module1.flag = 1 Then
            formload()
        ElseIf Module1.flag = 2 Then
            editload()
        End If
        cmb_supplier.Select()
    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        purreturn = True
        type = "RETURN"
        cmbbilload()
        cmb_billnl.Visible = True
        DateTimePicker2.Enabled = False
        cmb_storage.Enabled = False
        txt_tppass.Enabled = False
        cmb_scheme.Enabled = False
        cmb_supplier.Select()
    End Sub

    Private Sub formload()
        griddesign()
        For i = 0 To dsstore.Tables(0).Rows.Count - 1
            If Module1.comdefstore = dsstore.Tables(0).Rows(i).Item(0) Then
                cmb_storage.SelectedItem = dsstore.Tables(0).Rows(i).Item(1).ToString
                Exit For
            End If
        Next
        If dsscheme.Tables(0).Rows.Count > 0 Then
            cmb_scheme.SelectedIndex = 0
        End If
        DateTimePicker2.MaxDate = DateTimePicker1.Value
        cmb_supplier.SelectedIndex = 0
        If frm_MainForm.dsload.Tables(0).Rows.Count > 0 Then
            txt_trnno.Text = frm_MainForm.dsload.Tables(0).Rows(0).Item(0) + 1
        Else
            txt_trnno.Text = "1"
        End If
        'gettrn()
        txt_rounding_plus.Text = "0.00"
        txt_tppass.Text = ""
        txt_billno.Text = ""
        txt_paid.Text = "0.00"
        txt_narration.Text = ""
        txt_gross.Text = "0.00"
        txt_rounding_minus.Text = "0.00"
        txt_net.Text = "0.00"
        txt_due.Text = "0.00"
        cmb_supplier.Select()
        cmbbilload()
    End Sub


    Private Sub editload()
        RadioButton1.Visible = False
        RadioButton2.Visible = False
        griddesign()
        Dim s As String
        Dim ob As New Class1
        Dim dsedit As New DataSet
        s = "select * from purchasemain where companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "' and trnno='" & Module1.col1 & "'"
        dsedit = ob.populate(s)
        txt_trnno.Text = dsedit.Tables(0).Rows(0).Item(3).ToString
        txt_billno.Text = dsedit.Tables(0).Rows(0).Item(9).ToString
        txt_tppass.Text = dsedit.Tables(0).Rows(0).Item(11).ToString
        txt_narration.Text = dsedit.Tables(0).Rows(0).Item(16).ToString
        txt_gross.Text = dsedit.Tables(0).Rows(0).Item(13).ToString
        DateTimePicker1.Value = dsedit.Tables(0).Rows(0).Item(4).ToString
        DateTimePicker2.Value = dsedit.Tables(0).Rows(0).Item(10).ToString
        For i = 0 To dsstore.Tables(0).Rows.Count - 1
            If dsedit.Tables(0).Rows(0).Item(8).ToString = dsstore.Tables(0).Rows(i).Item(0) Then
                cmb_storage.SelectedItem = dsstore.Tables(0).Rows(i).Item(1).ToString
                Exit For
            End If
        Next
        For i = 0 To dssupp.Tables(0).Rows.Count - 1

            If dsedit.Tables(0).Rows(0).Item(7).ToString = dssupp.Tables(0).Rows(i).Item(0).ToString Then
                cmb_supplier.SelectedItem = dssupp.Tables(0).Rows(i).Item(1).ToString
                Exit For
            End If
        Next
        For i = 0 To dsscheme.Tables(0).Rows.Count - 1
            If dsedit.Tables(0).Rows(0).Item(12).ToString = dsscheme.Tables(0).Rows(i).Item(0).ToString Then
                cmb_scheme.Text = dsscheme.Tables(0).Rows(i).Item(1).ToString
                Exit For
            End If
        Next
        editset()
    End Sub


    Private Sub griddesign()
        dgv1.Columns.Clear()
        With dgv1
            .Columns.Clear()
            .Columns.Add("slno", "Sl.No.")
            itemname.Items.Clear()
            itemname.HeaderText = "Item Name"
            If dsitem.Tables(0).Rows.Count > 0 Then
                For i = 0 To dsitem.Tables(0).Rows.Count - 1
                    itemname.Items.Add(dsitem.Tables(0).Rows(i).Item(1).ToString)
                Next
            End If
            itemname.AutoComplete = System.Windows.Forms.AutoCompleteMode.Suggest
            .Columns.Add(itemname)
            .Columns.Add("itemcode", "itemcode")
            .Columns.Add("batch", "Batch No")
            .Columns.Add("box", "Box")
            .Columns.Add("loose", "Loose")
            .Columns.Add("quantity", "Quantity")
            .Columns.Add("rate", "Rate")
            .Columns.Add("amount", "Amount")
            If Module1.flag = 2 Then
                .Columns(0).HeaderText = "Sl.No."
                .Columns(1).HeaderText = "Item Name"
                .Columns(3).HeaderText = "Batch No"
                .Columns(4).HeaderText = "Box"
                .Columns(5).HeaderText = "Loose"
                .Columns(6).HeaderText = "Quantity"
                .Columns(7).HeaderText = "Rate"
                .Columns(8).HeaderText = "Amount"
            End If
            .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            .Columns(2).Visible = False
            .Columns(0).Width = 40
            .Columns(1).Width = 280
            .Columns(3).Width = 90
            .Columns(4).Width = 45
            .Columns(5).Width = 45
            .Columns(6).Width = 60
            .Columns(7).Width = 80
            .Columns(8).Width = 90
            If purreturn = True Then
                .Columns(0).ReadOnly = True
                .Columns(1).ReadOnly = False
                .Columns(2).ReadOnly = False
                .Columns(3).ReadOnly = False
                .Columns(7).ReadOnly = False
            End If
            .Columns(6).ReadOnly = True
        End With
    End Sub

    Private Sub editset()
        Dim s As String
        Dim ob As New Class1
        If purreturn = False Then
            s = "select slno,itemmst.itemname,purchasedetail.itemcode,batchno,itembox,itemloose,itemquantity,itemrate,itemamount,itemmst.packing, purchasedetail.trnno from purchasedetail join itemmst on itemmst.itemcode=purchasedetail.itemcode and itemmst.companycode=purchasedetail.companycode where purchasedetail. companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "' and trnno='" & Module1.col1 & "' order by slno"
        Else
            s = "select slno,itemmst.itemname,purchasedetail.itemcode,batchno,itembox,itemloose,itemquantity,itemrate,itemamount,itemmst.packing, purchasedetail.trnno from purchasedetail join itemmst on itemmst.itemcode=purchasedetail.itemcode and itemmst.companycode=purchasedetail.companycode where purchasedetail. companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "' and trnno='" & dsbill.Tables(0).Rows(cmb_billnl.SelectedIndex).Item(3) & "' order by slno"
        End If
        dsedit = ob.populate(s)
        dgvcombo.Items.Clear()
        If dsedit.Tables(0).Rows.Count > 0 Then
            ReDim irate(dsedit.Tables(0).Rows.Count)
            ReDim ibox(dsedit.Tables(0).Rows.Count)
            ReDim ipacking(dsedit.Tables(0).Rows.Count)
            ReDim iloose(dsedit.Tables(0).Rows.Count)
            ReDim iquantity(dsedit.Tables(0).Rows.Count)
            ReDim iamount(dsedit.Tables(0).Rows.Count)
            Dim rate_var As Double
            Dim amount_var As Double

            For i = 0 To dsedit.Tables(0).Rows.Count - 1
                If dsedit.Tables(0).Rows(i).Item(7).ToString <> "" Then
                    rate_var = dsedit.Tables(0).Rows(i).Item(7)
                Else
                    rate_var = 0
                End If

                If dsedit.Tables(0).Rows(i).Item(8).ToString <> "" Then
                    amount_var = dsedit.Tables(0).Rows(i).Item(8)
                Else
                    amount_var = 0
                End If

                dgv1.Rows.Add(dsedit.Tables(0).Rows(i).Item(0), dsedit.Tables(0).Rows(i).Item(1), dsedit.Tables(0).Rows(i).Item(2), dsedit.Tables(0).Rows(i).Item(3), dsedit.Tables(0).Rows(i).Item(4), dsedit.Tables(0).Rows(i).Item(5), dsedit.Tables(0).Rows(i).Item(6), rate_var, amount_var)
                If dsedit.Tables(0).Rows(i).Item(7).ToString <> Nothing Then
                    irate(i) = dsedit.Tables(0).Rows(i).Item(7)
                Else
                    irate(i) = 0
                End If
                ibox(i) = dsedit.Tables(0).Rows(i).Item(4)
                iloose(i) = dsedit.Tables(0).Rows(i).Item(5)
                iquantity(i) = dsedit.Tables(0).Rows(i).Item(6)
                If dsedit.Tables(0).Rows(i).Item(8).ToString <> Nothing Then
                    iamount(i) = dsedit.Tables(0).Rows(i).Item(8)
                Else
                    iamount(i) = 0
                End If
                ipacking(i) = dsedit.Tables(0).Rows(i).Item(9)
            Next
            If purreturn = True Then
                dgv1.Columns(0).ReadOnly = True
                dgv1.Columns(1).ReadOnly = True
                dgv1.Columns(2).ReadOnly = True
                dgv1.Columns(3).ReadOnly = True
                dgv1.Columns(7).ReadOnly = True
            End If
            Dim dstaxedit As New DataSet
            s = "select * from purchasetaxdetail where companycode='" & Module1.companycode & "'and yearcode='" & Module1.yearcode & "'and trnno='" & Module1.col1 & "'"
            dstaxedit = ob.populate(s)
            If dstaxedit.Tables(0).Rows.Count > 0 Then
                ReDim ratelist(dstaxedit.Tables(0).Rows.Count - 1)
                ReDim amountlist(dstaxedit.Tables(0).Rows.Count - 1)
                ReDim Preserve glist(dstaxedit.Tables(0).Rows.Count)
                For i = 0 To dstaxedit.Tables(0).Rows.Count - 1
                    Try
                        txt1(i).Text = dstaxedit.Tables(0).Rows(i).Item(4)
                        ratelist(i) = dstaxedit.Tables(0).Rows(i).Item(4)
                        glist(i) = dstaxedit.Tables(0).Rows(i).Item(5)
                        txt2(i).Text = dstaxedit.Tables(0).Rows(i).Item(6)
                        amountlist(i) = dstaxedit.Tables(0).Rows(i).Item(4)
                    Catch ex As IndexOutOfRangeException
                    End Try
                Next
                gridcalc()
            End If
        End If
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_trnno.KeyPress
        e.Handled = True
    End Sub

    Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_trnno.KeyDown
        If e.KeyData = Keys.Enter Then
            cmb_puchaseacc.Select()
        End If
        If e.KeyCode = Keys.Delete Then
            e.SuppressKeyPress = True
        End If
    End Sub
    Private Sub ComboBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmb_puchaseacc.KeyPress
        e.Handled = True
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmb_puchaseacc.SelectedIndexChanged
        If cmb_puchaseacc.SelectedIndex >= 0 Then
            pledcode = dsled.Tables(0).Rows(cmb_puchaseacc.SelectedIndex).Item(0).ToString
        End If
    End Sub
    Private Sub ComboBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmb_puchaseacc.KeyDown
        If e.KeyData = Keys.Enter Then
            e.SuppressKeyPress = True
            cmb_supplier.Select()
        End If
    End Sub
    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmb_storage.SelectedIndexChanged
        If dsstore.Tables(0).Rows.Count > 0 Then
            pstorecode = dsstore.Tables(0).Rows(cmb_storage.SelectedIndex).Item(0).ToString
        End If
    End Sub
    Private Sub ComboBox2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmb_storage.KeyDown
        If e.KeyData = Keys.Enter Then
            If cmb_storage.SelectedIndex >= 0 Then
                DateTimePicker1.Select()
            End If
        End If
        If e.KeyData = Keys.Down Then
            cmb_storage.DroppedDown = True
        End If
    End Sub
    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmb_supplier.SelectedIndexChanged
        If cmb_supplier.SelectedIndex >= 0 Then
            psuppledcode = dssupp.Tables(0).Rows(cmb_supplier.SelectedIndex).Item(0).ToString
            cmbbilload()
        End If
    End Sub
    Private Sub ComboBox3_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmb_supplier.KeyDown
        If e.KeyData = Keys.Enter Then
            If cmb_supplier.SelectedIndex >= 0 Then
                If purreturn = True Then
                    cmb_billnl.Select()
                Else
                    cmb_storage.Select()
                End If
            End If
        End If
        If e.KeyData = Keys.Down Then
            cmb_supplier.DroppedDown = True
        End If
    End Sub
    Private Sub cmbbilload()
        cmb_billnl.Items.Clear()
        Dim s As String
        Dim ob As New Class1
        s = "select * from purchasemain where companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "' and suppliercode='" & psuppledcode & "' and ptype='PURCHASE'"
        dsbill = ob.populate(s)
        If dsbill.Tables(0).Rows.Count > 0 Then
            For i = 0 To dsbill.Tables(0).Rows.Count - 1
                cmb_billnl.Items.Add(dsbill.Tables(0).Rows(i).Item(9))
            Next
        End If
    End Sub
    Private Sub DateTimePicker1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker1.ValueChanged
        DateTimePicker2.MaxDate = DateTimePicker1.Value
    End Sub
    Private Sub DateTimePicker1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DateTimePicker1.KeyDown
        If e.KeyData = Keys.Enter Then
            DateTimePicker2.Select()
        End If
    End Sub
    Private Sub DateTimePicker2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DateTimePicker2.KeyDown
        If e.KeyData = Keys.Enter Then
            txt_billno.Select()
        End If
    End Sub
    Private Sub TextBox4_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_billno.KeyDown
        If e.KeyData = Keys.Enter And txt_billno.Text <> "" Then
            billduplicate()
            If billcheck = True Then
                txt_billno.Select()
                Exit Sub
            Else
                txt_tppass.Select()
            End If
        End If
    End Sub
    Private Sub billduplicate()
        If dsbill.Tables(0).Rows.Count > 0 Then
            For i = 0 To dsbill.Tables(0).Rows.Count - 1
                If Module1.flag = 1 Then
                    If txt_billno.Text.ToUpper = dsbill.Tables(0).Rows(i).Item(9) Then
                        MsgBox("Bill no already present from this supplier.", MsgBoxStyle.Exclamation, "Duplicate")
                        billcheck = True
                    Else
                        billcheck = False
                    End If
                ElseIf Module1.flag = 2 Or purreturn = True Then
                    If txt_billno.Text.ToUpper = dsbill.Tables(0).Rows(i).Item(9) And txt_billno.Text.ToUpper <> Module1.col2 Then
                        MsgBox("Bill no already present from this supplier.", MsgBoxStyle.Exclamation, "Duplicate")
                        billcheck = True
                    Else
                        billcheck = False
                    End If
                End If
            Next
        Else
            billcheck = False
        End If
    End Sub
    Private Sub TextBox3_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_tppass.KeyDown
        If e.KeyData = Keys.Enter Then
            dgv1.Select()
            dgv1.ClearSelection()
            dgv1.CurrentCell = dgv1.Item(1, 0)
            dgv1.CurrentCell.Selected = True
        End If
    End Sub

    Private Sub ComboBox5_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmb_billnl.SelectedIndexChanged
        If cmb_billnl.SelectedIndex >= 0 Then
            dgv1.Enabled = True
            sereturn()
        Else
            MsgBox("There is no purchase bill from the selected supplier.", MsgBoxStyle.Exclamation, "No Bill")
            dgv1.Enabled = False
        End If
    End Sub
    Private Sub ComboBox5_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmb_billnl.KeyDown
        If e.KeyData = Keys.Enter Then
            If cmb_billnl.SelectedIndex >= 0 Then
                dgv1.Select()
                dgv1.ClearSelection()
                dgv1.CurrentCell = dgv1.Item(1, 0)
                dgv1.CurrentCell.Selected = True
            End If
        End If
        If e.KeyData = Keys.Down Then
            cmb_billnl.DroppedDown = True
        End If
    End Sub


    Private Sub sereturn()
        txt_tppass.Text = dsbill.Tables(0).Rows(cmb_billnl.SelectedIndex).Item(11).ToString
        txt_gross.Text = dsbill.Tables(0).Rows(cmb_billnl.SelectedIndex).Item(13).ToString
        txt_net.Text = dsbill.Tables(0).Rows(cmb_billnl.SelectedIndex).Item(15).ToString
        DateTimePicker1.Value = dsbill.Tables(0).Rows(cmb_billnl.SelectedIndex).Item(4)
        DateTimePicker2.Value = dsbill.Tables(0).Rows(cmb_billnl.SelectedIndex).Item(10)
        For i = 0 To dsstore.Tables(0).Rows.Count - 1
            If dsbill.Tables(0).Rows(cmb_billnl.SelectedIndex).Item(8) = dsstore.Tables(0).Rows(i).Item(0) Then
                cmb_storage.SelectedItem = dsstore.Tables(0).Rows(i).Item(1).ToString
                Exit For
            End If
        Next
        For i = 0 To dsscheme.Tables(0).Rows.Count - 1
            If dsbill.Tables(0).Rows(cmb_billnl.SelectedIndex).Item(12) = dsscheme.Tables(0).Rows(i).Item(0) Then
                cmb_scheme.SelectedItem = dsscheme.Tables(0).Rows(i).Item(1).ToString
                Exit For
            End If
        Next
        griddesign()
        editset()
    End Sub



    Private Sub dgv1_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgv1.EditingControlShowing
        editmode = True
        r = dgv1.CurrentCell.RowIndex
        c = dgv1.CurrentCell.ColumnIndex
        dgv1.Item(0, r).Value = ""
        dgv1.Item(0, r).Value = dgv1.CurrentCell.RowIndex + 1
        If c = 1 Then
            dgvcombo = e.Control
            dgvcombo.AutoCompleteMode = AutoCompleteMode.Suggest
            dgvcombo.AutoCompleteSource = AutoCompleteSource.ListItems
            dgvcombo.DropDownStyle = ComboBoxStyle.DropDownList
            RemoveHandler dgvcombo.DropDownClosed, AddressOf Me.dgvcombo_DropDownClosed
            RemoveHandler dgvcombo.KeyDown, AddressOf Me.dgvcombo_KeyDown
            AddHandler dgvcombo.DropDownClosed, AddressOf Me.dgvcombo_DropDownClosed
            AddHandler dgvcombo.KeyDown, AddressOf Me.dgvcombo_KeyDown
            If purreturn = False Then
                ReDim Preserve irate(dgv1.Rows.Count)
                ReDim Preserve ibox(dgv1.Rows.Count)
                ReDim Preserve ipacking(dgv1.Rows.Count)
                ReDim Preserve iloose(dgv1.Rows.Count)
                ReDim Preserve iquantity(dgv1.Rows.Count)
                ReDim Preserve iamount(dgv1.Rows.Count)
            End If
        Else
            Dim dgvtxt As TextBox = CType(e.Control, TextBox)
            RemoveHandler dgvtxt.KeyPress, AddressOf Me.TextBox_KeyPress
            AddHandler dgvtxt.KeyPress, AddressOf Me.TextBox_KeyPress
        End If
    End Sub
    Private Sub dgvcombo_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyData = Keys.Enter Then
            If dgvcombo.SelectedIndex >= 0 And c = 1 Then
                dgv1.ClearSelection()
                dgv1.CurrentCell = dgv1.Item(3, r)
                dgv1.CurrentCell.Selected = True
            End If
        End If
        If e.KeyData = Keys.Down Then
            dgvcombo.DroppedDown = True
        End If
    End Sub

    Private Sub dgvcombo_DropDownClosed(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If dsitem.Tables(0).Rows.Count > 0 And dgvcombo.SelectedIndex >= 0 Then
            dgv1.Item(2, r).Value = dsitem.Tables(0).Rows(dgvcombo.SelectedIndex).Item(0)
            dgv1.Item(1, r).Value = dsitem.Tables(0).Rows(dgvcombo.SelectedIndex).Item(1)
            ipacking(r) = dsitem.Tables(0).Rows(dgvcombo.SelectedIndex).Item(2)

            Dim s As String
            Dim ob As New Class1
            Dim dsp As New DataSet
            If dsp.Tables.Count > 0 Then
                dsp.Tables(0).Dispose()
            End If
            If c = 1 Then
                s = "select top 1 itemrate from purchasedetail where itemcode='" & dgv1.Item(2, r).Value & "' and companycode='" & Module1.companycode & "' order by trnno desc"
                dsp = ob.populate(s)
                If dsp.Tables(0).Rows.Count > 0 Then
                    If dsp.Tables(0).Rows(0).Item(0).ToString <> "" Then
                        irate(r) = dsp.Tables(0).Rows(0).Item(0)
                    End If
                Else
                    irate(r) = 0
                End If
                dgv1.Item(7, r).Value = irate(r)
            End If
            gridcalc()
        End If
    End Sub

    Private Sub TextBox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If c <> 6 Then
            If c > 3 Then
                If c = 7 Or c = 8 Then
                    Select Case e.KeyChar
                        Case "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", ".", vbBack
                        Case Else
                            e.Handled = True
                    End Select
                End If
                If c = 4 Or c = 5 Then
                    Select Case e.KeyChar
                        Case "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", vbBack
                        Case Else
                            e.Handled = True
                    End Select
                End If
            End If
        Else
            e.Handled = True
        End If
    End Sub
    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        Dim KeyCode As Integer = CType(keyData, Integer)
        Dim Key As Keys = CType(KeyCode, Keys)
        If ActiveControl.GetType Is GetType(DataGridViewTextBoxEditingControl) Then
            If Key = Keys.Return OrElse Key = Keys.Enter Then
                If c > 8 And c <> 1 Then
                    dgv1.CurrentCell = dgv1.Rows(0).Cells(1) 'Manual tab
                Else : SendKeys.SendWait("{TAB}") 'tab automatically. 
                End If
                Return True
            End If
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function
    Private Sub dgv1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgv1.KeyDown
        For i = 0 To dgv1.Rows.Count - 2
            dgv1.Item(0, i).Value = ""
            dgv1.Item(0, i).Value = i + 1
        Next
        If e.KeyData = Keys.Enter Then
            r = dgv1.CurrentCell.RowIndex
            c = dgv1.CurrentCell.ColumnIndex
            If c = 1 Then
                If dgv1.Rows.Count > 1 And dgv1.Item(1, r).Value = "" Then
                    cmb_scheme.Select()
                End If
            End If
            e.SuppressKeyPress = True
            dgv1.ClearSelection()
        End If
        If e.KeyData = Keys.Up And dgv1.NewRowIndex = r Then
            dgv1.Item(0, dgv1.NewRowIndex).Value = ""
        End If

        If e.KeyData = Keys.Delete Then
            If dgv1.NewRowIndex <> dgv1.CurrentCell.RowIndex Then
                dgv1.Rows.Remove(dgv1.CurrentRow)
deleted:
                For i = r To dgv1.Rows.Count - 1
                    irate(i) = irate(i + 1)
                    ibox(i) = ibox(i + 1)
                    iloose(i) = iloose(i + 1)
                    ipacking(i) = ipacking(i + 1)
                    iquantity(i) = iquantity(i + 1)
                    iamount(i) = iamount(i + 1)
                Next
                ReDim Preserve irate(dgv1.Rows.Count)
                ReDim Preserve ibox(dgv1.Rows.Count)
                ReDim Preserve iloose(dgv1.Rows.Count)
                ReDim Preserve ipacking(dgv1.Rows.Count)
                ReDim Preserve iquantity(dgv1.Rows.Count)
                ReDim Preserve iamount(dgv1.Rows.Count)
                'gridcalc()
                Exit Sub
            Else
                For i = 0 To 8
                    dgv1.Item(i, dgv1.CurrentCell.RowIndex).Value = ""
                Next
                If purreturn = True Or Module1.flag = 2 Then
                    GoTo deleted
                End If
            End If
        End If
    End Sub

    Private Sub dgv1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgv1.KeyUp
        If e.KeyData = Keys.Enter Or e.KeyData = Keys.Down Or e.KeyData = Keys.Left Or e.KeyData = Keys.Right Then
            If editmode = True Then
                editmode = False
                ReDim Preserve irate(dgv1.Rows.Count)
                ReDim Preserve ibox(dgv1.Rows.Count)
                ReDim Preserve ipacking(dgv1.Rows.Count)
                ReDim Preserve iloose(dgv1.Rows.Count)
                ReDim Preserve iquantity(dgv1.Rows.Count)
                ReDim Preserve iamount(dgv1.Rows.Count)
                gridcalc()
            End If
        End If
        If e.KeyData = Keys.Enter Then
            If dgv1.Rows.Count > 1 Then
                If c < 8 Then
                    If c = 1 Then
                        If String.IsNullOrEmpty(CStr(Me.dgv1.Rows(r).Cells(1).Value)) Then
                            If taxcount > 0 Then
                                cmb_scheme.Select()
                            Else
                                Button1.Select()
                            End If
                        End If
                        dgv1.CurrentCell = dgv1.Item(c + 2, r)
                    Else
                        dgv1.CurrentCell = dgv1.Item(c + 1, r)
                    End If
                Else
                    Try
                        dgv1.CurrentCell = dgv1.Item(1, r + 1)
                    Catch ex As Exception
                    End Try
                End If
            End If
            dgv1.CurrentCell.Selected = True
        End If
    End Sub
    Private Sub gridcalc()
        txt_gross.Text = "0.00"
        If c = 4 Then
            ibox(r) = dgv1.Item(4, r).Value
        End If
        If c = 5 Then
            iloose(r) = dgv1.Item(5, r).Value
        End If
        iquantity(r) = (ibox(r) * ipacking(r)) + iloose(r)
        dgv1.Item(6, r).Value = iquantity(r)
        If c = 7 Or c = 4 Or c = 5 Then
            irate(r) = dgv1.Item(7, r).Value
            iamount(r) = Format(irate(r) * iquantity(r), "0.00")
            dgv1.Item(8, r).Value = iamount(r)
        End If
        If c = 8 Or c = 4 Or c = 5 Then
            iamount(r) = dgv1.Item(8, r).Value
            irate(r) = Format(iamount(r) / iquantity(r), "0.00")
            dgv1.Item(7, r).Value = irate(r)
        End If
        For i = 0 To dgv1.Rows.Count - 1
            'If r < dgv1.NewRowIndex And Val(dgv1.Item(8, i).Value) > 0 Then
            txt_gross.Text = Format((Val(txt_gross.Text) + dgv1.Item(8, i).Value), "0.00")
            'End If
        Next
        taxcalc()
    End Sub
    Private Sub ComboBox4_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmb_scheme.KeyDown
        If e.KeyData = Keys.Enter Then
            If cmb_scheme.SelectedIndex >= 0 Then
                txt1(0).Select()
            Else
                Button1.Select()
            End If
        End If
        If e.KeyData = Keys.Down Then
            cmb_scheme.DroppedDown = True
        End If
    End Sub

    Private Sub ComboBox4_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmb_scheme.SelectedIndexChanged
        If dsscheme.Tables(0).Rows.Count > 0 Then
            pschemecode = dsscheme.Tables(0).Rows(cmb_scheme.SelectedIndex).Item(0).ToString
            taxset()
        End If
    End Sub


    Private Sub taxset()
        If taxctrl = True Then
            Panel1.Controls.Clear()
            taxctrl = False
        End If
        If taxctrl = False Then
            Dim s As String
            Dim ob As New Class1
            s = "select * from taxdetail where companycode='" & Module1.companycode & "' and schemecode='" & pschemecode & "'"
            dstax = ob.populate(s)
            ReDim Preserve ratelist(dstax.Tables(0).Rows.Count - 1)
            ReDim Preserve amountlist(dstax.Tables(0).Rows.Count - 1)
            ReDim Preserve baselist(dstax.Tables(0).Rows.Count - 1)
            ReDim Preserve glist(dstax.Tables(0).Rows.Count)

            If dstax.Tables(0).Rows.Count > 0 Then
                Dim top As Integer
                top = 0
                ReDim lbl1(dstax.Tables(0).Rows.Count - 1)
                ReDim lbl2(dstax.Tables(0).Rows.Count - 1)
                ReDim txt1(dstax.Tables(0).Rows.Count - 1)
                ReDim txt2(dstax.Tables(0).Rows.Count - 1)

                For i = 0 To dstax.Tables(0).Rows.Count - 1
                    ratelist(i) = Format(dstax.Tables(0).Rows(i).Item(8), "0.00")
                    amountlist(i) = Format(dstax.Tables(0).Rows(i).Item(9), "0.00")
                    baselist(i) = Format(dstax.Tables(0).Rows(i).Item(10).ToString, "0.00")

                    lbl1(i) = New System.Windows.Forms.Label
                    Panel1.Controls.Add(lbl1(i))
                    lbl1(i).BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
                    lbl1(i).Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                    lbl1(i).Location = New System.Drawing.Point(0, top)
                    lbl1(i).Size = New System.Drawing.Size(106, 20)
                    lbl1(i).TabIndex = 106
                    lbl1(i).TextAlign = System.Drawing.ContentAlignment.MiddleLeft
                    lbl1(i).Text = dstax.Tables(0).Rows(i).Item(5).ToString
                    lbl1(i).Name = "lbl1" + i.ToString

                    lbl2(i) = New System.Windows.Forms.Label
                    Panel1.Controls.Add(lbl2(i))
                    lbl2(i).BorderStyle = System.Windows.Forms.BorderStyle.None
                    lbl2(i).Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                    lbl2(i).Location = New System.Drawing.Point(180, top)
                    lbl2(i).Size = New System.Drawing.Size(15, 20)
                    lbl2(i).TabIndex = 106
                    lbl2(i).TextAlign = System.Drawing.ContentAlignment.MiddleLeft
                    lbl2(i).Text = "%"

                    txt1(i) = New System.Windows.Forms.TextBox
                    Panel1.Controls.Add(txt1(i))
                    txt1(i).Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                    txt1(i).Location = New System.Drawing.Point(109, top)
                    txt1(i).Size = New System.Drawing.Size(70, 19)
                    txt1(i).TabIndex = 52
                    txt1(i).TextAlign = System.Windows.Forms.HorizontalAlignment.Right
                    txt1(i).Text = Format(dstax.Tables(0).Rows(i).Item(8), "0.00")
                    txt1(i).BackColor = System.Drawing.SystemColors.ActiveBorder
                    txt1(i).Name = "txt1" + i.ToString

                    txt2(i) = New System.Windows.Forms.TextBox
                    Panel1.Controls.Add(txt2(i))
                    txt2(i).Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                    txt2(i).Location = New System.Drawing.Point(200, top)
                    txt2(i).Size = New System.Drawing.Size(100, 19)
                    txt2(i).TabIndex = 52
                    txt2(i).TextAlign = System.Windows.Forms.HorizontalAlignment.Right
                    txt2(i).Text = Format(dstax.Tables(0).Rows(i).Item(9), "0.00")
                    txt2(i).BackColor = System.Drawing.SystemColors.ActiveBorder
                    txt2(i).Name = "txt2" + i.ToString
                    top = top + 21
                    taxcount = i + 1
                    If i = 0 Then
                        RemoveHandler txt1(i).KeyPress, AddressOf Me.txtrate_KeyPress0
                        AddHandler txt1(i).KeyPress, AddressOf Me.txtrate_KeyPress0
                        RemoveHandler txt2(i).KeyPress, AddressOf Me.txtamount_KeyPress0
                        AddHandler txt2(i).KeyPress, AddressOf Me.txtamount_KeyPress0
                        RemoveHandler txt1(i).KeyDown, AddressOf Me.txtrate_KeyDown0
                        AddHandler txt1(i).KeyDown, AddressOf Me.txtrate_KeyDown0
                        RemoveHandler txt2(i).KeyDown, AddressOf Me.txtamount_KeyDown0
                        AddHandler txt2(i).KeyDown, AddressOf Me.txtamount_KeyDown0
                    End If
                    If i = 1 Then
                        RemoveHandler txt1(i).KeyPress, AddressOf Me.txtrate_KeyPress1
                        AddHandler txt1(i).KeyPress, AddressOf Me.txtrate_KeyPress1
                        RemoveHandler txt2(i).KeyPress, AddressOf Me.txtamount_KeyPress1
                        AddHandler txt2(i).KeyPress, AddressOf Me.txtamount_KeyPress1
                        RemoveHandler txt1(i).KeyDown, AddressOf Me.txtrate_KeyDown1
                        AddHandler txt1(i).KeyDown, AddressOf Me.txtrate_KeyDown1
                        RemoveHandler txt2(i).KeyDown, AddressOf Me.txtamount_KeyDown1
                        AddHandler txt2(i).KeyDown, AddressOf Me.txtamount_KeyDown1
                    End If
                    If i = 2 Then
                        RemoveHandler txt1(i).KeyPress, AddressOf Me.txtrate_KeyPress2
                        AddHandler txt1(i).KeyPress, AddressOf Me.txtrate_KeyPress2
                        RemoveHandler txt2(i).KeyPress, AddressOf Me.txtamount_KeyPress2
                        AddHandler txt2(i).KeyPress, AddressOf Me.txtamount_KeyPress2
                        RemoveHandler txt1(i).KeyDown, AddressOf Me.txtrate_KeyDown2
                        AddHandler txt1(i).KeyDown, AddressOf Me.txtrate_KeyDown2
                        RemoveHandler txt2(i).KeyDown, AddressOf Me.txtamount_KeyDown2
                        AddHandler txt2(i).KeyDown, AddressOf Me.txtamount_KeyDown2
                    End If
                    If i = 3 Then
                        RemoveHandler txt1(i).KeyPress, AddressOf Me.txtrate_KeyPress3
                        AddHandler txt1(i).KeyPress, AddressOf Me.txtrate_KeyPress3
                        RemoveHandler txt2(i).KeyPress, AddressOf Me.txtamount_KeyPress3
                        AddHandler txt2(i).KeyPress, AddressOf Me.txtamount_KeyPress3
                        RemoveHandler txt1(i).KeyDown, AddressOf Me.txtrate_KeyDown3
                        AddHandler txt1(i).KeyDown, AddressOf Me.txtrate_KeyDown3
                        RemoveHandler txt2(i).KeyDown, AddressOf Me.txtamount_KeyDown3
                        AddHandler txt2(i).KeyDown, AddressOf Me.txtamount_KeyDown3
                    End If
                    If i = 4 Then
                        RemoveHandler txt1(i).KeyPress, AddressOf Me.txtrate_KeyPress4
                        AddHandler txt1(i).KeyPress, AddressOf Me.txtrate_KeyPress4
                        RemoveHandler txt2(i).KeyPress, AddressOf Me.txtamount_KeyPress4
                        AddHandler txt2(i).KeyPress, AddressOf Me.txtamount_KeyPress4
                        RemoveHandler txt1(i).KeyDown, AddressOf Me.txtrate_KeyDown4
                        AddHandler txt1(i).KeyDown, AddressOf Me.txtrate_KeyDown4
                        RemoveHandler txt2(i).KeyDown, AddressOf Me.txtamount_KeyDown4
                        AddHandler txt2(i).KeyDown, AddressOf Me.txtamount_KeyDown4
                    End If
                    If i = 5 Then
                        RemoveHandler txt1(i).KeyPress, AddressOf Me.txtrate_KeyPress5
                        AddHandler txt1(i).KeyPress, AddressOf Me.txtrate_KeyPress5
                        RemoveHandler txt2(i).KeyPress, AddressOf Me.txtamount_KeyPress5
                        AddHandler txt2(i).KeyPress, AddressOf Me.txtamount_KeyPress5
                        RemoveHandler txt1(i).KeyDown, AddressOf Me.txtrate_KeyDown5
                        AddHandler txt1(i).KeyDown, AddressOf Me.txtrate_KeyDown5
                        RemoveHandler txt2(i).KeyDown, AddressOf Me.txtamount_KeyDown5
                        AddHandler txt2(i).KeyDown, AddressOf Me.txtamount_KeyDown5
                    End If
                Next
                If dsscheme.Tables(0).Rows.Count > 0 Then
                    If purreturn = False And Module1.flag = 1 Then
                        taxcalc()
                    End If
                End If
                taxctrl = True
            End If
        End If
    End Sub
    Private Sub txtrate_KeyPress0(ByVal sender As System.Object, ByVal e As KeyPressEventArgs)
        If Asc(e.KeyChar) = 13 Then
            ratelist(0) = Val(txt1(0).Text)
            amountlist(0) = 0
            taxcalc()
            txt2(0).Select()
        End If
    End Sub

    Private Sub txtrate_KeyDown0(ByVal sender As System.Object, ByVal e As KeyEventArgs)
        If e.KeyData = Keys.Left Then
            cmb_scheme.Select()
        End If
        If e.KeyData = Keys.Right Then
            txt2(0).Select()
        End If
        If e.KeyData = Keys.Down Then
            txt1(1).Select()
        End If
        If e.KeyData = Keys.Up Then
            cmb_scheme.Select()
        End If
    End Sub

    Private Sub txtamount_KeyPress0(ByVal sender As System.Object, ByVal e As KeyPressEventArgs)
        If Asc(e.KeyChar) = 13 Then
            amountlist(0) = Val(txt2(0).Text)
            ratelist(0) = 0
            taxcalc()
            If taxcount > 1 Then
                txt1(1).Select()
            Else
                Button1.Select()
            End If
        End If
    End Sub
    Private Sub txtamount_KeyDown0(ByVal sender As System.Object, ByVal e As KeyEventArgs)
        If e.KeyData = Keys.Left Then
            txt1(0).Select()
        End If
        If e.KeyData = Keys.Right Then
            If taxcount > 1 Then
                txt1(1).Select()
            Else
                Button1.Select()
            End If
        End If
        If e.KeyData = Keys.Down Then
            txt2(1).Select()
        End If
        If e.KeyData = Keys.Up Then
            cmb_scheme.Select()
        End If
    End Sub

    Private Sub txtrate_KeyPress1(ByVal sender As System.Object, ByVal e As KeyPressEventArgs)
        If Asc(e.KeyChar) = 13 Then
            ratelist(1) = Val(txt1(1).Text)
            amountlist(1) = 0
            taxcalc()
            txt2(1).Select()
        End If
    End Sub
    Private Sub txtrate_KeyDown1(ByVal sender As System.Object, ByVal e As KeyEventArgs)
        If e.KeyData = Keys.Left Then
            txt2(0).Select()
        End If
        If e.KeyData = Keys.Right Then
            txt2(1).Select()
        End If
        If e.KeyData = Keys.Down Then
            txt1(2).Select()
        End If
        If e.KeyData = Keys.Up Then
            txt1(0).Select()
        End If
    End Sub
    Private Sub txtamount_KeyPress1(ByVal sender As System.Object, ByVal e As KeyPressEventArgs)
        If Asc(e.KeyChar) = 13 Then
            amountlist(1) = Val(txt2(1).Text)
            ratelist(1) = 0
            taxcalc()
            If taxcount > 2 Then
                txt1(2).Select()
            Else
                Button1.Select()
            End If
        End If
    End Sub
    Private Sub txtamount_KeyDown1(ByVal sender As System.Object, ByVal e As KeyEventArgs)
        If e.KeyData = Keys.Left Then
            txt1(1).Select()
        End If
        If e.KeyData = Keys.Right Then
            If taxcount > 2 Then
                txt1(2).Select()
            Else
                Button1.Select()
            End If
        End If
        If e.KeyData = Keys.Down Then
            txt2(2).Select()
        End If
        If e.KeyData = Keys.Up Then
            txt2(0).Select()
        End If
    End Sub
    Private Sub txtrate_KeyPress2(ByVal sender As System.Object, ByVal e As KeyPressEventArgs)
        If Asc(e.KeyChar) = 13 Then
            ratelist(2) = Val(txt1(2).Text)
            amountlist(2) = 0
            taxcalc()
            txt2(2).Select()
        End If
    End Sub
    Private Sub txtrate_KeyDown2(ByVal sender As System.Object, ByVal e As KeyEventArgs)
        If e.KeyData = Keys.Left Then
            txt2(1).Select()
        End If
        If e.KeyData = Keys.Right Then
            txt2(2).Select()
        End If
        If e.KeyData = Keys.Down Then
            txt1(3).Select()
        End If
        If e.KeyData = Keys.Up Then
            txt1(1).Select()
        End If
    End Sub
    Private Sub txtamount_KeyPress2(ByVal sender As System.Object, ByVal e As KeyPressEventArgs)
        If Asc(e.KeyChar) = 13 Then
            amountlist(2) = Val(txt2(2).Text)
            ratelist(2) = 0
            taxcalc()
            If taxcount > 3 Then
                txt1(3).Select()
            Else
                Button1.Select()
            End If
        End If
    End Sub
    Private Sub txtamount_KeyDown2(ByVal sender As System.Object, ByVal e As KeyEventArgs)
        If e.KeyData = Keys.Left Then
            txt1(2).Select()
        End If
        If e.KeyData = Keys.Right Then
            If taxcount > 3 Then
                txt1(3).Select()
            Else
                Button1.Select()
            End If
        End If
        If e.KeyData = Keys.Down Then
            txt2(3).Select()
        End If
        If e.KeyData = Keys.Up Then
            txt2(1).Select()
        End If
    End Sub
    Private Sub txtrate_KeyPress3(ByVal sender As System.Object, ByVal e As KeyPressEventArgs)
        If Asc(e.KeyChar) = 13 Then
            ratelist(3) = Val(txt1(3).Text)
            amountlist(3) = 0
            taxcalc()
            txt2(3).Select()
        End If
    End Sub
    Private Sub txtrate_KeyDown3(ByVal sender As System.Object, ByVal e As KeyEventArgs)
        If e.KeyData = Keys.Left Then
            txt2(2).Select()
        End If
        If e.KeyData = Keys.Right Then
            txt2(3).Select()
        End If
        If e.KeyData = Keys.Down Then
            txt1(4).Select()
        End If
        If e.KeyData = Keys.Up Then
            txt1(2).Select()
        End If
    End Sub
    Private Sub txtamount_KeyPress3(ByVal sender As System.Object, ByVal e As KeyPressEventArgs)
        If Asc(e.KeyChar) = 13 Then
            amountlist(3) = Val(txt2(3).Text)
            ratelist(3) = 0
            taxcalc()
            If taxcount > 4 Then
                txt1(4).Select()
            Else
                Button1.Select()
            End If
        End If
    End Sub
    Private Sub txtamount_KeyDown3(ByVal sender As System.Object, ByVal e As KeyEventArgs)
        If e.KeyData = Keys.Left Then
            txt1(3).Select()
        End If
        If e.KeyData = Keys.Right Then
            If taxcount > 4 Then
                txt1(4).Select()
            Else
                Button1.Select()
            End If
        End If
        If e.KeyData = Keys.Down Then
            txt2(4).Select()
        End If
        If e.KeyData = Keys.Up Then
            txt2(2).Select()
        End If
    End Sub
    Private Sub txtrate_KeyPress4(ByVal sender As System.Object, ByVal e As KeyPressEventArgs)
        If Asc(e.KeyChar) = 13 Then
            ratelist(4) = Val(txt1(4).Text)
            amountlist(4) = 0
            taxcalc()
            txt2(4).Select()
        End If
    End Sub
    Private Sub txtrate_KeyDown4(ByVal sender As System.Object, ByVal e As KeyEventArgs)
        If e.KeyData = Keys.Left Then
            txt2(3).Select()
        End If
        If e.KeyData = Keys.Right Then
            txt2(4).Select()
        End If
        If e.KeyData = Keys.Down Then
            txt1(5).Select()
        End If
        If e.KeyData = Keys.Up Then
            txt1(3).Select()
        End If
    End Sub
    Private Sub txtamount_KeyPress4(ByVal sender As System.Object, ByVal e As KeyPressEventArgs)
        If Asc(e.KeyChar) = 13 Then
            amountlist(4) = Val(txt2(4).Text)
            ratelist(4) = 0
            taxcalc()
            If taxcount > 5 Then
                txt1(5).Select()
            Else
                Button1.Select()
            End If
        End If
    End Sub
    Private Sub txtamount_KeyDown4(ByVal sender As System.Object, ByVal e As KeyEventArgs)
        If e.KeyData = Keys.Left Then
            txt1(4).Select()
        End If
        If e.KeyData = Keys.Right Then
            If taxcount > 5 Then
                txt1(5).Select()
            Else
                Button1.Select()
            End If
        End If
        If e.KeyData = Keys.Down Then
            txt2(5).Select()
        End If
        If e.KeyData = Keys.Up Then
            txt2(3).Select()
        End If
    End Sub
    Private Sub txtrate_KeyPress5(ByVal sender As System.Object, ByVal e As KeyPressEventArgs)
        If Asc(e.KeyChar) = 13 Then
            ratelist(5) = Val(txt1(5).Text)
            amountlist(5) = 0
            taxcalc()
            txt2(5).Select()
        End If
    End Sub

    Private Sub txtrate_KeyDown5(ByVal sender As System.Object, ByVal e As KeyEventArgs)
        If e.KeyData = Keys.Left Then
            txt2(4).Select()
        End If
        If e.KeyData = Keys.Right Then
            txt2(5).Select()
        End If
        If e.KeyData = Keys.Down Then
            txt2(5).Select()
        End If
        If e.KeyData = Keys.Up Then
            txt1(4).Select()
        End If
    End Sub
    Private Sub txtamount_KeyPress5(ByVal sender As System.Object, ByVal e As KeyPressEventArgs)
        If Asc(e.KeyChar) = 13 Then
            amountlist(5) = Val(txt2(5).Text)
            ratelist(5) = 0
            taxcalc()
            Button1.Select()
        End If
    End Sub
    Private Sub txtamount_KeyDown5(ByVal sender As System.Object, ByVal e As KeyEventArgs)
        If e.KeyData = Keys.Left Then
            txt1(5).Select()
        End If
        If e.KeyData = Keys.Right Then
            Button1.Select()
        End If
        If e.KeyData = Keys.Down Then
            Button1.Select()
        End If
        If e.KeyData = Keys.Up Then
            txt2(4).Select()
        End If
    End Sub
    Private Sub taxcalc()
        txt_rounding_plus.Text = "0.00"
        txt_rounding_minus.Text = "0.00"
        Dim gross As Double = 0
        Dim netamt As Double = 0
        round = 0
        ReDim Preserve ratelist(taxcount)
        ReDim Preserve glist(taxcount + 1)
        ReDim Preserve amountlist(taxcount)
        If dsscheme.Tables(0).Rows.Count > 0 And taxcount > 0 Then
            glist(0) = Val(txt_gross.Text)
            txt_net.Text = Nothing
            For i = 0 To taxcount - 1

                If glist(i) > 0 Then
                    If ratelist(i) = 0 Then
                        txt1(i).Text = Format((amountlist(i) * 100) / glist(i), "0.00")
                        If Trim(dstax.Tables(0).Rows(i).Item(7).ToString) = "+" Then
                            glist(i + 1) = Format(glist(i) + amountlist(i), "0.00")
                        Else
                            glist(i + 1) = Format(glist(i) - amountlist(i), "0.00")
                        End If
                    Else
                        txt2(i).Text = Format(glist(i) * (ratelist(i) / 100), "0.00")
                        If Trim(dstax.Tables(0).Rows(i).Item(7).ToString) = "+" Then
                            glist(i + 1) = Format(glist(i) + txt2(i).Text, "0.00")
                        Else
                            glist(i + 1) = Format(glist(i) - txt2(i).Text, "0.00")
                        End If
                    End If
                End If
            Next
            gross = glist(taxcount)
            netamt = Convert.ToInt32(glist(taxcount))
        Else
            gross = Val(txt_gross.Text)
            netamt = Convert.ToInt64(Val(txt_gross.Text))
        End If
        If gross > netamt Then
            round = Format(gross - netamt, "0.00")
            If round >= 0.5 Then
                netamt = netamt + 1
                txt_rounding_plus.Text = round
            Else
                txt_rounding_minus.Text = round
            End If
        Else
            round = Format(netamt - gross, "0.00")
            If round > 0.49 Then
                netamt = netamt + 1
            End If
            txt_rounding_plus.Text = round
        End If
        txt_net.Text = netamt
    End Sub

    Private Sub TextBox2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_rounding_plus.KeyDown
        If e.KeyData = Keys.Enter Then
            txt_rounding_minus.Select()
        End If
    End Sub

    Private Sub TextBox7_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_gross.KeyPress
        e.Handled = True
    End Sub
    Private Sub TextBox5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_paid.TextChanged
        txt_due.Text = Val(txt_net.Text) - Val(txt_paid.Text)
    End Sub
    Private Sub Button1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Button1.KeyUp
        If e.KeyData = Keys.Up Then
            txt2(taxcount).Select()
        End If
    End Sub
    Private Sub purchase_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyData = Keys.F10 Then
            save()
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        save()
    End Sub

    Private Sub save()
        Dim ob As New Class1
        If ob.date_check(DateTimePicker2.Value.Date) = False Then
            Exit Sub
        End If
        Dim s As String
        Dim ds As New DataSet
        If txt_billno.Text = "" And cmb_billnl.Text = "" Then
            MsgBox("Please enter Doc Bill No.", MsgBoxStyle.Information, "Blank Field")
            txt_billno.Select()
            Exit Sub
        End If
        billduplicate()
        If billcheck = True Then
            txt_billno.Select()
            Exit Sub
        End If
        If Not dgv1.RowCount > 1 And editmode = True Then
            MsgBox("Please enter purchase items details.", MsgBoxStyle.Information, "Blank Field")
            dgv1.Select()
            Exit Sub
        Else
            For i = 0 To dgv1.Rows.Count - 2
                If String.IsNullOrEmpty(CStr(Me.dgv1.Rows(i).Cells(1).Value)) Then
                    MsgBox("Please enter purchase item name.", MsgBoxStyle.Information, "Blank Field")
                    dgv1.Select()
                    Exit Sub
                End If
                If String.IsNullOrEmpty(CStr(Me.dgv1.Rows(i).Cells(4).Value)) And String.IsNullOrEmpty(CStr(Me.dgv1.Rows(i).Cells(5).Value)) Then
                    MsgBox("Please enter no of box or no of loos bottle.", MsgBoxStyle.Information, "Blank Field")
                    dgv1.Select()
                    Exit Sub
                End If
                If String.IsNullOrEmpty(CStr(Me.dgv1.Rows(i).Cells(6).Value)) Then
                    MsgBox("Please enter no of box or no of loose bottle.", MsgBoxStyle.Information, "Blank Field")
                    dgv1.Select()
                    Exit Sub
                End If
            Next
        End If

        purchase_rate()

        If Module1.flag = 2 Then
            s = "delete from purchasemain where companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "' and trnno ='" & txt_trnno.Text & "'"
            ob.insert(s)
            s = "delete from purchasedetail where companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "' and trnno ='" & txt_trnno.Text & "'"
            ob.insert(s)
            s = "delete from purchasetaxdetail where companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "' and trnno ='" & txt_trnno.Text & "'"
            ob.insert(s)
        End If
        Dim txtbill As String
        If purreturn = True Then
            For i = 0 To dgv1.Rows.Count - 2
                For j = 0 To dsedit.Tables(0).Rows.Count - 1
                    If dsedit.Tables(0).Rows(j).Item(2) = dgv1.Rows(i).Cells(2).Value Then
                        If dsedit.Tables(0).Rows(j).Item(6) < dgv1.Rows(i).Cells(6).Value Then
                            MsgBox("Return quantity of '" & dsedit.Tables(0).Rows(j).Item(1) & "' is more than purchase quantity.", MsgBoxStyle.Exclamation, "Quantity")
                            Exit Sub
                        End If
                    End If
                Next
            Next
            txtbill = cmb_billnl.Text.ToUpper
        Else
            txtbill = txt_billno.Text.ToUpper
        End If
        s = "insert into purchasemain (companycode,yearcode,trnno,trndate,ptype,purchaseacccode,suppliercode,shopcode,docno,docdate,tppassno,schemecode,totamount,tottaxoth,totnetamt,narration,checkedby)values('" & Module1.companycode & "','" & Module1.yearcode & "','" & Val(txt_trnno.Text) & "','" & DateTimePicker1.Value.Date & "','" & type & "','" & pledcode & "','" & psuppledcode & "','" & pstorecode & "','" & txtbill & "','" & DateTimePicker2.Value.Date & "','" & Val(txt_tppass.Text) & "','" & pschemecode & "','" & Val(txt_gross.Text) & "','" & Val(txt_net.Text) - Val(txt_gross.Text) & "','" & Val(txt_net.Text) & "','" & txt_narration.Text & "','" & Module1.username & "')"
        ob.insert(s)
        For i = 0 To dgv1.Rows.Count - 2
            s = "insert into purchasedetail (companycode,yearcode,slno,itemcode,batchno,itembox,itemloose,itemquantity,itemrate,itemamount,trnno)values('" & Module1.companycode & "','" & Module1.yearcode & "','" & dgv1.Item(0, i).Value & "','" & dgv1.Item(2, i).Value & "','" & dgv1.Item(3, i).Value & "','" & dgv1.Item(4, i).Value & "','" & dgv1.Item(5, i).Value & "','" & dgv1.Item(6, i).Value & "','" & dgv1.Item(7, i).Value & "','" & dgv1.Item(8, i).Value & "','" & Val(txt_trnno.Text) & "')"
            ob.insert(s)
        Next
        If taxcount > 0 Then
            For i = 0 To taxcount - 1
                s = "insert into purchasetaxdetail (companycode,trnno,schemecode,taxcode,taxrate,onamount,taxamount,taxaccount,yearcode)values('" & Module1.companycode & "','" & Val(txt_trnno.Text) & "','" & pschemecode & "','" & dstax.Tables(0).Rows(i).Item(4) & "','" & Val(txt1(i).Text) & "','" & glist(i) & "','" & Val(txt2(i).Text) & "','" & dstax.Tables(0).Rows(i).Item(6) & "','" & Module1.yearcode & "')"
                ob.insert(s)
            Next
        End If

        '---initialising the array to default size 0 and deleting all the elements from that aray--->
        ReDim irate(0)
        ReDim ibox(0)
        ReDim iloose(0)
        ReDim ipacking(0)
        ReDim iquantity(0)
        ReDim iamount(0)
        ReDim baselist(0)
        ReDim ratelist(0)
        ReDim amountlist(0)
        ReDim glist(0)
        ReDim lbl1(0)
        ReDim lbl2(0)
        ReDim txt1(0)
        ReDim txt2(0)
        '-----end of deleting all the values from the array --->


        frm_MainForm.mainformload()
        If Module1.flag = 1 Then
            formload()
        Else
            Me.Close()
        End If
    End Sub

    Private Sub purchase_rate()
        If dgv1.Rows.Count > 1 Then
            Dim ds_prate As DataSet
            For i = 0 To dgv1.Rows.Count - 2
                s = "select purchaserate from itemmst where companycode='" & Module1.companycode & "' and itemcode='" & dgv1.Item(2, i).Value.ToString & "'"
                ds_prate = ob.populate(s)
                If Not ds_prate.Tables(0).Rows(0).Item(0).ToString = dgv1.Item(7, i).Value.ToString Then
                    s = "update itemmst set purchaserate='" & dgv1.Item(7, i).Value & "' where companycode='" & Module1.companycode & "' and itemcode='" & dgv1.Item(2, i).Value.ToString & "'"
                    ob.insert(s)
                End If
            Next
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        '*********** code for printing *********************
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
        frm_MainForm.Enabled = True
    End Sub
    Private Sub purchase_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        frm_MainForm.Enabled = True
    End Sub


End Class
