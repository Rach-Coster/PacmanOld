using System;
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

    public Transform topRightParent;

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
            
        DupeLevel(topRightParent, new Vector3(-1, 1, 1), 10, 0);
        DupeLevel(bottomLeftParent, new Vector3(1, -1, 1), 0, 17);
        DupeLevel(bottomRightParent, new Vector3(-1, -1, 1), -42.2f, 17);

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
                            mazeParts.Add(mazeOC);
                        }
                        else if (i == 9 && j == 5)
                        {
                            mazeOC = Instantiate(mazeInnerDouble);
                            mazeOC.transform.position = new Vector2((xOffset), (yOffset - 0.25f));
                            mazeOC.transform.localScale = new Vector2(-1, 1);
                            mazeParts.Add(mazeOC);
                        }
                        else if(i == 13)
                        {
                            mazeOC = Instantiate(mazeInnerDouble);
                            mazeOC.transform.position = new Vector2((xOffset), (yOffset - 0.25f));
                            mazeOC.transform.localScale = new Vector2(-1, -1);
                            mazeParts.Add(mazeOC);
                        }
                        else
                        {
                            mazeOC = Instantiate(mazeOuterCorner);
                            mazeOC.transform.position = new Vector2((xOffset - 1.24f), (yOffset + 0.01f));
                            mazeParts.Add(mazeOC);
                        }
                        break;

                    case 2:
                        GameObject mazeOL;
                        if (i >= 1 && j == 0 && i < 10)
                        {
                            mazeOL = Instantiate(mazeOuterLine);
                            mazeOL.transform.position = new Vector2((xOffset - 1.23f), yOffset);
                            mazeOL.transform.rotation = Quaternion.Euler(0, 0, 90);
                            mazeParts.Add(mazeOL);
                        }
                        else if (i == 9)
                        {
                            mazeOL = Instantiate(mazeOuterLine);
                            mazeOL.transform.position = new Vector2((xOffset - 1.23f), yOffset);
                            mazeOL.transform.localScale = new Vector2(1, -1);
                            mazeParts.Add(mazeOL);
                        }
                        else if (i == 10 || i == 11 || i == 12)
                        {
                            mazeOL = Instantiate(mazeOuterLine);
                            mazeOL.transform.position = new Vector2((xOffset + 0.26f), (yOffset - 0.27f));
                            mazeOL.transform.rotation = Quaternion.Euler(0, 0, 90);
                            mazeParts.Add(mazeOL);
                        }
                        else if(i == 13)
                        {
                            mazeOL = Instantiate(mazeOuterLine);
                            mazeOL.transform.position = new Vector2((xOffset - 0.5f), (yOffset - 0.51f));
                            mazeParts.Add(mazeOL);
                        }
                        else
                        {
                            mazeOL = Instantiate(mazeOuterLine);
                            mazeOL.transform.position = new Vector2(xOffset, yOffset);
                            mazeParts.Add(mazeOL);
                        }

                        break;

                    case 3:
                        GameObject mazeIC; 
                        if (i == 2 && j == 5 || i == 2 && j == 11 || i == 6 && j == 5 || i == 6 && j == 8 ||
                            i == 7 && j == 13 || i == 9 && j == 11)
                        {
                            mazeIC = Instantiate(mazeInnerCorner);
                            mazeIC.transform.position = new Vector2(xOffset, yOffset);
                            mazeIC.transform.localScale = new Vector2(-1, 1);
                            mazeParts.Add(mazeIC);

                        }
                        else if (i == 4 && j == 5 || i == 4 && j == 11 || i == 7 && j == 5 || i == 10 && j == 11
                                 || i == 13 && j == 8)
                        {
                            mazeIC = Instantiate(mazeInnerCorner);
                            mazeIC.transform.position = new Vector2(xOffset, yOffset);
                            mazeIC.transform.localScale = new Vector2(-1, -1);
                            mazeParts.Add(mazeIC);

                        }
                        else if (i == 4 && j == 2 || i == 4 && j == 7 || i == 7 && j == 2 || i == 7 && j == 10 || i == 9 && j == 8
                                 || i == 10 && j == 13 || i == 13 && j == 7)
                        {
                            mazeIC = Instantiate(mazeInnerCorner);
                            mazeIC.transform.position = new Vector2(xOffset, yOffset);
                            mazeIC.transform.localScale = new Vector2(1, -1);
                            mazeParts.Add(mazeIC);
                        }

                        else if (i == 4 && j == 13)
                        {
                            mazeIC = Instantiate(mazeInnerCorner);
                            mazeIC.transform.position = new Vector2((xOffset + 1.24f), yOffset);
                            mazeIC.transform.localScale = new Vector2(1, -1);
                            mazeParts.Add(mazeIC);
                        }

                        else
                        {
                            mazeIC = Instantiate(mazeInnerCorner);
                            mazeIC.transform.position = new Vector2(xOffset, yOffset);
                            mazeParts.Add(mazeIC);
                        }

                        break;

                    case 4:
                        GameObject mazeIL;
                        if (i < 6 && j == 13 && i >= 1)
                        {
                            mazeIL = Instantiate(mazeInnerLine);
                            mazeIL.transform.position = new Vector2((xOffset + 1.24f), yOffset);
                            mazeIL.transform.rotation = Quaternion.Euler(0, 0, 90);
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
                            mazeParts.Add(mazeIL);
                        }
                        else
                        {
                            mazeIL = Instantiate(mazeInnerLine);
                            mazeIL.transform.position = new Vector2(xOffset, yOffset);
                            mazeParts.Add(mazeIL);
                        }
               
                        break;

                    case 7:
                        GameObject mazeOT;
                        mazeOT = Instantiate(mazeOuterT);
                        mazeOT.transform.position = new Vector2((xOffset + 1.24f), (yOffset + 0.01f));
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
        for(var i = 0; i < mazeParts.Count; i++)
        {
           // mazeParts.RemoveAt(i);
            GameObject clone;
            clone = Instantiate(mazeParts[i]);

            clone.transform.position = new Vector2(mazeParts[i].transform.position.x + xAddition, mazeParts[i].transform.position.y + yAddition);
            clone.transform.parent = parent;
        }

        parent.transform.localScale = scale;
    }

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

    void ClearMaze()
    {

        for (var i = 0; i < mazeParts.Count; i++)
        {
             mazeParts.RemoveAt(i);
        }
    }

