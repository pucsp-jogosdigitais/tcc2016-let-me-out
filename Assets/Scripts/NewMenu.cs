using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using Assets.Scripts.Util;
using System.Linq;
using UnityStandardAssets.Characters.FirstPerson;

public class NewMenu : MonoBehaviour
{
    static NewMenu instance;
    
    public enum MenuContext { Intro, Menu, InGame };
    public MenuContext context;

    public bool isActive;

    public enum Menu { Default, Items, Config, Exit, None };
    public Menu[] menusToShow;
    Menu currMenu = Menu.Default;

    public bool inAnimation;
    public float tickAnim = 0.1f;
    public float switchAnim = 0.8f;

    GameObject smartphone;
    GameObject wrapperSmartphone;
    GameObject firstMenu;
    GameObject exitMenu;
    GameObject exitLoadingMenu;
    GameObject inventoryMenu;
    GameObject configMenu;
    GameObject tutorialMenu;
    GameObject transitionBgConfig;
    GameObject[] itemsDefaultMenu;
	List<GameObject> itemsInventory;

    
    Animator animatorSmartphone;

    GameObject callWait;
    GameObject callDialog;
    public float callInit = 2;
    public float changeImageWait = 2;
    public float goToGame = 1;

    GameObject buttonTutorial;
    GameObject buttonItemsMenu;
    GameObject buttonConfigMenu;
    GameObject buttonExitMenu;
    GameObject dropDownResolucoes;
    GameObject dropDownQualidade;
    GameObject sliderSensivity;
	GameObject sliderSound;
    GameObject buttonResolution;
    Text textResolution;
    
    [SerializeField]
    string windowedMode = "JANELA";
    [SerializeField]
    public string fullScreenMode = "TELA CHEIA";

    GameObject inventoryItemTemplate;
    GameObject inventoryItemPhone;
    GameObject inventoryItemPicture;
    GameObject inventoryItemPictureP1;
    GameObject inventoryItemPictureP2;
    GameObject inventoryItemPictureP3;
    GameObject inventoryItemPictureP4;
    GameObject inventoryItemStockWood;
    GameObject inventoryItemKey;

    public Camera defaultCamera;

    public static NewMenu GetInstance()
    {
        return instance;
    }

    // Use this for initializati1on
    void Start()
    {
        instance = this;

		InitialConfig.RestoreConfiguration();

		//Debug.Log (QualitySettings.GetQualityLevel());

        smartphone = GameObject.Find("SmartPhone");
        wrapperSmartphone = GameObject.Find("WrapperSmartphone");
        firstMenu = GameObject.Find("Principal");

        animatorSmartphone = wrapperSmartphone.GetComponent<Animator>();
        switch (context)
        {
            case MenuContext.InGame:
            case MenuContext.Menu:
                BindMenu();
                FillMenu();
                break;

            case MenuContext.Intro:
                firstMenu.SetActive(false);
                callWait = GameObject.Find("ChamadaRecebida");
                callDialog = GameObject.Find("LigacaoEmAndamento");

                //InitDialog();
                Invoke("InitDialog", callInit);
                break;
        }

        wrapperSmartphone.SetActive(false);
        SetBlur(false);
    }

