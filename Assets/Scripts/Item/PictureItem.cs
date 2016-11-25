using UnityEngine;
using System.Collections;
using Assets.Scripts.Util;

public class PictureItem : MonoBehaviour, IItem
{
    public string codItem;

    public string pictureComplete;
    public string pictureIncomplete;

    int pictureCounter = 4;
    int clickCounter = 0;

    bool hasCompletedPicture;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Interact()
    {
        string[] partsPicture = new string[] {
					Constants.PictureP1Item,
					Constants.PictureP2Item,
					Constants.PictureP3Item,
					Constants.PictureP4Item
		};

        clickCounter += 1;

        if (hasCompletedPicture)
        {
            EventManager.GetInstance().SetEvent("babyPersecution");
            PartsToCompletePicture(partsPicture);
            SubtitleManager.GetInstance().SetText(pictureComplete);
            gameObject.SetActive(false);
        }

        if (clickCounter > 2)
        {
            AudioRepository audioRepo = AudioRepository.GetInstance();

            if (!audioRepo.gameObject.GetComponent<AudioSource>().isPlaying)
            {
                audioRepo.gameObject.GetComponent<AudioSource>().clip = audioRepo.incompletePictureAudio;
                audioRepo.gameObject.GetComponent<AudioSource>().Play();
            }
        }

        Invoke("ResetCounter", 30f);

        AddPartsPicture(partsPicture);

        if (pictureCounter > 0)
        {
            SubtitleManager.GetInstance().SetText(pictureIncomplete);
        }
        else
        {
            hasCompletedPicture = true;
            HelperUtil.FindGameObject(GameObject.Find("Sangue pós quarta foto"), "SangueContainer").SetActive(true);
        }
    }

    void ResetCounter()
    {
        clickCounter = 0;
    }

    void AddPartsPicture(string[] parts)
    {
        foreach (string part in parts)
        {
            FillPicture(part);
        }
    }

    void FillPicture(string codPicture)
    {
        if (Player.GetInstance().Items.Contains(codPicture))
        {
            if (!GameObject.Find(codPicture).GetComponent<MeshRenderer>().enabled)
            {
                pictureCounter -= 1;
                GameObject.Find(codPicture).GetComponent<MeshRenderer>().enabled = true;

                switch (codPicture)
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

    void PartsToCompletePicture(string[] parts)
    {
        foreach (string part in parts)
        {
            Player.GetInstance().Items.Remove(part);
        }

        Player.GetInstance().Items.Add(Constants.PictureItem);
    }

}
