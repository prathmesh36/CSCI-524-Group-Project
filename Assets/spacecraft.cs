using System;
using System.Collections;
using Cinemachine;
using UnityEngine;

public class spacecraft : MonoBehaviour
{
    public Transform[] Targets; 
    private Transform currentTarget;
    public CinemachineVirtualCamera virtualCamera;
    private bool isMovingStraight = false; 
    //private int count = 0;
    private bool space = true;
    private float straightMoveTimer = 0f;
    private Quaternion initialRotation= Quaternion.Euler(0, 0, 180);

    //public GameObject AlienShip1;
    //public GameObject AlienShip2;

    public Transform destinationPlanet;

    //private bool cameraMoved = false;

    public float moveDuration = 2.0f;
    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManagerMain").GetComponent<GameManager>();
    }


    void Start()
    {
        Debug.Log("Inside start method of spacecraft script ");
        currentTarget = Targets[0];
        //initialRotation = transform.rotation;

        if (destinationPlanet != null && virtualCamera != null)
        {
            // Set the position of the camera to focus on the destination planet
            virtualCamera.Follow = destinationPlanet;
            virtualCamera.transform.position = destinationPlanet.position - new Vector3(0, 0, 0); // Adjust the offset if necessary
            virtualCamera.m_Lens.OrthographicSize = 5;

            // Invoke the method to transition to follow the spaceship after 5 seconds
            Invoke("TransitionToFollowSpaceship", 5f);
        }
        else
        {
            Debug.LogError("Destination planet or camera not found.");
        }
    }


    public void MoveCamera()
    {
        Debug.Log("Inside move camera");
        StartCoroutine(MoveCameraCoroutine(transform.position, moveDuration));
    }

    private IEnumerator MoveCameraCoroutine(Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = destinationPlanet.position;
        float startTime = Time.time;

        while (Time.time < startTime + duration)
        {
            Debug.Log("Inside MoveCameraCoroutine");
            float t = (Time.time - startTime) / duration;
            destinationPlanet.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null; // Wait for the next frame
        }

        // Ensure the camera reaches the exact target position at the end.
        destinationPlanet.position = targetPosition;
        virtualCamera.Follow = transform;
        virtualCamera.m_Lens.OrthographicSize = 7;
    }

    void TransitionToFollowSpaceship()
    {
        // Smoothly transition to follow the spacecraft
        //CinemachineBrain cinemachineBrain = virtualCamera.GetComponent<CinemachineBrain>();
        //if (cinemachineBrain != null)
        //{
        //    cinemachineBrain.m_DefaultBlend.m_Time = 50f; // Adjust the blend time as needed
        //}

        MoveCamera();

        // Set the camera to follow the spacecraft
        // Replace this line with your actual logic for camera follow
        //Debug.Log("Camera transition to follow spacecraft");
        //virtualCamera.Follow = transform;
        ////cameraMoved = true;
        //virtualCamera.m_Lens.OrthographicSize = 10;
    }


    void Update()
    {
        //Debug.Log("Inside update method of spacecraft script");
        

        CheckPlanetProximity();

        if (isMovingStraight)
        {
            //To do check if the spaceship moves out of bounds then make it stick to a nearby planet
            //float translationSpeed = 20f;
            straightMoveTimer += Time.deltaTime;
            Vector3 objectPosition = transform.position;
            //Debug.Log("Object's coordinates: " + objectPosition);
            if (space)
            {
                float movementSpeed = 50f; // Adjust the movement speed as needed
                Vector3 tipDirection = -transform.up;
                transform.Translate(tipDirection * Time.deltaTime * movementSpeed, Space.World);
                float distanceThisFrame = Time.deltaTime * movementSpeed;
                Debug.Log(distanceThisFrame);
                gameManager.updateFuel(distanceThisFrame);

            }
            
            if (straightMoveTimer >= 1f)
            {
                // Implement your pause logic here (e.g., show a pause menu)
                //SceneManager.LoadScene("GameOver");
                //Debug.Log("Game Over scene. I am here");

            }
            

        }
        else
        {
            float rotationSpeed = 100f;

            transform.RotateAround(currentTarget.position, Vector3.forward, -rotationSpeed * Time.deltaTime);
        }


        //if (Input.GetKeyDown(KeyCode.LeftArrow)){
        //    isLeft = true;
        //    isMovingStraight = !isMovingStraight;

        //}
        //else if (Input.GetKeyDown(KeyCode.RightArrow)){
        //    isLeft = false;
        //    isMovingStraight = !isMovingStraight;

        //}
        if (Input.GetKeyDown(KeyCode.A))
        {

            Debug.Log("Space Key Pressed");
            // Move the spaceship in the direction of its tip
            space = true;
            isMovingStraight = !isMovingStraight;
            //float movementSpeed = 20f; // Adjust the movement speed as needed
            //Vector3 tipDirection = -transform.up;
            //transform.Translate(tipDirection * Time.deltaTime * movementSpeed, Space.World);
        }

    }

    void CheckPlanetProximity()
    {
      
        float proximityDistance = 2.0f;
    
        foreach (Transform planet in Targets)
        {
            float distance = Vector3.Distance(transform.position, planet.position);
          
            if (distance < proximityDistance && currentTarget!=planet)
            {
                currentTarget = planet;
                isMovingStraight = false;
                
                // Calculate the direction from the spaceship to the planet
                Vector3 newDirection = planet.position - transform.position;

                // Set the rotation to point outwards from the planet
                transform.rotation = Quaternion.LookRotation(Vector3.forward, newDirection);
                Debug.Log("Rotation of the spaceship" + transform.rotation);
                break;
            }
        }
    }
}
