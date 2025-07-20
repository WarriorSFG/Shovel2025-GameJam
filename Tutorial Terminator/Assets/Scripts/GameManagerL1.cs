using System.Collections;
using UnityEngine;

public class GameManagerL1 : MonoBehaviour
{
    public GameObject StartBound;
    public GameObject player;
    public GameObject Bullseye;
    public GameObject Bow;
    public Transform ShootingPos;
    public Transform TutorialGuy;

    int TutorialGuyHealth=3;
    public bool canHitTutorialGuy;
    int count=0;
    int HitCount = 0;

    TutorialGuyMovement moveScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveScript = TutorialGuy.GetComponent<TutorialGuyMovement>();
        StartCoroutine(StartGame());
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(player.transform.position, ShootingPos.position) < 2)
        {
            Bow.SetActive(true);
            FindAnyObjectByType<PlayerController>().isStatic = true;
            //FindAnyObjectByType<SceneLoader>().LoadNextLevel();
        }
    }

    public void HitBullseye()
    {
        if (count == 0)
        {
            moveScript.moveAroundLocation = true;
            gameObject.GetComponent<TextController>().PushText("WOW! I will watch your career with great interest");
        }
        else if (count == 1)
        {
            gameObject.GetComponent<TextController>().PushText("Nice!");
        }
        else if (count == 2)
        {
            gameObject.GetComponent<TextController>().PushText("Let's keep trying");
        }
        else if (count == 3)
        {
            gameObject.GetComponent<TextController>().PushText("Nice shot!");
        }
        else if (count == 4)
        {
            gameObject.GetComponent<TextController>().PushText("Good going!");
        }
        else if (count >= 5)
        {
            gameObject.GetComponent<TextController>().PushText("Let's try again");
            if (canHitTutorialGuy == false)
            {
                canHitTutorialGuy = true;
            }

        } 
        count++;
    }

    public void HitTutorialGuy()
    {
        if (HitCount == 0)
        {
            gameObject.GetComponent<TextController>().PushText("OUCH!");
        }
        else if (HitCount == 1)
        {
            gameObject.GetComponent<TextController>().PushText("Stop!!");
        }
        else if (HitCount == 2)
        {
            gameObject.GetComponent<TextController>().PushText("OK FINE, just go to the next level...");
            StartCoroutine(EndGame());
        }
        HitCount++;
    }

    IEnumerator EndGame()
    {
        Destroy(TutorialGuy);
        yield return new WaitForSeconds(5);
        FindAnyObjectByType<SceneLoader>().LoadNextLevel();
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(5);
        gameObject.GetComponent<TextController>().PushText("Hello!");
        yield return new WaitForSeconds(3);
        gameObject.GetComponent<TextController>().PushText("This is going to be your first training sessions!");
        yield return new WaitForSeconds(5);
        gameObject.GetComponent<TextController>().PushText("In this session, we will learn how to defend ourself!");
        yield return new WaitForSeconds(7);
        gameObject.GetComponent<TextController>().PushText("Seeing the bullseye right there?");
        yield return new WaitForSeconds(7);
        gameObject.GetComponent<TextController>().PushText("Let's go there and practice our aim!");
        StartBound.SetActive(false);
    }
}
