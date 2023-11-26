using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilityController : MonoBehaviour
{
       // Call this method to make the asset appear
    public void AppearAsset()
    {
        gameObject.SetActive(true);
    }

    // Call this method to make the asset disappear
    public void DisappearAsset()
    {
        gameObject.SetActive(false);
    }
}
