namespace TournamentSoftwareLoader
{
    partial class MainForm
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
            this.m_guidLabel = new System.Windows.Forms.Label();
            this.m_guidEdit = new System.Windows.Forms.TextBox();
            this.m_translitCheck = new System.Windows.Forms.CheckBox();
            this.m_prepeareParticipant = new System.Windows.Forms.CheckBox();
            this.m_startButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_guidLabel
            // 
            this.m_guidLabel.AutoSize = true;
            this.m_guidLabel.Location = new System.Drawing.Point(8, 11);
            this.m_guidLabel.Name = "m_guidLabel";
            this.m_guidLabel.Size = new System.Drawing.Size(80, 13);
            this.m_guidLabel.TabIndex = 0;
            this.m_guidLabel.Text = "GUID турнира:";
            // 
            // m_guidEdit
            // 
            this.m_guidEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_guidEdit.Location = new System.Drawing.Point(98, 8);
            this.m_guidEdit.Name = "m_guidEdit";
            this.m_guidEdit.Size = new System.Drawing.Size(361, 20);
            this.m_guidEdit.TabIndex = 1;
            // 
            // m_translitCheck
            // 
            this.m_translitCheck.AutoSize = true;
            this.m_translitCheck.Location = new System.Drawing.Point(12, 34);
            this.m_translitCheck.Name = "m_translitCheck";
            this.m_translitCheck.Size = new System.Drawing.Size(186, 17);
            this.m_translitCheck.TabIndex = 2;
            this.m_translitCheck.Text = "Использовать транслитерацию";
            this.m_translitCheck.UseVisualStyleBackColor = true;
            // 
            // m_prepeareParticipant
            // 
            this.m_prepeareParticipant.AutoSize = true;
            this.m_prepeareParticipant.Location = new System.Drawing.Point(11, 57);
            this.m_prepeareParticipant.Name = "m_prepeareParticipant";
            this.m_prepeareParticipant.Size = new System.Drawing.Size(167, 17);
            this.m_prepeareParticipant.TabIndex = 3;
            this.m_prepeareParticipant.Text = "Имя стоит перед фамилией";
            this.m_prepeareParticipant.UseVisualStyleBackColor = true;
            // 
            // m_startButton
            // 
            this.m_startButton.Location = new System.Drawing.Point(11, 80);
            this.m_startButton.Name = "m_startButton";
            this.m_startButton.Size = new System.Drawing.Size(75, 23);
            this.m_startButton.TabIndex = 4;
            this.m_startButton.Text = "Пуск";
            this.m_startButton.UseVisualStyleBackColor = true;
            this.m_startButton.Click += new System.EventHandler(this.m_startButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 282);
            this.Controls.Add(this.m_startButton);
            this.Controls.Add(this.m_prepeareParticipant);
            this.Controls.Add(this.m_translitCheck);
            this.Controls.Add(this.m_guidEdit);
            this.Controls.Add(this.m_guidLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.Text = "Выгрузка из TournamentSoftware";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label m_guidLabel;
        private System.Windows.Forms.TextBox m_guidEdit;
        private System.Windows.Forms.CheckBox m_translitCheck;
        private System.Windows.Forms.CheckBox m_prepeareParticipant;
        private System.Windows.Forms.Button m_startButton;
    }
}

