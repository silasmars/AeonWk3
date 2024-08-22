using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Enumeration (enum) is a way to represent list of possible values (usually words) as numbers
// E.g. four types of property data: 'For Sale', 'Recently Sold', 'For Rent', and 'Land'
public enum CameraMode
{
    FirstPerson, // 0
    ThirdPerson  // 1
}


public class CameraController : MonoBehaviour
   {

      private FirstPersonCamera firstPersonCamera;
      private ThirdPersonCamera thirdPersonCamera;

      private MoveCustom player;


         // SerializeField allows to view & edit private variables in Unity inspector
            // Serialize = convert to readable data
            // Field = the variable
      [SerializeField] private CameraMode currentCameraMode;


         // Start is called before the first frame update
      void Start()
         {
               // Lock cursor to centre of screen
            Cursor.lockState = CursorLockMode.Locked;
               // Make cursor invisible
            Cursor.visible = false;


               // FindObjectOfType will look for first instance of provided type in scene
                   // Does NOT look in inactive game objects by default
                      // Can pass "true" inside parentheses at end of line to include inactive objects
            firstPersonCamera = FindObjectOfType<FirstPersonCamera>(true);
            thirdPersonCamera = FindObjectOfType<ThirdPersonCamera>(true);

            player = FindObjectOfType<MoveCustom>(true);


            SetCameraMode();
         }


         // Update is called once per frame
      void Update()
         {
               // KeyCode is enumerator which contains all keyboard keys
            if (Input.GetKeyDown(KeyCode.C))
               {
                  ToggleCameras();
               }
         }


         // Swap from crrent camera control, to other camera control
      private void ToggleCameras()
         {
            if (currentCameraMode == CameraMode.FirstPerson)
               {
                  currentCameraMode = CameraMode.ThirdPerson;
               }

            else
               {
                  currentCameraMode = CameraMode.FirstPerson;
               }

            SetCameraMode();

         }


         // Apply camera mode to game
      private void SetCameraMode()
         {
               // Checking if current camera mode is in First Person  
            if (currentCameraMode == CameraMode.FirstPerson)
               {
                     // Turn on first person camera's game object
                  firstPersonCamera.gameObject.SetActive(true);

                     // Ensure third person camera's game object is off
                  thirdPersonCamera.gameObject.SetActive(false);
               }

               // If current camera mode is not in First Person
            else
               {
                     // Turn on third person camera's game object
                  thirdPersonCamera.gameObject.SetActive(true);

                     // Ensure first person camera's game object is off
                  firstPersonCamera.gameObject.SetActive(false);
               }


            player.SetCamera(currentCameraMode);
         }

   }
