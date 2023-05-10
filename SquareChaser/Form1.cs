using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SquareChaser
{
    public partial class Form1 : Form
    {
        private int player1Speed = 5;
        private int player2Speed = 5;
        private int player1Score = 0;
        private int player2Score = 0;
        private bool player1Boosted = false;
        private bool player2Boosted = false;
        private Random random = new Random();
        private Rectangle player1Rect;
        private Rectangle player2Rect;
        private Rectangle squareRect;
        private Rectangle boostRect;
        private SoundPlayer scoreSound = new SoundPlayer("C:\\Users\\emmsg\\Downloads\\score.wav.wav");
        private SoundPlayer collisionSound = new SoundPlayer("C:\\Users\\emmsg\\Downloads\\collision.wav.wav");
        private SoundPlayer boostSound = new SoundPlayer("C:\\Users\\emmsg\\Downloads\\boost.wav.wav");
        bool wDown = false;
        bool aDown = false;
        bool sDown = false;
        bool dDown = false;
        bool upArrowDown = false;
        bool downArrowDown = false;
        bool leftDown = false;
        bool rightDown = false;
        public Form1()
        {
            InitializeComponent();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            // Set the initial positions of the players, square, and boost
            player1Rect = new Rectangle(50, 50, 30, 30);
            player2Rect = new Rectangle(50, 100, 30, 30);
            squareRect = new Rectangle(random.Next(0, this.ClientSize.Width - 30), random.Next(0, this.ClientSize.Height - 30), 30, 30);
            boostRect = new Rectangle(random.Next(0, this.ClientSize.Width - 30), random.Next(0, this.ClientSize.Height - 30), 30, 30);
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            // Draw the players, square, and boost
            e.Graphics.FillRectangle(Brushes.Red, player1Rect);
            e.Graphics.FillRectangle(Brushes.Blue, player2Rect);
            e.Graphics.FillRectangle(Brushes.Green, squareRect);
            e.Graphics.FillRectangle(Brushes.Yellow, boostRect);

            // Draw the scores
            //e.Graphics.DrawString("Player 1: " + player1Score, Font, Brushes.White, 10, 10);
            //e.Graphics.DrawString("Player 2: " + player2Score, Font, Brushes.White, this.ClientSize.Width - 100, 10);
            label1.Text = "Player 1: " + player1Score;
            label2.Text = "Player 2: " + player2Score;
            if (gameTimer.Enabled == false)
            {
                if (player1Score == 10)
                {
                    label1.Text = ("Player 1 Wins!");
                    
                }
                else if (player2Score == 10)
                {
                    label2.Text = ("Player 2 Wins!");
                    
                }
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Stop all sounds when the form is closing
            scoreSound.Stop();
            collisionSound.Stop();
            boostSound.Stop();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.A:
                    aDown = false;
                    break;
                case Keys.D:
                    dDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.Left:
                    leftDown = false;
                    break;
                case Keys.Right:
                    rightDown = false;
                    break;
            }
        }

        private void Form1_KeyDown_1(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.A:
                    aDown = true;
                    break;
                case Keys.D:
                    dDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.Left:
                    leftDown = true;
                    break;
                case Keys.Right:
                    rightDown = true;
                    break;
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {

            // Check for collisions with the square
            if (player1Rect.IntersectsWith(squareRect))
            {
                player1Score++;
                scoreSound.Play();
                squareRect.X = random.Next(0, this.ClientSize.Width - 30);
                squareRect.Y = random.Next(0, this.ClientSize.Height - 30);
            }
            if (player2Rect.IntersectsWith(squareRect))
            {
                player2Score++;
                scoreSound.Play();
                squareRect.X = random.Next(0, this.ClientSize.Width - 30);
                squareRect.Y = random.Next(0, this.ClientSize.Height - 30);
            }
            // Check for collisions with the boost
            if (player1Rect.IntersectsWith(boostRect))
            {
                player1Speed += 2;
                boostRect.X = random.Next(0, this.ClientSize.Width - 30);
                boostRect.Y = random.Next(0, this.ClientSize.Height - 30);
                boostSound.Play();
            }
            if (player2Rect.IntersectsWith(boostRect))
            {
                player2Speed += 1;
                boostRect.X = random.Next(0, this.ClientSize.Width - 30);
                boostRect.Y = random.Next(0, this.ClientSize.Height - 30);
                boostSound.Play();
            }
            // Check if player 1 or player 2 has won
            if (player1Score == 10)
            {
                label1.Text = ("Player 1 Wins!");
                gameTimer.Stop();
            }
            else if (player2Score == 10)
            {
                label2.Text = ("Player 2 Wins!");
                gameTimer.Stop();
            }

            // Check for collisions between the players
            if (player1Rect.IntersectsWith(player2Rect))
            {
                collisionSound.Play();

                // Move player 1 out of the way
                if (player1Rect.X < player2Rect.X)
                {
                    player1Rect.X -= player1Speed;
                    player2Rect.X += player2Speed;
                }
                else
                {
                    player1Rect.X += player1Speed;
                    player2Rect.X -= player2Speed;
                }

                if (player1Rect.Y < player2Rect.Y)
                {
                    player1Rect.Y -= player1Speed;
                    player2Rect.Y += player2Speed;
                }
                else
                {
                    player1Rect.Y += player1Speed;
                    player2Rect.Y -= player2Speed;
                }
            }

            // Move the boost and square if they are stuck inside the players
            //move player 1
            if (wDown == true && player1Rect.Y > 0)
            {
                player1Rect.Y -= player1Speed;
            }

            if (sDown == true && player1Rect.Y < this.Height - player1Rect.Height)
            {
                player1Rect.Y += player1Speed;
            }

            if (aDown == true && player1Rect.X > 0)
            {
                player1Rect.X -= player1Speed;
            }

            if (dDown == true && player1Rect.X < this.Width - player1Rect.Width)
            {
                player1Rect.X += player1Speed;
            }

            //move player 2

            if (upArrowDown == true && player2Rect.Y > 0)
            {
                player2Rect.Y -= player2Speed;
            }

            if (downArrowDown == true && player2Rect.Y < this.Height - player2Rect.Height)
            {
                player2Rect.Y += player2Speed;
            }

            if (leftDown == true && player2Rect.X > 0)
            {
                player2Rect.X -= player2Speed;
            }

            if (rightDown == true && player2Rect.X < this.Width - player2Rect.Width)
            {
                player2Rect.X += player2Speed;
            }

            /*
            if (player1Rect.IntersectsWith(boostRect) || player1Rect.IntersectsWith(squareRect))
            {
                

                
            }

            if (player2Rect.IntersectsWith(boostRect) || player2Rect.IntersectsWith(squareRect))
            {
                boostRect.X = random.Next(0, this.ClientSize.Width - 30);
                boostRect.Y = random.Next(0, this.ClientSize.Height - 30);

                squareRect.X = random.Next(0, this.ClientSize.Width - 30);
                squareRect.Y = random.Next(0, this.ClientSize.Height - 30);
            }*/

            // Redraw the screen
            Refresh();
        }
    }
}

