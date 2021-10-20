using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class Interactable : MonoBehaviour
{
    public DialogTrigger dialogTrigger;
    public DialogManager dialogManager;

    public abstract void Interact();

    private void OnTriggerEnter2D(Collider2D collision)
    {
         //Show interactability here
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Hide interactability here
    }
}
