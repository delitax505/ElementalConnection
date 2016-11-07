using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace Elemental_Connection
{
    class Block
    {
        private Texture2D SpriteSheet;
        public int BlockType;
        //public ArrayList<int> openings = new ArrayList<int>();
        public Boolean topOpening = false;
        public Boolean rightOpening = false;
        public Boolean bottomOpening = false;
        public Boolean leftOpening = false;
        public Boolean endBlock = false;
        private Vector2 blockLocation;
        private int xOffset = 75;
        private int yOffset = 145;
        private Rectangle longLocation = new Rectangle(0, 0, 50, 50);
        private Rectangle angleLocation = new Rectangle(50, 0, 50, 50);
        private Rectangle fireLocation = new Rectangle(100, 0, 50, 50);
        private Rectangle iceLocation = new Rectangle(150, 0, 50, 50);
        private Rectangle lightningLocation = new Rectangle(200, 0, 50, 50);
        private Rectangle earthLocation = new Rectangle(250, 0, 50, 50);
        private Rectangle windLocation = new Rectangle(300, 0, 50, 50);
        public int blockRotation = 0;
        private float radianRotation = 0;
        private Vector2 origin = new Vector2(25, 25);

        private Rectangle currentTypeTextureLocation;

        // Block Type
        // 0 long
        // 1 angle
        // 2 fire
        // 3 ice
        // 4 lightning
        // 5 earth
        // 6 wind

        // 0 top
        // 1 right
        // 2 bottom
        // 3 left


        public void Initialize(Texture2D spriteSheet)
        {
            this.SpriteSheet = spriteSheet;
        }

        public void SetSpriteSheet(Texture2D spriteSheet) 
        {
            this.SpriteSheet = spriteSheet;
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Vector2 location1 = new Vector2(50, 50);
            //Rectangle location2 = new Rectangle(0, 0, 50, 50);


            //spriteBatch.Draw(SpriteSheet, currentTypeTextureLocation, 0f);
            //spriteBatch.Draw(SpriteSheet, location1,location2, currentTypeTextureLocation,null, 0, null, Color.White, 0f);
            //spriteBatch.Draw(SpriteSheet, new Vector2(50, 50), Color.White);

            //spriteBatch.Draw(SpriteSheet, new Vector2(50, 50), location2, Color.White, new Vector2(0, 0), 1.0f, null, 0f);
            //spriteBatch.Draw(SpriteSheet, new Vector2(50, 50), location2, Color.White,0, new Vector2(0, 0), 1.0f, null, 0f);
            //spriteBatch.Draw(SpriteSheet, location1, location2, Color.White, 0f, new Vector2(0, 0), null, 0);
            //spriteBatch.Draw(SpriteSheet, blockLocation, currentTypeTextureLocation, Color.White);
            //spriteBatch.Draw(SpriteSheet, blockLocation, currentTypeTextureLocation, Color.White, rotation, origin, SpriteEffects.None, 0);
            spriteBatch.Draw(SpriteSheet, blockLocation, currentTypeTextureLocation, Color.White, radianRotation, origin, 1f, SpriteEffects.None, 0f);


        }

        public void SetBlockLocation(int player, int x, int y)
        {
            int xBlock = xOffset + (x * 50) + (5 * x);
            int yBlock = yOffset + (y * 50) + (5 * y);

            if (player == 2)
            {
                xBlock = xBlock + 80 + 550;
            }
            
            blockLocation.X = xBlock;
            blockLocation.Y = yBlock;
        }

        public void TranslateBlockRotation()
        {
            switch(blockRotation)
            {
                case 0:
                    radianRotation = 0;
                    break;
                case 1:
                    radianRotation = (float)Math.PI / 2;
                    break;
                case 2:
                    radianRotation = (float)Math.PI;
                    break;
                case 3:
                    radianRotation = 3 * (float)Math.PI / 2;
                    break;
            }
        }

        public void SetBlockOpenings()
        {
            if(this.BlockType == 0)
            {
                switch(blockRotation)
                {
                    case 0:
                        topOpening = false;
                        rightOpening = true;
                        bottomOpening = false;
                        leftOpening = true;
                        break;
                    case 1:
                        topOpening = true;
                        rightOpening = false;
                        bottomOpening = true;
                        leftOpening = false;
                        break;
                    case 2:
                        topOpening = false;
                        rightOpening = true;
                        bottomOpening = false;
                        leftOpening = true;
                        break;
                    case 3:
                        topOpening = true;
                        rightOpening = false;
                        bottomOpening = true;
                        leftOpening = false;
                        break;

                }
            }
            else if(this.BlockType == 1)
            {
                switch(blockRotation)
                {
                    case 0:
                        topOpening = true;
                        rightOpening = true;
                        bottomOpening = false;
                        leftOpening = false;
                        break;
                    case 1:
                        topOpening = false;
                        rightOpening = true;
                        bottomOpening = true;
                        leftOpening = false;
                        break;
                    case 2:
                        topOpening = false;
                        rightOpening = false;
                        bottomOpening = true;
                        leftOpening = true;
                        break;
                    case 3:
                        topOpening = true;
                        rightOpening = false;
                        bottomOpening = false;
                        leftOpening = true;
                        break;
                }
            }
            else
            {
                switch (blockRotation)
                {
                    case 0:
                        topOpening = false;
                        rightOpening = true;
                        bottomOpening = false;
                        leftOpening = false;
                        break;
                    case 1:
                        topOpening = false;
                        rightOpening = false;
                        bottomOpening = true;
                        leftOpening = false;
                        break;
                    case 2:
                        topOpening = false;
                        rightOpening = false;
                        bottomOpening = false;
                        leftOpening = true;
                        break;
                    case 3:
                        topOpening = true;
                        rightOpening = false;
                        bottomOpening = false;
                        leftOpening = false;
                        break;
                }
            }
            
        }

        public void SetBlockType(int blockType, int rotation)
        {
            this.BlockType = blockType;
            SetBlockOpenings();
            TranslateBlockRotation();
            switch (blockType)
            {
                case 0:
                    currentTypeTextureLocation = longLocation;
                    endBlock = false;
                    break;
                case 1:
                    currentTypeTextureLocation = angleLocation;
                    endBlock = false;
                    break;
                case 2:
                    currentTypeTextureLocation = fireLocation;
                    endBlock = true;
                    break;
                case 3:
                    currentTypeTextureLocation = iceLocation;
                    endBlock = true;
                    break;
                case 4:
                    currentTypeTextureLocation = lightningLocation;
                    endBlock = true;
                    break;
                case 5:
                    currentTypeTextureLocation = earthLocation;
                    endBlock = true;
                    break;
                case 6:
                    currentTypeTextureLocation = windLocation;
                    endBlock = true;
                    break;
            }
        }
         
    }
}
