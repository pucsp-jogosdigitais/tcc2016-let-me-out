using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class InteractItem : MonoBehaviour, IItem
{
    Animator anim;
    public string animTrigger = "interact";

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {

    }

    public void Interact()
    {
        anim = GetComponent<Animator>();
        Debug.Log("interagiu");
        anim.SetTrigger(animTrigger);
    }
}
