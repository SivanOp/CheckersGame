using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GameLogic;

namespace FormUI
{
    public partial class GameSettingsForm : Form
    {
        private string m_Player1Name = string.Empty;
        private string m_Player2Name = string.Empty;
        private int m_BoardSize;
        private bool m_GameStart = false;

        public GameSettingsForm()
        {
            InitializeComponent();
        }

        private void checkBoxplayerOrComputer_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;

            if (checkBox.Checked)
            {
                textBoxPlayer2Name.Text = string.Empty;
                textBoxPlayer2Name.Enabled = true;
            }

            else
            {
                textBoxPlayer2Name.Text = "[Computer]";
                textBoxPlayer2Name.Enabled = false;
            }
        }

        private void checkRadioButtonSize_Checked(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;

            if (string.Equals(radioButton.Text, "6x6"))
            {
                m_BoardSize = 6;
            }

            else if (string.Equals(radioButton.Text, "8x8"))
            {
                m_BoardSize = 8;
            }

            else
            {
                m_BoardSize = 10;
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            m_Player1Name = textBoxPlayer1Name.Text;
            m_Player2Name = textBoxPlayer2Name.Text;

            if( !radioButtonMaximum.Checked && !radioButtonMedium.Checked && !radioButtonMinimum.Checked)
            {
                MessageBox.Show("Please choose the board size to start playing!"," ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                m_GameStart = true;
                this.Close();
            } 
        }

        public int SizeOfBoard
        {
            get
            {
                return m_BoardSize;
            }
        }

        public string Player1Name
        {
            get
            {
                return m_Player1Name;
            }
        }

        public string Player2Name
        {
            get
            {
                return m_Player2Name;
            }
        }

        public bool GameStart
        {
            get
            {
                return m_GameStart;
            }
        }

        private void GameSettingsForm_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
