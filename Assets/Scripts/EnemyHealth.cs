using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyHealth : MonoBehaviour {
	[SerializeField] private float Health;
	[SerializeField] private float timeBetweenHits = .5f;
	[SerializeField] private float disapearSpeed = .5f;


	private Animator anim;
	private NavMeshAgent nav;
	private float timer = 0;
	private AudioSource audio;
	private bool isAlive = true;
	private Rigidbody rigidBody;
	private CapsuleCollider capsuleCollider;
	private bool dissapearEnemy = false;
	private ParticleSystem ps;
	//private GameObject explosion;
	public bool IsAlive {
		get{ return isAlive; }
	}
	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator> ();
		nav = GetComponent<NavMeshAgent> ();
		audio = GetComponent<AudioSource> ();
		rigidBody = GetComponent<Rigidbody> ();
		capsuleCollider = GetComponent<CapsuleCollider> ();
		ps = GetComponentInChildren<ParticleSystem> ();
		GameManager.instance.registerEnemy (this);

	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (dissapearEnemy) {
			transform.Translate (-Vector3.up * disapearSpeed * Time.deltaTime);
		}
	}




	void OnTriggerEnter( Collider other)
	{			
		if (!GameManager.instance.GameOver && timer >= timeBetweenHits) {
			print (Health);
			if (other.tag == "PlayerWeapon") {
				timer = 0;
				EnemyHurt ();
			}
			//print (Health);
		}
	}
		
	private void EnemyHurt ()
	{
		if (Health > 0) {
			ps.Play ();
			audio.PlayOneShot (audio.clip);
			if (Health - GameManager.instance.HeroAttack > 0) {
				Health -= GameManager.instance.HeroAttack;
				anim.Play ("Hurt");
			} else {
				anim.SetTrigger ("EnemyDie");
				killEnemy ();
			}

		}
	}

	void killEnemy (){
		
		GameManager.instance.KilledEnemy (this);
		Health = 0;
		capsuleCollider.enabled = false;
		rigidBody.isKinematic = true;
		nav.enabled = false;
		isAlive = false;
		StartCoroutine (removeEnemy ());


	}

	IEnumerator removeEnemy(){
		yield return new WaitForSeconds (4f);
		GameManager.instance.MakeExplosion (this.transform);
		yield return new WaitForSeconds (0.5f);
		dissapearEnemy = true;
		yield return new WaitForSeconds (2f);
		Destroy (gameObject);


	}
}
