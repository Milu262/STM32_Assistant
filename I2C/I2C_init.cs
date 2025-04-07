using System;
using System.IO.Ports;
using System.Windows.Forms;

namespace STM32_Assistant
{
    public partial class Form1 : Form
    {
        public void Serial_I2C_init()
        {
            for (int i = 1; i < 21; i++)//遍历所有可能的串口  COM1~COM20
            {
                try
                {
                    I2C_serialPort.PortName = "COM" + i.ToString();
                    I2C_serialPort.Open();
                    Serial_port_I2C_comboBox.Items.Add("COM" + i.ToString());
                    I2C_serialPort.Close();
                    Serial_port_I2C_comboBox.Text = "COM" + i.ToString();
                }
                catch
                {
                    continue;
                }
            }
            I2C_8bit_radioButton.Checked = true;//默认8位
            Band_rate_I2C_comboBox.Text = "115200";//显示波特率
            device_adress_textBox.MaxLength = 2;//限制设备地址文本框输入长度
            reg_adress_textBox.MaxLength = 6;//限制寄存器地址文本框输入长度
            reg_value_textBox.MaxLength = 2;//限制寄存器值文本框输入长度
            //添加串口接收处理函数
            I2C_serialPort.DataReceived += new SerialDataReceivedEventHandler(I2C_serialPort_DataReceived);
        }
        private void I2C_serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string uart_rec_str = I2C_serialPort.ReadExisting(); // 读取串口数据
            this.Invoke(new EventHandler(delegate //防止线程报错
            {
                I2C_recive_textBox.AppendText(uart_rec_str); // 显示接收数据
            }));

        }
        //格式化设备地址文本框
        private void device_adress_textBox_TextChanged(object sender, EventArgs e)
        {
            UpdateTextBoxForHexInput(device_adress_textBox);
        }
        //格式化寄存器地址文本框
        private void reg_adress_textBox_TextChanged(object sender, EventArgs e)
        {
            UpdateTextBoxForHexInput(reg_adress_textBox);
        }
        //格式化寄存器值文本框
        private void reg_value_textBox_TextChanged(object sender, EventArgs e)
        {
            UpdateTextBoxForHexInput(reg_value_textBox);
        }

    }
}