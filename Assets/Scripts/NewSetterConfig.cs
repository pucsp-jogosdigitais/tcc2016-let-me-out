using UnityEngine;
using System.Collections;
using System;

public class NewSetterConfig : MonoBehaviour {

	// Use this for initialization
	void Start () {

        SetFullScreen();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void SetFullScreen()
    {
        if(PlayerPrefs.HasKey("FullScreen"))
        {
            bool fullScreen = false;

            if (PlayerPrefs.GetInt("FullScreen") > 0)
            {
                fullScreen = true;
            }

            GameInfo.fullScreen =  fullScreen;
            Screen.fullScreen = fullScreen;
        }
    }
}
