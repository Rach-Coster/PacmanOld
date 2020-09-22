﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
    private GameObject mazeInnerLine;

    [SerializeField]
    private GameObject mazeOuterLine;

    [SerializeField]
    private GameObject mazeOuterT;

    [SerializeField]
    private GameObject mazeInnerDouble;

    [SerializeField]
    private GameObject pellet;

    [SerializeField]
    private GameObject dot;

    [SerializeField]
    private GameObject ghostYellow;

    [SerializeField]
    private GameObject ghostGreen;

    [SerializeField]
    private GameObject ghostRed;

    [SerializeField]
    private GameObject ghostPink;


    public Transform topRightParent;

    public Transform topLeftParent;

    public Transform bottomLeftParent;

    public Transform bottomRightParent;


    private float xOffset;
    private float yOffset;

    private List<GameObject> mazeParts;


    // Start is called before the first frame update
    void Start()
    {
        xOffset = -18;
        yOffset = 38;

        mazeParts = new List<GameObject>();

        CreateLevel();
        GenerateGhosts();

        DupeLevel(topRightParent, new Vector3(-1, 1, 1), 10, 0);
        DupeLevel(bottomLeftParent, new Vector3(1, -1, 1), 0, 17);
        DupeLevel(bottomRightParent, new Vector3(-1, -1, 1), -47.2f, 17);

        RemoveGaps();
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
            for (var j = 0; j < 14; j++)
            {
                /*   Debug.Log("LevelMap: " + levelMap[i, j]);
                   Debug.Log("2D Array" + i + " " + j);*/

                switch (levelMap[i, j])
                {
                    case 1:
                        GameObject mazeOC;
                        if (i == 9 && j == 0)
                        {
                            mazeOC = Instantiate(mazeOuterCorner);
                            mazeOC.transform.position = new Vector2((xOffset - 1.24f), (yOffset + 0.01f));
                            mazeOC.transform.localScale = new Vector2(1, -1);
                            mazeOC.transform.parent = topLeftParent;
                            mazeParts.Add(mazeOC);
                        }
                        else if (i == 9 && j == 5)
                        {
                            mazeOC = Instantiate(mazeInnerDouble);
                            mazeOC.transform.position = new Vector2((xOffset), (yOffset - 0.25f));
                            mazeOC.transform.localScale = new Vector2(-1, 1);
                            mazeOC.transform.parent = topLeftParent;
                            mazeParts.Add(mazeOC);
                        }
                        else if (i == 13)
                        {
                            mazeOC = Instantiate(mazeInnerDouble);
                            mazeOC.transform.position = new Vector2((xOffset), (yOffset - 0.25f));
                            mazeOC.transform.localScale = new Vector2(-1, -1);
                            mazeOC.transform.parent = topLeftParent;
                            mazeParts.Add(mazeOC);
                        }
                        else
                        {
                            mazeOC = Instantiate(mazeOuterCorner);
                            mazeOC.transform.position = new Vector2((xOffset - 1.24f), (yOffset + 0.01f));
                            mazeOC.transform.parent = topLeftParent;
                            mazeParts.Add(mazeOC);
                        }
                        mazeOC.name = "MazeOuterCorner " + i + " " + j;
                        break;

                    case 2:
                        GameObject mazeOL;
                        if (i >= 1 && j == 0 && i < 10)
                        {
                            mazeOL = Instantiate(mazeOuterLine);
                            mazeOL.transform.position = new Vector2((xOffset - 1.23f), yOffset);
                            mazeOL.transform.rotation = Quaternion.Euler(0, 0, 90);
                            mazeOL.transform.parent = topLeftParent;
                            mazeParts.Add(mazeOL);
                        }
                        else if (i == 9)
                        {
                            mazeOL = Instantiate(mazeOuterLine);
                            mazeOL.transform.position = new Vector2((xOffset - 1.23f), yOffset);
                            mazeOL.transform.localScale = new Vector2(1, -1);
                            mazeOL.transform.parent = topLeftParent;
                            mazeParts.Add(mazeOL);
                        }
                        else if (i == 10 || i == 11 || i == 12)
                        {
                            mazeOL = Instantiate(mazeOuterLine);
                            mazeOL.transform.position = new Vector2((xOffset + 0.26f), (yOffset - 0.27f));
                            mazeOL.transform.rotation = Quaternion.Euler(0, 0, 90);
                            mazeOL.transform.parent = topLeftParent;
                            mazeParts.Add(mazeOL);
                        }
                        else if (i == 13)
                        {
                            mazeOL = Instantiate(mazeOuterLine);
                            mazeOL.transform.position = new Vector2((xOffset - 0.5f), (yOffset - 0.51f));
                            mazeOL.transform.parent = topLeftParent;
                            mazeParts.Add(mazeOL);
                        }
                        else
                        {
                            mazeOL = Instantiate(mazeOuterLine);
                            mazeOL.transform.position = new Vector2(xOffset, yOffset);
                            mazeOL.transform.parent = topLeftParent;
                            mazeParts.Add(mazeOL);
                        }
                        mazeOL.name = "MazeOuterLine " + i + " " + j;
                        break;

                    case 3:
                        GameObject mazeIC;
                        if (i == 2 && j == 5 || i == 2 && j == 11 || i == 6 && j == 5 || i == 6 && j == 8 ||
                            i == 7 && j == 13 || i == 9 && j == 11)
                        {
                            mazeIC = Instantiate(mazeInnerCorner);
                            mazeIC.transform.position = new Vector2(xOffset, yOffset);
                            mazeIC.transform.localScale = new Vector2(-1, 1);
                            mazeIC.transform.parent = topLeftParent;
                            mazeParts.Add(mazeIC);

                        }
                        else if (i == 4 && j == 5 || i == 4 && j == 11 || i == 7 && j == 5 || i == 10 && j == 11
                                 || i == 13 && j == 8)
                        {
                            mazeIC = Instantiate(mazeInnerCorner);
                            mazeIC.transform.position = new Vector2(xOffset, yOffset);
                            mazeIC.transform.localScale = new Vector2(-1, -1);
                            mazeIC.transform.parent = topLeftParent;
                            mazeParts.Add(mazeIC);

                        }
                        else if (i == 4 && j == 2 || i == 4 && j == 7 || i == 7 && j == 2 || i == 7 && j == 10 || i == 9 && j == 8
                                 || i == 10 && j == 13 || i == 13 && j == 7)
                        {
                            mazeIC = Instantiate(mazeInnerCorner);
                            mazeIC.transform.position = new Vector2(xOffset, yOffset);
                            mazeIC.transform.localScale = new Vector2(1, -1);
                            mazeIC.transform.parent = topLeftParent;
                            mazeParts.Add(mazeIC);
                        }

                        else if (i == 4 && j == 13)
                        {
                            mazeIC = Instantiate(mazeInnerCorner);
                            mazeIC.transform.position = new Vector2((xOffset + 1.24f), yOffset);
                            mazeIC.transform.localScale = new Vector2(1, -1);
                            mazeIC.transform.parent = topLeftParent;
                            mazeParts.Add(mazeIC);
                        }

                        else
                        {
                            mazeIC = Instantiate(mazeInnerCorner);
                            mazeIC.transform.position = new Vector2(xOffset, yOffset);
                            mazeIC.transform.parent = topLeftParent;
                            mazeParts.Add(mazeIC);
                        }
                        mazeIC.name = "MazeInnerCorner " + i + " " + j;
                        break;

                    case 4:
                        GameObject mazeIL;
                        if (i < 6 && j == 13 && i >= 1)
                        {
                            mazeIL = Instantiate(mazeInnerLine);
                            mazeIL.transform.position = new Vector2((xOffset + 1.24f), yOffset);
                            mazeIL.transform.rotation = Quaternion.Euler(0, 0, 90);
                            mazeIL.transform.parent = topLeftParent;
                            mazeParts.Add(mazeIL);
                        }
                        else if (i == 3 || i == 7 && j == 7 || i == 7 && j == 8 || i == 8 && j == 7 ||
                                 i == 8 && j == 8 || i == 8 && j == 13 || i == 9 && j == 7 || i == 9 && j == 13
                                 || i == 10 && j == 7 || i == 11 || i == 12 && j == 7 || i == 12 && j == 8 || i == 13
                                 || i == 14)
                        {
                            mazeIL = Instantiate(mazeInnerLine);
                            mazeIL.transform.position = new Vector2((xOffset + 0.002f), yOffset);
                            mazeIL.transform.rotation = Quaternion.Euler(0, 0, 90);
                            mazeIL.transform.parent = topLeftParent;
                            mazeParts.Add(mazeIL);
                        }
                        else
                        {
                            mazeIL = Instantiate(mazeInnerLine);
                            mazeIL.transform.position = new Vector2(xOffset, yOffset);
                            mazeIL.transform.parent = topLeftParent;
                            mazeIL.name = "MazeInnerLine " + i + " " + j;
                            mazeParts.Add(mazeIL);
                        }

                        break;

                    case 5:
                        GameObject mazeD;
                        mazeD = Instantiate(dot);

                        if (i == 1)
                        {
                            mazeD.transform.position = new Vector2(xOffset * 1.1f + 1f, yOffset);
                        }
                        else if (i == 5)
                        {
                            mazeD.transform.position = new Vector2(xOffset * 1.15f + 1.8f, yOffset);
                        }

                        else if (i == 8 && j < 8)
                        {
                            mazeD.transform.position = new Vector2(xOffset * 1.1f + 1f, yOffset);
                        }
                        else if (i == 2 && j == 1 || i == 4 && j == 1 || i == 6 && j == 1 || i == 7 && j == 1)
                        {
                            mazeD.transform.position = new Vector2(xOffset - 0.75f, yOffset);
                        }
                        else if (i == 2 && j == 12 || i == 3 && j == 12 || i == 4 && j == 12)
                        {
                            mazeD.transform.position = new Vector2(xOffset + 0.75f, yOffset);
                        }
                        else if(i >= 9 && i <= 14 && j == 6)
                        {
                            mazeD.transform.position = new Vector2(xOffset, (yOffset * 1) + 0.5f);
                        }
                        else
                        {
                            mazeD.transform.position = new Vector2(xOffset, yOffset);
                        }


                        mazeD.name = "Dot " + i + " " + j; 
                        mazeD.transform.parent = topLeftParent;
                        mazeParts.Add(mazeD);
                        break;

                    case 6:
                        GameObject mazeP;
                        mazeP = Instantiate(pellet);
                        if(i == 3 && j == 1)
                        {
                            mazeP.transform.position = new Vector2(xOffset - 0.75f, yOffset);
                        }
                        else
                        {
                            mazeP.transform.position = new Vector2(xOffset, yOffset);
                        }
                        mazeP.name = "Pellet " + i + " " + j;
                        mazeP.transform.parent = topLeftParent;
                        mazeParts.Add(mazeP);
                        break; 

                    case 7:
                        GameObject mazeOT;
                        mazeOT = Instantiate(mazeOuterT);
                        mazeOT.transform.position = new Vector2((xOffset + 1.24f), (yOffset + 0.01f));

                        mazeOT.name = "MazeOuterT " + i + " " + j;
                        mazeOT.transform.parent = topLeftParent;
                        mazeParts.Add(mazeOT);
                        break;
                }

                //14 for row 2 straight 

                xOffset += 1.25f;
            }
            xOffset = -18f;
            yOffset -= 2.49f;
        }
    }

    void DupeLevel(Transform parent, Vector3 scale, float xAddition, float yAddition)
    {

        for (var i = 0; i < mazeParts.Count; i++)
        {
            GameObject clone;
            clone = Instantiate(mazeParts[i]);

            clone.transform.position = new Vector2(mazeParts[i].transform.position.x + xAddition, mazeParts[i].transform.position.y + yAddition);
            clone.transform.parent = parent;
        }

        parent.transform.localScale = scale;
    }

    //To streamline
    void RemoveGaps()
    {
        GameObject gap;

        gap = Instantiate(mazeInnerLine);
        gap.transform.position = new Vector2(0.5f, 13f);

        gap = Instantiate(mazeInnerLine);
        gap.transform.position = new Vector2(0.5f, 23f);

        gap = Instantiate(mazeInnerLine);
        gap.transform.position = new Vector2(0.5f, -8f);

        gap = Instantiate(mazeInnerLine);
        gap.transform.position = new Vector2(0.5f, -18f);

        gap = Instantiate(mazeInnerLine);
        gap.transform.position = new Vector2(0.5f, -3.1f);
        gap.transform.localScale = new Vector3(1.8f, 1, 1);

    }

    void GenerateGhosts()
    {
        GameObject ghost;
        ghost = Instantiate(ghostRed);
        ghost.transform.position = new Vector2(-3f, 0) ;
        ghost.transform.localScale = new Vector2(1, 1);

        ghost = Instantiate(ghostYellow);
        ghost.transform.position = new Vector2(4f, 0);
        ghost.transform.localScale = new Vector2(1, 1);

        ghost = Instantiate(ghostPink);
        ghost.transform.position = new Vector2(-3f, 5.5f);
        ghost.transform.localScale = new Vector2(1, 1);

        ghost = Instantiate(ghostGreen);
        ghost.transform.position = new Vector2(4, 5.5f);
        ghost.transform.localScale = new Vector2(1, 1);
    }

    void ClearMaze()
    {

        for (var i = 0; i < mazeParts.Count; i++)
        {
            mazeParts.RemoveAt(i);
        }
    }
}

