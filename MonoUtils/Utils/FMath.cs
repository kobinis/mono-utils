using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace XnaUtils
{

    //public static 

    //TOM: add a function that uniformly selects a random point in a band (Gets, random, maybe angle, so only do the radius)

    public static class FMath //change all values to floats
    {
        public static Random Rand = new Random(); //Todo: static per thread

        public static float FindScaleFitToBox(int width, int height, int limitWidth, int limitHeight)
        {
            float scaleX = limitWidth / (float)width;
            float scaleY = limitHeight / (float)height;
            float scale = Math.Min(scaleX, scaleY);
            return scale;
        }
        
        //implement WaveFunctionCollapse

        public static float FitInsideBox(ref int width, ref int height, int limitWidth, int limitHeight)
        {
            float scale = Math.Min(FindScaleFitToBox(width, height, limitWidth, limitHeight), 1f);
            width = (int)(width * scale);
            height = (int)(height * scale);
            return scale;
        }

        public static float MoveToTarget(float value, float target, float speed)
        {
            if (value > target)
            {
                value = Math.Max(value - speed, target);
            }
            if (value < target)
            {
                value = Math.Min(value + speed, target);
            }
            return value;
        }

        public static Rectangle ResizeRectangle(Rectangle rectangle, float scale)
        {
            return new Rectangle((int)(rectangle.X * scale), (int)(rectangle.Y * scale), (int)(rectangle.Width * scale), (int)(rectangle.Height * scale));
        }

        public static Vector2 GetMidPoint(Rectangle rect)
        {
            return new Vector2(rect.X + rect.Width / 2f, rect.Y + rect.Height / 2f);
        }

        public static void Shuffle<T>(IList<T> list, Random rng)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static float TriangleWave(float x, float period, float amplitude)
        {
            float y = (x % period) / period;
            y = y * 2 - 1;
            y = Math.Abs(y);
            y = y * amplitude;
            return y;
        }

        //Generade psudeo random number between 0 and max using hash function
        public static int PsuedoRandom(int seed, int max)
        {
            //return Math.Abs(seed.ToString().GetHashCode()) % max;
            return Math.Abs(seed.GetHashCode() * 523) % max;
        }

        public static int PsuedoRandomTri(int seed, int max)
        {
            return (int)(Math.Abs((int)FMath.TriangleWave(seed, 0.6712f, 123141242) % max));
        }


        //public static float SimplexNoise(float x, float y)
        //{
        //    float n0, n1, n2; // Noise contributions from the three corners
        //    // Skew the input space to determine which simplex cell we're in
        //    float F2 = 0.5f * (Mathf.Sqrt(3.0f) - 1.0f);
        //    float s = (x + y) * F2; // Hairy factor for 2D
        //    int i = Math.Floor(x + s);
        //    int j = Math.Floor(y + s);
        //    float G2 = (3.0f - Math.Sqrt(3.0f)) / 6.0f;
        //    float t = (i + j) * G2;
        //    float X0 = i - t; // Unskew the cell origin back to (x,y) space
        //    float Y0 = j - t;
        //    float x0 = x - X0; // The x,y distances from the cell origin
        //    float y0 = y - Y0;
        //    // For the 2D case, the simplex shape is an equilateral triangle.
        //    // Determine which simplex we are in.
        //    int i1, j1; // Offsets for second (middle) corner of simplex in (i,j) coords
        //    if (x0 > y0)
        //    { // lower triangle, XY order: (0,0)->(1,0)->(1,1)
        //        i1 = 1;
        //        j1 = 0;
        //    }
        //    else
        //    { // upper triangle, YX order: (0,0)->(0,1)->(1,1)
        //        i1 = 0;
        //        j1 = 1;
        //    }
        //    // A step of (1,0) in (i,j) means a step of (1-c,-c) in (x,y), and
        //    // a step of (0,1) in (i,j) means a step of (-c,1-c) in (x,y), where
        //    // c = (3-sqrt(3))/6
        //    float x1 = x0 - i1 + G2; // Offsets for middle corner in (x,y) unskewed coords
        //    float y1 = y0 - j1 + G2;
        //    float x2 = x0 - 1.0f + 2.0f * G2; //
        //}

            //public static float Fade(float t) { return t * t * t * (t * (t * 6 - 15) + 10); }
            //public static float PerlinNoise(float x, float y, float z)
            //{
            //    int X = (int)Math.Floor(x) & 255,                  // FIND UNIT CUBE THAT
            //        Y = (int)Math.Floor(y) & 255,                  // CONTAINS POINT.
            //        Z = (int)Math.Floor(z) & 255;
            //    x -= (float)Math.Floor(x);                                // FIND RELATIVE X,Y,Z
            //    y -= (float)Math.Floor(y);                                // OF POINT IN CUBE.
            //    z -= (float)Math.Floor(z);
            //    float u = Fade(x),                                // COMPUTE FADE CURVES
            //          v = Fade(y),                                // FOR EACH OF X,Y,Z.
            //          w = Fade(z);
            //    int A = p[X] + Y, AA = p[A] + Z, AB = p[A + 1] + Z,      // HASH COORDINATES OF
            //        B = p[X + 1] + Y, BA = p[B] + Z, BB = p[B + 1] + Z;      // THE 8 CUBE CORNERS,

            //    return Lerp(w, Lerp(v, Lerp(u, Grad(p[AA], x, y, z),  // AND ADD
            //                                 Grad(p[BA], x - 1, y, z)), // BLENDED
            //                         Lerp(u, Grad(p[AB], x, y - 1, z),  // RESULTS
            //                                 Grad(p[BB], x - 1, y - 1, z))),// FROM  8
            //                 Lerp(v, Lerp(u, Grad(p[AA + 1], x, y, z - 1),  // CORNERS
            //                                 Grad(p[BA + 1], x - 1, y, z - 1)), // OF CUBE
            //                         Lerp(u, Grad(p[AB + 1], x, y - 1, z - 1),
            //                                 Grad(p[BB + 1], x - 1, y - 1, z - 1))));
            //}
            //public static PerlinsNoise Noise = new PerlinsNoi


            public static int Noise(int x, int y)
        {
            int n = x + y * 57;
            n = (n << 13) ^ n;
            int nn = (n * (n * n * 15731 + 789221) + 1376312589) & 0x7fffffff;
            return nn;
        }

        public static int Noise(int x, int y, int z)
        {
            int n = x + y * 57 + z * 57 * 57;
            n = (n << 13) ^ n;
            int nn = (n * (n * n * 15731 + 789221) + 1376312589) & 0x7fffffff;
            return nn;
        }


        public static int Noise(int index)
        {
            int n = index;
            n = (n << 13) ^ n;
            int nn = (n * (n * n * 15731 + 789221) + 1376312589) & 0x7fffffff;
            return nn;
        }


        //        uint32_t noise(uint32_t index);

        //// usage
        //for (uint32_t i = 0; i< 512; i++) {
        //	// convert the noise to a float in [0, 1)
        //	float x = (float)noise(i) * 1.0p-32f;

        //	// ... do something with x
        //}
        

        public static int GetRandomIndex(int[] weights, Random rand)
        {
            int sum = 0;
            for (int i = 0; i < weights.Length; i++)
            {
                sum += weights[i];
            }
            int randValue = rand.Next(sum);
            int index = 0;
            while (randValue > weights[index])
            {
                randValue -= weights[index];
                index++;
            }
            return index;
        }

        public static Rectangle GetRectangle(Vector2 position, Vector2 size)
        {
            return new Rectangle((int)(position.X - size.X * 0.5f), (int)(position.Y - size.Y * 0.5f), (int)size.X, (int)size.Y);
        }

        /// <summary>
        /// Find the Greatest Common Divisor
        /// </summary>
        /// <param name="a">Number a</param>
        /// <param name="b">Number b</param>
        /// <returns>The greatest common Divisor</returns>
        public static long GCD(long a, long b)
        {
            while (b != 0)
            {
                long tmp = b;
                b = a % b;
                a = tmp;
            }
            return a;
        }

        /// <summary>
        /// Find the Least Common Multiple
        /// </summary>
        /// <param name="a">Number a</param>
        /// <param name="b">Number b</param>
        /// <returns>The least common multiple</returns>
        public static long LCM(long a, long b)
        {
            return (a * b) / GCD(a, b);
        }

        public static int Mod(int a, int b)
        {
            return (a % b + b) % b;
        }

        public static int Zigzag(int a, int b)
        {
            return Math.Abs(Mod(a + b, 2 * b) - b);
        }


        public static float AngleDiff(float deg1, float deg2) //change names
        {
            return AngleAbs(deg2 - deg1) * DegSign(deg2 - deg1);
        }

        public static float AngleAbs(float deg)
        {
            return Math.Abs(RealMod(Math.Abs(deg) + MathHelper.Pi, MathHelper.TwoPi) - MathHelper.Pi);
        }

        public static float DegSign(float deg) //ToDo
        {
            if (float.IsNaN(deg))
                //  return 0;
                throw new Exception();
            //return 0;//MyMath.Rand.Next(2) * 2 - 1; //Remove ? //throw exception
            else
                return Math.Sign(deg) * Math.Sign(RealMod(Math.Abs(deg) + MathHelper.Pi, MathHelper.TwoPi) - MathHelper.Pi);
        }

        public static float Frac(float value)
        {
            return value - (float)Math.Truncate(value);
        }

        /// <summary>Given an angle (in radians), returns it in [0, 2pi]</summary>        
        public static float NonNegativeAngle(float radians)
        {
            var result = radians % MathHelper.TwoPi;
            if (result < 0)
                result += MathHelper.TwoPi;

            return result;
        }

        public static float RealMod(float x, float y)
        {
            float res = x / y;
            return (float)(res - Math.Truncate(res)) * y;
        }

        /// <summary>Given two angles (in radians), returns the smallest rotation which would turn the second into the first</summary>        
        public static float SmallestAngleDiff(float radians1, float radians2)
        {
            var result = (NonNegativeAngle(radians1) - NonNegativeAngle(radians2)) % MathHelper.TwoPi;

            if (result > MathHelper.Pi)
                return result - MathHelper.TwoPi;
            if (result < -MathHelper.Pi)
                return result + MathHelper.TwoPi;
            return result;
        }

        /// <summary>
        /// Transforms from a uniformly distributed [0,1] interval to [minRadius,maxRadius] that will reslut in a uiform distribtion on a ring
        /// </summary>
        /// <param name="value"></param>
        /// <param name="maxRadius"></param>
        /// <param name="minRadius"></param>
        /// <returns></returns>
        public static float TransformToRadius(float value, float maxRadius, float minRadius = 0)
        {
            float radius = (float)Math.Sqrt(value * (maxRadius * maxRadius - minRadius * minRadius) + minRadius * minRadius);
            return radius;
        }

        public static bool Bern(float p, Random random)
        {
            return random.NextDouble() < p;
        }

        //Xna

        public static Vector2 MoveTowards(Vector2 initPos, Vector2 target, float speed = 1f)
        {
            Vector2 diff = target - initPos;
            float length = diff.Length();
            if (length <= speed)
            {
                return target;
            }
            return initPos + (diff / length) * speed;
        }

        /// <summary>
        /// Converts from Polar to a Cartesian coordinates
        /// </summary>
        /// <param name="radius">radius</param>
        /// <param name="angle"> Angle in radians</param>
        /// <returns></returns>
        public static Vector2 ToCartesian(float radius, float angle)
        {
            return new Vector2(radius * (float)Math.Cos(angle), radius * (float)Math.Sin(angle));
        }

        public static float GetRotation(Vector2 vector)
        {
            return (float)Math.Atan2(vector.Y, vector.X);
        }

        public static Vector2 RotateVector(Vector2 rotationVector, Vector2 vector2)
        {
            return new Vector2(rotationVector.X * vector2.X - rotationVector.Y * vector2.Y, rotationVector.X * vector2.Y + rotationVector.Y * vector2.X);
        }

        public static Vector2 RotateVector(Vector2 vector, float angle)
        {
            var sin = (float)Math.Sin(angle);
            var cos = (float)Math.Cos(angle);

            return new Vector2(vector.X * cos - vector.Y * sin, vector.X * sin + vector.Y * cos);
        }


        /// <summary>
        /// Returens the intercection point of a rectangle centerd at (0,0) with a vector ending at point
        /// </summary>
        public static Vector2 RectangleIntersectionPoint(Vector2 point, Vector2 halfSize)
        {
            float t1 = halfSize.Y / (Math.Abs(point.Y) + 0.001f);
            float x = Math.Min(halfSize.X, Math.Abs(t1 * point.X)) * Math.Sign(point.X);
            float t2 = halfSize.X / (Math.Abs(point.X) + 0.001f);
            float y = Math.Min(Math.Abs(t2 * point.Y), halfSize.Y) * Math.Sign(point.Y);
            return new Vector2(x, y);
        }

        public static float FindScale(Vector2 originalSize, Vector2 targetSize)
        {
            if (originalSize.X / originalSize.Y > targetSize.X / targetSize.Y)
            {
                return targetSize.X / originalSize.X;
            }
            else
            {
                return targetSize.Y / originalSize.Y;
            }
        }

        public static float FindBigScale(Vector2 originalSize, Vector2 targetSize)
        {
            if (originalSize.X / originalSize.Y > targetSize.X / targetSize.Y)
            {
                return targetSize.Y / originalSize.Y;
            }
            else
            {
                return targetSize.X / originalSize.X;
            }
        }

        /// <summary>
        /// Fit the originalSize inside the targetSize preserving the aspect ratio
        /// </summary>
        /// <param name="originalSize"></param>
        /// <param name="targetSize"></param>
        /// <returns></returns>
        public static Vector2 FitSize(Vector2 originalSize, Vector2 targetSize)
        {
            Vector2 fitedSize = new Vector2();

            if (originalSize.X / originalSize.Y > targetSize.X / targetSize.Y)
            {

                fitedSize.X = targetSize.X;
                fitedSize.Y = targetSize.X / originalSize.X * originalSize.Y;
            }
            else
            {
                fitedSize.Y = targetSize.Y;
                fitedSize.X = targetSize.Y / originalSize.Y * originalSize.X;
            }
            return fitedSize;
        }

        /// <summary>
        /// Cover the targetSize with originalSize preserving the aspect ratio
        /// </summary>
        /// <param name="originalSize"></param>
        /// <param name="targetSize"></param>
        /// <returns></returns>
        public static Vector2 CoverSize(Vector2 originalSize, Vector2 targetSize) //TODO: check
        {
            throw new NotImplementedException();
        }

        public static Vector2 GetFormationPosition(int index)
        {
            int xIndex = (int)Math.Floor((-3 + Math.Sqrt(1 + 8 * index)) / 2) + 1;
            float ypos = (index - Math.Max(((xIndex - 1) * (xIndex - 1) + 3 * (xIndex - 1)) / 2f + 1, 0)) - xIndex / 2f;
            return new Vector2(-xIndex, ypos);
        }

        public static Vector2 GetFormationPosition(int index, float angle)
        {
            return RotateVector(GetFormationPosition(index), angle);
        }

        public static Rectangle CenterAndSizeToRect(Vector2 center, Vector2 size)
        {
            Vector2 halfSize = size * 0.5f;
            return new Rectangle((int)(center.X - halfSize.X + 0.5f), (int)(center.Y - halfSize.Y + 0.5f), (int)(size.X + 0.5f), (int)(size.Y + 0.5f));
        }

        public static Vector2 FlipX(Vector2 vector)
        {
            return new Vector2(-vector.X, vector.Y);
        }

        public static Vector2 FlipY(Vector2 vector)
        {
            return new Vector2(vector.X, -vector.Y);
        }

        public static float Rotation(Vector2 vector)
        {
            return (float)Math.Atan2(vector.Y, vector.X);
        }

        public static Vector2 MinMagVector(Vector2 vec1, Vector2 vec2)
        {
            float x = Math.Abs(vec1.X) < Math.Abs(vec2.X) ? vec1.X : vec2.X;
            float y = Math.Abs(vec1.Y) < Math.Abs(vec2.Y) ? vec1.Y : vec2.Y;
            return new Vector2(x, y);
        }

        //Path utils

        public static Vector2 LerpPath(List<Vector2> path, float time)
        {
            int index = Math.Min(Math.Max((int)time, 0), path.Count - 2);
            float amount = Math.Min(time - index, 1);
            return Vector2.Lerp(path[index], path[index + 1], amount);
        }

        public static Vector2 CubicPath(List<Vector2> path, float time)
        {
            Microsoft.Xna.Framework.Curve c = new Curve();
            // c.Keys.Add(new CurveKey()
            //    c.Evaluate()
            int index = Math.Min(Math.Max((int)time, 0), path.Count - 2);
            float amount = Math.Min(time - index, 1);
            return Vector2.Lerp(path[index], path[index + 1], amount);
        }


        public static void PathAdd(List<Vector2> path, Vector2 point, float mult = 1)
        {
            for (int i = 0; i < path.Count; i++)
            {
                path[i] = path[i] * mult + point;
            }
        }

        public static float Noise(float x, float y)
        {
            return (float)Math.Sin(x * y);
        }


        public static float NextFloat(this Random random)
        {
            return (float)random.NextDouble();
        }

        public static Vector2 PointInCircle(this Random random, float radius)
        {
            var angle = MathHelper.TwoPi * random.NextFloat();
            var distance = radius * (float)Math.Sqrt(random.NextFloat());
            return new Vector2((float)Math.Cos(angle) * distance, (float)Math.Sin(angle) * distance);
        }


        public static Vector2 PointOnElipse(this Random random, Vector2 size) //Change
        {
            return new Vector2((float)Math.Cos(random.NextDouble() * MathHelper.TwoPi) * size.X, (float)Math.Sin(random.NextDouble() * MathHelper.TwoPi) * size.Y);
        }

    }
}
