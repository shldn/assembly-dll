using System.Collections.Generic;

namespace Assembly
{
    public class Node
    {
        public Node(Assembly parent, Triplet localHexPos) {
            this.assembly = parent;
            this.localHexPos = localHexPos;
        }
        private Assembly assembly; public Assembly MyAssembly { get { return assembly; } }
        private Triplet localHexPos; public Triplet LocalHexPos { get { return localHexPos; } }

		private List<Node> neighborsList = new List<Node>(); public List<Node> NeighborsList { get { return neighborsList; } }
		// Key is the direction from us to the neighbor.
		private Dictionary<Triplet, Node> neighborsDict = new Dictionary<Triplet, Node>();

		public void AssignNeighbor(Node newNeighbor){
			neighborsList.Add(newNeighbor);
			neighborsDict.Add(newNeighbor.LocalHexPos - localHexPos, newNeighbor);
		} // End of AssignNeighbor().

    } // End of Node.
}
