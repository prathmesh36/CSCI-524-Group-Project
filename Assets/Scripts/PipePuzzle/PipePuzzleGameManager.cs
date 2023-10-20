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

    List<List<string>> totalPipesList = new List<List<string>>();

    List<string> correctPipesList = new List<string>();

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

        totalPipesList.Add(new List<string> { "00", "01", "11", "21", "22", "23", "33", "34", "35" });
        totalPipesList.Add(new List<string> { "00", "01", "11", "12", "02", "03", "04", "05", "15", "25", "35" });
    }

    public void destroyPipes(string value) {
        List<List<string>> tempList = new List<List<string>>(totalPipesList);
        for (int i = 0; i < totalPipesList.Count; i++) {
            if (totalPipesList[i].Contains(value)) {
                Debug.Log("Destroying Pipes with Value");
                Debug.Log(value);
                tempList.RemoveAt(i);
            }
        }
        totalPipesList = new List<List<string>>(tempList);
        changeDestoryedPipes(value);
    }

    public void changeDestoryedPipes(string element)
    {
        for (int j = 0; j < Pipes.Length; j++)
        {
            if (Pipes[j].name.Substring(gameObject.name.Length - 2) == element)
            {
                SpriteRenderer spriteRenderer = Pipes[j].GetComponent<SpriteRenderer>();
                spriteRenderer.sortingOrder = -1;
            }
        }
    }

    // Update is called once per frame
    public void CorrectMove(string index)
    {
        correctPipesList.Add(index);
        Debug.Log("Correct Move");
        foreach (List<string> element in totalPipesList)
        {
            bool flag = true;
            foreach (string item in element)
            {
                if (!correctPipesList.Contains(item))
                {
                    flag = false;
                    break;
                }
            }
            if (flag == true) {
                Debug.Log("You Won");
                startWater(element);
                PlayerPrefs.SetInt("PipePuzzle", 1);
                SceneManager.LoadScene("YouWonMiniGame");
            }

        }
    }

    public void startWater(List<string> element) {
        for (int i = 0; i < element.Count; i++)
        {
            for (int j = 0; j < Pipes.Length; j++) {
                if (Pipes[j].name.Substring(gameObject.name.Length - 2) == element[i]) {
                    SpriteRenderer spriteRenderer = Pipes[j].GetComponent<SpriteRenderer>();
                    spriteRenderer.sortingOrder = -1;
                    SpriteRenderer waterSpriteRenderer = WaterPipes[j].GetComponent<SpriteRenderer>();
                    waterSpriteRenderer.sortingOrder = 1;
                    //SceneManager.LoadScene("MyGame");
                }
            }
           
        }
    }

    public void WrongMove(string index) {
        correctPipesList.Remove(index);
        Debug.Log("Wrong Move");
    }
}
