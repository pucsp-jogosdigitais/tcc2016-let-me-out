using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Util;
using UnityStandardAssets.Characters.FirstPerson;

public class EventManager : MonoBehaviour
{

    static EventManager instance;

    public static EventManager GetInstance()
    {
        return instance;
    }

    // Use this for initialization
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetEvent(string eventName)
    {
        switch (eventName)
        {
            case "pegueOCelular":
                SubtitleManager.GetInstance().SetText("Pegue o celular");
                break;

            case "espacoParaInteragir":
                SubtitleManager.GetInstance().SetText("Pressione [ESPAÇO] para interagir");
                break;

            case "desactivateAllCameras":
                DesactivateAllCameras();
                break;

            case "shakeDoors":
                ShakeDoors();
                break;

            case "lockDoors":
                LockDoors();

                break;

            case "activateIconFlashLight":
                ActivateIconFlash();
                break;

            case "activateIconSmartPhone":
                GameObject.Find("tutorial").SetActive(false);
                GameObject.Find("tutorial2").SetActive(false);
                ActivateIconSmartPhone();
                break;

            case "initialEvent":

                Invoke("InitialEvent", 3);
                Invoke("ActivateIconFlash", 3);
                Invoke("BabyCry", 5);
                Invoke("ActivateBaby", 6);

                //Monster.GetInstance().currActionState = Monster.MonsterActionState.Rest;

                break;

            case "lockCameraPicture1":
                Player.GetInstance().GetComponent<FirstPersonController>().enabled = false;
                Main.GetInstance().inCutScene = true;

                FadeInScreen();
                Invoke("ActivateCameraEventPicture", 1.2f);
                Invoke("FadeOutScreen", 1.8f);

                Invoke("FadeInScreen", 4.8f);
                Invoke("FadeOutScreen", 5.5f);
                Invoke("DesactivateCameraEventPicture", 5.5f);

                break;

            case "activatePart2":
                ShakeDoors();
                ActivatePartPicture(Constants.PictureP2Item);

                HelperUtil.FindGameObject(GameObject.Find("Eventos"), "Item2").SetActive(true);
                break;

            case "activateAnimPart2":

                //HelperUtil.FindGameObject(GameObject.Find("Quadros de Enfeite"), "Q01").GetComponent<Animator>().SetTrigger("activate");                
                HelperUtil.FindGameObject(GameObject.Find("Quadros de Enfeite"), "Q01").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                HelperUtil.FindGameObject(GameObject.Find("Audio"), "SoundQuadroCaindo").SetActive(true); ;
                break;

            case "activatePart3":
                Player.GetInstance().GetComponent<FirstPersonController>().enabled = false;
                Main.GetInstance().inCutScene = true;

                Monster.GetInstance().CancelAttack();
                Monster.GetInstance().currActionState = Monster.MonsterActionState.Rest;
                Monster.GetInstance().SetVisibility(false);

                FalseMonster.GetInstance().Activate();

                //Monster.GetInstance ().mosterNV.enabled = true;
                //Monster.SetAnimationState (Monster.MonsterAnimation.Crawl);
                //Monster.GetInstance().gameObject.transform.position = new Vector3(269.02f, 4.449f, 237.2929f);
                //Monster.GetInstance().gameObject.transform.position = new Vector3(270.67f, 4.449f, 237.2929f);
                //Monster.GetInstance().mosterNV.destination = GameObject.Find("AlvoBebe").transform.position;

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

            case "audioVidroArranhado":
                Invoke("ActivateSoundVidroArranhado", 0.6f);
                break;

            case "babyPersecution":

                //Monster.GetInstance().currActionState = Monster.MonsterActionState.Rest;
                Monster.GetInstance().PersecutionMode();
                Monster.GetInstanceAudioSource().GetComponent<AudioSource>().Pause();
                Monster.SetAnimationState(Monster.MonsterAnimation.Crawl);
                Player.GetMotionBlur().blurAmount = 0.6f;
                HelperUtil.FindGameObject(GameObject.Find("Audio"), "SoundPersecution").SetActive(true);
                IncreaseSoundPersecution();


                break;

            case "babyRest":
                
            
            Player.GetInstance().GetComponent<FirstPersonController>().enabled = false;
                Main.GetInstance().inCutScene = true;

                Player.GetInstance().GetComponent<FirstPersonController>().ChangeMouseSensitivity(0, 0);

                Monster.GetInstance().CancelAttack();
                Monster.GetInstance().currActionState = Monster.MonsterActionState.Rest;

                GameObject.Find("portaFinal").GetComponent<Animator>().SetTrigger("open2");

                HelperUtil.FindGameObject(GameObject.Find("Iluminação"), "spotPortaBebe").SetActive(true);
                HelperUtil.FindGameObject(GameObject.Find("Iluminação"), "luzParte3").GetComponentInChildren<Light>().range = 4;

                FadeInScreen();
                Invoke("FadeOutScreen", 1.8f);

                Invoke("ActivateCameraRoomBaby", 1.4f);

                Invoke("FadeInScreen", 4.8f);
                Invoke("FadeOutScreen", 5.5f);

                Invoke("DesactivateCameraRoomBaby", 5.5f);

                break;

            case "gameOver":
                Player.GetInstance().GetComponent<FirstPersonController>().enabled = false;
                Main.GetInstance().inCutScene = true;

                FadeInScreen();
                Invoke("FadeOutScreen", 1.8f);

                Invoke("ActivateSoundBaby", 2);

                Invoke("ActivateCameraGameOver", 1.4f);

                Invoke("FadeInScreen", 4.8f);
                Invoke("ActivateSoundWoman", 7.2f);

                Invoke("FadeInGameOver", 10);
                //Invoke("FadeOutScreen", 5.5f);

                break;
        }
    }

