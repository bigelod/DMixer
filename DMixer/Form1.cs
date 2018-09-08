using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMixer
{
    public partial class Form1 : Form
    {
        //A simple DropMix randomizer app, apologies for the poor variable naming it was originally just a test project
        //Originally made by Daven Bigelow 2018, this code is public domain
        //It will ask you to do one of the following:
        //[NEW] - Add a card to a slot
        //[DEL] - Delete any card from a slot
        //[CLR] - Clear all cards from the board

        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!snooze.Enabled)
            {
                Random rnd = new Random();

                int result = rnd.Next(1, 2000); //What are our odds?
                int slot = rnd.Next(1, 6); //Which slot?

                string cmd = "";

                if (result < 1200)
                {
                    //Add a card
                    cmd = "Slots " + slot.ToString() + " [NEW]";
                }
                else if (result > 1910 && result < 1950)
                {
                    //Clear the board, extremely rare
                    cmd = "Slot ALL [CLR]";
                }
                else if (result >= 1950)
                {
                    //Silently skip turn, also rare
                    cmd = "";
                }
                else
                {
                    //Remove a card
                    cmd = "Slot " + slot.ToString() + " [DEL]";
                }

                if (cmd != "")
                {
                    //We got a command!
                    label1.Text = "Cmd: " + cmd; //Tell the user their command

                    snooze.Enabled = true; //Enable the snooze timer

                    button1.Enabled = true;
                }

                timer1.Interval = rnd.Next(30000, 90000); //Set our own timer back up
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled)
            {
                //Now we wait for a command
                timer1.Enabled = true;
                label1.Text = "Waiting for CMD";
                button1.Enabled = false;
            }
            else if (snooze.Enabled)
            {
                //We received a command, now we wait again for it
                label1.Text = "Waiting for CMD";

                //Reset color back to white
                Color c = new Color();
                int r = 255;
                int g = r;
                int b = r;

                c = Color.FromArgb(255, r, g, b);

                this.BackColor = c;
                this.Update();

                snooze.Enabled = false;
                button1.Enabled = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Force it to top, set back color
            this.Focus();
            this.BringToFront();
            this.TopLevel = true;
            this.TopMost = true;

            Color c = new Color();
            int r = 255;
            int g = r;
            int b = r;

            c = Color.FromArgb(255, r, g, b);

            this.BackColor = c;
            this.Update();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
            {
                //Possibly allow pressing button1 this way
            }
        }

        private void snooze_Tick(object sender, EventArgs e)
        {
            //Remind the user that they can go now
            Random rnd = new Random();

            Color c = new Color();
            int r = 255 - rnd.Next(1, 100);
            int g = r;
            int b = r;

            c = Color.FromArgb(255, r, g, b);

            this.BackColor = c;
            this.Update();

            button1.Enabled = true;
        }
    }
}
