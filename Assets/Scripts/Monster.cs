using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
using UnityStandardAssets.Characters.FirstPerson;

public class Monster : MonoBehaviour {

    public static Monster that;

    public static Monster getInstance()
    {
        return that;
    }

    public NavMeshAgent mosterNV;

    public bool IsVisibleByPlayer { get; set; }

    //public Transform destinyPosition;

	// Use this for initialization
	void Start () {
        that = this;
	}
	
	// Update is called once per frame
	void Update () {
        mosterNV.destination = Player.getInstance().transform.position;

        Renderer view = gameObject.GetComponent<Renderer>();
        GameObject playerCamera = GameObject.Find("FirstPersonCharacter");
        NoiseAndGrain noisePlayer = playerCamera.GetComponent<NoiseAndGrain>();

        noisePlayer.intensityMultiplier = 3;
        //FirstPersonController fps = Player.getInstance().GetComponent<FirstPersonController>();
        //CharacterController c = Player.getInstance().GetComponent<CharacterController>();

        /*
        if(view.isVisible)
        {
            Debug.Log("entrou");
        } else
        {
            Debug.Log("saiu");
        }*/

        if (view.isVisible)
        {
            IsVisibleByPlayer = true;
            //Debug.Log("Visivel");
            //noisePlayer.intensityMultiplier = 3;
        } else
        {
            IsVisibleByPlayer = false;
            //Debug.Log("Nao visivel");
            //noisePlayer.intensityMultiplier = 0;
        }
    }

    /*
    void OnBecameVisible()
    {
        Debug.Log("entrou");
    }

    void OnBecameInvisible()
    {
        Debug.Log("saiu");
    }*/

    void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Player")
        {
            Player.getInstance().Kill();
        }
    }
}
