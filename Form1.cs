using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Text.RegularExpressions;
namespace STM32_Assistant
{
    public partial class Form1 : Form
    {
        readonly byte Frame_header = 0x55;//固定帧头
        // 创建一个不确定行数但列数为2的二维数组（列表的列表）
        List<List<byte>> File_content = new List<List<byte>>();
        //创建一个不确定长度的字节数组
        List<byte> byteList = new List<byte>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Serial_port_init();//串口模式的串口初始化
            Serial_I2C_init();//I2C模式的串口初始化
            //Control.CheckForIllegalCrossThreadCalls = false;//取消跨线程检查,不推荐使用这种方法
        }

        // 检查字符串是否为有效的十六进制数
        private bool IsValidHexNumber(string input)
        {
            // 正则表达式匹配十六进制数模式（0-9, a-f, A-F）
            string hexPattern = @"^[0-9a-fA-F]+$";
            return Regex.IsMatch(input, hexPattern);
        }


        private void Open_EPPROM_program_button_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();//创建一个OpenFileDialog对象
            openFileDialog.Filter = "HEX文件|*.hex|BIN文件|*.bin|文本文件|*.txt|所有文件|*.*";//设置文件过滤器
            if (openFileDialog.ShowDialog() == DialogResult.OK)//如果用户点击了确定按钮
            {
                string filePath = openFileDialog.FileName;//获取用户选择的文件路径
                //将文件路径显示在文本框中
                EPPROM_program_file_path_textBox.Text = filePath;
                // 获取文件扩展名（不带点）
                string fileExtension = System.IO.Path.GetExtension(filePath).TrimStart('.');

                // 判断文件类型并分别处理
                if (fileExtension.Equals("txt", StringComparison.OrdinalIgnoreCase))
                {
                    // 处理txt文件
                    try
                    {
                        // 使用 StreamReader 逐行读取文件
                        using (StreamReader reader = new StreamReader(filePath))
                        {
                            string line;
                            while ((line = reader.ReadLine()) != null)
                            {
                                // 去除行尾的分号（如果有）
                                if (line.EndsWith(";"))
                                {
                                    line = line.TrimEnd(';');
                                }

                                // 检查是否为空行
                                if (string.IsNullOrWhiteSpace(line))
                                {
                                    continue; // 跳过空行
                                }

                                // 按逗号分割字符串
                                string[] parts = line.Split(',');
                                if (parts.Length == 2)
                                {
                                    // 去除字符串前后的空格
                                    string address = parts[0].Trim();
                                    string value = parts[1].Trim();

                                    //去除0x前缀
                                    address = address.Substring(2);
                                    value = value.Substring(2);
                                    // 检查地址和值是否为有效的十六进制数
                                    if (IsValidHexNumber(address) && IsValidHexNumber(value))
                                    {
                                        // 在这里处理解析出的寄存器地址和值
                                        // 例如，将它们添加到某个数据结构中，或者显示到界面上
                                        File_content.Add(new List<byte> { Convert.ToByte(address, 16), Convert.ToByte(value, 16) });//将地址和值添加到列表中
                                    }
                                    else
                                    {
                                        //显示错误的数据
                                        MessageBox.Show("文件中包含无效的十六进制数: " + line);
                                    }

                                }
                            }
                            //测试数组结果是否正确
                            foreach (var entry in File_content)
                            {
                                // 将字节转换为十六进制字符串，并保留两位十六进制数（如果需要的话，可以使用X2格式化字符串）
                                string addressHex = entry[0].ToString("X2");
                                string valueHex = entry[1].ToString("X2");

                                // 格式化字符串并追加到文本框中
                                fileContentTextBox.Text += $"寄存器地址: {addressHex}, 寄存器值: {valueHex}";
                                fileContentTextBox.Text += Environment.NewLine;
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("读取文件时出错: " + ex.Message);
                    }
                }
                else if (fileExtension.Equals("bin", StringComparison.OrdinalIgnoreCase))
                {
                    // 处理bin文件
                    // 在这里添加处理bin文件的代码
                    //将bin文件的内容存到数组中

                }
                else
                {
                    // 处理其他类型的文件或给出提示
                    MessageBox.Show("选中的文件类型不是TXT或BIN文件: " + filePath);
                }


                

                // 显示文件内容
                //fileContentTextBox.Text = fileContent;
            }
        }
    }
}
