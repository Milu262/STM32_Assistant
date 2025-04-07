namespace STM32_Assistant
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.GroupBox groupBox1;
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.UART_tabPage = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Clear_rec_textbox_button = new System.Windows.Forms.Button();
            this.recive_textBox = new System.Windows.Forms.TextBox();
            this.Uart_send_button = new System.Windows.Forms.Button();
            this.Clear_send_textbox_button = new System.Windows.Forms.Button();
            this.Uart_send_textBox = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.uart_rec_str_radioButton = new System.Windows.Forms.RadioButton();
            this.uart_rec_hex_radioButton = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.uart_send_str_radioButton = new System.Windows.Forms.RadioButton();
            this.uart_send_hex_radioButton = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Band_rate_comboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Serial_port_comboBox = new System.Windows.Forms.ComboBox();
            this.uart_uart_control_button = new System.Windows.Forms.Button();
            this.I2C_tabPage = new System.Windows.Forms.TabPage();
            this.find_i2c_device_button = new System.Windows.Forms.Button();
            this.reg_value_textBox = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.write_i2c_button = new System.Windows.Forms.Button();
            this.read_i2c_button = new System.Windows.Forms.Button();
            this.reg_adress_textBox = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.device_adress_textBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.I2C_16bit_radioButton = new System.Windows.Forms.RadioButton();
            this.I2C_8bit_radioButton = new System.Windows.Forms.RadioButton();
            this.label10 = new System.Windows.Forms.Label();
            this.Clear_I2C_rec_button = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.I2C_recive_textBox = new System.Windows.Forms.TextBox();
            this.uart_I2C_control_button = new System.Windows.Forms.Button();
            this.Band_rate_I2C_comboBox = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.Serial_port_I2C_comboBox = new System.Windows.Forms.ComboBox();
            this.Uart_serialPort = new System.IO.Ports.SerialPort(this.components);
            this.I2C_serialPort = new System.IO.Ports.SerialPort(this.components);
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabControl1.SuspendLayout();
            this.UART_tabPage.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.I2C_tabPage.SuspendLayout();
            this.panel3.SuspendLayout();
            groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.UART_tabPage);
            this.tabControl1.Controls.Add(this.I2C_tabPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1011, 605);
            this.tabControl1.TabIndex = 0;
            // 
            // UART_tabPage
            // 
            this.UART_tabPage.Controls.Add(this.label6);
            this.UART_tabPage.Controls.Add(this.label5);
            this.UART_tabPage.Controls.Add(this.Clear_rec_textbox_button);
            this.UART_tabPage.Controls.Add(this.recive_textBox);
            this.UART_tabPage.Controls.Add(this.Uart_send_button);
            this.UART_tabPage.Controls.Add(this.Clear_send_textbox_button);
            this.UART_tabPage.Controls.Add(this.Uart_send_textBox);
            this.UART_tabPage.Controls.Add(this.panel2);
            this.UART_tabPage.Controls.Add(this.panel1);
            this.UART_tabPage.Controls.Add(this.label4);
            this.UART_tabPage.Controls.Add(this.label3);
            this.UART_tabPage.Controls.Add(this.Band_rate_comboBox);
            this.UART_tabPage.Controls.Add(this.label2);
            this.UART_tabPage.Controls.Add(this.label1);
            this.UART_tabPage.Controls.Add(this.Serial_port_comboBox);
            this.UART_tabPage.Controls.Add(this.uart_uart_control_button);
            this.UART_tabPage.Location = new System.Drawing.Point(4, 25);
            this.UART_tabPage.Name = "UART_tabPage";
            this.UART_tabPage.Padding = new System.Windows.Forms.Padding(3);
            this.UART_tabPage.Size = new System.Drawing.Size(1003, 576);
            this.UART_tabPage.TabIndex = 0;
            this.UART_tabPage.Text = "UART";
            this.UART_tabPage.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(397, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 24);
            this.label6.TabIndex = 19;
            this.label6.Text = "接收区：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(8, 314);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 24);
            this.label5.TabIndex = 18;
            this.label5.Text = "发送区：";
            // 
            // Clear_rec_textbox_button
            // 
            this.Clear_rec_textbox_button.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Clear_rec_textbox_button.Location = new System.Drawing.Point(860, 382);
            this.Clear_rec_textbox_button.Name = "Clear_rec_textbox_button";
            this.Clear_rec_textbox_button.Size = new System.Drawing.Size(125, 48);
            this.Clear_rec_textbox_button.TabIndex = 17;
            this.Clear_rec_textbox_button.Text = "清除";
            this.Clear_rec_textbox_button.UseVisualStyleBackColor = true;
            this.Clear_rec_textbox_button.Click += new System.EventHandler(this.Clear_rec_textbox_button_Click);
            // 
            // recive_textBox
            // 
            this.recive_textBox.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.recive_textBox.Location = new System.Drawing.Point(485, 4);
            this.recive_textBox.Multiline = true;
            this.recive_textBox.Name = "recive_textBox";
            this.recive_textBox.ReadOnly = true;
            this.recive_textBox.Size = new System.Drawing.Size(500, 369);
            this.recive_textBox.TabIndex = 16;
            this.recive_textBox.TextChanged += new System.EventHandler(this.recive_textBox_TextChanged);
            // 
            // Uart_send_button
            // 
            this.Uart_send_button.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Uart_send_button.Location = new System.Drawing.Point(167, 455);
            this.Uart_send_button.Name = "Uart_send_button";
            this.Uart_send_button.Size = new System.Drawing.Size(125, 48);
            this.Uart_send_button.TabIndex = 15;
            this.Uart_send_button.Text = "发送";
            this.Uart_send_button.UseVisualStyleBackColor = true;
            this.Uart_send_button.Click += new System.EventHandler(this.Uart_send_button_Click);
            // 
            // Clear_send_textbox_button
            // 
            this.Clear_send_textbox_button.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Clear_send_textbox_button.Location = new System.Drawing.Point(12, 455);
            this.Clear_send_textbox_button.Name = "Clear_send_textbox_button";
            this.Clear_send_textbox_button.Size = new System.Drawing.Size(125, 48);
            this.Clear_send_textbox_button.TabIndex = 14;
            this.Clear_send_textbox_button.Text = "清除";
            this.Clear_send_textbox_button.UseVisualStyleBackColor = true;
            this.Clear_send_textbox_button.Click += new System.EventHandler(this.Clear_send_textbox_button_Click);
            // 
            // Uart_send_textBox
            // 
            this.Uart_send_textBox.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Uart_send_textBox.Location = new System.Drawing.Point(11, 341);
            this.Uart_send_textBox.Multiline = true;
            this.Uart_send_textBox.Name = "Uart_send_textBox";
            this.Uart_send_textBox.Size = new System.Drawing.Size(281, 89);
            this.Uart_send_textBox.TabIndex = 13;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.uart_rec_str_radioButton);
            this.panel2.Controls.Add(this.uart_rec_hex_radioButton);
            this.panel2.Location = new System.Drawing.Point(91, 166);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(148, 44);
            this.panel2.TabIndex = 12;
            // 
            // uart_rec_str_radioButton
            // 
            this.uart_rec_str_radioButton.AutoSize = true;
            this.uart_rec_str_radioButton.Location = new System.Drawing.Point(76, 10);
            this.uart_rec_str_radioButton.Name = "uart_rec_str_radioButton";
            this.uart_rec_str_radioButton.Size = new System.Drawing.Size(58, 19);
            this.uart_rec_str_radioButton.TabIndex = 10;
            this.uart_rec_str_radioButton.TabStop = true;
            this.uart_rec_str_radioButton.Text = "字符";
            this.uart_rec_str_radioButton.UseVisualStyleBackColor = true;
            // 
            // uart_rec_hex_radioButton
            // 
            this.uart_rec_hex_radioButton.AutoSize = true;
            this.uart_rec_hex_radioButton.Location = new System.Drawing.Point(5, 10);
            this.uart_rec_hex_radioButton.Name = "uart_rec_hex_radioButton";
            this.uart_rec_hex_radioButton.Size = new System.Drawing.Size(52, 19);
            this.uart_rec_hex_radioButton.TabIndex = 9;
            this.uart_rec_hex_radioButton.TabStop = true;
            this.uart_rec_hex_radioButton.Text = "HEX";
            this.uart_rec_hex_radioButton.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.uart_send_str_radioButton);
            this.panel1.Controls.Add(this.uart_send_hex_radioButton);
            this.panel1.Location = new System.Drawing.Point(89, 105);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(151, 44);
            this.panel1.TabIndex = 11;
            // 
            // uart_send_str_radioButton
            // 
            this.uart_send_str_radioButton.AutoSize = true;
            this.uart_send_str_radioButton.Location = new System.Drawing.Point(78, 14);
            this.uart_send_str_radioButton.Name = "uart_send_str_radioButton";
            this.uart_send_str_radioButton.Size = new System.Drawing.Size(58, 19);
            this.uart_send_str_radioButton.TabIndex = 8;
            this.uart_send_str_radioButton.TabStop = true;
            this.uart_send_str_radioButton.Text = "字符";
            this.uart_send_str_radioButton.UseVisualStyleBackColor = true;
            // 
            // uart_send_hex_radioButton
            // 
            this.uart_send_hex_radioButton.AutoSize = true;
            this.uart_send_hex_radioButton.Location = new System.Drawing.Point(7, 14);
            this.uart_send_hex_radioButton.Name = "uart_send_hex_radioButton";
            this.uart_send_hex_radioButton.Size = new System.Drawing.Size(52, 19);
            this.uart_send_hex_radioButton.TabIndex = 7;
            this.uart_send_hex_radioButton.TabStop = true;
            this.uart_send_hex_radioButton.Text = "HEX";
            this.uart_send_hex_radioButton.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(8, 171);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 24);
            this.label4.TabIndex = 6;
            this.label4.Text = "接收模式";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(8, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 24);
            this.label3.TabIndex = 5;
            this.label3.Text = "发送模式";
            // 
            // Band_rate_comboBox
            // 
            this.Band_rate_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Band_rate_comboBox.FormattingEnabled = true;
            this.Band_rate_comboBox.Items.AddRange(new object[] {
            "9600",
            "14400",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.Band_rate_comboBox.Location = new System.Drawing.Point(91, 70);
            this.Band_rate_comboBox.Name = "Band_rate_comboBox";
            this.Band_rate_comboBox.Size = new System.Drawing.Size(121, 23);
            this.Band_rate_comboBox.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(8, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "波特率";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(8, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "串口号";
            // 
            // Serial_port_comboBox
            // 
            this.Serial_port_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Serial_port_comboBox.FormattingEnabled = true;
            this.Serial_port_comboBox.Location = new System.Drawing.Point(91, 24);
            this.Serial_port_comboBox.Name = "Serial_port_comboBox";
            this.Serial_port_comboBox.Size = new System.Drawing.Size(121, 23);
            this.Serial_port_comboBox.TabIndex = 1;
            // 
            // uart_uart_control_button
            // 
            this.uart_uart_control_button.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uart_uart_control_button.Location = new System.Drawing.Point(96, 216);
            this.uart_uart_control_button.Name = "uart_uart_control_button";
            this.uart_uart_control_button.Size = new System.Drawing.Size(125, 48);
            this.uart_uart_control_button.TabIndex = 0;
            this.uart_uart_control_button.Text = "打开串口";
            this.uart_uart_control_button.UseVisualStyleBackColor = true;
            this.uart_uart_control_button.Click += new System.EventHandler(this.uart_uart_control_button_Click);
            // 
            // I2C_tabPage
            // 
            this.I2C_tabPage.Controls.Add(groupBox1);
            this.I2C_tabPage.Controls.Add(this.label15);
            this.I2C_tabPage.Controls.Add(this.label14);
            this.I2C_tabPage.Controls.Add(this.find_i2c_device_button);
            this.I2C_tabPage.Controls.Add(this.reg_value_textBox);
            this.I2C_tabPage.Controls.Add(this.label13);
            this.I2C_tabPage.Controls.Add(this.write_i2c_button);
            this.I2C_tabPage.Controls.Add(this.read_i2c_button);
            this.I2C_tabPage.Controls.Add(this.reg_adress_textBox);
            this.I2C_tabPage.Controls.Add(this.label12);
            this.I2C_tabPage.Controls.Add(this.device_adress_textBox);
            this.I2C_tabPage.Controls.Add(this.label11);
            this.I2C_tabPage.Controls.Add(this.panel3);
            this.I2C_tabPage.Controls.Add(this.label10);
            this.I2C_tabPage.Controls.Add(this.Clear_I2C_rec_button);
            this.I2C_tabPage.Controls.Add(this.label9);
            this.I2C_tabPage.Controls.Add(this.I2C_recive_textBox);
            this.I2C_tabPage.Location = new System.Drawing.Point(4, 25);
            this.I2C_tabPage.Name = "I2C_tabPage";
            this.I2C_tabPage.Padding = new System.Windows.Forms.Padding(3);
            this.I2C_tabPage.Size = new System.Drawing.Size(1003, 576);
            this.I2C_tabPage.TabIndex = 1;
            this.I2C_tabPage.Text = "I2C";
            this.I2C_tabPage.UseVisualStyleBackColor = true;
            // 
            // find_i2c_device_button
            // 
            this.find_i2c_device_button.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.find_i2c_device_button.Location = new System.Drawing.Point(399, 486);
            this.find_i2c_device_button.Name = "find_i2c_device_button";
            this.find_i2c_device_button.Size = new System.Drawing.Size(172, 48);
            this.find_i2c_device_button.TabIndex = 23;
            this.find_i2c_device_button.Text = "Find i2c device";
            this.find_i2c_device_button.UseVisualStyleBackColor = true;
            this.find_i2c_device_button.Click += new System.EventHandler(this.find_i2c_device_button_Click);
            // 
            // reg_value_textBox
            // 
            this.reg_value_textBox.Location = new System.Drawing.Point(160, 417);
            this.reg_value_textBox.Name = "reg_value_textBox";
            this.reg_value_textBox.Size = new System.Drawing.Size(69, 25);
            this.reg_value_textBox.TabIndex = 22;
            this.reg_value_textBox.TextChanged += new System.EventHandler(this.reg_value_textBox_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(13, 414);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(82, 24);
            this.label13.TabIndex = 21;
            this.label13.Text = "寄存器值";
            // 
            // write_i2c_button
            // 
            this.write_i2c_button.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.write_i2c_button.Location = new System.Drawing.Point(205, 486);
            this.write_i2c_button.Name = "write_i2c_button";
            this.write_i2c_button.Size = new System.Drawing.Size(125, 48);
            this.write_i2c_button.TabIndex = 20;
            this.write_i2c_button.Text = "写I2C";
            this.write_i2c_button.UseVisualStyleBackColor = true;
            this.write_i2c_button.Click += new System.EventHandler(this.write_i2c_button_Click);
            // 
            // read_i2c_button
            // 
            this.read_i2c_button.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.read_i2c_button.Location = new System.Drawing.Point(30, 486);
            this.read_i2c_button.Name = "read_i2c_button";
            this.read_i2c_button.Size = new System.Drawing.Size(125, 48);
            this.read_i2c_button.TabIndex = 19;
            this.read_i2c_button.Text = "读I2C";
            this.read_i2c_button.UseVisualStyleBackColor = true;
            this.read_i2c_button.Click += new System.EventHandler(this.read_i2c_button_Click);
            // 
            // reg_adress_textBox
            // 
            this.reg_adress_textBox.Location = new System.Drawing.Point(160, 364);
            this.reg_adress_textBox.Name = "reg_adress_textBox";
            this.reg_adress_textBox.Size = new System.Drawing.Size(69, 25);
            this.reg_adress_textBox.TabIndex = 18;
            this.reg_adress_textBox.TextChanged += new System.EventHandler(this.reg_adress_textBox_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(13, 361);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(100, 24);
            this.label12.TabIndex = 17;
            this.label12.Text = "寄存器地址";
            // 
            // device_adress_textBox
            // 
            this.device_adress_textBox.Location = new System.Drawing.Point(160, 313);
            this.device_adress_textBox.Name = "device_adress_textBox";
            this.device_adress_textBox.Size = new System.Drawing.Size(69, 25);
            this.device_adress_textBox.TabIndex = 16;
            this.device_adress_textBox.TextChanged += new System.EventHandler(this.device_adress_textBox_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(11, 313);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(126, 24);
            this.label11.TabIndex = 15;
            this.label11.Text = "设备地址    0x:";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.I2C_16bit_radioButton);
            this.panel3.Controls.Add(this.I2C_8bit_radioButton);
            this.panel3.Location = new System.Drawing.Point(127, 243);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(151, 44);
            this.panel3.TabIndex = 14;
            // 
            // I2C_16bit_radioButton
            // 
            this.I2C_16bit_radioButton.AutoSize = true;
            this.I2C_16bit_radioButton.Location = new System.Drawing.Point(78, 14);
            this.I2C_16bit_radioButton.Name = "I2C_16bit_radioButton";
            this.I2C_16bit_radioButton.Size = new System.Drawing.Size(68, 19);
            this.I2C_16bit_radioButton.TabIndex = 8;
            this.I2C_16bit_radioButton.TabStop = true;
            this.I2C_16bit_radioButton.Text = "16bit";
            this.I2C_16bit_radioButton.UseVisualStyleBackColor = true;
            // 
            // I2C_8bit_radioButton
            // 
            this.I2C_8bit_radioButton.AutoSize = true;
            this.I2C_8bit_radioButton.Location = new System.Drawing.Point(7, 14);
            this.I2C_8bit_radioButton.Name = "I2C_8bit_radioButton";
            this.I2C_8bit_radioButton.Size = new System.Drawing.Size(60, 19);
            this.I2C_8bit_radioButton.TabIndex = 7;
            this.I2C_8bit_radioButton.TabStop = true;
            this.I2C_8bit_radioButton.Text = "8bit";
            this.I2C_8bit_radioButton.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(11, 252);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(110, 24);
            this.label10.TabIndex = 13;
            this.label10.Text = "I2C模式选择";
            // 
            // Clear_I2C_rec_button
            // 
            this.Clear_I2C_rec_button.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Clear_I2C_rec_button.Location = new System.Drawing.Point(872, 182);
            this.Clear_I2C_rec_button.Name = "Clear_I2C_rec_button";
            this.Clear_I2C_rec_button.Size = new System.Drawing.Size(125, 48);
            this.Clear_I2C_rec_button.TabIndex = 12;
            this.Clear_I2C_rec_button.Text = "清除";
            this.Clear_I2C_rec_button.UseVisualStyleBackColor = true;
            this.Clear_I2C_rec_button.Click += new System.EventHandler(this.Clear_I2C_rec_button_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(433, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 24);
            this.label9.TabIndex = 11;
            this.label9.Text = "接收区：";
            // 
            // I2C_recive_textBox
            // 
            this.I2C_recive_textBox.Location = new System.Drawing.Point(524, 3);
            this.I2C_recive_textBox.Multiline = true;
            this.I2C_recive_textBox.Name = "I2C_recive_textBox";
            this.I2C_recive_textBox.ReadOnly = true;
            this.I2C_recive_textBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.I2C_recive_textBox.Size = new System.Drawing.Size(471, 173);
            this.I2C_recive_textBox.TabIndex = 10;
            // 
            // uart_I2C_control_button
            // 
            this.uart_I2C_control_button.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uart_I2C_control_button.Location = new System.Drawing.Point(83, 113);
            this.uart_I2C_control_button.Name = "uart_I2C_control_button";
            this.uart_I2C_control_button.Size = new System.Drawing.Size(125, 48);
            this.uart_I2C_control_button.TabIndex = 9;
            this.uart_I2C_control_button.Text = "打开串口";
            this.uart_I2C_control_button.UseVisualStyleBackColor = true;
            this.uart_I2C_control_button.Click += new System.EventHandler(this.uart_I2C_control_button_Click);
            // 
            // Band_rate_I2C_comboBox
            // 
            this.Band_rate_I2C_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Band_rate_I2C_comboBox.FormattingEnabled = true;
            this.Band_rate_I2C_comboBox.Items.AddRange(new object[] {
            "9600",
            "14400",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.Band_rate_I2C_comboBox.Location = new System.Drawing.Point(87, 65);
            this.Band_rate_I2C_comboBox.Name = "Band_rate_I2C_comboBox";
            this.Band_rate_I2C_comboBox.Size = new System.Drawing.Size(121, 23);
            this.Band_rate_I2C_comboBox.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(4, 61);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 24);
            this.label7.TabIndex = 7;
            this.label7.Text = "波特率";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(4, 18);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 24);
            this.label8.TabIndex = 6;
            this.label8.Text = "串口号";
            // 
            // Serial_port_I2C_comboBox
            // 
            this.Serial_port_I2C_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Serial_port_I2C_comboBox.FormattingEnabled = true;
            this.Serial_port_I2C_comboBox.Location = new System.Drawing.Point(87, 19);
            this.Serial_port_I2C_comboBox.Name = "Serial_port_I2C_comboBox";
            this.Serial_port_I2C_comboBox.Size = new System.Drawing.Size(121, 23);
            this.Serial_port_I2C_comboBox.TabIndex = 5;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.Location = new System.Drawing.Point(11, 194);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(119, 24);
            this.label14.TabIndex = 24;
            this.label14.Text = "SDA引角:  B9";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.Location = new System.Drawing.Point(156, 194);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(113, 24);
            this.label15.TabIndex = 25;
            this.label15.Text = "SCL引角:  B8";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(this.uart_I2C_control_button);
            groupBox1.Controls.Add(this.Band_rate_I2C_comboBox);
            groupBox1.Controls.Add(this.label7);
            groupBox1.Controls.Add(this.label8);
            groupBox1.Controls.Add(this.Serial_port_I2C_comboBox);
            groupBox1.Location = new System.Drawing.Point(9, 15);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(260, 167);
            groupBox1.TabIndex = 26;
            groupBox1.TabStop = false;
            groupBox1.Text = "uart port";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1011, 605);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "STM32控制助手";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.UART_tabPage.ResumeLayout(false);
            this.UART_tabPage.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.I2C_tabPage.ResumeLayout(false);
            this.I2C_tabPage.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage UART_tabPage;
        private System.Windows.Forms.Button uart_uart_control_button;
        private System.Windows.Forms.TabPage I2C_tabPage;
        private System.Windows.Forms.ComboBox Band_rate_comboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox Serial_port_comboBox;
        private System.IO.Ports.SerialPort Uart_serialPort;
        private System.Windows.Forms.RadioButton uart_send_str_radioButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.IO.Ports.SerialPort I2C_serialPort;
        private System.Windows.Forms.RadioButton uart_rec_str_radioButton;
        private System.Windows.Forms.RadioButton uart_rec_hex_radioButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button Uart_send_button;
        private System.Windows.Forms.Button Clear_send_textbox_button;
        private System.Windows.Forms.TextBox Uart_send_textBox;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton uart_send_hex_radioButton;
        private System.Windows.Forms.Button Clear_rec_textbox_button;
        private System.Windows.Forms.TextBox recive_textBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox Band_rate_I2C_comboBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox Serial_port_I2C_comboBox;
        private System.Windows.Forms.Button uart_I2C_control_button;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox I2C_recive_textBox;
        private System.Windows.Forms.Button Clear_I2C_rec_button;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton I2C_16bit_radioButton;
        private System.Windows.Forms.RadioButton I2C_8bit_radioButton;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button write_i2c_button;
        private System.Windows.Forms.Button read_i2c_button;
        private System.Windows.Forms.TextBox reg_adress_textBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox device_adress_textBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox reg_value_textBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button find_i2c_device_button;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
    }
}

