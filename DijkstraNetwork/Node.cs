using System.Collections.Generic;

namespace DijkstraNetwork
{
    public class Node
    {
        //private HashSet<Node> links;
        public Dictionary<Node, int> Links = new Dictionary<Node, int>();

        public int Id { get; }

        public Node(int id)
        {
            Id = id;
        }
    }
}
