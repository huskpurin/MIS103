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
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        KinectSensor ks;

        kinectUserVideoClass videoImage;
        Texture2D kinectVideoTexture;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // 設定視窗大小
            this.graphics.PreferredBackBufferHeight = 768;
            this.graphics.PreferredBackBufferWidth = 1024;

        }

        protected override void Initialize()
        {
            this.IsMouseVisible = true; // 顯示滑鼠
            this.Window.Title = "hello world";  // 視窗抬頭
            this.Window.AllowUserResizing = true; // 使用者可調整視窗大小

            base.Initialize();
        }

        Texture2D startTexture;
        Rectangle startPosition;
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ks = KinectSensor.KinectSensors[0];
            ks.ColorStream.Enable();
            ks.SkeletonStream.Enable();
            ks.Start();

            // DrawableGameComponent - KinectUserVideoClass : 左下顯示小螢幕(彩色攝影機)
            videoImage = new kinectUserVideoClass(this, KinectSensor.KinectSensors[0]);
            videoImage.Initialize();
            this.Components.Add(videoImage);

            startTexture = Content.Load<Texture2D>("start");
            startPosition = new Rectangle(GraphicsDevice.Viewport.Width / 2 - startTexture.Width / 2,
                                          GraphicsDevice.Viewport.Height / 2 - startTexture.Height / 2,
                                          startTexture.Width, startTexture.Height);
            

            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            ks.Stop();
            // TODO: Unload any non ContentManager content here
        }

        Color choose = Color.Red;
        Color ready = Color.Black;
        Color startStatus = Color.White;
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to [exit]
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || // 按下搖桿的back鍵
                Keyboard.GetState().IsKeyDown(Keys.Space))  // 按下space
                this.Exit();    // 結束程式

            // skeleton
            using (SkeletonFrame frame = ks.SkeletonStream.OpenNextFrame(0))
            {
                if (frame != null)
                {
                    Skeleton[] fs = new Skeleton[frame.SkeletonArrayLength];
                    frame.CopySkeletonDataTo(fs);
                    int count = (from s in fs where s.TrackingState == SkeletonTrackingState.Tracked select s).Count();
                    if (count == 1)
                    {
                        Skeleton player1 = (from s in fs where s.TrackingState == SkeletonTrackingState.Tracked select s).ElementAt(0);

                        // 收集骨架資訊
                        Joint righthand = player1.Joints[JointType.HandRight];
                        Joint lefthand = player1.Joints[JointType.HandLeft];
                        Joint head = player1.Joints[JointType.Head];
                        Joint leftelbow = player1.Joints[JointType.ElbowLeft];
                        Joint rightelbow = player1.Joints[JointType.ElbowRight];
                        Joint centershoulders = player1.Joints[JointType.ShoulderCenter];
                        Joint leftshoulder = player1.Joints[JointType.ShoulderLeft];
                        Joint rightshoulder = player1.Joints[JointType.ShoulderRight];
                        Joint leftwrist = player1.Joints[JointType.WristLeft];
                        Joint rightwrist = player1.Joints[JointType.WristRight];
                        Joint shoulercenter = player1.Joints[JointType.ShoulderCenter];
                        Joint spine = player1.Joints[JointType.Spine];

                        //
                        if (leftelbow.Position.Y > shoulercenter.Position.Y &&
                            rightelbow.Position.Y > shoulercenter.Position.Y &&
                            Distance(righthand, lefthand) < 0.3)
                        {
                            startStatus = choose;
                        }
                        else
                        {
                            startStatus = ready;
                        }

                    } // end if
                } // end if
            } // end using


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            spriteBatch.Begin();
            //priteBatch.Draw(startTexture, new Vector2(GraphicsDevice.Viewport.X / 2 - startTexture.Width / 2, GraphicsDevice.Viewport.Y / 2 - startTexture.Height / 2), startStatus);
            spriteBatch.Draw(startTexture, startPosition, startStatus);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        // Kinect彩色攝影機串流處理 (被動式)
        void myColorFrame(object sender, ColorImageFrameReadyEventArgs e)
        {
            byte[] colorDate = null;
            using (ColorImageFrame frame = e.OpenColorImageFrame())
            {
                if (frame == null)
                    return;
                if (colorDate == null)
                    colorDate = new byte[frame.Width * frame.Height * 4]; // 乘上4是因為，每格顏色是由四種資訊(r,g,b,alpha)組成
                frame.CopyPixelDataTo(colorDate);
                DataToVideoTexture(colorDate, frame.Width, frame.Height);
            }
        }

        void DataToVideoTexture(byte[] colorDate, int w, int h)
        {
            kinectVideoTexture = new Texture2D(GraphicsDevice, w, h);
            Color[] bitmap =  new Color[w * h];
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

        //  計算兩節點間距離
        double Distance(Joint j1, Joint j2)
        {
            double dis;
            dis = (Math.Sqrt((j1.Position.X - j2.Position.X) * (j1.Position.X - j2.Position.X) + (j1.Position.Y - j2.Position.Y) * (j1.Position.Y - j2.Position.Y)));
            return dis;
        }

    }
}
