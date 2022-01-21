using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class BaseInteractable : MonoBehaviour, Interactable
{

    Transform mainCharacterTransform;
    public Outline outliner;
    Bounds colliderBounds;
    private Vector3 center;
    float radius;
    AudioSource sound;
    
    [SerializeField]
    private Dictionary<string, Color> outlineColorModes;
    private string currentOutlineState;
    public bool isPrerequisitesCompleted = false;
    public DialogueWindow dialogueWindow;
    public string allDialogueText;
    public string finalText;
    public bool isDialogueFinished = false;

    public void Awake() {
        colliderBounds = GetComponent<Collider>().bounds;
        setObjectProperties();
    }

    public void Start() {
        mainCharacterTransform = GameObject.FindWithTag("Player").transform;

        outliner = GetComponent<Outline>();
        DisableOutline();

        setOutlineColorModes();

        sound = GetComponentInChildren<AudioSource>();

        if( dialogueWindow != null )
            dialogueWindow.Disable();
    }

    public void Update() {
        if( isInteractable() ) {
            EnableInteraction();
            if(Input.GetKeyDown(KeyCode.E)){
            // if(Input.GetKey(KeyCode.E)){
                Interact();
            } 
        } else {
            DisableInteraction();
        }
        // Dialogue
        if( dialogueWindow != null && dialogueWindow.isDialogueFinished ){
            onDialogueFinished();
        }
    }

    public virtual void onDialogueFinished(){
        isDialogueFinished = true;
    }

    void setOutlineColorModes(){
        outlineColorModes = new Dictionary<string, Color>();
        outlineColorModes["unactive"] = new Color(0, 0, 0);
        outlineColorModes["interactable"] = new Color(144, 3, 219);
        outlineColorModes["enabled"] = new Color(255, 255, 0);
        outlineColorModes["hover"] = new Color(0, 255, 0);
        outlineColorModes["active"] = new Color(255, 0, 0);
    }

    void setObjectProperties(){
        center = colliderBounds.center;
        Debug.Log(center);
        radius = Mathf.Max(colliderBounds.size.x, colliderBounds.size.z) * 1.75f;
    }
    public Vector3 GetCenter(){
        return center;
    }
    public virtual bool isInteractable(){
        return isPlayerInRadius();
    }

    bool isPlayerInRadius(){
        float distanceToMainCharacter = Vector3.Distance(center, mainCharacterTransform.position);
        return ( radius >= distanceToMainCharacter );
    }

    public void EnableInteraction() {
        // Debug.Log("enabled");
        EnableOutline("enabled");
    }
    public void Hover() {
        // Debug.Log("hovered");
        EnableOutline("hover");
    }
    public virtual void Interact() {
        // Debug.Log("interacting");
        EnableOutline("active");
    }
    public virtual void DisableInteraction(){
        if( isPrerequisitesCompleted ){
            EnableOutline("interactable");
        }
        else{
            EnableOutline("unactive");
        }
        // DisableOutline();
    }
    public void StopSound(){
        if(sound)
            sound.Stop();
    }
    public void PlaySound(){
        if(sound){
            if( !sound.isPlaying )
                sound.Play();
        }
    }
    public void EnableOutline(string state) {
        if(outliner != null){
            outliner.enabled = true; 
            outliner.OutlineMode = Outline.Mode.OutlineVisible;
            SetOutlineColor(state); 
        }
    }
    public void DisableOutline() {
        if(outliner != null)
            outliner.enabled = false; 
    }
    void SetOutlineColor(string state){
        if(outliner != null)
            outliner.OutlineColor = outlineColorModes[state];   
    }    
    
    public void SetPrerequisitesCompleted(){
        EnableOutline("interactable");
        isPrerequisitesCompleted = true;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(center, radius);
    }
}