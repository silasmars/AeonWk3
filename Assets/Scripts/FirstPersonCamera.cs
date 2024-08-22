using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
   {
      public float sensitivity;


      private float currentRotationHorizontal;
      private float currentRotationVertical;


      public Transform playerTransform;


      public float playerEyeLevel = 0.5f;


      public float verticalRotationMin;
      public float verticalRotationMax;


         // Start is called before the first frame update
      void Start()
         {
            currentRotationHorizontal = transform.localEulerAngles.y;

            currentRotationVertical = transform.localEulerAngles.x;
         }

         // Update is called once per frame
      void Update()
         {
            currentRotationHorizontal += Input.GetAxis("Mouse X") * sensitivity;

               // On Unity + vertical number = up, on screen - vertical number is up
            currentRotationVertical -= Input.GetAxis("Mouse Y") * sensitivity;


               // Clamp will take a number and ensure it's between two other numbers, ie. ensure 7 is between 3 and 12  
            currentRotationVertical = Mathf.Clamp(currentRotationVertical, verticalRotationMin, verticalRotationMax);


               // Apply rotation to camera object   
            transform.localEulerAngles = new Vector3(currentRotationVertical, currentRotationHorizontal, 0);


               // Snap camera to player's eye level + position
            transform.position = playerTransform.position + Vector3.up * playerEyeLevel;
         }
   }
