
namespace MelodyImpact
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tbFile = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.rtbParse = new System.Windows.Forms.RichTextBox();
            this.tbDelay = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbOffset = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbTrack = new System.Windows.Forms.TextBox();
            this.cbAllTrack = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbStartAt = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tbFile
            // 
            this.tbFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFile.Location = new System.Drawing.Point(12, 14);
            this.tbFile.Name = "tbFile";
            this.tbFile.ReadOnly = true;
            this.tbFile.Size = new System.Drawing.Size(224, 20);
            this.tbFile.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(242, 12);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(81, 23);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPlay.Location = new System.Drawing.Point(174, 40);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(149, 99);
            this.btnPlay.TabIndex = 5;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // rtbParse
            // 
            this.rtbParse.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbParse.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbParse.Location = new System.Drawing.Point(12, 145);
            this.rtbParse.Name = "rtbParse";
            this.rtbParse.ReadOnly = true;
            this.rtbParse.Size = new System.Drawing.Size(311, 81);
            this.rtbParse.TabIndex = 6;
            this.rtbParse.Text = "";
            // 
            // tbDelay
            // 
            this.tbDelay.Location = new System.Drawing.Point(62, 42);
            this.tbDelay.Name = "tbDelay";
            this.tbDelay.Size = new System.Drawing.Size(106, 20);
            this.tbDelay.TabIndex = 2;
            this.tbDelay.Text = "200";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Delay";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Offset";
            // 
            // tbOffset
            // 
            this.tbOffset.Location = new System.Drawing.Point(62, 68);
            this.tbOffset.Name = "tbOffset";
            this.tbOffset.Size = new System.Drawing.Size(106, 20);
            this.tbOffset.TabIndex = 3;
            this.tbOffset.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Track";
            // 
            // tbTrack
            // 
            this.tbTrack.Enabled = false;
            this.tbTrack.Location = new System.Drawing.Point(62, 94);
            this.tbTrack.Name = "tbTrack";
            this.tbTrack.Size = new System.Drawing.Size(63, 20);
            this.tbTrack.TabIndex = 4;
            this.tbTrack.Text = "0";
            // 
            // cbAllTrack
            // 
            this.cbAllTrack.AutoSize = true;
            this.cbAllTrack.Checked = true;
            this.cbAllTrack.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAllTrack.Location = new System.Drawing.Point(131, 96);
            this.cbAllTrack.Name = "cbAllTrack";
            this.cbAllTrack.Size = new System.Drawing.Size(37, 17);
            this.cbAllTrack.TabIndex = 11;
            this.cbAllTrack.Text = "All";
            this.cbAllTrack.UseVisualStyleBackColor = true;
            this.cbAllTrack.CheckedChanged += new System.EventHandler(this.cbAllTrack_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Start At";
            // 
            // tbStartAt
            // 
            this.tbStartAt.Location = new System.Drawing.Point(62, 120);
            this.tbStartAt.Name = "tbStartAt";
            this.tbStartAt.Size = new System.Drawing.Size(106, 20);
            this.tbStartAt.TabIndex = 13;
            this.tbStartAt.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 238);
            this.Controls.Add(this.tbStartAt);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbAllTrack);
            this.Controls.Add(this.tbTrack);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbOffset);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbDelay);
            this.Controls.Add(this.rtbParse);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.tbFile);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Melody Impact";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbFile;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.RichTextBox rtbParse;
        private System.Windows.Forms.TextBox tbDelay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbOffset;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbTrack;
        private System.Windows.Forms.CheckBox cbAllTrack;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbStartAt;
    }
}

