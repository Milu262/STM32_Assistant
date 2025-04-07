using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace STM32_Assistant
{
    public partial class Form1 : Form
    {
        readonly byte Frame_header = 0x55;//固定帧头
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



    }
}
