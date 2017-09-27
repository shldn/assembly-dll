using System;
using System.Numerics;

namespace Assembly
{
    public class Assembly
    {
        public static Action<Assembly> onCreation;
        public Assembly(Vector3 pos) {
            position = pos;
            if (onCreation != null)
                onCreation(this);
        }
        public Vector3 position;
    }
}
