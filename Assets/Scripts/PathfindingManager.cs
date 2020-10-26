using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class PathfindingManager
{
    private const int MOVESTRAIGHTCOST = 10;
    private const int MOVEDIAGONALCOST = 14; 
    private GridManager<PathNode> grid;

    private List<PathNode> openList;
    private List<PathNode> closedList;

    public PathfindingManager(int width, int height)
    {
        grid = new GridManager<PathNode>(width, height, 3,
            (GridManager<PathNode> gridNode, int x, int y) => new PathNode(gridNode, x, y));
    }

    public GridManager<PathNode> GetGrid()
    {
             
        return grid; 
    }

    public List<PathNode> FindPath(int startX, int startY, int endX, int endY)
    {
        PathNode startNode = grid.GetGridObject(startX, startY);
        PathNode endNode = grid.GetGridObject(endX, endY);

        openList = new List<PathNode> { startNode };
        closedList = new List<PathNode>();

        for (var x = 0; x < grid.GetWidth(); x++)
        {
            for (var y = 0; y < grid.GetHeight(); y++)
            {
                PathNode pathNode = grid.GetGridObject(x, y);
                pathNode.gCost = int.MaxValue;
                pathNode.CalculateFCost();
                pathNode.priorNode = null; 
            }
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();

        while (openList.Count > 0)
        {
            PathNode currentNode = GetLowestFCostNode(openList);
            
            if (currentNode == endNode)
            {
                return CalculatePath(endNode); 
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            for(int i = 0; i < GetNeighboursList(currentNode).Count; i++)
            {
                //Debug.Log(GetNeighboursList(currentNode)[i]);
            }

            foreach (PathNode neighbourNode in GetNeighboursList(currentNode))
            {
                if (closedList.Contains(neighbourNode))
                {
                    continue;
                }

                if (!neighbourNode.isWalkable)
                {
                    closedList.Add(neighbourNode);
                    continue; 
                }

                int estimateGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode); 

                if(estimateGCost < neighbourNode.gCost)
                {
                    neighbourNode.priorNode = currentNode;
                    neighbourNode.gCost = estimateGCost;
                    neighbourNode.hCost = CalculateDistanceCost(neighbourNode, endNode);
                    neighbourNode.CalculateFCost();

                    if (!openList.Contains(neighbourNode))
                    {
                        openList.Add(neighbourNode); 
                    }
                }
            }
        }

        return null; 
    }

    private List<PathNode> GetNeighboursList(PathNode currentNode)
    {
        List<PathNode> neighbourList = new List<PathNode>(); 
        //Change to binary tree 


        if (currentNode.x - 1 >= 0)
        {   //Left
            neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y));
            //Left Down
            if (currentNode.y - 1 >= 0) {
                neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y - 1));
            }
            //Left Up
            if (currentNode.y + 1 < grid.GetHeight())
            {
                neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y + 1));
            }
        }

        if (currentNode.x + 1 < grid.GetWidth())
        {
            //Right
            neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y));
            //Right Down
            if (currentNode.y - 1 >= 0)
            {
                neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y - 1));
            }
            //Right Up
            if (currentNode.y + 1 < grid.GetHeight())
            {
                neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y + 1));
            }
        }

        //Down
        if (currentNode.y  - 1 >= 0)
        {   
            neighbourList.Add(GetNode(currentNode.x, currentNode.y - 1));
        }
        //Up
        if (currentNode.y + 1 < grid.GetHeight())
        {
            neighbourList.Add(GetNode(currentNode.x, currentNode.y + 1));
        }

        return neighbourList;
    }

    public PathNode GetNode(int x, int y)
    {
        return grid.GetGridObject(x, y);
    }

    private List<PathNode> CalculatePath(PathNode endNode)
    {
        List<PathNode> path = new List<PathNode>();
        path.Add(endNode);

        PathNode currentNode = endNode; 

        while(currentNode.priorNode != null)
        {
            path.Add(currentNode.priorNode);
            currentNode = currentNode.priorNode; 
        }

        path.Reverse();
        return path; 
    }

    private int CalculateDistanceCost(PathNode a, PathNode b)
    {

        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);

        int remaining = Mathf.Abs(xDistance - yDistance);

        return MOVEDIAGONALCOST * Mathf.Min(xDistance, yDistance) + MOVESTRAIGHTCOST * remaining;
    }

    private PathNode GetLowestFCostNode(List<PathNode> pathNodeList)
    {
        PathNode lowestFCostNode = pathNodeList[0];
        
        for(var i = 1; i < pathNodeList.Count; i++)
        {
            /*    Debug.Log("Choices " + pathNodeList[i]);
                Debug.Log("Cost " + pathNodeList[i].fCost);*/
            if (pathNodeList[i].fCost < lowestFCostNode.fCost)
            {
                lowestFCostNode = pathNodeList[i];
            }
        }
        return lowestFCostNode; 
    }
}
