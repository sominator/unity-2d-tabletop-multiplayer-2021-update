using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameManager : NetworkBehaviour
{
    //This simple GameManager script is attached to a Server-only game object, demonstrating how to implement game logic tracked by the Server
    public int TurnsPlayed = 0;
    public void UpdateTurnsPlayed()
    {
        TurnsPlayed++;
    }
}
