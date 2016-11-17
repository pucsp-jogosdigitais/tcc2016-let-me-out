using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TestBaby : MonoBehaviour
{

    Animation anim;
    List<AnimationState> states;


    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animation>();
        states = new List<AnimationState>(anim.Cast<AnimationState>());
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SetCurrAnim(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetCurrAnim(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetCurrAnim(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetCurrAnim(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetCurrAnim(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SetCurrAnim(5);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SetCurrAnim(6);
        }

    }

    private void SetCurrAnim(int index)
    {
        anim.Stop();
        anim.clip = states[index].clip;
        Debug.Log(anim.clip.name);
        anim.Play();
    }
}
