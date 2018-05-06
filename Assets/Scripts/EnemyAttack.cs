using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class EnemyAttack : MonoBehaviour {

	[SerializeField] private float enemyRange = 3f;
	[SerializeField] private float timeBetweenAttacks = 1f;
	//[SerializeField] private float attackPower;


	private BoxCollider[] weapon;
	private bool enemyInRange = false;
	private GameObject player;
	private EnemyHealth enemyHealth;
	private Animator anim;


	// Use this for initialization
	void Start () {
		player = GameManager.instance.Player;
		weapon = GetComponentsInChildren<BoxCollider> ();
		anim = GetComponent<Animator> ();
		StartCoroutine (Attack ());
		enemyHealth = GetComponent<EnemyHealth> ();

		
	}
	
	// Update is called once per frame
	void Update () {
		if(enemyHealth.IsAlive){
		isEnemyInRange ();
		if (IsGameOver ()) {
			anim.SetTrigger ("HeroDie");
		}
	}
	}

	public void isEnemyInRange(){
		if (Vector3.Distance (transform.position, player.transform.position) < enemyRange) {
			enemyInRange = true;
		} else {
			enemyInRange = false;
		}


	}

	IEnumerator Attack(){
		
		if (enemyInRange && !GameManager.instance.GameOver&&enemyHealth.IsAlive ) {
			anim.Play ("Attack");
			yield return new WaitForSeconds (timeBetweenAttacks);
		}
		yield return null;
		StartCoroutine (Attack ());

	
	}


	private bool IsGameOver(){
		if(GameManager.instance.GameOver)
			return true;
		else
			return false;
	}

	public void EnemyBegainAttack(){
		foreach(var weapon in weapon )
		weapon.enabled = true;
	}

	public void EnemyEndAttack(){
		foreach(var weapon in weapon )
			weapon.enabled = false;
	}
}
