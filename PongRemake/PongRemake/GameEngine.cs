﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PongRemake
{
    class GameEngine
    {
        public PongBall ball;
        public Player playerOne;
        public Player playerTwo;
        public bool multiplayer;

        public GameEngine()
        {
            playerOne = new Player(false, true);
            playerTwo = new Player(true, false);
            ball = new PongBall();
            multiplayer = false;
        }

        public int timer = 0;
        public void Update()
        {
            
            if (timer != 60)
                timer++;
            else
            {
                if (ball.isAlive)
                {
                    ball.Update();
                    playerOne.Update(ball);
                    playerTwo.Update(ball);
                    ball.CheckPlayerCollision(playerOne, ball);
                    ball.CheckPlayerCollision(playerTwo, ball);
                    ball.CheckPastPlayer(playerOne, playerTwo);
                }
                else
                { 
                    ////TODO: Play sound for scoring a point
                    playerOne.ResetPosition();
                    playerTwo.ResetPosition();
                    ball.Reset();
                    timer = -60;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (ball.drawBall)
                spriteBatch.Draw(ball.texture, ball.position, ball.color);
            
            spriteBatch.Draw(playerOne.texture, playerOne.position, playerOne.color);
            spriteBatch.Draw(playerTwo.texture, playerTwo.position, playerTwo.color);

            Color[] color = new Color[] { Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Yellow, Color.Purple };   
            for (int i = 0; i < 6; i++)
            {
                spriteBatch.Draw(playerTwo.texture, playerTwo.collisionBoxes[i], color[i]);//Debug
                spriteBatch.Draw(playerOne.texture, playerOne.collisionBoxes[i], color[i]);//Debug
            }
            spriteBatch.DrawString(Drawing.myFont, playerOne.score.ToString(), playerOne.scorePos, Color.White);
            spriteBatch.DrawString(Drawing.myFont, playerTwo.score.ToString(), playerTwo.scorePos, Color.White);
        }
    }
}
