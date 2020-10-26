using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class GridCreator : MonoBehaviour
{

    [SerializeField]
    GameObject pellet;
    GameObject pelletInstance; 

    PathfindingManager pathfindingManager;

    private void Start()
    {

         pathfindingManager = new PathfindingManager(13, 23);

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPosition = Input.mousePosition;
            Vector3 gridPosition = Camera.main.ScreenToWorldPoint(mouseWorldPosition);
            
            pathfindingManager.GetGrid().GetXYPos(gridPosition, out int x, out int y);
            List<PathNode> path = pathfindingManager.FindPath(0, 0, x, y);

            if(path != null)
            {
                for(var i = 0; i < path.Count - 1; i++)
                {
            /*        Debug.Log(path[i].x + " , "+ path[i].y);*/
                    Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 3f + Vector3.one * 2f, new Vector3(path[i+1].x, path[i+1].y) * 3f + Vector3.one * 2f, Color.green, 20);
                }
            }
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 mouseWorldPosition = Input.mousePosition;
            Vector3 gridPosition = Camera.main.ScreenToWorldPoint(mouseWorldPosition);

            pathfindingManager.GetGrid().GetXYPos(gridPosition, out int x, out int y);
            pathfindingManager.GetNode(x, y).SetIsWalkable(!pathfindingManager.GetNode(x, y).isWalkable);

            pelletInstance = Instantiate(pellet);
            pelletInstance.transform.position = new Vector2((x * 3) + 1.5f, (y * 3) + 1.5f);
            pelletInstance.name = "Pellet" + x + " , " + y;
        }
    }
}