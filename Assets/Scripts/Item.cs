using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour
{
    public enum ItemType { Get, Interact, StockWoodMaker, Picture };
    public ItemType currType;

    bool destroyed;
    public bool hide = false;

    [SerializeField]
    string codItem;

    [SerializeField]
    string sub;

    [SerializeField]
    string subRelatedItem;

    [SerializeField]
    string relatedItemCod;

    [SerializeField]
    string eventItem;

    int pictureCounter = 4;

    int clickCounter = 0;
    bool hasClicked = false;

	bool hasCompletedPicture;

    void Start()
    {

    }

    void Update()
    {

    }

    public void Hide()
    {
        MeshRenderer mesh = gameObject.GetComponent<MeshRenderer>();

        destroyed = true;
        hide = true;

        if (mesh != null)
        {
            mesh.enabled = false;
        }
        else
        {
            SetVisibility(false);
        }
    }

    public void Show()
    {
        MeshRenderer mesh = gameObject.GetComponent<MeshRenderer>();

        destroyed = false;

        if (mesh != null)
        {
            mesh.enabled = true;
        }
        else
        {
            SetVisibility(true);
        }
    }

    public void Destroy()
    {
        if (!destroyed)
        {
            AudioSource audio = gameObject.GetComponent<AudioSource>();
            MeshRenderer mesh = gameObject.GetComponent<MeshRenderer>();

            switch (currType)
            {
                case ItemType.Get:
                    SubtitleManager.GetInstance().SetText(sub);

                    if (!string.IsNullOrEmpty(eventItem))
                    {
                        EventManager.GetInstance().SetEvent(eventItem);
                    }

                    destroyed = true;
                    hide = false;

                    audio.Play();

                    if (mesh != null)
                    {
                        mesh.enabled = false;
                    }
                    else
                    {
                        SetVisibility(false);
                    }

                    break;
                case ItemType.Interact:
                    if (!Player.GetInstance().Items.Contains(relatedItemCod))
                    {
                        SubtitleManager.GetInstance().SetText(subRelatedItem);
                    }
                    else
                    {
                        SubtitleManager.GetInstance().SetText(sub);
                        destroyed = true;
                        audio.Play();
                    }

                    Debug.Log("Interact");
                    break;

                case ItemType.StockWoodMaker:
                    if (!Player.GetInstance().Items.Contains(Constants.StockWood))
                    {
                        SubtitleManager.GetInstance().SetText("Pegou Lenha");
                        Player.GetInstance().Items.Add(Constants.StockWood);
                    }
                    else
                    {
                        SubtitleManager.GetInstance().SetText("Você já tem lenha");
                    }


                    break;

			case ItemType.Picture:

				string[] partsPicture = new string[] {
					Constants.PictureP1Item,
					Constants.PictureP2Item,
					Constants.PictureP3Item,
					Constants.PictureP4Item
				};

				clickCounter += 1;

				Debug.Log (clickCounter);

				if (hasCompletedPicture) {
					EventManager.GetInstance().SetEvent("babyPersecution");
					PartsToCompletePicture(partsPicture);
					SubtitleManager.GetInstance().SetText("Quadro completo");
					gameObject.SetActive(false);
				}

                    if (clickCounter > 2)
                    {
                        AudioRepository audioRepo = AudioRepository.GetInstance();

                        if(!audioRepo.gameObject.GetComponent<AudioSource>().isPlaying)
                        {
                            audioRepo.gameObject.GetComponent<AudioSource>().clip = audioRepo.incompletePictureAudio;
                            audioRepo.gameObject.GetComponent<AudioSource>().Play();
                        }
                    }

                    Invoke("ResetCounter", 30f);

                    AddPartsPicture(partsPicture);

                    if (pictureCounter > 0)
                    {
                        SubtitleManager.GetInstance().SetText("Quadro incompleto");
                    }
                    else
                    {
						/*
                        EventManager.GetInstance().SetEvent("babyPersecution");
                        PartsToCompletePicture(partsPicture);
                        SubtitleManager.GetInstance().SetText("Quadro completo");
                        gameObject.SetActive(false);*/

					hasCompletedPicture = true;
                    }

					

                    break;
            }
        }
    }

    void ResetCounter()
    {
        clickCounter = 0;
    }

    void FillPicture(string codPicture)
    {
        if (Player.GetInstance().Items.Contains(codPicture))
        {
            if (!GameObject.Find(codPicture).GetComponent<MeshRenderer>().enabled)
            {
                pictureCounter -= 1;
                GameObject.Find(codPicture).GetComponent<MeshRenderer>().enabled = true;

                switch(codPicture)
                {
                    case Constants.PictureP1Item:
                        EventManager.GetInstance().SetEvent("activatePart2");
                        break;

                    case Constants.PictureP2Item:
                        EventManager.GetInstance().SetEvent("activatePart3");
                        break;

                    case Constants.PictureP3Item:
                        EventManager.GetInstance().SetEvent("activatePart4");
                        break;
                }
            }
        }
    }

    void AddPartsPicture(string[] parts)
    {
        foreach (string part in parts)
        {
            FillPicture(part);
        }
    }

    void PartsToCompletePicture(string[] parts)
    {
        foreach (string part in parts)
        {
            Player.GetInstance().Items.Remove(part);
        }

        Player.GetInstance().Items.Add(Constants.PictureItem);
    }

    void SetVisibility(bool visibility)
    {
        Renderer[] rs = GetComponentsInChildren<Renderer>();

        foreach (Renderer r in rs)
        {
            r.enabled = visibility;
        }
    }

    public string CodItem
    {
        get { return codItem; }
    }

    public bool isDestroyed
    {
        get { return destroyed; }
    }
}
