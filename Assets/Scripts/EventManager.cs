using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Util;

public class EventManager : MonoBehaviour {

    static EventManager instance;

    public static EventManager GetInstance()
    {
        return instance;
    }

    // Use this for initialization
	void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetEvent(string eventName)
    {
        switch(eventName)
        {
		case "espacoParaInteragir":
			SubtitleManager.GetInstance ().SetText ("Pegue o celular.");
			break;

		case "espacoParaInteragir2":
			SubtitleManager.GetInstance ().SetText ("[ESPACO] para interagir.");
			break;

            case "desactivateAllCameras":
                DesactivateAllCameras();
                break;

            case "lockDoors":
                LockDoors();
                break;

            case "activateIconFlashLight":
                ActivateIconFlash();
        break;

            case "activateIconSmartPhone":
                ActivateIconSmartPhone();
                break;

		case "initialEvent":

            Invoke ("InitialEvent", 3);
            Invoke("ActivateIconFlash", 3);
            Invoke ("BabyCry", 5);
			Invoke ("ActivateBaby", 6);

			//Monster.GetInstance().currActionState = Monster.MonsterActionState.Rest;

                break;

            case "lockCameraPicture1":
                FadeInScreen();
                Invoke("ActivateCameraEventPicture", 1.2f);
                Invoke("FadeOutScreen", 1.8f);

                Invoke("FadeInScreen", 4.8f);
                Invoke("FadeOutScreen", 5.5f);
                Invoke("DesactivateCameraEventPicture", 5.5f);
                LockDoors();

                break;

		case "activatePart2":

                ActivatePartPicture(Constants.PictureP2Item);
               
                HelperUtil.FindGameObject(GameObject.Find("Eventos"), "Item2").SetActive(true);

                break;

		case "activateAnimPart2":

				Debug.Log("anim porta 2");
                HelperUtil.FindGameObject(GameObject.Find("Quadros de Enfeite"), "Q01").GetComponent<Animator>().SetTrigger("activate");
                break;

            case "activatePart3":
                Monster.GetInstance().CancelAttack();
                Monster.GetInstance().currActionState = Monster.MonsterActionState.Rest;
                Monster.GetInstance().SetVisibility(true);
                Monster.GetInstance().mosterNV.enabled = true;

				Monster.GetInstance().gameObject.transform.position = new Vector3(270.67f, 4.449f, 237.2929f);
                //Monster.GetInstance().gameObject.transform.position = new Vector3(271.5951f, 4.449f, 237.2929f);
                Monster.GetInstance().mosterNV.destination = GameObject.Find("AlvoBebe").transform.position;

                ActivatePartPicture(Constants.PictureP3Item);
                GameObject.Find("PortaChave").GetComponent<DoorWrapper>().typeAnim = DoorWrapper.DoorAnim.Locked;

                FadeInScreen();
                Invoke("ActivateCameraOpenRoom", 1.2f);
                Invoke("FadeOutScreen", 2);

                Invoke("OpenClosedRoom", 1.4f);

                Invoke("FadeInScreen", 4.8f);
                Invoke("FadeOutScreen", 5.5f);
                Invoke("DesactivateCameraOpenRoom", 5.5f);

                //FadeInScreen();

                break;

            case "activatePart4":

                ActivatePartPicture(Constants.PictureP4Item);
                GameObject.Find("PortaEscritorio").GetComponent<DoorWrapper>().typeAnim = DoorWrapper.DoorAnim.Locked;

                break;


            case "babyPersecution":

                //Monster.GetInstance().currActionState = Monster.MonsterActionState.Rest;
                Monster.GetInstance().PersecutionMode();
                Player.GetMotionBlur().blurAmount = 0.6f;
                break;

            case "babyRest":
                Monster.GetInstance().CancelAttack();
                Monster.GetInstance().currActionState = Monster.MonsterActionState.Rest;

                GameObject.Find("portaFinal").GetComponent<Animator>().SetTrigger("open2");

				HelperUtil.FindGameObject (GameObject.Find ("Iluminação"), "luzParte3").GetComponentInChildren<Light> ().range = 4;

                FadeInScreen();
                Invoke("FadeOutScreen", 1.8f);

                Invoke("ActivateCameraRoomBaby", 1.4f);

                Invoke("FadeInScreen", 4.8f);
                Invoke("FadeOutScreen", 5.5f);

                Invoke("DesactivateCameraRoomBaby", 5.5f);

                break;

            case "gameOver":

                FadeInScreen();
                Invoke("FadeOutScreen", 1.8f);

                Invoke("ActivateSoundBaby", 2);

                Invoke("ActivateCameraGameOver", 1.4f);

                Invoke("FadeInScreen", 4.8f);
                Invoke("ActivateSoundWoman", 7.2f);
                //Invoke("FadeOutScreen", 5.5f);

                break;
        }
    }

