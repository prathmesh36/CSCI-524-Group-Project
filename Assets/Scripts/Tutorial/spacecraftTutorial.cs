using System;
using System.Collections;
using Cinemachine;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class spacecraftTutorial : MonoBehaviour
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
    public GameObject instruction1;
    public TMP_Text instruction1Text;
    public Button instruction1Button;
    public bool[] instructionByPlanet = new bool[10];
    public GameObject healthPuzzleWincanvas;
    public GameObject fuelPuzzleWincanvas;
    public GameObject puzzleLosecanvas;
    public GameObject indicatorFuel;
    public GameObject indicatorHealth;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private float changeRotation = 10.0f;
    [SerializeField] private int changePosition = 10;


    private void Awake()
    {
        gameManager = GameObject.Find("GameManagerMain").GetComponent<GameManager>();
        LeanTween.init(1000);
        gameManager.incrementBombCount(1);
    }


    void Start()
    {
        Debug.Log("Inside start method of spacecraft script ");
        currentTarget = Targets[0];
        //initialRotation = transform.rotation;
        if (GameManager.initialLoad)
        {
            gameManager.updateFuel(-55);
            Debug.Log("Initial Load");
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
            GameManager.initialLoad = false;
            Update();

        }
        else {
            Debug.Log("Non-Initial Load");

            if (PlayerPrefs.GetInt("MagnetPuzzle") == -1)
            {
                Debug.Log("Magnet Puzzle Lost data received in Main Game");
                puzzleLosecanvas.SetActive(true);
                StartCoroutine(DeactivateCanvasAfterDelay(3f, puzzleLosecanvas));
            }

            if (PlayerPrefs.GetInt("WirePuzzle") == -1)
            {
                Debug.Log("Wire Puzzle Lost data received in Main Game");
                puzzleLosecanvas.SetActive(true);
                StartCoroutine(DeactivateCanvasAfterDelay(3f, puzzleLosecanvas));
            }

            if (PlayerPrefs.GetInt("PipePuzzle") == 1) {
                Debug.Log("Pipe Puzzle Won data recieved in Main Game");
                gameManager.updateFuel(-40);
            }
            PlayerPrefs.SetInt("PipePuzzle", 0);

            if (PlayerPrefs.GetInt("WirePuzzle") == 1)
            {

                Debug.Log("Wire Puzzle Won data recieved in Main Game");
                gameManager.updateHealth(-20);
                shield.SetActive(true);
                PlayerPrefs.SetInt("Shield", 1);
                PlayerPrefs.SetInt("HealthInc", 1);
                healthPuzzleWincanvas.SetActive(true);
                // Deactivate the canvas after 3 seconds
                StartCoroutine(DeactivateCanvasAfterDelay(3f, healthPuzzleWincanvas));

            }
            PlayerPrefs.SetInt("WirePuzzle", 0);

            if (PlayerPrefs.GetInt("MagnetPuzzle") == 1)
            {
                Debug.Log("Magnet Puzzle Won data recieved in Main Game");
                gameManager.updateFuel(-40);
                fuelPuzzleWincanvas.SetActive(true);
                PlayerPrefs.SetInt("FuelInc", 1);
                // Deactivate the canvas after 3 seconds
                StartCoroutine(DeactivateCanvasAfterDelay(3f, fuelPuzzleWincanvas));
            }
            PlayerPrefs.SetInt("MagnetPuzzle", 0);

            if (PlayerPrefs.GetInt("SpaceGerms") == 1)
            {
                Debug.Log("The Space Germs Won data recieved in Main Game");
                gameManager.updateHealth(-20);

            }
            PlayerPrefs.GetInt("SpaceGerms", 0);
            transform.position = Targets[GameManager.currentPlanet].position + new Vector3(1.0f, 1.0f, 0);
            gameManager.updateFuel(0);
            gameManager.updateHealth(0);
        }
    }


    public void MoveCamera()
    {
        Debug.Log("Inside move camera");
        StartCoroutine(MoveCameraCoroutine(transform.position, moveDuration));
    }

    private IEnumerator MoveCameraCoroutine(Vector3 targetPosition, float duration)
    {
        virtualCamera.m_Lens.OrthographicSize = 8;
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
        Invoke("Instruction1Caller", 1f);
        instructionByPlanet[0] = true;
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
        if (Input.GetKeyDown(KeyCode.Space))
        {

            Debug.Log("Space Key Pressed");
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

            if (distance < proximityDistance && currentTarget != planet)
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Spaceship Collision Detected");
        Debug.Log(collision.gameObject.name);

        if (collision.gameObject.name.Equals("Planet 3") && PlayerPrefs.GetInt("Shield") == 1 && !instructionByPlanet[2]) {
            Invoke("Instruction3Caller", 3f);
            instructionByPlanet[2] = true;
        } else if (collision.gameObject.name.Equals("Planet 8") && !instructionByPlanet[7])
        {
            Invoke("Instruction5Caller", 1f);
            instructionByPlanet[7] = true;
        }else if (collision.gameObject.name.Equals("Planet 9") && !instructionByPlanet[8])
        {
            Invoke("Instruction6Caller", 1f);
            instructionByPlanet[8] = true;
        }
        else if (collision.gameObject.name.Equals("Planet 2") && !instructionByPlanet[1])
        {
            Invoke("Instruction7Caller", 1f);
            instructionByPlanet[1] = true;
        }
        else if (collision.gameObject.name.Equals("Planet 4") && !instructionByPlanet[3])
        {
            Invoke("Instruction8Caller", 3f);
            instructionByPlanet[3] = true;
        }


        //if (collision.gameObject.name.StartsWith("Planet"))
        //{
        //    string[] parts = collision.gameObject.name.Split(' ');
        //    if (parts.Length == 2)
        //    {
        //        // Attempt to parse the second part as an integer
        //        if (int.TryParse(parts[1], out int result))
        //        {
        //            GameManager.currentPlanet = result - 1;
        //            GameManager.boolArray[result - 1] = true;
        //        }
        //    }

        //}
    }

    private void Instruction1Caller() {

        Debug.Log("Instruction1 Activated");
        instruction1.SetActive(true);
        Time.timeScale = 0f;
    }

    private void Instruction3Caller()
    {
        Time.timeScale = 0f;
        Debug.Log("Spaceship Collision Detected for planet three after shield" + PlayerPrefs.GetInt("Shield"));
        instruction1.SetActive(true);
        instruction1Text.text = "You spacecraft got a one time protection SHIELD from asteriod";
        PlayerPrefs.SetInt("Shield", 0);
        PlayerPrefs.SetInt("UseShield", 1);
        instruction1Button.onClick.AddListener(Instruction4Caller);
    }

    private void Instruction4Caller() {
        if (PlayerPrefs.GetInt("HealthInc") == 1)
        {
            instruction1.SetActive(true);
            instruction1Text.text = "Your Health increased";
            indicatorHealth.SetActive(true);
            Time.timeScale = 0f;
            PlayerPrefs.SetInt("HealthInc", 0);
            instruction1Button.onClick.RemoveListener(Instruction4Caller);
        }
    }

    private void Instruction5Caller()
    {
        Time.timeScale = 0f;
        instruction1.SetActive(true);
        instruction1Text.text = "Avoid Asteriod as hitting it can reduce health";
        
    }


    private void Instruction6Caller()
    {
        Time.timeScale = 0f;
        instruction1.SetActive(true);
        instruction1Text.text = "Avoid Blackhole as you'll lose if you hit it";
    }

    private void Instruction7Caller()
    {
        Time.timeScale = 0f;
        instruction1.SetActive(true);
        indicatorFuel.SetActive(true);
        instruction1Text.text = "Your spaceship is losing fuel as it moves forward";
    }

    private void Instruction8Caller()
    {
        if (PlayerPrefs.GetInt("FuelInc") == 1)
        {
            instruction1.SetActive(true);
            instruction1Text.text = "Your Fuel increased";
            indicatorFuel.SetActive(true);
            Time.timeScale = 0f;
            PlayerPrefs.SetInt("FuelInc", 0);
        }
    }

    IEnumerator DeactivateCanvasAfterDelay(float delay, GameObject canvas)
    {
        yield return new WaitForSeconds(delay);
        canvas.SetActive(false);
    }

}
