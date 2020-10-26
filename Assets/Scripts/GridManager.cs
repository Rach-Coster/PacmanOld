using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UIElements;

//Allows for any type to be returned from this class
public class GridManager<TGridObject>
{
    private int width;
    private int height;

    private float cellSize;

    private TGridObject[,] gridArray;

    private TextMesh[,] debugTextArray;

    //Calling a function to make the array equal the gridObject
   //The extra brackets allows fdor the delegate function to take extra arguements
    public GridManager(int width, int height, float cellSize, Func<GridManager<TGridObject>, int, int, TGridObject> CreateGridObject)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize; 
        
        gridArray = new TGridObject[width, height];
        debugTextArray = new TextMesh[width, height]; 

        for(var i = 0; i < gridArray.GetLength(0); i++)
        {
            for(var j = 0; j < gridArray.GetLength(1); j++)
            {
                gridArray[i, j] = CreateGridObject(this, i, j);
            }
        }
        //Getting the length from the 1D part of the array
        for(var x = 0; x < gridArray.GetLength(0); x++)
        {
            //Getting the length from the 2D part of the array
            for (var y = 0; y < gridArray.GetLength(1); y++)
            {
               // gridArray[x, y] = CreateGridObject(x, y);

                debugTextArray[x, y] = CreateText(gridArray[x, y].ToString(), GameObject.Find("GridParent").transform, GetWorldPosition(x, y) + new Vector2(cellSize, cellSize) * 0.5f, 
                    15, Color.white, TextAnchor.MiddleCenter);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1),Color.red, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.red, 100f);

            }
        }
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.red, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.red, 100f);

        //SetGridObject(0, 0, 55);
    }

    public Vector2 GetWorldPosition(int x, int y)
    {
        return new Vector2(x, y) * cellSize; 
    }

    //Out keyword for returning two separate integers from the same function
    public void GetXYPos(Vector2 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt(worldPosition.x / cellSize);
        y = Mathf.FloorToInt(worldPosition.y / cellSize);
    }

    void SetGridObject (int x, int y, TGridObject value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = value;
            debugTextArray[x, y].text = gridArray[x, y].ToString(); 
        }
    }

    public void SetGridObject(Vector2 worldPosition, TGridObject value)
    {
        int x;
        int y;

        GetXYPos(worldPosition, out x, out y);
        SetGridObject(x, y, value);
    }

    public TGridObject GetGridObject(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        }

        return default(TGridObject);
    }

    public TGridObject GetGridObject(Vector2 worldPosition)
    {
        int x;
        int y;

        GetXYPos(worldPosition, out x, out y);
        return GetGridObject(x, y);
    }

    public int GetWidth()
    {
        return width; 
    }

    public int GetHeight()
    {
        return height; 
    }


    //Temp
    public static TextMesh CreateText(string text, Transform parent = null, Vector2 localPosition = default(Vector2), int fontSize = 40, Color? color = null, TextAnchor textAnchor = TextAnchor.UpperLeft, TextAlignment textAlignment = TextAlignment.Left, int sortingOrder = 1)
    {
        GameObject gameObject = new GameObject("Text ", typeof(TextMesh));
        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;

        TextMesh textmesh = gameObject.GetComponent<TextMesh>();
        textmesh.anchor = textAnchor;
        textmesh.alignment = textAlignment;
        textmesh.text = text;
        textmesh.fontSize = fontSize;
        textmesh.color = (Color) color;
        textmesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;

        return textmesh; 
    }
}
