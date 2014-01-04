Imports System.IO
Imports System.String
Public Class frm_itemmstadd

    Dim ds As New DataSet
    Dim ds1 As New DataSet
    Dim ds2 As New DataSet
    Dim ds3 As New DataSet
    Dim ds4 As New DataSet
    Dim ds5 As New DataSet
    Dim dataset As New DataSet
    Dim txt As String
    Dim itemname As Boolean
    Dim bar As Boolean

    Dim s As String
    Dim ob As New Class1



    Private Sub itemmstadd_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        s = "select categorycode,categoryname from categorymst where companycode='" & Module1.companycode & "'"
        ds = ob.populate(s)
        '-------populating the combobox1 with the category names
        For i = 0 To ds.Tables(0).Rows.Count - 1
            ComboBox1.Items.Add(ds.Tables(0).Rows(i).Item(1).ToString)
        Next
        s = "select groupcode,groupname from groupmst where companycode='" & Module1.companycode & "'"
        ds1 = ob.populate(s)
        '-------populating the combobox2 with the category names
        For i = 0 To ds1.Tables(0).Rows.Count - 1
            ComboBox2.Items.Add(ds1.Tables(0).Rows(i).Item(1).ToString)
        Next
        s = "select ml,packing from itemgroupml"
        ds2 = ob.populate(s)
        '-------populating the combobox3 with the category names
        For i = 0 To ds2.Tables(0).Rows.Count - 1
            ComboBox3.Items.Add(ds2.Tables(0).Rows(i).Item(0).ToString)
        Next
        s = "select kflcode,kflname from kflmst where companycode='" & Module1.companycode & "'"
        ds3 = ob.populate(s)
        '-------populating the combobox4 with the category names
        For i = 0 To ds3.Tables(0).Rows.Count - 1
            ComboBox4.Items.Add(ds3.Tables(0).Rows(i).Item(1).ToString)
        Next
        s = "select strengthname from strength  order by strengthname"
        ds4 = ob.populate(s)
        '-------populating the combobox5 with the category names
        For i = 0 To ds4.Tables(0).Rows.Count - 1
            ComboBox5.Items.Add(ds4.Tables(0).Rows(i).Item(0).ToString)
        Next
        frmitemload()
    End Sub

    Private Sub frmitemload()
        itemname = False
        TextBox1.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""

        TextBox1.Select()

        If ds4.Tables(0).Rows.Count <= 0 Or ds.Tables(0).Rows.Count <= 0 Or ds1.Tables(0).Rows.Count <= 0 Or ds2.Tables(0).Rows.Count <= 0 Or ds3.Tables(0).Rows.Count <= 0 Then
            MsgBox("Master missing, Please enter records in master.", MsgBoxStyle.Information, "Master Missing")
            Module1.flag = 0
            Exit Sub
        End If

        If Module1.flag = 1 Then
            ComboBox1.SelectedIndex = 0
            ComboBox2.SelectedIndex = 0
            ComboBox3.SelectedIndex = 0
            ComboBox4.SelectedIndex = 0
            ComboBox5.SelectedIndex = 0

            'ComboBox5.Text = ds4.Tables(0).Rows(0).Item(0).ToString
            'ComboBox1.Text = ds.Tables(0).Rows(0).Item(1).ToString
            'ComboBox2.Text = ds1.Tables(0).Rows(0).Item(1).ToString
            'ComboBox3.Text = ds2.Tables(0).Rows(0).Item(0).ToString
            'ComboBox4.Text = ds3.Tables(0).Rows(0).Item(1).ToString
            TextBox3.Text = 1
        End If
        If Module1.flag = 2 Then
            TextBox1.Text = itemmstmodule.name
            TextBox3.Text = itemmstmodule.salesrate
            TextBox4.Text = itemmstmodule.barcode
            TextBox5.Text = itemmstmodule.purchaserate
            ComboBox2.Text = itemmstmodule.groupname
            ComboBox1.Text = itemmstmodule.categoryname
            ComboBox4.Text = itemmstmodule.kflname
            ComboBox3.Text = itemmstmodule.ml
            TextBox2.Text = itemmstmodule.packing
            ComboBox5.Text = itemmstmodule.strengthname
        End If

    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click


        If TextBox1.Text = "" Or TextBox1.Text = "0" Or TextBox2.Text = "" Or TextBox2.Text = "0" Or TextBox3.Text = "" Or TextBox3.Text = "0" Or TextBox4.Text = "" Or TextBox4.Text = "0" Or ComboBox1.Text = "" Or ComboBox1.Text = "0" Or ComboBox2.Text = "" Or ComboBox2.Text = "0" Or ComboBox3.Text = "" Or ComboBox3.Text = "0" Or ComboBox4.Text = "" Or ComboBox4.Text = "0" Or ComboBox5.Text = "" Or ComboBox5.Text = "0" Then
            MsgBox("Please fill all the field")
            Exit Sub
        End If

        Dim S As String
        Dim itmcode As String
        Dim ob As New Class1
        S = Nothing
        itemname = False
        itemcheck()
        If itemname = True Then
            TextBox1.Select()
            Exit Sub
        End If
        bar = False
        barheck()
        If bar = True Then
            TextBox4.Select()
            Exit Sub
        End If


        If Module1.flag = 1 Then
            S = "select top 1 itemcode from itemMst where itemcode like '" & TextBox1.Text.Chars(0) & "%'order by itemcode desc "
            itmcode = ob.getcode(S, TextBox1.Text)
            S = "insert into itemmst (companycode,categoryCode,groupcode,itemcode,itemname,ml,packing,kflcode,strengthname,salesrate,barcode,purchaserate) values ('" & Module1.companycode & "','" & itemmstmodule.categorycode & "','" & itemmstmodule.groupcode & "','" & itmcode & "','" & TextBox1.Text.ToUpper & "','" & itemmstmodule.ml & "','" & TextBox2.Text & "','" & itemmstmodule.kflcode & "','" & ComboBox5.SelectedItem & "','" & TextBox3.Text & "','" & TextBox4.Text.ToUpper & "','" & TextBox5.Text & "') "
            ob.insert(S)
        ElseIf Module1.flag = 2 Then
            S = "update itemmst set itemname='" & TextBox1.Text.ToUpper & "', packing='" & TextBox2.Text & "', ml='" & itemmstmodule.ml & "',categorycode='" & itemmstmodule.categorycode & "',kflcode='" & itemmstmodule.kflcode & "',strengthname='" & itemmstmodule.strengthname & "',salesrate='" & TextBox3.Text & "',barcode='" & TextBox4.Text & "',purchaserate='" & TextBox5.Text & "',groupcode='" & itemmstmodule.groupcode & "' where itemcode ='" & itemmstmodule.itemcode & "'"
            ob.insert(S)
            'S = "update itemratemst set salesrate=" & TextBox3.Text & " where itemcode='" & itemmstmodule.itemcode & "' and companycode='" & Module1.companycode & "'"
            'ob.insert(S)
        End If



        If Module1.flag = 0 Then
            TextBox1.Select()
            Exit Sub
        ElseIf Module1.flag = 1 Then
            frmitemload()
        ElseIf Module1.flag = 2 Then
            Me.Close()
            frm_MainForm.Enabled = True
        End If
        frm_MainForm.mainformload()

    End Sub

    Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyData = Keys.Enter And Not TextBox1.Text = "" Then
            itemname = False
            itemcheck()
            If itemname = True Then
                TextBox1.Select()
            Else
                TextBox4.Select()
            End If
        End If
    End Sub

    Private Sub itemcheck()
        Dim s As String
        s = Nothing

        Dim ob As New Class1

        If Module1.flag = 1 Then
            s = "select itemname from itemmst where companycode='" & Module1.companycode & "' and itemname='" & TextBox1.Text & "'"
            s = ob.executereader(s)
            If s = Nothing Then
                itemname = False
            Else
                GoTo msg
            End If
        ElseIf Module1.flag = 2 Then
            If itemmstmodule.name <> TextBox1.Text Then
                If Not itemmstmodule.name.ToUpper = TextBox1.Text.ToUpper Then
                    s = "select itemname from itemmst where companycode='" & Module1.companycode & "' and itemname='" & TextBox1.Text & "'"
                    s = ob.executereader(s)
                End If
                If s = Nothing Then
                    itemname = False
                Else
