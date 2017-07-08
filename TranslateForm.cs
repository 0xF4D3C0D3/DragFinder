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

namespace DragFinder
{
    public partial class TranslateForm : Form
    {
        private string selectedText;
        private string translatedText;

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

        public TranslateForm()
        {
            InitializeComponent();

            //this.AutoSize = true;
            this.StartPosition = FormStartPosition.Manual;
            this.ShowInTaskbar = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            selectedText = Hook.getSelection();
            Console.WriteLine("select "+selectedText);
            translatedText = DictParser.getTranslateFromNaverAPI(selectedText);

            translatedTB.ReadOnly = true;
            translatedTB.BorderStyle = 0;
            translatedTB.BackColor = this.BackColor;
            translatedTB.TabStop = false;
            
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

        private void TranslateForm_Load(object sender, EventArgs e)
        {
            this.Location = Cursor.Position;
            translatedTB.Text = translatedText;

            Size sz = new Size(translatedTB.Width, int.MaxValue);
            TextFormatFlags flags = TextFormatFlags.WordBreak;
            int padding = 3;
            int borders = translatedTB.Height - translatedTB.ClientSize.Height;
            sz = TextRenderer.MeasureText(translatedTB.Text, translatedTB.Font, sz, flags);
            int h = sz.Height + borders + padding;
            translatedTB.Height = h;
        }

        private void TranslateForm_MouseEnter(object sender, EventArgs e)
        {
            this.Opacity = 1;
        }

        private void TranslateForm_MouseLeave(object sender, EventArgs e)
        {
            this.Opacity = 0.1;
        }
    }
}
