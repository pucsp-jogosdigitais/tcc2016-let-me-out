using UnityEngine;
using System.Collections;

public class DoorWrapper : MonoBehaviour
{

    public enum DoorAnim { Locked, Shaked, Open };
    public DoorAnim typeAnim;

    public Animator doorAnim;
    public AudioSource audioOpenDoor;
    bool opened;

	public string doorAnimTrigger = "open2";

	public string relatedItem;

    [SerializeField]
    string sub;


    void Start()
    {
        opened = false;
        audioOpenDoor = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {

    }

    public void OpenDoor()
    {
        AudioSource audio = gameObject.GetComponent<AudioSource>();

        Debug.Log("Abriu porta");

        switch (typeAnim)
        {
            case DoorAnim.Locked:

                if (!opened && isUnlocked())
                {
				doorAnim.SetTrigger(doorAnimTrigger);
                    audio.clip = AudioRepository.GetInstance().doorOpen;
                    audioOpenDoor.Play();
                } else
                {
                    if (!audio.isPlaying)
                    {
                        Debug.Log("entrou");
                        audio.clip = AudioRepository.GetInstance().doorLocked;
                        audio.Play();
                    }
                }

                break;

            case DoorAnim.Shaked:

                if(!audio.isPlaying)
                {
                    audio.clip = AudioRepository.GetInstance().doorShaked;
                    doorAnim.SetTrigger("shake");
                    audioOpenDoor.Play();
                    EventManager.GetInstance().SetEvent("lockDoors");
                }

                break;
        }


        /*if(!isUnlocked())
           {
               SubtitleManager.GetInstance ().SetText (sub);
           }

           if (!opened && isUnlocked())
           {
               doorAnim.SetTrigger("open");
               audioOpenDoor.Play();

               opened = true;
           }*/
    }

    bool isUnlocked()
    {
        bool unlucked = false;

        if (string.IsNullOrEmpty(relatedItem) || Player.GetInstance().Items.Contains(relatedItem) || Player.GetInstance().Actions.Contains(relatedItem))
        {
            unlucked = true;
            Debug.Log("aberto");
        }

        return unlucked;
    }
}
