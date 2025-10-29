using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace STM32_Assistant
{
    public partial class Form1 : Form
    {
        private string uart_rec_buffer = "";

        // 用于锁定的对象
        private readonly object lockObject = new object();

        // 全局标志位，表示是否接收到完整的数据帧
        private volatile bool dataFrameReceivedFlag;

        // 全局数组，用于存储接收到的数据
        private byte[] globalDataArray;

        // 接收到的帧头字节（根据你的协议定义）
        private const byte ReciveFrameHead = 0xFA;

        // 数据帧的最大长度（根据你的协议定义）
        private const int MaxFrameLength = 256+10;

        // 全局缓冲区：累积当前数据段的所有字节
        private readonly List<byte> _currentDataSegment = new List<byte>();
        // 空闲超时定时器：50ms无数据则触发处理
        private readonly System.Timers.Timer _idleTimer = new System.Timers.Timer(50);
        // 定时器线程安全锁
        private readonly object _timerLock = new object();

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
            I2C_serialPort.DataReceived += I2C_serialPort_DataReceivedAsync;

            // 初始化空闲超时定时器
            _idleTimer.AutoReset = false; // 只触发一次
            _idleTimer.Elapsed += IdleTimer_Elapsed; // 绑定超时事件
        }

        private void IdleTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            // 加锁取出当前数据段并清空缓冲区
            byte[] dataSegment;
            lock (lockObject)
            {
                dataSegment = _currentDataSegment.ToArray();
                _currentDataSegment.Clear(); // 清空缓冲区，准备下一段数据
            }

            if (dataSegment.Length == 0) return; // 空数据段不处理

            // 判断数据段类型（仅根据第一个字节）
            if (dataSegment[0] == ReciveFrameHead)
            {
                // 处理特殊帧（需满足最小长度）
                if (dataSegment.Length <= MaxFrameLength)
                {
                    lock (lockObject)
                    {
                        Array.Copy(dataSegment, 0, globalDataArray, 0, MaxFrameLength);
                        dataFrameReceivedFlag = true; // 标记帧接收完成
                    }
                }
                else
                {
                    // 特殊帧长度不足，作为异常debug信息显示（可选）
                    string errorMsg = $"不完整的特殊帧（长度：{dataSegment.Length}）：{BitConverter.ToString(dataSegment)}";
                    ShowDebugData(Encoding.Default.GetBytes(errorMsg));
                }
            }
            else
            {
                // 处理debug信息（直接转换为字符串）
                ShowDebugData(dataSegment);
            }
        }

        // 显示debug信息到文本框（封装跨线程调用）
        private void ShowDebugData(byte[] debugData)
        {
            string debugStr = Encoding.Default.GetString(debugData);
            this.Invoke(new Action(() =>
            {
                I2C_recive_textBox.AppendText(debugStr);
            }));
        }

        //private void I2C_serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        //{
        //    List<byte> receivedData = new List<byte>();
        //    int bytesRead;

        //    try
        //    {
        //        do
        //        {
        //            bytesRead = I2C_serialPort.BytesToRead;
        //            byte[] tempBuffer = new byte[bytesRead];

        //            if (bytesRead == 0)
        //            {
        //                break;
        //            }

        //            // 同步读取串口数据
        //            I2C_serialPort.Read(tempBuffer, 0, bytesRead);
        //            receivedData.AddRange(tempBuffer);

        //        } while (bytesRead > 0);
        //    }
        //    catch (Exception ex)
        //    {
        //        // 处理异常
        //        MessageBox.Show("读取串口数据时发生错误：" + ex.Message);
        //    }

        //    byte[] buffer = receivedData.ToArray();
        //    //控制台打印出该数组内容
        //    Console.WriteLine("一开始的Buffer值为：\n");
        //    // 遍历数组，每行输出16个16进制数
        //    for (int i = 0; i < buffer.Length; i++)
        //    {
        //        // 格式化当前元素为两位16进制（大写），不足两位补0
        //        string hex = buffer[i].ToString("X2");

        //        // 输出当前元素，末尾加逗号（最后一个元素除外）
        //        if (i % 16 != 15 && i != buffer.Length - 1)
        //        {
        //            Console.Write($"{hex}, ");
        //        }
        //        // 每行第16个元素或数组最后一个元素，输出后换行
        //        else
        //        {
        //            Console.WriteLine(hex);
        //        }
        //    }

        //    // 检查帧头
        //    if (buffer.Length > 0 && buffer[0] == ReciveFrameHead)
        //    {
        //        // 加锁以确保线程安全
        //        lock (lockObject)
        //        {
        //            // 帧头正确，处理数据
        //            Array.Copy(buffer, 0, globalDataArray, 0, buffer.Length); // 将数据复制到全局数组中
        //                                                                      // 设置标志位，表示已接收到完整的数据帧
        //            dataFrameReceivedFlag = true;
        //        }
        //    }
        //    else
        //    {
        //        string uart_rec_str = System.Text.Encoding.Default.GetString(buffer);
        //        Console.WriteLine("进入检查不正常状态\n");
        //        Console.WriteLine("Buffer值为：\n");

        //        for (int i = 0; i < buffer.Length; i++)
        //        {
        //            // 格式化当前元素为两位16进制（大写），不足两位补0
        //            string hex = buffer[i].ToString("X2");

        //            // 输出当前元素，末尾加逗号（最后一个元素除外）
        //            if (i % 16 != 15 && i != buffer.Length - 1)
        //            {
        //                Console.Write($"{hex}, ");
        //            }
        //            // 每行第16个元素或数组最后一个元素，输出后换行
        //            else
        //            {
        //                Console.WriteLine(hex);
        //            }
        //        }

        //        // 使用Invoke确保在UI线程上更新TextBox
        //        if (this.InvokeRequired)
        //        {
        //            this.Invoke(new Action(() =>
        //            {
        //                I2C_recive_textBox.AppendText(uart_rec_str); // 显示接收数据
        //            }));
        //        }
        //        else
        //        {
        //            I2C_recive_textBox.AppendText(uart_rec_str);
        //        }

        //        uart_rec_buffer = uart_rec_str; // 更新接收缓冲区
        //    }
        //}
        private async void I2C_serialPort_DataReceivedAsync(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                // 读取当前可用数据
                int bytesRead;
                List<byte> receivedData = new List<byte>();
                do
                {
                    bytesRead = I2C_serialPort.BytesToRead;
                    if (bytesRead == 0) break;
                    byte[] tempBuffer = new byte[bytesRead];
                    await I2C_serialPort.BaseStream.ReadAsync(tempBuffer, 0, bytesRead);
                    receivedData.AddRange(tempBuffer);
                } while (bytesRead > 0);

                if (receivedData.Count == 0) return;

                // 加锁累积数据到当前数据段
                lock (lockObject)
                {
                    _currentDataSegment.AddRange(receivedData);
                }

                // 重置空闲定时器（每次收到数据后重新计时）
                lock (_timerLock)
                {
                    _idleTimer.Stop(); // 停止当前计时
                    _idleTimer.Start(); // 重新开始计时（50ms后触发）
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("读取串口数据时发生错误：" + ex.Message);
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