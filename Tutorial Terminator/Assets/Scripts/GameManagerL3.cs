using System.Collections;
using UnityEngine;

public class GameManagerL3 : MonoBehaviour
{
    public GameObject StartBound;
    public GameObject EndBound;
    public GameObject PlayerBox;
    public GameObject PlayerTutorialGuy;
    public GameObject Box;
    public GameObject TutorialGuy;
    public GameObject FallingTutorialGuy;

    public Vector3 BoxOffset;

    public Transform player;
    public Transform BoxesPosition;
    public Vector3 LastBoxPos;

    public bool IsHoldingBox;
    public bool IsHoldingTutorialGuy;
    public bool canPickTutGuy;
    int count = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(StartGame());
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsHoldingBox && Vector3.Distance(player.position,BoxesPosition.position) < 2 && Input.GetKeyDown(KeyCode.B))
        {
            IsHoldingBox = true;
            PlayerBox.SetActive(true);
        }

        if (!IsHoldingBox && Vector3.Distance(player.position, TutorialGuy.transform.position) < 1 && Input.GetKeyDown(KeyCode.B))
        {
            IsHoldingBox = true;
            IsHoldingTutorialGuy = true;
            TutorialGuy.SetActive(false);
            PlayerTutorialGuy.SetActive(true);
        }
        if (IsHoldingBox && Vector3.Distance(player.position,LastBoxPos) < 1 && !IsHoldingTutorialGuy)
        {
            PlaceBox();
        }
        if (IsHoldingBox && Vector3.Distance(player.position, LastBoxPos) < 1 && IsHoldingTutorialGuy)
        {
            IsHoldingTutorialGuy = false;
            StartCoroutine(EndGame());
        }

        if (count >= 3 && !TutorialGuy.GetComponent<TutorialGuyMovement>().moveToLocation)
        {
            TutorialGuy.GetComponent<TutorialGuyMovement>().moveToLocation = true;
        }

        if(count>= 8 && !canPickTutGuy)
        {
            canPickTutGuy = true;
        }

    }


    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(2);
        PlayerTutorialGuy.SetActive(false);
        FallingTutorialGuy.SetActive(true);
        FallingTutorialGuy.transform.position = LastBoxPos + 2*BoxOffset;
    }

    void PlaceBox()
    {
        if (!IsHoldingTutorialGuy)
        {
            Vector3 Pos = LastBoxPos + BoxOffset;
            Instantiate(Box, Pos, Quaternion.identity);
            LastBoxPos += BoxOffset;
            IsHoldingBox = false;
            PlayerBox.SetActive(false);
            EndBound.transform.Translate(BoxOffset);
            if (count == 0)
            {
                gameObject.GetComponent<TextController>().PushText("Great Job! Let's practice again!");
            }
            else if (count == 1)
            {
                gameObject.GetComponent<TextController>().PushText("You are getting better at this!");
            }
            else if (count == 2)
            {
                gameObject.GetComponent<TextController>().PushText("Isn't this fun!!");
            }
            else if (count == 3)
            {
                gameObject.GetComponent<TextController>().PushText("Let's keep trying!");
            }
            else if (count == 4)
            {
                gameObject.GetComponent<TextController>().PushText("Wow! You are getting better!");
            }
            else if (count == 5)
            {
                gameObject.GetComponent<TextController>().PushText("Don't give up, not yet!");
            }
            else if (count == 6)
            {
                gameObject.GetComponent<TextController>().PushText("Just a little more!");
            }
            else if (count == 7)
            {
                gameObject.GetComponent<TextController>().PushText("Perfect! Keep trying!");
            }
            else if (count >= 8)
            {
                gameObject.GetComponent<TextController>().PushText("Wow! Nice try!");
            }
            count++;
        }
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(5);
        gameObject.GetComponent<TextController>().PushText("Welcome!");
        yield return new WaitForSeconds(3);
        gameObject.GetComponent<TextController>().PushText("In this tutorial, we will learn how to pick up and place objects");
        yield return new WaitForSeconds(5);
        gameObject.GetComponent<TextController>().PushText("You seeing this ocean and the boxes?");
        yield return new WaitForSeconds(7);
        gameObject.GetComponent<TextController>().PushText("Press the button 'B' to pickup the boxes, and place them in the correct place");
        yield return new WaitForSeconds(7);
        gameObject.GetComponent<TextController>().PushText("Let's try to cross the ocean!");
        StartBound.SetActive(false);
    }
}
