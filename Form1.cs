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
using System.Net;
using System.Drawing.Printing;

namespace STM32_Assistant
{
    public partial class Form1 : Form
    {
        private readonly byte Frame_header = 0x55;//固定帧头

        //创建一个不确定长度的字节数组
        private List<byte> byteList = new List<byte>();

        public Form1()
        {
            InitializeComponent();
        }

        private void EEPROM_adr_textBox_TextChanged(object sender, EventArgs e)
        {
            //限制输入的字符为十六进制数
            UpdateTextBoxForHexInput(EEPROM_adr_textBox);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Serial_port_init();//串口模式的串口初始化
            Serial_I2C_init();//I2C模式的串口初始化

            this.FormBorderStyle = FormBorderStyle.FixedSingle;//不能缩放窗口
            this.MaximizeBox = false;//不能最大化窗口
            //Control.CheckForIllegalCrossThreadCalls = false;//取消跨线程检查,不推荐使用这种方法
        }
        private void ReciveDataProcess(List<byte[]> ReciveData)
        {
            foreach(var data in ReciveData)
            {

            }
        }
        private async void Read_EEPROM_button_Click(object sender, EventArgs e)
        {
            EEPROMuiProcessBar.Value = 0;//进度条初始化
            File_content.Clear();//清空文件内容
            fileContentTextBox.Clear();//清空文本框内容
            byte[] send_data = new byte[255];
            send_data[0] = Frame_header;//固定帧头

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
            send_data[2] = 0x07;//EEPROM连续读命令
            send_data[3] = Convert.ToByte(EEPROM_adr_textBox.Text, 16);//将EEPROM地址转换为字节数据
            send_data[4] = 0xFF;//读256个字节
            send_data[5] = 0x00;//EEPROM的页内偏移地址
            int EEPROMSize = 0;//EEPROM的容量大小
            bool errorFlag = true;//记录写入EEPROM是否出错
            bool AdressMode = false;//记录寄存器地址模式false表示8位寄存器地址模式，true表示16位寄存器地址模式
            // 待加入对于不同容量大小的EEPROM写入字节数的判断
            // EEPROM型号及其容量和页大小：
            // - 24LC02: 容量256字节，页大小8字节
            // - 24LC04: 容量512字节，页大小16字节
            // - 24LC08: 容量1024字节，页大小16字节
            // - 24LC16: 容量2048字节，页大小16字节
            // - 24LC32: 容量4096字节，页大小32字节
            // - 24LC64: 容量8192字节，页大小32字节
            //send_data[1]表示选择I2C模式的选择的8位或16位寄存器地址模式

            switch (EEPROM_comboBox.Text)
            {
                case "24LC02":
                    send_data[1] = 0x02;//选择8位寄存器地址模式
                    EEPROMSize = 256;
                    AdressMode = false;
                    errorFlag = await ReadEEPROM_Content(send_data, EEPROMSize, AdressMode);//调用函数写入EEPROM
                    break;

                case "24LC04":
                    send_data[1] = 0x02;//选择8位寄存器地址模式
                    EEPROMSize = 512;
                    AdressMode = false;
                    errorFlag = await ReadEEPROM_Content(send_data, EEPROMSize, AdressMode);//调用函数写入EEPROM
                    break;

                case "24LC08":
                    send_data[1] = 0x02;//选择8位寄存器地址模式
                    EEPROMSize = 1024;
                    AdressMode = false;
                    errorFlag = await ReadEEPROM_Content(send_data, EEPROMSize, AdressMode);//调用函数写入EEPROM
                    break;

                case "24LC16":
                    send_data[1] = 0x02;//选择8位寄存器地址模式
                    EEPROMSize = 2048;
                    AdressMode = false;
                    errorFlag = await ReadEEPROM_Content(send_data, EEPROMSize, AdressMode);//调用函数写入EEPROM
                    break;

                case "24LC32":
                    send_data[1] = 0x03;//选择16位寄存器地址模式
                    EEPROMSize = 4096;
                    AdressMode = true;
                    errorFlag = await ReadEEPROM_Content(send_data, EEPROMSize, AdressMode);//调用函数写入EEPROM
                    break;

                case "24LC64":
                    send_data[1] = 0x03;//选择16位寄存器地址模式
                    EEPROMSize = 8192;
                    AdressMode = true;
                    errorFlag = await ReadEEPROM_Content(send_data, EEPROMSize, AdressMode);//调用函数写入EEPROM
                    break;

                default:
                    MessageBox.Show("请选择EEPROM型号", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }
        private async Task<bool> ReadEEPROM_Content(byte[] UartSendData, int EEPROMSize, bool AdressMod)
        {
            int CyclesCount = 0;//记录循环次数
            int DelayTime = 0;//记录延时时间
            //byte[] ReciveData = new byte[300];//接收到的数据

            List<byte[]> ReciveData = new List<byte[]>();
            CyclesCount = EEPROMSize / 256;//计算循环次数
            try
            {
                if (!AdressMod)//8位寄存器地址模式
                {
                    //UartSendData[1] = 0x02;//选择8位寄存器地址模式
                    //UartSendData[2] = 0x07;//选择EEPROM连续读命令

                    for (int i = 0; i < CyclesCount; i++)
                    {
                        await I2C_serialPort.BaseStream.WriteAsync(UartSendData, 0, 6);//发送数据
                        while(!dataFrameReceivedFlag)//等待接收数据
                        {
                            //延迟10ms
                            await Task.Delay(20);
                            DelayTime++;
                            if (DelayTime == 10)
                            {
                                DelayTime = 0;
                                MessageBox.Show("接收数据超时", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                //为什么一直显示接收数据超时？
                                //
                                return false;
                            }

                        }
                        lock (lockObject)//
                        {
                            //只添加globalDataArray的前262个字节的数据，因为只接收到这么多数据
                            ReciveData.Add(globalDataArray.Take(262).ToArray());
                            //ReciveData.Add(globalDataArray);//将接收到的数据存入数组
                            dataFrameReceivedFlag = false;//重置标志位
                        }
                        //减掉ReciveData[i]的前6个字节，因为前6个字节是命令字节
                        ReciveData[i] = ReciveData[i].Skip(6).ToArray();
                        //定义一个每行16个字节的二维数组
                        byte[,] ReciveDataArray = new byte[16, 16];
                        //将ReciveData[i]的256个字节存入ReciveDataArray中
                        Buffer.BlockCopy(ReciveData[i], 0, ReciveDataArray, 0, ReciveData[i].Length);

                        // 使用单次 AppendText 调用代替多次调用，减少 UI 更新次数
                        StringBuilder sb = new StringBuilder();
                        for (int row = 0; row < 16; row++)
                        {
                            for (int col = 0; col < 16; col++)
                            {
                                // 将每个字节转换为两位的十六进制字符串
                                string hexValue = ReciveDataArray[row, col].ToString("X2");
                                // 将十六进制字符串添加到 StringBuilder 中
                                sb.Append(hexValue + " ");
                            }
                            // 在每行结束后添加一个换行符
                            sb.AppendLine();
                        }
                        if (this.InvokeRequired)
                        {
                            this.Invoke(new Action(() =>
                            {
                                fileContentTextBox.AppendText(sb.ToString());
                            }));
                        }
                        else
                        {
                            fileContentTextBox.AppendText(sb.ToString());
                        }
                        //string ReciveDataString = BitConverter.ToString(ReciveData[i]);
                        //fileContentTextBox.AppendText(ReciveDataString);
                        ////换行
                        //fileContentTextBox.AppendText(Environment.NewLine);



                        UartSendData[3] += 0x02;//更新EEPROM地址
                    }

                }
                else//16位寄存器地址模式
                {
                    UartSendData[1] = 0x03;//选择16位寄存器地址模式
                    ushort AdressRegist = 0x0000;//记录寄存器地址
                    UartSendData[5] = 0x00;//寄存器地址高字节
                    UartSendData[6] = 0x00;//寄存器地址低字节
                    for (int i = 0; i < CyclesCount; i++)
                    {
                        await I2C_serialPort.BaseStream.WriteAsync(UartSendData, 0, 7);//发送数据
                        while (!dataFrameReceivedFlag)//等待接收数据
                        {

                        }
                        lock (lockObject)//
                        {
                            ReciveData.Add(globalDataArray);//将接收到的数据存入数组
                            dataFrameReceivedFlag = false;//重置标志位
                        }
                        AdressRegist += 0x0100;//更新EEPROM偏移地址
                        UartSendData[5] = (byte)(AdressRegist >> 8);//寄存器地址高字节
                        UartSendData[6] = (byte)(AdressRegist & 0x00FF);//寄存器地址低字节
                    }
                }
                //ReciveDataProcess(ReciveData);//处理接收到的数据
            }
            catch (Exception ex)
            {
                MessageBox.Show("读EEPROM失败：" + ex.Message);
                return false;
            }
            return true;
        }
    }
}