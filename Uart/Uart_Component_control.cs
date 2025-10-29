using System;
using System.Text.RegularExpressions;
using System.Text;
using System.Windows.Forms;

namespace STM32_Assistant
{
    public partial class Form1 : Form
    {
        private void uart_uart_control_button_Click(object sender, EventArgs e)//串口控制按钮
        {
            if (uart_uart_control_button.Text == "打开串口")
            {
                try
                {
                    Uart_serialPort.PortName = Serial_port_comboBox.Text;
                    Uart_serialPort.BaudRate = Convert.ToInt32(Band_rate_comboBox.Text);
                    Uart_serialPort.Open();
                    uart_uart_control_button.Text = "关闭串口";
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
                    Uart_serialPort.Close();
                    uart_uart_control_button.Text = "打开串口";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        //当发送HEX时，自动格式化
        //private void Uart_send_textBox_TextChanged(object sender, EventArgs e)
        //{
        //    if(uart_send_hex_radioButton.Checked)
        //    {
        //        UpdateTextBoxForHexInput(Uart_send_textBox);
        //    }
        //}
        //当接收HEX时，自动格式化
        private void recive_textBox_TextChanged(object sender, EventArgs e)
        {
            if (uart_rec_hex_radioButton.Checked)
            {
                UpdateTextBoxForHexInput(uiRichTextBox1);
            }
        }
        //清除发送区
        private void Clear_send_textbox_button_Click(object sender, EventArgs e)
        {
            Uart_send_textBox.Clear();
        }
        //清除接收区
        private void Clear_rec_textbox_button_Click(object sender, EventArgs e)
        {
            uiRichTextBox1.Clear();
        }

        //发送区HEX文本框格式化
        public void UpdateTextBoxForHexInput(dynamic textBox)
        {
            // 获取当前 TextBox 的文本
            string input = textBox.Text;

            // 使用正则表达式移除非十六进制字符
            string hexOnly = Regex.Replace(input, "[^0-9A-Fa-f]", "");

            // 创建一个新的 StringBuilder 对象来构建格式化后的字符串
            StringBuilder formatted = new StringBuilder();

            // 遍历十六进制字符串，按字节分割
            for (int i = 0; i < hexOnly.Length; i += 2)
            {
                // 检查剩余字符数量
                int byteLength = (hexOnly.Length - i) >= 2 ? 2 : 1;

                // 添加当前字节到格式化字符串
                formatted.Append(hexOnly.Substring(i, byteLength));

                // 如果不是最后一个字节，添加一个空格作为分隔符
                if (i + byteLength < hexOnly.Length)
                {
                    formatted.Append(" ");
                }
            }

            // 更新 TextBox 的文本
            textBox.Text = formatted.ToString().ToUpper();

            // 设置光标位置到文本末尾
            textBox.SelectionStart = textBox.Text.Length;
        }

        //串口发送函数
        private void Uart_send_button_Click(object sender, EventArgs e)
        {
            if (!Uart_serialPort.IsOpen)//串口未打开
            {
                MessageBox.Show("串口未打开", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!uart_send_hex_radioButton.Checked)//发送文本
            {
                try
                {
                    Uart_serialPort.Write(Uart_send_textBox.Text);
                }
                catch (Exception ex)
                {
                    Uart_serialPort.Close();//关闭串口
                    uart_uart_control_button.Text = "打开串口";
                    MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else//发送十六进制
            {
                try
                {
                    byte[] data = new byte[(Uart_send_textBox.Text.Length / 3) + 1];//发送字节数组
                    for (int i = 0; i < data.Length; i++)
                    {
                        // 检查剩余字符数量
                        int byteLength = (Uart_send_textBox.Text.Length - i * 3) >= 2 ? 2 : 1;
                        data[i] = Convert.ToByte(Uart_send_textBox.Text.Substring(i * 3, byteLength), 16);//将十六进制字符串转换为字节数
                    }
                    Uart_serialPort.Write(data, 0, data.Length);//发送字节数组
                }
                catch (Exception ex)
                {
                    Uart_serialPort.Close();//关闭串口
                    uart_uart_control_button.Text = "打开串口";
                    MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


        }
    }
}