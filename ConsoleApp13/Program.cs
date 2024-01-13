

 const int trigPin = 5, echoPin = 6;
long uzaklik;
long sure;
void setup()
{
    pinMode(trigPin, OUTPUT);
    pinMode(echoPin, INPUT);
    Serial.begin(9600);
}
void loop()
{
    digitalWrite(trigPin, LOW);
    delayMicroseconds(5);
    digitalWrite(trigPin, HIGH);
    delayMicroseconds(10);
    digitalWrite(trigPin, LOW);
    sure = pulseIn(echoPin, HIGH);
    uzaklik = sure / 2 / 29.1;
    if (uzaklik > 200)
        uzaklik = 200;
    Serial.println(uzaklik);
    delay(100);
}

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

namespace mesafe_olcer
{
    public partial class Form1 : Form
    {
        private string data;   
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();  
            foreach (string port in ports)
                comboBox1.Items.Add(port);               


            serialPort1.DataReceived += new SerialDataReceivedEventHandler(SerialPort1_DataReceived); 
        }

        private void SerialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            data = serialPort1.ReadLine();                     
            this.Invoke(new EventHandler(displayData_event));
        }

        private void displayData_event(object sender, EventArgs e)
        {
            label2.Text = data;                               
            progressBar1.Value = Convert.ToInt32(data);       
        }

        private void baglan_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.PortName = comboBox1.Text;       
                serialPort1.Open();                          
                kes.Enabled = true;                          
                baglan.Enabled = false;                      
                label3.Text = "Bağlantı sağlandı";
                label3.ForeColor = Color.Green;              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata");  
            }
        }
        private void kes_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Close();              
                kes.Enabled = false;              
                baglan.Enabled = true;            
                label3.Text = "Bağlantı kesildi";
                label3.ForeColor = Color.Red;     
            }
            catch (Exception ex2)
            {
                MessageBox.Show(ex2.Message); 
            }

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (serialPort1.IsOpen) serialPort1.Close();    
        }

    }
}
