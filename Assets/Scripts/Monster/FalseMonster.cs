using UnityEngine;
using System.Collections;
using Assets.Scripts.Util;
using System.Collections.Generic;
using System.Linq;

public class FalseMonster : MonoBehaviour {

    static FalseMonster instance;

    public static FalseMonster GetInstance()
    {
        return instance;
    }

    // Use this for initialization
    void Start () {
        instance = this;

        HelperUtil.SetVisibility(gameObject, false);
        HelperUtil.SetVisibility(HelperUtil.FindGameObject(gameObject, "falseNewBaby"), false);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Desactivate()
    {
        HelperUtil.SetVisibility(gameObject, false);
        HelperUtil.SetVisibility(HelperUtil.FindGameObject(gameObject, "falseNewBaby"), false);

        GetComponent<NavMeshAgent>().enabled = false;
    }

    public void Activate()
    {
        HelperUtil.SetVisibility(gameObject, true);
        HelperUtil.SetVisibility(HelperUtil.FindGameObject(gameObject, "falseNewBaby"), true);

        gameObject.transform.position = new Vector3(269.02f, 4.449f, 237.2929f);
        GetComponent<NavMeshAgent>().enabled = true;
        GetComponent<NavMeshAgent>().destination = GameObject.Find("AlvoBebe").transform.position;
        SetAnimationState(2);
    }

    public void SetAnimationState(int index)
    {
        List<AnimationState> states;
        Animation anim = GetAnimation();
        states = new List<AnimationState>(anim.Cast<AnimationState>());

        anim.Stop();
        anim.clip = states[(int)index].clip;
        Debug.Log(anim.clip.name);
        anim.Play();
    }

    public Animation GetAnimation()
    {
        return instance.GetComponentInChildren<Animation>();
    }
}
