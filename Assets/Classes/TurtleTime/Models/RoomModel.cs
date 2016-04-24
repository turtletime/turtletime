using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityMVC;
using UnityEngine;

namespace TurtleTime
{
    /// <summary>
    /// Represents the physical layout of the cafe room.
    /// Exposes several methods for pathfinding and object placement.
    /// </summary>
    class RoomModel : Model
    {
        private static Vector2[] X_AND_Y = new Vector2[] { new Vector2(1, 0), new Vector2(0, 1) };

        private class RoomGraphNode
        {
            public List<RoomGraphNode> AdjacencyList;
            public Vector2 Position;
            public WorldObjectModel ModelAtNode;

            public override string ToString()
            {
                return Position.ToString() + " " + ModelAtNode;
            }
        }

        private Dictionary<Vector2, RoomGraphNode> emptySpaceGraph;
        private bool graphValid = false;

        public int Width { get; private set; }
        public int Height { get; private set; }

        public List<WorldObjectModel> Models { get; private set; }

        public RoomModel()
        {
            Models = new List<WorldObjectModel>();
        }

        public override void LoadFromJson(IJsonObject jsonNode)
        {
            Width = jsonNode["dimensions"][0].AsInt;
            Height = jsonNode["dimensions"][1].AsInt;
            base.LoadFromJson(jsonNode);
        }

        public WorldObjectModel GetModelAtLocation(Vector2 location)
        {
            // TODO: Highly inefficient
            int x = (int)Mathf.Round(location.x);
            int y = (int)Mathf.Round(location.y);
            foreach (WorldObjectModel model in Models)
            {
                int modelX = (int)Math.Round(model.Position.x);
                int modelY = (int)Math.Round(model.Position.y);
                bool result = true;
                result = result && x >= modelX - model.Width / 2;
                result = result && x < modelX + model.Width / 2;
                result = result && y >= modelY - model.Height / 2;
                result = result && y < modelY + model.Height / 2;
                if (result)
                {
                    return model;
                }
            }
            return null;
        }

        /// <summary>
        /// Manually forces the room model to re-generate its pathfinding graph on any pathfinding operations.
        /// </summary>
        public void InvalidatePathfindingGraph()
        {
            graphValid = false;
        }

        /// <summary>
        /// Builds the pathfinding graph.
        /// </summary>
        public void RebuildPathfindingGraph()
        {
            if (emptySpaceGraph == null)
            {
                // Initial graph population
                emptySpaceGraph = new Dictionary<Vector2, RoomGraphNode>();
                for (int i = 0; i < Width; i++)
                {
                    for (int j = 0; j < Height; j++)
                    {
                        emptySpaceGraph.Add(new Vector2(i, j),
                            new RoomGraphNode() { AdjacencyList = new List<RoomGraphNode>(), Position = new Vector2(i, j) });
                    }
                }
            }
            foreach (var node in emptySpaceGraph)
            {
                node.Value.ModelAtNode = GetModelAtLocation(node.Key);
                node.Value.AdjacencyList.Clear();
            }
            foreach (var node in emptySpaceGraph)
            {
                foreach (var direction in X_AND_Y)
                {
                    Vector2 neighborPosition = node.Key + direction;
                    if (emptySpaceGraph.ContainsKey(neighborPosition))
                    {
                        // they're neighbors
                        emptySpaceGraph[node.Key].AdjacencyList.Add(emptySpaceGraph[neighborPosition]);
                        emptySpaceGraph[neighborPosition].AdjacencyList.Add(emptySpaceGraph[node.Key]);
                    }
                }
            }
        }

        /// <summary>
        /// Gets an adjacent space for a model to move to in order to reach a given target.
        /// </summary>
        /// <param name="model">The model for which the next space should be found.</param>
        /// <param name="target">The model's intended destination.</param>
        /// <param name="ignoreLayers">Layers that the object ignores when pathfinding.</param>
        /// <returns>An adjacent space if the model has a path to its target, or the model's
        /// original space if there is no such path.</returns>
        public Vector2 Pathfind(WorldObjectModel model, WorldObjectModel destination)
        {
            if (!graphValid)
            {
                RebuildPathfindingGraph();
            }
            Vector2 current = model.Position.RoundComponents();
            Vector2 target = destination.Position.RoundComponents();
            if (current == target)
            {
                // what are we doing
                return target;
            }
            // temp var
            List<RoomGraphNode> nodesToAdd = new List<RoomGraphNode>(4);

            Dictionary<RoomGraphNode, RoomGraphNode> prevPointers = new Dictionary<RoomGraphNode, RoomGraphNode>();
            Queue<RoomGraphNode> queue = new Queue<RoomGraphNode>();
            // initial population
            prevPointers[emptySpaceGraph[current]] = null;
            queue.Enqueue(emptySpaceGraph[current]);
            while (queue.Count > 0)
            {
                RoomGraphNode currentNode = queue.Dequeue();
                // Check if we are at the target
                if (currentNode.Position == target)
                {
                    // we found it
                    while (prevPointers[currentNode].Position != current)
                    {
                        currentNode = prevPointers[currentNode];
                    }
                    return currentNode.Position;
                }
                // TODO: Pathfinding currently ignores size completely. (Is this OK?)
                // Get current node's neighbors
                nodesToAdd.Clear();
                foreach (var adjacentNode in currentNode.AdjacencyList.GetElementsInRandomOrder())
                {
                    if (!prevPointers.ContainsKey(adjacentNode) &&
                        (adjacentNode.ModelAtNode == null ||
                        currentNode.ModelAtNode == adjacentNode.ModelAtNode ||
                        adjacentNode.ModelAtNode == destination))
                    {
                        nodesToAdd.Add(adjacentNode);
                    }
                }
                // If current node is original, we want to favor the direction that the object's currently facing
                if (currentNode.Position == current)
                {
                    for (int i = 0; i < nodesToAdd.Count; i++)
                    {
                        if (nodesToAdd[i].Position - current == model.Direction)
                        {
                            nodesToAdd.Swap(0, i);
                            break;
                        }
                    }
                }
                // Add valid neighbors to queue
                foreach (var nextNode in nodesToAdd)
                {
                    prevPointers[nextNode] = currentNode;
                    queue.Enqueue(nextNode);
                }
            }
            // couldn't find it
            Debug.Log("Couldn't find a path from " + current + " to " + target);
            return current;
        }

        public bool CanBePlacedAt(WorldObjectModel model, Vector2 position)
        {
            int modelX = (int)Math.Round(model.Position.x);
            int modelY = (int)Math.Round(model.Position.y);
            for (int i = modelX - model.Width / 2; i < modelX + model.Width / 2; i++)
            {
                for (int j = modelY - model.Height / 2; j < modelY + model.Height / 2; i++)
                {
                    if (GetModelAtLocation(new Vector2(i, j)) != null)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
