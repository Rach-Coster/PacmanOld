using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngineInternal;

public class PacmanMovement : MonoBehaviour
{
    [SerializeField]
    GameObject pacman;

    List<Tween> activeTweens;


    // Start is called before the first frame update
    void Start()
    {
        activeTweens = new List<Tween>(); 

        GeneratePacman();
        
    }

    // Update is called once per frame
    void Update()
    {
        lerp();
    }

    void GeneratePacman()
    {
        GameObject pm; 
        pm = Instantiate(pacman);
        pm.transform.position = new Vector2(-17, 2.5f);

        addTween(pm.transform, new Vector2(pm.transform.position.x, pm.transform.position.y), new Vector2(0, 2.5f), 1);
    }

    public bool addTween(Transform targetObject, Vector2 startPos, Vector2 endPos, float duration)
    {
        if (TweenExists(targetObject) == false)
        {
            Tween itemTweened = new Tween(targetObject, startPos, endPos, Time.time, duration);
            activeTweens.Add(itemTweened);
            return true;
        }
        return false;
    }

    void lerp()
    {
        if (activeTweens != null)
        {
            for (var i = 0; i < activeTweens.Count; i++)
            {
                if (Mathf.Abs((activeTweens[i].Target.position.x - activeTweens[i].EndPos.x)) > 0.01f ||
                   (Mathf.Abs((activeTweens[i].Target.position.y - activeTweens[i].EndPos.y)) > 0.01f))
                {
                    //Linear interpolation
                    float timeFraction = (Time.time - activeTweens[i].StartTime) / activeTweens[i].Duration;
                    float lengthOfJourney = Vector2.Distance(activeTweens[i].StartPos, activeTweens[i].EndPos);
                    float distanceFraction = timeFraction / lengthOfJourney;

                    activeTweens[i].Target.position = Vector2.Lerp(activeTweens[i].StartPos, activeTweens[i].EndPos, distanceFraction);
                    /*
                                        float timeDifference = (Time.time - activeTweens[i].StartTime) / activeTweens[i].Duration;
                                        float timeFraction = timeDifference * timeDifference * timeDifference;*/
/*
                    activeTweens[i].Target.position = Vector2.Lerp(activeTweens[i].StartPos, activeTweens[i].EndPos, timeFraction);*/
                }

                else if (Mathf.Abs((activeTweens[i].Target.position.x - activeTweens[i].EndPos.x)) <= 0.01f ||
                        (Mathf.Abs((activeTweens[i].Target.position.y - activeTweens[i].EndPos.y)) <= 0.01f))
                {
                    Debug.Log("We made it!");
                    activeTweens[i].Target.position = activeTweens[i].EndPos;
                    activeTweens.RemoveAt(i);
                }
            }
        }
    }
    public bool TweenExists(Transform target)
    {
        if (activeTweens == null)
        {
            return false;
        }


        for (var i = 0; i < activeTweens.Count; i++)
        {
            if (activeTweens[i].Target == target)
            {
                return true;
            }
        }
        return false;
    }
}
