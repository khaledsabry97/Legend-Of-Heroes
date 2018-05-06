using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CamManager : MonoBehaviour {
	public static CamManager instance = null;
	[SerializeField] List<GameObject> camNum = new List<GameObject> ();



	// Use this for initialization
	void Start () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0))
			DestoryCam (camNum [0]);	
		else if(Input.GetMouseButtonDown (1))
			SceneManager.LoadScene ("GameMenu");
		
	}

	private void openNext(){
		if (camNum.Count > 0)
			camNum [0].SetActive (true);
		else
			SceneManager.LoadScene ("GameMenu");
			
	}


	public void DestoryCam(GameObject cam)
	{
		camNum.RemoveAt (0);
		Destroy(cam);
		openNext ();

	}

}
