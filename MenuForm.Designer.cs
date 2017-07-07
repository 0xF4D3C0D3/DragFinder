namespace DragFinder
{
    partial class MenuForm
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
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.simpleMeaningsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(103, 26);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            // 
            // simpleMeaningsPanel
            // 
            this.simpleMeaningsPanel.AutoScroll = true;
            this.simpleMeaningsPanel.AutoSize = true;
            this.simpleMeaningsPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.simpleMeaningsPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.simpleMeaningsPanel.Location = new System.Drawing.Point(0, 0);
            this.simpleMeaningsPanel.MaximumSize = new System.Drawing.Size(0, 200);
            this.simpleMeaningsPanel.Name = "simpleMeaningsPanel";
            this.simpleMeaningsPanel.Size = new System.Drawing.Size(0, 0);
            this.simpleMeaningsPanel.TabIndex = 2;
            this.simpleMeaningsPanel.WrapContents = false;
            this.simpleMeaningsPanel.MouseEnter += new System.EventHandler(this.MenuForm_MouseHover);
            this.simpleMeaningsPanel.MouseLeave += new System.EventHandler(this.MenuForm_MouseLeave);
            // 
            // MenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(116, 0);
            this.Controls.Add(this.simpleMeaningsPanel);
            this.Name = "MenuForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "MenuForm";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.MenuForm_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.FlowLayoutPanel simpleMeaningsPanel;
    }
}