using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerL2 : MonoBehaviour
{
    public List<GameObject> Boundaries;
    public Transform player;
    public List<Transform> Point;
    public List<bool> hasCrossed;

    int current=0;
    int BoundIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hasCrossed = new List<bool>();
        StartCoroutine(StartGame());
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasCrossed[current] && ((player.position - Point[current].position).magnitude < 1))
        {
            hasCrossed[current] = true;
            Debug.Log("Player crossed the point!");
            gameObject.GetComponent<TextController>().PushText("Nicely done! Let's try that again!");
            StartCoroutine(DestoryTrees(4));
        }

    }


    IEnumerator DestoryTrees(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(Boundaries[BoundIndex]);
        BoundIndex++;
        current++;

    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(10);
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
        Destroy(Boundaries[BoundIndex]);
        BoundIndex++;
    }
}
