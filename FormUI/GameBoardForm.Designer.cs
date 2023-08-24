namespace FormUI
{
    public partial class GameBoardForm
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
            this.labelPlayer1NameAndScore = new System.Windows.Forms.Label();
            this.labelPlayer2NameAndScore = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelPlayer1NameAndScore
            // 
            this.labelPlayer1NameAndScore.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.labelPlayer1NameAndScore.AutoSize = true;
            this.labelPlayer1NameAndScore.Location = new System.Drawing.Point(264, 664);
            this.labelPlayer1NameAndScore.Name = "labelPlayer1NameAndScore";
            this.labelPlayer1NameAndScore.Size = new System.Drawing.Size(68, 17);
            this.labelPlayer1NameAndScore.TabIndex = 0;
            this.labelPlayer1NameAndScore.Text = "Player1:0";
            // 
            // labelPlayer2NameAndScore
            // 
            this.labelPlayer2NameAndScore.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.labelPlayer2NameAndScore.AutoSize = true;
            this.labelPlayer2NameAndScore.Location = new System.Drawing.Point(392, 664);
            this.labelPlayer2NameAndScore.Name = "labelPlayer2NameAndScore";
            this.labelPlayer2NameAndScore.Size = new System.Drawing.Size(68, 17);
            this.labelPlayer2NameAndScore.TabIndex = 1;
            this.labelPlayer2NameAndScore.Text = "Player2:0";
            // 
            // GameBoardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 712);
            this.Controls.Add(this.labelPlayer2NameAndScore);
            this.Controls.Add(this.labelPlayer1NameAndScore);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "GameBoardForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Damka";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelPlayer1NameAndScore;
        private System.Windows.Forms.Label labelPlayer2NameAndScore;
    }
}
