using UnityEngine;
using System.Collections;
using Assets.Scripts.Util;
using UnityEngine.UI;

public class DefaultMenu : MonoBehaviour {

    GameObject buttonPlay;
    GameObject buttonCredits;
    GameObject buttonExit;
    GameObject buttonOptions;

	// Use this for initialization
	void Start () {
        buttonPlay = HelperUtil.FindGameObject(this.gameObject, "Jogar");
        buttonCredits = HelperUtil.FindGameObject(this.gameObject, "Creditos");
        buttonExit = HelperUtil.FindGameObject(this.gameObject, "Sair");
        buttonOptions = HelperUtil.FindGameObject(this.gameObject, "Opcoes");

        buttonPlay.GetComponent<Button>().onClick.AddListener(delegate
        {
            Application.LoadLevel("new_Intro");
        });

        buttonCredits.GetComponent<Button>().onClick.AddListener(delegate
        {
            Application.LoadLevel("credits");
        });

        buttonExit.GetComponent<Button>().onClick.AddListener(delegate
        {
            Application.Quit();
        });

        buttonOptions.GetComponent<Button>().onClick.AddListener(delegate
        {
            Application.Quit();
        });
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
