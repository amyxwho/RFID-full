using System;
using Shapes;
using Phidgets;         //needed for the interfacekit class and the phidget exception class
using Phidgets.Events;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RFID_full
{
    public partial class Form2 : Form
    {

        Graphics g;
       
      

        // Set the initial look of your form
        public Form2()
        {
            InitializeComponent();
            BackColor = Color.Black;

            
        }





        private void Form2_Load(object sender, EventArgs e)
        {
            // Add event handlers for all functions you'll be using
   
            g = this.CreateGraphics();

            PictureBox picShowPicture = new PictureBox();
            Image image1 = Image.FromFile(@"/Downloads/grumpycat.jpg");
            picShowPicture.Image = image1;
            picShowPicture.Show();
         
            

        }


        // This code runs when you attach the RFID Phidget to your computer
        public void rfid_Attach(object sender, AttachEventArgs e)
        {
            
            RFID attached = (RFID)sender;
            BackColor = Color.Black;
        }

        // This code runs when you detach the RFID Phidget from your computer
        //detach event handler...clear all the fields, display the attached status, and disable the checkboxes.
        public void rfid_Detach(object sender, DetachEventArgs e)
        {
            RFID detached = (RFID)sender;
            BackColor = Color.White;
          
        }

        // When the Phidget is reading an RFID...
        public void rfid_Tag(object sender, TagEventArgs e)
        {
            if (e.Tag.Equals("01068ded0e")) // black circle 
            {
                BackColor = Color.Blue;
                
                label1.Text = e.Tag; 

            }

            if (e.Tag.Equals("0106935bbb")) // keychain
            {
                BackColor = Color.Pink;

                label1.Text = e.Tag;

            }
            else
            {
                PictureBox picShowPicture = new PictureBox();
                Image image1 = Image.FromFile(@"C:\Users\Orit\Downloads\grumpycat.jpg");
                picShowPicture.BackgroundImage = image1;
                picShowPicture.Image = image1;
                picShowPicture.Show();
                label1.Text = e.Tag;
            }

        }

        //Tag lost event handler...here we simply want to clear our tag field in the GUI
        public void rfid_TagLost(object sender, TagEventArgs e)
        {
           BackColor = Color.Black;
           label1.Text = "No Tag";
           
        }

        //When the application is being terminated, close the Phidget.
        public void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {

            //run any events in the message queue - otherwise close will hang if there are any outstanding events
            Application.DoEvents();

              
        }

        //Parses command line arguments and calls the appropriate open
        #region Command line open functions
        private void openCmdLine(Phidget p)
        {
            openCmdLine(p, null);
        }
        private void openCmdLine(Phidget p, String pass)
        {
            int serial = -1;
            int port = 5001;
            String host = null;
            bool remote = false, remoteIP = false;
            string[] args = Environment.GetCommandLineArgs();
            String appName = args[0];

            try
            { //Parse the flags
                for (int i = 1; i < args.Length; i++)
                {
                    if (args[i].StartsWith("-"))
                        switch (args[i].Remove(0, 1).ToLower())
                        {
                            case "n":
                                serial = int.Parse(args[++i]);
                                break;
                            case "r":
                                remote = true;
                                break;
                            case "s":
                                remote = true;
                                host = args[++i];
                                break;
                            case "p":
                                pass = args[++i];
                                break;
                            case "i":
                                remoteIP = true;
                                host = args[++i];
                                if (host.Contains(":"))
                                {
                                    host = host.Split(':')[0];
                                    port = int.Parse(host.Split(':')[1]);
                                }
                                break;
                            default:
                                goto usage;
                        }
                    else
                        goto usage;
                }

                if (remoteIP)
                    p.open(serial, host, port, pass);
                else if (remote)
                    p.open(serial, host, pass);
                else
                    p.open(serial);
                return; //success
            }
            catch { }
        usage:
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Invalid Command line arguments." + Environment.NewLine);
            sb.AppendLine("Usage: " + appName + " [Flags...]");
            sb.AppendLine("Flags:\t-n   serialNumber\tSerial Number, omit for any serial");
            sb.AppendLine("\t-r\t\tOpen remotely");
            sb.AppendLine("\t-s   serverID\tServer ID, omit for any server");
            sb.AppendLine("\t-i   ipAddress:port\tIp Address and Port. Port is optional, defaults to 5001");
            sb.AppendLine("\t-p   password\tPassword, omit for no password" + Environment.NewLine);
            sb.AppendLine("Examples: ");
            sb.AppendLine(appName + " -n 50098");
            sb.AppendLine(appName + " -r");
            sb.AppendLine(appName + " -s myphidgetserver");
            sb.AppendLine(appName + " -n 45670 -i 127.0.0.1:5001 -p paswrd");
            MessageBox.Show(sb.ToString(), "Argument Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            Application.Exit();
        }
        #endregion

        private void nameTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load_1(object sender, EventArgs e)
        {
            BackColor = Color.Black;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }







    }
        




}
