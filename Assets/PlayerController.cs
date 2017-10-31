using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	Rigidbody2D rb;
	float max_jump_charge=10f;
	float min_jump_charge=3f;
	float jump_charge;
	public float speed = 10f;
	private bool isGrounded = false;

	Animator anim;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		jump_charge = min_jump_charge;
	}
	
	// Update is called once per frame
	void Update () {
		if(isGrounded){	//charging the jump
			if(Input.GetButton("Jump") && (jump_charge <= max_jump_charge) ) {
				jump_charge += Time.deltaTime * 5f;
				anim.SetBool("charge_jump", true);
				anim.speed = 1.25f * jump_charge/10f;
			}

			if(Input.GetButtonUp("Jump")){	//jumping
				print(jump_charge);
				rb.velocity += new Vector2(0f,jump_charge);
				jump_charge = min_jump_charge;
				isGrounded = false;
				anim.SetBool("isGrounded", isGrounded);
				anim.SetBool("charge_jump", false);
				anim.speed = 1;
			}
		}
	}


	void FixedUpdate(){
		float move_h = Input.GetAxis("Horizontal"); //movement
		transform.position += new Vector3(move_h,0f) * Time.fixedDeltaTime * speed;
	}


	void OnCollisionEnter2D(Collision2D col){
		if(col.collider.CompareTag("ground")){	//landing
			isGrounded = true;
			anim.SetBool("isGrounded", isGrounded);
		}
	}

}
