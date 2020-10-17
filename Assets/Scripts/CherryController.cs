using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
public class CherryController : MonoBehaviour
{
    TweenLibrary tween;
    [SerializeField]
    GameObject cherryPrefab;
    GameObject cherryInstance; 

    float delta;
    bool hasSpawned;
    // Start is called before the first frame update
    void Start()
    {   
        hasSpawned = false; 
        //TODO: Externalise time into its own class for more accurate  
    }

    // Update is called once per frame
    void Update()
    {
        //Temp until externalised clock function
        if (Mathf.Round(delta) % 33 == 0 && Mathf.Round(delta) != 0 && hasSpawned == false)
        {
            SpawnCherry();
            hasSpawned = true;

        }
        else if(Mathf.Round(delta) % 33 != 0)
        {
            hasSpawned = false;
        }

        if (tween != null)
        {
            float timeFraction = (delta - tween.StartTime) / tween.Duration;
            float lengthOfJourney = Vector2.Distance(tween.StartPos, tween.EndPos);
            float distanceFraction = timeFraction / lengthOfJourney;

            tween.Target.position = Vector3.Lerp(tween.StartPos, tween.EndPos, distanceFraction);
        }
        
        if(tween != null && tween.Target.position.x == tween.EndPos.x)
        {
            Destroy(cherryInstance);
            cherryInstance = null;
            tween = null;
        }

        delta += Time.deltaTime;
    }

    void SpawnCherry()
    {
        cherryInstance = Instantiate(cherryPrefab);
        cherryInstance.transform.position = new Vector2(75, 0);
        //Fix to go across screen properly
        tween = new TweenLibrary(cherryInstance.transform, new Vector2(75, 0), new Vector2(-75, 0), delta, 0.1f);
    }
}
