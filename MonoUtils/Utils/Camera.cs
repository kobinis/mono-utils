//using System;
//using System.Collections.Generic;
//using System.Text;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Input;
//using Microsoft.Xna.Framework.Graphics;
//using XnaUtils.Graphics;
//using System.Linq;
//using System.Runtime.Serialization;
//using SolarConflict;
//using SolarConflict.XnaUtils;

//namespace XnaUtils
//{   
//    [Serializable]
//    public class Camera
//    {        
//        public float Zoom;
//        public Vector2 Position;
//        [NonSerialized]
//        private Rectangle Bounds;
//        [NonSerialized]
//        public Matrix Transform;             

//        [NonSerialized]
//        public SpriteBatch SpriteBatch;
//        [NonSerialized]
//        private Texture2D defaultNormals;
//        [NonSerialized]
//        private Texture2D defaultRoughness;
//        [NonSerialized]
//        private Texture2D defaultEmission;

        
//        private Vector2 topLeft;
//        private Vector2 bottomRight;

//        public Camera()
//        {            
//            Position = Vector2.Zero;
//            Zoom = 0.75f;
            
//            Zoom = 1f;
//            Position = Vector2.Zero;
//            LightData = new LightData();
//            InitNonSerializable();
//        }

//        private void InitNonSerializable()
//        {
//            this.SpriteBatch = Game1.sb;
//            defaultNormals = TextureBank.Inst.GetTexture("defaultnormals");
//            defaultRoughness = TextureBank.Inst.GetTexture("defaultroughness");
//            defaultEmission = TextureBank.Inst.GetTexture("defaultEmission");
//            Bounds = ActivityManager.GraphicsDevice.Viewport.Bounds;
//        }

//        public Rectangle GetWorldRectangle(float mult = 1)
//        {
//            Vector2 screenSize = new Vector2(ActivityManager.ScreenWidth, ActivityManager.ScreenHeight) * mult / Zoom;
//            return new Rectangle((int)(Position.X - screenSize.X / 2), (int)(Position.Y - screenSize.Y / 2), (int)screenSize.X, (int)screenSize.Y);
//            //return new Rectangle((int)(Position.X - Bounds.Width / 2 * mult), (int)(Position.Y - Bounds.Height / 2 * mult), (int)(Bounds.Width * mult), (int)(Bounds.Height * mult));
//            //return new Rectangle((int)Position.X - Bounds.Width / 2, (int)Position.Y - Bounds.Height / 2, Bounds.Width, Bounds.Height);
//        }
    

//        [OnDeserialized]
//        public void OnDeserializedMethod(StreamingContext context)
//        {
//            InitNonSerializable();
//        }

//        public void UpdateCameraWorldPosition()
//        {
//            topLeft = Position - new Vector2(Bounds.Width / 2, Bounds.Height / 2) / Zoom;
//            bottomRight = Position + new Vector2(Bounds.Width / 2, Bounds.Height / 2) / Zoom;
//        }

//        public bool CheckIfOnScreen(Vector2 worldPos, float size)
//        {
//            return worldPos.X + size > topLeft.X && worldPos.X - size < bottomRight.X && worldPos.Y + size > topLeft.Y && worldPos.Y - size < bottomRight.Y;
//        }


//        public bool IsOnScreen(Vector2 worldPos, float size, float sizeMult = 1.5f)  // TODO: Make this more efficient
//        {
//            //return worldPos.X + size > topLeft.X && worldPos.X - size < bottomRight.X && worldPos.Y + size > topLeft.Y && worldPos.Y - size < bottomRight.Y;

//            int maxSize = (int)(size * sizeMult);
//            Rectangle inflatedScreenSize = new Rectangle(0, 0, ActivityManager.ScreenWidth, ActivityManager.ScreenHeight);
//            inflatedScreenSize.Inflate(maxSize, maxSize);

//            Vector2 screenPos = ActivityManager.ScreenCenter + (worldPos - Position) * Zoom;

//            return inflatedScreenSize.Contains((int)screenPos.X, (int)screenPos.Y);
//        }

//        public void UpdateMatrix()
//        {
//            UpdateCameraWorldPosition();
//            Transform = Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) *
//                    Matrix.CreateScale(Zoom) *
//                    Matrix.CreateTranslation(new Vector3(Bounds.Width * 0.5f, Bounds.Height * 0.5f, 0));
//            //UpdateVisibleArea();
//        }

//        public Vector2 GetScreenPos(Vector2 worldPos)
//        {
//            return ActivityManager.ScreenCenter + (worldPos - Position) * Zoom;
//        }

//        public Vector2 GetWorldPos(Vector2 screenPos)
//        {
//            return (screenPos - ActivityManager.ScreenCenter) / Zoom + Position;
//        }
        
//        public void SetCameraPosToDrawPosOnScreenAt(Vector2 worldPos, Vector2 screenPos)
//        {
//            Position = (ActivityManager.ScreenCenter - screenPos) / Zoom + worldPos;
//            UpdateMatrix();
//        }
          
//        public void CameraDraw(Texture2D texture, Vector2 pos, float rot, Vector2 size, Color col)
//        {
         
//                SpriteBatch.Draw(texture, pos, null, col, rot, new Vector2(texture.Width / 2f, texture.Height / 2f), size, SpriteEffects.None, 0);
//        }

