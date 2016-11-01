using System;
using System.Collections.Generic;
using DijkstraNetwork;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class ProgramTest
    {
        [TestMethod]
        public void ProgramTest_GetTotalWeight()
        {
            // Arrange
            var node1 = new Node(1);
            var node2 = new Node(2);
            var node3 = new Node(3);

            node1.Childrens.Add(node2, 1);
            node1.Childrens.Add(node3, 3);

            node2.Childrens.Add(node3, 1);

            var nodesAncestors = new List<Tuple<Node, int, Node>>();

            // Act
            Program.GetTotalWeight();

            // Assert
        }
    }
}
