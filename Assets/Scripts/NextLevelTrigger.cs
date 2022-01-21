using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Outline))]
public class NextLevelTrigger : BaseInteractable
{

    public void Start() {
        base.Start();
        outliner = null;
    }

    public override void Interact() {
        base.Interact();
        if( isPrerequisitesCompleted )
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Update() {
        base.Update();
    }

}