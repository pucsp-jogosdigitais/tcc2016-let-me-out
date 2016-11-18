using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MultipleInteractItem: MonoBehaviour, IItem
{
    public Animator anim;
    public string animOpenTrigger = "interact";
    public string animCloseTrigger = "interact";
    private bool hasOpened = false;

    public void Interact()
    {
        string currAnimTrigger = animOpenTrigger;

        if(hasOpened)
        {
            currAnimTrigger = animCloseTrigger;
        }

        anim.SetTrigger(currAnimTrigger);
        hasOpened = !hasOpened;
        //Debug.Log("interagiu");
    }
}
