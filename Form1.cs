using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Threading;

namespace DragFinder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            this.Visible = false;

            notifyIcon1.Text = "Drag Finder";
            notifyIcon1.Visible = true;
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
            notifyIcon1.BalloonTipTitle = "안녕하세요.";
            notifyIcon1.BalloonTipText = "처음이라면 도움말을 확인해 보세요.";
            notifyIcon1.ShowBalloonTip(200);

            Hook.HookStart();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            exit();
        }

        public void exit()
        {

            notifyIcon1.BalloonTipTitle = "안녕히 계세요.";
            notifyIcon1.BalloonTipText = "ㅃㅃ";
            notifyIcon1.ShowBalloonTip(50);
            
            Hook.HookEnd();

            System.Threading.Thread.Sleep(1000);
            notifyIcon1.Dispose();
            Application.Exit();
        }

        private void exit_impl(object sender, EventArgs e)
        {
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var thread_help = new Thread(new ThreadStart(help_impl));
            thread_help.SetApartmentState(ApartmentState.STA);
            thread_help.Start();
        }
        
        private void help_impl()
        {
            HelpForm helpForm = new HelpForm();
            helpForm.Show();
            Application.Run(helpForm);
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var thread_info = new Thread(new ThreadStart(info_impl));
            thread_info.SetApartmentState(ApartmentState.STA);
            thread_info.Start();
        }

        private void info_impl()
        {
            AdditionalInfoForm additionalInfoForm = new AdditionalInfoForm();
            additionalInfoForm.Show();
            Application.Run(additionalInfoForm);
        }
    }
}
