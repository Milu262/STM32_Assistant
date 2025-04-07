using System;
using System.Windows.Forms;

namespace STM32_Assistant
{
    public partial class Form1 : Form
    {
        //串口控制按钮
        private void uart_I2C_control_button_Click(object sender, EventArgs e)
        {
            if (uart_I2C_control_button.Text == "打开串口")
            {
                try
                {
                    I2C_serialPort.PortName = Serial_port_comboBox.Text;
                    I2C_serialPort.BaudRate = Convert.ToInt32(Band_rate_comboBox.Text);
                    I2C_serialPort.Open();
                    uart_I2C_control_button.Text = "关闭串口";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                try
                {
                    I2C_serialPort.Close();
                    uart_I2C_control_button.Text = "打开串口";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //清除I2C接收区
        private void Clear_I2C_rec_button_Click(object sender, EventArgs e)
        {
            I2C_recive_textBox.Clear();
        }
        //读I2C按钮函数
        private void read_i2c_button_Click(object sender, EventArgs e)
        {
            byte[] send_data = new byte[7];
            send_data[0] = Frame_header;//固定帧头
            if (!I2C_serialPort.IsOpen)//串口没有打开
            {
                MessageBox.Show("串口未打开", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                uart_I2C_control_button.Text = "打开串口";
                return;
            }
            //判断输入是否为空
            if (device_adress_textBox.Text == "" || reg_adress_textBox.Text == "")
            {
                MessageBox.Show("输入不能为空", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            send_data[2] = 0x03;//读一个字节模式
            if (device_adress_textBox.Text.Length == 1)//设备地址为1位时，前面补0
            {
                device_adress_textBox.Text = "0" + device_adress_textBox.Text;
            }
            send_data[3] = Convert.ToByte(device_adress_textBox.Text, 16);//设备地址
            send_data[4] = 0x01;//读写一个字节

            if (I2C_8bit_radioButton.Checked)//8位寄存器地址模式
            {
                if (reg_adress_textBox.Text.Length > 2)
                {
                    MessageBox.Show("寄存器地址长度大于2，请选择I2C_16bit", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                send_data[1] = 0x02;//8bit寄存器地址模式
                if(reg_adress_textBox.Text.Length== 1)//寄存器地址为1位时，前面补0
                {
                    reg_adress_textBox.Text = "0" + reg_adress_textBox.Text;
                }
                send_data[5] = Convert.ToByte(reg_adress_textBox.Text, 16);//寄存器地址
                try
                {
                    I2C_serialPort.Write(send_data, 0, 6);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else//16位寄存器地址模式
            {
                send_data[1] = 0x03;//16bit寄存器地址模式
                if(reg_adress_textBox.Text.Length == 1)//寄存器地址为1位时，前面补0
                {
                    reg_adress_textBox.Text = "00 0" + reg_adress_textBox.Text;
                }
                if (reg_adress_textBox.Text.Length == 4)//寄存器地址为4位时，第4为前面加一个补0
                {
                    reg_adress_textBox.Text = reg_adress_textBox.Text.Insert(3, "0");
                }
                send_data[5] = Convert.ToByte(reg_adress_textBox.Text.Substring(0, 2), 16);//寄存器地址高字节
                send_data[6] = Convert.ToByte(reg_adress_textBox.Text.Substring(3, 2), 16);//寄存器地址低字节
                try
                {
                    I2C_serialPort.Write(send_data, 0, 7);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //写I2C按钮函数
        private void write_i2c_button_Click(object sender, EventArgs e)
        {
            byte[] send_data = new byte[8];
            send_data[0] = Frame_header;//固定帧头
            if (!I2C_serialPort.IsOpen)//串口没有打开
            {
                MessageBox.Show("串口未打开", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                uart_I2C_control_button.Text = "打开串口";
                return;
            }
            //判断输入是否为空
            if (device_adress_textBox.Text == "" || reg_adress_textBox.Text == "" || reg_value_textBox.Text == "")
            {
                MessageBox.Show("输入不能为空", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            send_data[2] = 0x01;//写一个字节模式
            if (device_adress_textBox.Text.Length == 1)//设备地址为1位时，前面补0
            {
                device_adress_textBox.Text = "0" + device_adress_textBox.Text;
            }
            send_data[3] = Convert.ToByte(device_adress_textBox.Text, 16);//设备地址
            send_data[4] = 0x01;//读写一个字节
            if (I2C_8bit_radioButton.Checked)//8位寄存器地址模式
            {
                if (reg_adress_textBox.Text.Length > 2)
                {
                    MessageBox.Show("寄存器地址长度大于2，请选择I2C_16bit", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                send_data[1] = 0x02;//8位寄存器地址模式
                if (reg_adress_textBox.Text.Length == 1)//寄存器地址为1位时，前面补0
                {
                    reg_adress_textBox.Text = "0" + reg_adress_textBox.Text;
                }
                if (reg_value_textBox.Text.Length == 1)//寄存器值为1位时，前面补0
                {
                    reg_value_textBox.Text = "0" + reg_value_textBox.Text;
                }
                send_data[5] = Convert.ToByte(reg_adress_textBox.Text, 16);//寄存器地址
                send_data[6] = Convert.ToByte(reg_value_textBox.Text, 16);//寄存器值
                try
                {
                    I2C_serialPort.Write(send_data, 0, 7);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else//16位寄存器地址模式
            {
                send_data[1] = 0x03;//16bit寄存器地址模式
                if (reg_adress_textBox.Text.Length == 1)//寄存器地址为1位时，前面补0
                {
                    reg_adress_textBox.Text = "00 0" + reg_adress_textBox.Text;
                }
                if (reg_adress_textBox.Text.Length == 4)//寄存器地址为4位时，第4为前面加一个补0
                {
                    reg_adress_textBox.Text = reg_adress_textBox.Text.Insert(3, "0");
                }
                if (reg_value_textBox.Text.Length == 1)//寄存器值为1位时，前面补0
                {
                    reg_value_textBox.Text = "0" + reg_value_textBox.Text;
                }
                send_data[5] = Convert.ToByte(reg_adress_textBox.Text.Substring(0, 2), 16);//寄存器地址高字节
                send_data[6] = Convert.ToByte(reg_adress_textBox.Text.Substring(3, 2), 16);//寄存器地址低字节
                send_data[7] = Convert.ToByte(reg_value_textBox.Text, 16);//寄存器值
                try
                {
                    I2C_serialPort.Write(send_data, 0, 8);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        //查找I2C总线上的设备
        private void find_i2c_device_button_Click(object sender, EventArgs e)
        {
            byte[] send_data = new byte[3];
            send_data[0] = Frame_header;//固定帧头
            if (!I2C_serialPort.IsOpen)//串口没有打开
            {
                MessageBox.Show("串口未打开", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                uart_I2C_control_button.Text = "打开串口";
                return;
            }
            send_data[1] = 0x02;//8bit寄存器地址模式
            send_data[2] = 0x05;//查找I2C总线上的设备
            try
            {
                I2C_serialPort.Write(send_data, 0, 3);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
