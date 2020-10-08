using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class SpriteManager : MonoBehaviour
{

    [SerializeField]
    GameObject dot;

    [SerializeField]
    GameObject pacman;
    private GameObject pm; 

    [SerializeField]
    GameObject ghostRed;
    private GameObject gr; 

    [SerializeField]
    GameObject ghostGreen;
    private GameObject gg; 

    [SerializeField]
    GameObject ghostYellow;
    private GameObject gy; 

    [SerializeField]
    GameObject ghostPurple;
    private GameObject gp;

    [SerializeField]
    GameObject mask;


    List<TweenLibrary> activeTweens;

     float deltaTime = 0; 
    // Start is called before the first frame update
    void Start()
    {
        activeTweens = new List<TweenLibrary>();

        GenerateMask(); 
        GenerateGhosts();
        CreateBorder();
        GeneratePacman();
    

        addTween(pm.transform, new Vector2(18, -6), 0.25f);
        addTween(gr.transform, new Vector2(18, -6), 0.25f);
        addTween(gg.transform, new Vector2(18, -6), 0.25f);
        addTween(gy.transform, new Vector2(18, -6), 0.25f);
        addTween(gp.transform, new Vector2(18, -6), 0.25f);
    }

    // Update is called once per frame
    void Update()
    {   
        LerpSprites();
        HideGhosts(gr, gr.transform.position.x);
        HideGhosts(gg, gg.transform.position.x);
        HideGhosts(gy, gy.transform.position.x);
        HideGhosts(gp, gy.transform.position.x);

        deltaTime += Time.deltaTime;
    }

    void CreateBorder()
    {
        GameObject dBorder;
        float xSpacingTop = -20;
        float xSpacingBottom = -20;

        float ySpacingLeft = 10;
        float ySpacingRight = 10;
        for (var i = 0; i < 58; i++)
        {
            dBorder = Instantiate(dot);

            if (i % 2 == 0 && i < 40 || i % 2 == 1 && i >= 40 && i < 48 || i % 2 == 0 && i > 48)
            {

                dBorder.GetComponent<Animator>().enabled = true;

            }

            if (i < 20)
            {
                dBorder.transform.position = new Vector2(xSpacingTop += 2, 8);
            }
            else if(i < 40)
            {
                
                dBorder.transform.position = new Vector2(xSpacingBottom += 2, -8);
            }
            else if(i < 49)
            {
                dBorder.transform.position = new Vector2(-20, ySpacingLeft -= 2);
            }
            else
            {
                dBorder.transform.position  = new Vector2(20, ySpacingRight -= 2);
            }

            dBorder.transform.parent = GameObject.Find("Border").transform;
            dBorder.name = "dot " + i;
        }
    }

    void GeneratePacman()
    {
        pm = Instantiate(pacman);
        pm.transform.position = new Vector2(-18, -6);
        pm.transform.localScale = new Vector2(0.5f, 0.5f);
        pm.GetComponent<SpriteRenderer>().sortingOrder = 3;
    }

    void GenerateGhosts()
    {
        gr = Instantiate(ghostRed);
        gr.transform.position = new Vector2(-13, -6);
        gr.transform.localScale = new Vector2(0.5f, 0.5f);

        gg =Instantiate(ghostGreen);
        gg.transform.position = new Vector2(-11, -6);
        gg.transform.localScale = new Vector2(0.5f, 0.5f);


        gy = Instantiate(ghostYellow);
        gy.transform.position = new Vector2(-9, -6);
        gy.transform.localScale = new Vector2(0.5f, 0.5f);

        gp = Instantiate(ghostPurple);
        gp.transform.position = new Vector2(-7, -6);
        gp.transform.localScale = new Vector2(0.5f, 0.5f);
    }

    void GenerateMask()
    {
        Instantiate(mask);
        mask.transform.position = new Vector2(18, -6);
        mask.transform.localScale = new Vector2(0.5f, 1);
        mask.GetComponent<SpriteRenderer>().sortingOrder = 1; 

    }

    void addTween(Transform targetObject, Vector2 endPos, float duration)
    {
        TweenLibrary tweenLibrary = new TweenLibrary(targetObject, targetObject.transform.position, endPos, deltaTime, duration);

        activeTweens.Add(tweenLibrary); 
    }

    void LerpSprites()
    {
        if(activeTweens != null)
        {
            for (var i = 0; i < activeTweens.Count; i++)
            {
                float timeFraction = (deltaTime - activeTweens[i].StartTime) / activeTweens[i].Duration;
                float lengthOfJourney = Vector2.Distance(activeTweens[i].StartPos, activeTweens[i].EndPos);
                float distanceFraction = timeFraction / lengthOfJourney;

                activeTweens[i].Target.position = Vector2.Lerp(activeTweens[i].StartPos, activeTweens[i].EndPos, distanceFraction);
            }
        }

    }

    void HideGhosts(GameObject ghost, float xPos)
    {
        if(xPos == 18)
        {
            ghost.SetActive(false); 
        }
    }
}
