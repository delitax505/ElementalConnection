using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Elemental_Connection
{
    class Player
    {
        private Texture2D SpriteSheet;
        private int currentHP;
        private int maxHp;
        public int currentX = 1;
        public int currentY = 1;
        private int playerNum;
        private Rectangle cursor1SpriteLocation = new Rectangle(0, 50, 60, 60);
        private Rectangle cursor2SpriteLocation = new Rectangle(60, 50, 60, 60);
        private int xOffset = 45;
        private int yOffset = 115;

        GamePadState currentGamePadState;
        GamePadState previousGamePadState;

        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;

        public Player(int playerNumber)
        {
            this.playerNum = playerNumber;
        }


        public void Initialize(Texture2D spriteSheet)
        {
            this.SpriteSheet = spriteSheet;
        }

        public void SetSpriteSheet(Texture2D spriteSheet)
        {
            this.SpriteSheet = spriteSheet;
        }

        //public void Update(GamePadState playerOnePadState)
        public void Update(Grid playerGrid)
        {
            if(playerNum == 1)
            {
                currentGamePadState = GamePad.GetState(PlayerIndex.One);
                currentKeyboardState = Keyboard.GetState();
            }
            else if(playerNum == 2)
            {
                currentGamePadState = GamePad.GetState(PlayerIndex.Two);
            }

            // keyboard
            if (currentKeyboardState.IsKeyUp(Keys.A) && previousKeyboardState.IsKeyDown(Keys.A))
            {
                if (currentX > 0)
                    currentX--;
            }

            if (currentKeyboardState.IsKeyUp(Keys.D) && previousKeyboardState.IsKeyDown(Keys.D))
            {
                if (currentX < 9)
                    currentX++;
            }

            if (currentKeyboardState.IsKeyUp(Keys.W) && previousKeyboardState.IsKeyDown(Keys.W))
            {
                if (currentY > 0)
                    currentY--;
            }

            if (currentKeyboardState.IsKeyUp(Keys.S) && previousKeyboardState.IsKeyDown(Keys.S))
            {
                if (currentY < 9)
                    currentY++;
            }


            if (currentKeyboardState.IsKeyUp(Keys.Left) && previousKeyboardState.IsKeyDown(Keys.Left))
            {
                if (playerGrid.playerBlocks[currentX, currentY].blockRotation == 0)
                {
                    playerGrid.playerBlocks[currentX, currentY].blockRotation = 3;
                }
                else if (playerGrid.playerBlocks[currentX, currentY].blockRotation <= 3)
                {
                    playerGrid.playerBlocks[currentX, currentY].blockRotation--;
                }
            }

            if (currentKeyboardState.IsKeyUp(Keys.Right) && previousKeyboardState.IsKeyDown(Keys.Right))
            {
                if (playerGrid.playerBlocks[currentX, currentY].blockRotation < 3)
                {
                    playerGrid.playerBlocks[currentX, currentY].blockRotation++;
                }
                else if (playerGrid.playerBlocks[currentX, currentY].blockRotation == 3)
                {
                    playerGrid.playerBlocks[currentX, currentY].blockRotation = 0;
                }
            }

            // if A is clicked
            if (currentKeyboardState.IsKeyUp(Keys.Space) && previousKeyboardState.IsKeyDown(Keys.Space))
            {
                playerGrid.Connect();
            }

            playerGrid.playerBlocks[currentX, currentY].SetBlockOpenings();
            playerGrid.playerBlocks[currentX, currentY].TranslateBlockRotation();


            previousKeyboardState = currentKeyboardState;

            // gamepad
            if(currentGamePadState.IsConnected)
            {
                if(currentGamePadState.DPad.Left == ButtonState.Released && previousGamePadState.DPad.Left == ButtonState.Pressed)
                {
                    if(currentX > 0)
                        currentX--;
                }

                if (currentGamePadState.DPad.Right == ButtonState.Released && previousGamePadState.DPad.Right == ButtonState.Pressed)
                {
                    if (currentX < 9)
                        currentX++;
                }
                if (currentGamePadState.DPad.Up == ButtonState.Released && previousGamePadState.DPad.Up == ButtonState.Pressed)
                {
                    if (currentY > 0)
                        currentY--;
                }

                if (currentGamePadState.DPad.Down == ButtonState.Released && previousGamePadState.DPad.Down == ButtonState.Pressed)
                {
                    if (currentY < 9)
                        currentY++;
                }



                if(currentGamePadState.Buttons.LeftShoulder == ButtonState.Released && previousGamePadState.Buttons.LeftShoulder == ButtonState.Pressed)
                {
                    if (playerGrid.playerBlocks[currentX, currentY].blockRotation  == 0)
                    {
                        playerGrid.playerBlocks[currentX, currentY].blockRotation = 3;
                    }
                    else if(playerGrid.playerBlocks[currentX, currentY].blockRotation <= 3)
                    {
                        playerGrid.playerBlocks[currentX, currentY].blockRotation--;
                    }
                }

                if (currentGamePadState.Buttons.RightShoulder == ButtonState.Released && previousGamePadState.Buttons.RightShoulder == ButtonState.Pressed)
                {
                    if (playerGrid.playerBlocks[currentX, currentY].blockRotation < 3)
                    {
                        playerGrid.playerBlocks[currentX, currentY].blockRotation++;
                    }
                    else if (playerGrid.playerBlocks[currentX, currentY].blockRotation == 3)
                    {
                        playerGrid.playerBlocks[currentX, currentY].blockRotation = 0;
                    }
                }

                // if A is clicked
                if(currentGamePadState.Buttons.A == ButtonState.Released && previousGamePadState.Buttons.A == ButtonState.Pressed)
                {
                    playerGrid.Connect();
                }

                playerGrid.playerBlocks[currentX, currentY].SetBlockOpenings();
                playerGrid.playerBlocks[currentX, currentY].TranslateBlockRotation();

                previousGamePadState = currentGamePadState;
                currentGamePadState = GamePad.GetState(PlayerIndex.One);
            }
        }

        public Vector2 GetCursorLocation(int x, int y)
        {
            if (playerNum == 1)
            {
                return new Vector2((xOffset + (x * 50) + (5 * x)), (yOffset + (y * 50) + (5 * y)));
            }
            else if(playerNum == 2)
            {
                return new Vector2((xOffset + (x * 50) + (5 * x)) + 80 + 550, (yOffset + (y * 50) + (5 * y)));
            }
            else
            {
                return new Vector2(0, 0);
            }
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
            // drawing cursor
            if(playerNum == 1)
            {   
                //if(currentGamePadState.IsConnected)
                spriteBatch.Draw(SpriteSheet, GetCursorLocation(currentX, currentY), cursor1SpriteLocation, Color.White);
            }
            else if (playerNum == 2)
            {
                if(currentGamePadState.IsConnected)
                spriteBatch.Draw(SpriteSheet, GetCursorLocation(currentX, currentY), cursor2SpriteLocation, Color.White);
            }

        }


    }
}
