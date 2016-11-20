using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections.Generic;
using System;

public class Main : MonoBehaviour
{
    public bool isMenu = true;

    public GameObject babyMonster;
    GameObject currGameMonster;

    static Main instance;

    public GameObject menuCelular;
    public GameObject menu;
    public GameObject menuPrincipal;
    public GameObject menuItens;
    public GameObject menuConfig;
    public GameObject menuSair;
    public bool active = false;
    public Dropdown dropDownResolucoes;
    public Dropdown dropDownQualidade;
    public Slider sliderMouseSensivity;
    public Slider sliderEfeitosSonoros;
    public GameObject brightness;

    public GameObject itemTemplate;

    public GameObject gameOverImage;
    float alphaGameOver = 0;

    public Animator animSmartPhone;

    public Texture2D cursor;

    float sensivityMouse = 2;
    float prevPosition = 0;

    string[] test = new string[] { "Muito baixa", "Baixa", "Normal", "Alta", "Muito Alta" };

    public bool hasActivate;
    public bool hasDesactivate;

    public static Main GetInstance()
    {
        return instance;
    }

    // Use this for initialization
    void Start()
    {
        GameObject.Find("CameraLockCameraPicture1").GetComponent<Camera>().enabled = false;

        //Cursor.visible = true;
        instance = this;

        if (isMenu)
        {
            Player.GetCamera().enabled = false;
        }

        if (!isMenu)
        {
            gameOverImage.GetComponent<CanvasRenderer>().SetAlpha(alphaGameOver);

            GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
            
            //DesativarItems(Constants.PictureP1Item, items);
            DesativarItems(Constants.PictureP2Item, items);
            DesativarItems(Constants.PictureP3Item, items);
            DesativarItems(Constants.PictureP4Item, items);
        }

        InitialConfig.Configure();

        sensivityMouse = GameInfo.mouseSensivity;
        sliderMouseSensivity.value = sensivityMouse / 10;

        AudioListener.volume = GameInfo.volumeEffects;
        sliderEfeitosSonoros.value = GameInfo.volumeEffects;

        brightness.GetComponent<Image>().color = new Color(0, 0, 0, GameInfo.brightness);

        if (!isMenu)
        {
            Player.GetInstance().GetComponent<FirstPersonController>().ChangeMouseSensitivity(sensivityMouse, sensivityMouse);
        }

        if (!isMenu)
        {
            UnityStandardAssets.ImageEffects.Blur blur = Player.GetCamera().GetComponent<UnityStandardAssets.ImageEffects.Blur>();


            blur.iterations = 0;
            blur.blurSpread = 0;
            blur.iterations = 3;
            blur.enabled = false;
        }

        if (!isMenu)
        {
            Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
            Cursor.visible = false;
        }

        Resolution[] resolutions = Screen.resolutions;
        foreach (Resolution res in resolutions)
        {
            if (res.height >= 960)
            {
                Dropdown.OptionData currRes = new Dropdown.OptionData();
                currRes.text = res.width + "x" + res.height;

                dropDownResolucoes.options.Add(currRes);
            }
        }

        if (dropDownResolucoes.options.Count > 0)
        {
            dropDownResolucoes.captionText.text = dropDownResolucoes.options[0].text;
        }
        else
        {
            Debug.Log("Mudança de resolução não disponível dentro do Editor da Unity");
        }

        string[] qualitys = QualitySettings.names;
        foreach (Resolution res in resolutions)
        {
            if (res.height >= 960)
            {
                Dropdown.OptionData currRes = new Dropdown.OptionData();
                currRes.text = res.width + "x" + res.height;

                dropDownResolucoes.options.Add(currRes);
            }
        }

        /*
        foreach(string name in QualitySettings.names) {
            Dropdown.OptionData currRes = new Dropdown.OptionData ();
            currRes.text = name;

            dropDownQualidade.options.Add (currRes);
        }*/

        for (int i = 0; i < test.Length; i++)
        {
            Dropdown.OptionData currRes = new Dropdown.OptionData();
            currRes.text = test[i];

            dropDownQualidade.options.Add(currRes);
        }

        dropDownQualidade.captionText.text = dropDownQualidade.options[0].text;
    }

