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
            List<Node> bestPath = DijkstraGo(nodes, "A", "J");

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
            var A = new Node("A");
            var B = new Node("B");
            var C = new Node("C");
            var D = new Node("D");
            var E = new Node("E");
            var F = new Node("F");
            var G = new Node("G");
            var H = new Node("H");
            var I = new Node("I");
            var J = new Node("J");

            A.Childrens.Add(B, 85);
            A.Childrens.Add(C, 217);
            A.Childrens.Add(E, 175);

            B.Childrens.Add(F, 80);

            F.Childrens.Add(I, 250);

            I.Childrens.Add(J, 84);

            C.Childrens.Add(G, 186);
            C.Childrens.Add(H, 103);

            H.Childrens.Add(D, 183);
            H.Childrens.Add(J, 167);

            E.Childrens.Add(J, 502);

            return new List<Node> { A, B, C, D, E, F, G, H, I, J };
        }

        private static List<Node> DijkstraGo(List<Node> nodes, string startNodeId, string endNodeId)
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
