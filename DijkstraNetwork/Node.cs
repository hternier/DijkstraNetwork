using System.Collections.Generic;

namespace DijkstraNetwork
{
    public class Node
    {
        //private HashSet<Node> links;
        public Dictionary<Node, int> Childrens = new Dictionary<Node, int>();

        public int Id { get; }

        public int? PathWeight { get; set; } = null;

        public bool IsVisited { get; set; } = false;

        public Node(int id)
        {
            Id = id;
        }
    }
}
