using UnityEngine;
using System.Collections;

public class IntroMenuAnim : MonoBehaviour {

    [SerializeField]
    GameObject target;

    public bool orbit;
	public float delta = 15;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (target != null && orbit)
        {
			transform.RotateAround(target.transform.position, Vector3.down, Time.deltaTime * delta);
        }

	}
}
