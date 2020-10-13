using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngineInternal;

public class PacmanMovement : MonoBehaviour {

    public AudioClip audioClip; 

    [SerializeField]
    GameObject pacman;

    GameObject pm;

    List<TweenLibrary> activeTweens;

    float deltaTime;

    // Start is called before the first frame update
    void Start()
    {
        activeTweens = new List<TweenLibrary>();

        GeneratePacman();
        deltaTime = 0; 
    }

    // Update is called once per frame
    void Update()
    {
        if (activeTweens != null)
        {
            for (var i = 0; i < activeTweens.Count; i++)
            {
                if (Mathf.Abs((activeTweens[i].Target.position.x - activeTweens[i].EndPos.x)) > 0.1f ||
                   (Mathf.Abs((activeTweens[i].Target.position.y - activeTweens[i].EndPos.y)) > 0.1f))
                {
                    
                    float timeFraction = (deltaTime - activeTweens[i].StartTime) / activeTweens[i].Duration;
                    float lengthOfJourney = Vector2.Distance(activeTweens[i].StartPos, activeTweens[i].EndPos);
                    float distanceFraction = timeFraction / lengthOfJourney;

                    activeTweens[i].Target.position = Vector3.Lerp(activeTweens[i].StartPos, activeTweens[i].EndPos, distanceFraction);
                }

                else if (Mathf.Abs((activeTweens[i].Target.position.x - activeTweens[i].EndPos.x)) <= 0.1f ||
                        (Mathf.Abs((activeTweens[i].Target.position.y - activeTweens[i].EndPos.y)) <= 0.1f))
                {
                    activeTweens[i].Target.position = activeTweens[i].EndPos;
                    activeTweens.RemoveAt(i);
                }
            }
        }
        deltaTime += Time.deltaTime;
    }

    void GeneratePacman()
    {
        pm = Instantiate(pacman);
        pm.transform.position = new Vector2(-17, 35.5f);
        pm.transform.localScale = new Vector2(1, 1);
        pm.AddComponent<AudioSource>();
        pm.GetComponent<SpriteRenderer>().sortingOrder = 1; 
    }

    void AddTween(Transform targetObject, Vector2 startPos, Vector2 endPos, float duration)
    {
        TweenLibrary itemTweened = new TweenLibrary(targetObject.transform, startPos, endPos, deltaTime, duration);
        
        activeTweens.Add(itemTweened);
    }


    public Vector2 GetPacmanPosition()
    {
        return new Vector2(pm.transform.position.x, pm.transform.position.y);

    }

    public Transform GetPacmanTransform()
    {
        return pm.transform; 
    }
}
