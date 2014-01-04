Public Class frm_userrights
    Dim dsu As New DataSet
    Dim dsc As New DataSet
    Dim dsy As New DataSet
    Dim acc As String

    Private Sub userrights_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim s As String
        Dim ob As New Class1
        s = "select usercode,username,accesslevel,password from id order by username"
        dsu = ob.populate(s)
        If dsu.Tables(0).Rows.Count <= 1 Then
            MsgBox("No user exist, please add new user & set their rights.", MsgBoxStyle.Exclamation, "No User")
            Module1.flag = 0
            Exit Sub
        End If
        For i = 0 To dsu.Tables(0).Rows.Count - 1
            If Not dsu.Tables(0).Rows(i).Item(0).ToString = "A00001" Then
                ComboBox1.Items.Add(dsu.Tables(0).Rows(i).Item(1).ToString)
            End If
        Next
        s = "select * from companymst order by companyname"
        dsc = ob.populate(s)
        For i = 0 To dsc.Tables(0).Rows.Count - 1
            ComboBox2.Items.Add(dsc.Tables(0).Rows(i).Item(1).ToString)
        Next
        If Module1.flag = 1 Then
            addload()
            ComboBox1.Text = dsu.Tables(0).Rows(0).Item(0)
            ComboBox2.Text = dsc.Tables(0).Rows(0).Item(0)
        ElseIf Module1.flag = 2 Then
            editload()
        End If
        ComboBox1.Select()
    End Sub

    Private Sub addload()
        acc = "0"
        dgv1.Columns.Clear()
        dgv2.Columns.Clear()
        dgv3.Columns.Clear()
        CheckBox1.Enabled = False
        CheckBox2.Enabled = False
        CheckBox3.Enabled = False
        CheckBox4.Enabled = False
        CheckBox5.Enabled = False
        CheckBox6.Enabled = False
        CheckBox7.Enabled = False
        CheckBox8.Enabled = False
        CheckBox9.Enabled = False

    End Sub
    Private Sub editload()
        addload()
        Dim s As String
        Dim ob As New Class1
        Dim ds As New DataSet
        s = "select id.username,companymst.companyname,userrights.usercode,userrights.companycode,userrights.accaccess,userrights.modulename,userrights.perv,userrights.pera,userrights.pere,userrights.perd from userrights join id on id.usercode=userrights.usercode join companymst on companymst.companycode=userrights.companycode where userrights.usercode='" & frm_MainForm.dsload.Tables(0).Rows(row).Item(0).ToString & " '"
        ds = ob.populate(s)
        If ds.Tables(0).Rows.Count > 0 Then
            ComboBox1.Text = ds.Tables(0).Rows(0).Item(0).ToString
            ComboBox2.Text = ds.Tables(0).Rows(0).Item(1).ToString
            If ds.Tables(0).Rows(0).Item(4).ToString = "1" Then
                CheckBox10.Checked = True
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    If i < 12 Then
                        dgv1.Item(0, i).Value = ds.Tables(0).Rows(i).Item(5).ToString
                        dgv1.Item(1, i).Value = ds.Tables(0).Rows(i).Item(6).ToString
                        dgv1.Item(2, i).Value = ds.Tables(0).Rows(i).Item(7).ToString
                        dgv1.Item(3, i).Value = ds.Tables(0).Rows(i).Item(8).ToString
                        dgv1.Item(4, i).Value = ds.Tables(0).Rows(i).Item(9).ToString
                    End If
                    If i > 11 And i < 19 Then
                        dgv2.Item(0, i - 12).Value = ds.Tables(0).Rows(i).Item(5).ToString
                        dgv2.Item(1, i - 12).Value = ds.Tables(0).Rows(i).Item(6).ToString
                        dgv2.Item(2, i - 12).Value = ds.Tables(0).Rows(i).Item(7).ToString
                        dgv2.Item(3, i - 12).Value = ds.Tables(0).Rows(i).Item(8).ToString
                        dgv2.Item(4, i - 12).Value = ds.Tables(0).Rows(i).Item(9).ToString
                    End If
                    If i > 18 And i < 26 Then
                        dgv3.Item(0, i - 19).Value = ds.Tables(0).Rows(i).Item(5).ToString
                        dgv3.Item(1, i - 19).Value = ds.Tables(0).Rows(i).Item(6).ToString
                    End If
                Next
            ElseIf ds.Tables(0).Rows(0).Item(4).ToString = "0" Then
                CheckBox10.Checked = False
            End If
        End If

    End Sub
    Private Sub CheckBox10_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox10.CheckedChanged
        If CheckBox10.Checked = True Then
            acc = "1"
            With dgv1
                .Columns.Clear()
                Dim Columndescrip As New DataGridViewTextBoxColumn
                With Columndescrip
                    .HeaderText = "Description"
                    .Width = 220
                End With
                Dim Columnview As New DataGridViewCheckBoxColumn
                With Columnview
                    .HeaderText = "View"
                    .Width = 40
                End With
                Dim Columnadd As New DataGridViewCheckBoxColumn
                With Columnadd
                    .HeaderText = "Add"
                    .Width = 40
                End With
                Dim Columnedit As New DataGridViewCheckBoxColumn
                With Columnedit
                    .HeaderText = "Edit"
                    .Width = 40
                End With
                Dim Columndel As New DataGridViewCheckBoxColumn
                With Columndel
                    .HeaderText = "Delte"
                    .Width = 40
                End With
                .Columns.Insert(0, Columndescrip)
                .Columns.Insert(1, Columnview)
                .Columns.Insert(2, Columnadd)
                .Columns.Insert(3, Columnedit)
                .Columns.Insert(4, Columndel)
                .RowCount = 12
                .Item(0, 0).Value = "Account Group"
                .Item(0, 1).Value = "Account Information"
                .Item(0, 2).Value = "Ledger Master"
                .Item(0, 3).Value = "Item Category"
                .Item(0, 4).Value = "Brand Master"
                .Item(0, 5).Value = "Foreign Kind Liquor"
                .Item(0, 6).Value = "Meassure & Packing"
                .Item(0, 7).Value = "Strength Master"
                .Item(0, 8).Value = "Item Information"
                .Item(0, 9).Value = "Sales Rate"
                .Item(0, 10).Value = "Tax Schemes"
                .Item(0, 11).Value = "Storage Location"
            End With

            With dgv2
                .Columns.Clear()
                Dim Columndescrip As New DataGridViewTextBoxColumn
                With Columndescrip
                    .HeaderText = "Description"
                    .Width = 220
                End With
                Dim Columnview As New DataGridViewCheckBoxColumn
                With Columnview
                    .HeaderText = "View"
                    .Width = 40
                End With
                Dim Columnadd As New DataGridViewCheckBoxColumn
                With Columnadd
                    .HeaderText = "Add"
                    .Width = 40
                End With
                Dim Columnedit As New DataGridViewCheckBoxColumn
                With Columnedit
                    .HeaderText = "Edit"
                    .Width = 40
                End With
                Dim Columndel As New DataGridViewCheckBoxColumn
                With Columndel
                    .HeaderText = "Delte"
                    .Width = 40
                End With
                .Columns.Insert(0, Columndescrip)
                .Columns.Insert(1, Columnview)
                .Columns.Insert(2, Columnadd)
                .Columns.Insert(3, Columnedit)
                .Columns.Insert(4, Columndel)
                .RowCount = 7
                .Item(0, 0).Value = "Purchase Bill"
                .Item(0, 1).Value = "Payment Voucher"
                .Item(0, 2).Value = "Receipt Voucher"
                .Item(0, 3).Value = "Counter Sale"
                .Item(0, 4).Value = "Opening Stock"
                .Item(0, 5).Value = "Stock Transfer"
                .Item(0, 6).Value = "Breakage Entry"
            End With

            With dgv3
                .Columns.Clear()
                Dim Columndescrip As New DataGridViewTextBoxColumn
                With Columndescrip
                    .HeaderText = "Description"
                    .Width = 220
                End With
                Dim Columnview As New DataGridViewCheckBoxColumn
                With Columnview
                    .HeaderText = "View"
                    .Width = 40
                End With
                .Columns.Insert(0, Columndescrip)
                .Columns.Insert(1, Columnview)
                .RowCount = 7
                .Item(0, 0).Value = "Account Ledger"
                .Item(0, 1).Value = "Stock Statement"
                .Item(0, 2).Value = "Purchase Report"
                .Item(0, 3).Value = "Sale Report"
                .Item(0, 3).Value = "Excise Report"
                .Item(0, 4).Value = "Goods Receipt Report"
                .Item(0, 5).Value = "Sale Order Report"
                .Item(0, 6).Value = "Delivery Challan Report"
                .Columns(0).Width = 340
                .Columns(1).Width = 40
            End With
            CheckBox1.Enabled = True
            CheckBox2.Enabled = True
            CheckBox3.Enabled = True
            CheckBox4.Enabled = True
            CheckBox5.Enabled = True
            CheckBox6.Enabled = True
            CheckBox7.Enabled = True
            CheckBox8.Enabled = True
            CheckBox9.Enabled = True
        Else
            addload()
        End If
    End Sub


    Private Sub ComboBox2_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox2.KeyUp
        If e.KeyData = Keys.Enter Then
            If ComboBox2.Text = "" Or Not ComboBox2.SelectedIndex >= 0 Then
                ComboBox2.DroppedDown = True
            Else
                ComboBox3.Select()
            End If
        End If
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        If ComboBox1.SelectedIndex >= 0 Then
            ComboBox3.Items.Clear()
            Dim s As String
            Dim ob As New Class1
            s = "select yearmst.yearcode,companymst.companycode,yearrange from yearmst join companymst on companymst.companycode=yearmst.companycode where companymst.companycode='" & dsc.Tables(0).Rows(ComboBox2.SelectedIndex).Item(0) & "' order by yearrange desc "
            dsy = ob.populate(s)
            For i = 0 To dsy.Tables(0).Rows.Count - 1
                ComboBox3.Items.Add(dsy.Tables(0).Rows(i).Item(2).ToString)
            Next
            ComboBox3.Text = dsy.Tables(0).Rows(0).Item(2)
        End If

    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
        frm_MainForm.Enabled = True
    End Sub

    Private Sub ComboBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox1.KeyUp
        If e.KeyData = Keys.Enter Then
            If ComboBox1.Text = "" Or Not ComboBox1.SelectedIndex >= 0 Then
                ComboBox1.DroppedDown = True
            Else
                ComboBox2.Select()
            End If
        End If

    End Sub

    Private Sub ComboBox3_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox3.KeyUp
        If e.KeyData = Keys.Enter Then
            If ComboBox3.Text = "" Or Not ComboBox3.SelectedIndex >= 0 Then
                ComboBox3.DroppedDown = True
            Else
                dgv1.Select()
            End If
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            For i = 0 To dgv1.Rows.Count - 1
                dgv1.Item(1, i).Value = 1
            Next
        Else
            For i = 0 To dgv1.Rows.Count - 1
                dgv1.Item(1, i).Value = 0
            Next
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            For i = 0 To dgv1.Rows.Count - 1
                dgv1.Item(2, i).Value = 1
            Next
            CheckBox1.Checked = True
        Else
            For i = 0 To dgv1.Rows.Count - 1
                dgv1.Item(2, i).Value = 0
            Next
        End If

    End Sub

    Private Sub CheckBox3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked = True Then
            For i = 0 To dgv1.Rows.Count - 1
                dgv1.Item(3, i).Value = 1
            Next
            CheckBox1.Checked = True
        Else
            For i = 0 To dgv1.Rows.Count - 1
                dgv1.Item(3, i).Value = 0
            Next
        End If
    End Sub

    Private Sub CheckBox4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox4.CheckedChanged
        If CheckBox4.Checked = True Then
            For i = 0 To dgv1.Rows.Count - 1
                dgv1.Item(4, i).Value = 1
            Next
            CheckBox1.Checked = True
        Else
            For i = 0 To dgv1.Rows.Count - 1
                dgv1.Item(4, i).Value = 0
            Next
        End If

    End Sub

    Private Sub CheckBox5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox5.CheckedChanged
        If CheckBox5.Checked = True Then
            For i = 0 To dgv2.Rows.Count - 1
                dgv2.Item(1, i).Value = 1
            Next
        Else
            For i = 0 To dgv2.Rows.Count - 1
                dgv2.Item(1, i).Value = 0
            Next
        End If

    End Sub

    Private Sub CheckBox6_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox6.CheckedChanged
        If CheckBox6.Checked = True Then
            For i = 0 To dgv2.Rows.Count - 1
                dgv2.Item(2, i).Value = 1
            Next
            CheckBox5.Checked = True
        Else
            For i = 0 To dgv2.Rows.Count - 1
                dgv2.Item(2, i).Value = 0
            Next
        End If

    End Sub

    Private Sub CheckBox7_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox7.CheckedChanged
        If CheckBox7.Checked = True Then
            For i = 0 To dgv2.Rows.Count - 1
                dgv2.Item(3, i).Value = 1
            Next
            CheckBox5.Checked = True
        Else
            For i = 0 To dgv2.Rows.Count - 1
                dgv2.Item(3, i).Value = 0
            Next
        End If

    End Sub

    Private Sub CheckBox8_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox8.CheckedChanged
        If CheckBox8.Checked = True Then
            For i = 0 To dgv2.Rows.Count - 1
                dgv2.Item(4, i).Value = 1
            Next
            CheckBox5.Checked = True
        Else
            For i = 0 To dgv2.Rows.Count - 1
                dgv2.Item(4, i).Value = 0
            Next
        End If

    End Sub

    Private Sub CheckBox9_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox9.CheckedChanged
        If CheckBox9.Checked = True Then
            For i = 0 To dgv3.Rows.Count - 1
                dgv3.Item(1, i).Value = 1
            Next
        Else
            For i = 0 To dgv3.Rows.Count - 1
                dgv3.Item(1, i).Value = 0
            Next
        End If

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim s As String
        Dim ob As New Class1
        Dim ds As New DataSet
        s = "select * from userrights where usercode='" & dsu.Tables(0).Rows(ComboBox1.SelectedIndex + 1).Item(0).ToString & "' and companycode='" & dsc.Tables(0).Rows(ComboBox2.SelectedIndex).Item(0) & "'"
        ds = ob.populate(s)
        If Module1.flag = 1 And ds.Tables(0).Rows.Count > 0 Then
            Dim r = MsgBox("This user allready has some rights to selected company, Do you want to overwright these?", MsgBoxStyle.YesNo, "User Rights")
            If r = 7 Then
                Exit Sub
            End If
        End If

        s = "delete from userrights where usercode='" & dsu.Tables(0).Rows(ComboBox1.SelectedIndex + 1).Item(0).ToString & "' and companycode='" & dsc.Tables(0).Rows(ComboBox2.SelectedIndex).Item(0) & "'or companycode=NULL"
        ob.insert(s)

        If acc = "0" Then
            For i = 0 To 11
                s = "insert into userrights (usercode,companycode,accaccess)values('" & dsu.Tables(0).Rows(ComboBox1.SelectedIndex + 1).Item(0).ToString & "','" & dsc.Tables(0).Rows(ComboBox2.SelectedIndex).Item(0) & "','" & acc & "')"
                ob.insert(s)
            Next
            For i = 12 To 18
                s = "insert into userrights (usercode,companycode,accaccess)values('" & dsu.Tables(0).Rows(ComboBox1.SelectedIndex + 1).Item(0).ToString & "','" & dsc.Tables(0).Rows(ComboBox2.SelectedIndex).Item(0) & "','" & acc & "')"
                ob.insert(s)
            Next
            For i = 19 To 25
                s = "insert into userrights (usercode,companycode,accaccess)values('" & dsu.Tables(0).Rows(ComboBox1.SelectedIndex + 1).Item(0).ToString & "','" & dsc.Tables(0).Rows(ComboBox2.SelectedIndex).Item(0) & "','" & acc & "')"
                ob.insert(s)
            Next
        ElseIf acc = "1" Then
            For i = 0 To 11
                s = "insert into userrights (usercode,companycode,accaccess,modulename,perv,pera,pere,perd)values('" & dsu.Tables(0).Rows(ComboBox1.SelectedIndex + 1).Item(0).ToString & "','" & dsc.Tables(0).Rows(ComboBox2.SelectedIndex).Item(0) & "','" & acc & "','" & dgv1.Item(0, i).Value.ToString & "','" & Convert.ToBoolean(dgv1.Item(1, i).Value).ToString & "','" & Convert.ToBoolean(dgv1.Item(2, i).Value).ToString & "','" & Convert.ToBoolean(dgv1.Item(3, i).Value).ToString & "','" & Convert.ToBoolean(dgv1.Item(4, i).Value).ToString & "')"
                ob.insert(s)
            Next
            For i = 12 To 18
                s = "insert into userrights (usercode,companycode,accaccess,modulename,perv,pera,pere,perd)values('" & dsu.Tables(0).Rows(ComboBox1.SelectedIndex + 1).Item(0).ToString & "','" & dsc.Tables(0).Rows(ComboBox2.SelectedIndex).Item(0) & "','" & acc & "','" & dgv2.Item(0, i - 12).Value.ToString & "','" & Convert.ToBoolean(dgv2.Item(1, i - 12).Value).ToString & "','" & Convert.ToBoolean(dgv2.Item(2, i - 12).Value).ToString & "','" & Convert.ToBoolean(dgv2.Item(3, i - 12).Value).ToString & "','" & Convert.ToBoolean(dgv2.Item(4, i - 12).Value).ToString & "')"
                ob.insert(s)
            Next
            For i = 19 To 25
                s = "insert into userrights (usercode,companycode,accaccess,modulename,perv)values('" & dsu.Tables(0).Rows(ComboBox1.SelectedIndex + 1).Item(0).ToString & "','" & dsc.Tables(0).Rows(ComboBox2.SelectedIndex).Item(0) & "','" & acc & "','" & dgv3.Item(0, i - 19).Value.ToString & "','" & Convert.ToBoolean(dgv3.Item(1, i - 19).Value).ToString & "')"
                ob.insert(s)
            Next
        End If
        frm_MainForm.mainformload()
        If Module1.flag = 0 Then
            ComboBox1.Select()
            Exit Sub
        ElseIf Module1.flag = 1 Then
            ComboBox1.Text = ""
            ComboBox2.Text = ""
            ComboBox3.Text = ""
            CheckBox1.Checked = False
            CheckBox2.Checked = False
            CheckBox3.Checked = False
            CheckBox4.Checked = False
            CheckBox5.Checked = False
            CheckBox6.Checked = False
            CheckBox7.Checked = False
            CheckBox8.Checked = False
            CheckBox9.Checked = False
            CheckBox10.Checked = False
            ComboBox1.Select()
            flag = 1
        ElseIf Module1.flag = 2 Then
            Me.Close()
            frm_MainForm.Enabled = True
            flag = 0
        End If
    End Sub
    Private Sub userrights_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        frm_MainForm.Enabled = True
    End Sub

    Private Sub dgv1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgv1.Click
        If dgv1.Rows.Count > 0 Then
            If dgv1.CurrentCell.ColumnIndex = 2 And dgv1.Item(2, dgv1.CurrentCell.RowIndex).Value = False Then
                dgv1.Item(1, dgv1.CurrentCell.RowIndex).Value = True
            End If
            If dgv1.CurrentCell.ColumnIndex = 3 And dgv1.Item(3, dgv1.CurrentCell.RowIndex).Value = False Then
                dgv1.Item(1, dgv1.CurrentCell.RowIndex).Value = True
            End If
            If dgv1.CurrentCell.ColumnIndex = 4 And dgv1.Item(4, dgv1.CurrentCell.RowIndex).Value = False Then
                dgv1.Item(1, dgv1.CurrentCell.RowIndex).Value = True
            End If
        End If
    End Sub
    Private Sub dgv2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgv2.Click
        If dgv1.Rows.Count > 0 Then
            If dgv2.CurrentCell.ColumnIndex = 2 And dgv2.Item(2, dgv2.CurrentCell.RowIndex).Value = False Then
                dgv2.Item(1, dgv2.CurrentCell.RowIndex).Value = True
            End If
            If dgv2.CurrentCell.ColumnIndex = 3 And dgv1.Item(3, dgv2.CurrentCell.RowIndex).Value = False Then
                dgv2.Item(1, dgv2.CurrentCell.RowIndex).Value = True
            End If
            If dgv2.CurrentCell.ColumnIndex = 4 And dgv2.Item(4, dgv2.CurrentCell.RowIndex).Value = False Then
                dgv2.Item(1, dgv2.CurrentCell.RowIndex).Value = True
            End If
        End If
    End Sub

End Class