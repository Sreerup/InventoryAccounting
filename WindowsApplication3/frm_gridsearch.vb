Public Class frm_gridsearch
    Dim s As String
    Dim ob As New Class1
    '======================================
    Dim packing As Integer
    Dim loose As Integer
    Dim rate As String
    Dim box As Integer
    Dim quantity As Integer
    Dim amnt As Double
    '=====================================
    Dim row As Integer
    Dim column As Integer
    Dim data_table As New DataTable

    Private Sub gridsearch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        assign_grid_property()
        '---disabeling any entry in the courter sales grid----->
        Module1.countersales.DataGridView1.Enabled = False
    End Sub
    '-----assinging the property for the gridsearch form-------->
    Private Sub assign_grid_property()
        '----creating a datatable for the grid------------------->
        s = "select itemcode,itemname,barcode,packing,ml from itemmst where itemname like'" & TextBox1.Text & "%' and companycode='" & Module1.companycode & "'"
        data_table = ob.populate2(s)
        If data_table.Rows.Count <= 0 Then
            s = "select itemcode,itemname,barcode,packing,ml from itemmst where itemname like'%' and companycode='" & Module1.companycode & "'"
            data_table.Rows.Clear()
            data_table = ob.populate2(s)
        End If
        '---linking the data_table with the grid------------>
        DataGridView1.DataSource = data_table
        '----enabeling or desabeling views of certain parts of the grid view--->
        DataGridView1.Columns(0).Visible = False
        DataGridView1.Columns(3).Visible = False
        DataGridView1.Columns(1).HeaderText = "Item Name"
        DataGridView1.Columns(2).HeaderText = "Bar Code"
        '---disabeling the sorting of the grid--------------------------------->
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView1.Columns(1).SortMode = DataGridViewColumnSortMode.Programmatic
        DataGridView1.Columns(2).SortMode = DataGridViewColumnSortMode.Programmatic
        DataGridView1.Columns(3).SortMode = DataGridViewColumnSortMode.Programmatic
    End Sub
    '----if the textbox1_text changed event is called---->
    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        '--if there is a change in the text then the datatable is again populated accordin to the chnaged text--->
        assign_grid_property()
    End Sub
    '-----if the textbox1 key up event is called---->
    Private Sub TextBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyUp
        '---the row is selected 0 for the purpose of selecting the 
        '--first row from the datatable if the user presses enter in the textbox-->
        row = 0
        '--work to be done if the user presses enter in the text box-->
        If e.KeyData = Keys.Enter Then
            '--if the user has pressed enter in the gridview then the item is selectted and put to
            '--to the grid---->
            put_to_gid()
            '---selecting the datagridview if the user has pressed the key down-->
        ElseIf e.KeyData = Keys.Down Then
            DataGridView1.Select()
            DataGridView1.ClearSelection()
            DataGridView1.Item(1, 0).Selected = True
            '---closing the form if the user has pressed the escape key--->
        ElseIf e.KeyData = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    '---if an user double clicks on the cell------->
    Private Sub DataGridView1_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        '-----getting the value for the current row and the current column------->
        row = DataGridView1.CurrentCell.RowIndex
        column = DataGridView1.CurrentCell.ColumnIndex
        '----if the user has double clicked a perticular item then the function put_to_grid is called-->
        put_to_gid()
    End Sub
    '---datagridview1 key up event--------->
    Private Sub DataGridView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyUp
        '-----getting the value for the current row and the current column------->
        row = DataGridView1.CurrentCell.RowIndex - 1
        column = DataGridView1.CurrentCell.ColumnIndex
        If e.KeyData = Keys.Enter Then
            '--if the user has pressed enter in the gridview then the item is selectted and put to
            '--to the grid---->
            put_to_gid()
            '---if the user presses any alphabetical key--->
        ElseIf e.KeyData >= 65 And e.KeyData <= 90 Or e.KeyData >= 97 And e.KeyData <= 122 Or e.KeyData >= Keys.D0 And e.KeyData <= Keys.D9 Or e.KeyData = Keys.Space Then
            '---putting the respective character in the text box and selecting the text box-->
            TextBox1.Text = TextBox1.Text + Convert.ToChar(e.KeyData)
            '---code for erasing the texts written in the textbox---->
        ElseIf e.KeyData = Keys.Back And TextBox1.Text.Length > 0 Then
            TextBox1.Text = TextBox1.Text.Substring(0, TextBox1.Text.Length - 1)
            '---action when the user presses escape in the grid------>
        ElseIf e.KeyData = Keys.Escape Then
            '-----form closing----------------->
            Me.Close()
        End If
    End Sub
    '---function for putting the values in the countersale pos mode--->
    Private Sub put_to_gid()
        '---checking if the item to be inputed is already present in the grid or not---->
        s = data_table.Rows(row).Item(0)
        '---if no such items are present---------------------->
        If Not Module1.countersales.searchgrid(s) Then
            '---getting the rate for that item-------------->
            s = "select salesrate from vw_ratemst where ratecode='" & Module1.ratecode & "' and itemcode='" & data_table.Rows(row).Item(0) & "' and companycode='" & Module1.companycode & "'"
            rate = Convert.ToDouble(ob.executereader(s))
            '---feeling up the gridview--------------------->
            Module1.countersales.DataGridView1.Item(0, Module1.row).Value = data_table.Rows(row).Item(2).ToString
            Module1.countersales.DataGridView1.Item(1, Module1.row).Value = data_table.Rows(row).Item(1).ToString
            Module1.countersales.DataGridView1.Item(2, Module1.row).Value = rate
            Module1.countersales.DataGridView1.Item(3, Module1.row).Value = 1
            Module1.countersales.DataGridView1.Item(4, Module1.row).Value = 0
            Module1.countersales.DataGridView1.Item(5, Module1.row).Value = 1
            Module1.countersales.DataGridView1.Item(6, Module1.row).Value = rate
            Module1.countersales.DataGridView1.Item(7, Module1.row).Value = data_table.Rows(row).Item(0).ToString
            Module1.countersales.DataGridView1.Item(8, Module1.row).Value = data_table.Rows(row).Item(3).ToString
            Module1.countersales.DataGridView1.Item(9, Module1.row).Value = data_table.Rows(row).Item(4).ToString
        Else
            '----if an item with that itemcode already exists--------->
            loose = Module1.countersales.DataGridView1.Item(3, Module1.countersales.position).Value + 1
            packing = Module1.countersales.DataGridView1.Item(8, Module1.countersales.position).Value
            rate = Module1.countersales.DataGridView1.Item(2, Module1.countersales.position).Value
            box = Module1.countersales.DataGridView1.Item(4, Module1.countersales.position).Value
            quantity = loose + box * packing
            amnt = quantity * rate
            '----putting the respective values in the datagridview1---->
            Module1.countersales.DataGridView1.Item(3, Module1.countersales.position).Value = loose
            Module1.countersales.DataGridView1.Item(5, Module1.countersales.position).Value = quantity
            Module1.countersales.DataGridView1.Item(6, Module1.countersales.position).Value = amnt
        End If
        '-----closing the form after the work has been done---->
        Me.Close()
    End Sub
    '---form closing event------------->
    Private Sub gridsearch_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        '---checking if no item is present in the presen row and then removing it--->
        If Module1.countersales.DataGridView1.Item(7, Module1.row).Value = Nothing Then
            '---code for selecting and removing the present row------------------------------->
            Module1.countersales.DataGridView1.ClearSelection()
            Module1.countersales.DataGridView1.CurrentCell = Module1.countersales.DataGridView1.Item(0, Module1.row)
            Module1.countersales.DataGridView1.CurrentCell.Selected = True
            Module1.countersales.DataGridView1.Rows.Remove(Module1.countersales.DataGridView1.CurrentRow)
            '--selecting the next entry portion of the grid--->
            Module1.countersales.DataGridView1.ClearSelection()
            Module1.countersales.DataGridView1.CurrentCell = Module1.countersales.DataGridView1.Item(0, Module1.countersales.DataGridView1.Rows.Count - 1)
            Module1.countersales.DataGridView1.CurrentCell.Selected = True
        End If
        '---code for enabeling the countersales grid----------------->
        Module1.countersales.DataGridView1.Enabled = True
    End Sub
End Class