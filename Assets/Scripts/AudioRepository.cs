using UnityEngine;
using System.Collections;

public class AudioRepository : MonoBehaviour {

    static AudioRepository instance;

    public AudioClip babyAudio;

    public AudioClip incompletePictureAudio;

    public AudioClip doorLocked;
    public AudioClip doorShaked;
    public AudioClip doorOpen;

    public static AudioRepository GetInstance()
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
}