    // Update is called once per frame
    void Update()
    {
		Player p = Player.GetInstance ();

        if (context == MenuContext.Menu || context == MenuContext.InGame)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!inAnimation)
                {
                    inAnimation = true;
                    isActive = !isActive;
                    //SetBlur(isActive);

                    if (isActive)
                    {
                        wrapperSmartphone.SetActive(true);
						SetBlur(true);
                        animatorSmartphone.SetTrigger("bounce");
                        Invoke("ActivateSmartPhone", 2.1f);

						if (p != null) {
							Player.GetInstance().GetComponent<FirstPersonController>().ChangeMouseSensitivity(0, 0);
						}
                    }
                    else
                    {
                        animatorSmartphone.SetTrigger("bounceOut");
                        Invoke("DesactivateSmartPhone", 2.1f);
						Invoke("ReactivatePlayer", 2.1f);
                    }
                }
            }
        }
    }

	private void ReactivatePlayer()
	{
		Player p = Player.GetInstance ();

		if (p != null) {
			Player.GetInstance().GetComponent<FirstPersonController>().ChangeMouseSensitivity(GameInfo.mouseSensivity * 10, GameInfo.mouseSensivity * 10);
		}

	}

    private void ActivateSmartPhone()
    {
        inAnimation = false;
    }

    private void DesactivateSmartPhone()
    {
        wrapperSmartphone.SetActive(true);
        SetBlur(false);
        inAnimation = false;
    }

    private void SetBlur(bool isActive)
    {
        UnityStandardAssets.ImageEffects.Blur blur = defaultCamera.GetComponent<UnityStandardAssets.ImageEffects.Blur>();

        if (isActive)
        {
            blur.iterations = 3;
        }
        else
        {
            blur.iterations = 0;
        }
    }

    public void InitDialog()
    {
        wrapperSmartphone.SetActive(true);
        animatorSmartphone.SetTrigger("bounce");
        smartphone.GetComponent<AudioSource>().Play();

        Invoke("ChangeImageInitialDialog", changeImageWait);
    }

    public void ChangeImageInitialDialog()
    {
        callWait.SetActive(false);

        Invoke("EndingInitialDialog", 0.1f);
    }

    public void EndingInitialDialog()
    {
        if (smartphone.GetComponent<AudioSource>().isPlaying)
        {
            Invoke("EndingInitialDialog", 0.1f);
        }
        else
        {
            animatorSmartphone.SetTrigger("bounceOut");
            Invoke("GoToGame", goToGame);
        }
    }

    void GoToGame()
    {
        Application.LoadLevel("game");
    }

	void RestorePrevConfig()
	{
		sliderSensivity.GetComponent<Slider>().value = GameInfo.mouseSensivity / 10;
		sliderSound.GetComponent<Slider>().value = GameInfo.volumeEffects;

		dropDownQualidade.GetComponent<Dropdown> ().value = QualitySettings.GetQualityLevel();
	}

    void BindMenu()
    {
        inventoryMenu = GameObject.Find("MenuInventario");

        configMenu = GameObject.Find("MenuConfiguracoes");
        transitionBgConfig = GameObject.Find("EfeitoTransicao");

        buttonConfigMenu = HelperUtil.FindGameObject(smartphone, "BotaoConfiguracoes");
        //buttonConfigMenu.GetComponent<Button>().onClick.AddListener(EnterConfig);

        buttonItemsMenu = HelperUtil.FindGameObject(smartphone, "BotaoItens");
        buttonConfigMenu.GetComponent<Button>().onClick.AddListener(EnterInventory);

        buttonExitMenu = HelperUtil.FindGameObject(smartphone, "BotaoSair");
        buttonExitMenu.GetComponent<Button>().onClick.AddListener(EnterExit);

        exitMenu = HelperUtil.FindGameObject(smartphone, "FundoPadraoSair");
        exitLoadingMenu = HelperUtil.FindGameObject(smartphone, "AnimacaoSair");

        GameObject itemInventory = HelperUtil.FindGameObject(smartphone, "ItemInventario");

        inventoryItemTemplate = itemInventory;
        inventoryItemPhone = itemInventory;
        inventoryItemPicture = HelperUtil.FindGameObject(smartphone, "ItemQuadroCompleto");
        inventoryItemPictureP1 = HelperUtil.FindGameObject(smartphone, "ItemFoto1");
        inventoryItemPictureP2 = HelperUtil.FindGameObject(smartphone, "ItemFoto2");
        inventoryItemPictureP3 = HelperUtil.FindGameObject(smartphone, "ItemFoto3");
        inventoryItemPictureP4 = HelperUtil.FindGameObject(smartphone, "ItemFoto4");
        inventoryItemStockWood = HelperUtil.FindGameObject(smartphone, "ItemTora");
        inventoryItemKey = HelperUtil.FindGameObject(smartphone, "ItemChave");

        buttonResolution = GameObject.Find("BotaoJanela");
        textResolution = buttonResolution.GetComponentInChildren<Text>();

        textResolution.text = fullScreenMode;

        if (!Screen.fullScreen)
        {
            textResolution.text = windowedMode;
        }

        buttonResolution.GetComponent<Button>().onClick.AddListener(delegate
        {
            ChangeScreenSize();
        }
        );

        sliderSensivity = HelperUtil.FindGameObject(smartphone, "SliderSensibilidade");

        sliderSensivity.GetComponent<Slider>().onValueChanged.AddListener(delegate
        {
            OnChangeSensivity();
        });

		sliderSound = HelperUtil.FindGameObject(smartphone, "SliderMusica");

		sliderSound.GetComponent<Slider>().onValueChanged.AddListener(delegate
		{
			AudioListener.volume = sliderSound.GetComponent<Slider>().value;
			GameInfo.volumeEffects = sliderSound.GetComponent<Slider>().value;
			
			PlayerPrefs.SetFloat("volumeEffects", sliderSound.GetComponent<Slider>().value);
			PlayerPrefs.Save();

				Debug.Log(PlayerPrefs.GetFloat("volumeEffects"));
		});


        dropDownResolucoes = HelperUtil.FindGameObject(smartphone, "DropdownResolucoes");
        Dropdown dropDownResolucoesAsDropDown = dropDownResolucoes.GetComponent<Dropdown>();
        Resolution[] resolutions = Screen.resolutions;

        foreach (Resolution res in resolutions)
        {
            if (res.height >= 960)
            {
                Dropdown.OptionData currRes = new Dropdown.OptionData();
                currRes.text = res.width + "x" + res.height;

                dropDownResolucoesAsDropDown.options.Add(currRes);

				if(res.width == Screen.width && res.height == Screen.height)
				{
					dropDownResolucoes.GetComponent<Dropdown>().value = dropDownResolucoes.GetComponent<Dropdown>().options.Count - 1;
				}
            }
        }

        if (dropDownResolucoesAsDropDown.options.Count > 0)
        {
			dropDownResolucoesAsDropDown.captionText.text = dropDownResolucoesAsDropDown.options[dropDownResolucoes.GetComponent<Dropdown>().value].text;

            dropDownResolucoesAsDropDown.onValueChanged.AddListener(delegate {

                string[] resolucao = dropDownResolucoesAsDropDown.options[dropDownResolucoesAsDropDown.value].text.Split('x');

                int width = int.Parse(resolucao[0]);
                int height = int.Parse(resolucao[1]);

                Screen.SetResolution(width, height, Screen.fullScreen);

            });
        }
        else
        {
            Debug.Log("Mudança de resolução não disponível dentro do Editor da Unity");
        }


        dropDownQualidade = HelperUtil.FindGameObject(smartphone, "DropdownQualidade");
        Dropdown dropDownQualidadeAsDropDown = dropDownQualidade.GetComponent<Dropdown>();

        string[] qualitysTranslated = new string[] { "Muito baixa", "Baixa", "Normal", "Alta", "Muito Alta" };

        for (int i = 0; i < qualitysTranslated.Length; i++)
        {
            Dropdown.OptionData currRes = new Dropdown.OptionData();
            currRes.text = qualitysTranslated[i];

            dropDownQualidadeAsDropDown.options.Add(currRes);
        }

        dropDownQualidadeAsDropDown.captionText.text = dropDownQualidadeAsDropDown.options[0].text;

        dropDownQualidadeAsDropDown.onValueChanged.AddListener(delegate
        {
            QualitySettings.SetQualityLevel(dropDownQualidadeAsDropDown.value);
        });

        sliderSensivity = HelperUtil.FindGameObject(smartphone, "SliderSensibilidade");
        Slider sliderSensivityAsSlider = sliderSensivity.GetComponent<Slider>();

        sliderSensivityAsSlider.onValueChanged.AddListener(delegate
        {
            OnChangeSensivity();
        });


        if (context == MenuContext.InGame)
        {
            tutorialMenu = HelperUtil.FindGameObject(smartphone, "Tutorial");
            tutorialMenu.SetActive(true);
            buttonTutorial = HelperUtil.FindGameObject(smartphone, "BotaoFecharTutorial");

            buttonTutorial.GetComponent<Button>().onClick.AddListener(delegate
            {
                InvokeRepeating("GoingOutTutorial", 0, 0.1f);
                //tutorialMenu.SetActive(false);
            });
        }
    }

    void FillMenu()
    {
        Vector3 originalPos = new Vector2(-118, 224);
        Vector2 initialPos = new Vector2(-118, 224);
        int counter = 0;
        int incrementY = 0;


        foreach (Menu menuItem in menusToShow)
        {
            counter += 1;
            GameObject go = null;

            switch (menuItem)
            {
                case Menu.Items:
                    go = buttonItemsMenu;
                    break;

                case Menu.Config:
                    go = buttonConfigMenu;
                    break;

                case Menu.Exit:
                    go = buttonExitMenu;
                    break;
            }

            if (menuItem != Menu.Default && menuItem != Menu.None)
            {
                go = (GameObject)Instantiate(go, new Vector2(0, 0), new Quaternion());

                switch (menuItem)
                {
                    case Menu.Items:
                        go.GetComponent<Button>().onClick.AddListener(EnterInventory);
                        break;
                    case Menu.Config:
                        go.GetComponent<Button>().onClick.AddListener(EnterConfig);
                        break;

                    case Menu.Exit:
                        go.GetComponent<Button>().onClick.AddListener(EnterExit);
                        break;
                }

                go.SetActive(true);
                go.transform.SetParent(inventoryMenu.transform, false);
                //go.transform.position = new Vector2 (initiaPos.x, initiaPos.y);
                go.GetComponent<RectTransform>().transform.localPosition = new Vector2(initialPos.x, initialPos.y);
            }


            initialPos = new Vector2(initialPos.x + 120, initialPos.y);

            if (counter % 3 == 0)
            {
                //counter = 0;
                initialPos = new Vector2(originalPos.x, originalPos.y);
                incrementY -= 120;
                initialPos.y += incrementY;
            }

            itemsDefaultMenu = GameObject.FindGameObjectsWithTag("FirstMenuItem");
        }
    }

    void GoingInMenu()
    {
        //GameObject[] menuItems = GameObject.FindGameObjectsWithTag("FirstMenuItem");

        bool alphaIsOne = false;

        foreach (GameObject menuItem in itemsDefaultMenu)
        {
            menuItem.SetActive(true);

            CanvasGroup cg = menuItem.GetComponent<CanvasGroup>();

            cg.alpha += 0.1f;

            if (cg.alpha > 0.90)
            {
                alphaIsOne = true;
            }

        }

        if (alphaIsOne)
        {
            inAnimation = false;
            currMenu = Menu.Default;
            CancelInvoke("GoingInMenu");

            foreach (GameObject menuItem in itemsDefaultMenu)
            {
                menuItem.GetComponent<Button>().enabled = true;
            }
        }
    }

    void GoingOutTutorial()
    {
        CanvasGroup cg = tutorialMenu.GetComponent<CanvasGroup>();

        cg.alpha -= 0.1f;

        if (cg.alpha < 0.01)
        {
            tutorialMenu.SetActive(false);
            CancelInvoke("GoingOutTutorial");
        }
    }

    void GoingOutFirstMenu()
    {
        //GameObject[] menuItems = GameObject.FindGameObjectsWithTag("FirstMenuItem");
        //List<GameObject> menuItems = HelperUtil.FindGameObjectsWithTag(smartphone, "FirstMenuItem");

        bool alphaIsZero = false;

        foreach (GameObject menuItem in itemsDefaultMenu)
        {
            CanvasGroup cg = menuItem.GetComponent<CanvasGroup>();

            cg.alpha -= 0.1f;

            if (cg.alpha < 0.01)
            {
                alphaIsZero = true;
                //CancelInvoke("GoingOutFirstMenu");
            }
        }

        if (alphaIsZero)
        {
            CancelInvoke("GoingOutFirstMenu");

            foreach (GameObject menuItem in itemsDefaultMenu)
            {
                menuItem.SetActive(false);
                //menuItem.GetComponent<Button>().enabled = false;
            }
        }
    }

    void GoingInInventory()
    {
        //GameObject[] menuItems = GameObject.FindGameObjectsWithTag("ItemInventory");



        foreach (GameObject menuItem in itemsInventory)
        {
            CanvasGroup cg = menuItem.GetComponent<CanvasGroup>();
            menuItem.SetActive(true);

            cg.alpha += 0.1f;

            if (cg.alpha > 0.90)
            {
                inAnimation = false;
                CancelInvoke("GoingInInventory");
            }

        }
    }

    void GoingInExit()
    {
        CanvasGroup cg = exitMenu.GetComponent<CanvasGroup>();
        exitLoadingMenu.SetActive(true);

        cg.alpha += 0.1f;

        inAnimation = true;

        if (cg.alpha > 0.99)
        {
            CancelInvoke("GoingInExit");
        }
    }

    void GoingOutInventory()
    {
        GameObject[] menuItems = GameObject.FindGameObjectsWithTag("ItemInventory");

        foreach (GameObject menuItem in menuItems)
        {
            CanvasGroup cg = menuItem.GetComponent<CanvasGroup>();

            cg.alpha -= 0.1f;

            if (cg.alpha < 0.01)
            {
                menuItem.SetActive(false);
                CancelInvoke("GoingOutInventory");
            }
        }
    }

    void GoingInConfig()
    {
        transitionBgConfig.SetActive(true);
        CanvasGroup cg = transitionBgConfig.GetComponent<CanvasGroup>();

        cg.alpha -= 0.10f;

        if (cg.alpha < 0.01)
        {
            inAnimation = false;
            CancelInvoke("GoingInConfig");
            transitionBgConfig.SetActive(false);
        }
    }

    void GoingOutConfig()
    {
        transitionBgConfig.SetActive(true);
        CanvasGroup cg = transitionBgConfig.GetComponent<CanvasGroup>();

        cg.alpha += 0.10f;

        if (cg.alpha > 0.99)
        {
            inAnimation = false;
            CancelInvoke("GoingOutConfig");
            //transitionBgConfig.SetActive(false);
        }
    }


    public void EnterInventory()
    {
        if (!inAnimation)
        {
            inAnimation = true;
            currMenu = Menu.Items;
            InvokeRepeating("GoingOutFirstMenu", 0, tickAnim);
            FillInventory();
            InvokeRepeating("GoingInInventory", switchAnim, tickAnim);
        }
    }

    public void EnterBack()
    {
        if (!inAnimation)
        {
            if (currMenu != Menu.Default)
            {
                inAnimation = true;

                switch (currMenu)
                {
                    case Menu.Items:
                        InvokeRepeating("GoingOutInventory", 0, tickAnim);
                        break;
                    case Menu.Config:
                        InvokeRepeating("GoingOutConfig", switchAnim, tickAnim);
                        break;
                }

                InvokeRepeating("GoingInMenu", 1.4f, 0.1f);
            }
        }
    }

    public void EnterConfig()
    {
        if (!inAnimation)
        {
            inAnimation = true;

            currMenu = Menu.Config;

            InvokeRepeating("GoingOutFirstMenu", 0, tickAnim);
            InvokeRepeating("GoingInConfig", switchAnim, tickAnim);
			RestorePrevConfig ();
        }
    }

    public void EnterExit()
    {
        currMenu = Menu.Exit;
        exitMenu.SetActive(true);

        CanvasGroup cg = exitMenu.GetComponent<CanvasGroup>();
        cg.alpha = 0;

        cg = exitMenu.GetComponent<CanvasGroup>();

        InvokeRepeating("GoingOutFirstMenu", 0, tickAnim);
        InvokeRepeating("GoingInExit", switchAnim, tickAnim);
        Invoke("ExitGame", switchAnim * 10);
    }

    public void OnChangeSensivity()
    {
        PlayerPrefs.SetFloat("mouseSensivity", sliderSensivity.GetComponent<Slider>().value * 10);
        PlayerPrefs.Save();
        GameInfo.mouseSensivity = sliderSensivity.GetComponent<Slider>().value * 10;
    }

    public void ChangeScreenSize()
    {
        Screen.fullScreen = !Screen.fullScreen;

        if (!Screen.fullScreen)
        {
            textResolution.text = fullScreenMode;
        }
        else
        {
            textResolution.text = windowedMode;
        }
    }

    void ExitGame()
    {
        Application.Quit();
    }

    void FillInventory()
    {
        List<string> items = new List<string>();

        if (context == MenuContext.Menu)
        {
            items = new List<string> {
			    Constants.PhoneItem,
                Constants.Key,
			    Constants.StockWood,
                Constants.PictureItem,
			    Constants.PictureP1Item,
			    Constants.PictureP2Item,
			    Constants.PictureP3Item,
			    Constants.PictureP4Item
		    };
        }
        else
        {
            items = Player.GetInstance().Items;
        }



        Vector2 originalPos = new Vector2(-238, 285);
        Vector2 initialPos = new Vector2(0, 0);

        //Vector2 originalPos = new Vector2(-100, 160);
        //Vector2 initialPos = new Vector2(-100, 160);

        int incrementY = 0;

        for (int i = 0; i < items.Count; i++)
        {
            Vector2 newPos = new Vector2(initialPos.x, initialPos.y);
            //Vector2 newPos = new Vector2 (initialPos.x, initialPos.y + (i * -120));

            GameObject currItem = null;

            switch (items[i])
            {
                case Constants.PhoneItem:
                    currItem = (GameObject)Instantiate(inventoryItemPhone, firstMenu.transform.position, new Quaternion(0, 0, 0, 0));
                    break;

                case Constants.Key:
                    currItem = (GameObject)Instantiate(inventoryItemKey, firstMenu.transform.position, new Quaternion(0, 0, 0, 0));
                    break;

                case Constants.StockWood:
                    currItem = (GameObject)Instantiate(inventoryItemStockWood, firstMenu.transform.position, new Quaternion(0, 0, 0, 0));
                    break;

                case Constants.PictureItem:
                    currItem = (GameObject)Instantiate(inventoryItemPicture, firstMenu.transform.position, new Quaternion(0, 0, 0, 0));
                    break;

                case Constants.PictureP1Item:
                    currItem = (GameObject)Instantiate(inventoryItemPictureP1, firstMenu.transform.position, new Quaternion(0, 0, 0, 0));
                    break;

                case Constants.PictureP2Item:
                    currItem = (GameObject)Instantiate(inventoryItemPictureP2, firstMenu.transform.position, new Quaternion(0, 0, 0, 0));
                    break;

                case Constants.PictureP3Item:
                    currItem = (GameObject)Instantiate(inventoryItemPictureP3, firstMenu.transform.position, new Quaternion(0, 0, 0, 0));
                    break;

                case Constants.PictureP4Item:
                    currItem = (GameObject)Instantiate(inventoryItemPictureP4, firstMenu.transform.position, new Quaternion(0, 0, 0, 0));
                    break;

                default:
                    currItem = (GameObject)Instantiate(inventoryItemTemplate, firstMenu.transform.position, new Quaternion(0, 0, 0, 0));
                    break;
            }

            if (i % 3 == 0)
            {
                initialPos = new Vector2(originalPos.x, originalPos.y);
                incrementY -= 140;
                initialPos.y += incrementY;
            }

            initialPos = new Vector2(initialPos.x + 120, initialPos.y);

            CanvasGroup cg = currItem.GetComponent<CanvasGroup>();

            //currItem.GetComponentInChildren<Text>().text = items[i];
            //currItem.GetComponentInChildren<Text>().text = "";

            currItem.transform.SetParent(firstMenu.transform, false);
            currItem.GetComponent<RectTransform>().transform.localPosition = new Vector3(initialPos.x, initialPos.y);
            currItem.SetActive(true);
            cg.alpha = 0;

			GameObject bgTitleInventory = HelperUtil.FindGameObject(smartphone, "FundoTextoInventario");
			GameObject titleInventory = HelperUtil.FindGameObject(smartphone, "TextoInventario");

			bgTitleInventory.SetActive (true);
			titleInventory.SetActive (true);

			itemsInventory = GameObject.FindGameObjectsWithTag("ItemInventory").ToList();



			//itemsInventory.Add (bgTitleInventory);
			//itemsInventory.Add (titleInventory);
        }
    }


}