Public Class frm_yearcreation


    Dim s As String
    Dim ycode As String
    Dim ob As New Class1

    Dim present As Integer


    Private Sub yearcreation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        present = 0
    End Sub

    Private Sub get_yearcode()
        s = "select top 1 yearcode from yearmst order by yearcode desc"
        s = ob.executereader(s)
        If Not s = Nothing Then
            ycode = Convert.ToInt32(s) + 1
        Else
            ycode = 1
        End If
    End Sub


    Private Sub check_present()

        s = "select count(*) from yearmst  where companycode='" & Module1.companycode & "'and yearrange='" & TextBox1.Text & "-" & TextBox2.Text & "'"
        s = ob.executereader(s)
        If Convert.ToInt32(s) > 0 Then
            MsgBox("Year already created for this company.", MsgBoxStyle.Exclamation, "Year Exist")
            present = 1
        End If

    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click


        If Date.Today.Day.ToString <> "31" And Date.Today.Day.ToString <> "01" And Date.Today.Month.ToString <> "03" And Date.Today.Day.ToString <> "04" Then
            Dim ycheck = MsgBox("This is not the time of Financial year ending or starting, do you want to create a new year this time?", MsgBoxStyle.YesNoCancel, "New Year?")
            If ycheck = 6 Then
                Dim ycheck1 = MsgBox("Are you sure you want to create new year?", MsgBoxStyle.YesNoCancel, "New Year?")
                If ycheck1 <> 6 Then
                    Exit Sub
                End If
            Else
                Exit Sub
            End If
        End If


        check_present()
        get_yearcode()
        If (present = 0) Then
            s = "insert into yearmst(yearcode,yearrange,stdate,enddate,companycode) values('" & ycode & "','" & TextBox1.Text & "-" & TextBox2.Text & "','" & TextBox1.Text & "-04-01','" & TextBox2.Text & "-03-31','" & Module1.companycode & "')"
            ob.insert(s)
            frm_ContainerForm.year_drop.DropDownItems.Add(TextBox1.Text & "-" & TextBox2.Text)
        End If
        Me.Close()
    End Sub



    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub TextBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyUp
        If e.KeyData = Keys.Enter Then
            TextBox2.Select()
        End If
    End Sub

    Private Sub TextBox2_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyUp
        If e.KeyData = Keys.Enter Then
            Button1.Select()
        End If
    End Sub

End Class