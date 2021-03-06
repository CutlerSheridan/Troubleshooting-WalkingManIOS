﻿using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;

namespace MGTestWalkingManIOS
{
    public class CharacterEntity
    {
        static Texture2D characterSheetTexture;
        Animation walkDown;
        Animation walkUp;
        Animation walkRight;
        Animation walkLeft;

        Animation standDown;
        Animation standUp;
        Animation standRight;
        Animation standLeft;

        Animation currentAnimation;

        public float X
        {
            get;
            set;
        }

        public float Y
        {
            get;
            set;
        }

        public CharacterEntity(GraphicsDevice graphicsDevice)
        {
            if (characterSheetTexture == null)
            {
                //using (var stream = TitleContainer.OpenStream("Content/charactersheet.png"))
                using (var stream = TitleContainer.OpenStream("Content/charactersheetbigger.png"))
                {
                    characterSheetTexture = Texture2D.FromStream(graphicsDevice, stream);
                    
                }
            }

            walkDown = new Animation();
            walkDown.AddFrame(new Rectangle(0, 0, 32, 32), TimeSpan.FromSeconds(.25));
            walkDown.AddFrame(new Rectangle(32, 0, 32, 32), TimeSpan.FromSeconds(.25));
            walkDown.AddFrame(new Rectangle(0, 0, 32, 32), TimeSpan.FromSeconds(.25));
            walkDown.AddFrame(new Rectangle(64, 0, 32, 32), TimeSpan.FromSeconds(.25));

            walkUp = new Animation();
            walkUp.AddFrame(new Rectangle(288, 0, 32, 32), TimeSpan.FromSeconds(.25));
            walkUp.AddFrame(new Rectangle(320, 0, 32, 32), TimeSpan.FromSeconds(.25));
            walkUp.AddFrame(new Rectangle(288, 0, 32, 32), TimeSpan.FromSeconds(.25));
            walkUp.AddFrame(new Rectangle(352, 0, 32, 32), TimeSpan.FromSeconds(.25));

            walkLeft = new Animation();
            walkLeft.AddFrame(new Rectangle(96, 0, 32, 32), TimeSpan.FromSeconds(.25));
            walkLeft.AddFrame(new Rectangle(128, 0, 32, 32), TimeSpan.FromSeconds(.25));
            walkLeft.AddFrame(new Rectangle(96, 0, 32, 32), TimeSpan.FromSeconds(.25));
            walkLeft.AddFrame(new Rectangle(160, 0, 32, 32), TimeSpan.FromSeconds(.25));

            walkRight = new Animation();
            walkRight.AddFrame(new Rectangle(192, 0, 32, 32), TimeSpan.FromSeconds(.25));
            walkRight.AddFrame(new Rectangle(224, 0, 32, 32), TimeSpan.FromSeconds(.25));
            walkRight.AddFrame(new Rectangle(192, 0, 32, 32), TimeSpan.FromSeconds(.25));
            walkRight.AddFrame(new Rectangle(256, 0, 32, 32), TimeSpan.FromSeconds(.25));

            standDown = new Animation();
            standDown.AddFrame(new Rectangle(0, 0, 32, 32), TimeSpan.FromSeconds(.25));

            standUp = new Animation();
            standUp.AddFrame(new Rectangle(288, 0, 32, 32), TimeSpan.FromSeconds(.25));

            standLeft = new Animation();
            standLeft.AddFrame(new Rectangle(96, 0, 32, 32), TimeSpan.FromSeconds(.25));

            standRight = new Animation();
            standRight.AddFrame(new Rectangle(192, 0, 32, 32), TimeSpan.FromSeconds(.25));

            /*walkDown = new Animation();
            walkDown.AddFrame(new Rectangle(0, 0, 16, 16), TimeSpan.FromSeconds(.25));
            walkDown.AddFrame(new Rectangle(16, 0, 16, 16), TimeSpan.FromSeconds(.25));
            walkDown.AddFrame(new Rectangle(0, 0, 16, 16), TimeSpan.FromSeconds(.25));
            walkDown.AddFrame(new Rectangle(32, 0, 16, 16), TimeSpan.FromSeconds(.25));

            walkUp = new Animation();
            walkUp.AddFrame(new Rectangle(144, 0, 16, 16), TimeSpan.FromSeconds(.25));
            walkUp.AddFrame(new Rectangle(160, 0, 16, 16), TimeSpan.FromSeconds(.25));
            walkUp.AddFrame(new Rectangle(144, 0, 16, 16), TimeSpan.FromSeconds(.25));
            walkUp.AddFrame(new Rectangle(176, 0, 16, 16), TimeSpan.FromSeconds(.25));

            walkLeft = new Animation();
            walkLeft.AddFrame(new Rectangle(48, 0, 16, 16), TimeSpan.FromSeconds(.25));
            walkLeft.AddFrame(new Rectangle(64, 0, 16, 16), TimeSpan.FromSeconds(.25));
            walkLeft.AddFrame(new Rectangle(48, 0, 16, 16), TimeSpan.FromSeconds(.25));
            walkLeft.AddFrame(new Rectangle(80, 0, 16, 16), TimeSpan.FromSeconds(.25));

            walkRight = new Animation();
            walkRight.AddFrame(new Rectangle(96, 0, 16, 16), TimeSpan.FromSeconds(.25));
            walkRight.AddFrame(new Rectangle(112, 0, 16, 16), TimeSpan.FromSeconds(.25));
            walkRight.AddFrame(new Rectangle(96, 0, 16, 16), TimeSpan.FromSeconds(.25));
            walkRight.AddFrame(new Rectangle(128, 0, 16, 16), TimeSpan.FromSeconds(.25));

            standDown = new Animation();
            standDown.AddFrame(new Rectangle(0, 0, 16, 16), TimeSpan.FromSeconds(.25));

            standUp = new Animation();
            standUp.AddFrame(new Rectangle(144, 0, 16, 16), TimeSpan.FromSeconds(.25));

            standLeft = new Animation();
            standLeft.AddFrame(new Rectangle(48, 0, 16, 16), TimeSpan.FromSeconds(.25));

            standRight = new Animation();
            standRight.AddFrame(new Rectangle(96, 0, 16, 16), TimeSpan.FromSeconds(.25));*/
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 topLeftOfSprite = new Vector2(this.X, this.Y);
            Color tintColor = Color.White;
            var sourceRectangle = currentAnimation.CurrentRectangle;
            spriteBatch.Draw(characterSheetTexture, topLeftOfSprite, sourceRectangle, tintColor);
        }

