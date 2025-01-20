using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Project_3
{
    public class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"C:\Users\Laptop\OneDrive - Sheffield Hallam University\University\Year 2\Algorithms and Data Structures\Assignment 07.01.2025\Project_3\Project_3\";

            // Process unweighted graph
            Console.WriteLine("Unweighted Graph:");
            UnweightedGraph unweightedGraph = GraphReader.ReadUnweightedGraph(filePath + "edge_list1.txt");

            Console.WriteLine($"GetInfluence BFS: Number of nodes is {unweightedGraph.NodeCount}");
            Console.WriteLine($"Number of Edges: {unweightedGraph.EdgeCount}.");

            Stopwatch stopwatch = Stopwatch.StartNew();
            double[] scores = new double[unweightedGraph.NodeCount];
            for (int i = 0; i < unweightedGraph.NodeCount; i++)
            {
                scores[i] = unweightedGraph.GetInfluenceBFS(i);
                Console.WriteLine($"Node {unweightedGraph.GetNodeName(i)} score: {scores[i]:F3}");
            }
            stopwatch.Stop();
            Console.WriteLine($"BFS Done. Execution time: {stopwatch.ElapsedMilliseconds} ms");

            int maxIndex = 0, minIndex = 0;
            for (int i = 1; i < unweightedGraph.NodeCount; i++)
            {
                if (scores[i] > scores[maxIndex]) maxIndex = i;
                if (scores[i] < scores[minIndex]) minIndex = i;
            }

            Console.WriteLine($"Highest influence score is {unweightedGraph.GetNodeName(maxIndex)} with a score of {scores[maxIndex]:F3}.");
            Console.WriteLine($"Lowest influence score is {unweightedGraph.GetNodeName(minIndex)} with a score of {scores[minIndex]:F3}.");

            // Process weighted graph
            Console.WriteLine("\nWeighted Graph:");
            WeightedGraph weightedGraph = GraphReader.ReadWeightedGraph(filePath + "edge_list2.txt");

            Console.WriteLine($"GetInfluenceWeighted: Number of Nodes: {weightedGraph.NodeCount}.");
            Console.WriteLine($"Number of Edges: {weightedGraph.EdgeCount}.");

            stopwatch = Stopwatch.StartNew();
            scores = new double[weightedGraph.NodeCount];
            for (int i = 0; i < weightedGraph.NodeCount; i++)
            {
                scores[i] = weightedGraph.GetInfluenceWeighted(i);
                Console.WriteLine($"Node {weightedGraph.GetNodeName(i)} score: {scores[i]:F3}");
            }
            stopwatch.Stop();
            Console.WriteLine($"Dijkstra's Done. Execution time: {stopwatch.ElapsedMilliseconds} ms");

            maxIndex = 0; minIndex = 0;
            for (int i = 1; i < weightedGraph.NodeCount; i++)
            {
                if (scores[i] > scores[maxIndex]) maxIndex = i;
                if (scores[i] < scores[minIndex]) minIndex = i;
            }

            Console.WriteLine($"Highest influence score is {weightedGraph.GetNodeName(maxIndex)} with a score of {scores[maxIndex]:F3}.");
            Console.WriteLine($"Lowest influence score is {weightedGraph.GetNodeName(minIndex)} with a score of {scores[minIndex]:F3}.");

            // Process road network with both approaches
            Console.WriteLine("\nRoad Network Analysis:");

            // Matrix version (Original)
            Console.WriteLine("\nMatrix Version (BFS):");
            UnweightedGraph roadNetworkMatrix = GraphReader.ReadRoadNetwork(filePath + "road-euroroad.edges");

            Console.WriteLine($"Number of nodes: {roadNetworkMatrix.NodeCount}");
            Console.WriteLine($"Number of edges: {roadNetworkMatrix.EdgeCount}");

            scores = new double[roadNetworkMatrix.NodeCount];

            stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < roadNetworkMatrix.NodeCount; i++)
            {
                scores[i] = roadNetworkMatrix.GetInfluenceBFS(i);
            }
            stopwatch.Stop();

            Console.WriteLine($"BFS Done. Execution time: {stopwatch.ElapsedMilliseconds} ms");

            maxIndex = 0; minIndex = 0;
            for (int i = 1; i < roadNetworkMatrix.NodeCount; i++)
            {
                if (scores[i] > scores[maxIndex]) maxIndex = i;
                if (scores[i] < scores[minIndex]) minIndex = i;
            }

            Console.WriteLine($"Matrix Version - Highest influence score is Node {roadNetworkMatrix.GetNodeName(maxIndex)} with a score of {scores[maxIndex]:F3}.");
            Console.WriteLine($"Matrix Version - Lowest influence score is Node {roadNetworkMatrix.GetNodeName(minIndex)} with a score of {scores[minIndex]:F3}.");

            // List version (New)
            Console.WriteLine("\nList Version (Dijkstra's):");
            WeightedGraph roadNetworkList = GraphReader.ReadWeightedRoadNetwork(filePath + "road-euroroad.edges");

            Console.WriteLine($"Number of nodes: {roadNetworkList.NodeCount}");
            Console.WriteLine($"Number of edges: {roadNetworkList.EdgeCount}");

            scores = new double[roadNetworkList.NodeCount];

            stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < roadNetworkList.NodeCount; i++)
            {
                scores[i] = roadNetworkList.GetInfluenceWeighted(i);
            }
            stopwatch.Stop();

            Console.WriteLine($"Dijkstra's Done. Execution time: {stopwatch.ElapsedMilliseconds} ms");

            maxIndex = 0; minIndex = 0;
            for (int i = 1; i < roadNetworkList.NodeCount; i++)
            {
                if (scores[i] > scores[maxIndex]) maxIndex = i;
                if (scores[i] < scores[minIndex]) minIndex = i;
            }

            Console.WriteLine($"List Version - Highest influence score is Node {roadNetworkList.GetNodeName(maxIndex)} with a score of {scores[maxIndex]:F3}.");
            Console.WriteLine($"List Version - Lowest influence score is Node {roadNetworkList.GetNodeName(minIndex)} with a score of {scores[minIndex]:F3}.");

            Console.ReadKey();
        }
    }
}