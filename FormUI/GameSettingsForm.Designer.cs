namespace FormUI
{
    public partial class GameSettingsForm
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
            this.labelBoardSize = new System.Windows.Forms.Label();
            this.labelPlayers = new System.Windows.Forms.Label();
            this.labelPlayer1 = new System.Windows.Forms.Label();
            this.checkBoxplayerOrComputer = new System.Windows.Forms.CheckBox();
            this.textBoxPlayer1Name = new System.Windows.Forms.TextBox();
            this.textBoxPlayer2Name = new System.Windows.Forms.TextBox();
            this.radioButtonMinimum = new System.Windows.Forms.RadioButton();
            this.radioButtonMedium = new System.Windows.Forms.RadioButton();
            this.radioButtonMaximum = new System.Windows.Forms.RadioButton();
            this.buttonDone = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelBoardSize
            // 
            this.labelBoardSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelBoardSize.Location = new System.Drawing.Point(15, 109);
            this.labelBoardSize.Name = "labelBoardSize";
            this.labelBoardSize.Size = new System.Drawing.Size(154, 23);
            this.labelBoardSize.TabIndex = 7;
            this.labelBoardSize.Text = "Board Size:";
            // 
            // labelPlayers
            // 
            this.labelPlayers.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelPlayers.Location = new System.Drawing.Point(19, 172);
            this.labelPlayers.Name = "labelPlayers";
            this.labelPlayers.Size = new System.Drawing.Size(97, 23);
            this.labelPlayers.TabIndex = 8;
            this.labelPlayers.Text = "Players:";
            // 
            // labelPlayer1
            // 
            this.labelPlayer1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelPlayer1.Location = new System.Drawing.Point(30, 203);
            this.labelPlayer1.Name = "labelPlayer1";
            this.labelPlayer1.Size = new System.Drawing.Size(100, 23);
            this.labelPlayer1.TabIndex = 9;
            this.labelPlayer1.Text = "Player1:";
            // 
            // checkBoxplayerOrComputer
            // 
            this.checkBoxplayerOrComputer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.checkBoxplayerOrComputer.Location = new System.Drawing.Point(12, 230);
            this.checkBoxplayerOrComputer.Name = "checkBoxplayerOrComputer";
            this.checkBoxplayerOrComputer.Size = new System.Drawing.Size(104, 24);
            this.checkBoxplayerOrComputer.TabIndex = 6;
            this.checkBoxplayerOrComputer.Text = "Player2:";
            this.checkBoxplayerOrComputer.UseVisualStyleBackColor = true;
            this.checkBoxplayerOrComputer.CheckedChanged += new System.EventHandler(this.checkBoxplayerOrComputer_CheckedChanged);
            // 
            // textBoxPlayer1Name
            // 
            this.textBoxPlayer1Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.textBoxPlayer1Name.Location = new System.Drawing.Point(102, 199);
            this.textBoxPlayer1Name.Name = "textBoxPlayer1Name";
            this.textBoxPlayer1Name.Size = new System.Drawing.Size(100, 24);
            this.textBoxPlayer1Name.TabIndex = 5;
            // 
            // textBoxPlayer2Name
            // 
            this.textBoxPlayer2Name.AcceptsTab = true;
            this.textBoxPlayer2Name.Enabled = false;
            this.textBoxPlayer2Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.textBoxPlayer2Name.Location = new System.Drawing.Point(102, 229);
            this.textBoxPlayer2Name.Name = "textBoxPlayer2Name";
            this.textBoxPlayer2Name.Size = new System.Drawing.Size(100, 24);
            this.textBoxPlayer2Name.TabIndex = 4;
            this.textBoxPlayer2Name.Text = "[Computer]";
            // 
            // radioButtonMinimum
            // 
            this.radioButtonMinimum.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.radioButtonMinimum.Location = new System.Drawing.Point(22, 135);
            this.radioButtonMinimum.Name = "radioButtonMinimum";
            this.radioButtonMinimum.Size = new System.Drawing.Size(74, 24);
            this.radioButtonMinimum.TabIndex = 10;
            this.radioButtonMinimum.Text = "6x6";
            this.radioButtonMinimum.CheckedChanged += new System.EventHandler(this.checkRadioButtonSize_Checked);
            // 
            // radioButtonMedium
            // 
            this.radioButtonMedium.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.radioButtonMedium.Location = new System.Drawing.Point(102, 135);
            this.radioButtonMedium.Name = "radioButtonMedium";
            this.radioButtonMedium.Size = new System.Drawing.Size(67, 24);
            this.radioButtonMedium.TabIndex = 2;
            this.radioButtonMedium.Text = "8x8";
            this.radioButtonMedium.CheckedChanged += new System.EventHandler(this.checkRadioButtonSize_Checked);
            // 
            // radioButtonMaximum
            // 
            this.radioButtonMaximum.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.radioButtonMaximum.Location = new System.Drawing.Point(175, 135);
            this.radioButtonMaximum.Name = "radioButtonMaximum";
            this.radioButtonMaximum.Size = new System.Drawing.Size(84, 24);
            this.radioButtonMaximum.TabIndex = 3;
            this.radioButtonMaximum.Text = "10x10";
            this.radioButtonMaximum.CheckedChanged += new System.EventHandler(this.checkRadioButtonSize_Checked);
            // 
            // buttonDone
            // 
            this.buttonDone.BackColor = System.Drawing.Color.SkyBlue;
            this.buttonDone.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.buttonDone.Location = new System.Drawing.Point(253, 284);
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.Size = new System.Drawing.Size(134, 36);
            this.buttonDone.TabIndex = 0;
            this.buttonDone.Text = "Start Playing!";
            this.buttonDone.UseVisualStyleBackColor = false;
            this.buttonDone.Click += new System.EventHandler(this.button_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::FormUI.Properties.Resources.start_game_picture;
            this.pictureBox1.Location = new System.Drawing.Point(432, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(245, 183);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(8, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(361, 60);
            this.label1.TabIndex = 12;
            this.label1.Text = "Welcome!\r\n\r\nPlease choose from the following options:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // GameSettingsForm
            // 
            this.AcceptButton = this.buttonDone;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(689, 346);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.buttonDone);
            this.Controls.Add(this.radioButtonMinimum);
            this.Controls.Add(this.radioButtonMedium);
            this.Controls.Add(this.radioButtonMaximum);
            this.Controls.Add(this.textBoxPlayer2Name);
            this.Controls.Add(this.textBoxPlayer1Name);
            this.Controls.Add(this.checkBoxplayerOrComputer);
            this.Controls.Add(this.labelBoardSize);
            this.Controls.Add(this.labelPlayers);
            this.Controls.Add(this.labelPlayer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameSettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game Settings:";
            this.Load += new System.EventHandler(this.GameSettingsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelBoardSize;
        private System.Windows.Forms.RadioButton radioButtonMinimum;
        private System.Windows.Forms.RadioButton radioButtonMedium;
        private System.Windows.Forms.RadioButton radioButtonMaximum;
        private System.Windows.Forms.Label labelPlayers;
        private System.Windows.Forms.Label labelPlayer1;
        private System.Windows.Forms.TextBox textBoxPlayer1Name;
        private System.Windows.Forms.TextBox textBoxPlayer2Name;
        private System.Windows.Forms.CheckBox checkBoxplayerOrComputer;
        private System.Windows.Forms.Button buttonDone;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
    }
}
