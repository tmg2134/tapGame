using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

  public float moveSpeed;
  public int damage;
  public int hitpoints;
  public float interactDistance;
  public float bounceDist;
  public GameObject thePlayer;
  public float bounceFramesTotal;

  float bounceFramesLeft;

  Rigidbody2D rigidBody;
  BoxCollider2D collider;


  // Animator enemyAnimator;
  // CharacterController controller;

	// initialization
	void Start () {
		collider = GetComponentInChildren<BoxCollider2D>();
    rigidBody = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
	  float theMag = (transform.position - thePlayer.transform.position ).magnitude;
    if (theMag <= interactDistance && bounceFramesLeft <= 0){
      collider.isTrigger = true;
    } else {
      collider.isTrigger = false;
      if(bounceFramesLeft > 0){
        // Debug.Log(rigidBody.velocity);
        transform.position += Vector3.right * bounceDist * Time.deltaTime;
        bounceFramesLeft -= 1;
      }
    }

    if(bounceFramesLeft <= 0){
      transform.position += Vector3.right * -1 * moveSpeed * Time.deltaTime;
    }
    
	}

  public void Bounce(){
    // transform.position += Vector3.right * bounceDist;
    bounceFramesLeft = bounceFramesTotal;
    collider.isTrigger = false;
    // rigidBody.AddForce(Vector3.right * bounceDist);
  }

  public int getDamage(){
    return damage;
  }

}
