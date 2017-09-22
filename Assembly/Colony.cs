using System;
using System.Collections.Generic;
using System.Numerics;

namespace Assembly
{
    public class Colony
    {
        float minBoundary = -10;
        float maxBoundary = 10;
        List<Assembly> assemblies = new List<Assembly>();
        Random random = new Random();

        public Colony(int num) {
            
            for (int i = 0; i < num; ++i) {
                assemblies.Add(new Assembly(RandomPos()));
            }
        }
        // This should be internal, but exposing for now to test
        public void Update() {
            for (int i = 0; i < assemblies.Count; ++i) {
                assemblies[i].position += 0.001f * assemblies[i].position;
            }
        }

        public List<Vector3> GetPositions() {
            List<Vector3> positions = new List<Vector3>();
            for(int i = 0; i < assemblies.Count; ++i) {
                positions.Add(assemblies[i].position);
            }
            return positions;
        }

        private Vector3 RandomPos() {
            return new Vector3(random.Next((int)minBoundary, (int)maxBoundary), random.Next((int)minBoundary, (int)maxBoundary), random.Next((int)minBoundary, (int)maxBoundary));
        }
    }
}
