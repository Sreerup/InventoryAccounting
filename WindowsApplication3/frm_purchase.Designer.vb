<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_purchase
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.Button2 = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.cmb_puchaseacc = New System.Windows.Forms.ComboBox
        Me.cmb_storage = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.txt_trnno = New System.Windows.Forms.TextBox
        Me.txt_billno = New System.Windows.Forms.TextBox
        Me.txt_narration = New System.Windows.Forms.TextBox
        Me.txt_gross = New System.Windows.Forms.TextBox
        Me.txt_net = New System.Windows.Forms.TextBox
        Me.cmb_supplier = New System.Windows.Forms.ComboBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.txt_tppass = New System.Windows.Forms.TextBox
        Me.RadioButton1 = New System.Windows.Forms.RadioButton
        Me.RadioButton2 = New System.Windows.Forms.RadioButton
        Me.Label10 = New System.Windows.Forms.Label
        Me.txt_rounding_plus = New System.Windows.Forms.TextBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.txt_rounding_minus = New System.Windows.Forms.TextBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.cmb_scheme = New System.Windows.Forms.ComboBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.dgv1 = New System.Windows.Forms.DataGridView
        Me.cmb_billnl = New System.Windows.Forms.ComboBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.txt_paid = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.txt_due = New System.Windows.Forms.TextBox
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Button3 = New System.Windows.Forms.Button
        CType(Me.dgv1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.FromArgb(CType(CType(252, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(182, Byte), Integer))
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(711, 570)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(100, 25)
        Me.Button2.TabIndex = 16
        Me.Button2.Text = "&Cancel"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(252, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(182, Byte), Integer))
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(498, 570)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(100, 25)
        Me.Button1.TabIndex = 14
        Me.Button1.Text = "&Save [F10]"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'cmb_puchaseacc
        '
        Me.cmb_puchaseacc.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmb_puchaseacc.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmb_puchaseacc.BackColor = System.Drawing.Color.Ivory
        Me.cmb_puchaseacc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_puchaseacc.Enabled = False
        Me.cmb_puchaseacc.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cmb_puchaseacc.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmb_puchaseacc.FormattingEnabled = True
        Me.cmb_puchaseacc.Location = New System.Drawing.Point(134, 69)
        Me.cmb_puchaseacc.Name = "cmb_puchaseacc"
        Me.cmb_puchaseacc.Size = New System.Drawing.Size(200, 21)
        Me.cmb_puchaseacc.TabIndex = 3
        Me.cmb_puchaseacc.TabStop = False
        '
        'cmb_storage
        '
        Me.cmb_storage.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmb_storage.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmb_storage.BackColor = System.Drawing.Color.Ivory
        Me.cmb_storage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_storage.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmb_storage.FormattingEnabled = True
        Me.cmb_storage.Location = New System.Drawing.Point(134, 113)
        Me.cmb_storage.Name = "cmb_storage"
        Me.cmb_storage.Size = New System.Drawing.Size(200, 21)
        Me.cmb_storage.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(32, 47)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 20)
        Me.Label1.TabIndex = 37
        Me.Label1.Text = "Trn NO"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(32, 69)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(100, 20)
        Me.Label2.TabIndex = 38
        Me.Label2.Text = "Purchase Head"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(601, 47)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(100, 20)
        Me.Label3.TabIndex = 39
        Me.Label3.Text = "Trans. Date"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(601, 69)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(100, 20)
        Me.Label7.TabIndex = 43
        Me.Label7.Text = "Doc./ Bill Date"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(32, 113)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(100, 20)
        Me.Label4.TabIndex = 40
        Me.Label4.Text = "Storage"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.CalendarMonthBackground = System.Drawing.SystemColors.ActiveCaptionText
        Me.DateTimePicker1.CalendarTitleBackColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(253, Byte), Integer))
        Me.DateTimePicker1.CalendarTitleForeColor = System.Drawing.SystemColors.ControlText
        Me.DateTimePicker1.CustomFormat = "dd/MM/yyyy"
        Me.DateTimePicker1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(703, 47)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(100, 20)
        Me.DateTimePicker1.TabIndex = 6
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.CalendarTitleBackColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(253, Byte), Integer))
        Me.DateTimePicker2.CalendarTitleForeColor = System.Drawing.SystemColors.ControlText
        Me.DateTimePicker2.CustomFormat = "dd/MM/yyyy"
        Me.DateTimePicker2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker2.Location = New System.Drawing.Point(703, 69)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(100, 20)
        Me.DateTimePicker2.TabIndex = 7
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(32, 91)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(100, 20)
        Me.Label5.TabIndex = 41
        Me.Label5.Text = "Supplier"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(601, 91)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(100, 20)
        Me.Label6.TabIndex = 42
        Me.Label6.Text = "Doc./Bill No"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(6, 482)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(64, 46)
        Me.Label8.TabIndex = 44
        Me.Label8.Text = "Narration"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Label9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(548, 328)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(105, 20)
        Me.Label9.TabIndex = 45
        Me.Label9.Text = "Gross Amount"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Label11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(548, 477)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(105, 20)
        Me.Label11.TabIndex = 47
        Me.Label11.Text = "Nett Amount"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txt_trnno
        '
        Me.txt_trnno.BackColor = System.Drawing.Color.Ivory
        Me.txt_trnno.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_trnno.Location = New System.Drawing.Point(134, 47)
        Me.txt_trnno.MaxLength = 18
        Me.txt_trnno.Name = "txt_trnno"
        Me.txt_trnno.ReadOnly = True
        Me.txt_trnno.Size = New System.Drawing.Size(200, 20)
        Me.txt_trnno.TabIndex = 2
        Me.txt_trnno.TabStop = False
        '
        'txt_billno
        '
        Me.txt_billno.BackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.txt_billno.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_billno.Location = New System.Drawing.Point(703, 91)
        Me.txt_billno.MaxLength = 15
        Me.txt_billno.Name = "txt_billno"
        Me.txt_billno.Size = New System.Drawing.Size(100, 20)
        Me.txt_billno.TabIndex = 8
        '
        'txt_narration
        '
        Me.txt_narration.BackColor = System.Drawing.Color.Ivory
        Me.txt_narration.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_narration.Location = New System.Drawing.Point(98, 505)
        Me.txt_narration.MaxLength = 100
        Me.txt_narration.Multiline = True
        Me.txt_narration.Name = "txt_narration"
        Me.txt_narration.Size = New System.Drawing.Size(242, 48)
        Me.txt_narration.TabIndex = 16
        '
        'txt_gross
        '
        Me.txt_gross.BackColor = System.Drawing.Color.Ivory
        Me.txt_gross.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_gross.Location = New System.Drawing.Point(655, 329)
        Me.txt_gross.MaxLength = 13
        Me.txt_gross.Name = "txt_gross"
        Me.txt_gross.ReadOnly = True
        Me.txt_gross.Size = New System.Drawing.Size(150, 20)
        Me.txt_gross.TabIndex = 52
        Me.txt_gross.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_net
        '
        Me.txt_net.BackColor = System.Drawing.Color.Ivory
        Me.txt_net.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_net.Location = New System.Drawing.Point(655, 477)
        Me.txt_net.MaxLength = 13
        Me.txt_net.Name = "txt_net"
        Me.txt_net.ReadOnly = True
        Me.txt_net.Size = New System.Drawing.Size(150, 20)
        Me.txt_net.TabIndex = 54
        Me.txt_net.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cmb_supplier
        '
        Me.cmb_supplier.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmb_supplier.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmb_supplier.BackColor = System.Drawing.Color.Ivory
        Me.cmb_supplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_supplier.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmb_supplier.FormattingEnabled = True
        Me.cmb_supplier.Location = New System.Drawing.Point(134, 91)
        Me.cmb_supplier.Name = "cmb_supplier"
        Me.cmb_supplier.Size = New System.Drawing.Size(200, 21)
        Me.cmb_supplier.TabIndex = 4
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Label13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(601, 113)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(100, 20)
        Me.Label13.TabIndex = 58
        Me.Label13.Text = "T.P.Pass No."
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txt_tppass
        '
        Me.txt_tppass.BackColor = System.Drawing.Color.Ivory
        Me.txt_tppass.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_tppass.Location = New System.Drawing.Point(703, 113)
        Me.txt_tppass.MaxLength = 15
        Me.txt_tppass.Name = "txt_tppass"
        Me.txt_tppass.Size = New System.Drawing.Size(100, 20)
        Me.txt_tppass.TabIndex = 10
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.BackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.RadioButton1.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.RadioButton1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadioButton1.Location = New System.Drawing.Point(22, 3)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(84, 18)
        Me.RadioButton1.TabIndex = 0
        Me.RadioButton1.Text = "&Purchase"
        Me.RadioButton1.UseVisualStyleBackColor = False
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.BackColor = System.Drawing.Color.Transparent
        Me.RadioButton2.FlatAppearance.BorderColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.RadioButton2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadioButton2.Location = New System.Drawing.Point(126, 4)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(120, 17)
        Me.RadioButton2.TabIndex = 1
        Me.RadioButton2.Text = "Purchase &Return"
        Me.RadioButton2.UseVisualStyleBackColor = False
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Label10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(548, 435)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(105, 20)
        Me.Label10.TabIndex = 85
        Me.Label10.Text = "Rounding off (+)"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txt_rounding_plus
        '
        Me.txt_rounding_plus.BackColor = System.Drawing.Color.Ivory
        Me.txt_rounding_plus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_rounding_plus.Location = New System.Drawing.Point(655, 435)
        Me.txt_rounding_plus.MaxLength = 13
        Me.txt_rounding_plus.Name = "txt_rounding_plus"
        Me.txt_rounding_plus.ReadOnly = True
        Me.txt_rounding_plus.Size = New System.Drawing.Size(150, 20)
        Me.txt_rounding_plus.TabIndex = 86
        Me.txt_rounding_plus.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Label20.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(548, 456)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(105, 20)
        Me.Label20.TabIndex = 87
        Me.Label20.Text = "Rounding off (-)"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txt_rounding_minus
        '
        Me.txt_rounding_minus.BackColor = System.Drawing.Color.Ivory
        Me.txt_rounding_minus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_rounding_minus.Location = New System.Drawing.Point(655, 456)
        Me.txt_rounding_minus.MaxLength = 13
        Me.txt_rounding_minus.Name = "txt_rounding_minus"
        Me.txt_rounding_minus.ReadOnly = True
        Me.txt_rounding_minus.Size = New System.Drawing.Size(150, 20)
        Me.txt_rounding_minus.TabIndex = 88
        Me.txt_rounding_minus.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Ivory
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Location = New System.Drawing.Point(31, 368)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(309, 129)
        Me.Panel1.TabIndex = 13
        Me.Panel1.TabStop = True
        '
        'cmb_scheme
        '
        Me.cmb_scheme.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmb_scheme.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmb_scheme.BackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.cmb_scheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_scheme.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmb_scheme.FormattingEnabled = True
        Me.cmb_scheme.Location = New System.Drawing.Point(31, 348)
        Me.cmb_scheme.Name = "cmb_scheme"
        Me.cmb_scheme.Size = New System.Drawing.Size(310, 21)
        Me.cmb_scheme.TabIndex = 12
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.Ivory
        Me.Label12.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(31, 329)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(309, 20)
        Me.Label12.TabIndex = 125
        Me.Label12.Text = "Tax Scheme"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'dgv1
        '
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.NavajoWhite
        Me.dgv1.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv1.BackgroundColor = System.Drawing.Color.Ivory
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgv1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.Ivory
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv1.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgv1.Location = New System.Drawing.Point(31, 136)
        Me.dgv1.Name = "dgv1"
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.dgv1.RowsDefaultCellStyle = DataGridViewCellStyle4
        Me.dgv1.Size = New System.Drawing.Size(774, 183)
        Me.dgv1.TabIndex = 11
        '
        'cmb_billnl
        '
        Me.cmb_billnl.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmb_billnl.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmb_billnl.BackColor = System.Drawing.Color.Ivory
        Me.cmb_billnl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_billnl.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmb_billnl.FormattingEnabled = True
        Me.cmb_billnl.Location = New System.Drawing.Point(703, 90)
        Me.cmb_billnl.Name = "cmb_billnl"
        Me.cmb_billnl.Size = New System.Drawing.Size(100, 21)
        Me.cmb_billnl.TabIndex = 9
        Me.cmb_billnl.Visible = False
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Label14.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(548, 498)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(105, 20)
        Me.Label14.TabIndex = 126
        Me.Label14.Text = "Paid Amount"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label14.Visible = False
        '
        'txt_paid
        '
        Me.txt_paid.BackColor = System.Drawing.Color.Ivory
        Me.txt_paid.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_paid.Location = New System.Drawing.Point(655, 498)
        Me.txt_paid.MaxLength = 13
        Me.txt_paid.Name = "txt_paid"
        Me.txt_paid.Size = New System.Drawing.Size(150, 20)
        Me.txt_paid.TabIndex = 127
        Me.txt_paid.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txt_paid.Visible = False
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Label15.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(548, 519)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(105, 20)
        Me.Label15.TabIndex = 128
        Me.Label15.Text = "Due Amount"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label15.Visible = False
        '
        'txt_due
        '
        Me.txt_due.BackColor = System.Drawing.Color.Ivory
        Me.txt_due.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_due.Location = New System.Drawing.Point(655, 519)
        Me.txt_due.MaxLength = 13
        Me.txt_due.Name = "txt_due"
        Me.txt_due.ReadOnly = True
        Me.txt_due.Size = New System.Drawing.Size(150, 20)
        Me.txt_due.TabIndex = 129
        Me.txt_due.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txt_due.Visible = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(147, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(147, Byte), Integer))
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.RadioButton1)
        Me.Panel2.Controls.Add(Me.RadioButton2)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Location = New System.Drawing.Point(25, 23)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(786, 535)
        Me.Panel2.TabIndex = 130
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.Color.FromArgb(CType(CType(252, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(182, Byte), Integer))
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.Location = New System.Drawing.Point(605, 570)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(100, 25)
        Me.Button3.TabIndex = 131
        Me.Button3.Text = "Print [F2]"
        Me.Button3.UseVisualStyleBackColor = False
        Me.Button3.Visible = False
        '
        'purchase
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.NavajoWhite
        Me.ClientSize = New System.Drawing.Size(844, 606)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.txt_due)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.txt_paid)
        Me.Controls.Add(Me.cmb_billnl)
        Me.Controls.Add(Me.dgv1)
        Me.Controls.Add(Me.cmb_scheme)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.txt_rounding_minus)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txt_rounding_plus)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.txt_tppass)
        Me.Controls.Add(Me.cmb_supplier)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.cmb_puchaseacc)
        Me.Controls.Add(Me.cmb_storage)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.DateTimePicker2)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.txt_trnno)
        Me.Controls.Add(Me.txt_billno)
        Me.Controls.Add(Me.txt_narration)
        Me.Controls.Add(Me.txt_gross)
        Me.Controls.Add(Me.txt_net)
        Me.Controls.Add(Me.Panel2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(170, 78)
        Me.MaximizeBox = False
        Me.Name = "purchase"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Purchase Bill"
        CType(Me.dgv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents cmb_puchaseacc As System.Windows.Forms.ComboBox
    Friend WithEvents cmb_storage As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DateTimePicker2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txt_trnno As System.Windows.Forms.TextBox
    Friend WithEvents txt_billno As System.Windows.Forms.TextBox
    Friend WithEvents txt_narration As System.Windows.Forms.TextBox
    Friend WithEvents txt_gross As System.Windows.Forms.TextBox
    Friend WithEvents txt_net As System.Windows.Forms.TextBox
    Friend WithEvents cmb_supplier As System.Windows.Forms.ComboBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txt_tppass As System.Windows.Forms.TextBox
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton2 As System.Windows.Forms.RadioButton
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txt_rounding_plus As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txt_rounding_minus As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents cmb_scheme As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents dgv1 As System.Windows.Forms.DataGridView
    Friend WithEvents cmb_billnl As System.Windows.Forms.ComboBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txt_paid As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txt_due As System.Windows.Forms.TextBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Button3 As System.Windows.Forms.Button
End Class
