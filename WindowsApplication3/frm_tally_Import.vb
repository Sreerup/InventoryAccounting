Imports System.Data.SqlClient
Imports ABNExportTool


Public Class frm_tally_Import

    '---initialising the instance variables--->
    Dim s As String
    Dim ob1 As New Class1
    Dim ds As New DataSet


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        '-----creating the view for the salesdata----->
        s = "IF  EXISTS (SELECT * FROM sys.views WHERE  name='vwsalesdata') drop view vwsalesdata"
        ob1.insert(s)
        s = "create view vwSalesData  as select date,saletype as SalesAcount,client as AcountHead,trnno,itemname,itemqty,itemrate,itemamount,tottaxamt as discount,totnetamt  from vw_tally_data where date>='" & DateTimePicker1.Value.Date & "' and date<='" & DateTimePicker2.Value.Date & "' and companycode='" & Module1.companycode & "' and yearcode='" & Module1.yearcode & "'"
        ob1.insert(s)
        s = "IF  EXISTS (SELECT * FROM sys.views WHERE  name='vwgrndata') drop view vwgrndata"
        ob1.insert(s)
        s = "create view vwgrndata as select companycode,date,no,supplier as suppcode,chno,chdate,tpassno,itempurchased as itemname,batchno,box,loose,itemqty,itemrate,itemamount from vw_tally_purchase where date>='" & DateTimePicker1.Value.Date & "' and date<='" & DateTimePicker2.Value.Date & "' and yearcode='" & Module1.yearcode & "' and companycode='" & Module1.companycode & "'"
        ob1.insert(s)

        '---creating the xml for the sales------------->
        If Module1.count = 100 Then
            Dim ob As New ABNExportTool.SalesExport
            ob.Show()
            '---ceratintg the xml for the purchase----->
        ElseIf Module1.count = 101 Then
            Dim ob As New ABNExportTool.PurchaseExport
            ob.Show()
        End If

    End Sub


End Class