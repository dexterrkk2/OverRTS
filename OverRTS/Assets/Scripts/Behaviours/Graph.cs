using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Edge
{
    public Node from;
    public Node to;
    public float cost;
}
public class Node
{
    public List<Edge> edges = new List<Edge>();
    public Transform self;
}
public class Graph
{
    List<Node> nodes;
    public class UnvistedNodeData
    {
        public Node node;
        public float minCostToTree = Mathf.Infinity;
        public Edge minCostEdge = null;
    }
    class UnvisitedNodeList
    {
        public List<UnvistedNodeData> nodeList;
        public void Add(Node node)
        {
            nodeList.Add(getData(node));
        }
        public void Remove(Node node)
        {
            nodeList.Remove(getData(node));
        }
        public UnvistedNodeData getLowestMinCost()
        {
            int i = 0;
            int lowestCostPos = 0;
            float cost =Mathf.Infinity;
            foreach(UnvistedNodeData data in nodeList)
            {
                if(cost > data.minCostToTree)
                {
                    cost = data.minCostToTree;
                    lowestCostPos = i;
                }
                i++;
            }
            return nodeList[lowestCostPos];
        }
        public UnvistedNodeData getData(Node node)
        {
            UnvistedNodeData data = new UnvistedNodeData();
            int i = 0;
            int lowestCostPos = 0;
            float cost = Mathf.Infinity;
            foreach (Edge edge in node.edges)
            {
                if (cost > edge.cost)
                {
                    cost = edge.cost;
                    lowestCostPos = i;
                }
                i++;
            }
            data.node = node;
            data.minCostEdge = node.edges[lowestCostPos];
            data.minCostToTree = cost;
            return data;
        }
        public void setData(Node node, float cost, Edge edge)
        {
            edge.to = node;
            edge.cost = cost;
        }
    }
    public List<Edge> primeMST(Graph graph, Node start)
    {
        UnvisitedNodeList unvisted = new UnvisitedNodeList();
        List<Edge> tree = new List<Edge>();
        void addNodeToTree(Node node)
        {
            unvisted.Remove(node);
            foreach(Edge edge in node.edges)
            {
                UnvistedNodeData data = unvisted.getData(edge.to);
                unvisted.setData(edge.to, edge.cost, edge);
            }
        }
        foreach(Node node in graph.nodes)
        {
            unvisted.Add(node);
        }
        addNodeToTree(start);
        while (unvisted.nodeList.Count>0)
        {
            UnvistedNodeData next = unvisted.getLowestMinCost();
            if(next != null)
            {
                tree.Add(next.minCostEdge);
                addNodeToTree(next.minCostEdge.to);
            }
            else
            {
                break;
            }
        }
        return tree;
    }
}
