using System;
using System.IO.Ports;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Drawing;
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

                    //如果接收到的数据是用[]包裹的，则改变包裹内容的颜色为蓝色
                    if (uart_rec_str.Contains("[") && uart_rec_str.Contains("]"))
                    {
                        // 保存当前选择位置和长度
                        int originalStart = uiRichTextBox1.SelectionStart;
                        int originalLength = uiRichTextBox1.SelectionLength;

                        // 追加文本
                        uiRichTextBox1.AppendText(uart_rec_str);

                        // 获取当前文本内容
                        string currentText = uiRichTextBox1.Text;

                        // 查找所有方括号对
                        int startIndex = 0;
                        while (startIndex < currentText.Length)
                        {
                            int openBracket = currentText.IndexOf('[', startIndex);
                            if (openBracket == -1) break;

                            int closeBracket = currentText.IndexOf(']', openBracket + 1);
                            if (closeBracket == -1 || closeBracket <= openBracket) break;

                            // 选择并设置颜色
                            uiRichTextBox1.Select(openBracket + 1, closeBracket - openBracket - 1);
                            uiRichTextBox1.SelectionColor = Color.Blue;

                            // 更新搜索位置
                            startIndex = closeBracket + 1;
                        }

                        // 恢复原始选择
                        uiRichTextBox1.SelectionStart = originalStart;
                        uiRichTextBox1.SelectionLength = originalLength;
                    }
                    else
                    {
                        uiRichTextBox1.AppendText(uart_rec_str);
                    }
                }
                else // 接收为16进制模式
                {
                    byte[] buffer = new byte[Uart_serialPort.BytesToRead]; // 接收字节数组
                    Uart_serialPort.Read(buffer, 0, Uart_serialPort.BytesToRead); // 读取串口数据
                    for (int i = 0; i < buffer.Length; i++)
                    {
                        string data = Convert.ToString(buffer[i], 16).ToUpper();
                        uiRichTextBox1.AppendText("0X" + (data.Length == 1 ? "0" + data : data) + " ");
                    }
                    uiRichTextBox1.AppendText(Environment.NewLine); // 换行
                }
            }));
        }



    }
}