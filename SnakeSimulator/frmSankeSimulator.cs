using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SnakeSimulator
{
    public partial class frmSankeSimulator : Form
    {
        public frmSankeSimulator()
        {
            InitializeComponent();
        }

        private void frmSankeSimulator_Load(object sender, EventArgs e)
        {

        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.Multiselect = true;

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (var item in dlg.FileNames)
                {
                    lstExec.Items.Add(item);
                }
                
            }


            
        }

        private void btnRun_Click(object sender, EventArgs e)
        {

            foreach (ListViewItem item in lstExec.Items)
            {
                BackgroundWorker bw = new BackgroundWorker();

                bw.WorkerSupportsCancellation = true;
                bw.WorkerReportsProgress = true;

                Simulator sim = new Simulator();

                sim.ExecPath = item.Text;
                var game = new SnakeGame();
                game.makeRandomGame(10, 10);
                sim.GameInput = game;

                bw.DoWork += new DoWorkEventHandler(sim.run);
                bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);

                btnRun.Enabled = false;
                bw.RunWorkerAsync();
            }
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (this.txtLogs.Text.Length > 10000)
                this.txtLogs.Text = "";

            this.txtLogs.Text += ((string)e.UserState) + "\r\n";
            this.txtLogs.SelectionStart = this.txtLogs.Text.Length;
            txtLogs.ScrollToCaret();
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnRun.Enabled = true;
            if ((e.Cancelled == true))
            {
                this.txtLogs.Text += "Canceled!";
            }

            else if (!(e.Error == null))
            {
                this.txtLogs.Text += ("Error: " + e.Error.Message);
            }

            else
            {
                this.txtLogs.Text += "Done!";
            }

            this.txtLogs.SelectionStart = this.txtLogs.Text.Length;
            txtLogs.ScrollToCaret();
        }
    }

}
