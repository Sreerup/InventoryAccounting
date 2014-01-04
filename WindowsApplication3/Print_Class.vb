Public Class Print_Class

    Public Function space_cal(ByVal str_num As String, ByVal count_space As Integer) As String
        Dim str_space As String
        str_space = ""
        count_space = count_space - Len(str_num)
        For i = 0 To count_space - 1
            str_space = str_space + " "
        Next
        Return str_space
    End Function

    Public Function centre_alignment(ByVal str_num As String, ByVal count_space As Integer) As String
        Dim str_space As String
        str_space = ""
        count_space = (count_space - Len(str_num)) / 2
        For i = 0 To count_space - 1
            str_space = str_space + " "
        Next
        Return str_space
    End Function

    Public Sub printer_setup(ByVal pintdoc As Printing.PrintDocument)
        Dim MyPrintDialog As New PrintDialog()
        MyPrintDialog.AllowCurrentPage = False
        MyPrintDialog.AllowPrintToFile = False
        MyPrintDialog.AllowSelection = False
        MyPrintDialog.AllowSomePages = False
        MyPrintDialog.PrintToFile = False
        MyPrintDialog.ShowHelp = False
        MyPrintDialog.ShowNetwork = False
        'If MyPrintDialog.ShowDialog() <> DialogResult.OK Then
        '    Return False
        'End If
        pintdoc.PrinterSettings = MyPrintDialog.PrinterSettings
        pintdoc.DefaultPageSettings = MyPrintDialog.PrinterSettings.DefaultPageSettings
        pintdoc.DefaultPageSettings.Margins = New Printing.Margins(80, 50, 50, 50)
    End Sub





End Class
