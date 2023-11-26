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
    private bool space = true;
    private float straightMoveTimer = 0f;
    public Transform destinationPlanet;
    public GameObject shield;
    public float moveDuration = 2.0f;
    GameManager gameManager;
    public GameObject healthPuzzleWincanvas;
    public GameObject fuelPuzzleWincanvas;
    public GameObject puzzleLosecanvas;
    public GameObject pipePuzzleWinCanvas;
    private bool canPressSpace = false;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private float changeRotation = 10.0f;
    [SerializeField] private int changePosition = 10;

    public GameObject instruction9;
    public bool[] instructionByPlanet = new bool[10];

    private void Awake()
    {
        gameManager = GameObject.Find("GameManagerMain").GetComponent<GameManager>();
    }


    void Start()
    {
        //Debug.Log("Inside start method of spacecraft script ");
        currentTarget = Targets[0];
        //initialRotation = transform.rotation;
        if (GameManager.initialLoad)
        {
            Debug.Log("Initial Load");
            if (destinationPlanet != null && virtualCamera != null)
            {
                // Set the position of the camera to focus on the destination planet
                virtualCamera.Follow = destinationPlanet;
                virtualCamera.transform.position = destinationPlanet.position - new Vector3(0, 0, 0); // Adjust the offset if necessary
                virtualCamera.m_Lens.OrthographicSize = 5;

                // Invoke the method to transition to follow the spaceship after 5 seconds
                Invoke("TransitionToFollowSpaceship", 2f);
            }
            else
            {
                Debug.LogError("Destination planet or camera not found.");
            }
            GameManager.initialLoad = false;
            Invoke("EnableSpaceKeyPress", 5f);
            Update();
        }

        else
        {
            Debug.Log("Non-Initial Load");
            canPressSpace=true;

            ////Unnati : Printing Player prefs
            //string[] allKeys = { "PipePuzzle", "WirePuzzle", "MagnetPuzzle", "SpaceGerms" };

            //// Print all PlayerPrefs values
            //foreach (var key in allKeys)
            //{
            //    if (PlayerPrefs.HasKey(key))
            //    {
            //        if (PlayerPrefs.GetString(key) != "")
            //        {
            //            Debug.Log($"PlayerPrefs key: {key}, value: {PlayerPrefs.GetString(key)}");
            //        }
            //        else
            //        {
            //            Debug.Log($"PlayerPrefs key: {key}, value: {PlayerPrefs.GetInt(key)}");
            //        }
            //    }
            //}
            

            int pipePuzzleValue = PlayerPrefs.GetInt("PipePuzzle", 0);
            if (pipePuzzleValue == -1)
            {
                Debug.Log("Pipe Puzzle Lost data received in Main Game");
                puzzleLosecanvas.SetActive(true);
                StartCoroutine(DeactivateCanvasAfterDelay(3f, puzzleLosecanvas));
            }
            else if (pipePuzzleValue == 1)
            {
                PlayerPrefs.SetInt("Mines", 1);
                pipePuzzleWinCanvas.SetActive(true);
                Debug.Log("Pipe Puzzle Won data received in Main Game");
                gameManager.updateFuel(-40);
                gameManager.incrementBombCount(PlayerPrefs.GetInt("MinesCollected", 0));
                StartCoroutine(DeactivateCanvasAfterDelay(3f, pipePuzzleWinCanvas));
            }
            PlayerPrefs.SetInt("PipePuzzle", 0);

            int wirePuzzleValue = PlayerPrefs.GetInt("WirePuzzle", 0);
            if (wirePuzzleValue == -1)
            {
                Debug.Log("Wire Puzzle Lost data received in Main Game");
                puzzleLosecanvas.SetActive(true);
                StartCoroutine(DeactivateCanvasAfterDelay(3f, puzzleLosecanvas));
            }
            else if (wirePuzzleValue == 1)
            {
                healthPuzzleWincanvas.SetActive(true);
                Debug.Log("Wire Puzzle Won data received in Main Game");
                gameManager.updateHealth(-20);
                shield.SetActive(true);
                StartCoroutine(DeactivateCanvasAfterDelay(3f, healthPuzzleWincanvas));
            }
            PlayerPrefs.SetInt("WirePuzzle", 0);

            int magnetPuzzleValue = PlayerPrefs.GetInt("MagnetPuzzle", 0);
            if (magnetPuzzleValue == -1)
            {
                Debug.Log("Magnet Puzzle Lost data received in Main Game");
                puzzleLosecanvas.SetActive(true);
                StartCoroutine(DeactivateCanvasAfterDelay(3f, puzzleLosecanvas));
            }
            else if (magnetPuzzleValue == 1)
            {
                fuelPuzzleWincanvas.SetActive(true);
                Debug.Log("Magnet Puzzle Won data received in Main Game");
                gameManager.updateFuel(-40);
                StartCoroutine(DeactivateCanvasAfterDelay(3f, fuelPuzzleWincanvas));
            }
            PlayerPrefs.SetInt("MagnetPuzzle", 0);

            int spaceGermsValue = PlayerPrefs.GetInt("SpaceGerms", 0);
            if (spaceGermsValue == -1)
            {
                Debug.Log("Space Germs Lost data received in Main Game");
                puzzleLosecanvas.SetActive(true);
                StartCoroutine(DeactivateCanvasAfterDelay(3f, puzzleLosecanvas));
            }
            else if (spaceGermsValue == 1)
            {
                Debug.Log("The Space Germs Won data received in Main Game");
                Debug.Log("Unnati: calling from the (Main game) and I am checking the value of playerpref " + PlayerPrefs.GetInt("SpaceGerms"));

                healthPuzzleWincanvas.SetActive(true);

                gameManager.updateHealth(-20);
                // Deactivate the canvas after 3 seconds
                StartCoroutine(DeactivateCanvasAfterDelay(3f, healthPuzzleWincanvas));
            }
            PlayerPrefs.SetInt("SpaceGerms", 0);



            transform.position = Targets[GameManager.currentPlanet].position + new Vector3(1.0f, 1.0f, 0);
            gameManager.updateFuel(0);
            gameManager.updateHealth(0);
        }

    }

    
    void EnableSpaceKeyPress()
    {
       canPressSpace = true;
    }

    public void MoveCamera()
    {
        //Debug.Log("Inside move camera");
        StartCoroutine(MoveCameraCoroutine(transform.position, moveDuration));
    }

    private IEnumerator MoveCameraCoroutine(Vector3 targetPosition, float duration)
    {
        virtualCamera.m_Lens.OrthographicSize = 8;
        Vector3 startPosition = destinationPlanet.position;
        float startTime = Time.time;

        while (Time.time < startTime + duration)
        {
            //Debug.Log("Inside MoveCameraCoroutine");
            float t = (Time.time - startTime) / duration;
            destinationPlanet.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null; // Wait for the next frame
        }

        // Ensure the camera reaches the exact target position at the end.
        destinationPlanet.position = targetPosition;
        virtualCamera.Follow = transform;
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
            //Vector3 objectPosition = transform.position;
            //Debug.Log("Object's coordinates: " + objectPosition);
            if (space)
            {
                float movementSpeed = 30f; // Adjust the movement speed as needed
                Vector3 tipDirection = -transform.up;
                transform.Translate(tipDirection * Time.deltaTime * movementSpeed, Space.World);
                float distanceThisFrame = Time.deltaTime * movementSpeed;
                //Debug.Log(distanceThisFrame);
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
            float rotationSpeed = 60f;

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
        if (canPressSpace && Input.GetKeyDown(KeyCode.Space))
        {

            //Debug.Log("Space Key Pressed");
            // Move the spaceship in the direction of its tip
            space = true;
            isMovingStraight = !isMovingStraight;
            //float movementSpeed = 20f; // Adjust the movement speed as needed
            //Vector3 tipDirection = -transform.up;
            //transform.Translate(tipDirection * Time.deltaTime * movementSpeed, Space.World);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            if (gameManager.getBombCount() > 0)
            {
                gameManager.decrementBombCount(1);
                Quaternion currentRotation = firingPoint.rotation;
                Quaternion rotationDelta = Quaternion.Euler(0, changeRotation, 0);
                Quaternion newRotation = currentRotation * rotationDelta;
                Vector3 forwardDirection = firingPoint.forward;
                Debug.Log("Shooting Logs");
                Debug.Log(currentRotation);
                Debug.Log(newRotation);
                Instantiate(bulletPrefab, firingPoint.position + forwardDirection * changePosition, newRotation);
            }
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
                //Debug.Log("Rotation of the spaceship" + transform.rotation);
                break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Planet 9") && !instructionByPlanet[8] && PlayerPrefs.GetInt("Mines") == 1)
        {
            Invoke("Instruction9Caller", 4f);
            instructionByPlanet[8] = true;
        }
    }


    private void Instruction9Caller()
    {
        if (PlayerPrefs.GetInt("Mines") == 1)
        {
            Time.timeScale = 0f;
            instruction9.SetActive(true);
            //instruction1Text.text = "Avoid Asteriod as hitting it can reduce health";
        }

    }

    IEnumerator DeactivateCanvasAfterDelay(float delay, GameObject canvas)
    {
        yield return new WaitForSeconds(delay);
        canvas.SetActive(false);
    }

}
