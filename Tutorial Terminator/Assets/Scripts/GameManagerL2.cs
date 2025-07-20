using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerL2 : MonoBehaviour
{
    public GameObject StartBound;
    public Transform player;
    public List<GameObject> Boundaries;
    public List<Transform> Point;
    public List<Transform> JPoint;
    public List<bool> hasCrossed;
    public List<bool> hasJumped;

    bool canJump=false;

    int Jumpcount;
    int crouchCount;
    int current=0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Boundaries = new List<GameObject>();
        Point = new List<Transform>();
        JPoint = new List<Transform>();
        hasCrossed = new List<bool>();
        hasJumped = new List<bool>();

        StartCoroutine(StartGame());
    }

    // Update is called once per frame
    void Update()
    {
        if (current < Point.Count && current < hasCrossed.Count && !hasCrossed[current] && Vector3.Distance(player.position, Point[current].position) < 1f)
        {
            hasCrossed[current] = true;
            Debug.Log("Player crossed the point!");
            if(crouchCount == 0)
            {
                GetComponent<TextController>().PushText("Wow! got it on the first try!");
            }
            else if (crouchCount == 1)
            {
                GetComponent<TextController>().PushText("You are getting good at this!");
            }
            else if (crouchCount == 2)
            {
                GetComponent<TextController>().PushText("well done! Let's that try again, shall we?");
            }
            else if (crouchCount == 3)
            {
                GetComponent<TextController>().PushText("NICE! Let's try that again");
            }
            else if (crouchCount == 4)
            {
                GetComponent<TextController>().PushText("Don't give up! Not yet XD");
            }
            else if (crouchCount == 5)
            {
                GetComponent<TextController>().PushText("Wow! Nice!");
            }
            else if (crouchCount >= 5)
            {
                GetComponent<TextController>().PushText("Let's keep the spirit High!");
            }
            crouchCount++;
            StartCoroutine(DestoryBounds(4));
        }

        if (current < JPoint.Count && current < hasCrossed.Count && !hasJumped[current] && Vector3.Distance(player.position, JPoint[current].position) < 1f)
        {
            hasJumped[current] = true;
            Debug.Log("Player jumped the point!");
            if(Jumpcount == 0)
            {
                GetComponent<TextController>().PushText("Nooo, we are practicing crouch!");
            }
            if(Jumpcount == 1)
            {
                GetComponent<TextController>().PushText("Can you stop, I don't like this");
            }
            if(Jumpcount == 2)
            {
                GetComponent<TextController>().PushText("THIS IS YOUR FINAL WARNING!");
            }
            if (Jumpcount == 3)
            {
                GetComponent<TextController>().PushText("Ok. Fine. you can pass this level...");
                StartCoroutine(EndGame());                
            }
            StartCoroutine(DestoryBounds(4));
            Jumpcount++;
        }
        if(current > 5 && !canJump)
        {
            player.gameObject.GetComponent<PlayerController>().canJump = true;
            canJump = true;
        }
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(5);
        FindAnyObjectByType<SceneLoader>().LoadNextLevel();
    }

    IEnumerator DestoryBounds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(Boundaries[current]);
        current++;
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(5);
        gameObject.GetComponent<TextController>().PushText("HELLO PLAYER!");
        yield return new WaitForSeconds(3);
        gameObject.GetComponent<TextController>().PushText("Let's start your second training session!");
        yield return new WaitForSeconds(5);
        gameObject.GetComponent<TextController>().PushText("In this session, you will learn how to crouch properly!");
        yield return new WaitForSeconds(7);
        gameObject.GetComponent<TextController>().PushText("To crouch, you can use the keyboard button 'C'");
        yield return new WaitForSeconds(7);
        gameObject.GetComponent<TextController>().PushText("You see this tree with a hanging out branch in the distance?");
        yield return new WaitForSeconds(7);
        gameObject.GetComponent<TextController>().PushText("Let's try passing it by crouching under it!");
        StartBound.SetActive(false);
    }
}
