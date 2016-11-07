using UnityEngine;
using System.Collections;

public class Event : MonoBehaviour {

    [SerializeField]
    string eventName;

    bool interact;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Player" && !interact)
        {
            interact = true;
            EventManager.GetInstance().SetEvent(eventName);
        }

        //gameObject.SetActive(false);
    }


    }
