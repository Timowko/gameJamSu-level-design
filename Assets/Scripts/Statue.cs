using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System.Collections.Generic;

[RequireComponent(typeof(Outline))]
public class Statue : BaseInteractable
{

    ParticleSystem vfx;
    
    MeshFilter currentMesh;
    int currentMeshIndex = 0;
    public Mesh[] statueStates;
    private bool isStatueCompleted = false;

    public void Start() {
        base.Start();
        vfx = GetComponentInChildren<ParticleSystem>();
        currentMesh = GetComponent<MeshFilter>();
    }

    public override void Interact() {
        // Debug.Log("building statue");
        if( !isPrerequisitesCompleted ){
            base.Interact();
            // dialogueWindow.Enable();
            // if( isDialogueFinished ){
            //     allDialogueText = "Statue:I'm done;";
            // }
            // dialogueWindow.SetAllDialogueText(allDialogueText, isDialogueFinished);
        }
        if( !isStatueCompleted ){
            PlayVFX();
            DisableOutline();
            StopSound();
            PlaySound();
            UpdateMesh();
        } else {
            base.Interact();
            dialogueWindow.Enable();
            if( isDialogueFinished ){
                allDialogueText = finalText;
            }
            dialogueWindow.SetAllDialogueText(allDialogueText, isDialogueFinished);
        }
    }

    public override bool isInteractable()
    {
        return base.isInteractable() 
            && isPrerequisitesCompleted 
            && (vfx == null || !vfx.isPlaying);
            // && !sound.isPlaying;
    }

    void UpdateMesh(){
        if( currentMeshIndex + 1 >= statueStates.Length ){
            isStatueCompleted = true;
            return;
        }
        currentMeshIndex++;
        currentMesh.mesh = statueStates[currentMeshIndex];
    }

    void PlayVFX() {
        if( vfx ){
            if( !vfx.isPlaying )
                vfx.Play();
        }
    }
}