    private void ActivatePartPicture(string itemCod)
    {
        GameObject[] itemsGO = GameObject.FindGameObjectsWithTag("Item");

        foreach (GameObject itemGO in itemsGO)
        {
            Item item = itemGO.GetComponent<Item>();

            if (item != null)
            {
                if (item.CodItem.Contains(itemCod))
                {
                    if(item.hide)
                    {
                        item.Show();
                    }
                    return;
                }
            }
        }
    }

    private void ActivateIconFlash()
    {
        GameObject.Find("IconeLanterna").GetComponent<CanvasGroup>().alpha = 0;
        GameObject.Find("IconeLanterna").GetComponent<Animator>().SetTrigger("activate");

        Invoke("ContinueIconFlash", 0.10f);
    }

    private void ContinueIconFlash()
    {
        GameObject.Find("IconeLanterna").GetComponent<CanvasGroup>().alpha += 0.01f;
        
        if (GameObject.Find("IconeLanterna").GetComponent<CanvasGroup>().alpha < 0.5)
        {
            Invoke("ContinueIconFlash", 0.10f);
        }
    }

    private void ActivateIconSmartPhone()
    {
        GameObject.Find("IconeSmartphone").GetComponent<CanvasGroup>().alpha = 0;
        GameObject.Find("IconeSmartphone").GetComponent<Animator>().SetTrigger("activate");

        Invoke("ContinueIconSmartPhone", 0.10f);
    }

    private void ContinueIconSmartPhone()
    {
        GameObject.Find("IconeSmartphone").GetComponent<CanvasGroup>().alpha += 0.01f;

        if (GameObject.Find("IconeSmartphone").GetComponent<CanvasGroup>().alpha < 0.5)
        {
            Invoke("ContinueIconSmartPhone", 0.10f);
        }
    }

    private void FadeInScreen()
    {
        GameObject fadeImage = GameObject.Find("FadeImage");

        fadeImage.GetComponent<CanvasGroup>().alpha = 0;

        ContinueFadeInScreen();
    }

    private void ContinueFadeInScreen()
    {
        GameObject fadeImage = GameObject.Find("FadeImage");
        fadeImage.GetComponent<CanvasGroup>().alpha += 0.10f;

        if (fadeImage.GetComponent<CanvasGroup>().alpha < 0.99)
        {
            Invoke("ContinueFadeInScreen", 0.04f);
        }
    }

    private void FadeOutScreen()
    {
        GameObject fadeImage = GameObject.Find("FadeImage");
        fadeImage.GetComponent<CanvasGroup>().alpha = 1;

        ContinueFadeOutScreen();
    }

    private void ContinueFadeOutScreen()
    {
        GameObject fadeImage = GameObject.Find("FadeImage");
        fadeImage.GetComponent<CanvasGroup>().alpha -= 0.10f;

        if (fadeImage.GetComponent<CanvasGroup>().alpha > 0)
        {
            Invoke("ContinueFadeOutScreen", 0.04f);
        }
    }

    private void ActivatePlayerCamera()
    {
        Player.GetCamera().enabled = true;
        HelperUtil.SetVisibility(Player.GetInstance().gameObject, true);
    }

    private void DesactivatePlayerCamera()
    {
        Player.GetCamera().enabled = false;
        HelperUtil.SetVisibility(Player.GetInstance().gameObject, false);
    }

    private void ActivateCameraEventPicture()
    {
        HelperUtil.SetVisibility(Player.GetInstance().gameObject, false);
        
        GameObject.Find("CameraLockCameraPicture1").GetComponent<Camera>().enabled = true;
        GameObject.Find("CameraLockCameraPicture1").GetComponent<Animator>().SetTrigger("activate");
    }

