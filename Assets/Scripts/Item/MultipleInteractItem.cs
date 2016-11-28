using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MultipleInteractItem : MonoBehaviour, IItem
{
    public Animator anim;
    public string animTrigger = "interact";

    public void Interact()
    {
        anim.SetTrigger(animTrigger);
    }
}