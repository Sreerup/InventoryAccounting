Public Class frm_Opening_balance

    Dim ds1 As New DataSet
    Dim ds2 As New DataSet
    Dim ob As New Class1
    Dim s As String




    '--form load event --->
    Private Sub Opening_balance_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '---the dataset ds1 ids for filling the cmb_cashbank combobox--->
        s = "select ledcode,name from ledger where companycode='" & Module1.companycode & "' and ledcode='" & Module1.comcashacc & "' or ledcode='" & Module1.combankacc & "'"
        ds1 = ob.populate(s)
        '--the dataset ds2 is for filling the cmb_party combobox-->
        s = "select ledcode,name from ledger where companycode='" & Module1.companycode & "'and ledcode<>'" & Module1.comcashacc & "' or companycode='" & Module1.companycode & "' and ledcode<>'" & Module1.combankacc & "'"
        ds2 = ob.populate(s)
        '---filling up the combboxes with the respective ledger codes------>
        ob.combofill(ds1, ComboBox1)
        ob.combofill(ds2, ComboBox2)
        '--putting 0 as default in text box 1 and textbox2--->
        TextBox1.Text = "0"
        TextBox2.Text = "0"
    End Sub

    '----text box for debit balance --->
    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        '---putting zero in the credit text box when the user inputs something in the debit text box --->
        TextBox2.Text = "0"
    End Sub
    '---text box for credit balance --->
    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        '---putting zero in the debit text box when the user inputs something in the credit text box --->
        TextBox1.Text = "0"
    End Sub



End Class