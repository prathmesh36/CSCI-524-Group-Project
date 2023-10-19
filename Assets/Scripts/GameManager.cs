using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public static long startTime = 0;
    public static long endTime = 0;

    public float fuel = 40;
    public float health = 100;

    public Transform[] Target;
    public string sceneName;
    public Animator transition;
    public float transitionTime = 1f;
    public GameObject bar;

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

        LeanTween.scaleX(bar, Math.Max(fuel/100, 0), 1);


        if (fuel <= 0) {
            StartCoroutine(LoadScene(sceneName));
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
