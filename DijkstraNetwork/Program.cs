using System;
using System.Collections.Generic;
using System.Linq;

namespace DijkstraNetwork
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");

            List<Node> nodes = Initialisation();
            List<Node> bestPath = DijkstraGo(nodes, 1, 3);

            Console.WriteLine("Compute ended. Result:");

            int index = 0;
            foreach (Node node in bestPath)
            {
                Console.WriteLine("Node {0}: {1} ({2})", index++, node.Id, node.PathWeight);
            }

            Console.WriteLine("Total path weight: {0}", bestPath.Last().PathWeight);

            Console.ReadKey();
        }

        private static List<Node> Initialisation()
        {
            var node1 = new Node(1);
            var node2 = new Node(2);
            var node3 = new Node(3);

            node1.Childrens.Add(node2, 1);
            node1.Childrens.Add(node3, 3);

            node2.Childrens.Add(node3, 1);

            return new List<Node> { node1, node2, node3 };
        }

        private static List<Node> DijkstraGo(List<Node> nodes, int startNodeId, int endNodeId)
        {
            var nodesAncestors = new Dictionary<Node, Node>();

            Node startNode = nodes.Where(n => n.Id == startNodeId).Single();
            Node endNode = nodes.Where(n => n.Id == endNodeId).Single();
            Node actualNode = null;

            startNode.PathWeight = 0;

            while (actualNode != endNode)
            {
                actualNode = nodes.Where(n => !n.IsVisited && n.PathWeight != null).OrderBy(n => n.PathWeight).First();
                ComputeChildeNodes(actualNode, nodesAncestors);
            }

            return GetPath(startNode, endNode, nodesAncestors);
        }

        internal static void ComputeChildeNodes(Node actualNode, Dictionary<Node, Node> nodesAncestors)
        {
            actualNode.IsVisited = true;

            foreach (var T in actualNode.Childrens)
            {
                Node childeNode = T.Key;
                int nodeWeight = T.Value;

                // Here the magic begin
                if (!childeNode.IsVisited && (childeNode.PathWeight == null || childeNode.PathWeight > actualNode.PathWeight + nodeWeight))
                {
                    childeNode.PathWeight = actualNode.PathWeight + nodeWeight;
                    nodesAncestors[childeNode] = actualNode;
                }
            }
        }

        internal static List<Node> GetPath(Node startNode, Node endNode, Dictionary<Node, Node> nodesAncestors)
        {
            var path = new List<Node>();

            path.Add(endNode);
            FindNext(startNode, endNode, path, nodesAncestors);
            
            return path.Reverse<Node>().ToList();
        }

        private static void FindNext(Node endNode, Node currentNode, List<Node> path, Dictionary<Node, Node> nodesAncestors)
        {
            currentNode = nodesAncestors[currentNode];
            path.Add(currentNode);

            if (currentNode != endNode)
            {
                FindNext(endNode, currentNode, path, nodesAncestors);
            }
            else
            {
                return;
            }

        }
    }
}
