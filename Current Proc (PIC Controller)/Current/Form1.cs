using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Current
{
    public partial class Form1 : Form
    {
        string temp1, temp2;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Fresh();
        }

        private void Fresh()
        {
            comboBox1.Items.Clear();
            for (int i = 1; i < 51; i++)
            {
                if (!serialPort1.IsOpen)
                {
                    try
                    {
                        serialPort1.PortName = "COM" + i.ToString();
                        serialPort1.Open();
                        serialPort1.Close();
                        comboBox1.Items.Add(serialPort1.PortName);
                    }
                    catch
                    {

                    }
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!serialPort1.IsOpen && comboBox1.SelectedIndex > -1)
            {
                try
                {
                    serialPort1.PortName = comboBox1.SelectedItem.ToString();
                    serialPort1.Open();
                    timer1.Enabled = true;
                }
                catch
                {

                }
            }         
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                temp1 = serialPort1.ReadExisting();
                if (temp1.IndexOf("\r") != -1 || temp1.IndexOf("\n") != -1)
                {
                    temp2 += temp1;
                    textBox1.Text = temp2;
                    temp2 = "";
                } 
                else
                {
                    temp2 = temp1;
                }
            }
        }
    }
}
