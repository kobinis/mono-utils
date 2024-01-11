using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace PaintPlay
{
    public delegate void PixelFunction(float x, float y, Color color); 

    static class MyGraphics
    {
        public static SpriteBatch sb;
        public static GraphicsDeviceManager gdm;
        private static Dictionary<String, Texture2D> textureStore = new Dictionary<string, Texture2D>();

        public static SpriteFont font; 


        public static Rectangle screenRect;


        public static void Init(SpriteBatch spriteBatch, GraphicsDeviceManager graphics, SpriteFont spriteFont)
        {
            sb = spriteBatch;
            gdm = graphics;
            font = spriteFont;                      
            screenRect = new Rectangle(0, 0, gdm.GraphicsDevice.Viewport.Width, gdm.GraphicsDevice.Viewport.Height);
        }

        public static void AddTexture(String key, Texture2D texture)
        {
            textureStore.Add(key, texture); 
        }

        public static Texture2D GetTexture(String key)
        {
            return textureStore[key];
        }        

        public static bool IsCircleOnScreen(Vector2 pos, float rad)
        {
            return !(pos.X < -rad || pos.X > screenRect.Width + rad || pos.Y < -rad || pos.Y > screenRect.Height + rad);
        }


        public static void Line(int x1, int y1, int x2, int y2, Color color, int jumpSize, PixelFunction pixelFunc) //change to
        {
            jumpSize = Math.Max(jumpSize, 1); // check

            int x, y;
            int deltaX = Math.Abs(x1 - x2);
            int deltaY = Math.Abs(y1 - y2);

            int N = Math.Max(deltaX, deltaY);

            if (N == 0)
            {
                //pixelFunc(x1, y1, color); //can remove
            }
            else
            {

                float dx = (float)(x2 - x1) / N;
                float dy = (float)(y2 - y1) / N;

                dx = dx * jumpSize;
                dy = dy * jumpSize;
                N = N / jumpSize;

                for (int i = 0; i <= N; i++)
                {
                    x = (int)Math.Round(x1 + i * dx); //can be replaced by addtions
                    y = (int)Math.Round(y1 + i * dy);
                    pixelFunc(x, y, color);
                }
            }

        }

        public static void Circle(Vector2 pos, float rad, Color col, PixelFunction pixelFunc)
        {
            if (IsCircleOnScreen(pos, rad))
            {
                if (rad > 0)
                {
                    int iterations = (int)Math.Round(Math.PI * 2.3 * rad);                 
                    double dt = Math.PI * 2.0 / iterations;
                    double deg = 0;
                    for (int i = 0; i < iterations; i++)
                    {
                        pixelFunc(pos.X + (float)Math.Cos(deg) * rad, pos.Y + (float)Math.Sin(deg) * rad, col);                        
                        deg += dt;
                    }
                }
                else
                {
                    pixelFunc(pos.X , pos.Y, col);                                         
                }
            }
        }
     

    }
}
