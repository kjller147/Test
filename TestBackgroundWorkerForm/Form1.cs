using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TestBackgroundWorkerForm.DataLayer;
using TestBackgroundWorkerForm.Models;

namespace TestBackgroundWorkerForm
{
    public partial class Form1 : Form
    {
        private BackgroundWorker bgWoker;
        private Timer timer;
        public Form1()
        {                                   
            InitializeComponent();
            bgWoker = new BackgroundWorker();
            bgWoker.DoWork += BgWoker_DoWork;
            bgWoker.WorkerSupportsCancellation = true;
            timer = new Timer();
            timer.Tick += Timer_Tick;
        }

        private void BgWoker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }

        private void BgWoker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker woker = sender as BackgroundWorker;
            var i = 0;

            while (!bgWoker.CancellationPending)
            {
                if (label1.BackColor == SystemColors.Control)
                    label1.BackColor = Color.Red;
                else
                    label1.BackColor = SystemColors.Control; 
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "COLOR TEST";
            label1.BackColor = Color.Blue;            

            timer.Interval = 3000;
            timer.Start();
            //bgWoker.RunWorkerAsync();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (label1.BackColor == SystemColors.Control)
                label1.BackColor = Color.FromArgb(255, 69, 0);
            else
                label1.BackColor = SystemColors.Control;
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            //this.bgWoker.CancelAsync();
            timer.Stop();
            label1.BackColor = SystemColors.Control;
        }
    }
}
