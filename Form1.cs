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


        private void find_i2c_device_button_Click(object sender, EventArgs e)
        {
            byte[] send_data = new byte[3];
            send_data[0] = Frame_header;//固定帧头
            if (!I2C_serialPort.IsOpen)//串口没有打开
            {
                MessageBox.Show("串口未打开", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                uart_I2C_control_button.Text = "打开串口";
                return;
            }
            send_data[1] = 0x02;//8bit寄存器地址模式
            send_data[2] = 0x05;//查找I2C总线上的设备
            try
            {
                I2C_serialPort.Write(send_data, 0, 3);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
