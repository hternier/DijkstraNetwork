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

            List<Node> nodes = InitSecondExemple();
            List<Node> bestPath = DijkstraGo(nodes, nodes.First().Id, nodes.Last().Id);

            Console.WriteLine("Compute ended. Result:");

            int index = 0;
            foreach (Node node in bestPath)
            {
                Console.WriteLine("Node {0}: {1} ({2})", index++, node.Id, node.PathWeight);
            }

            Console.WriteLine("Total path weight: {0}", bestPath.Last().PathWeight);

            Console.ReadKey();
        }

        private static List<Node> InitFirstExemple()
        {
            var A = new Node("A");
            var B = new Node("B");
            var C = new Node("C");
            var D = new Node("D");
            var E = new Node("E");

            A.Childrens.Add(B, 5);
            A.Childrens.Add(C, 8);

            B.Childrens.Add(D, 10);
            D.Childrens.Add(E, 1);

            C.Childrens.Add(E, 2);

            return new List<Node> { A, B, C, D, E };
        }

        private static List<Node> InitSecondExemple()
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

        /// <summary>
        /// Lauch the main Dijkstra method
        /// </summary>
        internal static List<Node> DijkstraGo(List<Node> nodes, string startNodeId, string endNodeId)
        {
            var nodesAncestors = new Dictionary<Node, Node>();

            Node startNode = nodes.Where(n => n.Id == startNodeId).Single();
            Node endNode = nodes.Where(n => n.Id == endNodeId).Single();
            Node actualNode = null;

            startNode.PathWeight = 0;

            while (actualNode != endNode)
            {
                // Select the lightweight not visited node
                actualNode = nodes.Where(n => !n.IsVisited && n.PathWeight != null).OrderBy(n => n.PathWeight).First();
                ComputeChildNodes(actualNode, nodesAncestors);
            }

            return GetPath(startNode, endNode, nodesAncestors);
        }

        /// <summary>
        /// Compute the weigh path for each actual child nodes.
        /// </summary>
        internal static void ComputeChildNodes(Node actualNode, Dictionary<Node, Node> nodesAncestors)
        {
            actualNode.IsVisited = true;

            foreach (var T in actualNode.Childrens)
            {
                Node childNode = T.Key;
                int nodeWeight = T.Value;

                // Here the magic begin
                if (!childNode.IsVisited && (childNode.PathWeight == null || childNode.PathWeight > actualNode.PathWeight + nodeWeight))
                {
                    childNode.PathWeight = actualNode.PathWeight + nodeWeight;
                    nodesAncestors[childNode] = actualNode;
                }
            }
        }

        /// <summary>
        /// Get the result path
        /// </summary>
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
