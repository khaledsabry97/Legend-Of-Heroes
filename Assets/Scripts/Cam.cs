using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour {

	void AnimationOver(){
		CamManager.instance.DestoryCam (gameObject);

	}
}
