using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characte2DController : MonoBehaviour
{
	private float movementInputDirection;
	
	private int amountOfJumpsLeft;
	
	private bool isFacingRight = true;
	private bool isRunning;
	private bool isGrounded;
	private bool canJump;
	private bool canFlip;
	
	private Rigidbody2D rb;
	private Animator anim;
	
	public int amountOfJumps = 1;
	
	public float movementSpeed = 10.0f;
	public float jumpForce = 16.0f;
	public float groundCheckRadius;
	
	public Transform groundCheck;
	
	public LayerMask whatIsGround;
	
	//Start is called before the first frame update
	
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent <Animator>();
		amountOfJumpsLeft = amountOfJumps;
	}
	
	
	//Update is called once per frame
	
	void Update()
	{
		CheckInput();
		CheckMovementDirection();
		UpdateAnimations();
		checkIfCanJump();
	}
	
	private void FixedUpdate()
	{
		ApplyMovement();
		CheckSurroundings();
	}
	
	private void CheckSurroundings()
	{
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
	}
	
	private void checkIfCanJump()
	{
		if(isGrounded && rb.velocity.y <=0)
		{
			amountOfJumpsLeft = amountOfJumps;
		}
		
		if(amountOfJumpsLeft <=0)
		{
			canJump = false;
		}
		else
		{
			canJump = true;
		}
	}
	
	
	private void CheckMovementDirection()
	{
		if(isFacingRight && movementInputDirection < 0)
		{
			Flip();
		}
		else if(!isFacingRight && movementInputDirection > 0)
		{
			Flip();
		}
		
		if(Mathf.Abs(rb.velocity.x) >=0.01f)
		{
			isRunning = true;
		}
		else
		{
			isRunning = false;
		}
	}
	
	private void UpdateAnimations()
	{
		anim.SetBool("isRunning", isRunning);
		anim.SetBool("isGrounded", isGrounded);
		anim.SetFloat("yVelocity", rb.velocity.y);
	}
	
	private void CheckInput()
	{
		movementInputDirection = Input.GetAxisRaw("Horizontal");
		
		if (Input.GetButtonDown("Jump"))
		{
			Jump();
		}
	}
	
	private void Jump ()
	
	{
		if(canJump)
		{
			rb.velocity = new Vector2(rb.velocity.x, jumpForce);
			amountOfJumpsLeft--;
		}
		
	}
	
	private void ApplyMovement()
	{
		rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y);
	}
	
	public void DisableFlip()
	{
		canFlip = false;
	}
	
	
	public void EnableFlip()
	{
		canFlip = true;
	}
	
	private void Flip()
	{
		isFacingRight = !isFacingRight;
		transform.Rotate(0.0f, 180.0f, 0.0f);
	}
	
	
	private void OnDrawGizmos ()
	{
		Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
	}
	
}