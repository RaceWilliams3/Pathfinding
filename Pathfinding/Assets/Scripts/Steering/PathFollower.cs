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

    public Graph graph;
    public Node startNode;
    public Node endNode;

    void Start()
    {
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

    /*private GameObject[] getTargetList()
    {
        private List<Connection> connections = Dijkstra.pathfind(graph, startNode, endNode);
        private List<Node> nodeList = new List<Node>();
        
        //foreach(Connection c in connections)
    }*/
}