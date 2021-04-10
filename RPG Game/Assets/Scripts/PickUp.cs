using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : Interactable
{
    public Item item;
    
    public override void Interact()
    {
        base.Interact();
        Pick();
    }

    void Pick()
    {
        Debug.Log("use item : " + item.name);
        Destroy(gameObject);
    }
}
