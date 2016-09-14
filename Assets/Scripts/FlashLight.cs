using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class FlashLight : MonoBehaviour {

    public Light flashLight;

    private Ray rayToInteract;
    private RaycastHit hitInfo;
    public float rangeInteract = 10f;

    int counter = 0;

    // Use this for initialization
    void Start () {
        flashLight = gameObject.GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Fire1"))
        {
            flashLight.enabled = !flashLight.enabled;
            Debug.Log("clicou");
        }

        Getitem();
    }

    public void Getitem()
    {
        //rayToInteract = Camera.current.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        rayToInteract = Player.getCamera().ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        if (Physics.Raycast(rayToInteract, out hitInfo, rangeInteract))
        {
            Debug.Log(hitInfo.collider.tag);

            if (hitInfo.collider.tag == "BabyMonster")
            {
                if(flashLight.enabled)
                {
                    GameObject playerCamera = GameObject.Find("FirstPersonCharacter");
                    NoiseAndGrain noisePlayer = playerCamera.GetComponent<NoiseAndGrain>();
                    noisePlayer.intensityMultiplier = 0;
                    Debug.Log("bebe monstro");
                    Destroy(hitInfo.collider.gameObject);
                    Main.getInstance().counterMinutes = 0;
                    Main.getInstance().counterMinutes = 0;
                    counter = 0;
                }
                else
                {
                    counter += 1;

                    if(counter >  20)
                    {
                        Monster.getInstance().mosterNV.enabled = false;

                        Vector3 newPos = new Vector3();
                        newPos.x = Player.getInstance().transform.position.x - 20;
                        newPos.y = Player.getInstance().transform.position.y - 20;
                        newPos.z = Player.getInstance().transform.position.z - 20;

                        Monster.getInstance().transform.position = newPos;

                        Player.getInstance().Kill();
                    }
                }
                // Destroy(hitInfo.collider.gameObject);
                //Main.getInstance().spawnTimeMinutes -= 1;
                //Debug.Log(Main.getInstance().spawnTimeMinutes);
            }

        }
    }

}
