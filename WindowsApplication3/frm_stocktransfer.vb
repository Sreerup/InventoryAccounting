Public Class frm_stocktransfer
    Dim ds1 As New DataSet
    Dim ds2 As New DataSet
    Dim ds As New DataSet


    Dim store_code1 As String
    Dim store_code2 As String

    Dim row As Integer

    Dim ob As New Class1
    Dim s As String

    Private Sub stocktransfer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load




        If Module1.flag = 1 Then

            s = "select companycode,shopcode,shopname from storage where companycode='" & Module1.companycode & "' "
            ds1 = ob.populate(s)
            For i = 0 To ds1.Tables(0).Rows.Count - 1
                ComboBox1.Items.Add(ds1.Tables(0).Rows(i).Item(2).ToString)
                ComboBox2.Items.Add(ds1.Tables(0).Rows(i).Item(2).ToString)
            Next
            ComboBox1.Text = ds1.Tables(0).Rows(0).Item(2).ToString
            ComboBox2.Text = ds1.Tables(0).Rows(0).Item(2).ToString

        ElseIf Module1.flag = 2 Then

            DateTimePicker1.Value = Module1.transaction_date
            ComboBox1.Text = Module1.storename1
            ComboBox2.Text = Module1.storename2
            TextBox1.Text = Module1.transaction

        End If
        fill_datagrid()

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        store_code1 = ds1.Tables(0).Rows(ComboBox1.SelectedIndex).Item(1)
        fill_datagrid()
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        store_code2 = ds1.Tables(0).Rows(ComboBox2.SelectedIndex).Item(1)
    End Sub


    Private Sub fill_datagrid()


        If Module1.flag = 1 Then

            s = "select itemcode,itemname,stock,companycode,storecode,yearcode from stock where companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "' and storecode='" & store_code1 & "' and  stock<>0"
            ds = ob.populate(s)
            ds.Tables(0).Columns.Add("loose_to_transfer")
            ds.Tables(0).Columns.Add("box")
            ds.Tables(0).Columns.Add("stock_to_transfer")
            DataGridView1.DataSource = ds.Tables(0)

            gettrn()

        ElseIf Module1.flag = 2 Then

            s = "select itemmst.itemcode,itemname,qnty,stk_transfer_main.companycode,shopcode_to,stk_transfer_main.yearcode,loose,box,'0' as stk_to_transfer from stk_transfer_detail join stk_transfer_main on stk_transfer_main.companycode=stk_transfer_detail.companycode and stk_transfer_main.yearcode=stk_transfer_detail.yearcode and stk_transfer_main.trnno=stk_transfer_detail.trnno join itemmst on itemmst.itemcode=stk_transfer_detail.itemcode and itemmst.companycode=stk_transfer_detail.companycode where stk_transfer_detail.trnno='" & Module1.transaction & "' and stk_transfer_detail.companycode='" & Module1.companycode & "' and stk_transfer_detail.yearcode='" & Module1.yearcode & "' and stk_transfer_main.shopcode_to='" & Module1.storecode2 & "'"
            ds = ob.populate(s)
            DataGridView1.DataSource = ds.Tables(0)

        End If
        DataGridView1.Columns(0).Visible = False
        DataGridView1.Columns(3).Visible = False
        DataGridView1.Columns(4).Visible = False
        DataGridView1.Columns(5).Visible = False
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub

    Private Function get_packing(ByVal code As String) As String
        s = "select packing from itemmst where itemcode='" & code & "' and companycode='" & Module1.companycode & "'"
        s = ob.executereader(s)
        Return s
    End Function



    Private Sub DataGridView1_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyUp

        row = DataGridView1.CurrentCell.RowIndex - 1

        If e.KeyData = Keys.Enter Then
            If row < 0 Then
                Exit Sub
            End If

            Dim loose As Integer
            Dim box As Integer
            Dim packing As Integer
            Dim qnty As Integer

            Try
                loose = DataGridView1.Item(6, row).Value
            Catch ex As InvalidCastException
                loose = 0
            End Try

            Try
                box = DataGridView1.Item(7, row).Value
            Catch ex As InvalidCastException
                box = 0
            End Try
            s = DataGridView1.Item(0, row).Value.ToString
            packing = Convert.ToDouble(get_packing(s))
            qnty = loose + box * packing
            DataGridView1.Item(8, row).Value = qnty



        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If ob.date_check(DateTimePicker1.Value.Date) = False Then
            Exit Sub
        End If



        If Module1.flag = 1 Then

            If Not store_code1 = store_code2 Then

                Dim dataset As New DataSet
                dataset = ds.GetChanges
                For i = 0 To dataset.Tables(0).Rows.Count - 1
                    s = "insert into stk_transfer_detail(companycode,itemcode,box,loose,qnty,trnno,trndate,yearcode) values('" & Module1.companycode & "','" & dataset.Tables(0).Rows(i).Item(0) & "','" & dataset.Tables(0).Rows(i).Item(7) & "','" & dataset.Tables(0).Rows(i).Item(6) & "','" & dataset.Tables(0).Rows(i).Item(8) & "','" & TextBox1.Text & "','" & DateTimePicker1.Value.Date & "','" & Module1.yearcode & "')"
                    ob.insert(s)
                Next
                s = "insert into stk_transfer_main(companycode,yearcode,trnno,trndate,shopname_from,shopname_to,shopcode_frm,shopcode_to) values('" & Module1.companycode & "','" & Module1.yearcode & "','" & TextBox1.Text & "','" & DateTimePicker1.Value.Date & "','" & ComboBox1.Text & "','" & ComboBox2.Text & "','" & store_code1 & "','" & store_code2 & "')"
                ob.insert(s)


            Else
                MsgBox("STOCK CANNOT BE TRANSFERRED TO AND FROM THE SAME STORE")
            End If


        ElseIf Module1.flag = 2 Then

            Dim dataset As New DataSet
            dataset = ds.GetChanges
            For i = 0 To dataset.Tables(0).Rows.Count - 1
                s = "update stk_transfer_detail set loose='" & dataset.Tables(0).Rows(i).Item(6) & "',box='" & dataset.Tables(0).Rows(i).Item(7) & "',qnty='" & dataset.Tables(0).Rows(i).Item(8) & "' where trnno='" & TextBox1.Text & "' and yearcode='" & Module1.yearcode & "' and companycode='" & Module1.companycode & "'"
                ob.insert(s)
            Next

        End If

        fill_datagrid()
        frm_MainForm.mainformload()

    End Sub

    Private Sub gettrn()

        Dim trn As New DataSet
        s = "select top 1 trnno from stk_transfer_main order by trnno desc"
        trn = ob.populate(s)
        If trn.Tables(0).Rows.Count = 0 Then
            TextBox1.Text = 1
        Else
            TextBox1.Text = Convert.ToInt32(trn.Tables(0).Rows(0).Item(0)) + 1
        End If

    End Sub



    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub


End Class