using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Collections.ArrayList;
using Microsoft.Xna.Framework;

namespace Elemental_Connection
{
    class Grid
    {
        private int xSize = 10;
        private int ySize = 10;
        public Block[,] playerBlocks;
        private int playerID;
        Random random;
        //private ArrayList<Vector2> endBlocks = new ArrayList<Vector2>();
        private HashSet<Point> endBlocks = new HashSet<Point>();
        private HashSet<Point> blocksToBeRemoved = new HashSet<Point>();
        private HashSet<Point> currentBlockList = new HashSet<Point>();
        private HashSet<HashSet<Point>> finalBlockList = new HashSet<HashSet<Point>>();

        public void Initialize(int playerNum)
        {
            this.playerID = playerNum;
            playerBlocks = new Block[xSize, ySize];
            random = new Random(DateTime.Now.Millisecond);

        }

        public void LoadContent(Texture2D SpriteSheet)
        {
            for (int i = 0; i < xSize; i++)
            {
                for (int j = 0; j < ySize; j++)
                {
                    // create and load texture
                    playerBlocks[i, j] = new Block();
                    playerBlocks[i, j].SetSpriteSheet(SpriteSheet);

                    // randomly assign block
                    int randomBlock = random.Next(0, 2);
                    playerBlocks[i, j].SetBlockType(randomBlock, 0);
                    playerBlocks[i, j].SetBlockLocation(playerID, i, j);


                }
            }

            HashSet<int> elementBlock = new HashSet<int>();
            while (elementBlock.Count < 10)
            {
                elementBlock.Add(random.Next(0, 99));
            }


            int counter = 0;
            foreach (int blockLoc in elementBlock)
            {
                int xLocation = (int)(blockLoc / 10);
                int yLocation = blockLoc % 10;
                if (counter == 0 || counter == 1)
                {
                    playerBlocks[xLocation, yLocation].SetBlockType(2, 0);
                }
                else if (counter == 2 || counter == 3)
                {
                    playerBlocks[xLocation, yLocation].SetBlockType(3, 0);
                }
                else if (counter == 4 || counter == 5)
                {
                    playerBlocks[xLocation, yLocation].SetBlockType(4, 0);
                }
                else if (counter == 6 || counter == 7)
                {
                    playerBlocks[xLocation, yLocation].SetBlockType(5, 0);
                }
                else if (counter == 8 || counter == 9)
                {
                    playerBlocks[xLocation, yLocation].SetBlockType(6, 0);
                }
                counter++;

                endBlocks.Add(new Point(xLocation, yLocation));
            }

        }

        public void Connect()
        {
            //Console.Out.WriteLine("Starting Connect");
            finalBlockList = new HashSet<HashSet<Point>>();
            foreach(Point endBlockLocation in endBlocks)
            {
                //Console.Out.WriteLine("Current End Block: " + endBlockLocation.X + ":" + endBlockLocation.Y);
                if(playerBlocks[endBlockLocation.X, endBlockLocation.Y].endBlock)
                {
                    currentBlockList =  new HashSet<Point>();
                    currentBlockList.Add(new Point(endBlockLocation.X, endBlockLocation.Y));
                    
                    if (playerBlocks[endBlockLocation.X, endBlockLocation.Y].topOpening)
                    {
                        if(endBlockLocation.Y - 1 >= 0)
                        {
                            if (Connection(new Point(endBlockLocation.X, endBlockLocation.Y), new Point(endBlockLocation.X, endBlockLocation.Y - 1), 0))
                            {
                                if (!finalBlockList.Contains(currentBlockList))
                                finalBlockList.Add(currentBlockList);
                            }
                        }                    

                    }

                    if (playerBlocks[endBlockLocation.X, endBlockLocation.Y].rightOpening)
                    {
                        if (endBlockLocation.X + 1 <= 9)
                        {
                            if (Connection(new Point(endBlockLocation.X, endBlockLocation.Y), new Point(endBlockLocation.X + 1, endBlockLocation.Y),1))
                            {
                                if (!finalBlockList.Contains(currentBlockList))
                                finalBlockList.Add(currentBlockList);
                            }
                        }

                    }

                    if (playerBlocks[endBlockLocation.X, endBlockLocation.Y].bottomOpening)
                    {
                        if (endBlockLocation.Y + 1 <= 9)
                        {
                            if (Connection(new Point(endBlockLocation.X, endBlockLocation.Y), new Point(endBlockLocation.X, endBlockLocation.Y + 1),2))
                            {
                                if (!finalBlockList.Contains(currentBlockList))
                                finalBlockList.Add(currentBlockList);
                            }
                        }

                    }

                    if (playerBlocks[endBlockLocation.X, endBlockLocation.Y].leftOpening)
                    {
                        if (endBlockLocation.X - 1 >= 0)
                        {
                            if (Connection(new Point(endBlockLocation.X, endBlockLocation.Y), new Point(endBlockLocation.X - 1, endBlockLocation.Y),3))
                            {
                                if (!finalBlockList.Contains(currentBlockList))
                                finalBlockList.Add(currentBlockList);
                            }
                        }

                    }


                    

                }

                //Console.Out.WriteLine("End Current End Block:");
            }

            //Console.Out.WriteLine("Ending Connect");

            foreach (HashSet<Point> blockList in finalBlockList)
            {
                foreach (Point block in blockList)
                {
                    Console.WriteLine(block.X + " " + block.Y);
                }
            }
        }

