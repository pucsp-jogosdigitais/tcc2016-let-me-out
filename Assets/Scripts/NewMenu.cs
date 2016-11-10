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

    public enum Menu { Default, Items, Config, Exit, None };
    public Menu[] menusToShow;
    Menu currMenu = Menu.Default;

    public bool inAnimation;

    public GameObject smartphone;
    public GameObject firstMenu;
    public GameObject exitMenu;
    public GameObject exitLoadingMenu;
    public GameObject inventoryMenu;
    public GameObject configMenu;
    public GameObject transitionBgConfig;

    public GameObject buttonItemsMenu;
    public GameObject buttonConfigMenu;
    public GameObject buttonExitMenu;

    public GameObject inventoryItemTemplate;
    public GameObject inventoryItemPhone;
    public GameObject inventoryItemPicture;
    public GameObject inventoryItemPictureP1;
    public GameObject inventoryItemPictureP2;
    public GameObject inventoryItemPictureP3;
    public GameObject inventoryItemPictureP4;
    public GameObject inventoryItemStockWood;
    public GameObject inventoryItemKey;

    public Camera defaultCamera;

    // Use this for initializati1on
    void Start()
    {
        smartphone = GameObject.Find("SmartPhone");
        firstMenu = GameObject.Find("Principal");
        
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
        inventoryItemPicture = HelperUtil.FindObject(smartphone, "ItemTora");
        inventoryItemPictureP1 = itemInventory;
        inventoryItemPictureP2 = itemInventory;
        inventoryItemPictureP3 = itemInventory;
        inventoryItemPictureP4 = itemInventory;
        inventoryItemStockWood = HelperUtil.FindObject(smartphone, "ItemTora");
        inventoryItemKey = HelperUtil.FindObject(smartphone, "ItemChave");

        FillMenu();
    }

    // Update is called once per frame
    void Update()
    {
        //GameObject quitWrapper = GameObject.Find("MenuSair");
    }

    void DesactivateBlur()
    {
        UnityStandardAssets.ImageEffects.Blur blur = defaultCamera.GetComponent<UnityStandardAssets.ImageEffects.Blur>();

        blur.blurSpread = 0;
        blur.iterations = 3;
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
                go.transform.SetParent(firstMenu.transform, false);
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
            InvokeRepeating("GoingOutFirstMenu", 0.1f, 0.1f);
            FillInventory();
            InvokeRepeating("GoingInInventory", 1.4f, 0.1f);
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
                        InvokeRepeating("GoingOutInventory", 0.1f, 0.1f);
                        break;
                    case Menu.Config:
                        InvokeRepeating("GoingOutConfig", 0.1f, 0.1f);
                        break;
                }

                InvokeRepeating("GoingInMenu", 1.4f, 0.1f);
            }
        }
    }

    public void EnterConfig()
    {
        if(!inAnimation)
        {
            inAnimation = true;

            currMenu = Menu.Config;

            InvokeRepeating("GoingOutFirstMenu", 0.1f, 0.1f);
            InvokeRepeating("GoingInConfig", 1.2f, 0.1f);
        }
    }

    public void EnterExit()
    {
        currMenu = Menu.Exit;
        exitMenu.SetActive(true);

        CanvasGroup cg = exitMenu.GetComponent<CanvasGroup>();
        cg.alpha = 0;

        cg = exitMenu.GetComponent<CanvasGroup>();

        InvokeRepeating("GoingOutFirstMenu", 0.1f, 0.1f);
        InvokeRepeating("GoingInExit", 1.4f, 0.1f);
    }

    void FillInventory()
    {
        List<string> items = new List<string>();

        if(!inGame)
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

            currItem.GetComponentInChildren<Text>().text = items[i];

            currItem.transform.SetParent(firstMenu.transform, false);
            currItem.GetComponent<RectTransform>().transform.localPosition = new Vector3(initialPos.x, initialPos.y);
            currItem.SetActive(true);
            cg.alpha = 0;
        }
    }


}