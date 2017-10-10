using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;

namespace EGL
{
    public class Colony
    {
        private float minBoundary = -10;
        private float maxBoundary = 10;
        private List<Assembly> assemblies = new List<Assembly>(); public List<Assembly> Assemblies { get { return assemblies; } }

        // simulation control
        private static bool killAll = false;
        private bool running = true;
		private float lifetime = 0f; public float Lifetime { get { return lifetime; } }
		public float TimeStep { get { return sleepMillis * 0.001f; } }

        // frame rate control
        private int sleepMillis = 10;

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
				//int numNodes = Random.Range(3, 8);
				int numNodes = 10;
                assemblies.Add(new Assembly(this, RandomPos(), numNodes));
            }
        }

        private void UpdateLoop() {
            while (running && !killAll) {
                Update();
				lifetime += sleepMillis * 0.001f;
                Thread.Sleep(sleepMillis);
            }
        } // End of UpdateLoop().

        void Update() {
            for (int i = 0; i < assemblies.Count; ++i)
                assemblies[i].Update();
        } // End of Update().
		
        private Vector3 RandomPos() {
            return new Vector3(Random.Range((int)minBoundary, (int)maxBoundary), Random.Range((int)minBoundary, (int)maxBoundary), Random.Range((int)minBoundary, (int)maxBoundary));
        } // End of RandomPos().

		/*
        public List<Tuple<int,Vector3>> GetPositions() {
            List<Tuple<int, Vector3>> positions = new List<Tuple<int, Vector3>>();
            for (int i = 0; i < assemblies.Count; ++i) {
                positions.Add(new Tuple<int,Vector3>(assemblies[i].Id,assemblies[i].Position));
            }
            return positions;
        } // End of GetPositions().
		*/
		
    } // End of Colony.
}
