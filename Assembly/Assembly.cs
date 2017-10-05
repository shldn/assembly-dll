using System;
using System.Collections.Generic;
using System.Numerics;

namespace Assembly
{
    public class Assembly
    {
        // static
        static int idCounter = 0;

        // local
        private int _id;
        private Vector3 _position;

        // locks
        object posLock = new object();

        // accessers
        public int Id { get { return _id; } set { _id = value; } }
        public Vector3 Position { get { lock (posLock) { return _position; } } set { lock (posLock) { _position = value; } } }

        // data structures
        public List<Node> nodes = new List<Node>();

        // events
        public static Action<Assembly> onCreation;

        public Assembly(Vector3 pos, int numNodes) {
            _position = pos;
            _id = idCounter++;
            for (int i = 0; i < numNodes; ++i)
                nodes.Add(new Node(this, new Triplet(i, i, i)));
            if (onCreation != null)
                onCreation(this);
        }

    }
}
