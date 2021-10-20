using UnityEngine;

public class NPC : Interactable
{
    public override void Interact()
    {
        this.dialogTrigger.TriggerDialog();
    }
}
