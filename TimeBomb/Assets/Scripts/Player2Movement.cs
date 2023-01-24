using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Movement : MonoBehaviour
{
    public ParticleSystem ps;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private TrailRenderer trail;
    private BoxCollider2D coll;

    [Header("Ground")]
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] public Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;

    private float dirX = 0f;

    private bool isWallTouch;
    private bool isWallSliding;
    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingCounter;
    private bool isFacingRight = true;
    private bool doubleJump;


    private Vector2 dirDash;
    private bool isDashing;
    private bool canDash;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private float dashForce = 20f;
    public float wallSlidingSpeed;
    [SerializeField] private float wallJumpingDuration;
    [SerializeField] private Vector2 wallJumpingForce;

    private enum MovementState { idle, running, jumping, falling, doubleJumping, prout }

    [Header("Sound")]
    [SerializeField] private AudioSource jumpSoundEffect;

    /*public float speed = 10;
    public float jumpForce = 50;
    public float slideSpeed = 5;
    public float wallJumpLerp = 10;
    public float dashSpeed = 20;*/


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        trail = GetComponent<TrailRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        dirX = Input.GetAxisRaw("Debug Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);


        // Dash
        if (Input.GetButtonDown("DashPlayer2") && canDash)
        {
            isDashing = true;
            canDash = false;
            trail.emitting = true;
            dirDash = new Vector2(Input.GetAxisRaw("Debug Horizontal"), Input.GetAxisRaw("Debug Vertical"));
            if (dirDash == Vector2.zero)
            {
                dirDash = new Vector2(dirX, 0);
            }
            StartCoroutine(StopDashing());
        }

        if (isDashing)
        {
            rb.velocity = dirDash.normalized * dashForce;
            return;
        }

        if (IsGrounded())
        {
            canDash = true;
        }


        if (IsGrounded() && !Input.GetButtonDown("JumpPlayer2"))
        {
            doubleJump = false;
        }

        // Jump / Wall Jump
        if (Input.GetButtonDown("JumpPlayer2"))
        {
            if (IsGrounded() || !doubleJump && !isWallTouch)
            {
                jumpSoundEffect.Play();
                CreateDust();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                doubleJump = !doubleJump;
            }
            else if (isWallSliding)
            {
                isWallJumping = true;
            }
        }

        // Wall Slide
        isWallTouch = IsWallTouch();
        if (isWallTouch && !IsGrounded() && dirX != 0)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }

        if (isWallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }

        if (isWallJumping)
        {
            rb.velocity = new Vector2(-dirX * wallJumpingForce.x, wallJumpingForce.y);
            CreateDust();
            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
        

        Flip();
        UpdateAnimationState();
    }



    void Flip()
    {
        if (dirX < -0.01f) transform.localScale = new Vector3(-1, 1, 1);
        if (dirX > 0.01f) transform.localScale = new Vector3(1, 1, 1);
    }



    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private bool IsWallTouch()
    {
        return Physics2D.OverlapBox(wallCheck.position, new Vector2(.3f, 1.3f), 0, wallLayer);
    }

    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(0.15f);
        trail.emitting = false;
        isDashing = false;
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private void CreateDust()
    {
        ps.Play();
    }





    private void UpdateAnimationState()
    {
        MovementState state;

        if (isFacingRight && dirX < 0f || !isFacingRight && dirX > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }

        if (dirX > 0f)
        {   
            state = MovementState.running;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
        }
        else
        {
            if (!Input.GetButtonDown("ProutPlayer2")){
                state = MovementState.idle;
            }
            else
            {
                state = MovementState.prout;
            }
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }
        if (doubleJump && !isWallTouch && !IsGrounded())
        {
            state = MovementState.doubleJumping;
        }
        

        anim.SetInteger("state", (int)state);
    }
}

