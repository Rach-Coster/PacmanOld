using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.Contracts;
using System.Net;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Text.RegularExpressions;
using System.Xml;

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
    private Camera cam;

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
    private GameObject gameBoard;

    public Transform topRightParent;

    public Transform topLeftParent;

    public Transform bottomLeftParent;

    public Transform bottomRightParent;

    public AudioClip audioClip;

    private float xOffset;
    private float yOffset;

    private List<GameObject> mazeParts;
    private List<GameObject> pellets;
    
    private List<KeyValuePair<GameObject, Boolean>> levelParts; 
    
    // Start is called before the first frame update
    void Start()
    {
        xOffset = -18;
        yOffset = 38;

        mazeParts = new List<GameObject>();
        pellets = new List<GameObject>();

        levelParts = new List<KeyValuePair<GameObject, Boolean>>(); 

        topLeftParent.transform.parent = gameBoard.transform;

        CreateLevel();

        DupeLevel(topRightParent, new Vector3(-1, 1, 1), 10, 0);
        DupeLevel(bottomLeftParent, new Vector3(1, -1, 1), 0, 17);
        DupeLevel(bottomRightParent, new Vector3(-1, -1, 1), -47.2f, 17);

        //ignoreBottomRow(); 
        RemoveGaps();
    }

    private void Update()
    {
        StartLevelAudio();
    }

    void CreateLevel()
    {
        for (var i = 0; i < 15; i++)
        {
            //temp
            for (var j = 0; j < 14; j++)
            {
                switch (levelMap[i, j])
                {
                    case 1:
                        GameObject mazeOC;

                        //To fix
                        if (i == 0 && j == 0)
                        {
                            mazeOC = Instantiate(mazeOuterCorner);
                            mazeOC.transform.position = new Vector2((xOffset - 1.233f), yOffset);

                        }
                        else if (i == 9 && j == 0)
                        {
                            mazeOC = Instantiate(mazeOuterCorner);
                            mazeOC.transform.position = new Vector2((xOffset - 1.233f), yOffset);
                            mazeOC.transform.localScale = new Vector2(1, -1);
                        }
                        else if (i == 9 && j == 5)
                        {
                            mazeOC = Instantiate(mazeInnerDouble);
                            mazeOC.transform.position = new Vector2((xOffset), (yOffset - 0.26f));
                            mazeOC.transform.localScale = new Vector2(-1, 1);
                        }
                        else if (i == 13)
                        {
                            mazeOC = Instantiate(mazeInnerDouble);
                            mazeOC.transform.position = new Vector2((xOffset), (yOffset - 0.25f));
                            mazeOC.transform.localScale = new Vector2(-1, -1);
                        }
                        else
                        {
                            mazeOC = Instantiate(mazeOuterCorner);
                            mazeOC.transform.position = new Vector2((xOffset - 1.24f), (yOffset + 0.01f));
                        }
                        mazeOC.name = "MazeOuterCorner " + i + " " + j;
                        mazeOC.transform.parent = topLeftParent;

                        mazeOC.AddComponent<BoxCollider2D>();
                        mazeOC.GetComponent<BoxCollider2D>().size = new Vector2(2.5f, 1);
                        mazeOC.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0.15f);

                        mazeParts.Add(mazeOC);
                        levelParts.Add(new KeyValuePair<GameObject, Boolean>(mazeOC, true));
                        break;

                    case 2:
                        GameObject mazeOL;
                        mazeOL = Instantiate(mazeOuterLine);

                        if (i >= 1 && j == 0 && i < 10)
                        {
                            mazeOL.transform.position = new Vector2((xOffset - 1.233f), yOffset);
                            mazeOL.transform.rotation = Quaternion.Euler(0, 0, 90);

                        }
                        else if (i == 9)
                        {
                            mazeOL.transform.position = new Vector2((xOffset - 1.23f), yOffset);
                            mazeOL.transform.localScale = new Vector2(1, -1);
                        }
                        else if (i >= 10 && i < 13)
                        {
                            mazeOL.transform.position = new Vector2((xOffset + 0.26f), (yOffset - 0.27f));
                            mazeOL.transform.rotation = Quaternion.Euler(0, 0, 90);
                        }
                        else if (i == 13)
                        {
                            mazeOL.transform.position = new Vector2((xOffset - 0.5f), (yOffset - 0.51f));
                        }
                        else
                        {
                            mazeOL.transform.position = new Vector2(xOffset, yOffset);
                        }

                        mazeOL.name = "MazeOuterLine " + i + " " + j;
                        mazeOL.transform.parent = topLeftParent;

                        mazeOL.AddComponent<BoxCollider2D>();
                        mazeOL.GetComponent<BoxCollider2D>().size = new Vector2(2.5f, 1);
                        mazeOL.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0.15f);

                        mazeParts.Add(mazeOL);
                        levelParts.Add(new KeyValuePair<GameObject, Boolean>(mazeOL, true));
                        break;
                    //TODO: Create offset generator 
                    case 3:
                        GameObject mazeIC;
                        mazeIC = Instantiate(mazeInnerCorner);

                        if (i == 6 && j == 5 || i == 6 && j == 8 ||
                           i == 9 && j == 11)
                        {
                            mazeIC.transform.position = new Vector2(xOffset, yOffset);
                            mazeIC.transform.localScale = new Vector2(-1, 1);

                        }
                        else if (i == 2 && j == 5 || i == 6 && j == 5)
                        {
                            mazeIC.transform.position = new Vector2(xOffset - 0.4f, yOffset);
                            mazeIC.transform.localScale = new Vector2(-1, 1);
                        }

                        else if (i == 2 && j == 2 || i == 6 && j == 2)
                        {
                            mazeIC.transform.position = new Vector2(xOffset - 0.4f, yOffset);
                            mazeIC.transform.localScale = new Vector2(1, 1);
                        }

                        else if (i == 4 && j == 2 || i == 7 && j == 2)
                        {
                            mazeIC.transform.position = new Vector2(xOffset - 0.4f, yOffset);
                            mazeIC.transform.localScale = new Vector2(1, -1);
                        }

                        else if (i == 4 && j == 5 | i == 7 && j == 5)
                        {
                            mazeIC.transform.position = new Vector2(xOffset - 0.4f, yOffset);
                            mazeIC.transform.localScale = new Vector2(-1, -1);
                        }
                        else if (i == 2 && j == 11)
                        {
                            mazeIC.transform.position = new Vector2(xOffset + 0.4f, yOffset);
                            mazeIC.transform.localScale = new Vector2(-1, 1);
                        }

                        else if (i == 2 && j == 7)
                        {
                            mazeIC.transform.position = new Vector2(xOffset + 0.4f, yOffset);
                            mazeIC.transform.localScale = new Vector2(1, 1);
                        }

                        else if (i == 4 && j == 7)
                        {
                            mazeIC.transform.position = new Vector2(xOffset + 0.4f, yOffset);
                            mazeIC.transform.localScale = new Vector2(1, -1);
                        }

                        else if (i == 4 && j == 11)
                        {
                            mazeIC.transform.position = new Vector2(xOffset + 0.4f, yOffset);
                            mazeIC.transform.localScale = new Vector2(-1, -1);
                        }
                        else if (i == 4 && j == 11 || i == 7 && j == 5)
                        {
                            mazeIC.transform.position = new Vector2(xOffset, yOffset);
                            mazeIC.transform.localScale = new Vector2(-1, -1);

                        }
                        else if (i == 4 && j == 7 || i == 7 && j == 2
                                || i == 10 && j == 13 || i == 13 && j == 7)
                        {
                            mazeIC.transform.position = new Vector2(xOffset, yOffset);
                            mazeIC.transform.localScale = new Vector2(1, -1);
                        }
                        else if (i == 4 && j == 13)
                        {
                            mazeIC.transform.position = new Vector2((xOffset + 1.24f), yOffset);
                            mazeIC.transform.localScale = new Vector2(1, -1);
                        }
                        else if (i == 7 && j == 10)
                        {
                            mazeIC.transform.position = new Vector2(xOffset, (yOffset + 0.02f));
                            mazeIC.transform.localScale = new Vector2(1, -1);
                        }
                        else if (i == 7 && j == 13)
                        {
                            mazeIC.transform.position = new Vector2((xOffset - 0.02f), yOffset);
                            mazeIC.transform.localScale = new Vector2(-1, 1);
                        }
                        else if (i == 9 && j == 8)
                        {
                            mazeIC.transform.position = new Vector2((xOffset + 0.02f), (yOffset + 0.02f));
                            mazeIC.transform.localScale = new Vector2(1, -1);
                        }
                        else if (i == 10 && j == 11)
                        {
                            mazeIC.transform.position = new Vector2(xOffset, (yOffset + 0.02f));
                            mazeIC.transform.localScale = new Vector2(-1, -1);
                        }
                        else if (i == 13 && j == 8)
                        {
                            mazeIC.transform.position = new Vector2((xOffset - 0.02f), yOffset);
                            mazeIC.transform.localScale = new Vector2(-1, -1);
                        }
                        else
                        {
                            mazeIC.transform.position = new Vector2(xOffset, yOffset);

                        }

                        mazeIC.name = "MazeInnerCorner " + i + " " + j;
                        mazeIC.transform.parent = topLeftParent;

                        mazeIC.AddComponent<BoxCollider2D>();
                        mazeIC.GetComponent<BoxCollider2D>().size = new Vector2(1.4f, 0.5f);
                        mazeIC.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0.15f);

                        mazeParts.Add(mazeIC);
                        levelParts.Add(new KeyValuePair<GameObject, Boolean>(mazeIC, true));
                        break;

                    case 4:
                        GameObject mazeIL;
                        mazeIL = Instantiate(mazeInnerLine);
                        if (i >= 7 && i < 9 && j == 8)
                        {
                            mazeIL.transform.position = new Vector2((xOffset + 0.02f), yOffset);
                            mazeIL.transform.rotation = Quaternion.Euler(0, 0, 90);
                        }
                        else if (i == 3 && j == 2)
                        {
                            mazeIL.transform.position = new Vector2((xOffset - 0.4f), yOffset);
                            mazeIL.transform.rotation = Quaternion.Euler(0, 0, 90);
                        }
                        else if (i == 2 && j == 3 || i == 2 && j == 4 || i == 6 && j == 3 || i == 6 && j == 4)
                        {
                            mazeIL.transform.position = new Vector2((xOffset - 0.4f), (yOffset));
                            mazeIL.transform.rotation = Quaternion.Euler(0, 0, 0);
                        }
                        else if (i == 4 && j == 3 || i == 4 && j == 4 || i == 7 && j == 3 || i == 7 && j == 4)
                        {
                            mazeIL.transform.position = new Vector2((xOffset - 0.4f), (yOffset - 0.02f));
                            mazeIL.transform.rotation = Quaternion.Euler(0, 0, 0);
                        }
                        else if (i == 3 && j == 7)
                        {
                            mazeIL.transform.position = new Vector2((xOffset + 0.4f), yOffset);
                            mazeIL.transform.rotation = Quaternion.Euler(0, 0, 90);
                        }
                        else if (i == 3 && j == 5)
                        {
                            mazeIL.transform.position = new Vector2((xOffset - 0.38f), yOffset);
                            mazeIL.transform.rotation = Quaternion.Euler(0, 0, 90);
                        }
                        else if (i == 3 && j == 11)
                        {
                            mazeIL.transform.position = new Vector2((xOffset + 0.42f), yOffset);
                            mazeIL.transform.rotation = Quaternion.Euler(0, 0, 90);
                        }
                        else if (i == 2 && j == 8 || i == 2 && j == 9)
                        {
                            mazeIL.transform.position = new Vector2((xOffset + 0.4f), (yOffset));
                            mazeIL.transform.rotation = Quaternion.Euler(0, 0, 0);
                        }
                        else if(i == 4 && j == 8|| i == 4 && j == 9)
                        {
                            mazeIL.transform.position = new Vector2((xOffset + 0.4f), (yOffset - 0.02f));
                            mazeIL.transform.rotation = Quaternion.Euler(0, 0, 0);
                        }
                        else if (i < 6 && j == 13 && i >= 1)
                        {
                            mazeIL.transform.position = new Vector2((xOffset + 1.24f), yOffset);
                            mazeIL.transform.rotation = Quaternion.Euler(0, 0, 90);
                        }
                        else if (i == 3 || i >= 7 && i < 9 && j >= 7 && j < 9
                                 || i >= 8 && i < 10 && j == 13 || i >= 9 && i < 11 && j == 7
                                 || i == 11 || i == 12 && j >= 7 && j < 9 || i == 13
                                 || i == 14)
                        {
                            mazeIL.transform.position = new Vector2((xOffset + 0.002f), yOffset);
                            mazeIL.transform.rotation = Quaternion.Euler(0, 0, 90);
                        }
                        else if (i == 4 && j >= 3 && j < 5 || i == 4 && j >= 8 && j < 11 || i == 7 && j >= 3 && j < 5)
                        {
                            mazeIL.transform.position = new Vector2(xOffset, (yOffset - 0.02f));
                        }
                        else
                        {
                            mazeIL.transform.position = new Vector2(xOffset, yOffset);

                        }

                        mazeIL.name = "MazeInnerLine " + i + " " + j;
                        mazeIL.transform.parent = topLeftParent;

                        mazeIL.AddComponent<BoxCollider2D>();
                        mazeIL.GetComponent<BoxCollider2D>().size = new Vector2(2.5f, 1);
                        mazeIL.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0.15f);

                        mazeParts.Add(mazeIL);
                        levelParts.Add(new KeyValuePair<GameObject, Boolean>(mazeIL, true));
                        break;

                    case 5:
                        GameObject mazeD;
                        mazeD = Instantiate(dot);

                        if (i == 1)
                        {
                            mazeD.transform.position = new Vector2((xOffset * 1.1f) + 1f, yOffset);
                        }
                        else if (i == 5)
                        {
                            mazeD.transform.position = new Vector2((xOffset * 1.11f) + 1.1f, yOffset);
                        }
                        else if (i == 8 && j < 8)
                        {
                            mazeD.transform.position = new Vector2((xOffset * 1.1f) + 1f, yOffset);
                        }
                        else if (i % 2 == 0 && i < 8 && j == 1 || i == 7 && j == 1)
                        {
                            mazeD.transform.position = new Vector2(xOffset - 0.75f, yOffset);
                        }
                        else if (i >= 2 && i < 5 && j == 12)
                        {
                            mazeD.transform.position = new Vector2(xOffset + 0.75f, yOffset);
                        }
                        else if (i >= 9 && i <= 14 && j == 6)
                        {
                            mazeD.transform.position = new Vector2(xOffset, (yOffset + 0.5f));
                        }
                        else
                        {
                            mazeD.transform.position = new Vector2(xOffset, yOffset);
                        }


                        mazeD.name = "Dot " + i + " " + j;
                        mazeD.transform.parent = topLeftParent;
                        mazeParts.Add(mazeD);
                        pellets.Add(mazeD);
                        levelParts.Add(new KeyValuePair<GameObject, Boolean>(mazeD, false));
                        break;

                    case 6:
                        GameObject mazeP;
                        mazeP = Instantiate(pellet);
                        if (i == 3 && j == 1)
                        {
                            mazeP.transform.position = new Vector2(xOffset - 0.75f, yOffset);
                        }
                        else
                        {
                            mazeP.transform.position = new Vector2(xOffset, yOffset);
                        }

                        mazeP.transform.parent = topLeftParent;
                        pellets.Add(mazeP);
                        mazeParts.Add(mazeP);
                        levelParts.Add(new KeyValuePair<GameObject, Boolean>(mazeP, false));

                        break;

                    case 7:
                        GameObject mazeOT;
                        mazeOT = Instantiate(mazeOuterT);
                        mazeOT.transform.position = new Vector2((xOffset + 1.24f), (yOffset + 0.01f));

                        mazeOT.name = "MazeOuterT " + i + " " + j;
                        mazeOT.transform.parent = topLeftParent;

                        mazeOT.AddComponent<BoxCollider2D>();
                        mazeOT.GetComponent<BoxCollider2D>().size = new Vector2(2.5f, 1);
                        mazeOT.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0.15f);

                        mazeParts.Add(mazeOT);

                        levelParts.Add(new KeyValuePair<GameObject, Boolean>(mazeOT, true));
                        break;
                }

                xOffset += 1.25f;
            }
            xOffset = -18f;
            yOffset -= 2.49f;
        }
    }

    void DupeLevel(Transform parent, Vector3 scale, float xAddition, float yAddition)
    {

        for (var i = 0; i < levelParts.Count; i++)
        {
            GameObject clone;
            clone = Instantiate(levelParts[i].Key);

            clone.transform.position = new Vector2(levelParts[i].Key.transform.position.x + xAddition, levelParts[i].Key.transform.position.y + yAddition);
            clone.transform.parent = parent;
           
        }

        parent.transform.localScale = scale;
        parent.parent = gameBoard.transform;
    }

    //To streamline
    void RemoveGaps()
    {
        GameObject gap;

        gap = Instantiate(mazeInnerLine);
        gap.transform.position = new Vector2((xOffset + 18.5f), (yOffset + 12.43f));

        gap = Instantiate(mazeInnerLine);
        gap.transform.position = new Vector2((xOffset + 18.5f), (yOffset + 22.41f));

        gap = Instantiate(mazeInnerLine);
        gap.transform.position = new Vector2((xOffset + 18.5f), (yOffset - 8.67f));

        gap = Instantiate(mazeInnerLine);
        gap.transform.position = new Vector2((xOffset + 18.5f), (yOffset - 18.65f));

        gap = Instantiate(mazeInnerLine);
        gap.transform.position = new Vector2((xOffset + 18.5f), (yOffset - 3.71f));
        gap.transform.localScale = new Vector3(1.85f, 1, 1);

        gap = Instantiate(dot);
        gap.transform.position = new Vector2((xOffset + 18.5f), (yOffset + 24.9f));
        gap = Instantiate(dot);
        gap.transform.position = new Vector2((xOffset + 18.5f), (yOffset - 21.1f));
    }
    public List<GameObject> GetPellets()
    {
        if (pellets.Count != 0)
        {
            return pellets;
        }

        return null;
    }
    public List<GameObject> GetWalls()
    {   if (levelParts != null)
        {
            List<GameObject> walls;
            walls = new List<GameObject>();

            for (var i = 0; i < levelParts.Count; i++)
            {
                if (levelParts[i].Value == true)
                {
                    walls.Add(levelParts[i].Key);
                }
            }

            return walls;
        }
        return null; 
    }
    public GameObject GetGameboard()
    {
        return gameBoard;
    }

    void StartLevelAudio()
    {
        AudioSource audioSource = cam.GetComponent<AudioSource>();

        if (!audioSource.isPlaying)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
            audioSource.loop = true;
        }
    }
}

