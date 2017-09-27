
namespace Assembly
{
    class Node
    {
        public Node(Assembly parent, Triplet localHexPos) {
            this.assembly = parent;
            this.localHexPos = localHexPos;
        }
        public Assembly assembly;
        public Triplet localHexPos;
    }
}
