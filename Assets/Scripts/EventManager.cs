using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
            case "initialEvent":

                Invoke("InitialEvent", 3);
                Invoke("BabyCry", 5);
                Invoke("ActivateBaby", 6);

                Monster.GetInstance().currActionState = Monster.MonsterActionState.Rest;

                break;

            case "activatePart2":

                ActivatePartPicture(Constants.PictureP2Item);

                break;

            case "activatePart3":

                ActivatePartPicture(Constants.PictureP3Item);

                break;

            case "activatePart4":

                ActivatePartPicture(Constants.PictureP4Item);

                break;


            case "babyPersecution":

                //Monster.GetInstance().currActionState = Monster.MonsterActionState.Rest;
                Monster.GetInstance().PersecutionMode();
                Player.GetMotionBlur().blurAmount = 0.6f;
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

    private void InitialEvent()
    {
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
}
