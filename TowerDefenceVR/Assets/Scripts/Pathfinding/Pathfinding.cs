using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Pathfinding Script that implements the A* algorithm
 * 
 * This script is from the tutorial "A* Pathfinding Tutorial (Unity)" by Sebastian Lague
 * link: https://www.youtube.com/playlist?list=PLFt_AvWsXl0cq5Umv3pMC9SPnKjfp9eGW
 * 
 *  * parts taken from the tutorial are marked with 
 * // ** SEBASTIAN LAGUE ** //
 *       his code here...
 *       any modifications within his code are marked with
 *       // ** AARON MOONEY ** //
 *       my code here
 *       // ** AARON MOONEY END ** //
 * // ** SEBASTIAN LAGUE END ** //
 * */

// ** SEBASTIAN LAGUE ** //
public class Pathfinding : MonoBehaviour
{
    WorldGrid grid;
    PathRequestController requestController;

    private void Awake()
    {
        grid = GetComponent<WorldGrid>();
        requestController = GetComponent<PathRequestController>();
    }

    // Start to find a path
    public void StartFindPath(Vector3 startPos, Vector3 targetPos)
    {
        StartCoroutine(FindPath(startPos, targetPos));
    }

    // Find path coroutine
    IEnumerator FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Vector3[] waypoints = new Vector3[0];
        bool pathSuccess = false;

        // initialise start and target note
        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);

        // make sure both nodes are walkable
        if (startNode.walkable && targetNode.walkable)
        {
            Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
            HashSet<Node> closedSet = new HashSet<Node>();
            // add start node to open set
            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                // Remove from open set and add to closed set
                Node node = openSet.RemoveFirst();
                closedSet.Add(node);

                // break with success if the node is the target
                if (node == targetNode)
                {
                    pathSuccess = true;
                    break;
                }

                // loop through node's neighbours
                foreach (Node neighbour in grid.GetNeighbours(node))
                {
                    // skip if this has is a neighbour of the closed set or is not walkable
                    if (!neighbour.walkable || closedSet.Contains(neighbour))
                    {
                        continue;
                    }

                    // calculate cost
                    int newCostToNeighbour = node.gCost + GetDistance(node, neighbour);
                    // check that cost is less than the neighbours gCost or that the open set does not contain the neighbour
                    if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                    {
                        // set neighbour costs
                        neighbour.gCost = newCostToNeighbour;
                        neighbour.hCost = GetDistance(neighbour, targetNode);
                        neighbour.parent = node;

                        if (!openSet.Contains(neighbour))
                        {
                            // add neighbour to open set
                            openSet.Add(neighbour);
                        }
                        else
                        {
                            // update the heap
                            openSet.UpdateItem(neighbour);
                        }
                    }
                }
            }
        }
        yield return null;
        // retrace path on success
        if (pathSuccess)
        {
            waypoints = RetracePath(startNode, targetNode);
        }
        // Finish processing the path
        requestController.FinishedProcessingPath(waypoints, pathSuccess);
    }

    // Retrace path method that traces back from the end node to the start node
    Vector3[] RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        Vector3[] waypoints = SimplifyPath(path);
        Array.Reverse(waypoints);

        return waypoints;
    }

    // Simplify Path method that places waypoints where the path changes direction to avoid a huge number of waypoints
    Vector3[] SimplifyPath(List<Node> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;

        for (int i = 1; i < path.Count; i++)
        {
            Vector2 directionNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
            if (directionNew != directionOld)
            {
                waypoints.Add(path[i].worldPos);
            }
            directionOld = directionNew;
        }
        return waypoints.ToArray();
    }

    // Get the distance between nodes
    int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }
}
// ** SEBASTIAN LAGUE END ** //