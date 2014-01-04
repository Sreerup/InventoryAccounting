Public Class frm_Taxscm
    Dim ds As New DataSet
    Dim ds1 As New DataSet
    Dim taxledcode As String
    Dim dsgrid As New DataSet
    Dim r As Integer
    Dim blank As Boolean
    Dim desc As Boolean
    Dim scheme As Boolean
    Dim base As Integer
    Dim code1 As String
    Dim fledit As Boolean
    Dim calcon As String

    Private Sub Taxscm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim s As String
        Dim ob As New Class1
        s = "select ledcode, name from ledger where companycode='" & Module1.companycode & "' order by name"
        ds = ob.populate(s)
        For i = 0 To ds.Tables(0).Rows.Count - 1
            ComboBox2.Items.Add(ds.Tables(0).Rows(i).Item(1))
        Next
        schemeload()
    End Sub

    Private Sub schemeload()
        r = 0
        base = 0
        code1 = ""
        TextBox2.Text = ""
        ComboBox1.Items.Clear()
        ComboBox3.Items.Clear()
        ComboBox4.Items.Clear()
        ComboBox1.Items.Add("Gross")
        ComboBox3.Items.Add(" +")
        ComboBox3.Items.Add(" -")
        ComboBox4.Items.Add("Gross")
        ComboBox4.Items.Add("Tax")
        taxload()
        griddesign()
        If Module1.flag = 2 Then
            Dim s As String
            Dim ob As New Class1
            s = "Select taxdetail.companycode,schemecode,schemename,srno,taxcode,taxname,taxdetail.ledcode,ledger.name,sig,taxrate,taxamount,calc_on,base from taxdetail join ledger on ledger.companycode=taxdetail.companycode and ledger.ledcode=taxdetail.ledcode where schemecode='" & Module1.col1 & "' and taxdetail.companycode='" & Module1.companycode & "' order by srno"
            ds1 = ob.populate(s)
            TextBox2.Text = ds1.Tables(0).Rows(0).Item(2).ToString
            code1 = ds1.Tables(0).Rows(0).Item(1).ToString
            For i = 0 To ds1.Tables(0).Rows.Count - 1
                dgv1.Rows.Add()
                For j = 0 To ds1.Tables(0).Columns.Count - 1
                    dgv1.Item(j, i).Value = ds1.Tables(0).Rows(i).Item(j)
                    If j = 4 Then
                        ComboBox1.Items.Add(ds1.Tables(0).Rows(i).Item(5).ToString)
                    End If
                Next
            Next
            r = dgv1.Rows.Count
        End If
        TextBox2.Select()
    End Sub
    Private Sub taxload()
        TextBox1.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
        ComboBox1.SelectedIndex = 0
        ComboBox2.SelectedIndex = 0
        ComboBox3.SelectedIndex = 0
        ComboBox4.SelectedIndex = 0
        TextBox1.Select()
    End Sub
    Private Sub griddesign()
        With dgv1
            .Columns.Clear()
            .Columns.Add("companycode", "companycode")
            .Columns.Add("schemecode", "schemecode")
            .Columns.Add("schemename", "schemename")
            .Columns.Add("slno", "Sl.No.")
            .Columns.Add("taxcode", "taxcode")
            .Columns.Add("taxname", "Description")
            .Columns.Add("ledcode", "ledcode")
            .Columns.Add("accname", "A/C Name")
            .Columns.Add("sign", "Formula")
            .Columns.Add("percent", "%tage")
            .Columns.Add("amount", "Amount")
            .Columns.Add("calc_on", "calc_on")
            .Columns.Add("base", "base")
            If Module1.flag = 2 Then
                .Columns(3).HeaderText = "Sl.No."
                .Columns(5).HeaderText = "Description"
                .Columns(7).HeaderText = "A/C Name"
                .Columns(8).HeaderText = "Formula"
                .Columns(9).HeaderText = "%tage"
                .Columns(10).HeaderText = "Amount"
            End If
            .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            .Columns(0).Visible = False
            .Columns(1).Visible = False
            .Columns(2).Visible = False
            .Columns(4).Visible = False
            .Columns(6).Visible = False
            .Columns(11).Visible = False
            .Columns(12).Visible = False
            .Columns(3).Width = 40
            .Columns(5).Width = 78
            .Columns(7).Width = 170
            .Columns(8).Width = 55
            .Columns(9).Width = 44
            .Columns(10).Width = 78
            .RowHeadersVisible = False
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        End With
    End Sub


    Private Sub TextBox2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyData = Keys.Enter And TextBox2.Text <> "" Then
            schemeduplicate()
            If scheme = True Then
                TextBox2.Select()
                Exit Sub
            Else
                TextBox1.Select()
            End If
        End If
    End Sub

    Private Sub schemeduplicate()
        If frm_MainForm.dsload.Tables(0).Rows.Count > 0 Then
            For i = 0 To frm_MainForm.dsload.Tables(0).Rows.Count - 1
                If Module1.flag = 1 Then
                    If TextBox2.Text.ToUpper = frm_MainForm.dsload.Tables(0).Rows(i).Item(3) Then
                        MsgBox("Scheme already present, enter new.", MsgBoxStyle.Exclamation, "Duplicate")
                        scheme = True
                    Else
                        scheme = False
                    End If
                ElseIf Module1.flag = 2 Then
                    If TextBox2.Text.ToUpper = frm_MainForm.dsload.Tables(0).Rows(i).Item(3) And TextBox2.Text.ToUpper <> Module1.col2 Then
                        MsgBox("Scheme already present, enter new.", MsgBoxStyle.Exclamation, "Duplicate")
                        scheme = True
                    Else
                        scheme = False
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyData = Keys.Enter And TextBox1.Text <> "" Then
            ComboBox2.Select()
        End If
    End Sub


    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        If ComboBox2.SelectedIndex >= 0 Then
            taxledcode = ds.Tables(0).Rows(ComboBox2.SelectedIndex).Item(0)
        End If
    End Sub
    Private Sub ComboBox2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox2.KeyDown
        If e.KeyData = Keys.Enter Then
            If ComboBox2.SelectedIndex >= 0 Then
                ComboBox3.Select()
            End If
        End If
        If e.KeyData = Keys.Down Then
            ComboBox2.DroppedDown = True
        End If
    End Sub

    Private Sub ComboBox3_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox3.KeyDown
        If e.KeyData = Keys.Enter Then
            If ComboBox3.SelectedIndex >= 0 Then
                TextBox6.Select()
            End If
        End If
        If e.KeyData = Keys.Down Then
            ComboBox3.DroppedDown = True
        End If
    End Sub

    Private Sub TextBox6_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox6.KeyPress
        Select Case e.KeyChar
            Case "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", ".", vbBack
                e.Handled = False
            Case Else
                e.Handled = True
        End Select
    End Sub


    Private Sub TextBox6_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox6.KeyDown
        If e.KeyData = Keys.Enter Then
            If Trim(TextBox6.Text) <> "" Then
                TextBox5.Text = ""
            End If
            TextBox5.Select()
        End If
    End Sub

    Private Sub TextBox5_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox5.KeyPress
        Select Case e.KeyChar
            Case "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", ".", vbBack
                e.Handled = False
            Case Else
                e.Handled = True
        End Select
    End Sub


    Private Sub TextBox5_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox5.KeyDown
        If e.KeyData = Keys.Enter Then
            If Trim(TextBox5.Text) <> "" Then
                TextBox6.Text = ""
            End If
            ComboBox4.Select()
        End If
    End Sub
    Private Sub ComboBox4_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox4.SelectedIndexChanged
        If ComboBox4.SelectedIndex = 0 Then
            calcon = "G"
        Else
            calcon = "T"
        End If
    End Sub

    Private Sub ComboBox4_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox4.KeyDown
        If e.KeyData = Keys.Enter Then
            If ComboBox4.SelectedIndex >= 0 Then
                ComboBox1.Select()
            End If
        End If
        If e.KeyData = Keys.Down Then
            ComboBox4.DroppedDown = True
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        base = ComboBox1.SelectedIndex
    End Sub

    Private Sub ComboBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox1.KeyDown
        If e.KeyData = Keys.Enter Then
            If ComboBox1.SelectedIndex >= 0 Then
                Button3.Select()
            End If
        End If
        If e.KeyData = Keys.Down Then
            ComboBox1.DroppedDown = True
        End If
    End Sub

    'Private Sub checkblank()
    '    If TextBox2.Text = "" Then
    '        MsgBox("Please Enter Scheme Name")
    '        TextBox2.Select()
    '        blank = True
    '    End If
    '    If TextBox1.Text = "" Then
    '        MsgBox("Please Enter Description")
    '        TextBox1.Select()
    '        blank = True
    '        Exit Sub
    '    End If
    '    If Val(Trim(TextBox5.Text)) = 0 And Val(Trim(TextBox6.Text)) = 0 Then
    '        MsgBox("Please enter either Percenteage or Amount")
    '        TextBox6.Select()
    '        blank = True
    '        Exit Sub
    '    End If
    'End Sub


    'Private Sub descduplicate()
    '    For i = 0 To dgv1.Rows.Count - 1
    '        Try
    '            If TextBox1.Text = dgv1.Item(0, i).Value.ToString Then
    '                MsgBox("Description already present, enter new.", MsgBoxStyle.Exclamation, "Duplicate")
    '                desc = True
    '            Else
    '                desc = False
    '            End If
    '        Catch ex As NullReferenceException
    '        End Try
    '    Next
    'End Sub


    '----button click event fo rthe apply button --->
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        'If dgv1.Rows.Count > 6 And Module1.flag1 = 1 Then
        'MsgBox("Maximum six Tax can be save in a Tax Scheme", MsgBoxStyle.Information)
        'Exit Sub
        'End If
        'blank = False
        'desc = False
        'checkblank()
        'If blank = True Then
        'Exit Sub
        'End If
        'descduplicate()
        'If desc = True Then
        'Exit Sub
        'End If

        Dim s1 As String
        Dim s2 As String
        Dim ob As New Class1
        Dim code As String
        s1 = "select top 1 taxcode from taxdetail where taxcode like '" & TextBox1.Text.Chars(0) & "%'order by taxcode  desc  "
        code = ob.getcode(s1, TextBox1.Text)
        If fledit = True Then
            dgv1.Item(5, r).Value = TextBox1.Text.ToUpper
            dgv1.Item(6, r).Value = taxledcode
            dgv1.Item(7, r).Value = ComboBox2.SelectedItem
            dgv1.Item(8, r).Value = Trim(ComboBox3.SelectedItem.ToString)
            dgv1.Item(9, r).Value = TextBox6.Text
            dgv1.Item(10, r).Value = TextBox5.Text
            dgv1.Item(11, r).Value = calcon
            dgv1.Item(12, r).Value = base
            fledit = False
        Else
            If code1 = "" Then
                s2 = "select top 1 schemecode from taxdetail where schemecode like '" & TextBox2.Text.Chars(0) & "%'order by schemecode  desc  "
                code1 = ob.getcode(s2, TextBox2.Text)
            End If
            dgv1.Rows.Add(Module1.companycode, code1, TextBox2.Text.ToUpper, dgv1.Rows.Count, code, TextBox1.Text.ToUpper, taxledcode, ComboBox2.SelectedItem.ToString, Trim(ComboBox3.SelectedItem.ToString), TextBox6.Text, TextBox5.Text, calcon, base)
            r = r + 1
            ComboBox1.Items.Add(TextBox1.Text.ToUpper)
        End If
        taxload()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If dgv1.Rows.Count > 7 Then
            MsgBox("Maximum six Tax can be save in a Tax Scheme", MsgBoxStyle.Information)
            Exit Sub
        End If
        If TextBox2.Text = "" Then
            MsgBox("Please Enter Scheme Name")
            TextBox2.Select()
            Exit Sub
        End If
        scheme = False
        schemeduplicate()
        If scheme = True Then
            TextBox2.Select()
            Exit Sub
        End If
        If dgv1.Rows.Count > 1 And r > 0 Then
            Dim ob As New Class1
            Dim s As String
            If Module1.flag = 2 Then
                s = "delete from taxdetail where companycode='" & Module1.companycode & "' and schemecode='" & Module1.col1 & "'"
                ob.insert(s)
            End If
            For i = 0 To dgv1.Rows.Count - 2
                s = "insert into taxdetail(companycode,schemecode,schemename,srno,taxcode,taxname,ledcode,sig,taxrate,taxamount,calc_on,base) values('" & dgv1.Item(0, i).Value.ToString & "','" & dgv1.Item(1, i).Value.ToString & "','" & TextBox2.Text.ToString.ToUpper & "','" & dgv1.Item(3, i).Value & "','" & dgv1.Item(4, i).Value.ToString & "','" & dgv1.Item(5, i).Value.ToString & "','" & dgv1.Item(6, i).Value.ToString & "','" & dgv1.Item(8, i).Value.ToString & "','" & dgv1.Item(9, i).Value & "','" & dgv1.Item(10, i).Value & "','" & dgv1.Item(11, i).Value.ToString & "','" & dgv1.Item(12, i).Value.ToString & "')"
                ob.insert(s)
                Module1.flag = 1
            Next

            schemeload()
            frm_MainForm.mainformload()
        Else
            MsgBox("No description is added please enter atleast 1 description.", MsgBoxStyle.Exclamation, "Description")
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
        frm_MainForm.Enabled = True
    End Sub
    Private Sub Taxscm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        frm_MainForm.Enabled = True
    End Sub

    Private Sub dgv1_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv1.CellDoubleClick
        dgv1rowselect()
    End Sub

    Private Sub dgv1rowselect()
        If dgv1.Rows.Count > 1 Then
            If dgv1.CurrentCell.RowIndex <> dgv1.Rows.Count - 1 Then
                r = dgv1.CurrentCell.RowIndex
                TextBox1.Text = dgv1.Item(5, r).Value.ToString
                TextBox6.Text = dgv1.Item(9, r).Value.ToString
                TextBox5.Text = dgv1.Item(10, r).Value.ToString
                ComboBox2.SelectedItem = dgv1.Item(7, r).Value.ToString
                ComboBox3.SelectedItem = dgv1.Item(8, r).Value.ToString
                ComboBox1.SelectedIndex = dgv1.Item(12, r).Value
                If dgv1.Item(11, r).Value.ToString = "G" Then
                    ComboBox4.SelectedIndex = 0
                Else
                    ComboBox4.SelectedIndex = 1
                End If
                fledit = True
            End If
        End If
    End Sub
    Private Sub dgv1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgv1.KeyDown
        r = dgv1.CurrentCell.RowIndex
        If e.KeyData = Keys.Delete Then
            dgv1.Rows.Remove(dgv1.CurrentRow)
            ComboBox1.Items.Clear()
            ComboBox1.Items.Add("Gross")
            If dgv1.Rows.Count > 0 Then
                For i = 0 To dgv1.Rows.Count - 2
                    If Not String.IsNullOrEmpty(CStr(Me.dgv1.Rows(i).Cells(5).Value)) Then
                        ComboBox1.Items.Add(dgv1.Rows(i).Cells(5).Value.ToString)
                    End If
                    dgv1.Item(3, i).Value = i + 1
                Next
            End If
            ComboBox1.SelectedIndex = 0
        End If
        If e.KeyData = Keys.Enter Then
            e.SuppressKeyPress = True
        End If
    End Sub
    Private Sub CheckedListBox1_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Button3.Select()
    End Sub

End Class