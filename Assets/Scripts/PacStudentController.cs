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
        LerpTweens(); 
        delta += Time.deltaTime; 
    }

    void FixedUpdate()
    {

        if (Input.GetKey("w"))
        {
            hitWall = Physics2D.Raycast(pacmanInstance.transform.position, Vector2.up, 1);

            pacmanInstance.transform.localScale = new Vector2(0.8f, 0.8f);
            pacmanInstance.transform.rotation = Quaternion.Euler(0, 0, 90);

            if (hitWall)
            {
                Debug.Log("Hit Wall up");
                pacmanInstance.transform.position = new Vector2(hitWall.point.x, hitWall.point.y - 2);
            }
            AddTween(pacmanInstance.transform, pacmanInstance.transform.position, new Vector2(pacmanInstance.transform.position.x, pacmanInstance.transform.position.y + 1), 0.1f);
        }

        if (Input.GetKey("s"))
        {
            hitWall = Physics2D.Raycast(pacmanInstance.transform.position, Vector2.down, 1);

            pacmanInstance.transform.localScale = new Vector2(0.8f, 0.8f);
            pacmanInstance.transform.rotation = Quaternion.Euler(0, 0, -90);

            if (hitWall)
            {
                pacmanInstance.transform.position = new Vector2(hitWall.point.x, hitWall.point.y + 2);
            }
            AddTween(pacmanInstance.transform, pacmanInstance.transform.position, new Vector2(pacmanInstance.transform.position.x, pacmanInstance.transform.position.y - 1), 0.1f);
        }


        if (Input.GetKey("a"))
        {
            hitWall = Physics2D.Raycast(pacmanInstance.transform.position, Vector2.left, 1);

            pacmanInstance.transform.localScale = new Vector2(-0.8f, 0.8f);
            pacmanInstance.transform.rotation = Quaternion.Euler(0, 0, 0);
            Debug.Log("Hit Wall left");
            if (hitWall)
            {
                pacmanInstance.transform.position = new Vector2(hitWall.point.x + 2, hitWall.point.y);
            }
            AddTween(pacmanInstance.transform, pacmanInstance.transform.position, new Vector2(pacmanInstance.transform.position.x - 1, pacmanInstance.transform.position.y), 0.1f);
        }



        if (Input.GetKey("d"))
        {
            hitWall = Physics2D.Raycast(pacmanInstance.transform.position, Vector2.right, 1);

            pacmanInstance.transform.localScale = new Vector2(0.8f, 0.8f);
            pacmanInstance.transform.rotation = Quaternion.Euler(0, 0, 0);
            Debug.Log("Hit Wall right");
            if (hitWall)
            {
                pacmanInstance.transform.position = new Vector2(hitWall.point.x - 2, hitWall.point.y);
            }
            AddTween(pacmanInstance.transform, pacmanInstance.transform.position, new Vector2(pacmanInstance.transform.position.x + 1, pacmanInstance.transform.position.y), 0.1f);
        }

        else
        {
            //TODO: Add Switch lastInput 

        }

    }

    void GeneratePacman()
    {
        pacmanInstance = Instantiate(pacmanObject);
        pacmanInstance.transform.position = new Vector2(-17.52f, 35.5f);
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
