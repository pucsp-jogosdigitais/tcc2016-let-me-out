using UnityEngine;
using System.Collections;
using Assets.Scripts.Util;
using UnityEngine.UI;

public class CreditsMenu : MonoBehaviour {

    GameObject buttonBack;

	// Use this for initialization
	void Start () {
        buttonBack = HelperUtil.FindGameObject(this.gameObject, "Voltar");

        buttonBack.GetComponent<Button>().onClick.AddListener(delegate
        {
            Application.LoadLevel("newMenu");
        });
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
