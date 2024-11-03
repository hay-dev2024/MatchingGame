using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Media; 

namespace MatchingGame
{
    public partial class Form1 : Form
    {
        Random random = new Random();

        // card pairs
        List<string> images = new List<string>()
        {
            "p1.png", "p1.png",
            "p2.png", "p2.png",
            "p3.png", "p3.png",
            "p4.png", "p4.png",
            "p5.png", "p5.png",
            "p6.png", "p6.png",
            "p7.png", "p7.png",
            "p8.png", "p8.png"
        };

        // Track the clicked PictureBoxes
        PictureBox firstClicked, secondClicked;

        // Music
        SoundPlayer music = new SoundPlayer("Audio/Circhester.wav");

        public Form1()
        {
            InitializeComponent();
            AssignIconsToSquares();

            // background music on loop
            music.PlayLooping();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void image_click(object sender, EventArgs e)
        {
            // no multiple click
            if (firstClicked != null && secondClicked != null) { return; }

            PictureBox clickedImg = sender as PictureBox;

            if (clickedImg == null || clickedImg.Image != null)
            {
                return;
            }

            // Show image for first clicked card
            if (firstClicked == null)
            {
                firstClicked = clickedImg;
                firstClicked.Image = Image.FromFile((string)firstClicked.Tag); // Show the image
                return;
            }

            // Show image for second clicked card
            secondClicked = clickedImg;
            secondClicked.Image = Image.FromFile((string)secondClicked.Tag);

            if (firstClicked.Tag.ToString() == secondClicked.Tag.ToString())
            {
                CheckForWinner(); // winner winner!
                firstClicked = null;
                secondClicked = null;
            }
            else
            {
                timer.Start();
            }
        }

        private void CheckForWinner()
        {
            // tracking matched pairs
            int matchedPairs = 0;
            PictureBox pic;

            for (int i = 0; i < tableLayoutPanel1.Controls.Count; i++)
            {
                pic = tableLayoutPanel1.Controls[i] as PictureBox;

                // check if the PictureBox exists and has an image
                if (pic != null && pic.Image != null)
                {
                    matchedPairs++; // if the image is not null
                }
            }

            if (matchedPairs == 16)
            {
                MessageBox.Show("피카피카! 피피카 피카피카츄!!", "게임 승리", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            // Stop the timer
            timer.Stop();

            // check match
            if (firstClicked.Tag.ToString() != secondClicked.Tag.ToString())
            {
                // hide the images again
                firstClicked.Image = null;
                secondClicked.Image = null;
            }

            // Reset
            firstClicked = null;
            secondClicked = null;
        }

        private void AssignIconsToSquares()
        {
            PictureBox img;
            int randomNumber;

            for (int i = 0; i < tableLayoutPanel1.Controls.Count; i++)
            {
                if (tableLayoutPanel1.Controls[i] is PictureBox)
                {
                    img = (PictureBox)tableLayoutPanel1.Controls[i];
                }
                else
                {
                    continue;
                }
                randomNumber = random.Next(0, images.Count);

                // image path
                img.Tag = $"Images/{images[randomNumber]}";

                // prevent duplicates
                images.RemoveAt(randomNumber);

                //img.SizeMode = PictureBoxSizeMode.StretchImage;
                
                // hide images 
                img.Image = null;

                // click event handler to each PictureBox
                img.Click += image_click;
            }
        }
    }
}
