
Imports System.Data.SqlClient


Public Class Class1
    '----generating codes for the items added-------------------->
    Public Function generatecode(ByVal s As String) As String
        Dim ch As Char
        ch = "0"
        Dim i As Integer
        For i = 1 To s.Length()
            If Not s.Chars(i).Equals(ch) Then
                Exit For
            End If
        Next
        Dim str As String
        str = s.Substring(i)
        str = Convert.ToInt32(str) + 1
        s = s.Substring(0, i) + str
        If s.Length() > 6 Then
            s = s.Substring(0, i - 1) + str
        End If
        Return s

    End Function
    '----generating codes for a new item addition---------------->
    Public Function generatenewcode(ByVal s As String) As String
        Dim str As String
        str = s.Chars(0).ToString.ToUpper
        str = str + "00001"
        Return str
    End Function
    '---this function is used for inserting/updating/deleting in the database------>
    Public Function insert(ByVal s2 As String) As Integer
        Module1.opencon()
        Dim cmd1 As New SqlCommand(s2, Module1.con)
        'Try
        cmd1.ExecuteNonQuery()
        'Catch ex As SqlException
        'MsgBox("Request can not be process.", MsgBoxStyle.Exclamation, "")
        'Module1.flag = 0
        'End Try
        Module1.closecon()

        Return 0
    End Function
    '-------------function used to check if the code has been used or not and create a new code on the decision
    Public Function getcode(ByVal s1 As String, ByVal s2 As String) As String
        Dim ds As New DataSet
        Module1.opencon()
        Dim da As New SqlDataAdapter(s1, Module1.con)
        da.Fill(ds)
        Module1.closecon()
        Dim str As String
        If ds.Tables(0).Rows.Count > 0 Then
            str = generatecode(ds.Tables(0).Rows(0).Item(0))
        Else
            str = generatenewcode(s2)
        End If
        Return str
    End Function
    '-------------------------function for populating dataset---------------------->
    Public Function populate(ByVal s As String) As DataSet
        Dim ds As New DataSet
        Module1.opencon()
        Dim da As New SqlDataAdapter(s, Module1.con)
        da.Fill(ds)
        Module1.closecon()
        Return ds
    End Function
    '-------------------------function for populating dataset---------------------->
    Public Function populate2(ByVal s As String) As DataTable
        Dim dt As New DataTable
        Module1.opencon()
        Dim da As New SqlDataAdapter(s, Module1.con)
        da.Fill(dt)
        Module1.closecon()
        Return dt
    End Function
    '--------------------function to check if the data is already present or not--->
    Public Function checkpresent(ByVal s As String) As Boolean
        Dim f As Boolean
        Dim ds As New DataSet
        Module1.opencon()
        Dim da As New SqlDataAdapter(s, Module1.con)
        da.Fill(ds)
        Module1.closecon()
        If ds.Tables(0).Rows.Count > 0 Then
            f = True
        Else
            f = False
        End If
        Return f
    End Function
    '---public sub for execute reader------------>
    Public Function executereader(ByVal s As String) As String
        Dim count As String
        count = Nothing
        Module1.opencon()
        Dim cmd As New SqlCommand(s, Module1.con)
        Dim dr As SqlDataReader
        dr = cmd.ExecuteReader
        While dr.Read()
            count = dr(0).ToString
        End While
        Module1.closecon()
        Return count
    End Function
    '---command for checking if the database is present or not--->
    Public Function check_database_present(ByVal s As String) As String
        Dim count As String
        count = Nothing
        Module1.openmaster()
        Dim cmd As New SqlCommand(s, Module1.con)
        Dim dr As SqlDataReader
        dr = cmd.ExecuteReader
        While dr.Read()
            count = dr(0).ToString
        End While
        Module1.closecon()
        Return count
    End Function
    '---command for fillin the comboboxes with the items in the dataset--->
    Public Sub combofill(ByVal datas As DataSet, ByVal combo As ComboBox)
        Dim datarowcount As Integer
        datarowcount = datas.Tables(0).Rows.Count
        If datarowcount > 0 Then
            For i = 0 To datarowcount - 1
                combo.Items.Add(datas.Tables(0).Rows(i).Item(1).ToString)
            Next
        End If
    End Sub


    Public Sub combo_fill_by_code(ByVal datas As DataSet, ByVal combo As ComboBox, ByVal check As String)
        For i = 0 To datas.Tables(0).Rows.Count - 1
            If check = datas.Tables(0).Rows(i).Item(0) Then
                combo.Items.Add(datas.Tables(0).Rows(i).Item(1))
                combo.SelectedItem = datas.Tables(0).Rows(i).Item(1).ToString
            End If
        Next
    End Sub

    Public Function date_check(ByVal dt As Date) As Boolean
        Dim fl As Boolean
        Try
            If dt >= Module1.comstdate And dt <= Module1.comenddate Then
                fl = True
            Else
                fl = False
                MsgBox("Date out of financial year.", MsgBoxStyle.Exclamation, "Date")
            End If
        Catch ex As SqlException
            fl = False
        End Try
        Return fl
    End Function
End Class
