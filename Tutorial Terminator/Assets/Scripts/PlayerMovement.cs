using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem.Composites;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpPower = 7f;
    [SerializeField] private float wallJumpPushAgainstWallForce = 3f;
    [SerializeField] private float wallJumpUpPushForce = 20f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float wallDrag = 9.8f;
    [SerializeField] private bool allowCrouch = false;
    [SerializeField] private float crouchSpeed = 5f;
    [SerializeField] private bool allowAttack = false;
    [SerializeField] private Transform head;
    [SerializeField] private LayerMask obstacleLayer;

    [Range(0, 1)]
    [SerializeField] private float crouchShrink = 0.5f;
    private Rigidbody2D body;
    private CapsuleCollider2D capsuleCollider;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;
    private PlayerAttacks playerAttacks;

    private Vector2 standingSize;
    private Vector2 standingOffset;
    private Vector2 crouchingSize;
    private Vector2 crouchingOffset;
    private bool isCrouched;
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        playerAttacks = GetComponent<PlayerAttacks>();
        standingSize = capsuleCollider.size;
        standingOffset = capsuleCollider.offset;

        crouchingSize = new Vector2(standingSize.x, standingSize.y * crouchShrink);
        crouchingOffset = new Vector2(standingOffset.x, standingOffset.y - (standingSize.y - crouchingSize.y) * crouchShrink);
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        //Flip player when moving left or right
        if (horizontalInput < -0.01f && Mathf.Sign(transform.localScale.x) > 0)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
        else if (horizontalInput > 0.01f && Mathf.Sign(transform.localScale.x) < 0)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }

        //Set animation parameters
        anim.SetBool("isRun", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        float moveSpeed = isCrouched ? crouchSpeed : speed;
        //Wall jump logic
        if (wallJumpCooldown > 0.2f)
        {
            //player basic movement
            body.linearVelocity = new Vector2(horizontalInput * moveSpeed, body.linearVelocity.y);
            if (onWall() && !isGrounded())
            {
                body.gravityScale = wallDrag;
                body.linearVelocity = Vector2.zero;
            }
            else
            {
                body.gravityScale = 2.5f;
            }
            if (Input.GetKey(KeyCode.Space))
                Jump();
        }
        else
        {
            wallJumpCooldown += Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.C) && allowCrouch)
        {
            ToggleCrouch();
        }
    }
    private void Jump()
    {
        if (isGrounded())
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
            anim.SetTrigger("jump");
        }
        else if (onWall() && !isGrounded())
        {
            if (horizontalInput == 0)
            {
                body.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * jumpPower * 1.5f, jumpPower * 3);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x) * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
                body.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpPushAgainstWallForce, wallJumpUpPushForce);
            wallJumpCooldown = 0f;


        }
    }
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.CapsuleCast(capsuleCollider.bounds.center, capsuleCollider.bounds.size, capsuleCollider.direction, 0, Vector2.down, 0.1f, groundLayer);
        //RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.CapsuleCast(capsuleCollider.bounds.center, capsuleCollider.bounds.size, capsuleCollider.direction, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);

        // RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall();
    }

    public bool canStand() {
        Vector2 origin = transform.position;
        Vector2 target = head.position; 

        Vector2 direction = (target - origin).normalized;
        float distance = Vector2.Distance(origin, target);

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, distance, obstacleLayer);
        return hit.collider == null;
    }

    public void ToggleCrouch()
    {
        if (isCrouched)
        {
            if (!canStand())
                return;
            isCrouched = false;
            Debug.Log("Stood up");
            capsuleCollider.size = standingSize;
            capsuleCollider.offset = standingOffset;
        }
        else
        {
            Debug.Log("Crouched");
            isCrouched = true;
            capsuleCollider.size = crouchingSize;
            capsuleCollider.offset = crouchingOffset;
        }

    }
}

