using System;
using System.Numerics;

namespace Assembly
{
    public class Assembly
    {
        static int idCounter = 0;

        // events
        public static Action<Assembly> onCreation;

        public Assembly(Vector3 pos) {
            position = pos;
            id = idCounter++;
            if (onCreation != null)
                onCreation(this);
        }
        public int id;
        public Vector3 position;
    }
}
