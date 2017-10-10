using System.Collections.Generic;
using System.Numerics;

namespace EGL
{
    public class Node
    {
        public Node(Assembly parent, Triplet localHexPos) {
            this.assembly = parent;
            this.localHexPos = localHexPos;

			myNodeType = (NodeType)Random.Range(0, 4);
        }
        private Assembly assembly; public Assembly MyAssembly { get { return assembly; } }
        private Triplet localHexPos; public Triplet LocalHexPos { get { return localHexPos; } }
        public Vector3 LocalUnitPos { get { return HexUtilities.HexToWorld(localHexPos); } }

		// This accounts for the center of mass of the assembly.
        public Vector3 LocalRealPos { get { return -assembly.LocalCenterOfMassOffset + HexUtilities.HexToWorld(localHexPos); } }

		private List<Node> neighborsList = new List<Node>(); public List<Node> NeighborsList { get { return neighborsList; } }
		// Key is the direction from us to the neighbor.
		private Dictionary<Triplet, Node> neighborsDict = new Dictionary<Triplet, Node>();

		public Vector3 WorldPosition { get { return assembly.Position + Vector3.Transform(LocalUnitPos, assembly.Rotation); } }
		
		private NodeType myNodeType = NodeType.basic; public NodeType MyNodeType { get { return myNodeType; } }


		public void AssignNeighbor(Node newNeighbor){
			neighborsList.Add(newNeighbor);
			neighborsDict.Add(newNeighbor.LocalHexPos - localHexPos, newNeighbor);
		} // End of AssignNeighbor().

    } // End of Node.

	public enum NodeType {
		basic,
		detector,
		processor,
		motor
	} // End of NodeType.

}