//        public void CameraDraw(Texture2D texture, Vector2 pos, float rot, float size, Color col)
//        {            
//                SpriteBatch.Draw(texture, pos, null, col, rot, new Vector2(texture.Width / 2, texture.Height / 2), size, SpriteEffects.None, 0);            
//        }        

//        public void CameraDraw(Sprite sprite, Vector2 pos, float rot, float size, Color col, SpriteEffects spriteEffects = SpriteEffects.None)
//        {
//            if (!DrawLit)
//            {   
//                NormalMapEffect.CurrentTechnique = NormalTechniqueBasicSprite;
//                SpriteBatch.Draw(sprite.Texture, pos, null, col, rot, new Vector2(sprite.Width / 2, sprite.Height / 2), size, spriteEffects, 0);
//            }
//            else
//            {
//                NormalMapEffect.CurrentTechnique = NormalTechniqueWithLights;
//                DrawWithLighting(sprite, SpriteBatch, pos, null, col, rot, new Vector2(sprite.Width / 2, sprite.Height / 2), size, spriteEffects, 0, Position, Zoom, ActivityManager.ScreenCenter);
//            }
//        }

//        public void CameraDraw(Spritesheet sheet, int spriteIndex, Vector2 pos, float rot, float size, Color col)
//        {

//            var sourceRect = sheet.SourceRect(spriteIndex);
//            var position = new Vector2(sheet.SpriteWidth / 2, sheet.SpriteHeight / 2);

//            if (!DrawLit)
//            {
//                NormalMapEffect.CurrentTechnique = NormalTechniqueBasicSprite;
//                SpriteBatch.Draw(sheet.Sheet, pos, sourceRect, col, rot, position, size, SpriteEffects.None, 0);
//            }
//            else
//            {
//                NormalMapEffect.CurrentTechnique = NormalTechniqueWithLights;
//                DrawWithLighting(sheet.Sheet, SpriteBatch, pos, sourceRect, col, rot, position, size, SpriteEffects.None, 0, Position, Zoom, ActivityManager.ScreenCenter);
//            }
//        }

//        public Vector2 DrawText(SpriteFont font, string text, Vector2 position, Color color, float minScale = 0.25f, float maxScale = 2)
//        {
//            float clampedZoom = MathHelper.Clamp(Zoom, minScale, maxScale); //TODO: change
//            Vector2 size = font.MeasureString(text) * clampedZoom;
//            SpriteBatch.DrawString(font, text, position - size * 0.5f, color);
//            return Vector2.Zero;
//        }

        

//        ///// <param name="batch">A batch that isn't presently between Begin() and End() calls.</param>
//        ///// <param name="effects">Presently disregarded.</param>
//        ///// <param name="positionOfScreen">World position of screen.</param>
//        ///// <warning>Presently disregards effects arg and just applies the normal map effect.</warning>
//        void DrawWithLighting(Sprite sprite, SpriteBatch batch, Vector2 position, Rectangle? source, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth, Vector2 positionOfScreen, float zoom, Vector2 halfResolution)
//        {            
//            var graphicsDevice = batch.GraphicsDevice;

//            // Pass sprite-specific shader parameters
//            Camera.NormalEffectRotation.SetValue(rotation);
//            Camera.NormalEffectNormal.SetValue(sprite.NormalMap ?? defaultNormals);
//            Camera.NormalEffectRoughness.SetValue(sprite.RoughnessMap ?? defaultRoughness);
//            Camera.NormalEffectEmission.SetValue(sprite.EmissionMap ?? defaultEmission);

//            //Camera.NormalMapEffect.Parameters["MatrixTransform"].SetValue(Transform);

//            batch.Draw(sprite.Texture, position, source, color, rotation, origin, scale, effects, layerDepth); //scale
//            //batch.End();
//        }

//        /// <summary>Passes our lighting-related state to our shaders etc.</summary>
//        public void PrepareForLitDraw(List<GameObject> lights, Vector2 position)
//        {
//            if (!GraphicsSettings.UseLighting)
//                return;

//            if (LightData == null || lights.Count == 0)
//            {
//                Camera.NormalEffectNumLights.SetValue(0);
//                return;
//            }

//            LightData.SetValues(lights, position, this.Position);

//            // Pass lights etc to the shader   
            
//            NormalMapEffect.Parameters["NumLights"].SetValue(LightData.NumLights);
//            NormalMapEffect.Parameters["LightColors"].SetValue(LightData.LightColorsAndIntensity);
//            NormalMapEffect.Parameters["LightPositions"].SetValue(LightData.LightPositions);
//            NormalMapEffect.Parameters["LightHotspots"].SetValue(LightData.LightHotspots);
//            NormalMapEffect.Parameters["LightAttenuation"].SetValue(LightData.LightAttenuation);


            
//            NormalMapEffect.Parameters["NumLineLights"].SetValue(LightData.NumLineLights);// startPositions.Length);
//            if (LightData.NumLineLights == 0)
//                return;
//            NormalMapEffect.Parameters["LineLightPosition"].SetValue(LightData.LineLightPosition);
//            NormalMapEffect.Parameters["LineLightDirection"].SetValue(LightData.LineLightDirection);
//            NormalMapEffect.Parameters["LineLightLength"].SetValue(LightData.LineLightLength);
//            NormalMapEffect.Parameters["LineLightColor"].SetValue(LightData.LineLightColor);
//            NormalMapEffect.Parameters["LineLightAttenuation"].SetValue(LightData.LineLightAttenuation);
//        }
//    }
//}
