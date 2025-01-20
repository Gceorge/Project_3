using System;
using System.IO;
using System.Collections.Generic;

namespace Project_3
{
    public static class GraphReader
    {
        public static WeightedGraph ReadWeightedRoadNetwork(string filename)
        {
            string[] lines = File.ReadAllLines(filename);
            HashSet<string> uniqueNodes = new HashSet<string>();

            // Find actual number of nodes, skipping header lines
            foreach (string line in lines)
            {
                if (line.StartsWith("%")) continue;

                string[] parts = line.Split(' ');
                if (parts.Length >= 2)
                {
                    uniqueNodes.Add(parts[0]);
                    uniqueNodes.Add(parts[1]);
                }
            }

            var graph = new WeightedGraph(uniqueNodes.Count);

            // Add edges with weight 1
            foreach (string line in lines)
            {
                if (line.StartsWith("%")) continue;

                string[] parts = line.Split(' ');
                if (parts.Length >= 2)
                {
                    graph.AddEdge(parts[0], parts[1], 1); // Using weight of 1 for all edges
                }
            }

            return graph;
        }
        public static UnweightedGraph ReadUnweightedGraph(string filename)
        {
            string[] lines = File.ReadAllLines(filename);
            HashSet<string> uniqueNodes = new HashSet<string>();

            // Find actual number of nodes
            foreach (string line in lines)
            {
                string[] parts = line.Split(' ');
                if (parts.Length >= 2)
                {
                    uniqueNodes.Add(parts[0]);
                    uniqueNodes.Add(parts[1]);
                }
            }

            var graph = new UnweightedGraph(uniqueNodes.Count);  // Use actual count

            foreach (string line in lines)
            {
                string[] parts = line.Split(' ');
                if (parts.Length >= 2)
                {
                    graph.AddEdge(parts[0], parts[1]);
                }
            }

            return graph;
        }

        public static WeightedGraph ReadWeightedGraph(string filename)
        {
            string[] lines = File.ReadAllLines(filename);
            HashSet<string> uniqueNodes = new HashSet<string>();

            // Find actual number of nodes first
            foreach (string line in lines)
            {
                string[] parts = line.Split(' ');
                if (parts.Length >= 2)
                {
                    uniqueNodes.Add(parts[0]);
                    uniqueNodes.Add(parts[1]);
                }
            }

            var graph = new WeightedGraph(uniqueNodes.Count);

            foreach (string line in lines)
            {
                string[] parts = line.Split(' ');
                if (parts.Length >= 3 && int.TryParse(parts[2], out int weight))
                {
                    graph.AddEdge(parts[0], parts[1], weight);
                }
            }

            return graph;
        }

        public static UnweightedGraph ReadRoadNetwork(string filename)
        {
            string[] lines = File.ReadAllLines(filename);
            HashSet<string> uniqueNodes = new HashSet<string>();

            // Find actual number of nodes, skipping header lines
            foreach (string line in lines)
            {
                if (line.StartsWith("%")) continue;

                string[] parts = line.Split(' ');
                if (parts.Length >= 2)
                {
                    uniqueNodes.Add(parts[0]);
                    uniqueNodes.Add(parts[1]);
                }
            }

            var graph = new UnweightedGraph(uniqueNodes.Count);

            foreach (string line in lines)
            {
                if (line.StartsWith("%")) continue;

                string[] parts = line.Split(' ');
                if (parts.Length >= 2)
                {
                    graph.AddEdge(parts[0], parts[1]);
                }
            }

            return graph;
        }
    }
}