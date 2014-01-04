Public Class frm_breakage_gridsearch

    '---variable description for gridsearch--->
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
    Dim ds As New DataSet
    Dim dv As New DataView

    Private Sub breakage_gridsearch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '---assigning the default value for the instance variable--->
        row = 0
        '---disabeling any entry in the courter sales grid----->
        Module1.breakage.Enabled = False
        '---loading the dataset---->
        s = "select itemcode,itemname,barcode,packing,ml from itemmst where companycode='" & Module1.companycode & "'"
        ds = ob.populate(s)
        '--assigning the grid property---->
        refresh_datagrid()
    End Sub

    '-----assinging the property for the gridsearch form-------->
    Private Sub refresh_datagrid()
        '---creating a datview from dataset ds---->
        dv.Table = ds.Tables(0)
        dv.RowFilter = "itemname like '" & TextBox1.Text & "%'"
        '--linking the datview with the datagridview--->
        DataGridView1.DataSource = dv
        If DataGridView1.RowCount = 1 Then
            dv.RowFilter = "itemname like '%'"
            '--linking the datview with the datagridview--->
            DataGridView1.DataSource = dv
        End If
        '----enabeling or desabeling views of certain parts of the grid view--->
        DataGridView1.Columns(0).Visible = False
        DataGridView1.Columns(3).Visible = False
        DataGridView1.Columns(1).HeaderText = "Item Name"
        DataGridView1.Columns(2).HeaderText = "Bar Code"
        '---disabeling the sorting of the grid--------------------------------->
        DataGridView1.Columns(1).SortMode = DataGridViewColumnSortMode.Programmatic
        DataGridView1.Columns(2).SortMode = DataGridViewColumnSortMode.Programmatic
        DataGridView1.Columns(3).SortMode = DataGridViewColumnSortMode.Programmatic
        '---assigning the color for the datagridview--->
        DataGridView1.BackgroundColor = Color.Ivory
        DataGridView1.DefaultCellStyle.BackColor = Color.NavajoWhite
        DataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Ivory
        '---enabeling the visibility of the header--->
        DataGridView1.RowHeadersVisible = False
        DataGridView1.ColumnHeadersVisible = True
        '---assingningn the column size--->
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub
    '----if the textbox1_text changed event is called---->
    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        '--if there is a change in the text then the datatable is again populated accordin to the chnaged text--->
        '--this event will be fored before the form load event
        If ds.Tables.Count > 0 Then
            refresh_datagrid()
        End If
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
        Module1.breakage.DataGridView1.Item(0, Module1.breakage_row).Value = DataGridView1.Item(2, row).Value
        Module1.breakage.DataGridView1.Item(1, Module1.breakage_row).Value = DataGridView1.Item(1, row).Value
        Module1.breakage.DataGridView1.Item(2, Module1.breakage_row).Value = DataGridView1.Item(4, row).Value
        Module1.breakage.DataGridView1.Item(3, Module1.breakage_row).Value = 1
        Module1.breakage.DataGridView1.Item(4, Module1.breakage_row).Value = 0
        Module1.breakage.DataGridView1.Item(5, Module1.breakage_row).Value = 1
        Module1.breakage.DataGridView1.Item(6, Module1.breakage_row).Value = DataGridView1.Item(0, row).Value
        Module1.breakage.DataGridView1.Item(7, Module1.breakage_row).Value = DataGridView1.Item(3, row).Value
        Module1.breakage.DataGridView1.Item(8, Module1.breakage_row).Value = 0 '---kepping the default value for the check box false
        '-----closing the form after the work has been done---->
        Me.Close()
    End Sub
    '---form closing event------------->
    Private Sub gridsearch_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        '---checking if no item is present in the presen row and then removing it--->
        If Module1.breakage.DataGridView1.Item(7, Module1.breakage_row).Value = Nothing Then
            '---code for selecting and removing the present row------------------------------->
            Module1.breakage.DataGridView1.ClearSelection()
            Module1.breakage.DataGridView1.CurrentCell = Module1.breakage.DataGridView1.Item(0, Module1.breakage_row)
            Module1.breakage.DataGridView1.CurrentCell.Selected = True
            Module1.breakage.DataGridView1.Rows.Remove(Module1.breakage.DataGridView1.CurrentRow)
            '--selecting the next entry portion of the grid--->
            Module1.breakage.DataGridView1.ClearSelection()
            Module1.breakage.DataGridView1.CurrentCell = Module1.breakage.DataGridView1.Item(0, Module1.breakage.DataGridView1.Rows.Count - 1)
            Module1.breakage.DataGridView1.CurrentCell.Selected = True
        End If
        '---code for enabeling the countersales grid----------------->
        Module1.breakage.Enabled = True
    End Sub


End Class