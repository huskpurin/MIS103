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
    public class GameComponent_Start : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public Texture2D startTexture;
        Rectangle startPosition;

        Color normColor = Color.White;
        Color hoverColor = Color.Black;
        Color startStatus = Color.White;

        public GameComponent_Start(Game game)
            : base(game)
        {
            // TODO: Construct any child components here

            
        }

        public override void Initialize()
        {
            // TODO: Add your initialization code here

            

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            // 確認目前遊戲狀態
            if ( GameStateClass.currentGameState != GameStateClass.GameState.Start )
                return;

            // TODO: Add your update code here

            // 設定Start圖片位置
            startPosition = new Rectangle(GraphicsDevice.Viewport.Width / 2 - startTexture.Width / 2,
                                          GraphicsDevice.Viewport.Height / 2 - startTexture.Height / 2,
                                          startTexture.Width, startTexture.Height);

            // 取得滑鼠狀態
            MouseState mouthState = Mouse.GetState();   
            // 滑鼠經過圖片就重新設定顏色
            if (mouthState.X >= startPosition.X && mouthState.X <= startPosition.X + startTexture.Width
                && mouthState.Y >= startPosition.Y && mouthState.Y <= startPosition.Y + startTexture.Height)
            {   
                startStatus = hoverColor;
                // 如果滑鼠點擊圖片(按下左鍵)
                if (mouthState.LeftButton == ButtonState.Pressed)   
                {
                    // 改變遊戲畫面狀態 : 跳至Menu
                    GameStateClass.currentGameState = GameStateClass.GameState.Menu;    
                }   
            } 
            else
                startStatus = normColor;    // 變回原來顏色

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            // 確認目前遊戲狀態
            if (GameStateClass.currentGameState != GameStateClass.GameState.Start)
                return;

            SpriteBatch spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteBatch.Begin();
            
            spriteBatch.Draw(startTexture, startPosition, startStatus); // 繪出Start圖片

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
