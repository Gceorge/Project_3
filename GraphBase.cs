using System;
using System.Collections.Generic;

namespace Project_3
{
    public class GraphBase
    {
        protected Dictionary<string, int> nameToIndex;
        protected Dictionary<int, string> indexToName;
        protected int numberOfNodes;
        protected int numberOfEdges;

        public GraphBase()
        {
            nameToIndex = new Dictionary<string, int>();
            indexToName = new Dictionary<int, string>();
        }

        public int NodeCount => numberOfNodes;
        public int EdgeCount => numberOfEdges;
        public string GetNodeName(int index) => indexToName[index];

        protected void AddNodeMapping(string nodeName)
        {
            if (!nameToIndex.ContainsKey(nodeName))
            {
                int index = nameToIndex.Count;
                nameToIndex[nodeName] = index;
                indexToName[index] = nodeName;
            }
        }
    }

    public class UnweightedGraph : GraphBase
    {
        private int[,] adjacencyMatrix;

        public UnweightedGraph(int initialCapacity) : base()
        {
            numberOfNodes = initialCapacity;
            adjacencyMatrix = new int[initialCapacity, initialCapacity];
        }

        public void AddEdge(string source, string destination)
        {
            AddNodeMapping(source);
            AddNodeMapping(destination);

            int sourceIndex = nameToIndex[source];
            int destIndex = nameToIndex[destination];

            adjacencyMatrix[sourceIndex, destIndex] = 1;
            adjacencyMatrix[destIndex, sourceIndex] = 1;
            numberOfEdges++;
        }

        public double GetInfluenceBFS(int startNode)
        {
            long[] distances = new long[numberOfNodes];  // Changed from int to long
            bool[] visited = new bool[numberOfNodes];

            for (int i = 0; i < numberOfNodes; i++)
            {
                distances[i] = long.MaxValue;  // Changed from int.MaxValue
                visited[i] = false;
            }

            Queue<int> queue = new Queue<int>();
            queue.Enqueue(startNode);
            distances[startNode] = 0;
            visited[startNode] = true;

            while (queue.Count > 0)
            {
                int current = queue.Dequeue();
                for (int neighbor = 0; neighbor < numberOfNodes; neighbor++)
                {
                    if (!visited[neighbor] && adjacencyMatrix[current, neighbor] == 1)
                    {
                        queue.Enqueue(neighbor);
                        visited[neighbor] = true;
                        distances[neighbor] = distances[current] + 1;
                    }
                }
            }

            long sum = 0;  // Changed from double to long still doesnt work for road map
            int reachableNodes = 0;
            for (int i = 0; i < numberOfNodes; i++)
            {
                if (i != startNode && distances[i] != long.MaxValue)  // Changed from int.MaxValue to long
                {
                    sum += distances[i];
                    reachableNodes++;
                }
            }

            return (numberOfNodes - 1) / (double)sum;  // Cast to double for division
        }
    }

    public class WeightedGraph : GraphBase
    {
        private LinkedList<Tuple<int, int>>[] adjacencyList;

        public WeightedGraph(int initialCapacity) : base()
        {
            numberOfNodes = initialCapacity;
            adjacencyList = new LinkedList<Tuple<int, int>>[initialCapacity];
            for (int i = 0; i < initialCapacity; i++)
            {
                adjacencyList[i] = new LinkedList<Tuple<int, int>>();
            }
        }

        public void AddEdge(string source, string destination, int weight)
        {
            AddNodeMapping(source);
            AddNodeMapping(destination);

            int sourceIndex = nameToIndex[source];
            int destIndex = nameToIndex[destination];

            adjacencyList[sourceIndex].AddLast(new Tuple<int, int>(destIndex, weight));
            adjacencyList[destIndex].AddLast(new Tuple<int, int>(sourceIndex, weight));
            numberOfEdges++;
        }

        public double GetInfluenceWeighted(int startNode)
        { //https://www.geeksforgeeks.org/dijkstras-algorithm-for-adjacency-list-representation-greedy-algo-8/

            int[] distances = new int[numberOfNodes]; //stores shortest distance to each node1
            bool[] visited = new bool[numberOfNodes]; //has the node been fully explored, shortest path found or all connections explroed

            for (int i = 0; i < numberOfNodes; i++)
            {
                distances[i] = int.MaxValue; //maxvalue to represent infiinity. i.e no path found yet
                visited[i] = false;
            }
            distances[startNode] = 0;

            // Main loop,runs for each node in the graph
            for (int count = 0; count < numberOfNodes - 1; count++)
            {
                // Find the unvisited node with smallest known distance
                int minDistance = int.MaxValue;
                int minIndex = -1;

                for (int v = 0; v < numberOfNodes; v++) //checks everynode in graph
                {
                    if (!visited[v] && distances[v] < minDistance) //for each node, checks has it been fully explored 
                    {
                        minDistance = distances[v]; //and is the distnace to this node smaller than what its found so far. If true the update mindistance
                        minIndex = v;
                    }
                }

                // If can't find an unvisited node, end
                if (minIndex == -1) break;

                // Mark node as visited if shortpath found and move onto next in queue
                visited[minIndex] = true;

                // STEP 3: Update distances to all neighbors of current node
                foreach (var edge in adjacencyList[minIndex])
                {
                    int neighborNode = edge.Item1;    // The node this edge connects to
                    int edgeWeight = edge.Item2;      // The length/weight of this connection

                    // Only update distance if:
                    // Neighbor is not visited
                    // Current node's distance isn't infinity
                    // New path is shorter than known path , must be all three
                    if (!visited[neighborNode] &&
                        distances[minIndex] != int.MaxValue &&
                        distances[minIndex] + edgeWeight < distances[neighborNode])
                    {
                        distances[neighborNode] = distances[minIndex] + edgeWeight;
                    }
                }
            }

            // Calculate influence score:
            // Sum up all reachable distances
            // Count how many nodes can be reached
            double sum = 0;
            int reachableNodes = 0;
            for (int i = 0; i < numberOfNodes; i++)
            {
                if (i != startNode && distances[i] != int.MaxValue)
                {
                    sum += distances[i];
                    reachableNodes++;
                }
            }

            // Return influence score:
            if (reachableNodes > 0)
            {
                return reachableNodes / sum;
            }
            else
            {
                return 0;
            }
        }

    }
}