using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] float interactDistance;
    [SerializeField] LayerMask hitable;
    [SerializeField] PlayerMovement player;
    [SerializeField] GameLogic gameLogic;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameLogic.active)
        {
            if(Input.GetMouseButtonDown(0))
            {
                Debug.Log("Looking to interact");
                if(Physics.Raycast(Camera.main.transform.position,Camera.main.transform.forward,out hit, interactDistance, hitable))
                {
                    Debug.Log("Found Object To Interact");
                    hit.transform.gameObject.GetComponent<BombDeffuseButton>().Interact();
         
                }
            }
        }
    }
}
