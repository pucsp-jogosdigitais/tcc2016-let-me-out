using UnityEngine;
using System.Collections;

public class TesteMorte : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.O)) {
			
			GameObject cabeca = new GameObject("cabeca");
			cabeca.AddComponent<Camera>();
			cabeca.AddComponent<Rigidbody>();
			CapsuleCollider cap=cabeca.AddComponent<CapsuleCollider>();
			cabeca.transform.position=transform.position+Vector3.up;

			cap.height=1.5f;

			cabeca.GetComponent<Rigidbody> ().AddForce (new Vector3(1, 1, 1));
		}

	}
}
