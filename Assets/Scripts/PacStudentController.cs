using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
    string currentInput;

    RaycastHit2D hitWall;
    
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
        //TODO: Animation should stop
        walls = gameObject.GetComponent<LevelGenerator>().GetWalls(); 

        if (Input.GetKey("d"))
        {
            hitWall = Physics2D.Raycast(pacmanInstance.transform.position, Vector2.right);

            pacmanInstance.transform.localScale = new Vector2(0.8f, 0.8f);
            pacmanInstance.transform.rotation = Quaternion.Euler(0, 0, 0);

            if (Mathf.Abs(hitWall.distance) <= 2)
            {
                Debug.Log("Distance " + hitWall.distance);
                Debug.Log("Result: " + (hitWall.point.x - 2) + " " + hitWall.point.y);
                
                if((hitWall.point.x - 2) != -2 && hitWall.point.y != 0)
                    pacmanInstance.transform.position = new Vector2(hitWall.point.x - 2, hitWall.point.y);
            }
            else
            {
                AddTween(pacmanInstance.transform, pacmanInstance.transform.position, new Vector2(pacmanInstance.transform.position.x + 1, pacmanInstance.transform.position.y), 0.1f);
            }
            lastInput = "d";
        }

        else if (Input.GetKey("w"))
        {
            hitWall = Physics2D.Raycast(pacmanInstance.transform.position, Vector2.up);

            pacmanInstance.transform.localScale = new Vector2(0.8f, 0.8f);
            pacmanInstance.transform.rotation = Quaternion.Euler(0, 0, 90);
            if(Mathf.Abs(hitWall.distance) <= 2)
            {
                pacmanInstance.transform.position = new Vector2(hitWall.point.x, hitWall.point.y - 2);
            } 
            else 
            {
                AddTween(pacmanInstance.transform, pacmanInstance.transform.position, new Vector2(pacmanInstance.transform.position.x, pacmanInstance.transform.position.y + 1), 0.1f);
            }
            lastInput = "w";
        }

        else if (Input.GetKey("a"))
        {
            hitWall = Physics2D.Raycast(pacmanInstance.transform.position, Vector2.left);

            pacmanInstance.transform.localScale = new Vector2(-0.8f, 0.8f);
            pacmanInstance.transform.rotation = Quaternion.Euler(0, 0, 0);

            if (Mathf.Abs(hitWall.distance) <= 2)
            {
                Debug.Log("Distance " + hitWall.distance);
                Debug.Log("Result: " + (hitWall.point.x - 2) + " " + hitWall.point.y);

                if ((hitWall.point.x - 2) != -2 && hitWall.point.y != 0)
                    pacmanInstance.transform.position = new Vector2(hitWall.point.x + 2, hitWall.point.y);
            }
            else
            {
                AddTween(pacmanInstance.transform, pacmanInstance.transform.position, new Vector2(pacmanInstance.transform.position.x - 1, pacmanInstance.transform.position.y), 0.1f);
            }
            lastInput = "a";
        }

        else if (Input.GetKey("s"))
        {
            hitWall = Physics2D.Raycast(pacmanInstance.transform.position, Vector2.down);

            pacmanInstance.transform.localScale = new Vector2(0.8f, 0.8f);
            pacmanInstance.transform.rotation = Quaternion.Euler(0, 0, -90);
            if (Mathf.Abs(hitWall.distance) <= 2)
            {
                pacmanInstance.transform.position = new Vector2(hitWall.point.x, hitWall.point.y - 2);
            }
            else
            {
                AddTween(pacmanInstance.transform, pacmanInstance.transform.position, new Vector2(pacmanInstance.transform.position.x, pacmanInstance.transform.position.y - 1), 0.1f);
            }
            lastInput = "s"; 
        }

        else
        {
           //TODO: Add Switch lastInput 

        }

        LerpTweens(); 
        delta += Time.deltaTime; 
    }

    void GeneratePacman()
    {
        pacmanInstance = Instantiate(pacmanObject);
        pacmanInstance.transform.position = new Vector2(-17, 35.5f);
        pacmanInstance.transform.localScale = new Vector2(0.8f, 0.8f);
        pacmanInstance.AddComponent<Rigidbody2D>();
        pacmanInstance.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        pacmanInstance.GetComponent<Rigidbody2D>().sleepMode = RigidbodySleepMode2D.NeverSleep;

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
