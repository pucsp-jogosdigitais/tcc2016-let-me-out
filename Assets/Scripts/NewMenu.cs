using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using Assets.Scripts.Util;

public class NewMenu : MonoBehaviour
{
    [SerializeField]
    bool inGame;
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
    GameObject transitionBgConfig;

    Animator animatorSmartphone;

    GameObject buttonItemsMenu;
    GameObject buttonConfigMenu;
    GameObject buttonExitMenu;

    Slider sliderSensivity;
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

    // Use this for initializati1on
    void Start()
    {
        smartphone = GameObject.Find("SmartPhone");
        wrapperSmartphone = GameObject.Find("WrapperSmartphone");

        firstMenu = GameObject.Find("Principal");

        inventoryMenu = GameObject.Find("MenuInventario");

        configMenu = GameObject.Find("MenuConfiguracoes");
        transitionBgConfig = GameObject.Find("EfeitoTransicao");

        buttonConfigMenu = HelperUtil.FindObject(smartphone, "BotaoConfiguracoes");
        buttonItemsMenu = HelperUtil.FindObject(smartphone, "BotaoItens");
        buttonExitMenu = HelperUtil.FindObject(smartphone, "BotaoSair");

        exitMenu = HelperUtil.FindObject(smartphone, "FundoPadraoSair");
        exitLoadingMenu = HelperUtil.FindObject(smartphone, "AnimacaoSair");

        GameObject itemInventory = HelperUtil.FindObject(smartphone, "ItemInventario");

        inventoryItemTemplate = itemInventory;
        inventoryItemPhone = itemInventory;
        inventoryItemPicture = HelperUtil.FindObject(smartphone, "ItemQuadroCompleto");
        inventoryItemPictureP1 = HelperUtil.FindObject(smartphone, "ItemFoto1");
        inventoryItemPictureP2 = HelperUtil.FindObject(smartphone, "ItemFoto2");
        inventoryItemPictureP3 = HelperUtil.FindObject(smartphone, "ItemFoto3");
        inventoryItemPictureP4 = HelperUtil.FindObject(smartphone, "ItemFoto4");
        inventoryItemStockWood = HelperUtil.FindObject(smartphone, "ItemTora");
        inventoryItemKey = HelperUtil.FindObject(smartphone, "ItemChave");

        buttonResolution = GameObject.Find("BotaoJanela");
        textResolution = buttonResolution.GetComponentInChildren<Text>();

        textResolution.text = fullScreenMode;

        if (!Screen.fullScreen)
        {
            textResolution.text = windowedMode;
        }

        buttonResolution.GetComponent<Button>().onClick.AddListener(delegate {
                ChangeScreenSize();
            }
        );

        sliderSensivity = HelperUtil.FindObject(smartphone, "SliderSensibilidade").GetComponent<Slider>();

        sliderSensivity.GetComponent<Slider>().onValueChanged.AddListener(delegate {
            OnChangeSensivity();
        });


        wrapperSmartphone.SetActive(false);
        SetBlur(false);
        animatorSmartphone = wrapperSmartphone.GetComponent<Animator>();

        FillMenu();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(!inAnimation)
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
                }
                else
                {
                    animatorSmartphone.SetTrigger("bounceOut");
                    Invoke("DesactivateSmartPhone", 2.1f);
                }
            }
        }

        //GameObject quitWrapper = GameObject.Find("MenuSair");
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

    void FillMenu()
    {
        Vector3 originalPos = new Vector2(-110, 224);
        Vector2 initialPos = new Vector2(-110, 224);
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
        }
    }

    void GoingInMenu()
    {
        GameObject[] menuItems = GameObject.FindGameObjectsWithTag("FirstMenuItem");

        foreach (GameObject menuItem in menuItems)
        {
            CanvasGroup cg = menuItem.GetComponent<CanvasGroup>();

            cg.alpha += 0.1f;

            if (cg.alpha > 0.90)
            {
                inAnimation = false;
                currMenu = Menu.Default;
                CancelInvoke("GoingInMenu");
            }

        }
    }

    void GoingOutFirstMenu()
    {
        GameObject[] menuItems = GameObject.FindGameObjectsWithTag("FirstMenuItem");

        foreach (GameObject menuItem in menuItems)
        {
            CanvasGroup cg = menuItem.GetComponent<CanvasGroup>();

            cg.alpha -= 0.1f;

            if (cg.alpha < 0.01)
            {
                CancelInvoke("GoingOutFirstMenu");
            }
        }
    }

    void GoingInInventory()
    {
        GameObject[] menuItems = GameObject.FindGameObjectsWithTag("ItemInventory");

        foreach (GameObject menuItem in menuItems)
        {
            CanvasGroup cg = menuItem.GetComponent<CanvasGroup>();

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
    }

    public void OnChangeSensivity()
    {
        PlayerPrefs.SetFloat("mouseSensivity", sliderSensivity.value * 10);
        PlayerPrefs.Save();
        GameInfo.mouseSensivity = sliderSensivity.value * 10;
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

    void FillInventory()
    {
        List<string> items = new List<string>();

        if (!inGame)
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



        Vector2 originalPos = new Vector2(-243, 240);
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
                incrementY -= 120;
                initialPos.y += incrementY;
            }

            initialPos = new Vector2(initialPos.x + 120, initialPos.y);

            CanvasGroup cg = currItem.GetComponent<CanvasGroup>();

            //currItem.GetComponentInChildren<Text>().text = items[i];
            currItem.GetComponentInChildren<Text>().text = "";

            currItem.transform.SetParent(firstMenu.transform, false);
            currItem.GetComponent<RectTransform>().transform.localPosition = new Vector3(initialPos.x, initialPos.y);
            currItem.SetActive(true);
            cg.alpha = 0;
        }
    }


}