using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace lightstudio
{
    public partial class serialManagerForm : Form
    {
        public bool isConnected = false;
        public SerialPort port;

        public serialManagerForm()
        {
            InitializeComponent();
            
        }

        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {
            comboBox1.Items.Clear();

            string[] ports = SerialPort.GetPortNames();

            foreach (string port in ports)
                comboBox1.Items.Add(port);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (isConnected == false)
                {
                    string portname = comboBox1.Text;
                    string baudtext = baudTextBox.Text;
                    int baudrate = Int32.Parse(baudtext);
                    port = new SerialPort(portname, baudrate);
                    port.DataReceived += Port_DataReceived;
                    port.WriteBufferSize = 16;
                    port.Open();
                }
                else if (isConnected && button1.Text == "Disconnect")
                {
                    port.Close();
                }

                if (port.IsOpen)
                {
                    connectionStatusLabel.Text = "Connected";
                    connectionStatusLabel.ForeColor = Color.Green;
                    button1.Text = "Disconnect";
                    isConnected = true;
                }
                else
                {
                    connectionStatusLabel.Text = "Disconnected";
                    connectionStatusLabel.ForeColor = Color.Red;
                    button1.Text = "Connect";
                    isConnected = false;
                }
                
                
            }
            catch(Exception ex)
            {

            }
        }
        int errorCount = 0;
        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string readResult = port.ReadExisting();
            
                errorCount++;
                System.Diagnostics.Debug.WriteLine("ERRORS: " + errorCount);
        }

        private void serialManagerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void serialManagerForm_Load(object sender, EventArgs e)
        {

        }
    }
}
