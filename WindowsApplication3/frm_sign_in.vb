Imports System.Configuration
Imports System.Data.SqlClient

Public Class frm_sign_in
    Dim config As System.Configuration.Configuration

    Private Sub sign_in_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ComboBox1.SelectedItem = ComboBox1.Items(0)
        config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
        TextBox1.Select()

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click


        config.AppSettings.Settings("server_name").Value = TextBox1.Text
        config.AppSettings.Settings("authentication").Value = ComboBox1.Text


        config.Save(ConfigurationSaveMode.Modified)
        ConfigurationManager.RefreshSection("appSettings")
        Me.Close()

    End Sub

    Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyData = Keys.Enter And TextBox1.Text <> "" Then
            Button1.Select()
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedIndex = 0 Then
            TextBox2.Enabled = False
            TextBox3.Enabled = False
        Else
            TextBox2.Enabled = True
            TextBox3.Enabled = True
        End If
    End Sub
End Class