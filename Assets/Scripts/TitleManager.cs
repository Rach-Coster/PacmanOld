using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject titleScreen;

    void Start()
    {
        GenerateCanvas();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void GenerateCanvas()
    {
        Instantiate(titleScreen);
    }
}
