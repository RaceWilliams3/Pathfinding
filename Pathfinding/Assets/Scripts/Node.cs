using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Node[] ConnectsTo;

    public bool autoConnect;
    public int autoConnectNum;

    public bool redNode;
    public float redChance;

    public Material red;
    public Material blue;

    private void OnDrawGizmos()
    {
        foreach (Node n in ConnectsTo)
        {
            Gizmos.color = Color.red;
            //Gizmos.DrawLine(transform.position, n.transform.position);
            Gizmos.DrawRay(transform.position, (n.transform.position - transform.position).normalized * 2);
        }
    }

    public void FindConnections()
    {
        if (autoConnect)
        {
            ConnectsTo = new Node[autoConnectNum];

            var allNodesArray = FindObjectsOfType<Node>();
            List<Node> allNodes = allNodesArray.ToList();

            float closestDist = 10000;
            Node closestNode = null;

            //loop through every index of connections array
            for (int i = 0; i < autoConnectNum; i++)
            {
                //loop through every node in the scene to find the closest
                foreach (Node n in allNodes)
                {
                    if (n != this)
                    {
                        if (Vector3.Distance(n.transform.position, this.transform.position) < closestDist)
                        {
                            closestDist = Vector3.Distance(n.transform.position, this.transform.position);
                            closestNode = n;
                        }
                    }
                }
                ConnectsTo[i] = closestNode;
                allNodes.Remove(closestNode);
                closestDist = 10000;
                closestNode = null;
            }
        }
    }

    private void Awake()
    {
        if (Random.Range(0f, 100f) < redChance)
        {
            redNode = true;
            GetComponent<Renderer>().material = red;
        }
        else
        {
            redNode = false;
            GetComponent<Renderer>().material = blue;
        }
    }
}