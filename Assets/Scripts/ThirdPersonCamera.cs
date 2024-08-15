using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
       public float sensitivity;


          // How close/far camera should be to anchor
       public float cameraZoom;


       public float verticalRotationMin;
       public float verticalRotationMax;


       public Transform playerTransform;


       private float currentRotationVertical;
       private float currentRotationHorizontal;


          // How far camera actually is from anchor at this moment
       private float currentCameraZoom;


       private Transform boomTransform;

       private Transform cameraTransform;


       public LayerMask avoidLayer;

    // Start is called before the first frame update
    void Start()
       {
              // Will get first child of "ThirdPersonAnchor"
           boomTransform = transform.GetChild(0);

              // Will get first child from "boomTransform" object
           cameraTransform = boomTransform.GetChild(0);


              // Set current camera zoom to default
           currentCameraZoom = cameraZoom;

              // Get initial camera rotation
           currentRotationHorizontal = transform.localEulerAngles.y;

           currentRotationVertical = boomTransform.localEulerAngles.x;
       }

    // Update is called once per frame
    void Update()
       {
           currentRotationHorizontal += Input.GetAxis("Mouse X") * sensitivity;
           currentRotationVertical -= Input.GetAxis("Mouse Y") * sensitivity;

           currentRotationVertical = Mathf.Clamp(currentRotationVertical, verticalRotationMin, verticalRotationMax);


           transform.position = playerTransform.position;


           transform.localEulerAngles = new Vector3(0, currentRotationHorizontal, 0);

           boomTransform.localEulerAngles = new Vector3(currentRotationVertical, 0, 0);


              // Direction from Point A to Point B = B - A
           Vector3 directionToCamera = cameraTransform.position - transform.position;


           directionToCamera.Normalize();


           if (Physics.Raycast(transform.position, directionToCamera, out RaycastHit hit, cameraZoom, avoidLayer))
              {
                 currentCameraZoom = hit.distance;
              }

           else
              {
                 currentCameraZoom = cameraZoom;
              }


              // Position relative to parent's location
              // "-currentCameraZoom" to ensure is behind player
           cameraTransform.localPosition = new Vector3(0, 0, -currentCameraZoom);
       }

    private void OnDrawGizmos()
       {
              // Gizmos are used to give visual debugging/setup aids in the 'Scene' view
        
        
           boomTransform = transform.GetChild(0);

           cameraTransform = boomTransform.GetChild(0);

              // Sets colour of gizmos that are drawn next
           Gizmos.color = Color.yellow;

              // Draws a line starting at 'from', towards 'to'
           Gizmos.DrawLine(transform.position, cameraTransform.position);
       }
}
