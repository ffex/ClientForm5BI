using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientForm
{
    public partial class Form1 : Form
    {
        //variabili globali
        Socket client;

        public Form1()
        {
            InitializeComponent();

            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        }

        private void btnConnetti_Click(object sender, EventArgs e)
        {
            IPAddress ipaddr = null;
            int nPort = 0;

            if(!IPAddress.TryParse(txtIPServer.Text,out ipaddr))
            {
                //ip address sbagliato
                MessageBox.Show("Ip vuoto o non valido.","Errore");
                //return;
            }

            if(!int.TryParse(txtPorta.Text,out nPort))
            {
                MessageBox.Show("Porta vuota o non valida", "Errore");
            }
            //Controllo porte
            if (nPort <0 && nPort > 65535)
            {
                MessageBox.Show("La porta deve essere compresa fra 0 e 65535", "Errore");
            }
            try { 

                client.Connect(ipaddr, nPort);
            }catch (Exception ex)
            {
                MessageBox.Show("Errore di connessione", "Errore");
                if(client != null)
                {
                    if (client.Connected)
                    {
                        client.Shutdown(SocketShutdown.Both);
                    }
                    client.Close();
                    
                    client.Dispose();
                }
            }
            txtIPServer.Enabled = false;
            txtPorta.Enabled = false;
            btnConnetti.Enabled = false;

            txtMessaggio.Enabled = true;
            btnInvia.Enabled = true;
        }

        private void btnInvia_Click(object sender, EventArgs e)
        {
            byte[] sendBuff = new byte[512];
            byte[] recvBuff = new byte[512];
            int nRecvBytes = 0;
            string recvString = "";
            try
            {
            sendBuff = Encoding.ASCII.GetBytes(txtMessaggio.Text);
            client.Send(sendBuff);

            nRecvBytes= client.Receive(recvBuff);
            recvString = Encoding.ASCII.GetString(recvBuff, 0, nRecvBytes);

                lstMessaggiServ.Items.Add(recvString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Errore");
             
            }
        }
    }
}
