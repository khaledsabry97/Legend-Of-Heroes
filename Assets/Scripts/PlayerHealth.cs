using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
	[SerializeField] private float healthPower = 1000;
	[SerializeField] private float timeBetweenHurt = 2f;
	[SerializeField] private Slider healthSlider;
	[SerializeField] private float healthPowerIncrease = 80;

	private float timer = 0;
	private CharacterController character;
	private Animator anim;
	private AudioSource audio;
	private ParticleSystem ps;
	private float maxHealth;
	private bool takeHealth = false;





	public bool TakeHealth{
		get{ return takeHealth; }
		set{ takeHealth = value; }

	}
	// Use this for initialization

	public float HealthPower{
		get{ return healthPower; }
		set{ healthPower = value; }
	}

	public float MaxHealth{
		get{ return maxHealth; }
	}
	void Start () {
		maxHealth = healthPower;
		character = GetComponent<CharacterController> ();
		anim = GetComponent<Animator> ();
		audio = GetComponent<AudioSource> ();
		ps = GetComponentInChildren<ParticleSystem> ();

	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
	
	}



	public void OnTriggerEnter(Collider other){
		if (other.tag == "HealthPowerUp") {
			takeHealth = true;
			IncreaseHealth ();
			GameManager.instance.resettimertogethealth ();
			GameManager.instance.HealthPoint.GetComponent<SpriteRenderer> ().enabled = false;
			GameManager.instance.HealthPoint.GetComponent<SphereCollider> ().enabled = false;
		}


		if (!GameManager.instance.GameOver && timer >= timeBetweenHurt) {
			if (other.tag == "WeaponTanker") {
				timer = 0;
				Hurt (GameManager.instance.TankerAttack);
			} else if (other.tag == "WeaponRanger") {		
				timer = 0;
				Hurt (GameManager.instance.RangerAttack);
			} else if (other.tag == "WeaponSoldier") {
				timer = 0;
				Hurt (GameManager.instance.SoldierAttack);
			}
		}



	}

		
	public void Hurt(float amount)
	{
		ps.Play ();
		audio.PlayOneShot (audio.clip);
		anim.Play ("Hurt");

		if (healthPower - amount <= 0) {
			GameManager.instance.GameOver = true;
			healthPower = 0;
			PlayerKilled ();
		}
		else
		healthPower -= amount;

		healthSlider.value = healthPower;
	}


	public void PlayerKilled ()
		{
		GameManager.instance.endGame (0);
		anim.SetTrigger ("Die");
		character.enabled = false;

		}

	public void IncreaseHealth ()
	{
		if ((healthPower + healthPowerIncrease) > maxHealth) {
			healthPower = maxHealth;
		} else
			healthPower += healthPowerIncrease;

		healthSlider.value = healthPower;
	}
}
