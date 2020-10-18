using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    Text countDown;
    Text timeText; 
    RectTransform rectTransform; 
    float delta;
    float timer; 
    // Start is called before the first frame update
    void Start()
    {
        countDown = GameObject.FindGameObjectWithTag("CountDown").GetComponent<Text>();
        timeText = GameObject.FindGameObjectWithTag("TimeText").GetComponent<Text>();

        rectTransform = GameObject.FindGameObjectWithTag("CountDown").GetComponent<RectTransform>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (delta < 1)
        {
            countDown.text = "3";
        }
        else if (delta < 2)
        {
            countDown.text = "2";
        }
        else if (delta < 3)
        {
            countDown.text = "1";
        }
        else if (delta < 4)
        {
            rectTransform.position = new Vector2(430, 350); 
            countDown.text = "GO!";
        }
        else if(delta > 5)
        {
            countDown.text = " ";
            timer += delta;
            //TODO: Split into 00:00:00
            timeText.text = "Time: " + timer.ToString();  
        }

        delta += Time.deltaTime; 
    }
}
