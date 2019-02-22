using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoxField
{
    public partial class GameScreen : UserControl
    {
        //player1 button control keys
        Boolean leftArrowDown, rightArrowDown;

        //used to draw boxes on screen
        SolidBrush boxBrush = new SolidBrush(Color.White);

        List<BoxClass> boxesLeft = new List<BoxClass>();
        List<BoxClass> boxesRight = new List<BoxClass>();

        BoxClass player;

        public int red, green, blue;
        Random randGen = new Random();
        int boxCounter;
        int boxSpeed = 10;
        

        public GameScreen()
        {
            InitializeComponent();
            OnStart();
        }

        /// <summary>
        /// Set initial game values here
        /// </summary>
        public void OnStart()
        {
            //TODO - set game start values
            BoxClass b1 = new BoxClass(25, 25, 20);
            BoxClass b2 = new BoxClass(Width - 25, 25, 20);
            player = new BoxClass(Height - 20, Width / 2, 15);
            boxesRight.Add(b2);
            boxesLeft.Add(b1);
            
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //player 1 button presses
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;           
            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            //player 1 button releases
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
            }
        }

        private void gameLoop_Tick(object sender, EventArgs e)
        {
            //TODO - update location of all boxes (drop down screen)
            foreach (BoxClass b in boxesLeft)
            {
                b.Move(boxSpeed);
            }
            foreach (BoxClass b in boxesRight)
            {
                b.Move(boxSpeed);
            }
            if (rightArrowDown)
            {
                player.Move(boxSpeed, "right");
            }
            if (leftArrowDown)
            {
                player.Move(boxSpeed, "left");
            }

            //Check for collision between player and boxes
            foreach(BoxClass b in boxesLeft.Union(boxesRight))
            {
                if (player.Collison(b))
                {
                    gameLoop.Stop();
                }
            }


            //TODO - remove box if it has gone of screen
            if (boxesLeft[0].y > this.Height - 100)
            {
                boxesLeft.RemoveAt(0);
            }
            if (boxesRight[0].y > this.Height - 100)
            {
                boxesRight.RemoveAt(0);
            }
            //TODO - add new box if it is time
            boxCounter++;

            if (boxCounter % 5 == 0)
            {
                BoxClass b1 = new BoxClass(25, 25, 20);
                boxesLeft.Add(b1);
                BoxClass b2 = new BoxClass(Width - 80, 25, 20);
                boxesRight.Add(b2);
                // boxCounter = 0;
            }
            Refresh();
        }
        
     
        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            //TODO - draw boxes to screen
            //for (int i = 0; i < boxesLeft.Count; i ++)
            //{
            //    e.Graphics.FillRectangle(boxBrush, boxesLeft[i].x, boxesLeft[i].y, boxesLeft[i].size, boxesLeft[i].size);
            //}

            

            foreach (BoxClass b in boxesLeft)
            {
                e.Graphics.FillRectangle(boxBrush, b.x, b.y, b.size, b.size);
                e.Graphics.FillRectangle(boxBrush, b.x, b.y, b.size, b.size);
            }
            foreach (BoxClass b in boxesRight)
            {
                e.Graphics.FillRectangle(boxBrush, b.x, b.y, b.size, b.size);
                e.Graphics.FillRectangle(boxBrush, b.x, b.y, b.size, b.size);
            }
            
                e.Graphics.FillEllipse(boxBrush, player.x, player.y, player.size, player.size);
            
        }
    }
}
