using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ItemState : MonoBehaviour
{
    private List<GameObject> pellet;

    [SerializeField]
    GameObject cherry;

    [SerializeField]
    GameObject remainingLives;

    private GameObject gameBoard;
    private GameObject mapBottomLeft;
    private GameObject mapTopLeft;

    private GameObject pelletAudioSource; 

    public AudioClip[] audioClip;
    AudioSource audioSource;

    const int lives = 3; 
    Vector2 pacmanPos;
    // Start is called before the first frame update
    void Start()
    {
        pellet = new List<GameObject>();
        pelletAudioSource = new GameObject();
        audioSource = pelletAudioSource.AddComponent<AudioSource>();
        pelletAudioSource.name = "PelletAudioSource";
        audioSource.clip = audioClip[0]; 

        pacmanPos = new Vector2();

        CreateItems();  


    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time < 1)
        {
            pellet = gameObject.GetComponent<LevelGenerator>().GetPellets();
            gameBoard = gameObject.GetComponent<LevelGenerator>().GetGameboard();
        }


        pacmanPos = gameObject.GetComponent<PacmanMovement>().GetPacmanPosition();


        if (Math.Round(pacmanPos.x) == -17 && Math.Round(pacmanPos.y) == 35 || Math.Round(pacmanPos.x) == -17 && Math.Round(pacmanPos.y) == 36)
        {
            mapTopLeft = gameBoard.transform.Find("MapTopLeft").gameObject;
            GameObject dot = mapTopLeft.transform.Find("Dot 1 1").gameObject;
            dot.SetActive(false);
        }
    }

    void CreateItems()
    {
        float xPos = -15.8f; 
        Instantiate(cherry);
        cherry.transform.position = new Vector2(-17.8f, -35.5f);

        for (var i = 0; i < lives; i++)
        {
            Instantiate(remainingLives);
            remainingLives.transform.position = new Vector2(xPos += 2, -35.5f);
        }
    }

}
