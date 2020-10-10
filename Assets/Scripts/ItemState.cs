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
    private GameObject mapTopLeft;

    private GameObject pelletAudioSource; 

    public AudioClip[] audioClip;
    AudioSource audioSource;

    Canvas canvas;
    const int lives = 3; 
    // Start is called before the first frame update
    void Start()
    {
        pellet = new List<GameObject>();
        pelletAudioSource = new GameObject();
        audioSource = pelletAudioSource.AddComponent<AudioSource>();
        pelletAudioSource.name = "PelletAudioSource";
        audioSource.clip = audioClip[0]; 

        CreateItems();  


    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time < 1)
        {
            pellet = gameObject.GetComponent<LevelGenerator>().GetPellets();
            gameBoard = gameObject.GetComponent<LevelGenerator>().GetGameboard();

            mapTopLeft = gameBoard.transform.Find("MapTopLeft").gameObject;
            GameObject dot = mapTopLeft.transform.Find("Dot 1 1").gameObject;
            dot.SetActive(false);
        }
    }

    void CreateItems()
    {
        /*float xPos = -15.8f; 
        Instantiate(cherry);
        cherry.transform.position = new Vector2(-17.8f, -35.5f);*/
    }

}
