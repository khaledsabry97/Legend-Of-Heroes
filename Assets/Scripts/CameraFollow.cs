using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	[SerializeField] private Transform Target;
	[SerializeField] private float smoothing = 5f;

	Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position - Target.position;
		
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 targetoncamera = Target.position + offset;
		transform.position = Vector3.Lerp (transform.position, targetoncamera, smoothing * Time.deltaTime);
		
	}








}
