using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAll : MonoBehaviour
{
    private Transform dragging = null;
    private Vector3 offset;

    // Reference to the material of the object being dragged
    private Material draggingMaterial;

    // Reference to the original color of the object being dragged
    private Color originalColor;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {
                dragging = hit.transform;
                offset = dragging.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);

                // Store the material and original color of the object being dragged
                draggingMaterial = dragging.GetComponent<Renderer>().material;
                originalColor = draggingMaterial.color;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            dragging = null;
        }

        if (dragging != null)
        {
            dragging.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;

            // Check for overlapping objects
            Collider2D[] overlaps = Physics2D.OverlapBoxAll(dragging.position, dragging.localScale, 0);
            foreach (Collider2D overlap in overlaps)
            {
                if (overlap.transform != dragging)
                {
                    // Blend colors when overlapping
                    Material overlapMaterial = overlap.GetComponent<Renderer>().material;
                    Color combinedColor = Color.Lerp(originalColor, overlapMaterial.color, 0.5f);
                    draggingMaterial.color = combinedColor;
                }
            }
        }
    }
}
