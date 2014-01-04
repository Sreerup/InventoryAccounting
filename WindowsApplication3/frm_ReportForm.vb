Imports System.Data.SqlClient
Imports System.IO
Imports xcell = Microsoft.Office.Interop.Excel
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Text
Imports System.Windows.Forms
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared

Public Class frm_ReportForm
    Dim group_by As String
    Dim ds_cmb_load As DataSet
    Dim ds0 As DataSet
    Dim ds1 As DataSet
    Dim ds2 As DataSet
    Dim ds3 As DataSet
    Dim ds4 As DataSet

    Dim date_string As String
    Dim title_string As String
    Dim sql As String
    Dim sql1 As String
    Dim sql2 As String
    Dim sql3 As String
    Dim sql4 As String
    Dim sql5 As String
    Dim s As String
    Dim ob As New Class1
    Dim dtstr1 As Date
    Dim dtstr2 As Date
    Dim querystring As String
    Dim days As Integer


    '---declaring an instance of the report--->
    Dim Rpt As ReportDocument
    '--variable declaration for accounting report --->
    Dim dataset_for_combo As New DataSet
    Dim ledger_code As String
    '-----instance variable for selecting the shopcode ---------->
    Dim shop_code As String
    '---declaring an universal daaset----->
    Dim data_set As New DataSet


    Private Sub frm_load()
        set_control()
        If DateTimePicker1.Value > Date.Now Then
            MsgBox("Wrong Date, please select valid date/period.", MsgBoxStyle.Exclamation, "Wrong Date")
            Exit Sub
        End If
        sql = Nothing
        sql1 = Nothing
        sql2 = Nothing
        sql3 = Nothing
        sql4 = Nothing
        sql5 = Nothing
        ds0 = New DataSet
        ds1 = New DataSet
        ds2 = New DataSet
        ds3 = New DataSet
        ds4 = New DataSet
        If Module1.report_no = 1 Then
            '---------this query for category & brand wise stock----------------->
            sql = "select groupname as group_by_name,categoryname as group_by_name2,ml, sum(opening_stock)as opening,sum(bottles_purchased)as purchased,sum(bottles_sold)as sold,sum(stock)as closing from opening_closing_stock('" & Module1.comstdate & "','" & DateTimePicker1.Value.Date & "','" & DateTimePicker1.Value.Date & "') join itemmst on itemmst.itemcode=opening_closing_stock.itemcode and itemmst.companycode=opening_closing_stock.companycode join categorymst on categorymst.categorycode = itemmst.categorycode and categorymst.companycode=itemmst.companycode join groupmst on groupmst.groupcode = itemmst.groupcode and groupmst.companycode=itemmst.companycode where opening_closing_stock.companycode='" & Module1.companycode & "' and yearcode ='" & Module1.yearcode & "' " + querystring + " and opening_stock<>0 or opening_closing_stock.companycode='" & Module1.companycode & "' and yearcode ='" & Module1.yearcode & "' " + querystring + " and bottles_purchased<>0 or opening_closing_stock.companycode='" & Module1.companycode & "' and yearcode ='" & Module1.yearcode & "' " + querystring + " and  bottles_sold<>0 or opening_closing_stock.companycode='" & Module1.companycode & "' and yearcode ='" & Module1.yearcode & "' " + querystring + " and stock<>0 group by groupname,categoryname,opening_closing_stock.itemname,ml order by groupname,categoryname,opening_closing_stock.itemname"
            stock_load()
        ElseIf Module1.report_no = 2 Then
            '--------this query for category & brand wise stock------------------>
            sql = "select groupname as group_by_name,categoryname as group_by_name2,ml, sum(opening_stock)as opening,sum(bottles_purchased)as purchased,sum(bottles_sold)as sold,sum(stock)as closing from opening_closing_stock('" & Module1.comstdate & "','" & dtstr1 & "','" & dtstr2 & "') join itemmst on itemmst.itemcode=opening_closing_stock.itemcode and itemmst.companycode=opening_closing_stock.companycode join categorymst on categorymst.categorycode = itemmst.categorycode and categorymst.companycode=itemmst.companycode join groupmst on groupmst.groupcode = itemmst.groupcode and groupmst.companycode=itemmst.companycode where opening_closing_stock.companycode='" & Module1.companycode & "' and yearcode ='" & Module1.yearcode & "' " + querystring + " and opening_stock<>0 or opening_closing_stock.companycode='" & Module1.companycode & "' and yearcode ='" & Module1.yearcode & "' " + querystring + " and bottles_purchased<>0 or opening_closing_stock.companycode='" & Module1.companycode & "' and yearcode ='" & Module1.yearcode & "' " + querystring + " and  bottles_sold<>0 or opening_closing_stock.companycode='" & Module1.companycode & "' and yearcode ='" & Module1.yearcode & "' " + querystring + " and stock<>0 group by groupname,categoryname,opening_closing_stock.itemname,ml order by groupname,categoryname,opening_closing_stock.itemname"
            stock_load()
            '--------stock report brand sise and ml wise ------------------------>
        ElseIf Module1.report_no = 3 Then
            sql = "select groupname as group_by_name,categoryname as group_by_name2,ml, sum(opening_stock)as opening,sum(bottles_purchased)as purchased,sum(bottles_sold)as sold,sum(stock)as closing from opening_closing_stock('" & Module1.comstdate & "','" & DateTimePicker1.Value.Date & "','" & DateTimePicker2.Value.Date & "') join itemmst on itemmst.itemcode=opening_closing_stock.itemcode and itemmst.companycode=opening_closing_stock.companycode join categorymst on categorymst.categorycode = itemmst.categorycode and categorymst.companycode=itemmst.companycode join groupmst on groupmst.groupcode = itemmst.groupcode and groupmst.companycode=itemmst.companycode where opening_closing_stock.companycode='" & Module1.companycode & "' and yearcode ='" & Module1.yearcode & "' " + querystring + " and opening_stock<>0 or opening_closing_stock.companycode='" & Module1.companycode & "' and yearcode ='" & Module1.yearcode & "' " + querystring + " and bottles_purchased<>0 or opening_closing_stock.companycode='" & Module1.companycode & "' and yearcode ='" & Module1.yearcode & "' " + querystring + " and  bottles_sold<>0 or opening_closing_stock.companycode='" & Module1.companycode & "' and yearcode ='" & Module1.yearcode & "' " + querystring + " and stock<>0 group by groupname,categoryname,ml order by groupname,categoryname,ml"
            stock_load()
        ElseIf Module1.report_no = 4 Then
            sql = "select groupname as group_by_name,ml,sum(opening_stock)as opening,sum(bottles_purchased)as purchased,sum(bottles_sold)as sold,sum(stock)as closing from opening_closing_stock('" & Module1.comstdate & "','" & DateTimePicker1.Value.Date & "','" & DateTimePicker1.Value.Date & "') join itemmst on itemmst.itemcode=opening_closing_stock.itemcode and itemmst.companycode=opening_closing_stock.companycode join groupmst on groupmst.groupcode = itemmst.groupcode and groupmst.companycode=itemmst.companycode where opening_closing_stock.companycode='" & Module1.companycode & "' and yearcode ='" & Module1.yearcode & "' " + querystring + "  and opening_stock<>0 or bottles_purchased<>0 or bottles_sold<>0 or stock<>0 group by groupname,opening_closing_stock.itemname,ml order by groupname,opening_closing_stock.itemname"
            stock_load()
        ElseIf Module1.report_no = 5 Then
            sql = "select groupname as group_by_name,ml,sum(opening_stock)as opening,sum(bottles_purchased)as purchased,sum(bottles_sold)as sold,sum(stock)as closing from opening_closing_stock('" & Module1.comstdate & "','" & dtstr1 & "','" & dtstr2 & "') join itemmst on itemmst.itemcode=opening_closing_stock.itemcode and itemmst.companycode=opening_closing_stock.companycode join groupmst on groupmst.groupcode = itemmst.groupcode and groupmst.companycode=itemmst.companycode where opening_closing_stock.companycode='" & Module1.companycode & "' and yearcode ='" & Module1.yearcode & "' " + querystring + "  and opening_stock<>0 or bottles_purchased<>0 or bottles_sold<>0 or stock<>0 group by groupname,opening_closing_stock.itemname,ml order by groupname,opening_closing_stock.itemname"
            stock_load()
            '----stock report category wise when the user selects all as default shop----------------->
        ElseIf Module1.report_no = 6 And shop_code = Nothing Then
            sql = "select groupname as group_by_name,ml,sum(opening_stock)as opening,sum(bottles_purchased)as purchased,sum(bottles_sold)as sold,sum(stock)as closing from opening_closing_stock('" & Module1.comstdate & "','" & DateTimePicker1.Value.Date & "','" & DateTimePicker2.Value.Date & "') join itemmst on itemmst.itemcode=opening_closing_stock.itemcode and itemmst.companycode=opening_closing_stock.companycode join groupmst on groupmst.groupcode = itemmst.groupcode and groupmst.companycode=itemmst.companycode where opening_closing_stock.companycode='" & Module1.companycode & "' and yearcode ='" & Module1.yearcode & "' and opening_stock<>0 or opening_closing_stock.companycode='" & Module1.companycode & "' and yearcode ='" & Module1.yearcode & "' and bottles_purchased<>0 or opening_closing_stock.companycode='" & Module1.companycode & "' and yearcode ='" & Module1.yearcode & "' and bottles_sold<>0 or opening_closing_stock.companycode='" & Module1.companycode & "' and yearcode ='" & Module1.yearcode & "'  and stock<>0 group by itemmst.companycode,yearcode,groupmst.groupcode,groupname,ml order by groupname,ml"
            stock_load()
            '---syock report categorywise when the user selects a perticular default shop ---->
        ElseIf Module1.report_no = 6 And Not shop_code = Nothing Then
            sql = "select groupname as group_by_name,ml,sum(opening_stock)as opening,sum(bottles_purchased)as purchased,sum(bottles_sold)as sold,sum(stock)as closing from opening_closing_stock('" & Module1.comstdate & "','" & DateTimePicker1.Value.Date & "','" & DateTimePicker2.Value.Date & "') join itemmst on itemmst.itemcode=opening_closing_stock.itemcode and itemmst.companycode=opening_closing_stock.companycode join groupmst on groupmst.groupcode = itemmst.groupcode and groupmst.companycode=itemmst.companycode where opening_closing_stock.companycode='" & Module1.companycode & "' and yearcode ='" & Module1.yearcode & "' and storecode='" & shop_code & "' and opening_stock<>0 or opening_closing_stock.companycode='" & Module1.companycode & "' and yearcode ='" & Module1.yearcode & "' and storecode='" & shop_code & "' and bottles_purchased<>0 or opening_closing_stock.companycode='" & Module1.companycode & "' and yearcode ='" & Module1.yearcode & "' and storecode='" & shop_code & "' and bottles_sold<>0 or opening_closing_stock.companycode='" & Module1.companycode & "' and yearcode ='" & Module1.yearcode & "'  and storecode='" & shopcode & "' and stock<>0 group by itemmst.companycode,yearcode,storecode,groupmst.groupcode,groupname,ml order by groupname,ml "
            stock_load()
            '----------Sales Satetment with & without ml ------------->
        ElseIf Module1.report_no = 7 Then
            sql = "select itemname, qnty as qnty_sold,rate,itemamount as amt,ml,companycode   from sales_statement('" & DateTimePicker1.Value.Date & "','" & DateTimePicker1.Value.Date & "') order by itemname"
            sql1 = "select  sum(tot_discount) as tot_discount, sum(tot_net) as tot_net from(SELECT SUM(discamount) AS tot_discount, SUM(netamount) AS tot_net FROM salesbillmain where companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "'  and (trndate = '" & DateTimePicker1.Value.Date & "') AND salestype<>'RETURN' GROUP BY trndate union all SELECT (-1)*SUM(discamount) AS tot_discount, (-1)*SUM(netamount) AS tot_net FROM salesbillmain where companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "' and (trndate = '" & DateTimePicker1.Value.Date & "') AND salestype='RETURN' GROUP BY trndate )x "
            sql2 = "select isnull(sum(bottles_sold),0)as tot_qnty,isnull(sum(amnt),0)as tot_amnt from vw_cash_sale_daywise where companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "' and trndate='" & DateTimePicker1.Value.Date & "' "
            sql3 = "select isnull(sum(bottles_sold),0)as tot_qnty,isnull(sum(amnt),0)as tot_amnt from vw_return_sale_daywise where companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "' and trndate='" & DateTimePicker1.Value.Date & "' "
            sql4 = "select isnull(sum(bottles_sold),0)as tot_qnty,isnull(sum(amnt),0)as tot_amnt from vw_credit_sale_daywise where companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "' and trndate='" & DateTimePicker1.Value.Date & "' "
            sql5 = "select isnull(sum(bottles_sold),0)as tot_qnty,isnull(sum(amnt),0)as tot_amnt from vw_creditcard_sale_daywise where companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "' and trndate='" & DateTimePicker1.Value.Date & "' "
            sale_load()
        ElseIf Module1.report_no = 8 Then
            sql = "select itemname, qnty as qnty_sold,rate,itemamount as amt,ml,companycode   from sales_statement('" & dtstr1 & "','" & dtstr2 & "') order by itemname"
            sql1 = "SELECT SUM(discamount) AS tot_discount, SUM(netamount) AS tot_net FROM salesbillmain where companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "' and month(trndate)='" & DateTimePicker1.Value.Month & "'and  year(trndate)='" & DateTimePicker1.Value.Year & "' GROUP BY month(trndate)"  ' and salestype<>'RETURN'
            sql2 = "select isnull(sum(bottles_sold),0)as tot_qnty,isnull(sum(amnt),0)as tot_amnt from vw_cash_sale_daywise where companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "' and month(trndate)='" & DateTimePicker1.Value.Month & "'and  year(trndate)='" & DateTimePicker1.Value.Year & "'"
            sql3 = "select isnull(sum(bottles_sold),0)as tot_qnty,isnull(sum(amnt),0)as tot_amnt from vw_return_sale_daywise where companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "' and month(trndate)='" & DateTimePicker1.Value.Month & "'and  year(trndate)='" & DateTimePicker1.Value.Year & "'"
            sql4 = "select isnull(sum(bottles_sold),0)as tot_qnty,isnull(sum(amnt),0)as tot_amnt from vw_credit_sale_daywise where companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "' and month(trndate)='" & DateTimePicker1.Value.Month & "'and  year(trndate)='" & DateTimePicker1.Value.Year & "'"
            sql5 = "select isnull(sum(bottles_sold),0)as tot_qnty,isnull(sum(amnt),0)as tot_amnt from vw_creditcard_sale_daywise where companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "' and month(trndate)='" & DateTimePicker1.Value.Month & "'and  year(trndate)='" & DateTimePicker1.Value.Year & "'"
            sale_load()
            '----sale statement report ----------------------->
        ElseIf Module1.report_no = 9 Then
            sql = "select itemname, qnty as qnty_sold,rate,itemamount as amt,ml,companycode   from sales_statement('" & DateTimePicker1.Value.Date & "','" & DateTimePicker2.Value.Date & "') order by itemname"
            sql1 = "select sum(discamount) AS tot_discount, SUM(netamount) AS tot_net FROM salesbillmain where companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "' and (trndate >= '" & DateTimePicker1.Value.Date & "'AND trndate <= '" & DateTimePicker2.Value.Date & "') AND salestype<>'RETURN' GROUP BY trndate"
            sql2 = "select isnull(sum(bottles_sold),0)as tot_qnty,isnull(sum(amnt),0)as tot_amnt from vw_cash_sale_daywise where companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "' and (trndate >= '" & DateTimePicker1.Value.Date & "'AND trndate <= '" & DateTimePicker2.Value.Date & "')"
            sql3 = "select isnull(sum(bottles_sold),0)as tot_qnty,isnull(sum(amnt),0)as tot_amnt from vw_return_sale_daywise where companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "' and (trndate >= '" & DateTimePicker1.Value.Date & "'AND trndate <= '" & DateTimePicker2.Value.Date & "')"
            sql4 = "select isnull(sum(bottles_sold),0)as tot_qnty,isnull(sum(amnt),0)as tot_amnt from vw_credit_sale_daywise where companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "' and (trndate >= '" & DateTimePicker1.Value.Date & "'AND trndate <= '" & DateTimePicker2.Value.Date & "')"
            sql5 = "select isnull(sum(bottles_sold),0)as tot_qnty,isnull(sum(amnt),0)as tot_amnt from vw_creditcard_sale_daywise where companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "' and (trndate >= '" & DateTimePicker1.Value.Date & "'AND trndate <= '" & DateTimePicker2.Value.Date & "')"
            sale_load()
        ElseIf Module1.report_no = 10 Then
            '-----report for stock by item daily --------->
        ElseIf Module1.report_no = 11 Then
            sql = "select itemname,bottles_sold,stock from opening_closing_stock('" & Module1.comstdate & "','" & DateTimePicker1.Value.Date & "','" & DateTimePicker1.Value & "') where companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "'" + querystring + " and stock<>0 or  companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "'" + querystring + " and bottles_sold<>0order by itemname"
            stock_itemwise()
            '----report for stock by item monthly ----------->
        ElseIf Module1.report_no = 12 Then
            sql = "select itemname,bottles_sold,stock from opening_closing_stock('" & Module1.comstdate & "','" & dtstr1 & "','" & dtstr2 & "') where companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "'" + querystring + " and stock<>0 or  companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "'" + querystring + " and bottles_sold<>0order by itemname"
            stock_itemwise()
            '---report for stock by item day to day----->
        ElseIf Module1.report_no = 13 Then
            sql = "select itemname,bottles_sold,stock from opening_closing_stock('" & Module1.comstdate & "','" & DateTimePicker1.Value.Date & "','" & DateTimePicker2.Value & "') where companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "'" + querystring + " and stock<>0 or  companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "'" + querystring + " and bottles_sold<>0order by itemname"
            stock_itemwise()
            '----------------- stock by ml ------------->
        ElseIf Module1.report_no = 14 Then
            sql = "select ml as group_by_name, sum(opening_stock)as opening,sum(bottles_purchased)as purchased,sum(bottles_sold)as sold,sum(stock)as closing from opening_closing_stock('" & Module1.comstdate & "','" & DateTimePicker1.Value & "','" & DateTimePicker1.Value & "') join itemmst on itemmst.itemcode=opening_closing_stock.itemcode and itemmst.companycode=opening_closing_stock.companycode where opening_closing_stock.companycode='" & Module1.companycode & "' and yearcode ='" & Module1.yearcode & "' " + querystring + " and opening_stock<>0 or opening_closing_stock.companycode='" & Module1.companycode & "' and yearcode ='" & Module1.yearcode & "' " + querystring + " and bottles_purchased<>0 or opening_closing_stock.companycode='" & Module1.companycode & "' and yearcode ='" & Module1.yearcode & "' " + querystring + " and  bottles_sold<>0 or opening_closing_stock.companycode='" & Module1.companycode & "' and yearcode ='" & Module1.yearcode & "' " + querystring + " and stock<>0 group by ml"
            stock_load()
        ElseIf Module1.report_no = 15 Then
            sql = "select ml as group_by_name, sum(opening_stock)as opening,sum(bottles_purchased)as purchased,sum(bottles_sold)as sold,sum(stock)as closing from opening_closing_stock('" & Module1.comstdate & "','" & dtstr1 & "','" & dtstr2 & "') join itemmst on itemmst.itemcode=opening_closing_stock.itemcode and itemmst.companycode=opening_closing_stock.companycode where opening_closing_stock.companycode='" & Module1.companycode & "' and yearcode ='" & Module1.yearcode & "' " + querystring + " and opening_stock<>0 or opening_closing_stock.companycode='" & Module1.companycode & "' and yearcode ='" & Module1.yearcode & "' " + querystring + " and bottles_purchased<>0 or opening_closing_stock.companycode='" & Module1.companycode & "' and yearcode ='" & Module1.yearcode & "' " + querystring + " and  bottles_sold<>0 or opening_closing_stock.companycode='" & Module1.companycode & "' and yearcode ='" & Module1.yearcode & "' " + querystring + " and stock<>0 group by ml"
            stock_load()
        ElseIf Module1.report_no = 16 Then
            sql = "select ml as group_by_name, sum(opening_stock)as opening,sum(bottles_purchased)as purchased,sum(bottles_sold)as sold,sum(stock)as closing from opening_closing_stock('" & Module1.comstdate & "','" & DateTimePicker1.Value.Date & "','" & DateTimePicker2.Value.Date & "') join itemmst on itemmst.itemcode=opening_closing_stock.itemcode and itemmst.companycode=opening_closing_stock.companycode where opening_closing_stock.companycode='" & Module1.companycode & "' and yearcode ='" & Module1.yearcode & "' " + querystring + " and opening_stock<>0 or opening_closing_stock.companycode='" & Module1.companycode & "' and yearcode ='" & Module1.yearcode & "' " + querystring + " and bottles_purchased<>0 or opening_closing_stock.companycode='" & Module1.companycode & "' and yearcode ='" & Module1.yearcode & "' " + querystring + " and  bottles_sold<>0 or opening_closing_stock.companycode='" & Module1.companycode & "' and yearcode ='" & Module1.yearcode & "' " + querystring + " and stock<>0 group by ml"
            stock_load()
        ElseIf Module1.report_no = 17 Then
            sql = "SELECT purchasedetail.companycode, purchasedetail.yearcode, purchasemain.trndate, purchasemain.docno AS billno, purchasedetail.trnno,itemmst.itemname, purchasedetail.itembox AS box, purchasedetail.itemloose AS loose, purchasedetail.itemquantity AS qty, purchasedetail.itemrate AS rate, purchasedetail.itemamount AS amt FROM purchasedetail INNER JOIN purchasemain ON purchasedetail.companycode = purchasemain.companycode AND purchasedetail.yearcode = purchasemain.yearcode AND purchasedetail.trnno = purchasemain.trnno INNER JOIN itemmst ON purchasedetail.itemcode = itemmst.itemcode AND purchasedetail.companycode = itemmst.companycode where purchasedetail.companycode='" & Module1.companycode & "' and purchasedetail.yearcode='" & Module1.yearcode & "' and purchasemain.trndate='" & DateTimePicker1.Value.Date & "' "
            sql1 = "SELECT companymst.companyname, purchasemain.companycode, purchasemain.yearcode, purchasemain.trndate, purchasemain.docno as billno, purchasemain.trnno, purchasemain.ptype, ledger.name as party,ledger.name as group_column, purchasemain.schemecode, purchasemain.totnetamt as net FROM purchasemain INNER JOIN ledger ON purchasemain.companycode = ledger.companycode AND purchasemain.suppliercode = ledger.ledcode INNER JOIN companymst ON purchasemain.companycode = companymst.companycode where purchasemain.companycode='" & Module1.companycode & "' and purchasemain.yearcode='" & Module1.yearcode & "' and purchasemain.trndate='" & DateTimePicker1.Value.Date & "'" + querystring
            sql2 = "SELECT taxdetail.taxname, taxdetail.sig, PurchaseTaxDetail.TaxAmount, purchasemain.companycode, purchasemain.yearcode, purchasemain.trnno, purchasemain.docno as billno FROM PurchaseTaxDetail INNER JOIN taxdetail ON PurchaseTaxDetail.companycode = taxdetail.companycode AND PurchaseTaxDetail.schemecode = taxdetail.SchemeCode AND PurchaseTaxDetail.TaxCode = taxdetail.TaxCode INNER JOIN purchasemain ON PurchaseTaxDetail.companycode = purchasemain.companycode AND PurchaseTaxDetail.yearcode = purchasemain.yearcode AND PurchaseTaxDetail.trnno = purchasemain.trnno where purchasemain.companycode='" & Module1.companycode & "' and purchasemain.yearcode='" & Module1.yearcode & "' and purchasemain.trndate='" & DateTimePicker1.Value.Date & "'"
            purchase_load()
        ElseIf Module1.report_no = 18 Then
            sql = "SELECT purchasedetail.companycode, purchasedetail.yearcode, purchasemain.trndate, purchasemain.docno AS billno, purchasedetail.trnno,itemmst.itemname, purchasedetail.itembox AS box, purchasedetail.itemloose AS loose, purchasedetail.itemquantity AS qty, purchasedetail.itemrate AS rate, purchasedetail.itemamount AS amt FROM purchasedetail INNER JOIN purchasemain ON purchasedetail.companycode = purchasemain.companycode AND purchasedetail.yearcode = purchasemain.yearcode AND purchasedetail.trnno = purchasemain.trnno INNER JOIN itemmst ON purchasedetail.itemcode = itemmst.itemcode AND purchasedetail.companycode = itemmst.companycode where purchasedetail.companycode='" & Module1.companycode & "' and purchasedetail.yearcode='" & Module1.yearcode & "'and month(purchasemain.trndate)='" & DateTimePicker1.Value.Month & "'and year(purchasemain.trndate)='" & DateTimePicker1.Value.Year & "' "
            sql1 = "SELECT companymst.companyname, purchasemain.companycode, purchasemain.yearcode, purchasemain.trndate, purchasemain.docno as billno, purchasemain.trnno, purchasemain.ptype, ledger.name as party,ledger.name as group_column, purchasemain.schemecode, purchasemain.totnetamt as net FROM purchasemain INNER JOIN ledger ON purchasemain.companycode = ledger.companycode AND purchasemain.suppliercode = ledger.ledcode INNER JOIN companymst ON purchasemain.companycode = companymst.companycode where purchasemain.companycode='" & Module1.companycode & "' and purchasemain.yearcode='" & Module1.yearcode & "' and month(purchasemain.trndate)='" & DateTimePicker1.Value.Month & "'and year(purchasemain.trndate)='" & DateTimePicker1.Value.Year & "'" + querystring
            sql2 = "SELECT taxdetail.taxname, taxdetail.sig, PurchaseTaxDetail.TaxAmount, purchasemain.companycode, purchasemain.yearcode, purchasemain.trnno, purchasemain.docno as billno FROM PurchaseTaxDetail INNER JOIN taxdetail ON PurchaseTaxDetail.companycode = taxdetail.companycode AND PurchaseTaxDetail.schemecode = taxdetail.SchemeCode AND PurchaseTaxDetail.TaxCode = taxdetail.TaxCode INNER JOIN purchasemain ON PurchaseTaxDetail.companycode = purchasemain.companycode AND PurchaseTaxDetail.yearcode = purchasemain.yearcode AND PurchaseTaxDetail.trnno = purchasemain.trnno where purchasemain.companycode='" & Module1.companycode & "' and purchasemain.yearcode='" & Module1.yearcode & "' and month(purchasemain.trndate)='" & DateTimePicker1.Value.Month & "'and year(purchasemain.trndate)='" & DateTimePicker1.Value.Year & "'"
            purchase_load()
        ElseIf Module1.report_no = 19 Then
            sql = "SELECT purchasedetail.companycode, purchasedetail.yearcode, purchasemain.trndate, purchasemain.docno AS billno, purchasedetail.trnno,itemmst.itemname, purchasedetail.itembox AS box, purchasedetail.itemloose AS loose, purchasedetail.itemquantity AS qty, purchasedetail.itemrate AS rate, purchasedetail.itemamount AS amt FROM purchasedetail INNER JOIN purchasemain ON purchasedetail.companycode = purchasemain.companycode AND purchasedetail.yearcode = purchasemain.yearcode AND purchasedetail.trnno = purchasemain.trnno INNER JOIN itemmst ON purchasedetail.itemcode = itemmst.itemcode AND purchasedetail.companycode = itemmst.companycode where purchasedetail.companycode='" & Module1.companycode & "' and purchasedetail.yearcode='" & Module1.yearcode & "' and purchasemain.trndate>='" & DateTimePicker1.Value.Date & "'and purchasemain.trndate<='" & DateTimePicker2.Value.Date & "' "
            sql1 = "SELECT companymst.companyname, purchasemain.companycode, purchasemain.yearcode, purchasemain.trndate, purchasemain.docno as billno, purchasemain.trnno, purchasemain.ptype, ledger.name as party, ledger.name as group_column,purchasemain.schemecode, purchasemain.totnetamt as net FROM purchasemain INNER JOIN ledger ON purchasemain.companycode = ledger.companycode AND purchasemain.suppliercode = ledger.ledcode INNER JOIN companymst ON purchasemain.companycode = companymst.companycode where purchasemain.companycode='" & Module1.companycode & "' and purchasemain.yearcode='" & Module1.yearcode & "' and purchasemain.trndate>='" & DateTimePicker1.Value.Date & "'and purchasemain.trndate<='" & DateTimePicker2.Value.Date & "'" + querystring
            sql2 = "SELECT taxdetail.taxname, taxdetail.sig, PurchaseTaxDetail.TaxAmount, purchasemain.companycode, purchasemain.yearcode, purchasemain.trnno, purchasemain.docno as billno FROM PurchaseTaxDetail INNER JOIN taxdetail ON PurchaseTaxDetail.companycode = taxdetail.companycode AND PurchaseTaxDetail.schemecode = taxdetail.SchemeCode AND PurchaseTaxDetail.TaxCode = taxdetail.TaxCode INNER JOIN purchasemain ON PurchaseTaxDetail.companycode = purchasemain.companycode AND PurchaseTaxDetail.yearcode = purchasemain.yearcode AND PurchaseTaxDetail.trnno = purchasemain.trnno where purchasemain.companycode='" & Module1.companycode & "' and purchasemain.yearcode='" & Module1.yearcode & "' and purchasemain.trndate>='" & DateTimePicker1.Value.Date & "'and purchasemain.trndate<='" & DateTimePicker2.Value.Date & "'"
            purchase_load()
        ElseIf Module1.report_no = 20 Then
            sql = "SELECT purchasedetail.companycode, purchasedetail.yearcode, purchasemain.trndate, purchasemain.docno AS billno, purchasedetail.trnno,itemmst.itemname, purchasedetail.itembox AS box, purchasedetail.itemloose AS loose, purchasedetail.itemquantity AS qty, purchasedetail.itemrate AS rate, purchasedetail.itemamount AS amt FROM purchasedetail INNER JOIN purchasemain ON purchasedetail.companycode = purchasemain.companycode AND purchasedetail.yearcode = purchasemain.yearcode AND purchasedetail.trnno = purchasemain.trnno INNER JOIN itemmst ON purchasedetail.itemcode = itemmst.itemcode AND purchasedetail.companycode = itemmst.companycode where purchasedetail.companycode='" & Module1.companycode & "' and purchasedetail.yearcode='" & Module1.yearcode & "' and purchasemain.trndate='" & DateTimePicker1.Value.Date & "' "
            sql1 = "SELECT companymst.companyname, purchasemain.companycode, purchasemain.yearcode, purchasemain.trndate, purchasemain.docno as billno, purchasemain.trnno, purchasemain.ptype, ledger.name as party,purchasemain.docno as group_column, purchasemain.schemecode, purchasemain.totnetamt as net FROM purchasemain INNER JOIN ledger ON purchasemain.companycode = ledger.companycode AND purchasemain.suppliercode = ledger.ledcode INNER JOIN companymst ON purchasemain.companycode = companymst.companycode where purchasemain.companycode='" & Module1.companycode & "' and purchasemain.yearcode='" & Module1.yearcode & "' and purchasemain.trndate='" & DateTimePicker1.Value.Date & "'" + querystring
            sql2 = "SELECT taxdetail.taxname, taxdetail.sig, PurchaseTaxDetail.TaxAmount, purchasemain.companycode, purchasemain.yearcode, purchasemain.trnno, purchasemain.docno as billno FROM PurchaseTaxDetail INNER JOIN taxdetail ON PurchaseTaxDetail.companycode = taxdetail.companycode AND PurchaseTaxDetail.schemecode = taxdetail.SchemeCode AND PurchaseTaxDetail.TaxCode = taxdetail.TaxCode INNER JOIN purchasemain ON PurchaseTaxDetail.companycode = purchasemain.companycode AND PurchaseTaxDetail.yearcode = purchasemain.yearcode AND PurchaseTaxDetail.trnno = purchasemain.trnno where purchasemain.companycode='" & Module1.companycode & "' and purchasemain.yearcode='" & Module1.yearcode & "' and purchasemain.trndate='" & DateTimePicker1.Value.Date & "'"
            purchase_load()
        ElseIf Module1.report_no = 21 Then
            sql = "SELECT purchasedetail.companycode, purchasedetail.yearcode, purchasemain.trndate, purchasemain.docno AS billno, purchasedetail.trnno,itemmst.itemname, purchasedetail.itembox AS box, purchasedetail.itemloose AS loose, purchasedetail.itemquantity AS qty, purchasedetail.itemrate AS rate, purchasedetail.itemamount AS amt FROM purchasedetail INNER JOIN purchasemain ON purchasedetail.companycode = purchasemain.companycode AND purchasedetail.yearcode = purchasemain.yearcode AND purchasedetail.trnno = purchasemain.trnno INNER JOIN itemmst ON purchasedetail.itemcode = itemmst.itemcode AND purchasedetail.companycode = itemmst.companycode where purchasedetail.companycode='" & Module1.companycode & "' and purchasedetail.yearcode='" & Module1.yearcode & "'and month(purchasemain.trndate)='" & DateTimePicker1.Value.Month & "'and year(purchasemain.trndate)='" & DateTimePicker1.Value.Year & "' "
            sql1 = "SELECT companymst.companyname, purchasemain.companycode, purchasemain.yearcode, purchasemain.trndate, purchasemain.docno as billno, purchasemain.trnno, purchasemain.ptype, ledger.name as party,purchasemain.docno as group_column, purchasemain.schemecode, purchasemain.totnetamt as net FROM purchasemain INNER JOIN ledger ON purchasemain.companycode = ledger.companycode AND purchasemain.suppliercode = ledger.ledcode INNER JOIN companymst ON purchasemain.companycode = companymst.companycode where purchasemain.companycode='" & Module1.companycode & "' and purchasemain.yearcode='" & Module1.yearcode & "' and month(purchasemain.trndate)='" & DateTimePicker1.Value.Month & "'and year(purchasemain.trndate)='" & DateTimePicker1.Value.Year & "'" + querystring
            sql2 = "SELECT taxdetail.taxname, taxdetail.sig, PurchaseTaxDetail.TaxAmount, purchasemain.companycode, purchasemain.yearcode, purchasemain.trnno, purchasemain.docno as billno FROM PurchaseTaxDetail INNER JOIN taxdetail ON PurchaseTaxDetail.companycode = taxdetail.companycode AND PurchaseTaxDetail.schemecode = taxdetail.SchemeCode AND PurchaseTaxDetail.TaxCode = taxdetail.TaxCode INNER JOIN purchasemain ON PurchaseTaxDetail.companycode = purchasemain.companycode AND PurchaseTaxDetail.yearcode = purchasemain.yearcode AND PurchaseTaxDetail.trnno = purchasemain.trnno where purchasemain.companycode='" & Module1.companycode & "' and purchasemain.yearcode='" & Module1.yearcode & "' and month(purchasemain.trndate)='" & DateTimePicker1.Value.Month & "'and year(purchasemain.trndate)='" & DateTimePicker1.Value.Year & "'"
            purchase_load()
        ElseIf Module1.report_no = 22 Then
            sql = "SELECT purchasedetail.companycode, purchasedetail.yearcode, purchasemain.trndate, purchasemain.docno AS billno, purchasedetail.trnno,itemmst.itemname, purchasedetail.itembox AS box, purchasedetail.itemloose AS loose, purchasedetail.itemquantity AS qty, purchasedetail.itemrate AS rate, purchasedetail.itemamount AS amt FROM purchasedetail INNER JOIN purchasemain ON purchasedetail.companycode = purchasemain.companycode AND purchasedetail.yearcode = purchasemain.yearcode AND purchasedetail.trnno = purchasemain.trnno INNER JOIN itemmst ON purchasedetail.itemcode = itemmst.itemcode AND purchasedetail.companycode = itemmst.companycode where purchasedetail.companycode='" & Module1.companycode & "' and purchasedetail.yearcode='" & Module1.yearcode & "' and purchasemain.trndate>='" & DateTimePicker1.Value.Date & "'and purchasemain.trndate<='" & DateTimePicker2.Value.Date & "' "
            sql1 = "SELECT companymst.companyname, purchasemain.companycode, purchasemain.yearcode, purchasemain.trndate, purchasemain.docno as billno, purchasemain.trnno, purchasemain.ptype, ledger.name as party, purchasemain.docno as group_column,purchasemain.schemecode, purchasemain.totnetamt as net FROM purchasemain INNER JOIN ledger ON purchasemain.companycode = ledger.companycode AND purchasemain.suppliercode = ledger.ledcode INNER JOIN companymst ON purchasemain.companycode = companymst.companycode where purchasemain.companycode='" & Module1.companycode & "' and purchasemain.yearcode='" & Module1.yearcode & "' and purchasemain.trndate>='" & DateTimePicker1.Value.Date & "'and purchasemain.trndate<='" & DateTimePicker2.Value.Date & "'" + querystring
            sql2 = "SELECT taxdetail.taxname, taxdetail.sig, PurchaseTaxDetail.TaxAmount, purchasemain.companycode, purchasemain.yearcode, purchasemain.trnno, purchasemain.docno as billno FROM PurchaseTaxDetail INNER JOIN taxdetail ON PurchaseTaxDetail.companycode = taxdetail.companycode AND PurchaseTaxDetail.schemecode = taxdetail.SchemeCode AND PurchaseTaxDetail.TaxCode = taxdetail.TaxCode INNER JOIN purchasemain ON PurchaseTaxDetail.companycode = purchasemain.companycode AND PurchaseTaxDetail.yearcode = purchasemain.yearcode AND PurchaseTaxDetail.trnno = purchasemain.trnno where purchasemain.companycode='" & Module1.companycode & "' and purchasemain.yearcode='" & Module1.yearcode & "' and purchasemain.trndate>='" & DateTimePicker1.Value.Date & "'and purchasemain.trndate<='" & DateTimePicker2.Value.Date & "'"
            purchase_load()
        ElseIf Module1.report_no = 23 Then
            sql = "SELECT purchasedetail.companycode, purchasedetail.yearcode, purchasedetail.trnno, itemmst.itemname, itemmst.itemname AS group_column, SUM(purchasedetail.itembox) AS box, SUM(purchasedetail.itemloose) AS loose, SUM(purchasedetail.itemquantity) AS qty,                       purchasedetail.itemrate AS rate, SUM(purchasedetail.itemamount) AS amt, ledger.name AS party, purchasemain.docno AS billno, purchasemain.trndate, companymst.companyname FROM purchasedetail INNER JOIN purchasemain ON purchasedetail.companycode = purchasemain.companycode AND purchasedetail.yearcode = purchasemain.yearcode AND purchasedetail.trnno = purchasemain.trnno INNER JOIN itemmst ON purchasedetail.itemcode = itemmst.itemcode AND purchasedetail.companycode = itemmst.companycode INNER JOIN ledger ON purchasemain.companycode = ledger.companycode AND purchasemain.suppliercode = ledger.ledcode INNER JOIN companymst ON purchasemain.companycode = companymst.companycode WHERE (purchasedetail.companycode = '" & Module1.companycode & "') AND (purchasedetail.yearcode = '" & Module1.yearcode & "') AND (purchasemain.trndate = '" & DateTimePicker1.Value.Date & "') " + querystring + " GROUP BY itemmst.itemname, purchasedetail.trnno, purchasedetail.itemrate, ledger.name, purchasemain.docno, companymst.companyname, purchasedetail.companycode, purchasedetail.yearcode, purchasemain.trndate ORDER BY itemmst.itemname"
            purchase_load_itemwise()
        ElseIf Module1.report_no = 24 Then
            sql = "SELECT purchasedetail.companycode, purchasedetail.yearcode, purchasedetail.trnno, itemmst.itemname, itemmst.itemname AS group_column, SUM(purchasedetail.itembox) AS box, SUM(purchasedetail.itemloose) AS loose, SUM(purchasedetail.itemquantity) AS qty,                       purchasedetail.itemrate AS rate, SUM(purchasedetail.itemamount) AS amt, ledger.name AS party, purchasemain.docno AS billno, purchasemain.trndate, companymst.companyname FROM purchasedetail INNER JOIN purchasemain ON purchasedetail.companycode = purchasemain.companycode AND purchasedetail.yearcode = purchasemain.yearcode AND purchasedetail.trnno = purchasemain.trnno INNER JOIN itemmst ON purchasedetail.itemcode = itemmst.itemcode AND purchasedetail.companycode = itemmst.companycode INNER JOIN ledger ON purchasemain.companycode = ledger.companycode AND purchasemain.suppliercode = ledger.ledcode INNER JOIN companymst ON purchasemain.companycode = companymst.companycode WHERE (purchasedetail.companycode = '" & Module1.companycode & "') AND (purchasedetail.yearcode = '" & Module1.yearcode & "') AND (month(purchasemain.trndate) = '" & DateTimePicker1.Value.Month & "')AND (year(purchasemain.trndate) = '" & DateTimePicker1.Value.Year & "')" + querystring + " GROUP BY itemmst.itemname, purchasedetail.trnno, purchasedetail.itemrate, ledger.name, purchasemain.docno, companymst.companyname, purchasedetail.companycode, purchasedetail.yearcode, purchasemain.trndate ORDER BY itemmst.itemname"
            purchase_load_itemwise()
        ElseIf Module1.report_no = 25 Then
            sql = "SELECT purchasedetail.companycode, purchasedetail.yearcode, purchasedetail.trnno, itemmst.itemname, itemmst.itemname AS group_column, SUM(purchasedetail.itembox) AS box, SUM(purchasedetail.itemloose) AS loose, SUM(purchasedetail.itemquantity) AS qty,                       purchasedetail.itemrate AS rate, SUM(purchasedetail.itemamount) AS amt, ledger.name AS party, purchasemain.docno AS billno, purchasemain.trndate, companymst.companyname FROM purchasedetail INNER JOIN purchasemain ON purchasedetail.companycode = purchasemain.companycode AND purchasedetail.yearcode = purchasemain.yearcode AND purchasedetail.trnno = purchasemain.trnno INNER JOIN itemmst ON purchasedetail.itemcode = itemmst.itemcode AND purchasedetail.companycode = itemmst.companycode INNER JOIN ledger ON purchasemain.companycode = ledger.companycode AND purchasemain.suppliercode = ledger.ledcode INNER JOIN companymst ON purchasemain.companycode = companymst.companycode WHERE (purchasedetail.companycode = '" & Module1.companycode & "') AND (purchasedetail.yearcode = '" & Module1.yearcode & "') AND (purchasemain.trndate >= '" & DateTimePicker1.Value.Date & "')AND (purchasemain.trndate <= '" & DateTimePicker2.Value.Date & "')" + querystring + " GROUP BY itemmst.itemname, purchasedetail.trnno, purchasedetail.itemrate, ledger.name, purchasemain.docno, companymst.companyname, purchasedetail.companycode, purchasedetail.yearcode, purchasemain.trndate ORDER BY itemmst.itemname"
            purchase_load_itemwise()
        ElseIf Module1.report_no = 26 Then
            sql = "select purchasemain.companycode,purchasemain.yearcode,docno as billno,docno as group_column,ledger.name as party,trnno,trndate,totamount as amount,tottaxoth as taxothers,totnetamt as net from purchasemain INNER JOIN ledger ON purchasemain.companycode = ledger.companycode AND purchasemain.suppliercode = ledger.ledcode WHERE (purchasemain.companycode = '" & Module1.companycode & "') AND (purchasemain.yearcode = '" & Module1.yearcode & "') AND (purchasemain.trndate = '" & DateTimePicker1.Value.Date & "') " + querystring + " order by docno"
            purchase_billsummary_load()
        ElseIf Module1.report_no = 27 Then
            sql = "select purchasemain.companycode,purchasemain.yearcode,docno as billno,docno as group_column,ledger.name as party,trnno,trndate,totamount as amount,tottaxoth as taxothers,totnetamt as net from purchasemain INNER JOIN ledger ON purchasemain.companycode = ledger.companycode AND purchasemain.suppliercode = ledger.ledcode WHERE (purchasemain.companycode = '" & Module1.companycode & "') AND (purchasemain.yearcode = '" & Module1.yearcode & "') AND (month(purchasemain.trndate) = '" & DateTimePicker1.Value.Month & "')AND (year(purchasemain.trndate) = '" & DateTimePicker1.Value.Year & "')" + querystring + " order by docno"
            purchase_billsummary_load()
        ElseIf Module1.report_no = 28 Then
            sql = "select purchasemain.companycode,purchasemain.yearcode,docno as billno,docno as group_column,ledger.name as party,trnno,trndate,totamount as amount,tottaxoth as taxothers,totnetamt as net from purchasemain INNER JOIN ledger ON purchasemain.companycode = ledger.companycode AND purchasemain.suppliercode = ledger.ledcode WHERE (purchasemain.companycode = '" & Module1.companycode & "') AND (purchasemain.yearcode = '" & Module1.yearcode & "') AND (purchasemain.trndate >= '" & DateTimePicker1.Value.Date & "')AND (purchasemain.trndate <= '" & DateTimePicker2.Value.Date & "')" + querystring + " order by docno"
            purchase_billsummary_load()
            '----accounts report---->
        ElseIf Module1.report_no = 35 Then
            '---declaring the variables---->
            Dim dr_opening_balance As Double
            Dim cr_opening_balance As Double
            Dim sum_debit As Double
            Dim sum_credit As Double
            Dim closing_debit As Double
            Dim closing_credit As Double
            '---initialising the variables--->
            dr_opening_balance = 0
            cr_opening_balance = 0
            sum_debit = 0
            sum_credit = 0
            closing_debit = 0
            closing_credit = 0
            '--query to fill up the dataset --->
            sql = "select date,vchtype,billno,name,credit as debit,debit as credit from vw_receipt_accounts where billno in(select  distinct billno from vw_receipt_accounts where ledgercode='" & ledger_code & "' and yearcode='" & Module1.yearcode & "' and companycode='" & Module1.companycode & "' and date>='" & DateTimePicker1.Value.Date & "' and date<='" & DateTimePicker2.Value.Date & "') and credit-debit in(select debit-credit from vw_receipt_accounts where ledgercode='" & ledger_code & "' and companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "' and date>='" & DateTimePicker1.Value.Date & "' and date<='" & DateTimePicker2.Value.Date & "') and date>='" & DateTimePicker1.Value.Date & "' and date<='" & DateTimePicker2.Value.Date & "' and ledgercode<>'" & ledger_code & "' and yearcode='" & Module1.yearcode & "' and companycode='" & Module1.companycode & "'  order by date,billno"
            '----creating the opening balance if any----->
            s = "select sum(openingbalance) from(select isnull(sum(credit-debit),0) as openingbalance from vw_receipt_accounts where billno in(select  distinct billno from vw_receipt_accounts where ledgercode='" & ledger_code & "' and yearcode='" & Module1.yearcode & "' and companycode='" & Module1.companycode & "' and date>='" & Module1.comstdate & "' and date<'" & DateTimePicker1.Value.Date & "') and credit-debit in(select debit-credit from vw_receipt_accounts  where ledgercode='" & ledger_code & "' and companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "' and date>='" & Module1.comstdate & "' and date<'" & DateTimePicker1.Value.Date & "') and date>='" & Module1.comstdate & "' and date<'" & DateTimePicker1.Value.Date & "' and ledgercode<>'" & ledger_code & "' and yearcode='" & Module1.yearcode & "' and companycode='" & Module1.companycode & "' union all select isnull(sum(debit-credit),0) as openingbalance from ledger where ledcode='" & ledger_code & "' and companycode='" & Module1.companycode & "')as x"
            '---calculating if the opening balance should be on the debit side or the credit side
            If Not ob.executereader(s) = Nothing Then
                dr_opening_balance = Convert.ToDouble(ob.executereader(s))
                If dr_opening_balance < 0 Then
                    cr_opening_balance = -1 * dr_opening_balance
                    dr_opening_balance = 0
                End If
            End If
            data_set = New ds_Accounts
            '---first closing the connection before opening it --->
            Module1.closecon()
            Module1.opencon()
            Dim da As New SqlDataAdapter(sql, Module1.con)
            da.Fill(data_set, "Accounts")
            Module1.closecon()
            '---creting a new table to poppulate with the companyname,ledgername and date -->
            Dim dt As DataTable
            Dim dr As DataRow
            dt = data_set.Tables("table2")
            dr = dt.NewRow
            '---getting the company name ------->
            dr("companyname") = Module1.companyname
            '---getting the party name------>
            dr("Party_Name") = cmb_rptselector.Text
            '---getting the value for from date to date --->
            dr("Date_String") = "LEDGER STATEMENT FROM " & Format(DateTimePicker2.Value.Date, "dd/MM/yyyy").ToString & " TO " & Format(DateTimePicker2.Value.Date, "dd/MM/yyyy").ToString
            '----filling the values of the debit openig balance---->
            dr("dr_opening_balance") = dr_opening_balance
            '----filling the values of the credit openig balance---->
            dr("cr_opening_balance") = cr_opening_balance
            '---filling the total debit ------>
            Dim query = (From p As DataRow In data_set.Tables(0) Select p.Field(Of Double)(4)).Sum
            sum_debit = query
            dr("sum_debit") = query
            '---filling the total credit ---->
            query = (From p As DataRow In data_set.Tables(0) Select p.Field(Of Double)(5)).Sum
            sum_credit = query
            dr("sum_credit") = query
            '---putting the values in the closing debit and credit --->
            If dr_opening_balance + sum_debit > cr_opening_balance + sum_credit Then
                closing_debit = (dr_opening_balance + sum_debit) - (cr_opening_balance + sum_credit)
                closing_credit = 0
            ElseIf cr_opening_balance + sum_credit > dr_opening_balance + sum_debit Then
                closing_credit = (cr_opening_balance + sum_credit) - (dr_opening_balance + sum_debit)
                closing_debit = 0
            End If
            '---filling the closing debit --->
            dr("closing_debit") = closing_debit
            '--filling the closing credit --->
            dr("closing_credit") = closing_credit
            '---filling up the date for the openingbalance--->
            dr("date") = DateTimePicker1.Value.Date.ToString
            '---adding the rows back to the dataset       --->
            data_set.Tables("table2").Rows.Add(dr)
            '--filling up the report form ---->
            Rpt = New rep_Accounts
            '--ml wise daily sales report ----->
        ElseIf Module1.report_no = 38 Then
            sql = "select itemname, qnty as qnty_sold,rate,itemamount as amt,ml,companycode   from sales_statement('" & DateTimePicker1.Value.Date & "','" & DateTimePicker2.Value.Date & "') where companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "' order by ml desc,itemname"
            sql1 = "SELECT SUM(discamount) AS tot_discount, SUM(netamount) AS tot_net FROM salesbillmain where companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "' and (trndate >= '" & DateTimePicker1.Value.Date & "'AND trndate <= '" & DateTimePicker2.Value.Date & "') AND salestype<>'RETURN' GROUP BY trndate"
            sql2 = "select isnull(sum(bottles_sold),0)as tot_qnty,isnull(sum(amnt),0)as tot_amnt from vw_cash_sale_daywise where companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "' and (trndate >= '" & DateTimePicker1.Value.Date & "'AND trndate <= '" & DateTimePicker2.Value.Date & "')"
            sql3 = "select isnull(sum(bottles_sold),0)as tot_qnty,isnull(sum(amnt),0)as tot_amnt from vw_return_sale_daywise where companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "' and (trndate >= '" & DateTimePicker1.Value.Date & "'AND trndate <= '" & DateTimePicker2.Value.Date & "')"
            sql4 = "select isnull(sum(bottles_sold),0)as tot_qnty,isnull(sum(amnt),0)as tot_amnt from vw_credit_sale_daywise where companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "' and (trndate >= '" & DateTimePicker1.Value.Date & "'AND trndate <= '" & DateTimePicker2.Value.Date & "')"
            sql5 = "select isnull(sum(bottles_sold),0)as tot_qnty,isnull(sum(amnt),0)as tot_amnt from vw_creditcard_sale_daywise where companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "' and (trndate >= '" & DateTimePicker1.Value.Date & "'AND trndate <= '" & DateTimePicker2.Value.Date & "')"
            sale_load()
            '------- Sales report item wise - daily --------------->
        ElseIf Module1.report_no = 39 Then
            sql = "select salesbillmain.trndate,salesbilldetail.trnno,itemname,ml,sum(qnty) as qnty_sold,rate,sum(itemamount) as amt,name as party,name as group_by_col  from salesbilldetail join itemmst on salesbilldetail.companycode=itemmst.companycode and salesbilldetail.itemcode=itemmst.itemcode join salesbillmain on salesbilldetail.companycode=salesbillmain.companycode and salesbilldetail.yearcode=salesbillmain.yearcode and salesbilldetail.trnno=salesbillmain.trnno join ledger on salesbillmain.companycode=ledger.companycode and salesbillmain.ledgercode=ledcode where salesbillmain.trndate='" & DateTimePicker1.Value.Date & "' and salesbilldetail.companycode='" & Module1.companycode & "' and salesbilldetail.yearcode='" & yearcode & "' '" + querystring + "' "  'group by itemname,itemmst.ml
            sales_all_load()
            '--------Sales report item wise - monthly-------------->
        ElseIf Module1.report_no = 40 Then
            sql = "select salesbillmain.trndate,salesbilldetail.trnno,itemname,ml,qnty as qnty_sold,rate,itemamount as amt,name as party,name as group_by_col  from salesbilldetail join itemmst on salesbilldetail.companycode=itemmst.companycode and salesbilldetail.itemcode=itemmst.itemcode join salesbillmain on salesbilldetail.companycode=salesbillmain.companycode and salesbilldetail.yearcode=salesbillmain.yearcode and salesbilldetail.trnno=salesbillmain.trnno join ledger on salesbillmain.companycode=ledger.companycode and salesbillmain.ledgercode=ledcode where salesbillmain.trndate>='" & dtstr1 & "' and salesbillmain.trndate<='" & dtstr2 & "'and salesbilldetail.companycode='" & Module1.companycode & "' and salesbilldetail.yearcode='" & yearcode & "'" + querystring
            sales_all_load()
            '--------Sales report party wise/itemwise/billwise - date range --------------------->
        ElseIf Module1.report_no = 41 Or Module1.report_no = 44 Or Module1.report_no = 47 Or Module1.report_no = 50 Then
            sql = "select salesbillmain.trndate,salesbilldetail.trnno,itemname,ml,qnty as qnty_sold,rate,itemamount as amt,name as party from salesbilldetail join itemmst on salesbilldetail.companycode=itemmst.companycode and salesbilldetail.itemcode=itemmst.itemcode join salesbillmain on salesbilldetail.companycode=salesbillmain.companycode and salesbilldetail.yearcode=salesbillmain.yearcode and salesbilldetail.trnno=salesbillmain.trnno join ledger on salesbillmain.companycode=ledger.companycode and salesbillmain.ledgercode=ledcode where salesbillmain.trndate>='" & DateTimePicker1.Value.Date & "' and salesbillmain.trndate<='" & DateTimePicker2.Value.Date & "'and salesbilldetail.companycode='" & Module1.companycode & "' and salesbilldetail.yearcode='" & yearcode & "' and salestype<>'RETURN'"
            sales_all_load()
            '-------breakage report ------------------------------------------->
        ElseIf Module1.report_no = 51 Then
            sql = "SELECT itemmst.itemname, breakagemain.trndate, itemmst.ml, breakagedetail.quantity, itemmst.purchaserate, (itemmst.purchaserate* breakagedetail.quantity)as value, ledger.name AS party FROM breakagemain INNER JOIN breakagedetail ON breakagemain.CompanyCode = breakagedetail.CompanyCode AND breakagemain.YearCode = breakagedetail.yearcode AND breakagemain.TrnNo = breakagedetail.trnno AND breakagemain.trndate = breakagedetail.trndate INNER JOIN itemmst ON breakagedetail.ItemCode = itemmst.itemcode AND breakagedetail.CompanyCode = itemmst.companycode INNER JOIN ledger ON breakagemain.CompanyCode = ledger.companycode AND breakagemain.ledgercode = ledger.ledcode where breakagemain.companycode='" & Module1.companycode & "' and breakagemain.yearcode='" & Module1.yearcode & "' and breakagemain.trndate>='" & DateTimePicker1.Value.Date & "' and breakagemain.trndate<='" & DateTimePicker2.Value.Date & "'"
            breakage_load()
            '-------- Return Report ------------------------------------------->
        ElseIf Module1.report_no = 52 And shop_code = Nothing Then
            sql = "select convert(varchar(10),salesbillmain.trndate,103)  as trndate,itemname,ml,rate,qnty,itemamount from salesbilldetail join salesbillmain on salesbillmain.trnno=salesbilldetail.trnno and salesbillmain.yearcode=salesbilldetail.yearcode and salesbillmain.companycode=salesbilldetail.companycode join itemmst on itemmst.itemcode=salesbilldetail.itemcode and itemmst.companycode=salesbilldetail.companycode where salestype='RETURN' and salesbillmain.yearcode='" & Module1.yearcode & "' and salesbillmain.companycode='" & Module1.companycode & "' and salesbillmain.trndate>='" & DateTimePicker1.Value.Date & "' and salesbillmain.trndate<='" & DateTimePicker2.Value.Date & "'"
            data_set = New ds_return
            '---filling up the dataset------>
            Module1.closecon()
            Module1.opencon()
            Dim da As New SqlDataAdapter(sql, Module1.con)
            da.Fill(data_set.Tables("dt_return"))
            Module1.closecon()
            '---filing up the table information in the dataset ds_return--->
            Dim dt As DataTable
            Dim dr As DataRow
            dt = data_set.Tables("dt_information")
            dr = dt.NewRow
            dr("companyname") = Module1.companyname
            dr("date_string") = "RETURN REPORT " + date_string
            data_set.Tables("dt_information").Rows.Add(dr)
            '--creating the reference of the crystal report--->
            Rpt = New rep_return
        ElseIf Module1.report_no = 52 And Not shop_code = Nothing Then
            sql = "select convert(varchar(10),salesbillmain.trndate,103)  as trndate,itemname,ml,rate,qnty,itemamount from salesbilldetail join salesbillmain on salesbillmain.trnno=salesbilldetail.trnno and salesbillmain.yearcode=salesbilldetail.yearcode and salesbillmain.companycode=salesbilldetail.companycode join itemmst on itemmst.itemcode=salesbilldetail.itemcode and itemmst.companycode=salesbilldetail.companycode where salestype='RETURN' and storecode='" & shop_code & "' and salesbillmain.yearcode='" & Module1.yearcode & "' and salesbillmain.companycode='" & Module1.companycode & "' and salesbillmain.trndate>='" & DateTimePicker1.Value.Date & "' and salesbillmain.trndate<='" & DateTimePicker2.Value.Date & "'"
            data_set = New ds_return
            '---filling up the dataset------>
            Module1.closecon()
            Module1.opencon()
            Dim da As New SqlDataAdapter(sql, Module1.con)
            da.Fill(data_set.Tables("dt_return"))
            Module1.closecon()
            '---filing up the table information in the dataset ds_retirn--->
            Dim dt As DataTable
            Dim dr As DataRow
            dt = data_set.Tables("dt_information")
            dr = dt.NewRow
            dr("companyname") = Module1.companyname
            dr("date_string") = "RETURN REPORT " + date_string
            data_set.Tables("dt_information").Rows.Add(dr)
            '--creating the reference of the crystal report--->
            Rpt = New rep_return
            '------ Stock Report Value wise detail----------------------------->
        ElseIf Module1.report_no = 53 Then
            sql = "SELECT opening_closing_stock_rep.itemname, itemmst.purchaseRate, opening_closing_stock_rep.rate, SUM(opening_closing_stock_rep.opening_stock) AS opening, SUM(opening_closing_stock_rep.opening_stock * itemmst.purchaseRate) AS opening_val, SUM(opening_closing_stock_rep.bottles_purchased) AS purchased, SUM(opening_closing_stock_rep.bottles_purchased * itemmst.purchaseRate) AS purchase_val, SUM(opening_closing_stock_rep.bottles_sold) AS sold, SUM(opening_closing_stock_rep.bottles_sold * opening_closing_stock_rep.rate) AS sold_val, SUM(opening_closing_stock_rep.stock) AS closing, SUM(opening_closing_stock_rep.stock * itemmst.purchaseRate) AS closing_val FROM opening_closing_stock_rep('" & Module1.comstdate & "', '" & DateTimePicker1.Value.Date & "', '" & DateTimePicker2.Value.Date & "') INNER JOIN itemmst ON opening_closing_stock_rep.companycode = itemmst.companycode AND opening_closing_stock_rep.itemcode = itemmst.itemcode WHERE opening_closing_stock_rep.companycode = '" & Module1.companycode & "' and opening_closing_stock_rep.yearcode='" & Module1.yearcode & "' AND opening_closing_stock_rep.opening_stock <> 0 OR opening_closing_stock_rep.companycode = '" & Module1.companycode & "' and opening_closing_stock_rep.yearcode='" & Module1.yearcode & "' AND opening_closing_stock_rep.bottles_purchased <> 0 OR opening_closing_stock_rep.companycode = '" & Module1.companycode & "' and opening_closing_stock_rep.yearcode='" & Module1.yearcode & "' AND opening_closing_stock_rep.bottles_sold <> 0 OR opening_closing_stock_rep.companycode = '" & Module1.companycode & "' and opening_closing_stock_rep.yearcode='" & Module1.yearcode & "' and opening_closing_stock_rep.stock <> 0 GROUP BY opening_closing_stock_rep.itemname, itemmst.purchaseRate, opening_closing_stock_rep.rate ORDER BY opening_closing_stock_rep.itemname"
            stock_value_load()
            '---------Stock Report Value wise closing---------------------------->
        ElseIf Module1.report_no = 54 Then
            sql = "SELECT opening_closing_stock_rep.itemname, itemmst.purchaseRate, opening_closing_stock_rep.rate, SUM(opening_closing_stock_rep.bottles_sold) AS sold,SUM(opening_closing_stock_rep.bottles_sold * opening_closing_stock_rep.rate) AS sold_val, SUM(opening_closing_stock_rep.stock) AS closing, SUM(opening_closing_stock_rep.stock * itemmst.purchaseRate) AS closing_val FROM dbo.opening_closing_stock_rep('" & Module1.comstdate & "', '" & DateTimePicker1.Value.Date & "', '" & DateTimePicker2.Value.Date & "')INNER JOIN   itemmst ON opening_closing_stock_rep.companycode = itemmst.companycode AND opening_closing_stock_rep.itemcode = itemmst.itemcode WHERE opening_closing_stock_rep.companycode = '" & Module1.companycode & "' and opening_closing_stock_rep.yearcode='" & Module1.yearcode & "' AND opening_closing_stock_rep.bottles_sold <> 0 OR opening_closing_stock_rep.companycode = '" & Module1.companycode & "' and opening_closing_stock_rep.yearcode='" & Module1.yearcode & "' AND opening_closing_stock_rep.stock <> 0 GROUP BY opening_closing_stock_rep.itemname, itemmst.purchaseRate, opening_closing_stock_rep.rate ORDER BY opening_closing_stock_rep.itemname"
            stock_value_load()
            '---report for bult liter ----------------->
        ElseIf Module1.report_no = 55 Then
            '---creating a new instance of the dataset ----->
            data_set = New ds_bulk_liter
            '--creating the query--->
            sql = "select liquor,groupname,kflname,sum(ltr_sale) as  'ltr_sale' from vw_ltr_sale where companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "' and trndate>='" & DateTimePicker1.Value.Date & "' and trndate<='" & DateTimePicker2.Value.Date & "' group by liquor,companycode,yearcode,groupname,kflname order by liquor"
            '---first closing the connection before opening it --->
            Module1.closecon()
            Module1.opencon()
            Dim da As New SqlDataAdapter(sql, Module1.con)
            '---fuilling up the values for datatable1--->
            da.Fill(data_set, "DataTable1")
            Module1.closecon()
            '---filling up the values of datatable2----->
            Dim dt As DataTable
            Dim dr As DataRow
            dt = data_set.Tables("DataTable2")
            dr = dt.NewRow
            '---getting the company name      --->
            dr("companyname") = Module1.companyname
            '---getting the value for date1   --->
            dr("date1") = DateTimePicker1.Value.Date.ToString
            '---getting the value for date2   --->
            dr("date2") = DateTimePicker2.Value.Date.ToString
            data_set.Tables("DataTable2").Rows.Add(dr)
            '----creating a reference of the bulk liter report--->
            Rpt = New rep_bulk_liter
        ElseIf Module1.report_no = 56 Then
            data_set = New ds_excise_report
            sql = "select companycode,yearcode,type,groupname,ml,qnty from xcise_statement('" & Module1.comstdate & "','" & DateTimePicker1.Value.Date & "','" & DateTimePicker2.Value.Date & "') where companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "'"
            '---first closing the connection before opening it --->
            Module1.closecon()
            Module1.opencon()
            Dim da As New SqlDataAdapter(sql, Module1.con)
            '---fuilling up the values for datatable1--->
            da.Fill(data_set, "dt_excise")
            Module1.closecon()
            '--referring the report---->
            Rpt = New rep_excise
        End If


        If Module1.report_no = 35 Or Module1.report_no = 52 Or Module1.report_no = 55 Or Module1.report_no = 56 Then
            Rpt.SetDataSource(data_set)
            CrystalReportViewer1.ReportSource = Rpt
            CrystalReportViewer1.Refresh()
        End If



    End Sub

    Private Sub ReportForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        frm_ContainerForm.ToolStripLabel1.Text = ""
    End Sub

    Private Sub ReportForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        set_control()
        cmb_load()
    End Sub


    Private Sub set_control()
        date_string = ""
        Select Case Module1.report_no
            Case 1, 4, 7, 11, 14, 17, 20, 23, 26, 32, 36, 39, 42, 45
                dtpicker_date_set()
            Case 2, 5, 8, 12, 15, 18, 21, 24, 27, 31, 33, 34, 37, 40, 43, 46
                dtpicker_month_set()
            Case 3, 6, 9, 13, 16, 19, 22, 25, 28, 35, 38, 41, 44, 47, 51, 52, 53, 54
                dtpicker_daterange_set()
        End Select
    End Sub

    '---filling the combobox with the dataset -------------->
    Private Sub cmb_load()
        Select Case Module1.report_no
            '----selecting the shopname in the combobox for the following report numbers ------->
            Case 1, 2, 3, 4, 5, 6, 11, 12, 13, 14, 15, 16, 52
                tsl_selector.Text = "Store"
                cmb_rptselector.Visible = True
                ds_cmb_load = New DataSet
                s = "SELECT shopcode,shopname FROM storage where companycode='" & Module1.companycode & "' order by shopname"
                ds_cmb_load = ob.populate(s)
                cmb_rptselector.Items.Clear()
                cmb_rptselector.Items.Add("--------- All ---------")
                ob.combofill(ds_cmb_load, cmb_rptselector)
                cmb_rptselector.SelectedIndex = 0
            Case 17, 18, 19
                combo_party_selection()
            Case 20, 21, 22, 26, 27, 28
                combo_bill_selection()
            Case 23, 24, 25
                combo_item_selection()
                '--case for filling the comboboxes with the ledger names from accounting report ---->
            Case 35
                cmb_rptselector.Visible = True
                s = "select ledcode,name from ledger where companycode='" & Module1.companycode & "' order by name"
                dataset_for_combo = ob.populate(s)
                ob.combofill(dataset_for_combo, cmb_rptselector)
                cmb_rptselector.Text = dataset_for_combo.Tables(0).Rows(0).Item(1).ToString
            Case 39, 40, 41
                combo_sales_item_selection()
            Case 42, 43, 44
                combo_sales_bill_selection()
            Case 45, 46, 47
                combo_sales_party_selection()
        End Select
        CrystalReportViewer1.Width = Me.Width
        CrystalReportViewer1.Height = Me.Height - 34
    End Sub

    '---selected index change for the combobox cmb_rptselector -------------->
    Private Sub cmb_rptselector_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmb_rptselector.SelectedIndexChanged
        querystring = Nothing
        If cmb_rptselector.SelectedIndex > 0 Then
            Select Case Module1.report_no
                Case 1, 2, 3, 4, 5, 11, 12, 13, 14, 15, 16
                    querystring = " and opening_closing_stock.storecode  ='" & ds_cmb_load.Tables(0).Rows(cmb_rptselector.SelectedIndex - 1).Item(0) & "'"
                Case 17, 18, 19
                    querystring = " and ledger.name  ='" & cmb_rptselector.SelectedItem.ToString & "' "
                Case 20, 21, 22
                    querystring = " and docno='" & cmb_rptselector.SelectedItem.ToString & "' "
                Case 23, 24, 25
                    querystring = " and itemmst.Itemname ='" + cmb_rptselector.SelectedItem.ToString & "' "
                Case 39, 40, 41
                    querystring = " and itemname ='" & cmb_rptselector.SelectedItem & "'"
                Case 42, 43, 44
                    querystring = " and salesbillmain.trnno ='" & cmb_rptselector.SelectedItem & "'"
                Case 45, 46, 47
                    querystring = " and name ='" & cmb_rptselector.SelectedItem & "'"
            End Select
        End If

        '---selecting the shopcode from the combobox for report 6(category wise stock report)--------->
        If Module1.report_no = 6 Or Module1.report_no = 52 Then
            Dim query = From p As DataRow In ds_cmb_load.Tables(0) Where p(1) = cmb_rptselector.Text Select p(0)
            If query.Count > 0 Then : shop_code = query(0).ToString : Else : shop_code = Nothing : End If
            '---selecting the account code for the name selected in the combobox --->
        ElseIf Module1.report_no = 35 Then
            Dim query = From p As DataRow In dataset_for_combo.Tables(0) Where p(1) = cmb_rptselector.Text Select p(0)
            ledger_code = query(0).ToString
        End If

    End Sub

    '---setting only the visibility of the datetimepiker1 true and datetime piker2 as false--->
    Private Sub dtpicker_date_set()
        DateTimePicker2.Visible = False
        tsl_from_to.Text = "Date"
        DateTimePicker1.CustomFormat = "dd/MM/yyyy"
        date_string = " OF : " + Format(DateTimePicker1.Value.Date, "dd/MM/yyyy").ToString
    End Sub

    '---setting only the visibility of the datetimepiker1 true and datetime piker2 as false--->
    Private Sub dtpicker_month_set()
        DateTimePicker2.Visible = False
        tsl_from_to.Text = "Month"
        DateTimePicker1.CustomFormat = "MMMM"
        days = Date.DaysInMonth(DateTimePicker1.Value.Year, DateTimePicker1.Value.Month)
        date_string = " FOR THE MONTH OF : " + Format(DateTimePicker1.Value, "MMMMMMMMMMMMM").ToString.ToUpper + ", " + DateTimePicker1.Value.Year.ToString
        dtstr1 = DateTimePicker1.Value.Year.ToString & "-" & DateTimePicker1.Value.Month.ToString & "-01"
        dtstr2 = DateTimePicker1.Value.Year.ToString & "-" & DateTimePicker1.Value.Month.ToString & "-" & days.ToString
    End Sub

    '---setting the visibility of both the datetime picker as true --->
    Private Sub dtpicker_daterange_set()
        DateTimePicker2.Visible = True
        tsl_from_to.Text = "From                           To                           "
        DateTimePicker1.CustomFormat = "dd/MM/yyyy"
        date_string = " FROM : " + Format(DateTimePicker1.Value.Date, "dd/MM/yyyy").ToString + " TO : " + Format(DateTimePicker2.Value.Date, "dd/MM/yyyy").ToString
    End Sub

    Private Sub combo_sales_item_selection()
        tsl_selector.Text = "Item"
        cmb_rptselector.Visible = True
        ds_cmb_load = New DataSet
        s = "select distinct itemmst.itemcode,itemname from salesbilldetail join itemmst on salesbilldetail.companycode=itemmst.companycode and salesbilldetail.itemcode=itemmst.itemcode where salesbilldetail.companycode='" & Module1.companycode & "'  and yearcode='" & Module1.yearcode & "' order by itemname"
        ds_cmb_load = ob.populate(s)
        cmb_rptselector.Items.Clear()
        cmb_rptselector.Items.Add("--------- All ---------")
        ob.combofill(ds_cmb_load, cmb_rptselector)
        cmb_rptselector.SelectedIndex = 0
    End Sub

    Private Sub combo_sales_party_selection()
        tsl_selector.Text = "Party"
        cmb_rptselector.Visible = True
        ds_cmb_load = New DataSet
        s = "select distinct ledgercode,name from salesbillmain join ledger on salesbillmain.companycode=ledger.companycode and salesbillmain.ledgercode=ledcode where salesbillmain.companycode='" & Module1.companycode & "'  and yearcode='" & Module1.yearcode & "' order by name"
        ds_cmb_load = ob.populate(s)
        cmb_rptselector.Items.Clear()
        cmb_rptselector.Items.Add("--------- All ---------")
        ob.combofill(ds_cmb_load, cmb_rptselector)
        cmb_rptselector.SelectedIndex = 0
    End Sub

    Private Sub combo_sales_bill_selection()
        tsl_selector.Text = "Bill No"
        cmb_rptselector.Visible = True
        ds_cmb_load = New DataSet
        s = "select distinct trnno from salesbillmain where salesbillmain.companycode='" & Module1.companycode & "'  and yearcode='" & Module1.yearcode & "' order by trnno"
        ds_cmb_load = ob.populate(s)
        cmb_rptselector.Items.Clear()
        cmb_rptselector.Items.Add("--------- All ---------")
        If ds_cmb_load.Tables(0).Rows.Count > 0 Then
            For i = 0 To ds_cmb_load.Tables(0).Rows.Count - 1
                cmb_rptselector.Items.Add(ds_cmb_load.Tables(0).Rows(i).Item(0))
            Next
        End If
        cmb_rptselector.SelectedIndex = 0
    End Sub

    Private Sub combo_party_selection()
        tsl_selector.Text = "Party"
        cmb_rptselector.Visible = True
        ds_cmb_load = New DataSet
        Select Case Module1.report_no
            Case 17
                s = "SELECT distinct suppliercode,ledger.name FROM purchasemain INNER JOIN ledger ON purchasemain.companycode = ledger.companycode AND purchasemain.suppliercode = ledger.ledcode INNER JOIN companymst ON purchasemain.companycode = companymst.companycode where purchasemain.companycode='" & Module1.companycode & "' and purchasemain.yearcode='" & Module1.yearcode & "' and trndate='" & DateTimePicker1.Value.Date & "'order by ledger.name"
            Case 18
                s = "SELECT distinct suppliercode,ledger.name FROM purchasemain INNER JOIN ledger ON purchasemain.companycode = ledger.companycode AND purchasemain.suppliercode = ledger.ledcode INNER JOIN companymst ON purchasemain.companycode = companymst.companycode where purchasemain.companycode='" & Module1.companycode & "' and purchasemain.yearcode='" & Module1.yearcode & "' and month(trndate)='" & DateTimePicker1.Value.Month & "'and year(trndate)='" & DateTimePicker1.Value.Year & "'order by ledger.name"
            Case 19
                s = "SELECT distinct suppliercode,ledger.name FROM purchasemain INNER JOIN ledger ON purchasemain.companycode = ledger.companycode AND purchasemain.suppliercode = ledger.ledcode INNER JOIN companymst ON purchasemain.companycode = companymst.companycode where purchasemain.companycode='" & Module1.companycode & "' and purchasemain.yearcode='" & Module1.yearcode & "' and trndate>='" & DateTimePicker1.Value.Date & "'and trndate<='" & DateTimePicker2.Value.Date & "'order by ledger.name"
        End Select
        cmb_rptselector.Items.Clear()
        cmb_rptselector.Items.Add("--------- All ---------")
        ds_cmb_load = ob.populate(s)
        ob.combofill(ds_cmb_load, cmb_rptselector)
        cmb_rptselector.SelectedIndex = 0
    End Sub

    Private Sub combo_bill_selection()
        tsl_selector.Text = "Bill No"
        cmb_rptselector.Visible = True
        ds_cmb_load = New DataSet
        Select Case Module1.report_no
            Case 20, 26
                s = "SELECT docno from purchasemain where companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "' and trndate='" & DateTimePicker1.Value.Date & "' order by docno"
            Case 21, 27
                s = "SELECT docno from purchasemain where companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "' and month(trndate)='" & DateTimePicker1.Value.Month & "'and year(trndate)='" & DateTimePicker1.Value.Year & "' order by docno"
            Case 22, 28
                s = "SELECT docno from purchasemain where companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "' and trndate>='" & DateTimePicker1.Value.Date & "'and trndate<='" & DateTimePicker2.Value.Date & "'order by docno"
        End Select
        ds_cmb_load = ob.populate(s)
        cmb_rptselector.Items.Clear()
        cmb_rptselector.Items.Add("--------- All ---------")
        For i = 0 To ds_cmb_load.Tables(0).Rows.Count - 1
            cmb_rptselector.Items.Add(ds_cmb_load.Tables(0).Rows(i).Item(0))
        Next
        cmb_rptselector.SelectedIndex = 0
    End Sub

    Private Sub combo_item_selection()
        tsl_selector.Text = "Bill No"
        cmb_rptselector.Visible = True
        ds_cmb_load = New DataSet
        Select Case Module1.report_no
            Case 23
                s = "SELECT DISTINCT itemmst.itemname FROM purchasedetail INNER JOIN itemmst ON purchasedetail.itemcode = itemmst.itemcode AND purchasedetail.companycode = itemmst.companycode INNER JOIN purchasemain ON purchasedetail.companycode = purchasemain.companycode AND purchasedetail.yearcode = purchasemain.yearcode AND purchasedetail.trnno = purchasemain.trnno where purchasemain.companycode='" & Module1.companycode & "' and purchasemain.yearcode='" & Module1.yearcode & "'and trndate='" & DateTimePicker1.Value.Date & "' order by itemmst.itemname"
            Case 24
                s = "SELECT DISTINCT itemmst.itemname FROM purchasedetail INNER JOIN itemmst ON purchasedetail.itemcode = itemmst.itemcode AND purchasedetail.companycode = itemmst.companycode INNER JOIN purchasemain ON purchasedetail.companycode = purchasemain.companycode AND purchasedetail.yearcode = purchasemain.yearcode AND purchasedetail.trnno = purchasemain.trnno where purchasemain.companycode='" & Module1.companycode & "' and purchasemain.yearcode='" & Module1.yearcode & "'and month(trndate)='" & DateTimePicker1.Value.Month & "'and year(trndate)='" & DateTimePicker1.Value.Year & "' order by itemmst.itemname"
            Case 25
                s = "SELECT DISTINCT itemmst.itemname FROM purchasedetail INNER JOIN itemmst ON purchasedetail.itemcode = itemmst.itemcode AND purchasedetail.companycode = itemmst.companycode INNER JOIN purchasemain ON purchasedetail.companycode = purchasemain.companycode AND purchasedetail.yearcode = purchasemain.yearcode AND purchasedetail.trnno = purchasemain.trnno where purchasemain.companycode='" & Module1.companycode & "' and purchasemain.yearcode='" & Module1.yearcode & "'and trndate>='" & DateTimePicker1.Value.Date & "' and trndate<='" & DateTimePicker2.Value.Date & "' order by itemmst.itemname"
        End Select
        ds_cmb_load = ob.populate(s)
        cmb_rptselector.Items.Clear()
        cmb_rptselector.Items.Add("--------- All ---------")
        For i = 0 To ds_cmb_load.Tables(0).Rows.Count - 1
            cmb_rptselector.Items.Add(ds_cmb_load.Tables(0).Rows(i).Item(0))
        Next
        cmb_rptselector.SelectedIndex = 0
    End Sub


    Private Sub mini_printer_check()
        Try
            If Not Module1.comprinter = Nothing Then
                For Each prt In Printing.PrinterSettings.InstalledPrinters
                    If prt = Module1.comprinter Then
                        Rpt.PrintOptions.PrinterName = Module1.comprinter
                        Exit Sub
                    End If
                Next
            Else
                GoTo PRIN
            End If
        Catch ex As Exception
            GoTo PRIN
        End Try
