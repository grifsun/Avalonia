﻿using System;
using Avalonia.Rendering.SceneGraph;
using Avalonia.VisualTree;
using Moq;
using Xunit;

namespace Avalonia.Visuals.UnitTests.Rendering.SceneGraph
{
    public class VisualNodeTests
    {
        [Fact]
        public void Empty_Children_Collections_Should_Be_Shared()
        {
            var node1 = new VisualNode(Mock.Of<IVisual>(), null);
            var node2 = new VisualNode(Mock.Of<IVisual>(), null);

            Assert.Same(node1.Children, node2.Children);
        }

        [Fact]
        public void Adding_Child_Should_Create_Collection()
        {
            var node = new VisualNode(Mock.Of<IVisual>(), null);
            var collection = node.Children;

            node.AddChild(Mock.Of<IVisualNode>());

            Assert.NotSame(collection, node.Children);
        }

        [Fact]
        public void Empty_DrawOperations_Collections_Should_Be_Shared()
        {
            var node1 = new VisualNode(Mock.Of<IVisual>(), null);
            var node2 = new VisualNode(Mock.Of<IVisual>(), null);

            Assert.Same(node1.DrawOperations, node2.DrawOperations);
        }

        [Fact]
        public void Adding_DrawOperation_Should_Create_Collection()
        {
            var node = new VisualNode(Mock.Of<IVisual>(), null);
            var collection = node.DrawOperations;

            node.AddDrawOperation(Mock.Of<IDrawOperation>());

            Assert.NotSame(collection, node.DrawOperations);
        }

        [Fact]
        public void Cloned_Nodes_Should_Share_DrawOperations_Collection()
        {
            var node1 = new VisualNode(Mock.Of<IVisual>(), null);
            node1.AddDrawOperation(Mock.Of<IDrawOperation>());

            var node2 = node1.Clone(null);

            Assert.Same(node1.DrawOperations, node2.DrawOperations);
        }

        [Fact]
        public void Adding_DrawOperation_To_Cloned_Node_Should_Create_New_Collection()
        {
            var node1 = new VisualNode(Mock.Of<IVisual>(), null);
            var operation1 = Mock.Of<IDrawOperation>();
            node1.AddDrawOperation(operation1);

            var node2 = node1.Clone(null);
            var operation2 = Mock.Of<IDrawOperation>();
            node2.ReplaceDrawOperation(0, operation2);

            Assert.NotSame(node1.DrawOperations, node2.DrawOperations);
            Assert.Equal(1, node1.DrawOperations.Count);
            Assert.Equal(1, node2.DrawOperations.Count);
            Assert.Same(operation1, node1.DrawOperations[0]);
            Assert.Same(operation2, node2.DrawOperations[0]);
        }
    }
}
