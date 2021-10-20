using UnityEngine;

public static class Extensions
{
    public static Interactable IsInteractable(this RaycastHit2D hit)
    {
        return hit.transform.GetComponent<Interactable>();
    }
}
