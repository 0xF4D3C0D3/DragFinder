namespace DragFinder
{
    partial class TranslateForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.translatedTB = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // translatedTB
            // 
            this.translatedTB.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.translatedTB.Location = new System.Drawing.Point(0, 0);
            this.translatedTB.Multiline = true;
            this.translatedTB.Name = "translatedTB";
            this.translatedTB.ReadOnly = true;
            this.translatedTB.Size = new System.Drawing.Size(500, 10);
            this.translatedTB.TabIndex = 0;
            // 
            // TranslateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(116, 0);
            this.Controls.Add(this.translatedTB);
            this.Name = "TranslateForm";
            this.Text = "TranslateForm";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.TranslateForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox translatedTB;
    }
}