    private void FadeInGameOver()
    {
        CanvasGroup cg = GameObject.Find("LetMeOut").GetComponent<CanvasGroup>();

        cg.alpha += 0.10f;

        if (cg.alpha < 0.99f)
        {
            Invoke("FadeInGameOver", 0.10f);
        }
        else
        {
            FadeInTextGameOver();
        }
    }

    private void FadeInTextGameOver()
    {
        CanvasGroup cg = GameObject.Find("LetMeOutText").GetComponent<CanvasGroup>();

        cg.alpha += 0.10f;

        if (cg.alpha < 0.99f)
        {
            Invoke("FadeInTextGameOver", 0.10f);
        }
        else
        {
            Invoke("QuitGame", 0.5f);
        }
    }

    private void QuitGame()
    {
        Debug.Log("Saiu");
        Application.Quit();
    }

    private void IncreaseSoundPersecution()
    {
        GameObject audioGO = HelperUtil.FindGameObject(GameObject.Find("Audio"), "SoundPersecution");
        AudioSource audio = audioGO.GetComponent<AudioSource>();
        audio.volume += 0.01f;

        if (audio.volume < 0.99)
        {
            Invoke("IncreaseSoundPersecution", 0.20f);
        }
    }

    private void ActivateSoundVidroArranhado()
    {
        HelperUtil.FindGameObject(GameObject.Find("Audio"), "SoundVidroArranhado").SetActive(true);
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
                    if (item.hide)
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
        //HelperUtil.SetVisibility(Player.GetInstance().gameObject, true);
    }

    private void DesactivatePlayerCamera()
    {
        Player.GetCamera().enabled = false;
        HelperUtil.SetVisibility(Player.GetInstance().gameObject, false);
    }

    private void ActivateCameraEventPicture()
    {
        //HelperUtil.SetVisibility(Player.GetInstance().gameObject, false);

        GameObject.Find("CameraLockCameraPicture1").GetComponent<Camera>().enabled = true;
        GameObject.Find("CameraLockCameraPicture1").GetComponent<Animator>().SetTrigger("activate");
        HelperUtil.FindGameObject(GameObject.Find("Audio"), "BackgroundMusic").SetActive(true);

        IncreaseVolumeBackgroundMusic();
        Invoke("PlayWomanEssaMolduraNaoENossa", 1.5f);
    }

    private void IncreaseVolumeBackgroundMusic()
    {
        GameObject audioGO = HelperUtil.FindGameObject(GameObject.Find("Audio"), "BackgroundMusic");
        AudioSource audio = audioGO.GetComponent<AudioSource>();
        audio.volume += 0.01f;

        if (audio.volume < 0.1)
        {
            Invoke("IncreaseVolumeBackgroundMusic", 0.20f);
        }
    }

    private void PlayWomanEssaMolduraNaoENossa()
    {
        HelperUtil.FindGameObject(GameObject.Find("Audio"), "SoundEssaMolduraNaoÉNossa").SetActive(true);
    }

