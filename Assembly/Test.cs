using System;
using System.Numerics;

namespace EGL
{
    public class Test
    {
        public static string Hello() {
            Vector3 v1 = new Vector3(1,1,1);
            Vector3 v2 = new Vector3(2,2,2);
            return "Hello World " + (v1 + v2);
        }

        public static Vector3 Add(Vector3 v1, Vector3 v2) {
            return v1 + v2;
        }
    }
}
