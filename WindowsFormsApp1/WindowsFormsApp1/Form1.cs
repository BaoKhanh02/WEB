using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private List<Button> buttons = new List<Button>();
        private Button firstClick = null;
        private Button secondClick = null;
        private Timer flipBackTimer = new Timer();
        private int score = 1000;
        private int timeElapsed = 0;
        private Timer gameTimer = new Timer();

        private string[] icons = new string[]
        {
            "A","A","B","B","C","C","D","D"
        };

        public Form1()
        {
            InitializeComponent();

            // timer cho viec lat sai
            flipBackTimer.Interval = 1000;
            flipBackTimer.Tick += FlipBackTimer_Tick;

            // timer tinh thoi gian
            gameTimer.Interval = 1000;
            gameTimer.Tick += GameTimer_Tick;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StartGame();
        }

        private void StartGame()
        {
            score = 1000;
            timeElapsed = 0;
            firstClick = null;
            secondClick = null;

            gameTimer.Start();

            // tron ngau nhien khong dung LINQ
            ShuffleArray(icons);

            // tao grid 4x2
            int rows = 2, cols = 4, size = 100;
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    Button btn = new Button();
                    btn.Size = new Size(size, size);
                    btn.Location = new Point(c * (size + 5), r * (size + 5) + 40);
                    btn.Font = new Font(FontFamily.GenericSansSerif, 24, FontStyle.Bold);
                    btn.Text = "?";
                    btn.Tag = icons[r * cols + c];
                    btn.Click += Card_Click;
                    this.Controls.Add(btn);
                    buttons.Add(btn);
                }
            }
        }

        private void Card_Click(object sender, EventArgs e)
        {
            if (flipBackTimer.Enabled) return;

            Button clickedButton = sender as Button;
            if (clickedButton == null || clickedButton.Text != "?") return;

            if (firstClick == null)
            {
                firstClick = clickedButton;
                firstClick.Text = firstClick.Tag.ToString();
                return;
            }

            secondClick = clickedButton;
            secondClick.Text = secondClick.Tag.ToString();

            if (firstClick.Tag.ToString() == secondClick.Tag.ToString())
            {
                firstClick = null;
                secondClick = null;

                if (CheckWin())
                {
                    gameTimer.Stop();
                    MessageBox.Show("Ban da thang!\nThoi gian: " + timeElapsed + " giay\nDiem: " + score);
                }
            }
            else
            {
                score -= 10;
                flipBackTimer.Start();
            }
        }

        private void FlipBackTimer_Tick(object sender, EventArgs e)
        {
            flipBackTimer.Stop();
            if (firstClick != null) firstClick.Text = "?";
            if (secondClick != null) secondClick.Text = "?";
            firstClick = null;
            secondClick = null;
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            timeElapsed++;
            score = Math.Max(0, score - 1);
            this.Text = "Memory Game - Time: " + timeElapsed + "s | Score: " + score;
        }

        private bool CheckWin()
        {
            foreach (Button b in buttons)
            {
                if (b.Text == "?")
                {
                    return false;
                }
            }
            return true;
        }

        // Tron mang thu cong
        private void ShuffleArray(string[] array)
        {
            Random rnd = new Random();
            for (int i = array.Length - 1; i > 0; i--)
            {
                int j = rnd.Next(i + 1);
                string temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }
        }
    }
}
