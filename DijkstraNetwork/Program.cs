using System;
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
            var nodesAncestors = new List<Tuple<Node, int, Node>>();
            var nodesTotalWeight = new Dictionary<Node, int>();

            Node startNode = nodes.Where(n => n.Id == startNodeId).Single();
            Node endNode = nodes.Where(n => n.Id == endNodeId).Single();

            while (nodesTotalWeight.OrderBy(w => w.Value).First().Key != endNode)
            {
                KeyValuePair<Node, int> linkedNode = startNode.Links.OrderBy(l => l.Value).First();

                if (!nodesTotalWeight.ContainsKey(linkedNode.Key))
                {
                    nodesAncestors.Add(new Tuple<Node, int, Node>(startNode, linkedNode.Value, linkedNode.Key));

                    int totalWeight = GetTotalWeight(linkedNode.Key, nodesAncestors);
                    nodesTotalWeight.Add(linkedNode.Key, totalWeight);
                }
            }


            startNode.Links.Min(l => l.Value);
        }

        private static Node FindLowestNoneReadNode(Node startNode, Node actualNode, List<Tuple<Node, int, Node>> nodesAncestors, Dictionary<Node, int> nodesTotalWeight)
        {
            int actualWeight = GetTotalWeight(startNode, actualNode, nodesAncestors);

            foreach (var nextNode in actualNode.Links.OrderBy(l => l.Value))
            {
                if (!nodesTotalWeight.Contains(nextNode) 
                    && GetTotalWeight(startNode, nextNode.Key, nodesAncestors) <  actualWeight)
                {
                    return nextNode.Key;
                }
            }

            return actualNode;
        }

        private static int GetTotalWeight(Node startNode, Node nodeToReach, List<Tuple<Node, int, Node>> nodesAncestors)
        {
            int totalWeight = 0;

            return FindWeight(startNode, nodeToReach, nodesAncestors, totalWeight);
        }

        /// <summary>
        /// Compute recursively the weight between two nodes.
        /// </summary>
        /// <param name="parentNode">The parent node</param>
        /// <param name="nodesAncestors">List of tuples as Node, Weight, Parent node</param>
        /// <returns></returns>
        private static int FindWeight(Node startNode, Node parentNode, List<Tuple<Node, int, Node>> nodesAncestors, int actualWeight)
        {
            var link = nodesAncestors.FirstOrDefault(n => n.Item3 == parentNode);
            if (link != null)
            {
                actualWeight += link.Item2;

                parentNode = link.Item1;

                if (parentNode != startNode)
                {
                    FindWeight(startNode, parentNode, nodesAncestors, actualWeight);
                }
                else
                {
                    return actualWeight;
                }
            }

            // The finding node doesn't exist
            return -1;
        }
    }
}
