Imports System.Data.SqlClient
Imports System.IO
Imports xcell = Microsoft.Office.Interop.Excel
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Text
Imports System.Windows.Forms

Public Class frm_MainForm

    Dim row As Integer
    Dim s As String
    Dim ob As Class1
    Dim obj As New Print_Class
    Public dsload As New DataSet
    Private MyDataGridViewPrinter As DataGridViewPrinter
    Dim dv As DataView

    '---form load event---->
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Height = frm_ContainerForm.Height - 108
        DataGridView1.Height = Me.Height - 70
        ob = New Class1
        frm.Close()
        mainformload()
        userset()
    End Sub

    Public Sub mainformload()
        '--initialising the row to 0----->
        row = 0
        '--clearing the columns of the datagridview before loading it with data---->
        DataGridView1.Columns.Clear()
        '---loading the categorymst----------->
        If Module1.count = 1 Then
            s = "select companycode,categorycode,categoryname from CategoryMst where companycode ='" & Module1.companycode & "'"
            '--loading the kfl mst---------->
        ElseIf Module1.count = 2 Then
            s = "select companycode,kflcode,kflname from KflMst where companycode='" & Module1.companycode & "'"
            '---loading the ml packing--->
        ElseIf Module1.count = 3 Then
            s = "select ml,packing from itemgroupml"
            '--loading the groupmst--->
        ElseIf Module1.count = 4 Then
            s = "select companycode,groupcode,groupname from groupMst where companycode ='" & Module1.companycode & "'"
            '---loading the itemmst-->
        ElseIf Module1.count = 5 Then
            s = "select groupmst.groupname,itemcode,itemname,barcode,categorymst.categoryname,kflmst.kflname,ml,packing,salesrate,strengthname,purchaserate from itemmst join groupmst on groupmst.groupcode=itemmst.groupcode and groupmst.companycode=itemmst.companycode join categorymst on categorymst.categorycode=itemmst.categorycode and categorymst.companycode=itemmst.companycode join kflmst on kflmst.kflcode=itemmst.kflcode and kflmst.companycode=itemmst.companycode where itemmst.companycode='" & Module1.companycode & "' order by itemname"
            '--loading the strength mst--->
        ElseIf Module1.count = 6 Then
            s = "select strengthname from strength"
            '----loading the rate mst--->
        ElseIf Module1.count = 7 Then
            s = "select ratecode,ratename,companycode from itemrateinfo where companycode ='" & Module1.companycode & "'"
            '----loading the companymst--->
        ElseIf Module1.count = 8 Then
            s = "select companycode,companyname,address1,address2,city,district,stat,pin,phn,email,website,faxno,lstno,cstno,panno,vatno,stno from companymst order by companyname"
            '---loading the shopmst or the opening stockmst--->
        ElseIf Module1.count = 10 Or Module1.count = 14 Then
            s = "select companycode,shopcode,shopname,address from storage where companycode='" & Module1.companycode & "'"
            '--loading the acount main group--->
        ElseIf Module1.count = 11 Then
            s = "select acgroupcode,acgroupname,actype from acgroupmst"
            '--loading the acount sub group--->
        ElseIf Module1.count = 12 Then
            s = "select accode,acname from acountname"
            '--loading the ledgermst--->
        ElseIf Module1.count = 13 Then
            s = "select ledcode,name,acountname.acname,debit,credit,contperson,city,district,state,phone,email,www,address1,address2,area,vatno,panno,acountname.accode  from ledger join acountname on acountname.accode=ledger.accode where ledger.companycode='" & Module1.companycode & "'"
            '---loading the countersale mst--->
        ElseIf Module1.count = 15 Then
            s = "select trndate,name,trnno,amount,discamount,surcharge_amount,netamount,salestype,ratename,shopname,ledgercode,storecode,ratecode,date from vw_salesbill_main where companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "' order by trnno desc"
            '--loading the breakagemst--->
        ElseIf Module1.count = 16 Then
            s = "select convert(varchar(10),trndate,103) as trndate,trnno,billno,tp_pass_no,shopname,name,narration,breakagemain.shopcode,ledgercode,breakagemain.companycode,yearcode,trndate as date from breakagemain join storage on breakagemain.shopcode=storage.shopcode and breakagemain.companycode=storage.companycode join ledger on breakagemain.ledgercode=ledger.ledcode and breakagemain.companycode=ledger.companycode where breakagemain.companycode='" & Module1.companycode & "' and breakagemain.yearcode='" & Module1.yearcode & "'"
            '---loading the accesslevel--->
        ElseIf Module1.count = 21 Then
            s = "select usercode,username,accesslevel,password from id where usercode!='A00001'"   'where companycode='" & Module1.companycode & "'"
            '---loadin gthe accesslevel--->
        ElseIf Module1.count = 22 Then
            s = "select distinct id.usercode,id.username,userrights.companycode,companymst.companyname,id.accesslevel,userrights.accaccess from userrights join id on id.usercode=userrights.usercode join companymst on companymst.companycode=userrights.companycode where userrights.usercode !='A00001'"
            '--loading the purchase mst--->
        ElseIf Module1.count = 23 Then
            s = "select purchasemain.trnno,ledger.name,convert(varchar(10),trndate,103) as trndate,docno,ptype,totnetamt from purchasemain join ledger on ledger.companycode=purchasemain.companycode and ledger.ledcode=purchasemain.suppliercode  where purchasemain.companycode='" & Module1.companycode & "' and purchasemain.yearcode='" & Module1.yearcode & "' order by purchasemain.trnno desc"
            '--loading the taxmst--->
        ElseIf Module1.count = 24 Then
            s = "select distinct taxdetail.companycode,companymst.companyname,schemecode,taxdetail.schemename from taxdetail join companymst on companymst.companycode=taxdetail.companycode where taxdetail.companycode='" & Module1.companycode & "'"
            '---loading the stock transfer----->
        ElseIf Module1.count = 25 Then
            s = "select companycode,yearcode,trnno,convert(varchar(10),trndate,103) as trndate,shopname_from,shopname_to,shopcode_frm,shopcode_to,trndate as date from stk_transfer_main where companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "'"
            '---loading the receipt voucher--->
        ElseIf Module1.count = 26 Then
            s = "select convert(varchar(10),vchdate,103) as vchdate,vchno,head_account,client_account,cheque_no,due,amount_paid,discount,net_due,narration,companycode,yearcode,head_account_code,client_account_code from vw_receipt_main where companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "'"
            '--loading the payment voucher------->
        ElseIf Module1.count = 27 Then
            s = "select convert(varchar(10),vchdate,103) as vchdate,vchno,head_account,client_account,cheque_no,due,amount_paid,discount,net_due,narration,companycode,yearcode,head_account_code,client_account_code from vw_payment_main where companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "'"
        End If

        '-----loading the datagridview1 with the dataset------------------> 
        dsload = ob.populate(s)
        DataGridView1.Columns.Clear()
        DataGridView1.DataSource = dsload.Tables(0)

        '-----------assigning the properties------------------------------>
        frm_ContainerForm.ToolStripStatusLabel6.Text = "Total Records : " + dsload.Tables(0).Rows.Count.ToString
        hidecolumn()
        DataGridView1.Select()
    End Sub
    '---searching option for in the textbox8 key up event-->
    Private Sub TextBox3_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox3.KeyUp
        dv = New DataView
        dv.Table = dsload.Tables(0)
        '----Search for the category mst--->
        If Module1.count = 1 Then
            dv.RowFilter = "categoryname like '" & TextBox3.Text & "%'"
            '---search for the kfl mst-->
        ElseIf Module1.count = 2 Then
            dv.RowFilter = "kflname like '" & TextBox3.Text & "%'"
            '--search for the ml paking mst---><
        ElseIf Module1.count = 3 Then
            dv.RowFilter = " Convert(ml,System.String) like '" & TextBox3.Text & "%' or Convert(packing,System.String) like '" & TextBox3.Text & "%'"
            '---search for the group mst-->
        ElseIf Module1.count = 4 Then
            dv.RowFilter = "groupname like '" & TextBox3.Text & "%'"
            '---search for the itemmst--->
        ElseIf Module1.count = 5 Then
            dv.RowFilter = "itemname like '" & TextBox3.Text & "%' or categoryname like '" & TextBox3.Text & "%'  or kflname like '" & TextBox3.Text & "%' or Convert(ml,System.String) like '" & TextBox3.Text & "%' or strengthname like '" & TextBox3.Text & "%'"
            '---search for the strengthmst--->
        ElseIf Module1.count = 6 Then
            dv.RowFilter = "strengthname like '" & TextBox3.Text & "%'"
            '---search for the rate mst--->
        ElseIf Module1.count = 7 Then
            dv.RowFilter = "ratename like '" & TextBox3.Text & "%'"
            '--search for the companymst--->
        ElseIf Module1.count = 8 Then
            dv.RowFilter = "companyname like '" & TextBox3.Text & "%'"
            '--search for the shopmst-->
        ElseIf Module1.count = 10 Or Module1.count = 14 Then
            dv.RowFilter = "shopname like '" & TextBox3.Text & "%' or address like '" & TextBox3.Text & "%'"
            '--search for the acount main group--->
        ElseIf Module1.count = 11 Then
            dv.RowFilter = "acgroupname like '" & TextBox3.Text & "%' or actype like '" & TextBox3.Text & "%'"
            '--search for the acount sub group-->
        ElseIf Module1.count = 12 Then
            dv.RowFilter = "acname like '" & TextBox3.Text & "%'"
            '--search for the ledger--->
        ElseIf Module1.count = 13 Then
            dv.RowFilter = "name like '" & TextBox3.Text & "%'"
        ElseIf Module1.count = 14 Then
            '---search for the countersale mst-->
        ElseIf Module1.count = 15 Then
            dv.RowFilter = "name like '" & TextBox3.Text & "%' or trnno ='" & Val(TextBox3.Text) & "' or amount = '" & Val(TextBox3.Text) & "' or salestype like '" & TextBox3.Text & "%' or convert(trndate,System.String) like'*/" & TextBox3.Text & "/*%'"
        ElseIf Module1.count = 16 Then
        ElseIf Module1.count = 17 Then
        ElseIf Module1.count = 18 Then
        ElseIf Module1.count = 19 Then
        ElseIf Module1.count = 20 Then
            '--search for the accesslable-->
        ElseIf Module1.count = 21 Or Module1.count = 22 Then
            dv.RowFilter = "username like '" & TextBox3.Text & "%'"
            '--search for the purchase mst-->
        ElseIf Module1.count = 23 Then
            dv.RowFilter = "name like '" & TextBox3.Text & "%' or docno = '" & TextBox3.Text & "'"
            '---search for the tax mst--->
        ElseIf Module1.count = 24 Then
            dv.RowFilter = "schemename like '" & TextBox3.Text & "%' or companyname like '" & TextBox3.Text & "%'"
        ElseIf Module1.count = 25 Then

        End If
        DataGridView1.DataSource = dv
        hidecolumn()
        frm_ContainerForm.mnu_closing_stock.Text = Module1.companyname
    End Sub
    '---add buttion click event handler-->
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Module1.flag = 1
        Module1.flag1 = 0
        formshow()
    End Sub
    '---edit button click event handler-->
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Module1.flag = 2
        initialise()
        formshow()
    End Sub
    '--delete button click event handler--->
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Module1.flag = 3
        Dim d = MsgBox("Do you want to delete the record permanently?", MsgBoxStyle.YesNo, "Delete Record?")
        If d = 6 Then
            If row < dsload.Tables(0).Rows.Count Then
                '---deleting from categorymst--->
                If Module1.count = 1 Then
                    s = "delete from CategoryMst where categoryname='" & DataGridView1.Item(2, row).Value.ToString & "' and companycode='" & Module1.companycode & "'"
                    ob.insert(s)
                    '---deleting form kflmst---->
                ElseIf Module1.count = 2 Then
                    s = "delete from kflMst where kflname='" & DataGridView1.Item(2, row).Value.ToString & "' and companycode='" & Module1.companycode & "'"
                    ob.insert(s)
                    '---deleting from gropmst--->
                ElseIf Module1.count = 4 Then
                    s = "delete from groupMst where groupname='" & DataGridView1.Item(2, row).Value.ToString & "' and companycode='" & Module1.companycode & "'"
                    ob.insert(s)
                    '---delete form itemgroupml--->
                ElseIf Module1.count = 3 Then
                    s = "delete from itemgroupml where   ml='" & DataGridView1.Item(0, row).Value.ToString & "'"
                    ob.insert(s)
                    '---delete from itemmst--->
                ElseIf Module1.count = 5 Then
                    s = "delete from itemmst where itemname='" & DataGridView1.Item(2, row).Value.ToString & "' and companycode='" & Module1.companycode & "'"
                    ob.insert(s)
                    s = "delete from itemratemst where itemcode='" & DataGridView1.Item(1, row).Value.ToString & "' and companycode='" & Module1.companycode & "'"
                    ob.insert(s)
                    '---deleting form strengthmst--->
                ElseIf Module1.count = 6 Then
                    s = "delete from strength where strengthname='" & DataGridView1.Item(0, row).Value.ToString & "'"
                    ob.insert(s)
                    '-deleting form itemrateinfo--->
                ElseIf Module1.count = 7 Then
                    s = "delete from itemrateinfo where ratename='" & DataGridView1.Item(1, row).Value.ToString & "' and companycode='" & Module1.companycode & "'"
                    ob.insert(s)
                    '---deleting form companymst---->
                ElseIf Module1.count = 8 Then
                    s = "delete from companymst where companycode='" & DataGridView1.Item(0, row).Value.ToString & "'"
                    ob.insert(s)
                    If Module1.flag <> 0 Then
                        Dim ds0 As New DataSet
                        s = "select top 1 companyname from companymst"
                        ds0 = ob.populate(s)
                        If Not ds0.Tables(0).Rows.Count > 0 Or DataGridView1.Item(1, row).Value.ToString = Module1.companyname Then
                            Me.Close()
                            frm_ContainerForm.comcheck()
                        End If
                    End If
                    '--deleting from storage--->
                ElseIf Module1.count = 10 Or Module1.count = 14 Then
                    s = "delete from storage where shopcode='" & DataGridView1.Item(1, row).Value.ToString & "' and companycode='" & Module1.companycode & "'"
                    ob.insert(s)
                    '---deleting form acountname--->
                ElseIf Module1.count = 12 Then
                    s = "delete from acountname where accode='" & DataGridView1.Item(0, row).Value.ToString & "'"
                    ob.insert(s)
                    '--deleting from ledger--->
                ElseIf Module1.count = 13 Then
                    s = "delete from ledger where name='" & DataGridView1.Item(1, row).Value.ToString & "'"
                    ob.insert(s)
                    '---deleting form salesbilldetail and salesbillmain--->
                ElseIf Module1.count = 15 Then
                    '---deleting from the receipt part----->
                    s = "delete receipt_detail from receipt_detail join receipt_main on receipt_detail.companycode=receipt_main.companycode and receipt_detail.yearcode=receipt_main.yearcode and receipt_detail.vchno=receipt_main.vchno where head_account='" & Module1.comsaleacc & "' and receipt_main.companycode='" & Module1.companycode & "' and receipt_main.yearcode='" & Module1.yearcode & "'"
                    ob.insert(s)
                    s = "delete receipt_main from receipt_main join receipt_detail on receipt_main.companycode=receipt_detail.companycode and receipt_main.yearcode=receipt_detail.yearcode and receipt_main.vchno=receipt_detail.vchno join salesbillmain on receipt_detail.companycode=salesbillmain.companycode and receipt_detail.yearcode=salesbillmain.yearcode and receipt_detail.trnno=salesbillmain.trnno where salesbillmain.trnno='" & DataGridView1.Item(2, row).Value.ToString & "' and salesbillmain.yearcode='" & Module1.yearcode & "' and salesbillmain.companycode='" & Module1.companycode & "' and head_account='" & Module1.comsaleacc & "'"
                    ob.insert(s)
                    '---deleting form the salesbilldetail and salesbillmain--->
                    s = "delete from salesbilldetail where trnno='" & DataGridView1.Item(2, row).Value.ToString & "' and companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "'"
                    ob.insert(s)
                    s = "delete from salesbillmain where trnno='" & DataGridView1.Item(2, row).Value.ToString & "' and companycode='" & Module1.companycode & "' and yearcode ='" & Module1.yearcode & "'"
                    ob.insert(s)
                    s = "delete from sales_tax where trnno='" & DataGridView1.Item(2, row).Value.ToString & "' and companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "'"
                    ob.insert(s)
                    '---deleting form breakagedetail and breakagemain ------->
                ElseIf Module1.count = 16 Then
                    s = "delete from breakagedetail where trnno='" & DataGridView1.Item(1, row).Value.ToString & "' and companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "'"
                    ob.insert(s)
                    s = "delete from breakagemain where trnno='" & DataGridView1.Item(1, row).Value.ToString & "' and companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "'"
                    ob.insert(s)
                    '---deleting form user id--->
                ElseIf Module1.count = 21 Then
                    If dsload.Tables(0).Rows(row).Item(0) = "A00001" Then
                        MsgBox("It is the System Admin user, It can not be deleted.", MsgBoxStyle.Critical, "Delete")
                    Else
                        s = "delete from id where usercode='" & DataGridView1.Item(0, row).Value.ToString & "'"
                        ob.insert(s)
                    End If
                    '---deleting form user rights--->
                ElseIf Module1.count = 22 Then
                    If dsload.Tables(0).Rows(row).Item(0) = "A00001" Then
                        MsgBox("It is the System Admin user, It can not be deleted.", MsgBoxStyle.Critical, "Delete")
                    Else
                        s = "delete from userrights where usercode='" & DataGridView1.Item(0, row).Value.ToString & "'and companycode='" & DataGridView1.Item(2, row).Value.ToString & "'"
                        ob.insert(s)
                    End If
                    '---deleting from purchasetax detail,purchasebillmain and purchasebilldetail--->
                ElseIf Module1.count = 23 Then
                    s = "delete from purchasetaxdetail where companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "' and trnno='" & DataGridView1.Item(0, row).Value.ToString & "'"
                    ob.insert(s)
                    s = "delete from purchasedetail where companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "' and trnno='" & DataGridView1.Item(0, row).Value.ToString & "'"
                    ob.insert(s)
                    s = "delete from purchasemain where companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "' and trnno='" & DataGridView1.Item(0, row).Value.ToString & "'"
                    ob.insert(s)
                    '---deleting form tax detail--->
                ElseIf Module1.count = 24 Then
                    s = "delete from taxdetail where schemecode='" & DataGridView1.Item(2, row).Value.ToString & "'and companycode='" & DataGridView1.Item(0, row).Value.ToString & "'"
                    ob.insert(s)
                    '--deleting form stock transfermain and stock transfer main-->
                ElseIf Module1.count = 25 Then
                    s = "delete from stk_transfer_detail where trnno='" & DataGridView1.Item(2, row).Value.ToString & "' and companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "'"
                    ob.insert(s)
                    s = "delete from stk_transfer_main where trnno='" & DataGridView1.Item(2, row).Value.ToString & "' and companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "'"
                    ob.insert(s)
                    '---deleting form receiptdetail_detail and  receipt_main--->
                ElseIf Module1.count = 26 Then
                    s = "delete from receipt_detail where vchno='" & DataGridView1.Item(1, row).Value.ToString & "' and companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "'"
                    ob.insert(s)
                    s = "delete from receipt_main where vchno='" & DataGridView1.Item(1, row).Value.ToString & "' and companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "'"
                    ob.insert(s)
                    '---deleting from payment_detail and pay_ment_main--->
                ElseIf Module1.count = 27 Then
                    s = "delete from payment_detail where vchno='" & DataGridView1.Item(1, row).Value.ToString & "' and companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "'"
                    ob.insert(s)
                    s = "delete from payment_main where vchno='" & DataGridView1.Item(1, row).Value.ToString & "' and companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "'"
                    ob.insert(s)
                End If
            End If
            mainformload()
        End If
    End Sub

    Private Sub DataGridView1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.Click
        If dsload.Tables(0).Rows.Count > 0 Then
            row = DataGridView1.CurrentCell.RowIndex
        End If
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Module1.flag = 2
        initialise()
        formshow()
    End Sub

    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyData = Keys.Enter Then
            Module1.flag = 2
            initialise()
            formshow()
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub DataGridView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyUp
        If e.KeyData = Keys.Up Or e.KeyData = Keys.Down Then
            row = DataGridView1.CurrentCell.RowIndex
        End If
    End Sub
    '---initialising te varible when the the child forms are opend in the editing mode---->
    Public Sub initialise()
        If dsload.Tables.Count > 0 Then
            If row < dsload.Tables(0).Rows.Count And dsload.Tables.Count > 0 Then
                '---initialisation for the categorymst,kflmst and groupmst-->
                If Module1.count = 1 Or Module1.count = 2 Or Module1.count = 4 Then
                    Module1.col1 = DataGridView1.Item(1, row).Value.ToString
                    Module1.col2 = DataGridView1.Item(2, row).Value.ToString
                    '---initialisation for ml and packing-->
                ElseIf Module1.count = 3 Then
                    itemmstmodule.ml = DataGridView1.Item(0, row).Value.ToString
                    itemmstmodule.packing = DataGridView1.Item(1, row).Value.ToString
                    '---initialisation for strength--->
                ElseIf Module1.count = 6 Then
                    itemmstmodule.strengthname = DataGridView1.Item(0, row).Value.ToString
                    '--initialisation fo rthe itemmst--->
                ElseIf Module1.count = 5 Then
                    itemmstmodule.groupname = DataGridView1.Item(0, row).Value.ToString
                    itemmstmodule.itemcode = DataGridView1.Item(1, row).Value.ToString
                    itemmstmodule.name = DataGridView1.Item(2, row).Value.ToString
                    itemmstmodule.barcode = DataGridView1.Item(3, row).Value.ToString
                    itemmstmodule.categoryname = DataGridView1.Item(4, row).Value.ToString
                    itemmstmodule.kflname = DataGridView1.Item(5, row).Value.ToString
                    itemmstmodule.ml = DataGridView1.Item(6, row).Value.ToString
                    itemmstmodule.packing = DataGridView1.Item(7, row).Value.ToString
                    itemmstmodule.salesrate = DataGridView1.Item(8, row).Value.ToString
                    itemmstmodule.strengthname = DataGridView1.Item(9, row).Value.ToString
                    itemmstmodule.purchaserate = DataGridView1.Item(10, row).Value.ToString
                    '---initiaLisation for the ratemst--->
                ElseIf Module1.count = 7 Then
                    Module1.sales_rate_code = DataGridView1.Item(0, row).Value.ToString
                    Module1.sales_rate_name = DataGridView1.Item(1, row).Value.ToString
                    '---initialisation fo rthe companymst--->
                ElseIf Module1.count = 8 Then
                    Module1.comcode = DataGridView1.Item(0, row).Value.ToString
                    Module1.comname = DataGridView1.Item(1, row).Value.ToString
                    Module1.address1 = DataGridView1.Item(2, row).Value.ToString
                    Module1.address2 = DataGridView1.Item(3, row).Value.ToString
                    Module1.city = DataGridView1.Item(4, row).Value.ToString
                    Module1.district = DataGridView1.Item(5, row).Value.ToString
                    Module1.state = DataGridView1.Item(6, row).Value.ToString
                    Module1.pin = DataGridView1.Item(7, row).Value.ToString
                    Module1.phone = DataGridView1.Item(8, row).Value.ToString
                    Module1.email = DataGridView1.Item(9, row).Value.ToString
                    Module1.www = DataGridView1.Item(10, row).Value.ToString
                    Module1.fax = DataGridView1.Item(11, row).Value.ToString
                    Module1.lstno = DataGridView1.Item(12, row).Value.ToString
                    Module1.cstno = DataGridView1.Item(13, row).Value.ToString
                    Module1.panno = DataGridView1.Item(14, row).Value.ToString
                    Module1.vatno = DataGridView1.Item(15, row).Value.ToString
                    Module1.stno = DataGridView1.Item(16, row).Value.ToString
                    '---initialisation fo rthe shopmst and the openingstockmst--->
                ElseIf Module1.count = 10 Or Module1.count = 14 Then
                    Module1.shopcode = DataGridView1.Item(1, row).Value.ToString
                    Module1.col1 = DataGridView1.Item(2, row).Value.ToString
                    Module1.col2 = DataGridView1.Item(3, row).Value.ToString
                    '---initialisation for the account sub group--->
                ElseIf Module1.count = 12 Then
                    Module1.account_code = DataGridView1.Item(0, row).Value.ToString
                    Module1.account_name = DataGridView1.Item(1, row).Value.ToString
                    '---initialisation for the ledgermst---->
                ElseIf Module1.count = 13 Then
                    Module1.ledgercode = DataGridView1.Item(0, row).Value.ToString
                    Module1.ledgername = DataGridView1.Item(1, row).Value.ToString
                    Module1.acountname = DataGridView1.Item(2, row).Value.ToString
                    Module1.debit = DataGridView1.Item(3, row).Value.ToString
                    Module1.credit = DataGridView1.Item(4, row).Value.ToString
                    Module1.contactperson = DataGridView1.Item(5, row).Value.ToString
                    Module1.city = DataGridView1.Item(6, row).Value.ToString
                    Module1.district = DataGridView1.Item(7, row).Value.ToString
                    Module1.state = DataGridView1.Item(8, row).Value.ToString
                    Module1.phone = DataGridView1.Item(9, row).Value.ToString
                    Module1.email = DataGridView1.Item(10, row).Value.ToString
                    Module1.www = DataGridView1.Item(11, row).Value.ToString
                    Module1.address1 = DataGridView1.Item(12, row).Value.ToString
                    Module1.address2 = DataGridView1.Item(13, row).Value.ToString
                    Module1.area = DataGridView1.Item(14, row).Value.ToString
                    Module1.vatno = DataGridView1.Item(15, row).Value.ToString
                    Module1.panno = DataGridView1.Item(16, row).Value.ToString
                    Module1.acountcode = DataGridView1.Item(17, row).Value.ToString
                    '----initialisation for the countersales---->
                ElseIf Module1.count = 15 Then
                    Module1.ledgercode = DataGridView1.Item(10, row).Value.ToString
                    Module1.shopcode = DataGridView1.Item(11, row).Value.ToString
                    Module1.ratecode = DataGridView1.Item(12, row).Value.ToString
                    '---getting the ledgername,shopname and the raename------>
                    Module1.ledgername = DataGridView1.Item(1, row).Value.ToString
                    Module1.ratename = DataGridView1.Item(8, row).Value.ToString
                    Module1.shopname = DataGridView1.Item(9, row).Value.ToString
                    '----getting the amount and everything------->
                    Module1.transaction = DataGridView1.Item(2, row).Value.ToString
                    Module1.amount = DataGridView1.Item(3, row).Value.ToString
                    Module1.discount = DataGridView1.Item(4, row).Value.ToString
                    Module1.surcharge_amount = DataGridView1.Item(5, row).Value.ToString
                    Module1.salestype = DataGridView1.Item(7, row).Value.ToString
                    Module1.transaction_date = DataGridView1.Item(13, row).Value.ToString
                    '---initialisation for the breakage---->
                ElseIf Module1.count = 16 Then
                    Module1.breakage_trn = DataGridView1.Item(1, row).Value.ToString
                    Module1.breakage_bill_no = DataGridView1.Item(2, row).Value.ToString
                    Module1.breakage_tp_pass_no = DataGridView1.Item(3, row).Value.ToString
                    Module1.breakage_store_name = DataGridView1.Item(4, row).Value.ToString
                    Module1.breakage_party_name = DataGridView1.Item(5, row).Value.ToString
                    Module1.breakage_narration = DataGridView1.Item(6, row).Value.ToString
                    Module1.breakage_trndate = DataGridView1.Item(11, row).Value.ToString
                ElseIf Module1.count = 21 Then
                ElseIf Module1.count = 22 Then
                    '----initialisation for the purchasemst--->
                ElseIf Module1.count = 23 Then
                    Module1.col1 = DataGridView1.Item(0, row).Value.ToString
                    Module1.col2 = DataGridView1.Item(3, row).Value.ToString
                    '---initialisatin for the taxmst--->
                ElseIf Module1.count = 24 Then
                    Module1.col1 = DataGridView1.Item(2, row).Value.ToString
                    Module1.col2 = DataGridView1.Item(3, row).Value.ToString
                    '---initialisation for the stocktransfer-->
                ElseIf Module1.count = 25 Then
                    Module1.transaction = DataGridView1.Item(2, row).Value.ToString
                    Module1.storecode1 = DataGridView1.Item(6, row).Value.ToString
                    Module1.storecode2 = DataGridView1.Item(7, row).Value.ToString
                    Module1.storename1 = DataGridView1.Item(4, row).Value.ToString
                    Module1.storename2 = DataGridView1.Item(5, row).Value.ToString
                    Module1.transaction_date = DataGridView1.Item(8, row).Value.ToString
                    '---initialisation for the receipt voucher and payment voucher--->
                ElseIf Module1.count = 26 Or Module1.count = 27 Then
                    Module1.vch_date = DataGridView1.Item(0, row).Value.ToString
                    Module1.vchno = DataGridView1.Item(1, row).Value.ToString
                    Module1.vch_head_name = DataGridView1.Item(2, row).Value.ToString
                    Module1.vch_client_name = DataGridView1.Item(3, row).Value.ToString
                    Module1.vch_cheque_no = DataGridView1.Item(4, row).Value.ToString
                    If Not DataGridView1.Item(5, row).Value.ToString = Nothing Then : Module1.vch_due = DataGridView1.Item(5, row).Value : End If
                    If Not DataGridView1.Item(6, row).Value.ToString = Nothing Then : Module1.vch_amt_paid = DataGridView1.Item(6, row).Value : End If
                    If Not DataGridView1.Item(7, row).Value.ToString = Nothing Then : Module1.vch_discount = DataGridView1.Item(7, row).Value : End If
                    If Not DataGridView1.Item(8, row).Value.ToString = Nothing Then : Module1.vch_net_due = DataGridView1.Item(8, row).Value : End If
                    Module1.vch_narration = DataGridView1.Item(9, row).Value.ToString
                    Module1.vch_head_account = DataGridView1.Item(12, row).Value.ToString
                    Module1.vch_client_account = DataGridView1.Item(13, row).Value.ToString
                End If
            Else
                Dim r = MsgBox("No record for edit. Do you want to add new records?", MsgBoxStyle.YesNo, "Edit")
                If r = 6 Then
                    Module1.flag = 1
                Else
                    Module1.flag = 0
                End If
            End If
        End If
    End Sub
    '---sub declared for opening the forms-->
    Private Sub formshow()
        Dim frm As New Form
        '---calling the add edit form by the users need----------------------------->
        If Module1.count = 1 Or Module1.count = 2 Or Module1.count = 4 Or Module1.count = 6 Then
            frm = frm_bedit
            frm.MdiParent = frm_ContainerForm
            frm.Show()
            Me.Enabled = False
        ElseIf Module1.count = 3 Then
            frm = frm_storage
            frm.MdiParent = frm_ContainerForm
            frm.Show()
            Me.Enabled = False
            '---opening the form for adding or editing the itemmst --->
        ElseIf Module1.count = 5 Then
            frm = frm_itemmstadd
            frm.MdiParent = frm_ContainerForm
            frm.Show()
            Me.Enabled = False
            '---opening the form for the adding or editing the sales rate --->
        ElseIf Module1.count = 7 Then
            frm = frm_Create_rate_name
            frm.MdiParent = frm_ContainerForm
            frm.Show()
            '--openign the form for creating or editing the companymst--->
        ElseIf Module1.count = 8 Then
            frm = frm_createcompany
            frm.MdiParent = frm_ContainerForm
            frm.Show()
            Me.Enabled = False
        ElseIf Module1.count = 9 Then
        ElseIf Module1.count = 10 Then
            frm = frm_storage
            frm.MdiParent = frm_ContainerForm
            frm.Show()
            Me.Enabled = False
        ElseIf Module1.count = 11 Then
            Me.Enabled = True
            '----form ahow for account sub groub or acount table ----->
        ElseIf Module1.count = 12 Then
            frm = frm_accreate
            frm.MdiParent = frm_ContainerForm
            frm.Show()
            Me.Enabled = False
        ElseIf Module1.count = 13 Then
            frm = frm_ledger
            frm.MdiParent = frm_ContainerForm
            frm.Show()
            Me.Enabled = False
        ElseIf Module1.count = 14 And Module1.flag = 2 Then
            frm = frm_SalesRate
            frm.MdiParent = frm_ContainerForm
            frm.Show()
            Me.Enabled = False
            '--form openimg for the counter sales entry in the pos mode--->
        ElseIf Module1.count = 15 And Not Module1.compos = "1" Then
            Module1.countersales = New frm_countersales
            Module1.countersales.Show()
            '----form showing for breakage entry--->
        ElseIf Module1.count = 16 Then
            Module1.breakage = New frm_breakageentry
            Module1.breakage.MdiParent = frm_ContainerForm
            Module1.breakage.Show()
        ElseIf Module1.count = 21 Then
            frm = frm_userinfo
            frm.MdiParent = frm_ContainerForm
            frm.Show()
            Me.Enabled = False
        ElseIf Module1.count = 22 Then
            frm = frm_userrights
            frm.MdiParent = frm_ContainerForm
            frm.Show()
            Me.Enabled = False
        ElseIf Module1.count = 23 Then
            frm = frm_purchase
            frm.Show()
        ElseIf Module1.count = 24 Then
            frm = frm_Taxscm
            frm.MdiParent = frm_ContainerForm
            frm.Show()
            Me.Enabled = False
        ElseIf Module1.count = 25 Then
            frm = frm_stocktransfer
            frm.Show()
        ElseIf Module1.count = 26 Then
            frm = frm_receipt_voucher
            frm.Show()
        ElseIf Module1.count = 27 Then
            frm = frm_payment_voucher
            frm.Show()
        End If

        If Module1.flag = 0 Then
            frm.Close()
        End If
    End Sub

    '------------hiddin some columns from the user---------------------------->
    Public Sub hidecolumn()
        If Module1.count = 1 Then
            DataGridView1.Columns(0).Visible = False
            DataGridView1.Columns(1).Visible = False
            DataGridView1.Columns(2).HeaderText = "Brand Name"
            DataGridView1.Columns(2).SortMode = DataGridViewColumnSortMode.Programmatic
        ElseIf Module1.count = 2 Then
            DataGridView1.Columns(0).Visible = False
            DataGridView1.Columns(1).Visible = False
            DataGridView1.Columns(2).HeaderText = "KFL Name"
            DataGridView1.Columns(2).SortMode = DataGridViewColumnSortMode.Automatic
        ElseIf Module1.count = 3 Then
            DataGridView1.Columns(0).HeaderText = "Measures in ML"
            DataGridView1.Columns(1).HeaderText = "Packing"
            DataGridView1.Columns(0).SortMode = DataGridViewColumnSortMode.Programmatic
            DataGridView1.Columns(1).SortMode = DataGridViewColumnSortMode.Programmatic
        ElseIf Module1.count = 4 Then
            DataGridView1.Columns(0).Visible = False
            DataGridView1.Columns(1).Visible = False
            DataGridView1.Columns(2).HeaderText = "Category Name"
            DataGridView1.Columns(2).SortMode = DataGridViewColumnSortMode.Programmatic
        ElseIf Module1.count = 5 Then
            DataGridView1.Columns(0).Visible = False
            DataGridView1.Columns(1).Visible = False
            DataGridView1.Columns(10).Visible = False
            DataGridView1.Columns(2).HeaderText = "Item Name"
            DataGridView1.Columns(3).HeaderText = "Barcode"
            DataGridView1.Columns(4).HeaderText = "Brand Name"
            DataGridView1.Columns(5).HeaderText = "KFL Name"
            DataGridView1.Columns(6).HeaderText = "ML"
            DataGridView1.Columns(7).HeaderText = "Packing"
            DataGridView1.Columns(8).HeaderText = "Sales Rate"
            DataGridView1.Columns(9).HeaderText = "Strength"
            DataGridView1.Columns(2).SortMode = DataGridViewColumnSortMode.Programmatic
            DataGridView1.Columns(3).SortMode = DataGridViewColumnSortMode.Programmatic
            DataGridView1.Columns(4).SortMode = DataGridViewColumnSortMode.Programmatic
            DataGridView1.Columns(5).SortMode = DataGridViewColumnSortMode.Programmatic
            DataGridView1.Columns(6).SortMode = DataGridViewColumnSortMode.Programmatic
            DataGridView1.Columns(7).SortMode = DataGridViewColumnSortMode.Programmatic
            DataGridView1.Columns(8).SortMode = DataGridViewColumnSortMode.Programmatic
        ElseIf Module1.count = 6 Then
            DataGridView1.Columns(0).HeaderText = "Strength"
            DataGridView1.Columns(0).SortMode = DataGridViewColumnSortMode.Programmatic
        ElseIf Module1.count = 7 Then
            DataGridView1.Columns(0).Visible = False
            DataGridView1.Columns(2).Visible = False
            DataGridView1.Columns(1).HeaderText = "Rate Name"
            DataGridView1.Columns(1).SortMode = DataGridViewColumnSortMode.Programmatic
        ElseIf Module1.count = 8 Then
            DataGridView1.Columns(0).Visible = False
            DataGridView1.Columns(2).Visible = False
            DataGridView1.Columns(3).Visible = False
            DataGridView1.Columns(4).Visible = False
            DataGridView1.Columns(5).Visible = False
            DataGridView1.Columns(6).Visible = False
            DataGridView1.Columns(7).Visible = False
            DataGridView1.Columns(8).Visible = False
            DataGridView1.Columns(9).Visible = False
            DataGridView1.Columns(10).Visible = False
            DataGridView1.Columns(11).Visible = False
            DataGridView1.Columns(12).Visible = False
            DataGridView1.Columns(13).Visible = False
            DataGridView1.Columns(14).Visible = False
            DataGridView1.Columns(15).Visible = False
            DataGridView1.Columns(16).Visible = False
            DataGridView1.Columns(1).HeaderText = "Company Name"
            DataGridView1.Columns(1).SortMode = DataGridViewColumnSortMode.Programmatic
        ElseIf Module1.count = 10 Then
            DataGridView1.Columns(0).Visible = False
            DataGridView1.Columns(1).Visible = False
            DataGridView1.Columns(2).HeaderText = "Shop Name"
            DataGridView1.Columns(3).HeaderText = "Address"
            DataGridView1.Columns(2).SortMode = DataGridViewColumnSortMode.Programmatic
            DataGridView1.Columns(3).SortMode = DataGridViewColumnSortMode.Programmatic
        ElseIf Module1.count = 11 Then
            DataGridView1.Columns(0).Visible = False
            DataGridView1.Columns(1).HeaderText = "Account Group Name"
            DataGridView1.Columns(2).HeaderText = "Account Type"
            DataGridView1.Columns(1).SortMode = DataGridViewColumnSortMode.Programmatic
            DataGridView1.Columns(2).SortMode = DataGridViewColumnSortMode.Programmatic
        ElseIf Module1.count = 12 Then
            DataGridView1.Columns(0).Visible = False
            DataGridView1.Columns(1).HeaderText = "Account Name"
            DataGridView1.Columns(1).SortMode = DataGridViewColumnSortMode.Programmatic
        ElseIf Module1.count = 13 Then
            DataGridView1.Columns(0).Visible = False
            For i = 6 To DataGridView1.Columns.Count - 1
                DataGridView1.Columns(i).Visible = False
            Next
            DataGridView1.Columns(1).HeaderText = "Ledger Name"
            DataGridView1.Columns(2).HeaderText = "A/C Sub-Group Name"
            DataGridView1.Columns(3).HeaderText = "Debit"
            DataGridView1.Columns(4).HeaderText = "Credit"
            DataGridView1.Columns(5).HeaderText = "Contact Persion"
            DataGridView1.Columns(1).SortMode = DataGridViewColumnSortMode.Programmatic
            DataGridView1.Columns(2).SortMode = DataGridViewColumnSortMode.Programmatic
            DataGridView1.Columns(3).SortMode = DataGridViewColumnSortMode.Programmatic
            DataGridView1.Columns(4).SortMode = DataGridViewColumnSortMode.Programmatic
            DataGridView1.Columns(5).SortMode = DataGridViewColumnSortMode.Programmatic
        ElseIf Module1.count = 14 Then
            DataGridView1.Columns(0).Visible = False
            DataGridView1.Columns(1).Visible = False
            DataGridView1.Columns(2).HeaderText = "Shop Name"
            DataGridView1.Columns(3).HeaderText = "Address"
            DataGridView1.Columns(2).SortMode = DataGridViewColumnSortMode.Programmatic
            DataGridView1.Columns(3).SortMode = DataGridViewColumnSortMode.Programmatic
            '---visibility on off for counter sales--->
        ElseIf Module1.count = 15 Then
            DataGridView1.Columns(10).Visible = False
            DataGridView1.Columns(11).Visible = False
            DataGridView1.Columns(12).Visible = False
            DataGridView1.Columns(13).Visible = False
            DataGridView1.Columns(0).HeaderText = "Date"
            DataGridView1.Columns(1).HeaderText = "Sale Account Name"
            DataGridView1.Columns(2).HeaderText = "Trn No"
            DataGridView1.Columns(3).HeaderText = "Amount"
            DataGridView1.Columns(4).HeaderText = "Discount Amount"
            DataGridView1.Columns(5).HeaderText = "Surcharge Amount"
            DataGridView1.Columns(6).HeaderText = "Total Amount"
            DataGridView1.Columns(7).HeaderText = "Sale Type"
            DataGridView1.Columns(8).HeaderText = "Rate Name"
            DataGridView1.Columns(9).HeaderText = "Store"
            DataGridView1.Columns(0).SortMode = DataGridViewColumnSortMode.Programmatic
            DataGridView1.Columns(1).SortMode = DataGridViewColumnSortMode.Programmatic
            DataGridView1.Columns(2).SortMode = DataGridViewColumnSortMode.Programmatic
            DataGridView1.Columns(3).SortMode = DataGridViewColumnSortMode.Programmatic
            DataGridView1.Columns(4).SortMode = DataGridViewColumnSortMode.Programmatic
            DataGridView1.Columns(5).SortMode = DataGridViewColumnSortMode.Programmatic
            DataGridView1.Columns(6).SortMode = DataGridViewColumnSortMode.Programmatic
            DataGridView1.Columns(7).SortMode = DataGridViewColumnSortMode.Programmatic
            DataGridView1.Columns(8).SortMode = DataGridViewColumnSortMode.Programmatic
            DataGridView1.Columns(9).SortMode = DataGridViewColumnSortMode.Programmatic
            '--assigning the grid property for the breakage--->
        ElseIf Module1.count = 16 Then
            DataGridView1.Columns(7).Visible = False
            DataGridView1.Columns(8).Visible = False
            DataGridView1.Columns(9).Visible = False
            DataGridView1.Columns(10).Visible = False
            DataGridView1.Columns(11).Visible = False
            DataGridView1.Columns(0).HeaderText = "Date"
            DataGridView1.Columns(1).HeaderText = "Trn No"
            DataGridView1.Columns(2).HeaderText = "Billno"
            DataGridView1.Columns(3).HeaderText = "Tp_Pass_No"
            DataGridView1.Columns(4).HeaderText = "Shopname"
            DataGridView1.Columns(5).HeaderText = "Party Name"
            DataGridView1.Columns(6).HeaderText = "Narration"
            DataGridView1.Columns(0).SortMode = DataGridViewColumnSortMode.Programmatic
            DataGridView1.Columns(1).SortMode = DataGridViewColumnSortMode.Programmatic
            DataGridView1.Columns(2).SortMode = DataGridViewColumnSortMode.Programmatic
            DataGridView1.Columns(3).SortMode = DataGridViewColumnSortMode.Programmatic
            DataGridView1.Columns(4).SortMode = DataGridViewColumnSortMode.Programmatic
            DataGridView1.Columns(5).SortMode = DataGridViewColumnSortMode.Programmatic
            DataGridView1.Columns(6).SortMode = DataGridViewColumnSortMode.Programmatic
        ElseIf Module1.count = 21 Then
            DataGridView1.Columns(0).Visible = False
            DataGridView1.Columns(2).Visible = False
            DataGridView1.Columns(3).Visible = False
            DataGridView1.Columns(1).HeaderText = "User Name"
            DataGridView1.Columns(1).SortMode = DataGridViewColumnSortMode.Programmatic
        ElseIf Module1.count = 22 Then
            DataGridView1.Columns(0).Visible = False
            DataGridView1.Columns(2).Visible = False
            DataGridView1.Columns(5).Visible = False
            DataGridView1.Columns(1).HeaderText = "User Name"
            DataGridView1.Columns(3).HeaderText = "Company Name"
            DataGridView1.Columns(4).HeaderText = "Access Level"
            DataGridView1.Columns(1).SortMode = DataGridViewColumnSortMode.Programmatic
            DataGridView1.Columns(3).SortMode = DataGridViewColumnSortMode.Programmatic
            DataGridView1.Columns(4).SortMode = DataGridViewColumnSortMode.Programmatic
        ElseIf Module1.count = 23 Then
            DataGridView1.Columns(0).Visible = False
            DataGridView1.Columns(1).HeaderText = "Supplier Name"
            DataGridView1.Columns(2).HeaderText = "Trans. Date"
            DataGridView1.Columns(3).HeaderText = "Bill No."
            DataGridView1.Columns(4).HeaderText = "Prchase/Return"
            DataGridView1.Columns(5).HeaderText = "Net Amount"
            DataGridView1.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
            DataGridView1.Columns(1).SortMode = DataGridViewColumnSortMode.Programmatic
            DataGridView1.Columns(2).SortMode = DataGridViewColumnSortMode.Programmatic
            DataGridView1.Columns(3).SortMode = DataGridViewColumnSortMode.Programmatic
            DataGridView1.Columns(4).SortMode = DataGridViewColumnSortMode.Programmatic
            DataGridView1.Columns(5).SortMode = DataGridViewColumnSortMode.Programmatic
        ElseIf Module1.count = 24 Then
            DataGridView1.Columns(0).Visible = False
            DataGridView1.Columns(2).Visible = False
            DataGridView1.Columns(1).HeaderText = "Company Name"
            DataGridView1.Columns(3).HeaderText = "Scheme Name"
            DataGridView1.Columns(1).SortMode = DataGridViewColumnSortMode.Programmatic
            DataGridView1.Columns(3).SortMode = DataGridViewColumnSortMode.Programmatic
            '---hidding the columns for stock transfer----->
        ElseIf Module1.count = 25 Then
            DataGridView1.Columns(0).Visible = False
            DataGridView1.Columns(1).Visible = False
            DataGridView1.Columns(6).Visible = False
            DataGridView1.Columns(7).Visible = False
            DataGridView1.Columns(8).Visible = False
            DataGridView1.Columns(2).HeaderText = "Transaction no."
            DataGridView1.Columns(3).HeaderText = "Date"
            DataGridView1.Columns(4).HeaderText = "Shopname From"
            DataGridView1.Columns(5).HeaderText = "Shopname To"
            DataGridView1.Columns(2).SortMode = DataGridViewColumnSortMode.Programmatic
            DataGridView1.Columns(3).SortMode = DataGridViewColumnSortMode.Programmatic
            DataGridView1.Columns(4).SortMode = DataGridViewColumnSortMode.Programmatic
            DataGridView1.Columns(5).SortMode = DataGridViewColumnSortMode.Programmatic
            '--assigning the column names for the receipt and the payment vouchers---->
        ElseIf Module1.count = 26 Or Module1.count = 27 Then
            DataGridView1.Columns(10).Visible = False
            DataGridView1.Columns(11).Visible = False
            DataGridView1.Columns(12).Visible = False
            DataGridView1.Columns(13).Visible = False
            DataGridView1.Columns(0).HeaderText = "Vch Date"
            DataGridView1.Columns(1).HeaderText = "Vch no"
            DataGridView1.Columns(2).HeaderText = "Ledger"
            DataGridView1.Columns(3).HeaderText = "Account Book"
            DataGridView1.Columns(4).HeaderText = "Cheque no"
            DataGridView1.Columns(5).HeaderText = "Due"
            DataGridView1.Columns(6).HeaderText = "Amount Paid"
            DataGridView1.Columns(7).HeaderText = "Discount"
            DataGridView1.Columns(8).HeaderText = "Net Due"
            DataGridView1.Columns(9).HeaderText = "Narration"
            DataGridView1.Columns(0).SortMode = DataGridViewColumnSortMode.Programmatic
            DataGridView1.Columns(1).SortMode = DataGridViewColumnSortMode.Programmatic
            DataGridView1.Columns(2).SortMode = DataGridViewColumnSortMode.Programmatic
            DataGridView1.Columns(3).SortMode = DataGridViewColumnSortMode.Programmatic
            DataGridView1.Columns(4).SortMode = DataGridViewColumnSortMode.Programmatic
            DataGridView1.Columns(5).SortMode = DataGridViewColumnSortMode.Programmatic
            DataGridView1.Columns(6).SortMode = DataGridViewColumnSortMode.Programmatic
            DataGridView1.Columns(7).SortMode = DataGridViewColumnSortMode.Programmatic
            DataGridView1.Columns(8).SortMode = DataGridViewColumnSortMode.Programmatic
            DataGridView1.Columns(9).SortMode = DataGridViewColumnSortMode.Programmatic
        End If
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub


    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        frm_ContainerForm.ToolStripLabel1.Text = ""
        Module1.frm.Close()
        Me.Close()
    End Sub

    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        frm_ContainerForm.ToolStripLabel1.Text = ""
        frm_ContainerForm.ToolStripStatusLabel6.Text = ""
        Module1.frm.Close()
    End Sub

    Private Sub ToolStripSplitButton1_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripSplitButton1.ButtonClick
        frm_print()
    End Sub

    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click
        frm_print()
    End Sub
    Private Sub frm_print()
        If dsload.Tables(0).Rows.Count > 0 Then
            'If SetupThePrinting() Then
            PrintDocument1.DocumentName = "Document on: " + frm_ContainerForm.ToolStripLabel1.Text.ToString
            obj.printer_setup(PrintDocument1)
            MyDataGridViewPrinter = New DataGridViewPrinter(DataGridView1, PrintDocument1, False, True, PrintDocument1.DocumentName, New Font("Tahoma", 18, FontStyle.Bold, GraphicsUnit.Point), New Font("Tahoma", 9, FontStyle.Bold, GraphicsUnit.Point), New Font("Tahoma", 11, FontStyle.Bold, GraphicsUnit.Point), Color.Black, True)
            PrintDocument1.Print()
            'End If
        Else
            MsgBox("There is no record for printing", MsgBoxStyle.Critical, "No Record")
        End If
        grid_color()
    End Sub
    Private Sub ToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem2.Click
        If dsload.Tables(0).Rows.Count > 0 Then
            'If SetupThePrinting() Then
            PrintDocument1.DocumentName = "Document on: " + frm_ContainerForm.ToolStripLabel1.Text.ToString
            obj.printer_setup(PrintDocument1)
            Dim MyPrintPreviewDialog As New PrintPreviewDialog()
            MyDataGridViewPrinter = New DataGridViewPrinter(DataGridView1, PrintDocument1, False, True, PrintDocument1.DocumentName, New Font("Tahoma", 18, FontStyle.Bold, GraphicsUnit.Point), New Font("Tahoma", 9, FontStyle.Bold, GraphicsUnit.Point), New Font("Tahoma", 11, FontStyle.Bold, GraphicsUnit.Point), Color.Black, True)
            MyPrintPreviewDialog.Left = 150
            MyPrintPreviewDialog.Top = 78
            MyPrintPreviewDialog.Width = 875
            MyPrintPreviewDialog.Height = 638
            MyPrintPreviewDialog.StartPosition = System.Windows.Forms.FormStartPosition.Manual
            MyPrintPreviewDialog.ShowIcon = False
            MyPrintPreviewDialog.Document = PrintDocument1
            MyPrintPreviewDialog.ShowDialog()
            'End If
        Else
            MsgBox("There is no record for printing", MsgBoxStyle.Critical, "No Record")
        End If
        grid_color()
    End Sub

    Private Function SetupThePrinting() As Boolean
        'Dim MyPrintDialog As New PrintDialog()
        'MyPrintDialog.AllowCurrentPage = False
        'MyPrintDialog.AllowPrintToFile = False
        'MyPrintDialog.AllowSelection = False
        'MyPrintDialog.AllowSomePages = False
        'MyPrintDialog.PrintToFile = False
        'MyPrintDialog.ShowHelp = False
        'MyPrintDialog.ShowNetwork = False

        ''If MyPrintDialog.ShowDialog() <> DialogResult.OK Then
        ''    Return False
        ''End If
        'PrintDocument1.DocumentName = "Document on: " + ContainerForm.ToolStripLabel1.Text.ToString
        'PrintDocument1.PrinterSettings = MyPrintDialog.PrinterSettings
        'PrintDocument1.DefaultPageSettings = MyPrintDialog.PrinterSettings.DefaultPageSettings
        'PrintDocument1.DefaultPageSettings.Margins = New Margins(80, 50, 50, 50)

        'If MessageBox.Show("Do you want the report to be centered on the page", "InvoiceManager - Center on Page", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
        '    MyDataGridViewPrinter = New DataGridViewPrinter(DataGridView1, PrintDocument1, True, True, ContainerForm.ToolStripLabel1.Text, New Font("Tahoma", 18, FontStyle.Bold, GraphicsUnit.Point), Color.Black, True)
        'Else

        'MyDataGridViewPrinter = New DataGridViewPrinter(DataGridView1, PrintDocument1, False, True, PrintDocument1.DocumentName, New Font("Tahoma", 18, FontStyle.Bold, GraphicsUnit.Point), New Font("Tahoma", 9, FontStyle.Bold, GraphicsUnit.Point), New Font("Tahoma", 11, FontStyle.Bold, GraphicsUnit.Point), Color.Black, True)
        'End If
        'Return True
    End Function

    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim more As Boolean = MyDataGridViewPrinter.DrawDataGridView(e.Graphics)
        If more = True Then
            e.HasMorePages = True
        End If
    End Sub

    Private Sub ToolStripMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem3.Click
        If dsload.Tables(0).Rows.Count > 0 Then
            Try
                SaveFileDialog1.Filter = "Excel Worksheets|*.xls;*.xlsx"
                SaveFileDialog1.FileName = frm_ContainerForm.ToolStripLabel1.Text + ".xls"
                'SaveFileDialog1.ShowDialog()

                If SaveFileDialog1.ShowDialog <> DialogResult.OK Then
                    Exit Sub
                End If
                Dim filepath As String = SaveFileDialog1.FileName
                Dim xlApp As xcell.Application
                Dim xlWorkBook As xcell.Workbook
                Dim xlWorkSheet As xcell.Worksheet
                Dim misValue As Object = System.Reflection.Missing.Value

                Dim i As Int16, j As Int16

                xlApp = New xcell.ApplicationClass
                xlWorkBook = xlApp.Workbooks.Add(misValue)
                xlWorkSheet = xlWorkBook.Sheets("sheet1")


                xlWorkSheet.Cells(1, 1) = "Company: " + Module1.companyname
                xlWorkSheet.Cells(2, 1) = "Document on: " + frm_ContainerForm.ToolStripLabel1.Text
                xlWorkSheet.Cells(3, 1) = "Date: " + Format(Date.Today, "dd/MM/yyyy").ToString

                Dim h1 As Integer = 0
                For j = 0 To DataGridView1.ColumnCount - 1
                    If DataGridView1.Columns(j).Visible = True Then
                        h1 = h1 + 1
                        xlWorkSheet.Cells(5, h1) = DataGridView1.Columns(j).HeaderText.ToString
                    End If
                Next
                For i = 0 To DataGridView1.RowCount - 2
                    Dim h2 As Integer = 0
                    For j = 0 To DataGridView1.ColumnCount - 1
                        If DataGridView1.Columns(j).Visible = True Then
                            h2 = h2 + 1
                            xlWorkSheet.Cells(i + 6, h2) = DataGridView1(j, i).Value.ToString()
                        End If
                    Next
                Next

                xlWorkBook.SaveAs(filepath, xcell.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, _
                 xcell.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue)
                xlWorkBook.Close(True, misValue, misValue)
                xlApp.Quit()

                releaseObject(xlWorkSheet)
                releaseObject(xlWorkBook)
                releaseObject(xlApp)

            Catch ex As Exception
            End Try
        Else
            MsgBox("There is no record for printing", MsgBoxStyle.Critical, "No Record")
        End If
    End Sub
    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
            MessageBox.Show("Exception Occured while releasing object " + ex.ToString())
        Finally
            GC.Collect()
        End Try
    End Sub

    Private Sub grid_color()
        DataGridView1.BackgroundColor = Color.Ivory
        DataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.SandyBrown
        DataGridView1.DefaultCellStyle.BackColor = Color.NavajoWhite
        DataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Ivory
    End Sub


    Private Sub userset()
        If Module1.uadmin = True Then
            Button1.Enabled = True
            Button2.Enabled = True
            Button3.Enabled = True
            Exit Sub
        End If
        Dim dsset As DataSet
        s = "select * from userrights join id on id.usercode=userrights.usercode where id.usercode='" & Module1.usercode & "' and companycode='" & Module1.companycode & "'"
        dsset = ob.populate(s)
        If dsset.Tables(0).Rows.Count > 0 Then
            If Module1.count = 1 Then
                If dsset.Tables(0).Rows(3).Item(5).ToString = "True" Then
                    Button1.Enabled = True
                End If
                If dsset.Tables(0).Rows(3).Item(6).ToString = "True" Then
                    Button2.Enabled = True
                End If
                If dsset.Tables(0).Rows(3).Item(7).ToString = "True" Then
                    Button3.Enabled = True
                End If
            End If
            If Module1.count = 2 Then
                If dsset.Tables(0).Rows(5).Item(5).ToString = "True" Then
                    Button1.Enabled = True
                End If
                If dsset.Tables(0).Rows(5).Item(6).ToString = "True" Then
                    Button2.Enabled = True
                End If
                If dsset.Tables(0).Rows(5).Item(7).ToString = "True" Then
                    Button3.Enabled = True
                End If
            End If
            If Module1.count = 3 Then
                If dsset.Tables(0).Rows(6).Item(5).ToString = "True" Then
                    Button1.Enabled = True
                End If
                If dsset.Tables(0).Rows(6).Item(6).ToString = "True" Then
                    Button2.Enabled = True
                End If
                If dsset.Tables(0).Rows(6).Item(7).ToString = "True" Then
                    Button3.Enabled = True
                End If
            End If
            If Module1.count = 4 Then
                If dsset.Tables(0).Rows(4).Item(5).ToString = "True" Then
                    Button1.Enabled = True
                End If
                If dsset.Tables(0).Rows(4).Item(6).ToString = "True" Then
                    Button2.Enabled = True
                End If
                If dsset.Tables(0).Rows(4).Item(7).ToString = "True" Then
                    Button3.Enabled = True
                End If
            End If
            If Module1.count = 5 Then
                If dsset.Tables(0).Rows(8).Item(5).ToString = "True" Then
                    Button1.Enabled = True
                End If
                If dsset.Tables(0).Rows(8).Item(6).ToString = "True" Then
                    Button2.Enabled = True
                End If
                If dsset.Tables(0).Rows(8).Item(7).ToString = "True" Then
                    Button3.Enabled = True
                End If
            End If
            If Module1.count = 6 Then
                If dsset.Tables(0).Rows(7).Item(5).ToString = "True" Then
                    Button1.Enabled = True
                End If
                If dsset.Tables(0).Rows(7).Item(6).ToString = "True" Then
                    Button2.Enabled = True
                End If
                If dsset.Tables(0).Rows(7).Item(7).ToString = "True" Then
                    Button3.Enabled = True
                End If
            End If
            If Module1.count = 7 Then
                If dsset.Tables(0).Rows(9).Item(5).ToString = "True" Then
                    Button1.Enabled = True
                End If
                If dsset.Tables(0).Rows(9).Item(6).ToString = "True" Then
                    Button2.Enabled = True
                End If
                If dsset.Tables(0).Rows(9).Item(7).ToString = "True" Then
                    Button3.Enabled = True
                End If
            End If
            If Module1.count = 10 Then
                If dsset.Tables(0).Rows(11).Item(5).ToString = "True" Then
                    Button1.Enabled = True
                End If
                If dsset.Tables(0).Rows(11).Item(6).ToString = "True" Then
                    Button2.Enabled = True
                End If
                If dsset.Tables(0).Rows(11).Item(7).ToString = "True" Then
                    Button3.Enabled = True
                End If
            End If
            If Module1.count = 11 Then
                If dsset.Tables(0).Rows(0).Item(5).ToString = "True" Then
                    Button1.Enabled = True
                End If
                If dsset.Tables(0).Rows(0).Item(6).ToString = "True" Then
                    Button2.Enabled = True
                End If
                If dsset.Tables(0).Rows(0).Item(7).ToString = "True" Then
                    Button3.Enabled = True
                End If
            End If
            If Module1.count = 12 Then
                If dsset.Tables(0).Rows(1).Item(5).ToString = "True" Then
                    Button1.Enabled = True
                End If
                If dsset.Tables(0).Rows(1).Item(6).ToString = "True" Then
                    Button2.Enabled = True
                End If
                If dsset.Tables(0).Rows(31).Item(7).ToString = "True" Then
                    Button3.Enabled = True
                End If
            End If
            If Module1.count = 13 Then
                If dsset.Tables(0).Rows(2).Item(5).ToString = "True" Then
                    Button1.Enabled = True
                End If
                If dsset.Tables(0).Rows(2).Item(6).ToString = "True" Then
                    Button2.Enabled = True
                End If
                If dsset.Tables(0).Rows(2).Item(7).ToString = "True" Then
                    Button3.Enabled = True
                End If
            End If
            If Module1.count = 14 Then
                If dsset.Tables(0).Rows(16).Item(5).ToString = "True" Then
                    Button1.Enabled = True
                End If
                If dsset.Tables(0).Rows(16).Item(6).ToString = "True" Then
                    Button2.Enabled = True
                End If
                If dsset.Tables(0).Rows(16).Item(7).ToString = "True" Then
                    Button3.Enabled = True
                End If
            End If
            If Module1.count = 15 Then
                If dsset.Tables(0).Rows(15).Item(5).ToString = "True" Then
                    Button1.Enabled = True
                End If
                If dsset.Tables(0).Rows(15).Item(6).ToString = "True" Then
                    Button2.Enabled = True
                End If
                If dsset.Tables(0).Rows(15).Item(7).ToString = "True" Then
                    Button3.Enabled = True
                End If
            End If
        End If
    End Sub



End Class



