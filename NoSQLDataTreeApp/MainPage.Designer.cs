using System.Windows.Forms;

namespace NoSQLDataTreeApp
{
    partial class MainPage
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelRoot = new System.Windows.Forms.Panel();
            this.contextMenuStripRoot = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addChildToRoot = new System.Windows.Forms.ToolStripMenuItem();
            this.labelRoot = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelRoot.SuspendLayout();
            this.contextMenuStripRoot.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.Controls.Add(this.panelRoot, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(785, 700);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panelRoot
            // 
            this.panelRoot.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panelRoot.ContextMenuStrip = this.contextMenuStripRoot;
            this.panelRoot.Controls.Add(this.labelRoot);
            this.panelRoot.Location = new System.Drawing.Point(3, 3);
            this.panelRoot.Name = "panelRoot";
            this.tableLayoutPanel1.SetRowSpan(this.panelRoot, 2);
            this.panelRoot.Size = new System.Drawing.Size(69, 54);
            this.panelRoot.TabIndex = 0;
            // 
            // contextMenuStripRoot
            // 
            this.contextMenuStripRoot.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addChildToRoot});
            this.contextMenuStripRoot.Name = "contextMenuStripRoot";
            this.contextMenuStripRoot.Size = new System.Drawing.Size(128, 26);
            // 
            // addChildToRoot
            // 
            this.addChildToRoot.Name = "addChildToRoot";
            this.addChildToRoot.Size = new System.Drawing.Size(127, 22);
            this.addChildToRoot.Text = "Add Child";
            this.addChildToRoot.Click += new System.EventHandler(this.AddChildToRoot_Click);
            // 
            // labelRoot
            // 
            this.labelRoot.AutoSize = true;
            this.labelRoot.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRoot.Location = new System.Drawing.Point(17, 19);
            this.labelRoot.Name = "labelRoot";
            this.labelRoot.Size = new System.Drawing.Size(37, 16);
            this.labelRoot.TabIndex = 0;
            this.labelRoot.Text = "Root";
            // 
            // MainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(785, 700);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainPage";
            this.Text = "NoSQL Data Tree Designer";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelRoot.ResumeLayout(false);
            this.panelRoot.PerformLayout();
            this.contextMenuStripRoot.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Label labelRoot;
        private Panel panelRoot;
        private ContextMenuStrip contextMenuStripRoot;
        private ToolStripMenuItem addChildToRoot;
   
    }
}

