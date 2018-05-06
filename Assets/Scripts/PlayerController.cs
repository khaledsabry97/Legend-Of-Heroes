using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	[SerializeField] private float moveSpeed = 10f;
	[SerializeField] private float maxMoveSpeed = 20f;
	[SerializeField] private LayerMask layerMask;
	[SerializeField] private int HeroAttack = 10;
	[SerializeField] 	

	private CharacterController characterController;
	private Vector3 currentlooktarget = Vector3.zero;
	private Animator anim;
	private BoxCollider[] weapon;
	private float currentSpeed;
	private GameObject fireTail;
	private ParticleSystem fireTailParticle;
	public float MoveSpeed {
		set{ moveSpeed = value; }
	}

	public float MaxMoveSpeed {
		get{ return maxMoveSpeed; }
		set{ maxMoveSpeed = value; }
	}



	// Use this for initialization
	void Start () {
		currentSpeed = moveSpeed;
		characterController = GetComponent<CharacterController> ();
		anim = GetComponent<Animator> ();
		weapon = GetComponentsInChildren<BoxCollider> ();
		fireTail = GameObject.FindWithTag ("Fire") as GameObject;
		fireTailParticle = fireTail.GetComponent<ParticleSystem> ();
		fireTail.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if(!GameManager.instance.GameOver)
		{
		Vector3 moveDirection = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
		characterController.SimpleMove (moveDirection * moveSpeed);
		if (moveDirection == Vector3.zero)
			anim.SetBool ("IsWalking", false);
		else
			anim.SetBool ("IsWalking", true);


		if (Input.GetMouseButtonDown (0)) {
			GameManager.instance.HeroAttackValue(HeroAttack);
			anim.Play ("DoubleChop");
		}

			else if (Input.GetMouseButtonDown (1)){
				GameManager.instance.HeroAttackValue ((int)(1.5 * HeroAttack));
			anim.Play ("SpritAttack");

		
	}
	}
	}


	void FixedUpdate(){
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		Debug.DrawRay (ray.origin, ray.direction * 500, Color.blue);
		if (Physics.Raycast (ray, out hit, 500, layerMask, QueryTriggerInteraction.Ignore) && !GameManager.instance.GameOver) {
			if (hit.point != currentlooktarget) {
				currentlooktarget = hit.point;
			}

			Vector3 targetPosition = new Vector3 (hit.point.x, transform.position.y, hit.point.z);
			Quaternion rotation = Quaternion.LookRotation (targetPosition - transform.position);
			transform.rotation = Quaternion.Lerp (transform.rotation, rotation, Time.deltaTime * 10f);


		}
	}

	public void HeroBegainAttack(){
		foreach(var weapon in weapon )
			weapon.enabled = true;
	}

	public void HeroEndAttack(){
		foreach(var weapon in weapon )
			weapon.enabled = false;
	}



	public void fireTailPlay(){
		StartCoroutine (fireWithTail ());
	}


	IEnumerator fireWithTail()
	{
		fireTail.SetActive (true);
		moveSpeed = maxMoveSpeed;
		yield return new WaitForSeconds (7);
		var m = fireTailParticle.emission;
		m.enabled = false;
		moveSpeed = currentSpeed;
		yield return new WaitForSeconds (5);
		m.enabled = true;
		fireTail.SetActive (false);





	}


	}
	


