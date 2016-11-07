using UnityEngine;
using System.Collections;

public class ReverbTrigger : MonoBehaviour {

    public ReverbCtrl reverbCtrl;
    public int triggerNr;

	// Use this for initialization
	void OnTriggerEnter ()
    {
        reverbCtrl.BlendSnapshot(triggerNr);
	}
		
}
