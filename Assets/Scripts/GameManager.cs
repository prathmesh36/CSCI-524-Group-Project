using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public static long startTime = 0;
    public static long endTime = 0;

    public static float fuel = 45;
    public static float health = 40;

    public Transform[] Target;
    public string sceneName;
    public Animator transition;
    public float transitionTime = 1f;
    public GameObject bar;
    public GameObject hbar;
    public static int currentPlanet = 0;
    public static bool initialLoad = true;
    public static bool[] boolArray = new bool[10];
    public static string lostCause = "N/A";

    void Start()
    {
        float minDistance = CalculateMinimumDistance(Target);
        Debug.Log("Minimum distance to cover all targets: " + minDistance);
    }


    void Update()
    {
        
    }

    public void updateFuel(float value) {
        fuel -= value;
        LeanTween.scaleX(bar, Math.Max(Math.Min(fuel/100,1), 0), 1);
        if (fuel <= 0) {
            GameManager.lostCause = "Fuel Over";
            StartCoroutine(LoadScene("FuelOver"));
        }
    }


    public void updateHealth(float value)
    {
        health -= value;

        LeanTween.scaleX(hbar, Math.Max(Math.Min(health / 100, 1), 0), 1);
        Debug.Log("Unnati: Health after updation:" + health);

        if (health <= 0)
        {
            GameManager.lostCause = "Health Over";
            StartCoroutine(LoadScene("HealthOver"));
        }
    }




    IEnumerator LoadScene(string sceneName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneName);
    }

    float CalculateMinimumDistance(Transform[] targetTransforms)
    {
        float minDistance = float.MaxValue;
        int[] perm = new int[targetTransforms.Length];
        for (int i = 0; i < targetTransforms.Length; i++)
        {
            perm[i] = i;
        }

        do
        {
            float distance = 0f;
            for (int i = 0; i < targetTransforms.Length - 1; i++)
            {
                distance += Vector3.Distance(targetTransforms[perm[i]].position, targetTransforms[perm[i + 1]].position);
            }
            distance += Vector3.Distance(targetTransforms[perm[targetTransforms.Length - 1]].position, targetTransforms[perm[0]].position);
            if (distance < minDistance)
            {
                minDistance = distance;
            }
        } while (NextPermutation(perm));

        return minDistance;
    }

    bool NextPermutation(int[] array)
    {
        int i = array.Length - 1;
        while (i > 0 && array[i - 1] >= array[i])
        {
            i--;
        }

        if (i <= 0)
        {
            return false;
        }

        int j = array.Length - 1;
        while (array[j] <= array[i - 1])
        {
            j--;
        }

        int temp = array[i - 1];
        array[i - 1] = array[j];
        array[j] = temp;

        j = array.Length - 1;
        while (i < j)
        {
            temp = array[i];
            array[i] = array[j];
            array[j] = temp;
            i++;
            j--;
        }

        return true;
    }
}
