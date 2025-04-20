using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace STM32_Assistant
{
    public partial class Form1 : Form
    {

        /// <summary>
        /// 线程安全的进度更新（UI线程保护[10](@ref)）
        /// </summary>
        private void UpdateProgress(int current, int total)
        {
            float percent = (current * 1000f) / total; // 
            if (EEPROMuiProcessBar.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    EEPROMuiProcessBar.Value = (int)percent;
                });
            }
            else
            {
                EEPROMuiProcessBar.Value = (int)percent;
            }
        }
        /// <summary>
        /// 执行带延时的安全写入（异步优化[9,10](@ref)）
        /// </summary>
        private async Task  SafeSerialWrite(byte[] data, int length)
        {
            await I2C_serialPort.BaseStream.WriteAsync(data, 0, length);
            await Task.Delay(10); // 动态延时[3](@ref)
        }
        /// <summary>
        /// 发送EEPROM内容到串口
        /// </summary>
        /// <param name="UartSendData">要发送的数据缓冲区</param>
        /// <param name="PageSize">EEPROM的页大小</param>
        /// <param name="Row">需要写入的行数</param>
        /// <returns>如果发送成功返回true，否则返回false</returns>
        private async Task<bool> SendEEPROM_Content(byte[] UartSendData, int PageSize, int Row)

        {
            int File_content_Row = File_content.Count;//获取文件内容的行数
            if (File_content_Row < Row)//如果文件内容的行数小于要发送的行数，弹出一个提示框，让用户选择是否继续
            {
                DialogResult result = MessageBox.Show($"文件内容的行数{File_content_Row}小于要发送的行数{Row}，是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    return true; // 用户选择不继续，直接返回
                }
                // 如果用户选择继续，则继续执行后续的代码
            }
            else if (File_content_Row > Row)//如果文件内容的行数大于要发送的行数，弹出一个提示框，让用户选择是否继续
            {
                DialogResult result = MessageBox.Show($"文件内容的行数{File_content_Row}大于要发送的行数{Row}，是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    return true; // 用户选择不继续，直接返回
                }
            }
            //当前行数
            int CurrentRow = 0;

            try
            {
                if (PageSize == 8)
                {
                    foreach (var entry in File_content)//遍历二维数组，File_content二维数组是读取到的bin文件每行最多16个字节
                    {
                        CurrentRow++;//行数加1
                        if (CurrentRow == File_content_Row)//如果当前行数等于文件内容的行数，则表示已经到达文件的最后一行
                        {
                            //判断最后行数量是否大于8，如果大于8，则每次写入8个字节，否则写入剩余的字节
                            if (entry.Count > PageSize)
                            {
                                Array.Copy(entry.ToArray(), 0, UartSendData, 6, 8);//将entry数组的前8个元素复制到UartSendData数组的第5个元素开始的位置
                                await SafeSerialWrite(UartSendData, 14);//发送数据
                                //延时10ms
                                                                  //处理剩余的数据
                                UartSendData[5] += 8;//页内偏移地址加8
                                Array.Copy(entry.ToArray(), 8, UartSendData, 6, entry.Count - 8);//将entry数组的
                                await SafeSerialWrite(UartSendData, entry.Count - 2);//发送数据
                                //延时10ms，EEPROM按页写入需要时间，等待EEPROM写入完成
                                UartSendData[5] += 8;//页内偏移地址加8
                            }
                            else
                            {
                                Array.Copy(entry.ToArray(), 0, UartSendData, 6, entry.Count);//将entry数组的
                                await SafeSerialWrite(UartSendData, entry.Count + 6);//发送数据
                            }
                        }
                        else //如果当前行数不等于文件内容的行数，则每行有16个字节
                        {
                            Array.Copy(entry.ToArray(), 0, UartSendData, 6, 8);//将entry数组的前8个元素复制到UartSendData数组的第5个元素开始的位置
                            await SafeSerialWrite(UartSendData, 14);//发送数据
                            //延时10ms
                            UartSendData[5] += 8;//页内偏移地址加8
                            Array.Copy(entry.ToArray(), 8, UartSendData, 6, 8);//将entry数组的后8个元素复制到UartSendData数组的第5个元素开始的位置
                            await SafeSerialWrite(UartSendData, 14);//发送数据
                            //延时10ms，EEPROM按页写入需要时间，等待EEPROM写入完成
                            UartSendData[5] += 8;//页内偏移地址加8
                        }
                        UpdateProgress(CurrentRow,Row);//更新进度条
                        if (CurrentRow == Row)//如果当前行数等于需要写入的行数，则跳出循环
                            break;
                    }
                }
                else if (PageSize == 16)
                {
                    foreach (var entry in File_content)//遍历二维数组，File_content二维数组是读取到的bin文件每行最多16个字节
                    {
                        CurrentRow++;//行数加1
                        if (CurrentRow == File_content_Row)//如果当前行数等于文件内容的行数，则表示已经到达文件的最后一行
                        {
                            Array.Copy(entry.ToArray(), 0, UartSendData, 6, entry.Count);//将entry数组的
                            await SafeSerialWrite(UartSendData, entry.Count + 6);//发送数据
                        }
                        else if ((CurrentRow % 16 == 1) && (CurrentRow != 1))
                        {
                            UartSendData[3] += 2;//设备地址+2
                            UartSendData[5] = 0;//页内偏移地址清零
                            Array.Copy(entry.ToArray(), 0, UartSendData, 6, 16);//将entry数组的数组的前16个元素复制到UartSendData数组的第6个元素开始的位置
                            await SafeSerialWrite(UartSendData, 22);//发送数据
                            //延时10ms
                            UartSendData[5] += 16;//页内偏移地址加16
                        }
                        else
                        {
                            Array.Copy(entry.ToArray(), 0, UartSendData, 6, 16);//将entry数组的数组的前16个元素复制到UartSendData数组的第6个元素开始的位置
                            await SafeSerialWrite(UartSendData, 22);//发送数据
                            //延时10ms
                            UartSendData[5] += 16;//页内偏移地址加16
                        }
                        UpdateProgress(CurrentRow,Row);//更新进度条
                        if (CurrentRow == Row)//如果当前行数等于需要写入的行数，则跳出循环
                            break;
                    }
                }
                else if (PageSize == 32)
                {
                    ushort Adress = 0x0000;//EEPROM的页内偏移地址
                    UartSendData[5] = (byte)(Adress >> 8);//EEPROM的页内偏移地址的页内偏移地址高8位
                    UartSendData[6] = (byte)(Adress & 0x00FF);//EEPROM的页内偏移地址的页内偏移地址低8位
                    foreach (var entry in File_content)//遍历二维数组，File_content二维数组是读取到的bin文件每行最多16个字节
                    {
                        CurrentRow++;//行数加1
                        if (CurrentRow == File_content_Row && CurrentRow % 2 == 1)//如果当前行数等于文件内容的行数，则表示已经到达文件的最后一行
                        {
                            Array.Copy(entry.ToArray(), 0, UartSendData, 7, entry.Count);//将entry数组复制到UartSendData数组的第7个元素开始的位置
                            await SafeSerialWrite(UartSendData, entry.Count + 7);//发送数据
                        }
                        else if (CurrentRow == File_content_Row && CurrentRow % 2 == 0)
                        {
                            Array.Copy(entry.ToArray(), 0, UartSendData, 23, entry.Count);//
                            await SafeSerialWrite(UartSendData, entry.Count + 22);//发送数据
                        }
                        else if (CurrentRow % 2 == 0)
                        {
                            Array.Copy(entry.ToArray(), 0, UartSendData, 23, 16);//将entry数组复制到UartSendData数组的第23个元素开始的位置
                            await SafeSerialWrite(UartSendData, 7 + 32);//发送数据
                            
                            Adress += (ushort)PageSize;//EEPROM的页内偏移地址加32
                            UartSendData[5] = (byte)(Adress >> 8);//EEPROM的页内偏移地址的页内偏移
                            UartSendData[6] = (byte)(Adress & 0x00FF);//EEPROM的页内偏移地址的页
                        }
                        else
                        {
                            Array.Copy(entry.ToArray(), 0, UartSendData, 7, 16);
                        }
                        
                        UpdateProgress(CurrentRow, Row);//更新进度条

                        if (CurrentRow == Row)//如果当前行数等于需要写入的行数，则跳出循环
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("写入EEPROM失败：" + ex.Message);
                return false;
            }
            return true;
        }



        private async void Write_EEPROM_button_Click(object sender, EventArgs e)
        {
            byte[] send_data = new byte[255];
            EEPROMuiProcessBar.Value = 0;//进度条初始化
            send_data[0] = Frame_header;//固定帧头
            //byte RegisterAddressMode = send_data[1];//待改进，将数组地址付给变量

            if (File_content.Count == 0)
            {
                MessageBox.Show("请先打开文件");
                return;
            }
            if (!I2C_serialPort.IsOpen)//串口没有打开
            {
                MessageBox.Show("串口未打开", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                uart_I2C_control_button.Text = "打开串口";
                return;
            }
            if (EEPROM_adr_textBox.Text == "")
            {
                MessageBox.Show("请输入EEPROM地址", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (EEPROM_adr_textBox.Text.Length == 1)
            {
                EEPROM_adr_textBox.Text = "0" + EEPROM_adr_textBox.Text;//设备地址为1位时，前面补0
            }
            send_data[2] = 0x06;//EEPROM按页写入命令
            send_data[3] = Convert.ToByte(EEPROM_adr_textBox.Text, 16);//将EEPROM地址转换为字节数据
            send_data[5] = 0x00;//EEPROM的页内偏移地址

            int PageSize;//EEPROM的页大小
            int row;//需要写入的行数
            bool errorFlag = true;//记录写入EEPROM是否出错
            // 待加入对于不同容量大小的EEPROM写入字节数的判断
            // EEPROM型号及其容量和页大小：
            // - 24LC02: 容量256字节，页大小8字节
            // - 24LC04: 容量512字节，页大小16字节
            // - 24LC08: 容量1024字节，页大小16字节
            // - 24LC16: 容量2048字节，页大小16字节
            // - 24LC32: 容量4096字节，页大小32字节
            // - 24LC64: 容量8192字节，页大小32字节
            //send_data[1]表示选择I2C模式的选择的8位或16位寄存器地址模式

            //send_data从第五个开始及其后面的数据表示需要写入的数据
            switch (EEPROM_comboBox.Text)
            {
                case "24LC02":
                    send_data[1] = 0x02;//选择8位寄存器地址模式
                    PageSize = 8;//EEPROM的页大小
                    send_data[4] = (byte)PageSize;//写字节个数
                    row = 16;//需要写入的行数
                    //EEPROMuiProcessBar.Maximum = row;
                    errorFlag = await SendEEPROM_Content(send_data, PageSize, row);//调用函数写入EEPROM
                    break;

                case "24LC04":
                    send_data[1] = 0x02;//选择8位寄存器地址模式
                    PageSize = 16;//EEPROM的页大小
                    send_data[4] = (byte)PageSize;//写字节个数
                    row = 32;//需要写入的行数
                    //EEPROMuiProcessBar.Maximum = row;
                    errorFlag = await SendEEPROM_Content(send_data, PageSize, row);//调用函数写入EEPROM
                    break;

                case "24LC08":
                    send_data[1] = 0x02;//选择8位寄存器地址模式
                    PageSize = 16;//EEPROM的页大小
                    send_data[4] = (byte)PageSize;//写字节个数
                    row = 64;//需要写入的行数
                    //EEPROMuiProcessBar.Maximum = row;
                    errorFlag = await SendEEPROM_Content(send_data, PageSize, row);//调用函数写入EEPROM
                    break;

                case "24LC16":
                    send_data[1] = 0x02;//选择8位寄存器地址模式
                    PageSize = 16;//EEPROM的页大小
                    send_data[4] = (byte)PageSize;//写字节个数
                    row = 128;//需要写入的行数
                    //EEPROMuiProcessBar.Maximum = row;
                    errorFlag = await SendEEPROM_Content(send_data, PageSize, row);//调用函数写入EEPROM
                    break;

                case "24LC32":
                    send_data[1] = 0x03;//选择16位寄存器地址模式
                    PageSize = 32;//EEPROM的页大小
                    send_data[4] = (byte)PageSize;//写字节个数
                    row = 256;//需要写入的行数
                    //EEPROMuiProcessBar.Maximum = row;
                    errorFlag = await SendEEPROM_Content(send_data, PageSize, row);//调用函数写入EEPROM
                    break;

                case "24LC64":
                    send_data[1] = 0x03;//选择16位寄存器地址模式
                    PageSize = 32;//EEPROM的页大小
                    send_data[4] = (byte)PageSize;//写字节个数
                    row = 512;//需要写入的行数
                    //EEPROMuiProcessBar.Maximum = row;
                    errorFlag = await SendEEPROM_Content(send_data, PageSize, row);//调用函数写入EEPROM
                    break;

                default:
                    MessageBox.Show("请选择EEPROM型号", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
            if (errorFlag)
            {
                MessageBox.Show("写入EEPROM成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("写入EEPROM失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}