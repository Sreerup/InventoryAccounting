Public Class frm_SalesRate
    Dim s As String
    Dim s1 As Integer
    Dim s2 As Integer
    Dim count As Integer
    Dim row As Integer
    Dim salopname As Boolean
    Dim ds As New DataSet
    Dim value As String
    Dim ratecode As String
    Dim dv As New DataView
    Dim ob As New Class1

    Private Sub SalesRate_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        '---enabeling the form1 when the form is being closed ---->
        frm_MainForm.Enabled = True
    End Sub
    '---form load event ---->
    Private Sub SalesRate_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '--disabeling the form1 when the ratemst or the stockmst is open ----->
        frm_MainForm.Enabled = False
        TextBox1.Text = Nothing
        TextBox2.Text = Nothing
        '--for putting the ratename if the form is opened as 
        'itemratemst and in the edit mode-->
        If Module1.count = 7 And Module1.flag = 2 Then
            TextBox1.Text = Module1.sales_rate_name.ToUpper
            TextBox1.ReadOnly = True
            '--form opening for adding or editing the stock ---->
        ElseIf Module1.count = 14 Then
            Label1.Text = "Shop Name"
            Me.Text = "Opening Stock"
            TextBox1.Text = Module1.col1
        End If
        '---populating the grid ---->
        refresh_grid()
    End Sub
    '---code for refreshing the form --->
    Private Sub refresh_grid()
        If Module1.count = 7 Then
            s = "select itemname,itemcode,salesrate from vw_ratemst where companycode='" & Module1.companycode & "' and ratecode='" & Module1.sales_rate_code & "'"
        ElseIf Module1.count = 14 Then
            s = "select shopcode,companycode,itemname,loose,box,qnty,itemcode from opening_stock_show('" & Module1.yearcode & "','" & Module1.companycode & "','" & Module1.shopcode & "') order by itemname"
        End If
        '-----populating a dataset and creating the gridview------------------------->
        ds = ob.populate(s)
        DataGridView1.DataSource = ds.Tables(0)
        '-----deciding which columns to view in the dtagrid view---------------------->
        setvisible()
    End Sub
    '----this part is for searching through the grid ----->
    Private Sub TextBox2_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyUp
        dv.Table = ds.Tables(0)
        '---dynamic search option for the salesrate ----->
        If Module1.count = 7 Then
            dv.RowFilter = "itemname like '" & TextBox2.Text & "%' or convert(salesrate, System.String) like '" & TextBox2.Text & "%' "
            '---dynamic search option for the opening stock ---->
        ElseIf Module1.count = 14 Then
            dv.RowFilter = "itemname like '" & TextBox2.Text & "%' and shopcode='" & Module1.shopcode & "'"
        End If
        '--put the table of the dataset in the dataview---->
        DataGridView1.DataSource = dv
        setvisible()
    End Sub
    '--allowing the visibility of some of the columns in the datagridview1 ----->
    Private Sub setvisible()
        If Module1.count = 7 Then
            DataGridView1.Columns(1).Visible = False
            DataGridView1.Columns(0).SortMode = DataGridViewColumnSortMode.Programmatic
            DataGridView1.Columns(2).SortMode = DataGridViewColumnSortMode.Programmatic
        ElseIf Module1.count = 14 Then
            DataGridView1.Columns(0).Visible = False
            DataGridView1.Columns(1).Visible = False
            DataGridView1.Columns(6).Visible = False
            DataGridView1.Columns("itemname").SortMode = DataGridViewColumnSortMode.Programmatic
            DataGridView1.Columns("loose").SortMode = DataGridViewColumnSortMode.Programmatic
            DataGridView1.Columns("box").SortMode = DataGridViewColumnSortMode.Programmatic
            DataGridView1.Columns("qnty").SortMode = DataGridViewColumnSortMode.Programmatic
        End If
    End Sub
    '---code behind the save button ------------->
    Private Sub Create_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Create.Click
        Dim dt As New DataSet
        dt = ds.GetChanges
        If Not TextBox1.Text = Nothing Then
            '----saving the changed rates in the itemratemst ---->
            If Module1.count = 7 Then
                '---deleting the old rates--------------->
                s = "delete from itemratemst where companycode='" & Module1.companycode & "' and ratecode='" & Module1.sales_rate_code & "'"
                ob.insert(s)
                '---inserting the new rate---->
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    s = "insert into itemratemst (companycode,ratecode,itemcode,salesrate) values('" & Module1.companycode & "','" & Module1.sales_rate_code & "','" & ds.Tables(0).Rows(i).Item(1) & "','" & ds.Tables(0).Rows(i).Item(2) & "')"
                    ob.insert(s)
                Next
                '------end of module.count=7---------------------------->
            ElseIf Module1.count = 14 Then
                If Not ds.GetChanges Is Nothing Then
                    For i = 0 To dt.Tables(0).Rows.Count - 1
                        s1 = dt.Tables(0).Rows(i).Item(3)
                        s2 = dt.Tables(0).Rows(i).Item(4)
                        s = "select packing from itemmst where itemcode='" & dt.Tables(0).Rows(i).Item(6) & "' and companycode='" & Module1.companycode & "'"
                        count = Convert.ToDouble(ob.executereader(s))
                        count = (s2 * count) + s1
                        dt.Tables(0).Rows(i).Item(5) = count
                        s = "delete from openingstockmst where itemcode='" & dt.Tables(0).Rows(i).Item(6).ToString.ToUpper & "' and shopcode='" & Module1.shopcode & "' and companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "'"
                        ob.insert(s)
                        s = "insert into openingstockmst(shopcode,companycode,yearcode,itemcode,loose,box,qnty,trndate) values('" & Module1.shopcode & "','" & Module1.companycode & "','" & Module1.yearcode & "','" & dt.Tables(0).Rows(i).Item(6).ToString.ToUpper & "','" & dt.Tables(0).Rows(i).Item(3).ToString.ToUpper & "','" & dt.Tables(0).Rows(i).Item(4).ToString.ToUpper & "','" & dt.Tables(0).Rows(i).Item(5).ToString.ToUpper & "','" & Module1.comstdate & "')"
                        ob.insert(s)
                    Next
                End If
            End If
        Else
            MsgBox("Fields can not be blank.", MsgBoxStyle.Exclamation, "Blank Fields")
        End If  '----end of the if not textbox.text=nothing------------>
        If Module1.flag = 0 Then
            TextBox1.Select()
            Exit Sub
        ElseIf Module1.flag = 1 Then
            refresh_grid()
        ElseIf Module1.flag = 2 Then
            Me.Close()
            frm_MainForm.Enabled = True
        End If
        '---refreshing the form1 ---->
        frm_MainForm.mainformload()
    End Sub
    '---code behind the cancel button ----->
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub



    '    Private Sub salopcheck()
    '        Dim s As String
    '        Dim ob As New Class1
    '        If Module1.flag = 1 Then
    '            s = "select ratename from itemrateinfo where companycode='" & Module1.companycode & "' and ratename='" & TextBox1.Text & "'"
    '            s = ob.executereader(s)
    '            If s = Nothing Then
    '                salopname = False
    '            Else
    '                GoTo msg
    '            End If
    '        ElseIf Module1.flag = 2 Then
    '            If Module1.col2 <> TextBox1.Text Then
    '                s = "select ratename from itemrateinfo where companycode='" & Module1.companycode & "' and ratename='" & TextBox1.Text & "'"
    '                s = ob.executereader(s)
    '                If s = Nothing Then
    '                    salopname = False
    '                Else
    'msg:                MsgBox("Ratename Name already present.", MsgBoxStyle.Information, "Account Name")
    '                    salopname = True
    '                    Exit Sub
    '                End If
    '            Else
    '            End If
    '        End If
    '    End Sub

    '---key up event of the datagridview ---->
    Private Sub DataGridView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyUp
        If Module1.count = 14 Then
            If e.KeyData = Keys.Enter Then
                row = DataGridView1.CurrentCell.RowIndex - 1
                If Not row < 0 Then
                    s1 = DataGridView1.Item(3, row).Value
                    s2 = DataGridView1.Item(4, row).Value
                    s = "select packing from itemmst where itemcode='" & DataGridView1.Item(6, row).Value & "' and companycode='" & Module1.companycode & "'"
                    count = Convert.ToDouble(ob.executereader(s))
                    count = (s2 * count) + s1
                    DataGridView1.Item(5, row).Value = count
                End If
            End If
        End If
    End Sub
End Class
