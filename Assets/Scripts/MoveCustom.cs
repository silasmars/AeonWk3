using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCustom : MonoBehaviour
   {
      [SerializeField] private CameraMode currentCameraMode;


      public Transform firstPersonCameraTransform;
      public Transform thirdPersonCameraTransform;


         // How fast should move when walking
      public float walkSpeed;
          
         // How fast should move when running
      public float runSpeed;
          
         // How fast should move when crouching
      public float crouchSpeed;
          
         // How strong jump should be
      public float jumpPower;
          
         // How strong gravity should be
      public float gravity;


         // Store how fast should move this frame
      public float speedThisFrame;

         // Store what left-right-up-down inputs are this frame  
      public Vector2 inputThisFrame;


         // Store overall movement this frame
      public Vector3 movementThisFrame;


         // Store reference to object's rigidbody
      public Rigidbody rb;


         // Define which layers are considered solid ground
      public LayerMask groundedMask;


         // Start is called before the first frame update
      void Start()
         {
               // Get rigidbody component and save in variable
            rb = GetComponent<Rigidbody>();

         }


         // Update is called once per frame
      void Update()
         {
               // Get inputs this frame
            inputThisFrame.x = Input.GetAxis("Horizontal");
            inputThisFrame.y = Input.GetAxis("Vertical");


               // Reset potential movement to 0, 0, 0
            movementThisFrame = Vector3.zero;


               // Apply new input direction left/right & forward/back
            movementThisFrame.x = inputThisFrame.x;
            movementThisFrame.z = inputThisFrame.y;


               // Figure out what speed should be this frame
            speedThisFrame = walkSpeed;


               // If "Sprint" button is held
            if (Input.GetButton("Sprint"))
               {
                  speedThisFrame = runSpeed;
               }
           
               // If "Crouch" button is held
            if (Input.GetButton("Crouch"))
               {
                  speedThisFrame = crouchSpeed;
               }


               // Multiply movement this frame by speed this frame
            movementThisFrame *= speedThisFrame;


               // Recall up/down speed are at from rigidbody, & apply gravity
            movementThisFrame.y = rb.velocity.y - gravity * Time.deltaTime;


               // Check if on ground
            if (IsGrounded())
               {
                     // If press jump button
                  if (Input.GetButton("Jump"))
                     {
                        movementThisFrame.y = jumpPower;
                     }
               }
              

               // Call our Move function
            Move(movementThisFrame);
         }


      private void Move(Vector3 movement)
         {
               // If camera mode is in third person 
            if (currentCameraMode == CameraMode.ThirdPerson)
               {
                  movement = thirdPersonCameraTransform.TransformDirection(movement);


                  Vector3 facingDirection = new Vector3(movement.x, 0, movement.z);

                  transform.forward = facingDirection;
               }
      
               // If NOT in third person
            else
               {
                     // Match left-right rotation
                  transform.localEulerAngles = new Vector3(0, firstPersonCameraTransform.localEulerAngles.y, 0);

                     // Take "global" movement direction, and convert to local direction
                  movement = transform.TransformDirection(movement);

               }
        

               // Set rigidbody's velocity using incomin movement vale
            rb.velocity = movement;
         }


      private bool IsGrounded()
         {
               // Return result of raycast (true or false)   
            return Physics.Raycast(transform.position, Vector3.down, 1.05f, groundedMask);
         }


      public void SetCamera(CameraMode mode)
         {
            currentCameraMode = mode;
         }
   }