        public void Update(GameTime gameTime)
        {
            var velocity = GetDesiredVelocityFromInput();
            this.X += velocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds;
            this.Y += velocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (velocity != Vector2.Zero)
            {
                bool movingHorizontally = Math.Abs(velocity.X) > Math.Abs(velocity.Y);
                if (movingHorizontally)
                {
                    if (velocity.X > 0)
                    {
                        currentAnimation = walkRight;
                    }
                    else
                    {
                        currentAnimation = walkLeft;
                    }
                }
                else
                {
                    if (velocity.Y > 0)
                    {
                        currentAnimation = walkDown;
                    }
                    else
                    {
                        currentAnimation = walkUp;
                    }
                }
            }
            else
            {
                if (currentAnimation == walkRight)
                {
                    currentAnimation = standRight;
                }
                else if (currentAnimation == walkLeft)
                {
                    currentAnimation = standLeft;
                }
                else if (currentAnimation == walkDown)
                {
                    currentAnimation = standDown;
                }
                else if (currentAnimation == walkUp)
                {
                    currentAnimation = standUp;
                }
                else if (currentAnimation == null)
                {
                    currentAnimation = standDown;
                }
            }
            currentAnimation.Update(gameTime);
        }

        Vector2 GetDesiredVelocityFromInput()
        {
            Vector2 desiredVelocity = new Vector2();
            TouchCollection touchCollection = TouchPanel.GetState();

            if (touchCollection.Count > 0)
            {
                desiredVelocity.X = touchCollection[0].Position.X - this.X;
                desiredVelocity.Y = touchCollection[0].Position.Y - this.Y;

                if (desiredVelocity.X != 0 && desiredVelocity.Y != 0)
                {
                    desiredVelocity.Normalize();
                    const float desiredSpeed = 250;
                    desiredVelocity *= desiredSpeed;
                }
            }
            return desiredVelocity;
        }
    }
}
