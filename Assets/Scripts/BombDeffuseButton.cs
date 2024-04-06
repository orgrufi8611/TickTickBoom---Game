using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDeffuseButton : MonoBehaviour
{
    [SerializeField] BombTickingScript bomb;
    public void Interact()
    {
        Debug.Log("Interacting with bomb");
        bomb.Interact();
    }
}
