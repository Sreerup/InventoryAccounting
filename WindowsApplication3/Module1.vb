Imports System.Data.SqlClient


Module Module1
    '---connection string------------------------------------------------>
    Public con As New SqlConnection
    '-------------------------------------------------------------------->
    Public server As String
    Public server_id As String
    Public server_password As String
    Public companycode As String
    '--asssigning the objects of the forms-------------------------------->
    Public frm As New Form
    Public frm1 As New frm_MainForm
    Public countersales As New frm_countersales
    Public breakage As New frm_breakageentry
    '----------variable description for the master forms------------------->
    Public shopcode As String
    Public shopname As String
    Public count As Integer
    Public companyname As String
    Public groupcode As String
    Public col1 As String
    Public col2 As String
    Public col3 As String
    Public ratename As String
    Public ratecode As String
    Public salesrateload As Integer
    Public flag As Integer
    Public flag1 As Integer
    Public usercode As String
    Public username As String
    Public accesslevel As String
    Public yearcode As String
    Public yearrange As String
    '-------variable description for company add edit------------------------------>
    Public comname As String
    Public comcode As String
    '----------------variable common to both ledger and company-------------------->
    Public address1 As String
    Public address2 As String
    Public city As String
    Public district As String
    Public state As String
    Public pin As String
    Public phone As String
    Public email As String
    Public www As String
    Public fax As String
    Public lstno As String
    Public cstno As String
    Public panno As String
    Public vatno As String
    Public stno As String
    '------------------------------variable description for ledger--------------->
    Public ledgercode As String
    Public ledgername As String
    Public acountname As String
    Public debit As String
    Public credit As String
    Public contactperson As String
    Public comemail As String
    Public area As String
    Public acountcode As String
    Public transaction As String
    Public uadmin As Boolean
    Public uacc As Boolean
    Public pos As Boolean
    '------variable description for counter sales ------------------------------>
    Public dataset As New DataSet
    Public counter As Integer
    Public deleted_itemcode(0) As String
    Public itemcode_array(0) As String
    Public searchflag As Integer
    Public position As Integer
    Public arraycount As Integer
    Public salestype As String
    Public amount As Double
    Public discount As String
    Public recv As String
    Public due As String
    Public row As Integer
    Public voucher_number As String
    Public surcharge_amount As String
    '------------------------------- Variables for reports ----------------------->
    Public report_no As Integer
    '--------------------- company info ------------------------------------------>
    Public comaddress1 As String
    Public comaddress2 As String
    Public comcity As String
    Public comdistrict As String
    Public comstate As String
    Public compin As String
    Public comphone As String
    '----------- varible holding the value of company parameters for counter sale & purchase ------------>
    Public combillno As String
    Public comsaleacc As String
    Public combankacc As String
    Public combreakacc As String
    Public comsaleretacc As String
    Public comlimit As String
    Public comsaveprint As String
    Public comprinter As String
    Public compuracc As String
    Public comcashacc As String
    Public comdiscacc As String
    Public com_trade_discount As String
    Public comdefstore As String
    Public comdefrate As String
    Public comexpenceacc As String
    Public comrndacc As String
    Public comsaleref As String
    Public compos As String
    Public comstdate As Date
    Public comenddate As Date
    Public combillfooter As String
    Public combillfooter2 As String
    Public combillfooter3 As String
    Public combillfooter4 As String
    Public back_up_path As String
    Public comprintmode As String
    Public com_surcharge_acc As String
    Public com_surcharge_percent As String
    '--------------variable description for breakage entry-------------------------->
    Public storecode1 As String
    Public storecode2 As String
    Public storename1 As String
    Public storename2 As String
    Public breakage_bill_no As String
    Public breakage_tp_pass_no As String
    '---varibale declared for accepting the transaction date--->
    Public transaction_date As DateTime
    '---variable description for receipt------------------------------------------->
    Public vch_head_account As String
    Public vch_client_account As String
    Public vchno As String
    Public vch_cheque_no As String
    Public vch_date As DateTime
    Public vch_due As Double
    Public vch_amt_paid As Double
    Public vch_discount As Double
    Public vch_net_due As Double
    Public vch_narration As String
    Public vch_head_name As String
    Public vch_client_name As String
    '---variable describption for breakage--->
    Public breakage_trn As String
    Public breakage_trndate As DateTime
    Public breakage_store_name As String
    Public breakage_party_name As String
    Public breakage_narration As String
    Public breakage_row As Integer
    '----variable description for acounttable/acoutname table/acount sub group----->
    Public account_code As String
    Public account_name As String
    '--varibale description for sales rate----------------------------------------->
    Public sales_rate_code As String
    Public sales_rate_name As String



    Private Sub createconnectionstring(ByVal catlog As String)
        closecon()
        'con = New SqlConnection("Data Source=" & server & ";Initial Catalog=" & catlog & ";Integrated Security=True")
        'con = New SqlConnection("Data Source=" & server & ";Initial Catalog=" & catlog & ";User ID=sa;password=1")
        con = New SqlConnection("Data Source=" & server & ";Initial Catalog=" & catlog & ";Trusted_Connection=True")
        con.Open()
    End Sub

    Public Sub opencon()
        createconnectionstring("barmanager")
    End Sub

    Public Sub openmaster()
        createconnectionstring("master")
    End Sub

    Public Sub closecon()
        con.Close()
        con.Dispose()
    End Sub

End Module
