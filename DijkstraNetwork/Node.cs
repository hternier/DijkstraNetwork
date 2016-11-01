using System.Collections.Generic;

namespace DijkstraNetwork
{
    public class Node
    {
        public Dictionary<Node, int> Childrens = new Dictionary<Node, int>();

        public string Id { get; }

        public int? PathWeight { get; set; } = null;

        public bool IsVisited { get; set; } = false;

        public Node(string id)
        {
            Id = id;
        }
    }
}
