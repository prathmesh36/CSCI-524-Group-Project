using UnityEngine;

public class AsteroidCollision : MonoBehaviour
{
    public GameObject explosion;
    public GameObject obstacle;
    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManagerMain").GetComponent<GameManager>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision Out Asteriod");
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Spacecraft")
        {
            if (GameManager.fuel > 0)
            {
                Debug.Log("Collision In Asteriod");
                GameObject expl = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
                Destroy(obstacle);
                Destroy(expl, 3);
                gameManager.updateHealth(10);
            }
            else
            {

                Debug.Log("Fuel Over");
            }

        }
    }
}
