using System;
using System.IO.Ports;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace STM32_Assistant
{
    public partial class Form1 : Form
    {
        public void Serial_port_init()//串口初始化
        {
            
            for(int i = 0; i < 21; i++)
            {
                try
                {
                    Uart_serialPort.PortName = "COM" + i.ToString();
                    Uart_serialPort.Open();
                    Serial_port_comboBox.Items.Add("COM" + i.ToString());
                    Uart_serialPort.Close();
                    Serial_port_comboBox.Text = "COM" + i.ToString();
                }
                catch
                {
                    continue;
                }
            }

            Band_rate_comboBox.Text = "115200";//显示波特率
            uart_send_hex_radioButton.Checked = true;//发送默认为16进制
            uart_rec_hex_radioButton.Checked = true;//接收默认为16进制

            //添加串口接收处理函数
            Uart_serialPort.DataReceived += new SerialDataReceivedEventHandler(Uart_serialPort_DataReceived);
        }

        //串口接收处理函数

        private void Uart_serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            this.Invoke(new EventHandler(delegate
            {
                if (!uart_rec_hex_radioButton.Checked) // 接收为字符模式
                {
                    string uart_rec_str = Uart_serialPort.ReadExisting(); // 读取串口数据
                    recive_textBox.AppendText(uart_rec_str); // 显示接收数据
                }
                else // 接收为16进制模式
                {
                    byte[] buffer = new byte[Uart_serialPort.BytesToRead]; // 接收字节数组
                    Uart_serialPort.Read(buffer, 0, Uart_serialPort.BytesToRead); // 读取串口数据
                    for (int i = 0; i < buffer.Length; i++)
                    {
                        string data = Convert.ToString(buffer[i], 16).ToUpper();
                        recive_textBox.AppendText("0X" + (data.Length == 1 ? "0" + data : data) + " ");
                    }
                    recive_textBox.AppendText(Environment.NewLine); // 换行
                }
            }));
        }


    }
}