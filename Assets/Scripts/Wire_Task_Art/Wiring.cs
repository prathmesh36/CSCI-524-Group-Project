using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Wiring : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer wireEnd;
    public GameObject lightOn;
    Vector3 startPoint;
    Vector3 startPosition;
    void Start()
    {
        startPoint = transform.parent.position;
        startPosition = transform.position;
    }

    // Update is called once per frame
    private void OnMouseDrag()
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0;
        //check for nearby connection points
        Collider2D[] colliders = Physics2D.OverlapCircleAll(newPosition, .2f);
        foreach(Collider2D collider in colliders)
        {
            //make sure not my own collider
            if (collider.gameObject != gameObject)
            {
                //update wire to the connection point position
                UpdateWire(collider.transform.position);
                if (transform.parent.name.Equals(collider.transform.parent.name))
                {
                    //count connection
                    Main.Instance.SwitchChange(1);
                    //finish step
                    collider.GetComponent<Wiring>()?.Done();
                    Done();
                }
                return;
            }
        }
        UpdateWire(newPosition);

    }
    void Done()
    {
        //turn on lights
        lightOn.SetActive(true);
        //destroy the script
        Destroy(this);
        SceneManager.LoadScene("MainMenu");
    }
    private void OnMouseUp()
    {
        UpdateWire(startPosition);

    }
    void UpdateWire(Vector3 newPosition)
    {
        transform.position = newPosition;
        Vector3 direction = newPosition - startPoint;
        transform.right = direction * transform.lossyScale.x;

        float dist = Vector2.Distance(startPoint, newPosition);
        wireEnd.size = new Vector2(dist, wireEnd.size.y);
    }
}
