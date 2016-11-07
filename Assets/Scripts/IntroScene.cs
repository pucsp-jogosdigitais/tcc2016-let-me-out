using UnityEngine;
using System.Collections;

public class IntroScene : MonoBehaviour
{

    public string[] subs;

    int currSub = 0;

    // Use this for initialization
    void Start()
    {
        SetNewSub();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            Application.LoadLevel("game");
        }
    }

    void SetNewSub()
    {
        SubtitleManager.GetInstance().SetText(subs[currSub]);

        currSub += 1;

        if (currSub < subs.Length)
        {
            Invoke("SetNewSub", 5f);
        }
        else
        {
            Application.LoadLevel("game");
        }
    }

}
