using System.Collections.Generic;
using System.Linq;

namespace DijkstraNetwork
{
    public class Program
    {
        static void Main(string[] args)
        {

        }

        private static List<Node> Initialisation()
        {
            var node1 = new Node(1);
            var node2 = new Node(2);
            var node3 = new Node(3);

            node1.Links.Add(node2, 1);
            node1.Links.Add(node3, 3);

            node2.Links.Add(node3, 1);

            return new List<Node> { node1, node2, node3 };
        }

        private static void FindShortestPath(List<Node> nodes, int startNodeId, int endNodeId)
        {
            Node startNode = nodes.Where(n => n.Id == startNodeId).Single();

            startNode.Links.Min(l => l.Value);
        }
    }
}
