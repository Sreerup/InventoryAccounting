Public Class frm_Create_rate_name

    Dim ob As New Class1
    Dim s As String


    Private Sub create_rate_name()
        '---inserting a new rate when the user chooses to create a new rate --->
        If Module1.flag = 1 Then
            '--getting the ratecode for the desired rate that the user wishes to create ---->
            s = "select top 1  ratecode from itemratemst where ratecode like '" & TextBox1.Text.Chars(0) & "%'order by ratecode desc "
            s = ob.getcode(s, TextBox1.Text)
            '---putting the ratecode in module1.col1-->
            Module1.sales_rate_code = s
            '----inserting the new rate info----->
            s = "insert into itemrateinfo(ratecode,ratename,companycode) values('" & s & "','" & TextBox1.Text.ToUpper & "','" & Module1.companycode & "')"
            ob.insert(s)
            '---updating the old rate namem --->
        ElseIf Module1.flag = 2 Then
            s = "update itemrateinfo set ratename='" & TextBox1.Text.ToUpper & "' where ratecode='" & Module1.sales_rate_code & "' and companycode='" & Module1.companycode & "'"
            ob.insert(s)
        End If



    End Sub

    '---form load event ----->
    Private Sub Create_rate_name_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        '---disabeling the form1 ------->
        frm_MainForm.Enabled = False
        '---selecting teh textbox1 as default --->
        TextBox1.Select()
        '---putting teh sales rate name in the text box when the user opens the form in the edit mode--->
        If Module1.flag = 2 Then
            TextBox1.Text = Module1.sales_rate_name
        End If
    End Sub

    '---from cloading event --->
    Private Sub Create_rate_name_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        frm_MainForm.Enabled = True
    End Sub

    '--key up event of the  form -->
    Private Sub Create_rate_name_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyData = Keys.Escape Then
            Me.Close()
        End If
    End Sub


    '--key down event for text box 1--->
    Private Sub TextBox1_Keydown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyData = Keys.Enter And Not TextBox1.Text = Nothing Then '---execution part only when the user presses enter -->
            '---calling the create ratename function --->
            create_rate_name()
            '---declaring the container form as th eparent form of the sales rate from -->
            frm_SalesRate.MdiParent = frm_ContainerForm
            '---openign the sales rate from -->
            frm_SalesRate.Show()
            '---putting the ratename in the sales rate form --->
            frm_SalesRate.TextBox1.Text = Me.TextBox1.Text
            '--refreshing the from 1--->
            frm_MainForm.mainformload()
            '--closing the create sales rate from --->
            Me.Close()
        End If
    End Sub


End Class