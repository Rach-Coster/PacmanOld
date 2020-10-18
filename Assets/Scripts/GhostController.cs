using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    List<TweenLibrary> tweenList;
    PacStudentController PacStudentController; 

    [SerializeField]
    GameObject ghostRedPrefab;
    GameObject ghostRedInstance; 

    [SerializeField]
    GameObject ghostYellowPrefab;
    GameObject ghostYellowInstance;

    [SerializeField]
    GameObject ghostPurplePrefab;
    GameObject ghostPurpleInstance;

    [SerializeField]
    GameObject ghostGreenPrefab;
    GameObject ghostGreenInstance;

    float delta; 
    // Start is called before the first frame update
    void Start()
    {
        tweenList = new List<TweenLibrary>();
        GenerateGhosts();
        StartCoroutine(MoveToStartPos());

        delta = 0;
    }

    // Update is called once per frame
    void Update()
    {
        LerpGhosts();

        delta += Time.deltaTime; 
    }

    void GenerateGhosts()
    {
        ghostRedInstance = Instantiate(ghostRedPrefab);
        ghostRedInstance.transform.position = new Vector2(-3f, 0);
        ghostRedInstance.transform.localScale = new Vector2(1, 1);
        ghostRedInstance.GetComponent<SpriteRenderer>().sortingOrder = 1;

        ghostYellowInstance = Instantiate(ghostYellowPrefab);
        ghostYellowInstance.transform.position = new Vector2(4f, 0);
        ghostYellowInstance.transform.localScale = new Vector2(1, 1);
        ghostYellowInstance.GetComponent<SpriteRenderer>().sortingOrder = 1;

        ghostPurpleInstance = Instantiate(ghostPurplePrefab);
        ghostPurpleInstance.transform.position = new Vector2(-3f, 5.5f);
        ghostPurpleInstance.transform.localScale = new Vector2(1, 1);
        ghostPurpleInstance.GetComponent<SpriteRenderer>().sortingOrder = 1;

        ghostGreenInstance = Instantiate(ghostGreenPrefab);
        ghostGreenInstance.transform.position = new Vector2(4, 5.5f);
        ghostGreenInstance.transform.localScale = new Vector2(1, 1);
        ghostGreenInstance.GetComponent<SpriteRenderer>().sortingOrder = 1;
    }


    void AddTweens(Transform targetTransform, Vector2 startPos, Vector2 endPos, float duration)
    {
        TweenLibrary tween = new TweenLibrary(targetTransform, startPos, endPos, delta, duration);
        tweenList.Add(tween); 
    }

    void LerpGhosts()
    {
        if (tweenList != null)
        {
            for (var i = 0; i < tweenList.Count; i++)
            {
                float timeFraction = (delta - tweenList[i].StartTime) / tweenList[i].Duration;
                float lengthOfJourney = Vector2.Distance(tweenList[i].StartPos, tweenList[i].EndPos);
                float distanceFraction = timeFraction / lengthOfJourney;

                tweenList[i].Target.position = Vector2.Lerp(tweenList[i].StartPos, tweenList[i].EndPos, distanceFraction);

                //Apparently is vector3? 
                if(tweenList[i].Target.position.x == tweenList[i].EndPos.x && tweenList[i].Target.position.y == tweenList[i].EndPos.y)
                {  
                    tweenList.RemoveAt(i);
                }
            }
        }
    }

    IEnumerator MoveToStartPos()
    {
        StartCoroutine(PurpleGhostSetup());
        yield return new WaitForSeconds(2);

        StartCoroutine(GreenGhostSetup());
        yield return new WaitForSeconds(2);

        StartCoroutine(RedGhostSetup());
        yield return new WaitForSeconds(2);

        StartCoroutine(YellowGhostSetup());
    }

    IEnumerator RedGhostSetup()
    {
        AddTweens(ghostRedInstance.transform, new Vector2(-3, 0), new Vector2(0, 0), 0.25f);
        yield return new WaitForSeconds(0.8f);
        AddTweens(ghostRedInstance.transform, new Vector2(0, 0), new Vector2(0, 10.5f), 0.25f);
        yield return new WaitForSeconds(2.6f);
        AddTweens(ghostRedInstance.transform, new Vector2(0, 10.5f), new Vector2(-6.5f, 10.5f), 0.25f);
    }

    IEnumerator GreenGhostSetup()
    {
        AddTweens(ghostGreenInstance.transform, new Vector2(4, 5.5f), new Vector2(0, 5.5f), 0.25f);
        yield return new WaitForSeconds(1);
        AddTweens(ghostGreenInstance.transform, new Vector2(0, 5.5f), new Vector2(0, 10.5f), 0.25f);
        yield return new WaitForSeconds(1.2f);
        AddTweens(ghostGreenInstance.transform, new Vector2(0, 10.5f), new Vector2(-3, 10.5f), 0.25f);
        yield return new WaitForSeconds(0.8f);
        AddTweens(ghostGreenInstance.transform, new Vector2(-3, 10.5f), new Vector2(-3, 18), 0.25f);
        yield return new WaitForSeconds(2);
        AddTweens(ghostGreenInstance.transform, new Vector2(-3, 18), new Vector2(-6.5f, 18), 0.25f);
    }

    IEnumerator YellowGhostSetup()
    {
        AddTweens(ghostYellowInstance.transform, new Vector2(4, 0), new Vector2(0, 0), 0.25f);
        yield return new WaitForSeconds(1);

        AddTweens(ghostYellowInstance.transform, new Vector2(0, 0), new Vector2(0, 10.5f), 0.25f);
        yield return new WaitForSeconds(2.6f);

        AddTweens(ghostYellowInstance.transform, new Vector2(0, 10.5f), new Vector2(7.5f, 10.5f), 0.25f);
     
    }

    IEnumerator PurpleGhostSetup()
    {
        AddTweens(ghostPurpleInstance.transform, new Vector2(-3, 5.5f), new Vector2(0, 5.5f), 0.25f);
        yield return new WaitForSeconds(0.8f);

        AddTweens(ghostPurpleInstance.transform, new Vector2(0, 5.5f), new Vector2(0, 10.5f), 0.25f);
        yield return new WaitForSeconds(1.2f);

        AddTweens(ghostPurpleInstance.transform, new Vector2(0, 10.5f), new Vector2(4, 10.5f), 0.25f);
        yield return new WaitForSeconds(1);

        AddTweens(ghostPurpleInstance.transform, new Vector2(4, 10.5f), new Vector2(4, 18), 0.25f);
        yield return new WaitForSeconds(1.9f);

        AddTweens(ghostPurpleInstance.transform, new Vector2(4, 18), new Vector2(7.5f, 18), 0.25f);
        yield return new WaitForSeconds(0.9f);

        AddTweens(ghostPurpleInstance.transform, new Vector2(7.5f, 18), new Vector2(7.5f, 26), 0.25f);
        yield return new WaitForSeconds(2);

        AddTweens(ghostPurpleInstance.transform, new Vector2(7.5f, 26), new Vector2(18.5f, 26), 0.25f);
        yield return new WaitForSeconds(2.7f);

        AddTweens(ghostPurpleInstance.transform, new Vector2(18.5f, 26), new Vector2(18.5f, 18), 0.25f);
        yield return new WaitForSeconds(2);

        StartCoroutine(PurpleGhostBehaviour());
    }

    IEnumerator PurpleGhostBehaviour()
    {
        while (true)
        {
            AddTweens(ghostPurpleInstance.transform, new Vector2(18.5f, 18), new Vector2(11.5f, 18), 0.25f);
            yield return new WaitForSeconds(1.8f);

            AddTweens(ghostPurpleInstance.transform, new Vector2(11.5f, 18), new Vector2(11.5f, -13), 0.25f);
            yield return new WaitForSeconds(7.8f);

            AddTweens(ghostPurpleInstance.transform, new Vector2(11.5f, -13), new Vector2(18.5f, -13), 0.25f);
            yield return new WaitForSeconds(1.8f);

            AddTweens(ghostPurpleInstance.transform, new Vector2(18.5f, -13), new Vector2(18.5f, -30.5f), 0.25f);
            yield return new WaitForSeconds(4.4f);

            AddTweens(ghostPurpleInstance.transform, new Vector2(18.5f, -30.5f), new Vector2(3.5f, -30.5f), 0.25f);
            yield return new WaitForSeconds(3.7f);

            AddTweens(ghostPurpleInstance.transform, new Vector2(3.5f, -30.5f), new Vector2(3.5f, -20.5f), 0.25f);
            yield return new WaitForSeconds(2.5f);

            AddTweens(ghostPurpleInstance.transform, new Vector2(3.5f, -20.5f), new Vector2(-2, -20.5f), 0.25f);
            yield return new WaitForSeconds(1.4f);

            AddTweens(ghostPurpleInstance.transform, new Vector2(-2, -20.5f), new Vector2(-2, -30.5f), 0.25f);
            yield return new WaitForSeconds(2.5f);

            AddTweens(ghostPurpleInstance.transform, new Vector2(-2, -30.5f), new Vector2(-17.5f, -30.5f), 0.25f);
            yield return new WaitForSeconds(3.9f);

            AddTweens(ghostPurpleInstance.transform, new Vector2(-17.5f, -30.5f), new Vector2(-17.5f, -13), 0.25f);
            yield return new WaitForSeconds(4.4f);

            AddTweens(ghostPurpleInstance.transform, new Vector2(-17.5f, -13), new Vector2(-10.5f, -13), 0.25f);
            yield return new WaitForSeconds(1.8f);

            AddTweens(ghostPurpleInstance.transform, new Vector2(-10.5f, -13), new Vector2(-10.5f, 18.5f), 0.25f);
            yield return new WaitForSeconds(7.9f);

            AddTweens(ghostPurpleInstance.transform, new Vector2(-10.5f, 18.5f), new Vector2(-17.5f, 18.5f), 0.25f);
            yield return new WaitForSeconds(1.8f);

            AddTweens(ghostPurpleInstance.transform, new Vector2(-17.5f, 18.5f), new Vector2(-17.5f, 36), 0.25f);
            yield return new WaitForSeconds(4.4f);

            AddTweens(ghostPurpleInstance.transform, new Vector2(-17.5f, 36), new Vector2(-2, 36), 0.25f);
            yield return new WaitForSeconds(3.9f);

            AddTweens(ghostPurpleInstance.transform, new Vector2(-2, 36), new Vector2(-2, 26), 0.25f);
            yield return new WaitForSeconds(2.5f);

            AddTweens(ghostPurpleInstance.transform, new Vector2(-2, 26), new Vector2(3.5f, 26), 0.25f);
            yield return new WaitForSeconds(1.4f);

            AddTweens(ghostPurpleInstance.transform, new Vector2(3.5f, 26), new Vector2(3.5f, 36), 0.25f);
            yield return new WaitForSeconds(2.5f);

            AddTweens(ghostPurpleInstance.transform, new Vector2(3.5f, 36), new Vector2(18.5f, 36), 0.25f);
            yield return new WaitForSeconds(3.8f);

            AddTweens(ghostPurpleInstance.transform, new Vector2(18.5f, 36), new Vector2(18.5f, 18.5f), 0.25f);
            yield return new WaitForSeconds(4.4f);


        }
    }
}