    private void ActivateCameraOpenRoom()
    {
        HelperUtil.SetVisibility(Player.GetInstance().gameObject, false);

        GameObject.Find("CameraLockClosedRoom").GetComponent<Camera>().enabled = true;
        GameObject.Find("CameraLockClosedRoom").GetComponent<Animator>().SetTrigger("activate");
    }

    private void ActivateCameraRoomBaby()
    {
        HelperUtil.SetVisibility(Player.GetInstance().gameObject, false);
        GameObject.Find("CameraLockOpenDoor").GetComponent<Camera>().enabled = true;
        GameObject.Find("CameraLockOpenDoor").GetComponent<Animator>().SetTrigger("activate");
    }

    private void DesactivateCameraRoomBaby()
    {
        HelperUtil.SetVisibility(Player.GetInstance().gameObject, true);
        GameObject.Find("CameraLockOpenDoor").GetComponent<Animator>().SetTrigger("activate");
        GameObject.Find("CameraLockOpenDoor").GetComponent<Camera>().enabled = false;
    }

    private void ActivateCameraGameOver()
    {
        HelperUtil.SetVisibility(Player.GetInstance().gameObject, false);
        GameObject.Find("CameraLockBabyRoom").GetComponent<Camera>().enabled = true;
        GameObject.Find("CameraLockBabyRoom").GetComponent<Animator>().SetTrigger("activate");
    }

    private void ActivateSoundWoman()
    {
        AudioRepository audioRepository = AudioRepository.GetInstance();

        audioRepository.gameObject.GetComponent<AudioSource>().clip = audioRepository.womanAudio;
        audioRepository.gameObject.GetComponent<AudioSource>().Play();
    }

    private void ActivateSoundBaby()
    {
        AudioRepository audioRepository = AudioRepository.GetInstance();

        audioRepository.gameObject.GetComponent<AudioSource>().clip = audioRepository.babyAudio2;
        audioRepository.gameObject.GetComponent<AudioSource>().Play();
    }

    private void DesactivateCameraEventPicture()
    {
        HelperUtil.SetVisibility(Player.GetInstance().gameObject, true);
        GameObject.Find("CameraLockCameraPicture1").GetComponent<Camera>().enabled = false;
    }

    private void DesactivateCameraOpenRoom()
    {
        Monster.GetInstance().currActionState = Monster.MonsterActionState.Spawn;
        Monster.GetInstance().SetVisibility(false);
        Monster.GetInstance().mosterNV.enabled = false;

        HelperUtil.SetVisibility(Player.GetInstance().gameObject, true);
        GameObject.Find("CameraLockClosedRoom").GetComponent<Camera>().enabled = false;
    }

    private void OpenClosedRoom()
    {
        GameObject porta = GameObject.Find("PortaChave");

        porta.GetComponent<DoorWrapper>().doorAnim.SetTrigger("open2");
        porta.GetComponent<AudioSource>().playOnAwake = false;
        porta.GetComponent<AudioSource>().clip = AudioRepository.GetInstance().doorOpen;
    }

    private void DesactivateAllCameras()
    {
        GameObject.Find("CameraLockCameraPicture1").GetComponent<Camera>().enabled = false;
        GameObject.Find("CameraLockClosedRoom").GetComponent<Camera>().enabled = false;
    }

    private void InitialEvent()
    {
		Player.GetInstance ().Items.Add ("ativar lanterna");

        GameObject lightsFather = GameObject.Find("Desativaveis");

        Light[] lights = lightsFather.transform.GetComponentsInChildren<Light>();

        foreach (Light light in lights)
        {
            light.range = 0;
        }
    }

    private void BabyCry()
    {
        GameObject event1 = GameObject.Find("Event1");

        event1.GetComponent<AudioSource>().clip = AudioRepository.GetInstance().babyAudio;
        event1.GetComponent<AudioSource>().Play();
    }

    private void ActivateBaby()
    {
        Monster.GetInstance().currActionState = Monster.MonsterActionState.Spawn;
    }

    private void LockDoors()
    {
        DoorWrapper[] doors = GameObject.Find("Portas").GetComponentsInChildren<DoorWrapper>();

        foreach (DoorWrapper door in doors)
        {
			door.typeAnim = DoorWrapper.DoorAnim.Shaked;
			//door.relatedItem = string.Empty;
        }

		GameObject.Find ("PortaBanheiro").GetComponent<DoorWrapper> ().typeAnim = DoorWrapper.DoorAnim.Locked;
    }
}
