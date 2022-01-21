using UnityEngine;
using System.Collections;
public interface Interactable 
{
    bool isInteractable();
    void Hover();
    void Interact();
    void EnableInteraction();
    void DisableInteraction();
    void EnableOutline(string state);
    void DisableOutline();
    void OnDrawGizmos();
}