PRIN:   MsgBox("Please Check Printer Name in Company Parameter", MsgBoxStyle.Information, "Printer")
    End Sub

    Private Sub printer_check()
        Try
            Dim pnt As New System.Drawing.Printing.PrinterSettings
            Rpt.PrintOptions.PrinterName = pnt.PrinterName
        Catch ex As Exception
            MsgBox("Please Check Default Printer in 'Start' --> 'Printer & Faxes'", MsgBoxStyle.Information, "Printer")
        End Try
    End Sub

    Private Sub stock_load()
        Dim dsrep As New ds_stock
        Module1.opencon()
        Dim adprep As New SqlDataAdapter(sql, Module1.con)
        adprep.Fill(dsrep.Tables(0))
        Module1.closecon()
        title_string = Nothing
        If cmb_rptselector.SelectedIndex = 0 Then
            title_string = "STOCKS FROM ALL SHOPS & GODOWN"
        ElseIf cmb_rptselector.SelectedIndex > 0 Then
            title_string = "STOCKS FROM: " + cmb_rptselector.SelectedItem.ToString.ToUpper
        End If
        For i = 0 To dsrep.Tables(0).Rows.Count - 1
            dsrep.Tables(0).Rows(i).Item("companyname") = Module1.companyname
            dsrep.Tables(0).Rows(i).Item("trndate1") = "STOCKS" + date_string
            dsrep.Tables(0).Rows(i).Item("title_string") = title_string
        Next

        Rpt = New rep_stock
        mini_printer_check()

        If dsrep.Tables(0).Rows.Count > 0 Then
            Rpt.SetDataSource(dsrep)
            CrystalReportViewer1.ReportSource = Rpt
            CrystalReportViewer1.Refresh()
        Else
            MsgBox("No Stock record for the selected date.", MsgBoxStyle.Information, "No Stock")
            CrystalReportViewer1.ReportSource = Nothing
            CrystalReportViewer1.Refresh()
        End If

    End Sub

    Private Sub stock_itemwise()
        Dim dsrep As New ds_stock_itemwise
        Module1.opencon()
        Dim adprep As New SqlDataAdapter(sql, Module1.con)
        adprep.Fill(dsrep.Tables(0))
        Module1.closecon()
        If cmb_rptselector.SelectedIndex = 0 Then
            title_string = "STOCKS FROM ALL SHOPS & GODOWN"
        ElseIf cmb_rptselector.SelectedIndex > 0 Then
            title_string = "STOCKS FROM: " + cmb_rptselector.SelectedItem.ToString.ToUpper
        End If
        For i = 0 To dsrep.Tables(0).Rows.Count - 1
            dsrep.Tables(0).Rows(i).Item("companyname") = Module1.companyname
            dsrep.Tables(0).Rows(i).Item("trndate1") = "STOCKS" + date_string
            dsrep.Tables(0).Rows(i).Item("title_string") = title_string
        Next
        Rpt = New rep_stock_itemwise
        mini_printer_check()
        If dsrep.Tables(0).Rows.Count > 0 Then
            Rpt.SetDataSource(dsrep)
            CrystalReportViewer1.ReportSource = Rpt
            CrystalReportViewer1.Refresh()
        Else
            MsgBox("No Stock record for the selected date.", MsgBoxStyle.Information, "No Stock")
            CrystalReportViewer1.ReportSource = Nothing
            CrystalReportViewer1.Refresh()
        End If
    End Sub

    Private Sub sales_all_load()
        Dim dsrep As New ds_sale
        Module1.opencon()
        Dim adprep As New SqlDataAdapter(sql, Module1.con)
        adprep.Fill(dsrep.Tables("sales"))
        Module1.closecon()
        Select Case Module1.report_no
            Case 39, 40, 41
                date_string = "ITEM WISE SALES REPORT " + date_string
                Rpt = New rep_sales
            Case 42, 43, 44
                date_string = "BILL WISE SALES REPORT " + date_string
                Rpt = New rep_sales_bill_wise
            Case 45, 46, 47
                date_string = "PARTY WISE SALES REPORT " + date_string
                Rpt = New rep_sales_party_wise
        End Select
        For i = 0 To dsrep.Tables("sales").Rows.Count - 1
            dsrep.Tables("sales").Rows(i).Item("companyname") = Module1.companyname
            dsrep.Tables("sales").Rows(i).Item("date_string") = date_string
        Next
        If dsrep.Tables("sales").Rows.Count > 0 Then
            Rpt.SetDataSource(dsrep)
            CrystalReportViewer1.ReportSource = Rpt
            CrystalReportViewer1.Refresh()
        Else
            MsgBox("No Sale record for the selected date.", MsgBoxStyle.Information, "No Sale")
            CrystalReportViewer1.ReportSource = Nothing
            CrystalReportViewer1.Refresh()
        End If
    End Sub

    Private Sub sale_load()
        Dim dsrep As New ds_sales_statement
        Module1.opencon()
        Dim adprep As New SqlDataAdapter(sql, Module1.con)
        adprep.Fill(dsrep.Tables(0))
        ds0 = ob.populate(sql1)
        ds1 = ob.populate(sql2)
        ds2 = ob.populate(sql3)
        ds3 = ob.populate(sql4)
        ds4 = ob.populate(sql5)
        Module1.closecon()
        For i = 0 To dsrep.Tables(0).Rows.Count - 1
            dsrep.Tables(0).Rows(i).Item("companyname") = Module1.companyname
            dsrep.Tables(0).Rows(i).Item("trndate1") = "SALES" + date_string
            If ds0.Tables(0).Rows.Count > 0 Then
                dsrep.Tables(0).Rows(i).Item("discount") = ds0.Tables(0).Rows(0).Item(0)
                dsrep.Tables(0).Rows(i).Item("netamount") = ds0.Tables(0).Rows(0).Item(1)
            Else
                dsrep.Tables(0).Rows(i).Item("discount") = 0
                dsrep.Tables(0).Rows(i).Item("netamount") = 0
            End If
            If ds1.Tables(0).Rows.Count > 0 Then
                dsrep.Tables(0).Rows(i).Item("cash_qnty") = ds1.Tables(0).Rows(0).Item(0)
                dsrep.Tables(0).Rows(i).Item("cash_amount") = ds1.Tables(0).Rows(0).Item(1)
            Else
                dsrep.Tables(0).Rows(i).Item("cash_qnty") = 0
                dsrep.Tables(0).Rows(i).Item("cash_amount") = 0
            End If
            If ds2.Tables(0).Rows.Count > 0 Then
                dsrep.Tables(0).Rows(i).Item("return_qnty") = ds2.Tables(0).Rows(0).Item(0)
                dsrep.Tables(0).Rows(i).Item("return_amount") = ds2.Tables(0).Rows(0).Item(1)
                dsrep.Tables(0).Rows(i).Item("net_cashsale_qty") = ds1.Tables(0).Rows(0).Item(0) - ds2.Tables(0).Rows(0).Item(0)
                dsrep.Tables(0).Rows(i).Item("net_cashsale_amt") = ds1.Tables(0).Rows(0).Item(1) - ds2.Tables(0).Rows(0).Item(1)
            Else
                dsrep.Tables(0).Rows(i).Item("return_qnty") = 0
                dsrep.Tables(0).Rows(i).Item("return_amount") = 0
            End If
            If ds3.Tables(0).Rows.Count > 0 Then
                dsrep.Tables(0).Rows(i).Item("credit_qnty") = ds3.Tables(0).Rows(0).Item(0)
                dsrep.Tables(0).Rows(i).Item("credit_amount") = ds3.Tables(0).Rows(0).Item(1)
            Else
                dsrep.Tables(0).Rows(i).Item("credit_qnty") = 0
                dsrep.Tables(0).Rows(i).Item("credit_amount") = 0
            End If
            If ds4.Tables(0).Rows.Count > 0 Then
                dsrep.Tables(0).Rows(i).Item("card_qnty") = ds4.Tables(0).Rows(0).Item(0)
                dsrep.Tables(0).Rows(i).Item("card_amount") = ds4.Tables(0).Rows(0).Item(1)
            Else
                dsrep.Tables(0).Rows(i).Item("card_qnty") = 0
                dsrep.Tables(0).Rows(i).Item("card_amount") = 0
            End If
        Next
        '---checking which report to open and when ----->
        If Module1.report_no = 38 Then
            Rpt = New rep_sales_ml_wise
        Else
            Rpt = New rep_sales_statement
            '---checking for the printer status ---->
            mini_printer_check()
        End If

        '---checking if record exists for that selected date --->
        If dsrep.Tables(0).Rows.Count > 0 Then
            Rpt.SetDataSource(dsrep)
            CrystalReportViewer1.ReportSource = Rpt
            CrystalReportViewer1.Refresh()
        Else
            MsgBox("No Sale record for the selected date.", MsgBoxStyle.Information, "No Sale")
            CrystalReportViewer1.ReportSource = Nothing
            CrystalReportViewer1.Refresh()
        End If

    End Sub

    Private Sub purchase_load()
        Dim dsrep As New ds_purchase
        Module1.opencon()
        Dim adprep As New SqlDataAdapter(sql, Module1.con)
        adprep.Fill(dsrep.Tables("dt_purchase_detail"))
        Dim adprep1 As New SqlDataAdapter(sql1, Module1.con)
        adprep1.Fill(dsrep.Tables("dt_purchase_main"))
        Dim adprep2 As New SqlDataAdapter(sql2, Module1.con)
        adprep2.Fill(dsrep.Tables("dt_purchase_tax"))
        Module1.closecon()
        If dsrep.Tables("dt_purchase_main").Rows.Count > 0 Then
            For i = 0 To dsrep.Tables("dt_purchase_main").Rows.Count - 1
                dsrep.Tables("dt_purchase_main").Rows(i).Item("date_string") = "PURCHASE BILLS" + date_string
                If dsrep.Tables("dt_purchase_tax").Rows.Count > 0 Then
                    Dim n As Integer = 0
                    For j = 0 To dsrep.Tables("dt_purchase_tax").Rows.Count - 1
                        If dsrep.Tables("dt_purchase_main").Rows(i).Item("billno") = dsrep.Tables("dt_purchase_tax").Rows(j).Item("billno") Then
                            dsrep.Tables("dt_purchase_main").Rows(i).Item(15 + (n * 3)) = dsrep.Tables("dt_purchase_tax").Rows(j).Item(0)
                            dsrep.Tables("dt_purchase_main").Rows(i).Item(16 + (n * 3)) = dsrep.Tables("dt_purchase_tax").Rows(j).Item(1)
                            dsrep.Tables("dt_purchase_main").Rows(i).Item(17 + (n * 3)) = dsrep.Tables("dt_purchase_tax").Rows(j).Item(2)
                            n = n + 1
                        Else
                            n = 0
                        End If
                    Next
                End If
            Next
        End If
        Rpt = New rep_purchase
        If dsrep.Tables(0).Rows.Count > 0 Then
            Rpt.SetDataSource(dsrep)
            CrystalReportViewer1.ReportSource = Rpt
            CrystalReportViewer1.Refresh()
        Else
            MsgBox("No Purchase record for the selected date.", MsgBoxStyle.Information, "No Purchase")
            CrystalReportViewer1.ReportSource = Nothing
            CrystalReportViewer1.Refresh()
        End If
    End Sub

    Private Sub purchase_billsummary_load()
        Dim dsrep As New ds_purchase
        Module1.opencon()
        Dim adprep As New SqlDataAdapter(sql, Module1.con)
        adprep.Fill(dsrep.Tables("dt_purchase_main"))
        Module1.closecon()
        Rpt = New rep_purchase_summary
        If dsrep.Tables("dt_purchase_main").Rows.Count > 0 Then
            For i = 0 To dsrep.Tables("dt_purchase_main").Rows.Count - 1
                dsrep.Tables("dt_purchase_main").Rows(i).Item("companyname") = Module1.companyname
                dsrep.Tables("dt_purchase_main").Rows(i).Item("date_string") = "PURCHASE BILLS" + date_string
            Next
            Rpt.SetDataSource(dsrep.Tables("dt_purchase_main"))
            CrystalReportViewer1.ReportSource = Rpt
            CrystalReportViewer1.Refresh()
        Else
            MsgBox("No Purchase record for the selected date.", MsgBoxStyle.Information, "No Purchase")
            CrystalReportViewer1.ReportSource = Nothing
            CrystalReportViewer1.Refresh()
        End If
    End Sub

    Private Sub purchase_load_itemwise()
        Dim dsrep As New ds_purchase
        Module1.opencon()
        Dim adprep As New SqlDataAdapter(sql, Module1.con)
        adprep.Fill(dsrep.Tables("dt_purchase_detail"))
        Module1.closecon()
        Rpt = New rep_purchase_itemwise
        Try
            Rpt.PrintOptions.PrinterName.DefaultIfEmpty()
        Catch ex As Exception
            MsgBox("Please Check Default Printer in 'Start' --> 'Printer & Faxes'", MsgBoxStyle.Information, "Printer")
        End Try
        If dsrep.Tables("dt_purchase_detail").Rows.Count > 0 Then
            For i = 0 To dsrep.Tables("dt_purchase_detail").Rows.Count - 1
                dsrep.Tables("dt_purchase_detail").Rows(i).Item("date_string") = "PURCHASE BILLS" + date_string
            Next
            Rpt.SetDataSource(dsrep.Tables("dt_purchase_detail"))
            CrystalReportViewer1.ReportSource = Rpt
            CrystalReportViewer1.Refresh()
        Else
            MsgBox("No Purchase record for the selected date.", MsgBoxStyle.Information, "No Purchase")
            CrystalReportViewer1.ReportSource = Nothing
            CrystalReportViewer1.Refresh()
        End If
    End Sub


    Private Sub breakage_load()
        Dim dsrep As New ds_breakage
        Module1.opencon()
        Dim adprep As New SqlDataAdapter(sql, Module1.con)
        adprep.Fill(dsrep.Tables(0))
        Module1.closecon()
        Rpt = New rep_breakage
        If dsrep.Tables(0).Rows.Count > 0 Then
            For i = 0 To dsrep.Tables(0).Rows.Count - 1
                dsrep.Tables(0).Rows(i).Item("companyname") = Module1.companyname
                dsrep.Tables(0).Rows(i).Item("date_string") = "BREAKAGE REPORT " + date_string
            Next
            Rpt.SetDataSource(dsrep)
            CrystalReportViewer1.ReportSource = Rpt
            CrystalReportViewer1.Refresh()
        Else
            MsgBox("No Breakage record for the selected date.", MsgBoxStyle.Information, "No Stock")
            CrystalReportViewer1.ReportSource = Nothing
            CrystalReportViewer1.Refresh()
        End If

    End Sub


    Private Sub stock_value_load()
        Dim dsrep As New ds_stock
        Module1.opencon()
        Dim adprep As New SqlDataAdapter(sql, Module1.con)
        adprep.Fill(dsrep.Tables(0))
        Module1.closecon()
        If Module1.report_no = 53 Then
            date_string = "STOCK REPORT - VALUE WISE DETAIL " + date_string
            Rpt = New rep_stock_val_det
        ElseIf Module1.report_no = 54 Then
            date_string = "STOCK REPORT - VALUE WISE CLOSING " + date_string
            Rpt = New rep_stock_val_closing
        End If
        If dsrep.Tables(0).Rows.Count > 0 Then
            For i = 0 To dsrep.Tables(0).Rows.Count - 1
                dsrep.Tables(0).Rows(i).Item("companyname") = Module1.companyname
                dsrep.Tables(0).Rows(i).Item("date_string") = date_string
            Next
            Rpt.SetDataSource(dsrep)
            CrystalReportViewer1.ReportSource = Rpt
            CrystalReportViewer1.Refresh()
        Else
            MsgBox("No Stock record for the selected date.", MsgBoxStyle.Information, "No Stock")
            CrystalReportViewer1.ReportSource = Nothing
            CrystalReportViewer1.Refresh()
        End If
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        If ob.date_check(DateTimePicker1.Value.Date) = False Then
            Exit Sub
        End If
        If ob.date_check(DateTimePicker2.Value.Date) = False Then
            Exit Sub
        End If
        frm_load()
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Select Case Module1.report_no
            Case 3, 6, 9, 13, 16
                mini_printer_check()
            Case Else
                printer_check()
        End Select
        Rpt.PrintToPrinter(Integer.Parse(tstb_copies.Text), False, 0, 0)
    End Sub

End Class