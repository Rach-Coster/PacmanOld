using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    int[,] levelMap =
    {
        {1,2,2,2,2,2,2,2,2,2,2,2,2,7},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,4},
        {2,6,4,0,0,4,5,4,0,0,0,4,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,3},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,5},
        {2,5,3,4,4,3,5,3,3,5,3,4,4,4},
        {2,5,3,4,4,3,5,4,4,5,3,4,4,3},
        {2,5,5,5,5,5,5,4,4,5,5,5,5,4},
        {1,2,2,2,2,1,5,4,3,4,4,3,0,4},
        {0,0,0,0,0,2,5,4,3,4,4,3,0,3},
        {0,0,0,0,0,2,5,4,4,0,0,0,0,0},
        {0,0,0,0,0,2,5,4,4,0,3,4,4,0},
        {2,2,2,2,2,1,5,3,3,0,4,0,0,0},
        {0,0,0,0,0,0,5,0,0,0,4,0,0,0},
    };

    [SerializeField]
    private GameObject mazeInnerCorner;

    [SerializeField]
    private GameObject mazeOuterCorner;

    [SerializeField]
    private GameObject mazeInnerStraight;

    [SerializeField]
    private GameObject mazeOuterStraight;

    [SerializeField]
    private GameObject mazeOuterT;

    // Start is called before the first frame update
    void Start()
    {
        CreateLevel();  
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    void CreateLevel()
    {
        for (var i = 0; i < 15; i++)
        {
            //temp
            for (var j = 0; j < i; j++)
            {
                Debug.Log("LevelMap: " + levelMap[i, j]);
                Debug.Log("2D Array" + i + " " + j);

                switch (levelMap[i, j])
                {
                    case 1:
                        Instantiate(mazeOuterCorner);
                        break;

                    case 2:
                        Instantiate(mazeOuterStraight);
                        break;

                    case 3:
                        Instantiate(mazeInnerCorner);
                        break;

                    case 4:
                        Instantiate(mazeInnerStraight);
                        break;

                    case 7:
                        Instantiate(mazeOuterT);
                        break;
                }
            }
        }
    }
}
