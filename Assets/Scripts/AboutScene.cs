using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class AboutScene : MonoBehaviour {

    public Vector2 initialPos = new Vector2(0, 0);

    GameObject canvas;
    public string[] legends;

    public GameObject legendTemplate;
    List<GameObject> legendsGameObjects;

	// Use this for initialization
	void Start () {
        canvas = GameObject.Find("CanvasAbout");
        CreateLegends();
	}
	
	// Update is called once per frame
	void Update () {
        MoveLegends();
	}

    private void CreateLegends()
    {
        legendsGameObjects = new List<GameObject>();
        Vector3 pos = new Vector3(initialPos.x, initialPos.y);

        foreach (string legend in legends)
        {
            GameObject go = (GameObject)Instantiate(legendTemplate, new Vector2(0, 0), new Quaternion());
            Text txt = go.GetComponent<Text>();

            go.SetActive(true);
            go.transform.SetParent(canvas.transform, false);
            go.GetComponent<RectTransform>().transform.localPosition = new Vector2(initialPos.x, initialPos.y);
            txt.text = legend;

            legendsGameObjects.Add(go);
        }
    }

    private void MoveLegends()
    {
        foreach (GameObject legend in legendsGameObjects)
        {

        }
    }
}
