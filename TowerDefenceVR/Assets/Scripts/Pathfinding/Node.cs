using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Node Script that contains information relating to the Node object
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
public class Node : IHeapItem<Node>
{

    public bool walkable;
    public Vector3 worldPos;
    public int gridX;
    public int gridY;

    public int gCost;
    public int hCost;
    public Node parent;
    int heapIndex;

    // Node Constructor
    public Node(bool m_walkable, Vector3 m_worldPos, int m_gridX, int m_gridY)
    {
        walkable = m_walkable;
        worldPos = m_worldPos;
        gridX = m_gridX;
        gridY = m_gridY;
    }

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }

    public int HeapIndex
    {
        get
        {
            return heapIndex;
        }

        set
        {
            heapIndex = value;
        }
    }

    // CompareTo Method
    public int CompareTo(Node nodeToCompare)
    {
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        if (compare == 0)
        {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }
        return -compare;
    }
}
// ** SEBASTIAN LAGUE END ** //
