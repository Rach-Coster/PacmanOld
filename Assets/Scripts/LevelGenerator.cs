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

    private float xOffset;
    private float yOffset;

    // Start is called before the first frame update
    void Start()
    {
        xOffset = -8.5f;
        yOffset = 18;

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
            for (var j = 0; j < 14; j++)
            {
                /*   Debug.Log("LevelMap: " + levelMap[i, j]);
                   Debug.Log("2D Array" + i + " " + j);*/

                switch (levelMap[i, j])
                {
                    case 1:
                        if (i == 9 && j == 0)
                        {

                            GameObject clone;
                            clone = Instantiate(mazeOuterCorner);
                            clone.transform.position = new Vector2((xOffset - 1.24f), (yOffset + 0.01f));
                            clone.transform.localScale = new Vector2(1, -1);
                        }
                        else if (i == 9 && j == 5)
                        {
                            GameObject clone;
                            clone = Instantiate(mazeInnerDouble);
                            clone.transform.position = new Vector2((xOffset), (yOffset - 0.25f));
                            clone.transform.localScale = new Vector2(-1, 1);
                        }
                        else if(i == 13)
                        {
                            GameObject clone;
                            clone = Instantiate(mazeInnerDouble);
                            clone.transform.position = new Vector2((xOffset), (yOffset - 0.25f));
                            clone.transform.localScale = new Vector2(-1, -1);
                        }
                        else
                        {
                            Instantiate(mazeOuterCorner);
                            mazeOuterCorner.transform.position = new Vector2((xOffset - 1.24f), (yOffset + 0.01f));
                        }
                        break;

                    case 2:
                        if (i >= 1 && j == 0 && i < 10)
                        {
                            GameObject clone;
                            clone = Instantiate(mazeOuterLine);
                            clone.transform.position = new Vector2((xOffset - 1.23f), yOffset);
                            clone.transform.rotation = Quaternion.Euler(0, 0, 90);
                        }
                        else if (i == 9)
                        {
                            GameObject clone;
                            clone = Instantiate(mazeOuterLine);
                            clone.transform.position = new Vector2((xOffset - 1.23f), yOffset);
                            clone.transform.localScale = new Vector2(1, -1);
                        }
                        else if (i == 10 || i == 11 || i == 12)
                        {
                            GameObject clone;
                            clone = Instantiate(mazeOuterLine);
                            clone.transform.position = new Vector2((xOffset + 0.26f), (yOffset - 0.27f));
                            clone.transform.rotation = Quaternion.Euler(0, 0, 90);
                        }
                        else if(i == 13)
                        {
                            GameObject clone;
                            clone = Instantiate(mazeOuterLine);
                            clone.transform.position = new Vector2((xOffset - 0.5f), (yOffset - 0.51f));
                        }
                        else
                        {
                            Instantiate(mazeOuterLine);
                            mazeOuterLine.transform.position = new Vector2(xOffset, yOffset);
                        }

                        break;

                    case 3:
                        if (i == 2 && j == 5 || i == 2 && j == 11 || i == 6 && j == 5 || i == 6 && j == 8 ||
                            i == 7 && j == 13 || i == 9 && j == 11)
                        {
                            GameObject clone;
                            clone = Instantiate(mazeInnerCorner);
                            clone.transform.position = new Vector2(xOffset, yOffset);
                            clone.transform.localScale = new Vector2(-1, 1);

                        }
                        else if (i == 4 && j == 5 || i == 4 && j == 11 || i == 7 && j == 5 || i == 10 && j == 11
                                 || i == 13 && j == 8)
                        {
                            GameObject clone;
                            clone = Instantiate(mazeInnerCorner);
                            clone.transform.position = new Vector2(xOffset, yOffset);
                            clone.transform.localScale = new Vector2(-1, -1);
                        }
                        else if (i == 4 && j == 2 || i == 4 && j == 7 || i == 7 && j == 2 || i == 7 && j == 10 || i == 9 && j == 8
                                 || i == 10 && j == 13 || i == 13 && j == 7)
                        {
                            GameObject clone;
                            clone = Instantiate(mazeInnerCorner);
                            clone.transform.position = new Vector2(xOffset, yOffset);
                            clone.transform.localScale = new Vector2(1, -1);
                        }

                        else if (i == 4 && j == 13)
                        {
                            GameObject clone;
                            clone = Instantiate(mazeInnerCorner);
                            clone.transform.position = new Vector2((xOffset + 1.24f), yOffset);
                            clone.transform.localScale = new Vector2(1, -1);
                        }

                        else
                        {
                            Instantiate(mazeInnerCorner);
                            mazeInnerCorner.transform.position = new Vector2(xOffset, yOffset);
                        }

                        break;

                    case 4:
                        if (i < 6 && j == 13 && i >= 1)
                        {
                            GameObject clone;
                            clone = Instantiate(mazeInnerLine);
                            clone.transform.position = new Vector2((xOffset + 1.24f), yOffset);
                            clone.transform.rotation = Quaternion.Euler(0, 0, 90);
                        }
                        else if (i == 3 || i == 7 && j == 7 || i == 7 && j == 8 || i == 8 && j == 7 ||
                                 i == 8 && j == 8 || i == 8 && j == 13 || i == 9 && j == 7 || i == 9 && j == 13 
                                 || i == 10 && j == 7 || i == 11 || i == 12 && j == 7 || i == 12 && j == 8 || i == 13
                                 || i == 14)
                        {
                            GameObject clone;
                            clone = Instantiate(mazeInnerLine);
                            clone.transform.position = new Vector2((xOffset + 0.002f), yOffset);
                            clone.transform.rotation = Quaternion.Euler(0, 0, 90);
                        }
                        else
                        {
                            Instantiate(mazeInnerLine);
                            mazeInnerLine.transform.position = new Vector2(xOffset, yOffset);
                        }
               
                        break;

                    case 7:
                        Instantiate(mazeOuterT);
                        mazeOuterT.transform.position = new Vector2((xOffset + 1.24f), (yOffset + 0.01f));
                        break;
                }
                
                //14 for row 2 straight 

                xOffset += 1.25f;
            }
            xOffset = -8.5f;
            yOffset -= 2.49f;
        }
    }
}

