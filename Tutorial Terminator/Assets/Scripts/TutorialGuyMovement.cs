using Unity.VisualScripting;
using UnityEngine;

public class TutorialGuyMovement : MonoBehaviour
{
    [SerializeField] private float height = 9.0f;
    [SerializeField] private Transform player;
    [SerializeField] private float xOffset = 2.0f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float yOffset = 1f;
    [SerializeField] private float verticalBound;
    [SerializeField] private float horizontalBoundA;
    [SerializeField] private float horizontalBoundB;
    [SerializeField] private float TimeBetweenRecalculation = 0.5f;
    public Transform Location;
    public bool moveToLocation = false;
    private Vector3 moveTo;
    private float timer;
    private float lastMoveUpdate = -5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (moveToLocation)
        {
            moveTo = Location.position;
        }
        else
        {
            if (timer - lastMoveUpdate > TimeBetweenRecalculation)
            {
                float xPos = player.position.x + Random.Range(-xOffset, xOffset);
                float yPos = player.position.y + height + Mathf.Sin(timer) * yOffset;
                moveTo = new Vector3(xPos, yPos, transform.position.z);
                lastMoveUpdate = timer;
            }
        }
    }
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, moveTo, moveSpeed * Time.deltaTime);
    }
}
