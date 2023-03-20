using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : Kinematic
{
    public Seek myMoveType;

    public GameObject[] targets;
    [SerializeField]
    private int targetIndex = 0;

    public float waypointDetectRange;

    public Graph graph = new Graph();
    public Node startNode;
    public Node endNode;

    void Awake()
    {
        graph.Build();
        targets = getTargetList();

        myMoveType = new Seek();
        myMoveType.character = this;
        myMoveType.target = targets[targetIndex];
    }

    protected override void Update()
    {
        if (Vector3.Distance(this.transform.position, targets[targetIndex].transform.position) < waypointDetectRange)
        {
            if (targetIndex < targets.Length - 1)
            {
                targetIndex++;
            }
            else
            {
                targetIndex = 0;
            }
            myMoveType.target = targets[targetIndex];
        }

        steeringUpdate = new SteeringOutput();
        steeringUpdate.linear = myMoveType.getSteering().linear;

        base.Update();
    }

    private GameObject[] getTargetList()
    {
        GameObject[] targetList;
        List<Connection> connections = Dijkstra.pathfind(graph, startNode, endNode);
        targetList = new GameObject[connections.Count + 1];
        for (int i = 0; i < connections.Count; i++)
        {
            targetList[i] = connections[i].getFromNode().gameObject;
        }
        targetList[0] = startNode.gameObject;
        targetList[connections.Count] = endNode.gameObject;

        return targetList;
    }
}