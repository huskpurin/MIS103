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
using Microsoft.Kinect;


namespace WindowsGame2
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class kinectUserVideoClass : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public KinectSensor kinectSensor;       // kinect sensor
        public Rectangle videoDisplayRectangle; // 彩色影像顯示所在位置
        public Texture2D kinectVideoTexture;    // 彩色影像texture

        public struct VideoSize
        {
           int Width;
           int Height;
        }
        VideoSize videoSize;
        
        public int videoW = 200;    // 影像寬 4
        public int videoH = 150;    // 影像長 3


        public kinectUserVideoClass(Game game, KinectSensor kinectSensor)
            : base(game)
        {
            // TODO: Construct any child components here
            this.kinectSensor = kinectSensor;
        }

        public override void Initialize()
        {
            // TODO: Add your initialization code here
            videoSize.
            /*
            if (kinectSensor != null)
            {
                kinectSensor.ColorStream.Enable();
                kinectSensor.Start();
            }
            */
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            // 設定顯示位置 - 左下角 - 矩形範圍 (Rectangle 儲存四個為一組的整數(x, y, width, height)，表示矩形位置和大小。)
            videoDisplayRectangle = new Rectangle(0, this.GraphicsDevice.Viewport.Height - videoH, videoW, videoH); 

            // kinect彩色串流處理(主動式)
            byte[] colorDate = null;
            using (ColorImageFrame frame = kinectSensor.ColorStream.OpenNextFrame(0))
            {
                if (frame == null)
                    return;
                if (colorDate == null)
                    colorDate = new byte[frame.Width * frame.Height * 4]; // 乘上4是因為，每格顏色是由四種資訊(r,g,b,alpha)組成
                frame.CopyPixelDataTo(colorDate);
                DataToVideoTexture(colorDate, frame.Width, frame.Height);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = new SpriteBatch(GraphicsDevice);
            
            spriteBatch.Begin();

            // 繪出彩色影像
            if (kinectVideoTexture != null)
            {
                spriteBatch.Draw(kinectVideoTexture, videoDisplayRectangle, Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        void DataToVideoTexture(byte[] colorDate, int w, int h)
        {
            kinectVideoTexture = new Texture2D(GraphicsDevice, w, h);
            Color[] bitmap = new Color[w * h];
            int sourceOffset = 0;
            for (int i = 0; i < bitmap.Length; i++)
            {
                bitmap[i] = new Color(colorDate[sourceOffset + 2],
                                       colorDate[sourceOffset + 1],
                                       colorDate[sourceOffset],
                                       255);
                sourceOffset += 4;
            }
            kinectVideoTexture.SetData(bitmap);
        }

    }
}
