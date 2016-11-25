using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections.Generic;
using System;

public class Main : MonoBehaviour
{
    public bool active;
    public bool hasActivate;

    static Main instance;

    public GameObject gameOverImage;
    float alphaGameOver = 0;

    public Animator animSmartPhone;

    public Texture2D cursor;

    public static Main GetInstance()
    {
        return instance;
    }

    // Use this for initialization
    void Start()
    {
        instance = this;

        GameObject.Find("CameraLockCameraPicture1").GetComponent<Camera>().enabled = false;


        gameOverImage.GetComponent<CanvasRenderer>().SetAlpha(alphaGameOver);

        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");

        DesativarItems(Constants.PictureP2Item, items);
        DesativarItems(Constants.PictureP3Item, items);
        DesativarItems(Constants.PictureP4Item, items);

        InitialConfig.Configure();
    }

    private void DesativarItems(string itemCod, GameObject[] itemsGO)
    {
        foreach (GameObject itemGO in itemsGO)
        {
            Item item = itemGO.GetComponent<Item>();

            if (item != null)
            {
                if (item.CodItem.Contains(itemCod))
                {
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
			EventManager.GetInstance ().SetEvent ("activatePart3");
		}

        if (GameInfo.gameOver)
        {
            alphaGameOver += 0.10f;
            gameOverImage.GetComponent<CanvasRenderer>().SetAlpha(alphaGameOver);
        }

        if (Input.GetKeyDown(KeyCode.C) && Player.GetInstance().Items.Contains(Constants.PhoneItem))
        {
            active = !active;
            Cursor.visible = active;

            if (active)
            {
                if (!hasActivate)
                {
                    GameObject.Find("IconeSmartphone").GetComponent<Animator>().SetTrigger("desactivate");
                    Player.GetInstance().Actions.Add(Constants.ActionGetPhone);
                    hasActivate = true;

                }
            }
        }
    }

    void ActivateFlashLightIcon()
    {
        EventManager.GetInstance().SetEvent("activateIconFlashLight");
    }

    void OnApplicationFocus(bool focusStatus)
    {
        Cursor.visible = false;
    }

    public void GameOver()
    {
        if (!GameInfo.gameOver)
        {
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
}
