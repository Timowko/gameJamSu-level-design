using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ObjectInteraction : MonoBehaviour
{

    Ray ray;
    RaycastHit clickedObject, objectInRadius;
    Interactable interactableObject, lastInteractableObject;

    private void Start() {
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)){
            Scene scene = SceneManager.GetActiveScene(); 
            SceneManager.LoadScene(scene.name);
        }
        
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if(Physics.Raycast(ray, out clickedObject))
        {
            // Debug.Log(clickedObject.collider.name);
            
            interactableObject = clickedObject.collider.GetComponent<Interactable>();
            if( interactableObject != null ){
                if( interactableObject.isInteractable() ){
                    lastInteractableObject = interactableObject;
                    if(Input.GetMouseButtonDown(0)){
                        interactableObject.Interact();
                    } 
                    else {
                        interactableObject.Hover();
                    }
                }
                else {
                    ResetInteractableObject();
                }
            }
            else{
                ResetInteractableObject();
            }
        } 

    }

    void ResetInteractableObject(){
        if( lastInteractableObject != null ){
            lastInteractableObject.DisableInteraction();
            lastInteractableObject = null;
        }
    }

}