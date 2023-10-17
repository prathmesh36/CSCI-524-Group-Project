using System.Collections;
using UnityEngine;

public class FollowObject : MonoBehaviour
{

    public Transform target; 
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public float minDistance = 1.0f;
    public float maxDistance = 3.0f; 

    private Vector3 initialPosition; // Initial position of the camera

    void Start()
    {

        // Store the initial position of the camera
        Debug.Log("Inside start method of follow object");
        initialPosition = transform.position;
       

    }

    void LateUpdate()
    {
        if (target != null)
        {
            Debug.Log("Inside lateupdate method of follow object");
            // Calculate the desired position of the camera to center on the target
            Vector3 desiredPosition = target.position + offset;

            // Calculate the distance between the camera and target
            float distance = Vector3.Distance(transform.position, target.position);

            // Limit the camera's distance from the target
            distance = Mathf.Clamp(distance, minDistance, maxDistance);

            // Only update the camera's position if the target moves out of the camera's view
            if (distance > minDistance)
            {
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

                // Update the camera's position
                transform.position = smoothedPosition;
            }
        }
    }
   

}
