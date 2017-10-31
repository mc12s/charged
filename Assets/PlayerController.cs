using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 10f;
	private float max_jump_charge = 15f;
	private float min_jump_charge = 5f;
	private float jump_charge;
	private bool isGrounded = false;
	private bool doJump = false;
	private float move_h;

	Rigidbody2D rb;
	Animator anim;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		jump_charge = min_jump_charge;
	}
	
	// Update is called once per frame
	void Update () {	//no physics, but put inputs here
		
		move_h = Input.GetAxis("Horizontal"); //movement

		if(isGrounded){	//charging the jump
			if(Input.GetButton("Jump") && (jump_charge <= max_jump_charge) ) {
				jump_charge += Time.deltaTime * 5f;

				anim.SetBool("charge_jump", true);	//change state to crouching animation
				anim.speed = 1f * jump_charge/10f; //gradually speed up animation
			}

			if(Input.GetButtonUp("Jump")){	//jumping
				print(jump_charge);
				//rb.velocity += new Vector2(0f,jump_charge);
				doJump = true;
				isGrounded = false;
				anim.SetBool("isGrounded", isGrounded);	
				anim.SetBool("charge_jump", false);
				anim.speed = 1;
			}
		}
	}


	void FixedUpdate(){  //moved all physics into FixedUpdate
		transform.position += new Vector3(move_h,0f) * Time.fixedDeltaTime * speed;

		if(doJump){	//if you pressed jump in update
			rb.velocity += new Vector2(0f, jump_charge);
			doJump = false;
			jump_charge = min_jump_charge;
		}
	}


	void OnCollisionEnter2D(Collision2D col){
		if(col.collider.CompareTag("ground")){	//landing
			isGrounded = true;

			anim.SetBool("isGrounded", isGrounded);	//go back to idle animation
		}
	}

}
