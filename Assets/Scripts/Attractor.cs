
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    public LayerMask AttractionLayer;
    public float gravity=10;
    [SerializeField] private float Radius=10;
    public List<Collider2D> AttractedObjects=new List<Collider2D>();
    [HideInInspector] public Transform attractorTransform;
    public bool flag;

    void Awake()
    {
        attractorTransform=GetComponent<Transform>();
    }
    // Start is called before the first frame update
    void Update()
    {
        if (flag == true)
        {
            SetAttractedObjects();
        }
        else {
            SetAttractedObjectsRect();
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        AttractObjects();
    }

    void SetAttractedObjects()
    {
        AttractedObjects=Physics2D.OverlapCircleAll(attractorTransform.position,Radius,AttractionLayer).ToList();
    }

    void SetAttractedObjectsRect()
    {
        // Calculate the bounds of the rectangle
        Vector2 halfSize = new Vector2(Radius, Radius * 2); // Adjust the size as needed
        Vector2 center = attractorTransform.position;

        // Create a list of attracted objects using OverlapBoxAll
        AttractedObjects = Physics2D.OverlapBoxAll(center, halfSize, 0f, AttractionLayer).ToList();
    }

    void AttractObjects() 
    {
        for(int i=0;i<AttractedObjects.Count;i++)
        {
            AttractedObjects[i].GetComponent<Attractable>().Attract(this);
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color=Color.magenta;
        Gizmos.DrawWireSphere(transform.position,Radius);
    }
}
