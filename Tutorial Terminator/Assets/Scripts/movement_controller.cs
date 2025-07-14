using Unity.VisualScripting;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class movement_controller : MonoBehaviour
{
    public bool jump_allowed;
    public bool move_allowed;
    public bool crouch_allowed;
    public float crouch_offset;
    public float move_speed;
    public float jump_force;
    public Transform head;
    public float crouch_move_speed;
    public float crouch_jump_force;
    public Transform ground_check;
    public float ground_check_radius = 0.1f;
    public LayerMask ground_layer;
    //public Animator animator;
    private Rigidbody2D rb;
    private float input;
    private bool is_crouched=false;
    private bool is_grounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        input = Input.GetAxisRaw("Horizontal");
        if (input > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (input < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        is_grounded = Physics2D.OverlapCircle(ground_check.position, ground_check_radius, ground_layer);

        if (Input.GetButtonDown("Jump") && is_grounded)
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.S) && is_grounded)
        {
            handleCrouch(); 
        }

    }
    void handleCrouch()
    {
        Vector3 crouch = new Vector3(0, -crouch_offset, 0);
        if (is_crouched)
        {
            head.position -= crouch;
            is_crouched = false;
        }
        else
        {
            head.position += crouch;
            is_crouched = true;
        }
        Debug.Log("Crouch");
    }

    void FixedUpdate()
    {
        float speed = is_crouched ? crouch_move_speed : move_speed;
        rb.linearVelocity = new Vector2(input * speed, rb.linearVelocity.y);

    }
    void Jump()
    {
        if (is_crouched)
        {
            handleCrouch();
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, crouch_jump_force);
            
        }
        else
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jump_force);

        }
    }
    //HANDLE THE ANIMATIONS Pls
    void UpdateAnimations()
    {
    
    }
}