        public Boolean Connection(Point currentBlock, Point nextBlock, int incOpening)
        {
            // check differently if it's an end block vs non end block
            if (playerBlocks[nextBlock.X, nextBlock.Y].endBlock)
            {
                // check all openings if end block
                if (playerBlocks[currentBlock.X, currentBlock.Y].topOpening)
                {
                    // check out of bound
                    if (currentBlock.Y > 0)
                    {
                        // finally check if the next block has the correct opening
                        if (playerBlocks[nextBlock.X, nextBlock.Y].bottomOpening)
                        {
                            currentBlockList.Add(new Point(nextBlock.X, nextBlock.Y));
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }

                if (playerBlocks[currentBlock.X, currentBlock.Y].rightOpening)
                {
                    // check out of bound
                    if (currentBlock.X < 9)
                    {
                        // finally check if the next block has the correct opening
                        if (playerBlocks[nextBlock.X, nextBlock.Y].leftOpening)
                        {
                            currentBlockList.Add(new Point(nextBlock.X, nextBlock.Y));
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }

                if (playerBlocks[currentBlock.X, currentBlock.Y].bottomOpening)
                {
                    // check out of bound
                    if (currentBlock.Y < 9)
                    {
                        // finally check if the next block has the correct opening
                        if (playerBlocks[nextBlock.X, nextBlock.Y].topOpening)
                        {
                            currentBlockList.Add(new Point(nextBlock.X, nextBlock.Y));
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }

                if (playerBlocks[currentBlock.X, currentBlock.Y].leftOpening)
                {
                    // check out of bound
                    if (currentBlock.X > 0)
                    {
                        // finally check if the next block has the correct opening
                        if (playerBlocks[nextBlock.X, nextBlock.Y].rightOpening)
                        {
                            currentBlockList.Add(new Point(nextBlock.X, nextBlock.Y));
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            else
            {
                // check only outgoing openings if not end block
                if (incOpening == 0)
                {
                    // check if the next block has a bottom opening
                    if (playerBlocks[nextBlock.X, nextBlock.Y].bottomOpening)
                    {
                        // don't check bottom since it's the inc direction
                        // check top opening
                        if (playerBlocks[nextBlock.X, nextBlock.Y].topOpening)
                        {
                            if (nextBlock.Y - 1 < 0)
                            {
                                return false;
                            }
                            else
                            {
                                currentBlockList.Add(new Point(nextBlock.X, nextBlock.Y));
                                Point nextNextBlock = new Point(nextBlock.X, nextBlock.Y - 1);
                                return (Connection(nextBlock, nextNextBlock, 0));
                            }
                        }
                        // check right
                        if (playerBlocks[nextBlock.X, nextBlock.Y].rightOpening)
                        {
                            if (nextBlock.X + 1 > 9)
                            {
                                return false;
                            }
                            else
                            {
                                currentBlockList.Add(new Point(nextBlock.X, nextBlock.Y));
                                Point nextNextBlock = new Point(nextBlock.X + 1, nextBlock.Y);
                                return (Connection(nextBlock, nextNextBlock, 1));
                            }
                        }
                        // check left
                        if (playerBlocks[nextBlock.X, nextBlock.Y].leftOpening)
                        {
                            if (nextBlock.X - 1 < 0)
                            {
                                return false;
                            }
                            else
                            {
                                currentBlockList.Add(new Point(nextBlock.X, nextBlock.Y));
                                Point nextNextBlock = new Point(nextBlock.X - 1, nextBlock.Y);
                                return (Connection(nextBlock, nextNextBlock, 3));
                            }
                        }
                    }
                    // if not then return false
                    else
                    {
                        return false;
                    }
                }
                else if(incOpening == 1)
                {
                    if (playerBlocks[nextBlock.X, nextBlock.Y].leftOpening)
                    {
                        // don't check left since it's the inc direction
                        // check top opening
                        if (playerBlocks[nextBlock.X, nextBlock.Y].topOpening)
                        {
                            if (nextBlock.Y - 1 < 0)
                            {
                                return false;
                            }
                            else
                            {
                                currentBlockList.Add(new Point(nextBlock.X, nextBlock.Y));
                                Point nextNextBlock = new Point(nextBlock.X, nextBlock.Y - 1);
                                return (Connection(nextBlock, nextNextBlock, 0));
                            }
                        }
                        // check right
                        if (playerBlocks[nextBlock.X, nextBlock.Y].rightOpening)
                        {
                            if (nextBlock.X + 1 > 9)
                            {
                                return false;
                            }
                            else
                            {
                                currentBlockList.Add(new Point(nextBlock.X, nextBlock.Y));
                                Point nextNextBlock = new Point(nextBlock.X + 1, nextBlock.Y);
                                return (Connection(nextBlock, nextNextBlock, 1));
                            }
                        }
                        // check bottom
                        if (playerBlocks[nextBlock.X, nextBlock.Y].bottomOpening)
                        {
                            if (nextBlock.Y + 1 > 9)
                            {
                                return false;
                            }
                            else
                            {
                                currentBlockList.Add(new Point(nextBlock.X, nextBlock.Y));
                                Point nextNextBlock = new Point(nextBlock.X, nextBlock.Y + 1);
                                return (Connection(nextBlock, nextNextBlock, 3));
                            }
                        }
                    }
                    // if not then return false
                    else
                    {
                        return false;
                    }
                }
                else if(incOpening == 2)
                {
                    // check if the next block has a top opening
                    if (playerBlocks[nextBlock.X, nextBlock.Y].topOpening)
                    {
                        // don't check top since it's the inc direction
                        // check right
                        if (playerBlocks[nextBlock.X, nextBlock.Y].rightOpening)
                        {
                            if (nextBlock.X + 1 > 9)
                            {
                                return false;
                            }
                            else
                            {
                                currentBlockList.Add(new Point(nextBlock.X, nextBlock.Y));
                                Point nextNextBlock = new Point(nextBlock.X + 1, nextBlock.Y);
                                return (Connection(nextBlock, nextNextBlock, 1));
                            }
                        }
                        // check left
                        if (playerBlocks[nextBlock.X, nextBlock.Y].leftOpening)
                        {
                            if (nextBlock.X - 1 < 0)
                            {
                                return false;
                            }
                            else
                            {
                                currentBlockList.Add(new Point(nextBlock.X, nextBlock.Y));
                                Point nextNextBlock = new Point(nextBlock.X - 1, nextBlock.Y);
                                return (Connection(nextBlock, nextNextBlock, 3));
                            }
                        }

                        // check bottom
                        if (playerBlocks[nextBlock.X, nextBlock.Y].bottomOpening)
                        {
                            if (nextBlock.Y + 1 > 9)
                            {
                                return false;
                            }
                            else
                            {
                                currentBlockList.Add(new Point(nextBlock.X, nextBlock.Y));
                                Point nextNextBlock = new Point(nextBlock.X, nextBlock.Y + 1);
                                return (Connection(nextBlock, nextNextBlock, 3));
                            }
                        }
                    }
                    // if not then return false
                    else
                    {
                        return false;
                    }
                }
                else if(incOpening == 3)
                {
                    // check if the next block has a right opening
                    if (playerBlocks[nextBlock.X, nextBlock.Y].rightOpening)
                    {
                        // don't check right since it's the inc direction
                        // check top opening
                        if (playerBlocks[nextBlock.X, nextBlock.Y].topOpening)
                        {
                            if (nextBlock.Y - 1 < 0)
                            {
                                return false;
                            }
                            else
                            {
                                currentBlockList.Add(new Point(nextBlock.X, nextBlock.Y));
                                Point nextNextBlock = new Point(nextBlock.X, nextBlock.Y - 1);
                                return (Connection(nextBlock, nextNextBlock, 0));
                            }
                        }
                        // check bottom
                        if (playerBlocks[nextBlock.X, nextBlock.Y].bottomOpening)
                        {
                            if (nextBlock.Y + 1 > 9)
                            {
                                return false;
                            }
                            else
                            {
                                currentBlockList.Add(new Point(nextBlock.X, nextBlock.Y));
                                Point nextNextBlock = new Point(nextBlock.X, nextBlock.Y + 1);
                                return (Connection(nextBlock, nextNextBlock, 3));
                            }
                        }

                        // check left
                        if (playerBlocks[nextBlock.X, nextBlock.Y].leftOpening)
                        {
                            if (nextBlock.X - 1 < 0)
                            {
                                return false;
                            }
                            else
                            {
                                currentBlockList.Add(new Point(nextBlock.X, nextBlock.Y));
                                Point nextNextBlock = new Point(nextBlock.X - 1, nextBlock.Y);
                                return (Connection(nextBlock, nextNextBlock, 3));
                            }
                        }

                    }
                    // if not then return false
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }
    

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {

            for (int i = 0; i < xSize; i++)
            {
                for (int j = 0; j < ySize; j++)
                {
                    playerBlocks[i, j].Draw(spriteBatch);
                }
            }
        }


    }
}
