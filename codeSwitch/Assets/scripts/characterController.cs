using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterController : MonoBehaviour {

	//Creating variable to hold reference to the player's CharacterController
    CharacterController cc;
    SpriteRenderer spriteRenderer;
    Animator animator;
	public Vector3 startPosition;

	//Creating public variables for the speed, jump height, and gravity of the player
	public float moveSpeed = 65f;
	public float jumpSpeed = 100f;
	public float gravity = -240f;

	//Creating variable to hold the y velocity of the player
	public float yVel = 0f;

	//Creating variable to check if player is jumping
	bool jumping = false;

	// Use this for initialization
	void Start () {

        //setting start position of the player for respawning
        startPosition = this.transform.position;
		//getting reference to the Player's CharacterController
        cc = gameObject.GetComponent<CharacterController>();

        //getting reference to the Player's spriteRenderer
        spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();

        //getting reference to the Player's animator
        animator = gameObject.GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		//Input code
		float hAxis = Input.GetAxis ("Horizontal");
		float vAxis = Input.GetAxis ("Vertical");

		//Creating an empty vector to hold all changes in movement
		//the information in this vector will then be added to the 
		//character controller at the end of update
		Vector3 amountToMove = Vector3.zero;

		//jump code
		//Reset our jump flag if we touch the ground
		if(cc.isGrounded){
			jumping = false;
            yVel = 0;
		}

		if(Input.GetKeyDown(KeyCode.UpArrow) && !jumping){
			yVel = jumpSpeed ;
			jumping = true;
		}

		if (Input.GetKeyUp (KeyCode.UpArrow) && yVel > 0){
			yVel = 0;
		}

		//Gravity Code
		//Apply gravity to our yVel if we are not touching the ground
		if(!cc.isGrounded){
			yVel += gravity * Time.deltaTime;
		}

		//Limit how fast we can fall (terminal velocity)
		yVel = Mathf.Clamp(yVel, gravity, jumpSpeed);

		//Apply the yVelocity to our translation accumulator
		amountToMove.y += yVel;

		//add all the calculations to amountToMove
		amountToMove += Vector3.right * hAxis * moveSpeed;

		//multiply by Time.deltaTime to ensure smooth movement and make movement frame independent
		amountToMove *= Time.deltaTime;

		//add all of the changes in movement to the character controller
		cc.Move (amountToMove);
        
        //set the z position of the player to -1 each update because the game is 2d
        // Vector3 temp = transform.position;
        // temp.z = 1;
        // transform.position = temp;

        //keep track of whether or not playing is walking for animations
        // if (hAxis == 0)
        // {
        //     animator.SetBool("walking", false);
        // }
        // else
        // {
        //     animator.SetBool("walking", true);
        // }

        // if (hAxis < 0)
        // {
        //     spriteRenderer.flipX = true;
        // }
        // if (hAxis > 0) {
        //     spriteRenderer.flipX = false;
        // }
       
    }
}

