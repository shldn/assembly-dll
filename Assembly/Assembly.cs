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
        private Quaternion _rotation;

		private Vector3 localCenterOfMass = Vector3.Zero;
		public Vector3 LocalCenterOfMass { get { return localCenterOfMass; } }
		public Vector3 WorldCenterOfMass { get { return Position + Vector3.Transform(localCenterOfMass, Rotation); } }

        // locks
        object posLock = new object();
        object rotLock = new object();

        // accessers
        public int Id { get { return _id; } set { _id = value; } }
        public Vector3 Position { get { lock (posLock) { return _position; } } set { lock (posLock) { _position = value; } } }
        public Quaternion Rotation { get { lock (rotLock) { return _rotation; } } set { lock (rotLock) { _rotation = value; } } }

        // data structures
        private List<Node> nodesList = new List<Node>(); public List<Node> NodesList { get { return nodesList; } }
		private Dictionary<Triplet, Node> nodesDict = new Dictionary<Triplet, Node>();

        // events
        public static Action<Assembly> onCreation;
		

		// Ctor.
        public Assembly(Vector3 pos, int numNodes) {
            _id = idCounter++;
            _position = pos;
			_rotation = Random.RandomRotation();

            for (int i = 0; i < numNodes; ++i)
                AddRandomNode();

			CalculateCenterOfMass();

            if (onCreation != null)
                onCreation(this);
        } // End of Ctor.


		// Add a new node to the Assembly at local position.
		private Node AddNode(Triplet localHexPos){
			Node newNode = new Node(this, localHexPos);
			nodesList.Add(newNode);
			nodesDict.Add(localHexPos, newNode);
			return newNode;
		} // End of AddNode().

		// Add a new node to the Assembly at local position.
		private void AddRandomNode(){
			int randomStartNode = Random.random.Next(0, nodesList.Count);
			Node rootNode = null;
			bool foundLegalPos = false;

			// If we don't have any nodes... we'll just make one at 0, 0, 0.
			Triplet newNodePos = Triplet.zero;
			if(nodesList.Count == 0){
				foundLegalPos = true;
			} else {
				for(int i = 0; i < nodesList.Count; i++){
					int wrappedNodeIndex = (randomStartNode + i) % nodesList.Count;
					rootNode = nodesList[wrappedNodeIndex];
					if(rootNode.NeighborsList.Count > 1)
						continue;

					int randomStartDir = Random.random.Next(12);
					for(int j = 0; j < nodesList.Count; j++){
						int wrappedDirIndex = (randomStartNode + j) % nodesList.Count;
						int curDir = wrappedDirIndex % 12;
						newNodePos = rootNode.LocalHexPos + HexUtilities.Adjacent(curDir);
						if(!nodesDict.ContainsKey(newNodePos)){
							foundLegalPos = true;
							break;
						}
					}

					if(foundLegalPos)
						break;
				}
			}

			if(foundLegalPos){
				Node newNode = AddNode(newNodePos);
				if(rootNode != null){
					rootNode.AssignNeighbor(newNode);
					newNode.AssignNeighbor(rootNode);
				}
			}

		} // End of AddNode().

		private void CalculateCenterOfMass(){
			localCenterOfMass = Vector3.Zero;
			for(int i = 0; i < nodesList.Count; i++)
				localCenterOfMass += nodesList[i].LocalHexPos.ToVector3();

			localCenterOfMass /= nodesList.Count;
		} // End of CalculateCenterOfMass().


		/*
		// Remove a specific node from the Assembly.
		private void RemoveNode(Node nodeToRemove){
			nodesDict.Remove(nodeToRemove.LocalHexPos);
			nodesList.Remove(nodeToRemove);
		} // End of AddNode().
		*/

    } // End of Assembly.
}


