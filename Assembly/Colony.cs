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

        // frame rate control
        int sleepMillis = 10;

        // locks
        Object transformDataLock = new object();

        // helpers
        Random random = new Random();

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
            for (int i = 0; i < num; ++i) {
                assemblies.Add(new Assembly(RandomPos()));
            }
        }

        private void UpdateLoop() {
            while (running && !killAll) {
                Update();
                Thread.Sleep(sleepMillis);
            }
        }

        // This should be internal, but exposing for now to test
        public void Update() {
            lock (transformDataLock) {
                for (int i = 0; i < assemblies.Count; ++i) {
                    assemblies[i].position += 0.001f * assemblies[i].position;
                }
            }
        }

        public List<Vector3> GetPositions() {
            List<Vector3> positions = new List<Vector3>();
            lock (transformDataLock) {
                for (int i = 0; i < assemblies.Count; ++i) {
                    positions.Add(assemblies[i].position);
                }
            }
            return positions;
        }

        private Vector3 RandomPos() {
            return new Vector3(random.Next((int)minBoundary, (int)maxBoundary), random.Next((int)minBoundary, (int)maxBoundary), random.Next((int)minBoundary, (int)maxBoundary));
        }
    }
}
