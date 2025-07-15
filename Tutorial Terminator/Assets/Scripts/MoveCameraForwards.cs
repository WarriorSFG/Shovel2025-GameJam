using UnityEngine;

public class BoundedCamera : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 5f;
    public Vector3 offset;

    public BoxCollider2D bounds;

    private float halfHeight;
    private float halfWidth;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        halfHeight = cam.orthographicSize;
        halfWidth = halfHeight * cam.aspect;
    }

    void LateUpdate()
    {
        Vector3 targetPos = player.position + offset;

        // Clamp target position to bounds
        Bounds cameraBounds = bounds.bounds;

        float clampedX = Mathf.Clamp(targetPos.x, cameraBounds.min.x + halfWidth, cameraBounds.max.x - halfWidth);
        float clampedY = Mathf.Clamp(targetPos.y, cameraBounds.min.y + halfHeight, cameraBounds.max.y - halfHeight);

        Vector3 boundedPosition = new Vector3(clampedX, clampedY, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, boundedPosition, smoothSpeed * Time.deltaTime);
    }
}
