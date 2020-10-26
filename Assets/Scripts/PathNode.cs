﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode {

    private GridManager<PathNode> grid;

    public int x, y;

    public int gCost;
    public int hCost;
    public int fCost;

    public bool isWalkable; 
    public PathNode priorNode;
    public PathNode(GridManager<PathNode> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
        isWalkable = true; 
    }

    public void CalculateFCost()
    {
        fCost = gCost + hCost; 
    } 

    public void SetIsWalkable(bool isWalkable)
    {
        this.isWalkable = isWalkable;
    }
    public override string ToString()
    {
        return x + "," + y;
    }
}
