using System;
using UnityEngine;
using UnityEngine.Rendering;

public class gun : MonoBehaviour
{
    public float maxAngle=90f;
    public float bullet_speed = 5f;
    public float minAngle = -90f;
    public int counts_till_shoot = 5;
    public Transform shoot_point;
    private int bullets_shot = 0;
    public LayerMask EnemyLayer;
    public float time_to_shoot = 0.2f;
    public GameObject bullet;
    private Vector3 aim_point;
    private bool available;
    private Camera main_cam;
    private float timer=0f;
    void Start()
    {
        main_cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        Aim();
        if (!available && timer > time_to_shoot)
        {
            available = true;
            timer = 0f;
        }
        timer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.G) && available)
        {
            Shoot();
            available = false;
        }

    }

    void Aim()
    {
        aim_point = main_cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = aim_point - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle = Mathf.Clamp(angle, minAngle, maxAngle);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Shoot()
    {
        RaycastHit2D hit = Physics2D.Raycast(shoot_point.position, transform.right, Mathf.Infinity, EnemyLayer);
        if (hit.collider != null && bullets_shot < counts_till_shoot)
        {
            Debug.Log("Cannot hit enemy yet");
            return;
        }
        GameObject shot = Instantiate(bullet, shoot_point.position, Quaternion.identity);
        bullets_shot += 1;
        Rigidbody2D rb = shot.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = transform.right * bullet_speed;
        }
        Debug.Log("bullet shot");
    }
}
