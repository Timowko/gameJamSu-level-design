using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class NPC : BaseInteractable
{

    public void Start() {
        base.Start();
        outliner = null;
    }

    public override void Interact() {
        base.Interact();
        // Debug.Log("interacting NPC");
        // Dialog
        dialogueWindow.Enable();
        if( isDialogueFinished ){
            allDialogueText = finalText;
        }
        dialogueWindow.SetAllDialogueText(allDialogueText, isDialogueFinished);
    }

    public override void DisableInteraction(){
        base.DisableInteraction();
        // dialogueWindow.Disable();
    }

    public override bool isInteractable()
    {
        return base.isInteractable() 
            && isPrerequisitesCompleted;
            // && !sound.isPlaying;
    }

    public void Update() {
        base.Update();
    }

}