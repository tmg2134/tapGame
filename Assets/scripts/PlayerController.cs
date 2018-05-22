using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

  float abilitySize;
  public float hitpoints;
  public Slider healthSlider;
  // Animator playerAnimator;
  
  // CharacterController controller;

  List<Ability> playerAbilities;

	// Initialization
	void Start () {
    // controller =  GetComponent<CharacterController>(); 
    // weaponCollider = GetComponentInChildren<CheckWeaponCollision>();
    // playerAnimator = GetComponent<Animator>(); 

    // input not needed? Auto move
    // input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    // inputDir = inputDirut.normalized;

    // targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
    healthSlider.maxValue = hitpoints;

	}
	
	void Update () {

    // List<Touch> touchList = Input.touches;

    // List of touches that will be used for attacks
    List<Touch> touchList = new List<Touch>();

    // Iterate through touches. 
    // If a touch is an Ability, use it, and do not use for attack
    // If a touch clicked a menu item, disable all touch attacks (return empty list) and activate menu

    touchList = checkTouches(Input.touches);

    if(touchList.Count > 0){
      Attack(touchList);
      Debug.Log(touchList);
    } else {
      Defend();
    }

	}

  public void Defend(){
    // Put up shield for defending projectiles and enemy attacks
  }

  public void Attack(List<Touch> attackTouches){
    // playAttack animation based on tap and and number of 
  }


  public void UseAbility(Ability theAbility){
    // get ability used
    // abilityNum = theAbility.whichAbility;
    // do ability stuff
    // allAbilitiesList[abilityNum].playAbility();
  }


  /* 
    Only want to count player's touches if they are not clicking on a menu item or ability
    Check if touching abilites
    Check if touching the menu
    Return the amount of touches not touching these
  */
  public List<Touch> checkTouches(Touch[] touchArray){
    // int fingerCount = 0;
    List<Touch> leftOverTouches = new List<Touch>();
    foreach (Touch touch in touchArray) {
      Debug.Log("testTouch");
      if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled){
        // Check if player has abilities, and if so, iterate
        bool usedTouch = false;
        if(playerAbilities != null){
          foreach (Ability someAbility in playerAbilities){
          // if within bounds of ability position
            // someAbility.useAbility()
            // usedTouch = true
          }
        }
        // foreach (MenuItem mItem in menuOptions)
          // if within bounds
            // goto menu item, disable touch for damage
            // usedTouch = true
        if(usedTouch == false){
          leftOverTouches.Add(touch);
          // fingerCount++;
        }
      }
    } 
    return leftOverTouches;
  }

  public void AddAbility(Vector3 position, float cooldown, int abilityNum){
    Ability newAbility = new Ability(position, cooldown, abilityNum);
    playerAbilities.Add(newAbility);
  }

  public void takeDamage(int damage){
    hitpoints -= damage;
    healthSlider.value = hitpoints;
  }

  void OnTriggerStay2D(Collider2D other) {
    if(other.gameObject.tag == "Enemy"){
      EnemyController enemyController = other.gameObject.GetComponentInParent<EnemyController>();
      enemyController.Bounce();
      takeDamage(enemyController.getDamage());
      Debug.Log(hitpoints);
    }
  }

}

public struct Ability {
  public readonly Vector3 position;
  public readonly float cooldown;
  public readonly int abilityNum;
  public float lastUsed;

  public Ability (Vector3 position, float cooldown, int abilityNum)
  {
    this.position = position;
    this.cooldown = cooldown;
    this.abilityNum = abilityNum;
    lastUsed = 0;
  }

  public void useAbility(float frame){
    if(lastUsed + cooldown > frame){
      lastUsed = frame;
      // Do Ability stuff
    } else {
      Debug.Log("Still on cooldown");
    }
    
  }

}
