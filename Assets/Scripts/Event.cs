using UnityEngine;
using System.Collections;

public class Event : MonoBehaviour {

    public enum TypeEvent { Default, CustomSound };

    public TypeEvent type;

    public AudioClip audioSource;

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

            switch (type)
            {
                case TypeEvent.Default:
                    EventManager.GetInstance().SetEvent(eventName);
                    break;
                    case TypeEvent.CustomSound:
                    gameObject.AddComponent<AudioSource>().playOnAwake = false;
                    gameObject.GetComponent<AudioSource>().clip = audioSource;
                    gameObject.GetComponent<AudioSource>().Play();

                    break;

            }
        }

        //gameObject.SetActive(false);
    }


    }
