using System;
using System.IO.Ports;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace STM32_Assistant
{
    public partial class Form1 : Form
    {
        private string uart_rec_buffer = "";

        // 用于锁定的对象
        private readonly object lockObject = new object();

        // 全局标志位，表示是否接收到完整的数据帧
        private bool dataFrameReceivedFlag;

        // 全局数组，用于存储接收到的数据
        private byte[] globalDataArray;

        // 接收到的帧头字节（根据你的协议定义）
        private const byte ReciveFrameHead = 0xAA;

        // 数据帧的最大长度（根据你的协议定义）
        private const int MaxFrameLength = 256;

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
            EEPROM_adr_textBox.Text = "A0";//显示默认设备EEPROM地址
            EEPROM_comboBox.SelectedIndex = 0;//显示默认EEPROM类型
            TxtFileWriteButton.Enabled = false;//默认禁用写入按钮
            device_adress_textBox.MaxLength = 2;//限制设备地址文本框输入长度
            reg_adress_textBox.MaxLength = 6;//限制寄存器地址文本框输入长度
            reg_value_textBox.MaxLength = 2;//限制寄存器值文本框输入长度

            // 初始化全局数组
            globalDataArray = new byte[MaxFrameLength];
            //添加串口接收处理函数
            I2C_serialPort.DataReceived += new SerialDataReceivedEventHandler(I2C_serialPort_DataReceived);
        }

        private void I2C_serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //string uart_rec_str = I2C_serialPort.ReadExisting(); // 读取串口数据
            //this.Invoke(new EventHandler(delegate //防止线程报错
            //{
            //    I2C_recive_textBox.AppendText(uart_rec_str); // 显示接收数据
            //}));
            //uart_rec_buffer = uart_rec_str;//更新接收缓冲区

            int bytesRead = I2C_serialPort.BytesToRead;
            byte[] buffer = new byte[bytesRead];
            I2C_serialPort.Read(buffer, 0, bytesRead);
            dataFrameReceivedFlag = false;
            // 检查帧头
            if (buffer.Length > 0 && buffer[0] == ReciveFrameHead)
            {
                // 加锁以确保线程安全
                lock (lockObject)
                {                          
                    // 帧头正确，处理数据
                    Array.Copy(buffer, 0, globalDataArray, 0, bytesRead);//将数据复制到全局数组中
                    // 设置标志位，表示已接收到完整的数据帧
                    dataFrameReceivedFlag = true;
                }
            }
            else
            {
                //string uart_rec_str = I2C_serialPort.ReadExisting(); // 读取串口数据
                string uart_rec_str = System.Text.Encoding.Default.GetString(buffer);
                this.Invoke(new EventHandler(delegate //防止线程报错
                {
                    I2C_recive_textBox.AppendText(uart_rec_str); // 显示接收数据
                }));
                uart_rec_buffer = uart_rec_str;//更新接收缓冲区
            }
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