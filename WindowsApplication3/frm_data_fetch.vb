Imports System.Data.SqlClient
Imports System.Configuration

Public Class frm_data_fetch
    Dim con As SqlConnection
    Dim flopen As Boolean

    Private Sub data_fetch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label1.Text = "Click on start to import Company Masters from old Database."

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Label1.Text = ""
        Label1.Text = "Importing...."
        import()
        frm_ContainerForm.logout()
        frm_login.Show()
        Me.Close()
    End Sub
    Private Sub import()
        '---- opening the connection --->
        Module1.opencon()
        '---inserting into the companymst and the yearmst --->
        Dim cmd As New SqlCommand("insert into companymst (companycode,companyname) select companycode,companyname from barmgmt.dbo.companymst", Module1.con)
        Dim cmd2 As New SqlCommand("insert into yearmst(companycode,yearcode,yearrange,stdate,enddate) select companycode,yearcode,yearrange,stdate,endate from barmgmt.dbo.yearmst", Module1.con)
        cmd.ExecuteNonQuery()
        cmd2.ExecuteNonQuery()
        '---fetching the acounts master --------------------->
        Dim cmd9 As New SqlCommand("insert into acountname(accode,acname) select acgroupcode,acgroupname from barmgmt.dbo.AcGroupMst", Module1.con)
        Dim cmd10 As New SqlCommand("insert into ledger(name,accode,ledcode,companycode)select accountname,acgroupcode,accountcode,companycode from barmgmt.dbo.AccountMst", Module1.con)
        cmd9.ExecuteNonQuery()
        cmd10.ExecuteNonQuery()
        '-----Fetching the inventorymaster ------->
        Dim cmd4 As New SqlCommand("insert into groupmst (companycode,groupcode,groupname) select companycode,matgroupcode,matgroupname from barmgmt.dbo.MatGroupMst", Module1.con)
        Dim cmd5 As New SqlCommand("insert into categorymst (companycode,categorycode,categoryname) select companycode,categorycode,categoryname from barmgmt.dbo.matcategoryMst", Module1.con)
        Dim cmd6 As New SqlCommand("insert into kflmst (companycode,kflcode,kflname) select companycode,kflcode,kflname from barmgmt.dbo.kflmst", Module1.con)
        Dim cmd7 As New SqlCommand("insert into itemgroupml(ml,packing) select distinct measureml,packing from barmgmt.dbo.itemmst", Module1.con)
        Dim cmd8 As New SqlCommand("insert into strength(strengthname) select distinct strength from barmgmt.dbo.itemmst", Module1.con)
        Dim cmd11 As New SqlCommand("insert into itemmst(groupcode,itemcode,itemname,categorycode,kflcode,ml,packing,strengthname,companycode,barcode,salesrate) select itemgroup,itemcode,itemname,itemcategory,kindforeign,measureml,packing,strength,companycode,shortcode,salerate from barmgmt.dbo.itemmst where itemcategory in(select categorycode from barmgmt.dbo.matcategoryMst)", Module1.con)
        Dim cmd15 As New SqlCommand("insert into storage(companycode,shopcode,shopname,address) select companycode,loccode,location,locaddress from barmgmt.dbo.locationmst group by companycode,loccode,location,locaddress", Module1.con)
        Dim stock As New SqlCommand("insert into openingstockmst(shopcode,companycode,itemcode,loose,box,qnty,yearcode,trndate) select loccode,barmgmt.dbo.itmopening.companycode,itemcode,loose,box,stock,barmgmt.dbo.itmopening.yearcode,stdate from barmgmt.dbo.itmopening join barmgmt.dbo.yearmst on barmgmt.dbo.yearmst.companycode=barmgmt.dbo.itmopening.companycode and barmgmt.dbo.yearmst.yearcode=barmgmt.dbo.itmopening.yearcode where loccode in(select loccode from barmgmt.dbo.locationmst)", Module1.con)
        cmd4.ExecuteNonQuery()
        cmd5.ExecuteNonQuery()
        cmd6.ExecuteNonQuery()
        cmd7.ExecuteNonQuery()
        cmd8.ExecuteNonQuery()
        cmd11.ExecuteNonQuery()
        cmd15.ExecuteNonQuery()
        stock.ExecuteNonQuery()
        '-----fetching into the companyparameter ---------------->
        Dim cmd3 As New SqlCommand("insert into companyparamtr (companycode,csalebillno,saleaccount_head,bankacc,breakacc,saleretrnacc,purchaseacchd,cashhd,discntacc,defshpgdn,ratecode,expenceacc,printbill,autorefcurrsal,actposmode) select companycode,cntsalebillno,cntrsalehead,crcardpartycode,breakageac,retsaleac,purchasehead,cashsaleac,discaccode,shoploc,defrate,expac,0,0,0 from barmgmt.dbo.parammst", Module1.con)
        cmd3.ExecuteNonQuery()
        '---fetching data to the opening stock and the rate mst -->
        Dim cmd12 As New SqlCommand("select loccode,barmgmt.dbo.itmopening.companycode,itemcode,loose,box,stock,barmgmt.dbo.yearmst.yearcode,stdate from barmgmt.dbo.itmopening join barmgmt.dbo.yearmst on barmgmt.dbo.yearmst.yearcode=barmgmt.dbo.itmopening.yearcode and barmgmt.dbo.yearmst.companycode=barmgmt.dbo.itmopening.companycode ", Module1.con)
        Dim cmd13 As New SqlCommand("insert into itemrateinfo(ratecode,ratename,companycode) select ratecode,ratename,companycode from barmgmt.dbo.itmratemst group by ratecode,ratename,companycode", Module1.con)
        Dim cmd14 As New SqlCommand("insert into itemratemst(companycode,ratecode,itemcode,salesrate) select companycode,ratecode,itemcode,itemrate from barmgmt.dbo.itmratemst where itemcode in(select itemcode from itemmst)", Module1.con)
        cmd12.ExecuteNonQuery()
        cmd13.ExecuteNonQuery()
        cmd14.ExecuteNonQuery()
        '----fetching the sale--------------------->
        'Dim sale As New SqlCommand("begin tran t1 insert into salesbillmain(companycode,trnno,trndate,ledgercode,amount,salestype,ratecode,storecode,yearcode,discamount,netamount) select companycode,trnno,trndate,clientcode,totamount,saletype,ratelist,storecode,yearcode,tottaxamt,totnetamt from barmgmt.dbo.salesbillmain begin tran t2 update salesbillmain set salestype='CASH' where salestype='0' begin tran t3 update salesbillmain set salestype='CREDIT CARD' where salestype='1' begin tran t4 update salesbillmain set salestype='CREDIT ACCOUNT' where salestype='2' begin tran t5 update salesbillmain set salestype='RETURN' where salestype='3' begin tran t6 insert into salesbilldetail(companycode,itemcode,loose,tot_box,qnty,rate,itemamount,trnno,yearcode,trndate) select barmgmt.dbo.salesbillmain.companycode,itemcode,itmloose,itmbox,itemqty,itemrate,itemamount,trnno,yearcode,trndate from barmgmt.dbo.salesbilldetail join barmgmt.dbo.salesbillmain on barmgmt.dbo.salesbilldetail.companycode=barmgmt.dbo.salesbillmain.companycode and barmgmt.dbo.salesbilldetail.systemno=barmgmt.dbo.salesbillmain.systemno commit tran t1 commit tran t2 commit tran t3 commit tran t4  commit tran t5 commit tran t6")
        'sale.ExecuteNonQuery()
        '-----fetching the purchase --------------->
        Dim purchase As New SqlCommand("insert into purchasemain(companycode,yearcode,trnno,trndate,ptype,purchaseacccode,suppliercode,shopcode,docno,docdate,tppassno) select companyparamtr.companycode,yearcode,grnno,grndate,'PURCHASE',purchaseacchd,suppcode,toloccode,chno,chdate,tpassno from barmgmt.dbo.grnmain join companyparamtr on  companyparamtr.companycode=barmgmt.dbo.grnmain.companycode ", Module1.con)
        purchase.ExecuteNonQuery()
        Dim purchase1 As New SqlCommand("insert into purchasedetail(companycode,yearcode,itemcode,batchno,itembox,itemloose,itemquantity,trnno) select barmgmt.dbo.grnmain.companycode,yearcode,itemcode,batchno,box,loose,itemqty,grnno from barmgmt.dbo.grndetail join barmgmt.dbo.grnmain on barmgmt.dbo.grnmain.companycode=barmgmt.dbo.grndetail.companycode and barmgmt.dbo.grnmain.systemno=barmgmt.dbo.grndetail.systemno", Module1.con)
        purchase1.ExecuteNonQuery()
        '---fetching the taxschemes -------------->
        Dim tax As New SqlCommand("insert into taxdetail(companycode,schemecode,schemename,srno,taxcode,ledcode,taxamount) select barmgmt.dbo.nwtaxdetail.companycode,barmgmt.dbo.nwtaxdetail.schemecode,schemename,srno,taxcode,accode,taxrate from barmgmt.dbo.nwtaxdetail join barmgmt.dbo.nwtaxmain on barmgmt.dbo.nwtaxdetail.companycode=barmgmt.dbo.nwtaxmain.companycode and barmgmt.dbo.nwtaxdetail.schemecode=barmgmt.dbo.nwtaxmain.schemecode ", Module1.con)
        tax.ExecuteNonQuery()
        '---closing the connection --->
        Module1.closecon()
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        '---opening a connection to the server --->
        Module1.opencon()
        '---deleting from ledger and acountname--->
        Dim del1 As New SqlCommand("delete from ledger", Module1.con)
        Dim del2 As New SqlCommand("delete from acountname", Module1.con)
        '--deleting from salesbillmain and sales billdetail ---->
        Dim del3 As New SqlCommand("delete from salesbillmain", Module1.con)
        Dim del4 As New SqlCommand("delete from salesbilldetail", Module1.con)
        '---deleting form all master----->
        Dim del5 As New SqlCommand("delete from itemratemst", Module1.con)
        Dim del6 As New SqlCommand("delete from itemrateinfo", Module1.con)
        Dim del7 As New SqlCommand("delete from openingstockmst", Module1.con)
        Dim del8 As New SqlCommand("delete from itemmst", Module1.con)
        Dim del9 As New SqlCommand("delete from kflmst", Module1.con)
        Dim del10 As New SqlCommand("delete from categorymst", Module1.con)
        Dim del11 As New SqlCommand("delete from groupmst", Module1.con)
        Dim del12 As New SqlCommand("delete from itemgroupml", Module1.con)
        Dim del13 As New SqlCommand("delete from strength", Module1.con)
        '--deleting from patment and receipt ---->
        Dim del14 As New SqlCommand("delete from payment_detail", Module1.con)
        Dim del15 As New SqlCommand("delete from payment_main", Module1.con)
        Dim del16 As New SqlCommand("delete from receipt_detail", Module1.con)
        Dim del17 As New SqlCommand("delete from receipt_main", Module1.con)
        '---deleting from purchase ----->
        Dim del18 As New SqlCommand("delete from purchasedetail", Module1.con)
        Dim del19 As New SqlCommand("delete from purchasemain", Module1.con)
        Dim del20 As New SqlCommand("delete from purchasetaxdetail", Module1.con)
        Dim del21 As New SqlCommand("delete from taxdetail", Module1.con)
        Dim del22 As New SqlCommand("delete from stk_transfer_detail", Module1.con)
        Dim del23 As New SqlCommand("delete from stk_transfer_main", Module1.con)
        Dim del24 As New SqlCommand("delete from storage", Module1.con)
        Dim del25 As New SqlCommand("delete from userrights", Module1.con)
        Dim del26 As New SqlCommand("delete from breakagedetail", Module1.con)
        Dim del27 As New SqlCommand("delete from breakagemain", Module1.con)
        Dim del28 As New SqlCommand("delete from companyparamtr", Module1.con)
        Dim del29 As New SqlCommand("delete from companymst", Module1.con)
        Dim del30 As New SqlCommand("delete from yearmst", Module1.con)
        '---executing the commands---->
        del1.ExecuteNonQuery()
        del2.ExecuteNonQuery()
        del3.ExecuteNonQuery()
        del4.ExecuteNonQuery()
        del5.ExecuteNonQuery()
        del6.ExecuteNonQuery()
        del7.ExecuteNonQuery()
        del8.ExecuteNonQuery()
        del9.ExecuteNonQuery()
        del10.ExecuteNonQuery()
        del11.ExecuteNonQuery()
        del12.ExecuteNonQuery()
        del13.ExecuteNonQuery()
        del14.ExecuteNonQuery()
        del15.ExecuteNonQuery()
        del16.ExecuteNonQuery()
        del17.ExecuteNonQuery()
        del18.ExecuteNonQuery()
        del19.ExecuteNonQuery()
        del20.ExecuteNonQuery()
        del21.ExecuteNonQuery()
        del22.ExecuteNonQuery()
        del23.ExecuteNonQuery()
        del24.ExecuteNonQuery()
        del25.ExecuteNonQuery()
        del26.ExecuteNonQuery()
        del27.ExecuteNonQuery()
        del28.ExecuteNonQuery()
        del29.ExecuteNonQuery()
        del30.ExecuteNonQuery()
        '---closing the present connection ---->
        Module1.closecon()
        '---msg box-->
        MsgBox("deleting complete")
    End Sub
End Class