msg:                MsgBox("Item already present.", MsgBoxStyle.Information, "Item")
                    itemname = True
                    Exit Sub
                End If
            Else
            End If
        End If
    End Sub

    Private Sub TextBox4_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox4.KeyDown
        If e.KeyData = Keys.Enter And Not TextBox4.Text = "" Then
            barheck()
            If bar = True Then
                TextBox4.Select()
            Else
                ComboBox1.Select()
            End If
        End If
    End Sub

    Private Sub barheck()
        Dim s As String
        Dim ob As New Class1
        If Module1.flag = 1 Then
            s = "select barcode from itemmst where companycode='" & Module1.companycode & "' and barcode='" & TextBox4.Text & "'"
            s = ob.executereader(s)
            If s = Nothing Then
                bar = False
            Else
                GoTo msg1
            End If
        ElseIf Module1.flag = 2 Then
            If itemmstmodule.barcode <> TextBox4.Text Then
                s = "select barcode from itemmst where companycode='" & Module1.companycode & "' and barcode='" & TextBox4.Text & "'"
                s = ob.executereader(s)
                If s = Nothing Then
                    bar = False
                Else
msg1:               MsgBox("Barcode already present.", MsgBoxStyle.Information, "Barcode")
                    bar = True
                    Exit Sub
                End If
            Else
            End If
        End If

    End Sub

    Private Sub TextBox5_KeyDown1(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox5.KeyDown
        If e.KeyData = Keys.Enter And Not TextBox5.Text = "" Then
            ComboBox1.Select()
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
        frm_MainForm.Enabled = True
    End Sub

    Private Sub TextBox5_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        'If e.KeyData = Keys.Enter And Not TextBox4.Text = Nothing Then
        '    ComboBox1.DroppedDown = True
        '    ComboBox1.Focus()
        'End If
    End Sub

    Private Sub ComboBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox1.KeyDown
        If e.KeyData = Keys.Enter Then
            If ComboBox1.SelectedIndex >= 0 Then
                ComboBox2.Select()
            End If
        End If
        If e.KeyData = Keys.Down Then
            ComboBox1.DroppedDown = True
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedIndex >= 0 Then
            itemmstmodule.categorycode = ds.Tables(0).Rows(ComboBox1.SelectedIndex).Item(0).ToString
            itemmstmodule.categoryname = ds.Tables(0).Rows(ComboBox1.SelectedIndex).Item(1).ToString
            'If Module1.flag = 2 Then
            '    ComboBox1.Text = itemmstmodule.categoryname
            'End If
        End If
    End Sub

    Private Sub ComboBox2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox2.KeyDown
        If e.KeyData = Keys.Enter Then
            If ComboBox2.SelectedIndex >= 0 Then
                ComboBox4.Select()
            End If
        End If
        If e.KeyData = Keys.Down Then
            ComboBox2.DroppedDown = True
        End If
    End Sub
    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        If ComboBox2.SelectedIndex >= 0 Then
            itemmstmodule.groupcode = ds1.Tables(0).Rows(ComboBox2.SelectedIndex).Item(0).ToString
            itemmstmodule.groupname = ds1.Tables(0).Rows(ComboBox2.SelectedIndex).Item(1).ToString
            'If Module1.flag = 2 Then
            '    ComboBox2.Text = itemmstmodule.groupname
            'End If
        End If
    End Sub


    Private Sub ComboBox3_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox3.KeyDown
        If e.KeyData = Keys.Enter Then
            If ComboBox3.SelectedIndex >= 0 Then
                ComboBox5.Select()
            End If
        End If
        If e.KeyData = Keys.Down Then
            ComboBox3.DroppedDown = True
        End If
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        If ComboBox3.SelectedIndex >= 0 Then
            itemmstmodule.ml = ds2.Tables(0).Rows(ComboBox3.SelectedIndex).Item(0)
            itemmstmodule.packing = ds2.Tables(0).Rows(ComboBox3.SelectedIndex).Item(1)
            'If Module1.flag = 2 Then
            '    ComboBox3.Text = itemmstmodule.ml
            '    TextBox2.Text = itemmstmodule.packing
            'End If
            TextBox2.Text = itemmstmodule.packing
        End If

    End Sub

    Private Sub ComboBox4_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox4.KeyDown
        If e.KeyData = Keys.Enter Then
            If ComboBox4.SelectedIndex >= 0 Then
                ComboBox3.Select()
            End If
        End If
        If e.KeyData = Keys.Down Then
            ComboBox4.DroppedDown = True
        End If
    End Sub

    Private Sub ComboBox4_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox4.SelectedIndexChanged
        If ComboBox4.SelectedIndex >= 0 Then
            itemmstmodule.kflcode = ds3.Tables(0).Rows(ComboBox4.SelectedIndex).Item(0).ToString
            itemmstmodule.kflname = ds3.Tables(0).Rows(ComboBox4.SelectedIndex).Item(1).ToString
            'If Module1.flag = 2 Then
            '    ComboBox4.Text = itemmstmodule.groupname
            'End If
        End If

    End Sub

    Private Sub ComboBox5_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox5.KeyDown
        If e.KeyData = Keys.Enter Then
            If ComboBox5.SelectedIndex >= 0 Then
                TextBox3.Select()
            End If
        End If
        If e.KeyData = Keys.Down Then
            ComboBox5.DroppedDown = True
        End If
    End Sub

    Private Sub ComboBox5_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox5.SelectedIndexChanged
        If ComboBox5.SelectedIndex >= 0 Then
            itemmstmodule.strengthname = ds4.Tables(0).Rows(ComboBox5.SelectedIndex).Item(0)
            'If Module1.flag = 2 Then
            '    ComboBox5.Text = itemmstmodule.strengthname
            'End If
        End If

    End Sub

    Private Sub TextBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        If e.KeyChar = "0" Or e.KeyChar = "1" Or e.KeyChar = "2" Or e.KeyChar = "3" Or e.KeyChar = "4" Or e.KeyChar = "5" Or e.KeyChar = "6" Or e.KeyChar = "7" Or e.KeyChar = "8" Or e.KeyChar = "9" Or e.KeyChar = "." Or e.KeyChar = vbBack Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox3_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox3.KeyDown
        If e.KeyData = Keys.Enter And Not TextBox4.Text = Nothing Then
            Button1.Focus()
        End If
    End Sub

    Private Sub itemmstadd_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        frm_MainForm.Enabled = True
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        e.Handled = True
    End Sub
End Class