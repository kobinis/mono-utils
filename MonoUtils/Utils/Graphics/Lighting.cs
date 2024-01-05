//using Microsoft.Xna.Framework;
//using SolarConflict;
//using System;
//using System.Collections.Generic;

//namespace XnaUtils.Graphics 
//{
//    //[Serializable]
//    //public class LightGrid
//    //{
//    //    const int CellNumX = 32;
//    //    const int CellNumY = 34;
//    //    public List<GameObject> Lights;
//    //    List<PointLight>[,] lightsGrid;

//    //    public float lowestValue;

//    //    public LightGrid()
//    //    {
//    //        Lights = new List<GameObject>();
//    //        lowestValue = 100000000;            
//    //    }
        

//    //    public static void AddLights(List<GameObject> potentialLights, Vector2 position)
//    //    {
            
//    //        int lightCount = 0;
//    //        float lowestValue = float.MaxValue;
//    //        foreach (var item in potentialLights)
//    //        {
//    //            float lightValue = item.Light.LightStrengthAt(item.Position, position);
//    //            if(lightCount < LightingGlobals.MaxLightsPerObject || lightValue > lowestValue)
//    //            {
                    
//    //            }
//    //        }
//    //    }

//    //}

//    public static class LightingGlobals 
//    {
//        /// <summary>By default, light intensity attenuates by a factor of (distance*c)^DefaultIntensityExponent,
//        /// where c is some constant (see DefaultIntensityHalvingDistance)</summary>
//        public static int DefaultIntensityExponent = 2;

//        /// <summary>For saturation prevention, see shader.</summary>
//        public static float MaxIntensity = 2f;


//        /// <summary>The max number of lights to consider in-depth when illuminating an object.</summary>
//        /// <remarks>Note that this is bounded above by the size of the normal map shader's LightDirections array.
//        /// Lights are ignored in ascending order of illumination (so the light which illuminates an object least will be ignored first).</remarks>
//        public static int MaxLightsPerObject = 32;

//        /// <remarks>We work out most of our lighting in 2D, then convert everything to 3D directional lights and give their direction vectors some
//        /// arbitrary Z component. This is that component. Note that the direction vectors point towards the lights, so this should generally be positive
//        /// (for lights on the player's side of the screen)</remarks>
//        public static float Tilt = 0.2f;        
//    }


//    //public interface IIntensityCalculator : ICloneable {
//    //    float IntensityAt(float distance);

//    //    float DistanceForIntensity(float targetIntensity);
//    //}
//    [Serializable]
//    public class LineLight
//    {
//        public Vector2 StartPositions;
//        public Vector2 Directions;
//        public float Length;
//        public Vector3 Color;
//        public float Attenuations;

//        public bool InRange(Camera camera)
//        {
//            float range = Attenuations;
//            Vector2 endPoint = StartPositions + Directions * Length;

//            Vector2 diff = (StartPositions - camera.Position) * camera.Zoom;            
//            Vector2 diffEnd = (endPoint - camera.Position) * camera.Zoom;
//            return Math.Abs(diff.X) < range + ActivityManager.ScreenWidth & Math.Abs(diff.Y) < range + ActivityManager.ScreenHeight ||
//                Math.Abs(diffEnd.X) < range + ActivityManager.ScreenWidth & Math.Abs(diffEnd.Y) < range + ActivityManager.ScreenHeight;
//        }

//        public object Clone()
//        {
//            return MemberwiseClone();
//        }
//    }

//    [Serializable]
//    public class PointLight
//    {               
//        /// /// <summary>The base color of the light, before being modified by intensity.</summary>
//        public Vector3 BaseColor;
//        /// <remarks>The light's color at a given point will be BaseColor * intensityAtPoint.
//        public float Intensity;
//        /// <summary>
//        /// Range in pixels
//        /// </summary>
//        public float Attenuation;
//        public float Hotspot;
   
//        //Simplified constructor
//        public PointLight(Vector3 baseColor, float attenuation, float intensity)
//        {
//            BaseColor = baseColor;
//            Attenuation = attenuation;
//            Intensity = intensity;
//            Hotspot = 0;
//        }

//        public PointLight(Vector3 baseColor, float attenuation, float intensity, float hotspot)
//        {
//            BaseColor = baseColor;
//            Attenuation = attenuation;
//            Intensity = intensity;
//            Hotspot = hotspot;
//        }
       
//        public object Clone()
//        {
//            return MemberwiseClone();
//        }

//        //public bool InRange(Vector2 lightPosition, Vector2 position)
//        //{
//        //    Vector2 diff = lightPosition - position;
//        //    float range = Attenuation + Hotspot + 1000;
//        //    return Math.Abs( diff.X) < range &  Math.Abs( diff.Y) < range;
//        //}

//        public bool InRange(Vector2 lightPosition, Camera camera)
//        {
//            Vector2 diff = (lightPosition - camera.Position) * camera.Zoom;
//            float range = Attenuation + Hotspot;
//            return Math.Abs(diff.X) < range + ActivityManager.ScreenWidth & Math.Abs(diff.Y) < range + ActivityManager.ScreenHeight;
//        }

//        public float LightStrengthAt(Vector2 lightPosition, Vector2 position)
//        {
//            float lightValue = Intensity * (1 - (lightPosition - position).Length() / (Attenuation + Hotspot));                            
//            return lightValue;
//        }

//        public PointLight GetWorkingCopy()
//        {
//            return (PointLight)MemberwiseClone();
//        }
//    }
        
//}
