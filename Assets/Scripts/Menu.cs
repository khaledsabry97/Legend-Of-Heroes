using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
	[SerializeField] private GameObject Hero;
	[SerializeField] private GameObject Soldier;
	[SerializeField] private GameObject Tanker;


	private Animator HeroAnim;
	private Animator SoldierAnim;
	private Animator TankerAnim;

	// Use this for initialization
	void Start () {
		HeroAnim = Hero.GetComponent<Animator> ();
		SoldierAnim = Soldier.GetComponent<Animator> ();
			TankerAnim = Tanker.GetComponent<Animator>();
			StartCoroutine(someAnimation());
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	IEnumerator someAnimation(){
		yield return new WaitForSeconds (2);
		SoldierAnim.Play ("Walk");
		yield return new WaitForSeconds (2);
		TankerAnim.Play ("Walk");
		yield return new WaitForSeconds (2);

	}

	public void Battle(){
		SceneManager.LoadScene ("Level 1");


	}
	public void Quit(){
		Application.Quit();

	}

	public void EasyMode(){
		GameManager.instance.EasyMode ();

	}

	public void MediumMode(){
		GameManager.instance.MediumMode ();
	}

	public void HardMode(){
		GameManager.instance.HardMode ();

	}


}
