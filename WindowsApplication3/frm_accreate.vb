Public Class frm_accreate

    Dim s As String
    Dim s1 As String
    Dim s2 As String

    Dim acgroupcode As String
    Dim acgroupname As String
    Dim accntname As Boolean

    Dim ob As New Class1
    Dim ds As New DataSet

    Private Sub accreate_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '---disabeling the for1 when this form is being opened ---->
        frm_MainForm.Enabled = False
        '---initialisation and by default selection --->
        TextBox1.Text = Module1.account_name
        '-----populating a dataset--------------------------------->
        s = "select acgroupcode,acgroupname from acgroupmst"
        ds = ob.populate(s)
        '---filling the combobox with the acount_group names --->
        ob.combofill(ds, ComboBox1)
        ComboBox1.Text = ds.Tables(0).Rows(0).Item(1).ToString
        '---section for populating the combobox with the name during the edit part --->
        If Module1.flag = 2 Then
            s = "select acgroupname from acgroupmst where acgroupcode in(select acgroupcode from acountname where accode='" & Module1.account_code & "')"
            acgroupname = ob.executereader(s)
            ComboBox1.Text = acgroupname
        End If
        '---keeping the text box1 selected as default --->
        TextBox1.Select()
    End Sub
    Private Sub accreate_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        '---enabeling the form1 when this form is being closed----->
        frm_MainForm.Enabled = True
    End Sub
    '---conbobox1 key down event --->
    Private Sub ComboBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox1.KeyDown
        If e.KeyData = Keys.Enter Then
            Button1.Select()
        End If
    End Sub
    '---selecting the account group code from the name present in the combo box --->
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim query = From p As DataRow In ds.Tables(0) Where p(1) = ComboBox1.Text Select p(0)
        If query.Count > 0 Then
            acgroupcode = query(0).ToString
        End If
    End Sub
    '---save button click event --->
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        '---checking if that accountname is already present in the table or not ---->
        'accntcheck()
        '---if that name is present then the sub is exited --->
        'If accntname = True Then
        'TextBox1.Select()
        'Exit Sub
        'End If
        '----section for adding to the acountname ----------------------->
        If Module1.flag = 1 Then
            Dim code As String
            s = "select top 1 accode from acountname where accode like '" & TextBox1.Text.ToUpper.Chars(0) & "%'order by accode desc"
            code = ob.getcode(s, TextBox1.Text)
            s = "insert into acountname (accode,acname,acgroupcode) values ('" & code & "','" & TextBox1.Text.ToUpper & "','" & acgroupcode & "') "
            '---section for updating the acountname on edit ---->
        Else
            s = "update acountname set acname='" & TextBox1.Text.ToUpper & "',acgroupcode='" & acgroupcode & "' where accode='" & Module1.account_code & "'"
        End If
        '----section for running the code in sql -->
        ob.insert(s)
        '---refreshing the main form --->
        frm_MainForm.mainformload()
        'If Module1.flag = 0 Then
        'TextBox1.Select()
        'Exit Sub
        '---refreshing the form after adding and editing -->
        If Module1.flag = 1 Then
            TextBox1.Text = Nothing
            TextBox1.Select()
        ElseIf Module1.flag = 2 Then
            Me.Close()
        End If
    End Sub
    '--button event for the cancel button --->
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
    '---text box1 key down event for selecting the next control when the user presses enter->
    Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyData = Keys.Enter And Not TextBox1.Text = "" Then
            'accntcheck()
            'If accntname = True Then
            'TextBox1.Select()
            'Exit Sub
            'Else
            ComboBox1.Select()
        End If
        'End If
    End Sub

    '---function for checking if the acount name that the user wishes to add ot update is present or not --->
    '    Private Sub accntcheck()
    '        Dim s As String
    '        Dim ob As New Class1
    '        If Module1.flag = 1 Then
    '            s = "select acname from acountname where acname='" & TextBox1.Text & "'"
    '            s = ob.executereader(s)
    '            If s = Nothing Then
    '                accntname = False
    '            Else
    '                GoTo msg
    '            End If
    '        ElseIf Module1.flag = 2 Then
    '            If Module1.col1 <> TextBox1.Text Then
    '                s = "select acname from acountname where acname='" & TextBox1.Text & "'"
    '                s = ob.executereader(s)
    '                If s = Nothing Then
    '                    accntname = False
    '                Else
    'msg:                MsgBox("Account Name already present.", MsgBoxStyle.Information, "Account Name")
    '                    accntname = True
    '                    Exit Sub
    '                End If
    '            Else
    '            End If
    '        End If
    '    End Sub

End Class