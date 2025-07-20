using System.Collections;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public GameObject Bullseye;
    public GameObject TutorialGuy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SelfDestruct());
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(gameObject.transform.position, Bullseye.transform.position) < 1)
        {
            FindAnyObjectByType<GameManagerL1>().HitBullseye();
            Destroy(gameObject);
        }

        if (FindAnyObjectByType<GameManagerL1>().canHitTutorialGuy && Vector3.Distance(gameObject.transform.position, TutorialGuy.transform.position) < 2)
        {
            FindAnyObjectByType<GameManagerL1>().HitTutorialGuy();
        }
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}
