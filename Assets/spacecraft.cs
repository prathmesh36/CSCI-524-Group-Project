using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class spacecraft : MonoBehaviour
{
    public Transform[] Targets; 
    private Transform currentTarget;
    private bool isMovingStraight = false; 
    private int count = 0;
    private bool isLeft = true;
    private float straightMoveTimer = 0f;
    public GameObject AlienShip1;
    public GameObject AlienShip2;

    void Start()
    {
        currentTarget = Targets[0]; 
    }

    void Update()
    {
        
        CheckPlanetProximity();

        if (isMovingStraight)
        {
            straightMoveTimer += Time.deltaTime;
            if (isLeft)
            {
                transform.Translate(Vector3.left * Time.deltaTime * 40f);
            }
            else
            {
                transform.Translate(Vector3.right * Time.deltaTime * 40f);
            }
            if (straightMoveTimer >= 1f)
            {
                // Implement your pause logic here (e.g., show a pause menu)
                SceneManager.LoadScene("GameOver");
            }
            if (AlienShip1 == null)
            {
                //Debug.Log("Alienship1");
            }
            if (AlienShip2 == null)
            {
                //Debug.Log("Alienship2");
            }
          
            if(AlienShip1 == null && AlienShip2 == null)
            {
                //SceneManager.LoadScene("YouWon");
            }

        }
        else
        {
          
            transform.RotateAround(currentTarget.position, Vector3.forward, 100 * Time.deltaTime);
        }

     
        if (count>8){
            SceneManager.LoadScene("GameOver");
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow)){
            isLeft = true;
            isMovingStraight = !isMovingStraight;
            count++;
            //Debug.Log("Count is"+count);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)){
            isLeft = false;
            isMovingStraight = !isMovingStraight;
            count++;
            //Debug.Log("Count is" + count);
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
                break;
            }
        }
    }
}
