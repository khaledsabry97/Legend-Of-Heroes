using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour {
	private Transform Player;
	private NavMeshAgent nav;
	private EnemyHealth enemyHealth;
	private Animator anim;
	public Animator Anim{
		get {
			return anim;
		}
	}
	// Use this for initialization
	void Start () {
		Player = GameManager.instance.Player.transform;
		anim = GetComponent<Animator> ();
		nav = GetComponent<NavMeshAgent> ();
		enemyHealth = GetComponent<EnemyHealth> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!GameManager.instance.GameOver && enemyHealth.IsAlive)
			nav.SetDestination (Player.position);
		else if (!enemyHealth.IsAlive)
			nav.enabled = false;
		else {
			nav.enabled = false;
			anim.SetTrigger ("HeroDie");
		}
		
	}
}
