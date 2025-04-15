using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace STM32_Assistant
{
    public partial class Form1 : Form
    {
        // 创建一个不确定行数的二维数组（列表的列表）
        private List<List<byte>> File_content = new List<List<byte>>();
        private void Open_EPPROM_program_button_Click(object sender, EventArgs e)
        {
            fileContentTextBox.Clear();//清空文本框
            File_content.Clear();                    //清空数组
            OpenFileDialog openFileDialog = new OpenFileDialog();//创建一个OpenFileDialog对象
            openFileDialog.Filter = "BIN文件|*.bin|文本文件|*.txt|所有文件|*.*";//设置文件过滤器
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
                    TxT_File_Processs(filePath);//处理txt文件
                }
                else if (fileExtension.Equals("bin", StringComparison.OrdinalIgnoreCase))
                {
                    Bin_File_Processs(filePath);// 处理bin文件
                }
                else
                {
                    // 处理其他类型的文件或给出提示
                    MessageBox.Show("选中的文件类型不是TXT或BIN文件: " + filePath);
                }
            }
        }

        // 检查字符串是否为有效的十六进制数
        private bool IsValidHexNumber(string input)
        {
            // 正则表达式匹配十六进制数模式（0-9, a-f, A-F）
            string hexPattern = @"^[0-9a-fA-F]+$";
            return Regex.IsMatch(input, hexPattern);
        }

        private void Bin_File_Processs(string FilePath)
        {
            //处理bin文件
            try
            {
                using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
                {
                    byte[] buffer = new byte[16];
                    int bytesRead;
                    while ((bytesRead = fs.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        // 如果读取的字节数少于16，说明是文件的最后一行，需要截断数组
                        if (bytesRead < 16)
                        {
                            byte[] tempBuffer = new byte[bytesRead]; // 创建一个临时数组来存储实际读取的字节数
                            Array.Copy(buffer, tempBuffer, bytesRead); // 将读取的字节复制到临时数组中
                            // 将临时数组转换为List<byte>并添加到fileContent中
                            File_content.Add(tempBuffer.ToList());
                        }
                        else
                        {
                            File_content.Add(buffer.ToList());
                        }
                    }
                }
                // 显示文件内容
                StringBuilder sb = new StringBuilder();
                foreach (var line in File_content)
                {
                    // 将List<byte>转换为十六进制字符串并添加到StringBuilder中
                    sb.AppendLine(string.Join(" ", line.Select(b => b.ToString("X2"))));
                }
                fileContentTextBox.Text = sb.ToString();// 将StringBuilder的内容一次性设置到TextBox中
            }
            catch (Exception ex)
            {
                MessageBox.Show("读取文件时出错: " + ex.Message);
            }
        }

        private void TxT_File_Processs(string FilePath)
        {
            // 处理txt文件
            try
            {
                // 使用 StreamReader 逐行读取文件
                using (StreamReader reader = new StreamReader(FilePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        // 去除行尾的分号（如果有）并检查是否为空行
                        line = line?.TrimEnd(';').Trim();

                        // 检查是否为空行
                        if (string.IsNullOrWhiteSpace(line))
                        {
                            continue; // 跳过空行
                        }

                        // 按逗号分割字符串
                        string[] parts = line.Split(',');
                        if ((parts.Length == 2) && IsValidHexNumber(parts[0].Substring(2)) && IsValidHexNumber(parts[1].Substring(2)))
                        {
                            if ((parts[0].Length == 4) && (parts[1].Length == 4))//检查地址和值是否为两位十六进制数
                            {
                                // 解析寄存器地址和值
                                byte address = Convert.ToByte(parts[0].TrimStart('0', 'x', 'X'), 16);
                                byte value = Convert.ToByte(parts[1].TrimStart('0', 'x', 'X'), 16);

                                // 将地址和值添加到列表中
                                File_content.Add(new List<byte> { address, value });
                            }
                            else if ((parts[0].Length == 6) && (parts[1].Length == 4))//检查地址是否为四位十六进制数
                            {
                                byte addressHigh = Convert.ToByte(parts[0].Substring(2, 2), 16);
                                byte addressLow = Convert.ToByte(parts[0].Substring(4, 2), 16);
                                byte value = Convert.ToByte(parts[1].TrimStart('0', 'x', 'X'), 16);
                                // 将地址和值添加到列表中
                                File_content.Add(new List<byte> { addressHigh, addressLow, value });
                            }
                            else
                            {
                                MessageBox.Show("文件中的格式错误: " + line);
                            }
                        }
                        else
                        {
                            // 显示错误的数据
                            MessageBox.Show("文件中包含无效的十六进制数或格式错误: " + line);
                        }
                    }

                    //测试数组结果是否正确
                    foreach (var entry in File_content)
                    {
                        int Count = entry.Count;
                        string addressHex;
                        string valueHex;
                        if (Count == 2)
                        {
                            // 将字节转换为十六进制字符串，并保留两位十六进制数（如果需要的话，可以使用X2格式化字符串）
                            addressHex = entry[0].ToString("X2");
                            valueHex = entry[1].ToString("X2");
                        }
                        else if (Count == 3)
                        {
                            addressHex = entry[0].ToString("X2") + entry[1].ToString("X2");
                            valueHex = entry[2].ToString("X2");
                        }
                        else
                        {
                            MessageBox.Show("文件中的格式错误: " + line);
                            continue;
                        }
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
    }
}