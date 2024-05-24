
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
/*
 SEMANUR ÜÇDAĞ BST 3. SINIF 21430070045-GÖRSEL PROGRAMLAMA UYGULAMLARI
 */
namespace SemaBackgroundWorker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int sum = 0;
            for (int i = 0; i < 100; i++)
            {
                if (backgroundWorker1.CancellationPending)
                {
                    e.Cancel = true;
                    backgroundWorker1.ReportProgress(0);
                    return;
                }

                Thread.Sleep(100);  
                sum += i;           
                backgroundWorker1.ReportProgress(i);
            }
            e.Result = sum; 
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            label1.Text = e.ProgressPercentage.ToString() + "%";
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                label1.Text = "Operation Cancelled";
                progressBar1.Value = 0;
            }
            else if (e.Error != null)
            {
                label1.Text = e.Error.Message;
            }
            else
            {
                label1.Text = "100%"; 
                progressBar1.Value = 100; 
            }
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy) 
            {
                backgroundWorker1.CancelAsync();
            }
        }
    }
}

