using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
using System.Collections.Generic;
using System;

public class Player : MonoBehaviour
{
    static Player instance;

    public Light flashLight;

    Ray rayToInteract;
    RaycastHit hitInfo;
    public float rangeInteract = 10f;

    public GameObject hand;
    public GameObject hairCross;

    List<string> items;

    public static Player GetInstance()
    {
        return instance;
    }

    public static Camera GetCamera()
    {
        return GameObject.Find("FirstPersonCharacter").GetComponent<Camera>();
    }

    public static Transform GetTransform()
    {
        return GameObject.Find("Player").GetComponent<Transform>();
    }

    public static CameraShake GetShake()
    {
        return GameObject.Find("Player").GetComponent<CameraShake>();
    }

    public static VignetteAndChromaticAberration GetVignette()
    {
        return GameObject.Find("FirstPersonCharacter").GetComponent<VignetteAndChromaticAberration>();
    }

    public static MotionBlur GetMotionBlur()
    {
        return GameObject.Find("FirstPersonCharacter").GetComponent<MotionBlur>();
    }

    void Start()
    {
        instance = this;
        items = new List<string>();

        InitAnimViggete();
    }

    private void InitAnimViggete()
    {
        GetVignette().intensity = 1;

        Invoke("ContinueAnimViggete", 0.10f);
    }

    private void ContinueAnimViggete()
    {
        GetVignette().intensity -= 0.01f;

        if(GetVignette().intensity > 0)
        {
            Invoke("ContinueAnimViggete", 0.02f);
        }
    }

    void Update()
    {
        Interact();
    }

    public void Die()
    {
        Main.GetInstance().GameOver();
        //Application.LoadLevel(Application.loadedLevel);
    }

    public void ActivateGrainCamera()
    {
        NoiseAndGrain noisePlayer = Player.GetCamera().GetComponent<NoiseAndGrain>();
        noisePlayer.intensityMultiplier = 3;
    }

    public void DesactivateGrainCamera()
    {
        NoiseAndGrain noisePlayer = Player.GetCamera().GetComponent<NoiseAndGrain>();
        noisePlayer.intensityMultiplier = 0;
    }

    public void Interact()
    {
        rayToInteract = Player.GetCamera().ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        if (Physics.Raycast(rayToInteract, out hitInfo, rangeInteract))
        {
            switch (hitInfo.collider.tag)
            {
                case "InteractItem":
                    ActivateAnimHand();
                    GetInteractItem(hitInfo);
                    break;

                case "Item":
                    if (IsActiveItem(hitInfo))
                    {
                        ActivateAnimHand();
                        GetItem(hitInfo);
                    }
                    break;
                case "Door":
                    ActivateAnimHand();
                    OpenDoor(hitInfo);
                    break;
                case "Fireplace":
                    ActivateAnimHand();
                    Fireplace firePlace = hitInfo.transform.gameObject.GetComponent<Fireplace>();

                    if (Input.GetKeyDown(KeyCode.Space))
                    {

                        if (Player.GetInstance().Items.Contains(Constants.PictureItem))
                        {
                            if (Player.GetInstance().items.Contains(Constants.StockWood))
                            {
                                Player.GetInstance().items.Remove(Constants.StockWood);
                                firePlace.light.intensity = firePlace.lightIntensity;
                            }

                                if (firePlace.light.intensity < firePlace.minlightIntensity)
                            {
                                SubtitleManager.GetInstance().SetText("Recarregue a lareira para queimar o quadro.");
                            }
                            else
                            {
                                SubtitleManager.GetInstance().SetText("Quadro destruído.");
                            }
                        }
                        else
                        {
                            if (Player.GetInstance().items.Contains(Constants.StockWood))
                            {
								firePlace.light.intensity = firePlace.lightIntensity;
								Debug.Log (firePlace.light.intensity);
								Player.GetInstance().items.Remove(Constants.StockWood);	
								SubtitleManager.GetInstance().SetText("Recarregou lareira");
                            }
                            else
                            {
                                SubtitleManager.GetInstance().SetText("Pegue lenha");
                            }
                        }
                    }

                    Debug.Log("Lareira");
                    break;
                default:
                    DesactivateAnimHand();
                    break;
            }
        }
        else
        {
            DesactivateAnimHand();
        }
    }

    void OpenDoor(RaycastHit hitInfo)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DoorWrapper door = hitInfo.collider.gameObject.GetComponent<DoorWrapper>();

            door.OpenDoor();
        }
    }

    bool IsActiveItem(RaycastHit hitInfo)
    {
        bool active = true;

        Item item = hitInfo.collider.gameObject.GetComponent<Item>();

        if (item.isDestroyed)
        {
            active = false;
        }

        return active;
    }

    void GetItem(RaycastHit hitInfo)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Item item = hitInfo.collider.gameObject.GetComponent<Item>();

            Debug.Log(item.CodItem);
            Debug.Log(Constants.PictureItem);

            if(!item.CodItem.Trim().Contains(Constants.PictureItem))
            {
                items.Add(item.CodItem);
                Debug.Log("entrou aqui");
            }
           
            item.Destroy();
            DesactivateAnimHand();
        }
    }

    public void GetInteractItem(RaycastHit hitInfo)
    {
        IItem item = hitInfo.collider.gameObject.GetComponent<IItem>();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            item.Interact();
        }
    }

    void ActivateAnimHand()
    {
        hand.SetActive(true);
        hairCross.SetActive(false);
    }

    void DesactivateAnimHand()
    {
        hand.SetActive(false);
        hairCross.SetActive(true);
    }

    public List<string> Items
    {
        get { return items; }
    }
}
