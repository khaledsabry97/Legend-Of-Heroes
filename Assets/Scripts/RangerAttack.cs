using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerAttack : MonoBehaviour {
		[SerializeField] private float enemyRange = 10f;
		[SerializeField] private float timeBetweenAttacks = 1f;
		[SerializeField] private GameObject arrow;
		private bool enemyInRange = false;
		private GameObject player;
		private EnemyHealth enemyHealth;
		private Animator anim;


		// Use this for initialization
		void Start () {
			player = GameManager.instance.Player;
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
			anim.SetBool ("PlayerInRange",true);
			RangerRotation ();
				enemyInRange = true;
			} else {
			anim.SetBool ("PlayerInRange",false);
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


	private void RangerRotation(){
		Vector3 direction = (player.transform.position -transform.position).normalized;
		Quaternion rotationDirection = Quaternion.LookRotation (direction);
		transform.rotation = Quaternion.Slerp (transform.rotation, rotationDirection, Time.deltaTime * 10);

	}

	void FireArrow(){
		GameObject newArrow = Instantiate (arrow) as GameObject;
		//newArrow.transform.position = GameManager.instance.ArrowPosition.position;
		newArrow.transform.rotation = GameManager.instance.Player.transform.rotation;
		newArrow.GetComponent<Rigidbody> ().velocity = transform.forward * 25f;

	}





		private bool IsGameOver(){
			if(GameManager.instance.GameOver)
				return true;
			else
				return false;
		}




	}
	