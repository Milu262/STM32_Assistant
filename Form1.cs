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

       

       
    }
}