using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DragDrop : NetworkBehaviour
{
    //Canvas and DropZone are assigned locally at runtime in Start(), whereas the rest are assigned contextually as this gameobject is dragged and dropped
    public GameObject Canvas;
    public GameObject DropZone;
    public PlayerManager PlayerManager;

    private bool isDragging = false;
    private bool isOverDropZone = false;
    private bool isDraggable = true;
    private GameObject dropZone;
    private GameObject startParent;
    private Vector2 startPosition;

    private void Start()
    {
        Canvas = GameObject.Find("Main Canvas");
        DropZone = GameObject.Find("DropZone");
        
        //check whether this client hasAuthority to manipulate this gameobject
        if (!hasAuthority)
        {
            isDraggable = false;
        }
    }
    void Update()
    {
        //check every frame to see if this gameobject is being dragged. If it is, make it follow the mouse and set it as a child of the Canvas to render above everything else
        if (isDragging)
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            transform.SetParent(Canvas.transform, true);
        }        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //in our scene, if this gameobject collides with something, it must be the dropzone, as specified in the layer collision matrix (cards are part of the "Cards" layer and the dropzone is part of the "DropZone" layer)
        isOverDropZone = true;
        dropZone = collision.gameObject;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isOverDropZone = false;
        dropZone = null;
    }

    //StartDrag() is called by the Begin Drag event in the Event Trigger component attached to this gameobject
    public void StartDrag()
    {
        //if the gameobject is draggable, store the parent and position of it so we know where to return it if it isn't put in a dropzone
        if (!isDraggable) return;
        startParent = transform.parent.gameObject;
        startPosition = transform.position;
        isDragging = true;
    }

    //EndDrag() is called by the End Drag event in the Event Trigger component attached to this gameobject
    public void EndDrag()
    {
        if (!isDraggable) return;
        isDragging = false;

        //if the gameobject is put in a dropzone, set it as a child of the dropzone and access the PlayerManager of this client to let the server know a card has been played
        if (isOverDropZone)
        {
            transform.SetParent(dropZone.transform, false);
            isDraggable = false;
            NetworkIdentity networkIdentity = NetworkClient.connection.identity;
            PlayerManager = networkIdentity.GetComponent<PlayerManager>();
            PlayerManager.PlayCard(gameObject);
        }
        //otherwise, send it back from whence it came
        else
        {
            transform.position = startPosition;
            transform.SetParent(startParent.transform, false);
        }
    }
}