    private void ActivateCameraOpenRoom()
    {
        //HelperUtil.SetVisibility(Player.GetInstance().gameObject, false);

        GameObject.Find("CameraLockClosedRoom").GetComponent<Camera>().enabled = true;
        GameObject.Find("CameraLockClosedRoom").GetComponent<Animator>().SetTrigger("activate");

        Invoke("PlaySoundViolin", 0);
        Invoke("PlaySoundViolin2", 2.2f);
    }

    private void PlaySoundViolin()
    {
        HelperUtil.FindGameObject(GameObject.Find("Audio"), "SoundViolin").SetActive(true);

        Invoke("DecreaseVolumeSoundViolin", 2);
    }

    private void PlaySoundViolin2()
    {
        HelperUtil.FindGameObject(GameObject.Find("Audio"), "SoundViolin2").SetActive(true);

        //Invoke("DecreaseVolumeSoundViolin", 2);
    }

    private void DecreaseVolumeSoundViolin()
    {
        GameObject audioGO = HelperUtil.FindGameObject(GameObject.Find("Audio"), "SoundViolin");
        AudioSource audio = audioGO.GetComponent<AudioSource>();
        audio.volume -= 0.1f;

        if (audio.volume > 0.01)
        {
            Invoke("DecreaseVolumeSoundViolin", 0.20f);
        }
    }

    private void ActivateCameraRoomBaby()
    {
        //HelperUtil.SetVisibility(Player.GetInstance().gameObject, false);
        GameObject.Find("CameraLockOpenDoor").GetComponent<Camera>().enabled = true;
        GameObject.Find("CameraLockOpenDoor").GetComponent<Animator>().SetTrigger("activate");
    }

    private void DesactivateCameraRoomBaby()
    {
        //HelperUtil.SetVisibility(Player.GetInstance().gameObject, true);
        GameObject.Find("CameraLockOpenDoor").GetComponent<Animator>().SetTrigger("activate");
        GameObject.Find("CameraLockOpenDoor").GetComponent<Camera>().enabled = false;
        Player.GetInstance().GetComponent<FirstPersonController>().enabled = true;
        Main.GetInstance().inCutScene = false;
    }

    private void ActivateCameraGameOver()
    {
        //HelperUtil.SetVisibility(Player.GetInstance().gameObject, false);
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
        //HelperUtil.SetVisibility(Player.GetInstance().gameObject, true);
        GameObject.Find("CameraLockCameraPicture1").GetComponent<Camera>().enabled = false;
        Player.GetInstance().GetComponent<FirstPersonController>().enabled = true;
        Main.GetInstance().inCutScene = false;
    }

    private void DesactivateCameraOpenRoom()
    {
        Monster.GetInstance().currActionState = Monster.MonsterActionState.Spawn;
        Monster.GetInstance().SetVisibility(false);
        Monster.GetInstance().mosterNV.enabled = false;
        Monster.SetAnimationState(Monster.MonsterAnimation.Idle1);

        FalseMonster.GetInstance().Desactivate();

        //HelperUtil.SetVisibility(Player.GetInstance().gameObject, true);
        GameObject.Find("CameraLockClosedRoom").GetComponent<Camera>().enabled = false;
        Player.GetInstance().GetComponent<FirstPersonController>().enabled = true;
        Main.GetInstance().inCutScene = false;
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
        Player.GetInstance().Actions.Add(Constants.ActionGetFlashLight);

        GameObject lightsFather = GameObject.Find("Desativaveis");

        Light[] lights = lightsFather.transform.GetComponentsInChildren<Light>();

        foreach (Light light in lights)
        {
            light.range = 0;
        }

        HelperUtil.FindGameObject(GameObject.Find("Audio"), "SoundLightOff").SetActive(true);
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

    private void ShakeDoors()
    {
        DoorWrapper[] doors = GameObject.Find("Portas").GetComponentsInChildren<DoorWrapper>();

        foreach (DoorWrapper door in doors)
        {
            door.typeAnim = DoorWrapper.DoorAnim.Shaked;
            //door.relatedItem = string.Empty;
        }

        GameObject.Find("PortaBanheiro").GetComponent<DoorWrapper>().typeAnim = DoorWrapper.DoorAnim.Locked;
    }

    private void LockDoors()
    {
        DoorWrapper[] doors = GameObject.Find("Portas").GetComponentsInChildren<DoorWrapper>();

        foreach (DoorWrapper door in doors)
        {
            door.typeAnim = DoorWrapper.DoorAnim.Locked;
        }
    }

}
