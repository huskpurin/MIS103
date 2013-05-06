using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;



namespace WindowsGame2
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class GameComponent_Menu : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public SpriteFont Font;

        int hoverSide;

        class Choice
        {
            public string title;
            public Vector2 pos;
            public Vector2 org;
            public Color fontColor;

            public Choice()
            {
                fontColor = Color.White;
            }
        }

        Choice choiceLeft;
        Choice choiceRight;

        public GameComponent_Menu(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        public override void Initialize()
        {
            // TODO: Add your initialization code here
            /*
            // 設定選單位置
            destRectAll.X = this.GraphicsDevice.Viewport.Width / 2 - destRectAll.Width / 2;
            destRectAll.Y = this.GraphicsDevice.Viewport.Height / 2 - destRectAll.Height / 2;
            */


            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            choiceLeft = new Choice();
            choiceLeft.title = "left choice";
            choiceLeft.org = Font.MeasureString(choiceLeft.title) / 2;
            choiceLeft.pos = new Vector2(GraphicsDevice.Viewport.Width / 2 - GraphicsDevice.Viewport.Width / 6, GraphicsDevice.Viewport.Height / 2);

            choiceRight = new Choice();
            choiceRight.title = "right choice";
            choiceRight.org = Font.MeasureString(choiceRight.title) / 2;
            choiceRight.pos = new Vector2(GraphicsDevice.Viewport.Width / 2 + GraphicsDevice.Viewport.Width / 6, GraphicsDevice.Viewport.Height / 2);

            MouseState mouseState = Mouse.GetState();

            hoverSide = -1;

            if (mouseState.X >= choiceLeft.pos.X - choiceLeft.org.X && mouseState.X <= choiceLeft.pos.X + choiceLeft.org.X)
            {
                choiceLeft.fontColor = Color.Black;
            }
            else
            {
                choiceLeft.fontColor = Color.White;
            }

            if (mouseState.X >= choiceRight.pos.X - choiceRight.org.X && mouseState.X <= choiceRight.pos.X + choiceRight.org.X)
            {
                choiceRight.fontColor = Color.Black;
            }
            else
            {
                choiceRight.fontColor = Color.White;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = new SpriteBatch(GraphicsDevice);

            spriteBatch.Begin();

            spriteBatch.DrawString(Font, choiceLeft.title, choiceLeft.pos, choiceLeft.fontColor, 0, choiceLeft.org, 1.0f, SpriteEffects.None, 0);
            spriteBatch.DrawString(Font, choiceRight.title, choiceRight.pos, choiceRight.fontColor, 0, choiceRight.org, 1.0f, SpriteEffects.None, 0);
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
