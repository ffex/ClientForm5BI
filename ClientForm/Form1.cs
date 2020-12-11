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

            client.Connect(ipaddr, nPort);

        }
    }
}
