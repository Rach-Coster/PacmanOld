using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    [SerializeField]
    GameObject pacmanObject;
    GameObject pacmanInstance;

    List<TweenLibrary> activeTweens;
    LevelGenerator levelGenerator;

    private List <GameObject> walls;

    float delta;

    string lastInput;
    // Start is called before the first frame update
    void Start()
    {
        activeTweens = new List<TweenLibrary>();
        walls = new List<GameObject>(); 

        delta = 0;

        GeneratePacman();
    }

    // Update is called once per frame
    void Update()
    {
        walls = gameObject.GetComponent<LevelGenerator>().GetWalls(); 

        if (Input.GetKeyDown("d"))
        {
            pacmanInstance.transform.rotation = Quaternion.Euler(0, 0, 0);
            AddTween(pacmanInstance.transform, pacmanInstance.transform.position, new Vector2(pacmanInstance.transform.position.x + 1.2f, pacmanInstance.transform.position.y), 0.5f);
            lastInput = "d";
        }

        else if (Input.GetKeyDown("w"))
        {
            pacmanInstance.transform.localScale = new Vector2(1, 1);
            pacmanInstance.transform.rotation = Quaternion.Euler(0, 0, 90);
            AddTween(pacmanInstance.transform, pacmanInstance.transform.position, new Vector2(pacmanInstance.transform.position.x, pacmanInstance.transform.position.y + 1.2f), 0.5f);
            lastInput = "w";
        }

        else if (Input.GetKeyDown("a"))
        {
            pacmanInstance.transform.localScale = new Vector2(-1, 1);
            pacmanInstance.transform.rotation = Quaternion.Euler(0, 0, 0);
            AddTween(pacmanInstance.transform, pacmanInstance.transform.position, new Vector2(pacmanInstance.transform.position.x - 1.2f, pacmanInstance.transform.position.y), 0.5f);
            lastInput = "a";
        }

        else if (Input.GetKeyDown("s"))
        {
            pacmanInstance.transform.localScale = new Vector2(1, 1);
            pacmanInstance.transform.rotation = Quaternion.Euler(0, 0, -90);
            AddTween(pacmanInstance.transform, pacmanInstance.transform.position, new Vector2(pacmanInstance.transform.position.x, pacmanInstance.transform.position.y - 1.2f), 0.5f);
            lastInput = "s"; 
        }

        else
        {
            //Insert collision here
        }

        LerpTweens(); 
        delta += Time.deltaTime; 
    }

    void GeneratePacman()
    {
        pacmanInstance = Instantiate(pacmanObject);
        pacmanInstance.transform.position = new Vector2(-17, 35.5f);
        pacmanInstance.transform.localScale = new Vector2(1, 1);
        pacmanInstance.GetComponent<SpriteRenderer>().sortingOrder = 1;
    }

    void AddTween(Transform targetObject, Vector2 startPos, Vector2 endPos, float duration)
    {
        TweenLibrary itemTweened = new TweenLibrary(targetObject.transform, startPos, endPos, delta, duration);

        activeTweens.Add(itemTweened);
    }

    void LerpTweens()
    {
        if (activeTweens != null)
        {
            for (var i = 0; i < activeTweens.Count; i++)
            {
                if (Mathf.Abs((activeTweens[i].Target.position.x - activeTweens[i].EndPos.x)) > 0.1f ||
                   (Mathf.Abs((activeTweens[i].Target.position.y - activeTweens[i].EndPos.y)) > 0.1f))
                {

                    float timeFraction = (delta - activeTweens[i].StartTime) / activeTweens[i].Duration;
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
    }
}
