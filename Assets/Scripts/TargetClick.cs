using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TargetClick : NetworkBehaviour
{
    public PlayerManager PlayerManager;
    public void OnTargetClick()
    {
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();
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
