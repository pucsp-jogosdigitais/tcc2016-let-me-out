using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

    private Ray rayToInteract;
    private RaycastHit hitInfo;

    public float rangeInteract = 10f;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        rayToInteract = Camera.current.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        if (Physics.Raycast(rayToInteract, out hitInfo, rangeInteract))
        {

            if (hitInfo.collider.tag == "Item")
            {
                Destroy(hitInfo.collider.gameObject);
            }

        }

    }
}