    private void DesativarItems(string itemCod, GameObject[] itemsGO)
    {
        foreach (GameObject itemGO in itemsGO)
        {
            Item item = itemGO.GetComponent<Item>();

            if (item != null)
            {
                if(item.CodItem.Contains(itemCod)) {
                    item.Hide();
                    return;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            EventManager.GetInstance().SetEvent("activatePart3");
        }

        if (GameInfo.gameOver)
        {
            alphaGameOver += 0.10f;
            gameOverImage.GetComponent<CanvasRenderer>().SetAlpha(alphaGameOver);
        }


        UnityStandardAssets.ImageEffects.Blur blur = Player.GetCamera().GetComponent<UnityStandardAssets.ImageEffects.Blur>();

        if (Input.GetKeyDown(KeyCode.C) && Player.GetInstance().Items.Contains(Constants.PhoneItem))
        {
            

            active = !active;
            //menu.SetActive(active);
            Cursor.visible = active;

            if (active)
            {
                if(!hasActivate)
                {
                    GameObject.Find("IconeSmartphone").GetComponent<Animator>().SetTrigger("desactivate");

                    hasActivate = true;
                }

                menu.SetActive(active);
                BackMenuPrincipal();
                blur.iterations = 5;
                blur.blurSpread = 0.6F;
                blur.enabled = true;

                if (!isMenu)
                {
                    Player.GetInstance().GetComponent<FirstPersonController>().ChangeMouseSensitivity(0, 0);
                    Player.GetInstance().GetComponent<FirstPersonController>().enabled = false;
                }
                //Player.GetInstance().GetComponent<MouseLook>().XSensitivity = 0;
                //Player.GetInstance().GetComponent<MouseLook>().YSensitivity = 0;
                GameInfo.paused = true;
                CanvasGroup cg = menuCelular.GetComponent<CanvasGroup>();
                cg.alpha = 1f;
                animSmartPhone.SetTrigger("bounce");
            }
            else
            {
                if(!hasDesactivate)
                {
                    hasDesactivate = true;
                    Player.GetInstance().Items.Add("Ativou celular");
                    //Invoke("ActivateFlashLightIcon", 0.8f);
                }    

                    animSmartPhone.SetTrigger("bounceOut");
                StartCoroutine("ExitSmartPhone");
            }
        }
    }

    void ActivateFlashLightIcon()
    {
        EventManager.GetInstance().SetEvent("activateIconFlashLight");
    }

    void OnApplicationFocus(bool focusStatus)
    {
        if (!isMenu)
        {
            Cursor.visible = false;
        }
    }

    public void GameOver()
    {
        if (!GameInfo.gameOver)
        {


            //Reinit();
            StartCoroutine("Reinit");
        }
    }

    public IEnumerator Reinit()
    {
        yield return new WaitForSeconds(1);
        GameInfo.gameOver = true;
        yield return new WaitForSeconds(5);
        GameInfo.gameOver = false;
        Application.LoadLevel(Application.loadedLevel);
    }

    public IEnumerator ExitSmartPhone()
    {
        UnityStandardAssets.ImageEffects.Blur blur = Player.GetCamera().GetComponent<UnityStandardAssets.ImageEffects.Blur>();

        yield return new WaitForSeconds(0.8f);

        blur.iterations = 0;
        blur.blurSpread = 0;
        blur.enabled = false;

        Debug.Log(sensivityMouse);

        Player.GetInstance().GetComponent<FirstPersonController>().ChangeMouseSensitivity(sensivityMouse, sensivityMouse);
        Player.GetInstance().GetComponent<FirstPersonController>().enabled = true;

        CanvasGroup cg = menuCelular.GetComponent<CanvasGroup>();
        cg.alpha = 0.4f;

        GameInfo.paused = false;
        menu.SetActive(false);
    }

    public void ActiveMenuItens()
    {
        Debug.Log("clicou");
        DesactiveMenu();
        menuItens.SetActive(true);
        GenerateItems();
    }

    void GenerateItems()
    {
        List<string> items = Player.GetInstance().Items;
        Vector2 originalPos = new Vector2(-100, 160);

        for (int i = 0; i < items.Count; i++)
        {
            Vector2 newPos = new Vector2(originalPos.x, originalPos.y + (i * -120));

            GameObject currItem = (GameObject)Instantiate(itemTemplate, menuItens.transform.position, new Quaternion(0, 0, 0, 0));
            currItem.GetComponentInChildren<Text>().text = items[i];

            currItem.transform.SetParent(menuItens.transform, false);
            currItem.GetComponent<RectTransform>().transform.localPosition = newPos;
            //Debug.Log ();

            //currItem.
            //currItem.transform.localPosition = new Vector2(0, 0);


            //currItem.transform.parent = menuItens.transform;
        }

        /*
        foreach(string item in items)
        {
            Debug.Log(item);
        }*/
    }

    /*
    void GenerateItems()
    {
        List<string> items = Player.GetInstance().Items;

        for (int i = 0; i < items.Count; i++)
        {
            GameObject currItem = (GameObject)Instantiate(itemTemplate, menuItens.transform.position, new Quaternion(0, 0, 0, 0));
            currItem.GetComponentInChildren<Text>().text = items[i];

            currItem.GetComponent<RectTransform>().transform.position = new Vector2(0, 0);

            currItem.transform.parent = menuItens.transform;
        }

        /*
        foreach(string item in items)
        {
            Debug.Log(item);
        }
    }*/

    public void ActiveMenuConfig()
    {
        DesactiveMenu();
        menuConfig.SetActive(true);
    }

    public void BackMenuPrincipal()
    {
        DesactiveMenu();
        menuPrincipal.SetActive(true);
    }

    public void BackMenuSair()
    {
        DesactiveMenu();
        menuSair.SetActive(true);

        StartCoroutine("Sair");
    }

    public IEnumerator Sair()
    {
        yield return new WaitForSeconds(2);
        Application.Quit();
    }

    public void DesactiveMenu()
    {
        menuPrincipal.SetActive(false);
        menuItens.SetActive(false);
        menuConfig.SetActive(false);
        menuSair.SetActive(false);
    }

    public void ChangeResolution()
    {
        string[] resolucao = dropDownResolucoes.options[dropDownResolucoes.value].text.Split('x');

        int width = int.Parse(resolucao[0]);
        int height = int.Parse(resolucao[1]);

        Screen.SetResolution(width, height, Screen.fullScreen);
    }

    public void ChangeQuality()
    {
        QualitySettings.SetQualityLevel(dropDownQualidade.value);
    }

    public void ChangeVoluneEffects()
    {
        //AudioListener.volume = 0;
        AudioListener.volume = sliderEfeitosSonoros.value;
        GameInfo.volumeEffects = sliderEfeitosSonoros.value;

        PlayerPrefs.SetFloat("volumeEffects", sliderEfeitosSonoros.value);
        PlayerPrefs.Save();
    }

    public void ChangeSensitivity()
    {
        PlayerPrefs.SetFloat("mouseSensivity", sliderMouseSensivity.value * 10);
        PlayerPrefs.Save();
        sensivityMouse = sliderMouseSensivity.value * 10;

        //Player.GetInstance().GetComponent<FirstPersonController>().MouseXSensitivity = sensivity;
        //Player.GetInstance().GetComponent<FirstPersonController>().MouseYSensitivity = sensivity;
    }

    public void ChangeBrightness(Slider slider)
    {

        //brightness.GetComponent<Image>().color = Color.black;

        //Debug.Log((byte)(slider.value));

        float sliderValue = slider.value;

        //brightness.GetComponent<Image>().color = new Color32(sliderValue, sliderValue, sliderValue, alpha);
        brightness.GetComponent<Image>().color = new Color(0, 0, 0, slider.value - 0.4f);

        Debug.Log(slider.value - 0.4f);
    }


    public void Opcoes()
    {
        active = !active;
        Cursor.visible = active;

        if (active)
        {
            menu.SetActive(active);
            BackMenuPrincipal();

            if (isMenu)
            {
                GameObject btnItems = GameObject.Find("ButtonItens");
                GameObject btnMensagem = GameObject.Find("ButtonMensagem");
                GameObject btnConfig = GameObject.Find("ButtonConfiguracoes");
                GameObject btnQuit = GameObject.Find("ButtonSair");

                btnConfig.GetComponent<RectTransform>().position = btnItems.GetComponent<RectTransform>().position;

                //btnConfig.transform.position = btnItems.transform.position;
                //btnQuit.transform.position = btnMensagem.transform.position;

                btnItems.SetActive(false);
                btnMensagem.SetActive(false);
                btnQuit.SetActive(false);
            }

            if (!isMenu)
            {
                Player.GetInstance().GetComponent<FirstPersonController>().ChangeMouseSensitivity(0, 0);
                Player.GetInstance().GetComponent<FirstPersonController>().enabled = false;
            }
            GameInfo.paused = true;
            CanvasGroup cg = menuCelular.GetComponent<CanvasGroup>();
            cg.alpha = 1f;
            animSmartPhone.SetTrigger("bounce");
        }
    }

    public void Sair2()
    {
        Application.Quit();
    }

    public void EntrarGame()
    {
        Application.LoadLevel("new_Intro");

        Debug.Log("clicou");
    }
    /*
    void OnMouseEnter()
    {
        Debug.Log("entrou");
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }
    void OnMouseExit()
    {
        Debug.Log("saiu");
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
    */
}
