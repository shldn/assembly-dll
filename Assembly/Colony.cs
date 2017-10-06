using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;

namespace Assembly
{
    public class Colony
    {
        float minBoundary = -10;
        float maxBoundary = 10;
        List<Assembly> assemblies = new List<Assembly>();

        // simulation control
        static bool killAll = false;
        bool running = true;
        public volatile float speed = 0.001f;

        // frame rate control
        int sleepMillis = 10;

        public static Colony Create(int num) {
            // Create the new Colony
            Colony colony = new Colony(num);

            // Start the update loop in the new thread.
            Thread t = new Thread(new ThreadStart(colony.UpdateLoop));
            t.Start();

            return colony;
        }

        public static void KillAll() {
            killAll = true;
        }

        private Colony(int num) {
            Console.WriteLine("Creating a colony " + num);
            int numNodes = Random.Range(3, 8);
            for (int i = 0; i < num; ++i) {
                assemblies.Add(new Assembly(RandomPos(), numNodes));
            }
        }

        private void UpdateLoop() {
            while (running && !killAll) {
                Update();
                Thread.Sleep(sleepMillis);
            }
        }

        void Update() {
            for (int i = 0; i < assemblies.Count; ++i) {
                assemblies[i].Position += Vector3.Transform(Vector3.UnitZ, assemblies[i].Rotation) * speed;
            }
        }

        public List<Tuple<int,Vector3>> GetPositions() {
            List<Tuple<int, Vector3>> positions = new List<Tuple<int, Vector3>>();
            for (int i = 0; i < assemblies.Count; ++i) {
                positions.Add(new Tuple<int,Vector3>(assemblies[i].Id,assemblies[i].Position));
            }
            return positions;
        }

        private Vector3 RandomPos() {
            return new Vector3(Random.Range((int)minBoundary, (int)maxBoundary), Random.Range((int)minBoundary, (int)maxBoundary), Random.Range((int)minBoundary, (int)maxBoundary));
        }
    }
}
