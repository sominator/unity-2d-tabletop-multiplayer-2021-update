using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

//this script demonstrates the usage of a TargetRpc to specify remote action on one client, rather than a ClientRpc, which would initiate actions on all clients
public class TargetClick : NetworkBehaviour
{
    public PlayerManager PlayerManager;

    //OnTargetClick() is called by the PointerDown event on the Event Trigger component attached to this gameobject
    public void OnTargetClick()
    {
        //locate the PlayerManager in this Client
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();

        //if this client hasAuthority over this gameobject, we don't need to pass in the gameobject to the server command. If it doesn't, we do!
        if (hasAuthority)
        {
            PlayerManager.CmdTargetSelfCard();
        }
        else
        {
            PlayerManager.CmdTargetOtherCard(gameObject);
        }
    }
}
