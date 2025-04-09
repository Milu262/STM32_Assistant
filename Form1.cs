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


        private void Open_EPPROM_program_button_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();//创建一个OpenFileDialog对象
            openFileDialog.Filter = "HEX文件|*.hex|BIN文件|*.bin|文本文件|*.txt|所有文件|*.*";//设置文件过滤器
            if (openFileDialog.ShowDialog() == DialogResult.OK)//如果用户点击了确定按钮
            {
                string filePath = openFileDialog.FileName;//获取用户选择的文件路径
                //将文件路径显示在文本框中
                EPPROM_program_file_path_textBox.Text = filePath;

                // 读取文件内容
                string fileContent = "";
                using (StreamReader reader = new StreamReader(filePath))
                {
                    fileContent = reader.ReadToEnd();
                }

                // 显示文件内容
                fileContentTextBox.Text = fileContent;
            }
        }
    }
}
