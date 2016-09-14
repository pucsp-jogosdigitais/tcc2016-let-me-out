using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public static Player thatPlayer;

    public Light flashLight;

    private Ray rayToInteract;
    private RaycastHit hitInfo;
    public float rangeInteract = 10f;

    public static Player getInstance()
    {
        return thatPlayer;
    }

    public static Camera getCamera()
    {
        return GameObject.Find("FirstPersonCharacter").GetComponent<Camera>();
    }

	// Use this for initialization
	void Start () {
        thatPlayer = this;
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            Getitem();
        }

        //if (Input.GetButtonDown("Fire1"))
        //{
        //    flashLight.enabled = !flashLight.enabled;
        //}
    }

    public void Kill()
    {
        Application.LoadLevel(Application.loadedLevel);
        //Debug.Log("morreu");
    }

    public void Getitem()
    {
        //rayToInteract = Camera.current.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        rayToInteract = Player.getCamera().ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        if (Physics.Raycast(rayToInteract, out hitInfo, rangeInteract))
        {
            Debug.Log(hitInfo.collider.tag);
                
                if (hitInfo.collider.tag == "Item")
                {
                    Destroy(hitInfo.collider.gameObject);
                    Main.getInstance().spawnTimeMinutes -= 1;
                    //Debug.Log(Main.getInstance().spawnTimeMinutes);
                }

            }
    }
}
