using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class FlashLight : MonoBehaviour
{

    public Light flashLight;
    public LayerMask monsterLayer;  
    private Ray rayToInteract;
    private RaycastHit hitInfo;
    public float rangeInteract = 100f;

    int counter = 0;

    public GameObject menuLanterna;

    bool hasActivate = false;

    // Use this for initialization
    void Start()
    {
        flashLight = gameObject.GetComponent<Light>();
        flashLight.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
		if (!GameInfo.paused && Player.GetInstance().Items.Contains("ativar lanterna") && Main.GetInstance().hasActivate)
        { 

            //if (Input.GetButtonDown("Fire1"))
            if (Input.GetKeyDown(KeyCode.F))
            {
                GameObject.Find("IconeLanterna").GetComponent<Animator>().SetTrigger("desactivate");

                if(!hasActivate)
                {
                    Player.GetInstance().Items.Add("flashlight");
                }

                /*
                    if (!hasActivate)
                    {
                        hasActivate = true;
                        EventManager.GetInstance().SetEvent("activateIconFlashLight");
                    }*/

                flashLight.enabled = !flashLight.enabled;
                //Debug.Log("clicou");

                if (flashLight.enabled)
                {
                    CanvasGroup cg = menuLanterna.GetComponent<CanvasGroup>();
                    cg.alpha = 1f;
                }
                else
                {
                    CanvasGroup cg = menuLanterna.GetComponent<CanvasGroup>();
                    cg.alpha = 0.4f;
                }
            }
        }

        //Getitem();
    }

    public void Getitem()
    {
        //rayToInteract = Camera.current.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        rayToInteract = Player.GetCamera().ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(rayToInteract, out hitInfo, rangeInteract, monsterLayer))
        {
            Debug.Log(hitInfo.transform.gameObject.name);
            if (hitInfo.collider.tag == "BabyMonster")
            {
                if (flashLight.enabled)
                {
                    GameObject playerCamera = GameObject.Find("FirstPersonCharacter");
                    NoiseAndGrain noisePlayer = playerCamera.GetComponent<NoiseAndGrain>();
                    noisePlayer.intensityMultiplier = 0;
                    Debug.Log("bebe monstro");
                    Destroy(hitInfo.collider.gameObject);
                    counter = 0;
                }
                else if (!flashLight.enabled && Monster.GetInstance().InScreen())
                {
                    counter += 1;

                    // if (counter > 1)
                    {
                        Monster.GetInstance().mosterNV.enabled = false;

                        /*Vector3 newPos = new Vector3();
                        newPos.x = Player.GetInstance().transform.position.x - 20;
                        newPos.y = Player.GetInstance().transform.position.y - 20;
                        newPos.z = Player.GetInstance().transform.position.z - 20;

                        Monster.GetInstance().transform.position = newPos;
                        */
                        //Player.GetInstance().Die();
                        Main.GetInstance().GameOver();
                    }
                }
                // Destroy(hitInfo.collider.gameObject);
                //Main.getInstance().spawnTimeMinutes -= 1;
                //Debug.Log(Main.getInstance().spawnTimeMinutes);
            }

        }
    }

}
