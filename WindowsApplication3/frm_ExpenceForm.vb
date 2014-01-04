Public Class frm_ExpenceForm

    '-----declaring the instance variables for the form --->
    Public voucher_date As DateTime
    Dim voucher_no As Integer
    Dim s As String
    Dim ob As New Class1


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text <> Nothing Then
            s = "insert into payment_main(companycode,yearcode                  ,vchno               ,amount_paid            ,vchdate               ,narration              ,head_account                   ,client_account              ,cheque_no,due,discount,net_due ,discount_account_head)" & _
               "values('" & Module1.companycode & "' ,'" & Module1.yearcode & "','" & voucher_no & "','" & TextBox1.Text & "','" & voucher_date & "','" & TextBox8.Text & "','" & Module1.comexpenceacc & "','" & Module1.comcashacc & "','0'      ,'0','0'     ,'0'     ,'" & Module1.comdiscacc & "')"
            ob.insert(s)
            s = "insert into payment_detail(companycode,yearcode                    ,vchno               ,trnno,debit                  ,credit,vchdate             ,due_amount) " & _
                    "values('" & Module1.companycode & "','" & Module1.yearcode & "','" & voucher_no & "','0'  ,'" & TextBox1.Text & "','0'   ,'" & Date.Today & "','0')"
            ob.insert(s)
            Me.Close()
        Else
            MsgBox("PLEASE INPUT AN AMOUNT TO PROCEED")
        End If
    End Sub

    '----load event of the expence form ----->
    Private Sub ExpenceForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '----calculating the voucher number for the perticular transaction----->
        s = "select top 1 vchno from payment_main where companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "' order by vchno desc"
        voucher_no = ob.executereader(s) + 1
    End Sub
End Class