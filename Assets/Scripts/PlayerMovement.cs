using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float rotHorVel;
    [SerializeField] float rotVerVel;
    [SerializeField] float movXVel;
    [SerializeField] float movZVel;
    [SerializeField] float gravity;
    [SerializeField] bool grounded;
    [SerializeField] float radius;
    [SerializeField] Transform cam;
    [SerializeField] LayerMask groundLayerMask;
    [SerializeField] Transform groundCheck;
    [SerializeField] CharacterController cC;
    [SerializeField] GameLogic gameLogic;
    [SerializeField] AudioClip step;
    AudioSource aS;
    Vector3 gravityVelocity;
    Vector3 axisMovement;
    float mouseX, mouseY;
    float cameraX;
    float moveX, moveZ;
    bool moving,playing;
    // Start is called before the first frame update
    void Start()
    {
       aS = GetComponent<AudioSource>();
        cC = GetComponent<CharacterController>();
        gravity = -9.81f;
        grounded = false;
        moving = false;
        playing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameLogic.active)
        {
            //check if on the ground
            if(Physics.CheckSphere(groundCheck.position, radius,groundLayerMask)) 
            {
                grounded = true;
            }
            else
            {
                grounded = false;
            }

            //if not on ground apply gravity
            if(!grounded)
            {
                gravityVelocity.y += gravity * Time.deltaTime;
            }
            else
            {
                gravityVelocity.y = 0;
            }
            
            //apply all movement
            PlayerRotation();
            moving = false;
            if(gameLogic.movable)
            {
                PlayerMotion();
            }
            if(moving && !playing)
            {
                playing = true;
                InvokeRepeating(nameof(PlayStep), 0, 0.35f);
            }
            else if(!moving)
            {
                playing = false;
                CancelInvoke();
            }
           
        }

        else
        {
            
        }

    }

   void PlayStep()
    {
        aS.PlayOneShot(step);
    }

    void PlayerMotion()
    {
        //player movement
        moveX = Input.GetAxis("Horizontal") * movXVel * Time.deltaTime;
        moveZ = Input.GetAxis("Vertical") * movZVel * Time.deltaTime;
        axisMovement = transform.forward * moveZ + transform.right * moveX; //local movment in local axis "transform.forward its local z direction
        if(axisMovement.magnitude > 0)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }
        cC.Move(axisMovement);
        cC.Move(gravityVelocity * Time.deltaTime);

    }

    void PlayerRotation()
    {
        //look movment
        mouseX = Input.GetAxis("Mouse X") * rotHorVel * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * rotVerVel * Time.deltaTime * (-1);
        transform.Rotate(0, mouseX, 0);
        cameraX = Mathf.Clamp(cameraX + mouseY, -90, 85);
        cam.localRotation = Quaternion.Euler(cameraX, 0, 0);

    }
}
