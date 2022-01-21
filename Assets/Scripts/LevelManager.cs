using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public BaseInteractable[] interactableItems;
    public int currentInteractableItem = 0;
    
    public GameObject diamond;
    public Material unactive, active;
    Renderer rd;

    public void Start() {
        currentInteractableItem = 0;
        EnableInteractableObject();
        rd = diamond.GetComponent<Renderer>();
        rd.material = unactive;
    }

    void MoveToNextInteractableObject() {
        if( currentInteractableItem + 1 > interactableItems.Length ){
            return;
        }
        if( currentInteractableItem + 1 >= interactableItems.Length ){
            return;
        }
        interactableItems[currentInteractableItem].dialogueWindow.Disable();
        currentInteractableItem++;
        EnableInteractableObject();
    }

    void EnableInteractableObject() {
        if( interactableItems != null ){
            interactableItems[currentInteractableItem].SetPrerequisitesCompleted();
            SetDiamondPosition();
        }
    }

    void SetDiamondPosition(){
        Vector3 pos = interactableItems[currentInteractableItem].GetCenter();
        Debug.Log(pos);
        pos.y += 2.25f;
        diamond.transform.position = pos;
    }

    public void Update() {
        if( interactableItems != null ){
            if( currentInteractableItem < interactableItems.Length ){
                if( interactableItems[currentInteractableItem].isDialogueFinished ){
                    MoveToNextInteractableObject();
                }
                if( interactableItems[currentInteractableItem].isInteractable() ){
                    if( rd != null )
                        rd.material = active;
                }else {
                    if( rd != null )
                        rd.material = unactive;
                }
            }
        }
    }


}