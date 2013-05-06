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

        // 文字選項 類別
        class Choice
        {
            public string title;    // 選項標題
            public Vector2 pos;     // 選項位置
            public Vector2 org;     // 選項中心點
            public Color fontColor; // 文字顏色
            public int value;

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

        public override void Update(GameTime gameTime)
        {
            if (GameStateClass.currentGameState != GameStateClass.GameState.Menu)
                return;

            // TODO: Add your update code here
            
            choiceLeft = new Choice();
            choiceRight = new Choice();

            if (UserSetting.userSex == 0)
            {
                choiceLeft.title = "Male";
                choiceRight.title = "Female";
            }
            else if (UserSetting.userHander == 0)
            {
                choiceLeft.title = "Left-Hander";
                choiceRight.title = "Right-Hander";
            }
            else
            {
                GameStateClass.currentGameState = GameStateClass.GameState.CountryGuide;
                return;
            }

            choiceLeft.org = Font.MeasureString(choiceLeft.title) / 2;
            choiceLeft.pos = new Vector2(GraphicsDevice.Viewport.Width / 2 - GraphicsDevice.Viewport.Width / 6, GraphicsDevice.Viewport.Height / 2);
            choiceLeft.value = 1;

            choiceRight.org = Font.MeasureString(choiceRight.title) / 2;
            choiceRight.pos = new Vector2(GraphicsDevice.Viewport.Width / 2 + GraphicsDevice.Viewport.Width / 6, GraphicsDevice.Viewport.Height / 2);
            choiceLeft.value = 2;

            MouseState mouseState = Mouse.GetState();

            hoverSide = -1;

            if (mouseState.X >= choiceLeft.pos.X - choiceLeft.org.X && mouseState.X <= choiceLeft.pos.X + choiceLeft.org.X)
            {
                choiceLeft.fontColor = Color.Black;
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    if (UserSetting.userSex == 0)
                    {
                        UserSetting.userSex = UserSetting.UserSex.Male;
                    }
                    else if (UserSetting.userHander == 0)
                    {
                        UserSetting.userHander = UserSetting.UserHander.Left_Hander;
                    }
                    return;
                }
            }
            else
            {
                choiceLeft.fontColor = Color.White;
            }

            if (mouseState.X >= choiceRight.pos.X - choiceRight.org.X && mouseState.X <= choiceRight.pos.X + choiceRight.org.X
                && mouseState.LeftButton != ButtonState.Pressed )
            {
                choiceRight.fontColor = Color.Black;
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    if (UserSetting.userSex == 0)
                    {
                        UserSetting.userSex = UserSetting.UserSex.Female;
                    }
                    else if (UserSetting.userHander == 0)
                    {
                        UserSetting.userHander = UserSetting.UserHander.Right_Hander;
                    }
                    return;
                }
            }
            else
            {
                choiceRight.fontColor = Color.White;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (GameStateClass.currentGameState != GameStateClass.GameState.Menu)
                return;

            SpriteBatch spriteBatch = new SpriteBatch(GraphicsDevice);

            spriteBatch.Begin();

            spriteBatch.DrawString(Font, choiceLeft.title, choiceLeft.pos, choiceLeft.fontColor, 0, choiceLeft.org, 1.0f, SpriteEffects.None, 0);
            spriteBatch.DrawString(Font, choiceRight.title, choiceRight.pos, choiceRight.fontColor, 0, choiceRight.org, 1.0f, SpriteEffects.None, 0);
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
