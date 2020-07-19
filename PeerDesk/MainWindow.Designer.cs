namespace PeerDesk
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.textIp = new System.Windows.Forms.TextBox();
            this.titleLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.connectBtn = new System.Windows.Forms.Button();
            this.titleLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // textIp
            // 
            this.textIp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textIp.AutoCompleteCustomSource.AddRange(new string[] {
            "127.0.0.1"});
            this.textIp.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textIp.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
            this.textIp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textIp.Font = new System.Drawing.Font("SimSun", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textIp.Location = new System.Drawing.Point(122, 21);
            this.textIp.MaxLength = 20;
            this.textIp.Name = "textIp";
            this.textIp.Size = new System.Drawing.Size(711, 29);
            this.textIp.TabIndex = 0;
            this.textIp.Text = "127.0.0.1";
            this.textIp.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textIp_KeyUp);
            // 
            // titleLayoutPanel
            // 
            this.titleLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.titleLayoutPanel.BackColor = System.Drawing.Color.Transparent;
            this.titleLayoutPanel.BackgroundImage = global::PeerDesk.Properties.Resources.noisy_texture_F9F9F9;
            this.titleLayoutPanel.ColumnCount = 3;
            this.titleLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 119F));
            this.titleLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.titleLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 108F));
            this.titleLayoutPanel.Controls.Add(this.textIp, 1, 0);
            this.titleLayoutPanel.Controls.Add(this.label1, 0, 0);
            this.titleLayoutPanel.Controls.Add(this.connectBtn, 2, 0);
            this.titleLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.titleLayoutPanel.Name = "titleLayoutPanel";
            this.titleLayoutPanel.RowCount = 1;
            this.titleLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.titleLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 71F));
            this.titleLayoutPanel.Size = new System.Drawing.Size(944, 71);
            this.titleLayoutPanel.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.CausesValidation = false;
            this.label1.Font = new System.Drawing.Font("SimSun", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(3, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "Remote IP";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // connectBtn
            // 
            this.connectBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.connectBtn.Font = new System.Drawing.Font("SimSun", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.connectBtn.Location = new System.Drawing.Point(839, 19);
            this.connectBtn.Name = "connectBtn";
            this.connectBtn.Size = new System.Drawing.Size(102, 32);
            this.connectBtn.TabIndex = 2;
            this.connectBtn.TabStop = false;
            this.connectBtn.Text = "Connect";
            this.connectBtn.UseVisualStyleBackColor = true;
            this.connectBtn.Click += new System.EventHandler(this.connectBtn_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 501);
            this.Controls.Add(this.titleLayoutPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainWindow";
            this.Text = "PeerDesk";
            this.titleLayoutPanel.ResumeLayout(false);
            this.titleLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textIp;
        private System.Windows.Forms.TableLayoutPanel titleLayoutPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button connectBtn;
    }
}

