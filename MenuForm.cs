using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Runtime.InteropServices;
using System.Diagnostics;
using WindowsInput;
using System.Net;
using System.Threading;
using System.Text.RegularExpressions;

namespace DragFinder
{
    public partial class MenuForm : Form
    {


        private bool bColor = true;
        private int currentDisplayCount = 1;
        private int prevScroll = 0;

        private string selectedText;

        [DllImport("user32.dll", SetLastError = false)]
        private static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        protected override void OnHandleCreated(EventArgs e)
        {
            if (this.Handle != IntPtr.Zero)
            {
                IntPtr hWndDeskTop = GetDesktopWindow();
                SetParent(this.Handle, hWndDeskTop);
            }
            base.OnHandleCreated(e);
        }

        private void addMeanings(List<KeyValuePair<string, string>> meanings)
        {
            foreach (var i in meanings)
            {
                LinkLabel label = new LinkLabel();
                label.Text = i.Key;
                label.Font = new Font("맑은 고딕", 10);
                label.LinkColor = Color.Black;
                label.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
                label.AutoSize = true;
                label.MaximumSize = new Size(500, 0);

                LinkLabel.Link link = new LinkLabel.Link();
                link.LinkData = i.Value;
                label.Links.Add(link);

                label.LinkClicked += new LinkLabelLinkClickedEventHandler(labelClicked);
                label.MouseEnter += new System.EventHandler(this.MenuForm_MouseHover);
                label.MouseLeave += new System.EventHandler(this.MenuForm_MouseLeave);

                if (bColor)
                {
                    label.BackColor = Color.FromArgb(255, 255, 255);
                    bColor = false;
                }
                else
                {
                    label.BackColor = Color.FromArgb(200, 200, 200);
                    bColor = true;
                }

                simpleMeaningsPanel.Controls.Add(label);
            }
        }

        private void initDisplay(int currentDisplayCount)
        {
            selectedText = Hook.getSelection();
            Console.WriteLine("SEARCH : " + selectedText);
            addMeanings(DictParser.getInfoFromNaverAPI(selectedText, currentDisplayCount));
        }

        private void labelClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo(e.Link.LinkData as string);
            Process.Start(sInfo);
        }

        public MenuForm()
        {
            InitializeComponent();

            this.AutoSize = true;
            this.StartPosition = FormStartPosition.Manual;
            this.ShowInTaskbar = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            initDisplay(currentDisplayCount);

            Action handler = null;
            handler = () =>
            {
                MultiKeyGesture.keyUpEvent -= handler;
                this.Close();
            };

            MultiKeyGesture.keyUpEvent += handler;

            if (MultiKeyGesture.bMatched == false)
            {
                MultiKeyGesture.keyUpEvent -= handler;
                return;
            }

            this.Show();
            Application.Run();
        }

        private void MenuForm_Load(object sender, EventArgs e)
        {
            this.Location = Cursor.Position;
            
            this.simpleMeaningsPanel.Select();
            this.MouseWheel += new MouseEventHandler(MouseWheelEvent);
            this.simpleMeaningsPanel.MouseWheel += new MouseEventHandler(MouseWheelEvent);
        }

        private void MouseWheelEvent(object sender, MouseEventArgs e)
        {
            var currentScroll = this.simpleMeaningsPanel.AutoScrollPosition;
            if(e.Delta < 0 && prevScroll == currentScroll.Y && currentDisplayCount < 100)
            {
                currentDisplayCount += 5;
                addMeanings(DictParser.getInfoFromNaverAPI(selectedText, currentDisplayCount));
            }
            prevScroll = currentScroll.Y;
            Console.WriteLine(currentScroll.Y);
        }

        private void MenuForm_MouseHover(object sender, EventArgs e)
        {
            this.Opacity = 1;
        }

        private void MenuForm_MouseLeave(object sender, EventArgs e)
        {
            this.Opacity = 0.1;
        }
    }
}
