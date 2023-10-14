using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PipePuzzleGameManager : MonoBehaviour
{
    public GameObject PipesHolder;
    public GameObject WaterPipesHolder;
    public GameObject[] Pipes;
    public GameObject[] WaterPipes;

    [SerializeField]
    int totalPipes = 0;

    [SerializeField]
    int correctPipes = 0;

    // Start is called before the first frame update
    void Start()
    {
        totalPipes = PipesHolder.transform.childCount;

        Pipes = new GameObject[totalPipes];
        WaterPipes = new GameObject[totalPipes];

        for (int i = 0; i < Pipes.Length; i++){
            Pipes[i] = PipesHolder.transform.GetChild(i).gameObject;
            WaterPipes[i] = WaterPipesHolder.transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    public void CorrectMove()
    {
        correctPipes += 1;
        Debug.Log("Correct Move");
        if (correctPipes == totalPipes) {
            Debug.Log("You Won");
            startWater();
        }
    }

    public void startWater() {
        for (int i = 0; i < Pipes.Length; i++)
        {
            SpriteRenderer spriteRenderer = Pipes[i].GetComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = -1;
            SpriteRenderer waterSpriteRenderer = WaterPipes[i].GetComponent<SpriteRenderer>();
            waterSpriteRenderer.sortingOrder = 1;
            SceneManager.LoadScene("MyGame");
        }
    }

    public void WrongMove() {
        correctPipes -= 1;
        Debug.Log("Wrong Move");
    }
}
