using UnityEngine;
using System.Collections;

public class DoorWrapper : MonoBehaviour {

    public Animator doorAnim;
    public AudioSource audioOpenDoor;
    bool opened;

    [SerializeField]
    string relatedItem;

	[SerializeField]
	string sub;


	void Start () {
        opened = false;
        audioOpenDoor = gameObject.GetComponent<AudioSource>();
	}
	
	void Update () {
	
	}

    public void OpenDoor()
    {
		if(!isUnlocked())
		{
			SubtitleManager.GetInstance ().SetText (sub);
		}

        if (!opened && isUnlocked())
        {
            doorAnim.SetTrigger("open");
            audioOpenDoor.Play();

            opened = true;
        }
    }

    bool isUnlocked()
    {
        bool unlucked = false;

        if (string.IsNullOrEmpty(relatedItem) || Player.GetInstance().Items.Contains(relatedItem))
        {
            unlucked = true;
            Debug.Log("aberto");
        }

        return unlucked;
    }
}
