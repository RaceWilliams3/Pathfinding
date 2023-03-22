using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph
{
    List<Connection> mConnections;

    public List<Connection> getConnections(Node fromNode)
    {
        List<Connection> connections = new List<Connection>();
        foreach (Connection c in mConnections)
        {
            if (c.getFromNode() == fromNode)
            {
                connections.Add(c);
            }
        }
        return connections;
    }

    public void Build()
    {
        mConnections = new List<Connection>();

        Node[] nodes = GameObject.FindObjectsOfType<Node>();
        foreach (Node fromNode in nodes)
        {
            fromNode.FindConnections();

            foreach (Node toNode in fromNode.ConnectsTo)
            {
                //Line to modify rule for cost, right now cost is just distance.
                float cost = (toNode.transform.position - fromNode.transform.position).magnitude;
                Connection c = new Connection(cost, fromNode, toNode);
                mConnections.Add(c);
            }
        }
    }
    
}
