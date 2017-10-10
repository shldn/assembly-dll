using System;
using System.Numerics;

namespace EGL
{
    public class Random
    {
		public static System.Random random = new System.Random();

		public static Quaternion RandomRotation(){
			float u1 = (float)random.NextDouble();
			float u2 = (float)random.NextDouble();
			float u3 = (float)random.NextDouble();

			return new Quaternion(
				(float)(Math.Sqrt(1f - u1) * Math.Sin(2f * Math.PI * u2)),
				(float)(Math.Sqrt(1f - u1) * Math.Cos(2f * Math.PI * u2)),
				(float)(Math.Sqrt(u1) * Math.Sin(2f * Math.PI * u3)),
				(float)(Math.Sqrt(u1) * Math.Cos(2f * Math.PI * u3))
			);

		} // End of RandomRotation().

        public static int Range(int min, int max) {
            return random.Next(min, max);
        }
	} // End of Range().
}