using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
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
    /*private float dirY = 0f;*/

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
    /*[SerializeField] private float dashTime = 0.2f;*/
    public float wallSlidingSpeed;
    [SerializeField] private float wallJumpingDuration;
    [SerializeField] private Vector2 wallJumpingForce;

    private enum MovementState { idle, running, jumping, falling, doubleJumping, prout }

    [Header("Sound")]
    [SerializeField] private AudioSource jumpSoundEffect;


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
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);


        // Dash
        if (Input.GetButtonDown("Dash") && canDash)
        {
            isDashing = true;
            canDash = false;
            trail.emitting = true;
            dirDash = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
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

        
        if(IsGrounded() && !Input.GetKeyDown("space"))
        {
            doubleJump = false;
        }

        // Jump / Wall Jump
        if (Input.GetKeyDown("space"))
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
        if(isWallTouch && !IsGrounded() && dirX != 0)
        {
            isWallSliding = true;
        }
        else{
            isWallSliding = false;
        }

        if (isWallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        /*private void WallSlide()
        {
            if (IsWalled() && !IsGrounded())
            {
                isWallSliding = true;
                Debug.Log(isWallSliding);
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
            }
            else
            {
                isWallSliding = false;
            }
        }*/

        if (isWallJumping)
        {
            rb.velocity = new Vector2(-dirX * wallJumpingForce.x, wallJumpingForce.y);
            CreateDust();
            Invoke(nameof(StopWallJumping), wallJumpingDuration);
            /*WallJump();*/
        }

        



        Flip();
        UpdateAnimationState();
    }



    void Flip()
    {
        
        if (dirX < -0.01f) transform.localScale = new Vector3(-1, 1, 1);
        if (dirX > 0.01f) transform.localScale = new Vector3(1, 1, 1);
    }





   /* private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingDuration;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }
        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(-wallJumpingDirection * wallJumpingForce.x, wallJumpingForce.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x = -1f;
                transform.localScale = localScale;
            }
        }
        Invoke(nameof(StopWallJumping), wallJumpingDuration);
    }*/


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
            /*sprite.flipX = false;*/
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            /*sprite.flipX = true;*/
        }
        else
        {
            if (!Input.GetButtonDown("Prout"))
            {
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
        else if (rb.velocity.y < -.1f || isWallSliding)
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

