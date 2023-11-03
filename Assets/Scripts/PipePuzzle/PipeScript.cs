using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeScript : MonoBehaviour
{
    float[] rotations = { 0, 90, 180, 270 };

    public float[] correctRotation;

    [SerializeField]
    bool isPlaced = false;

    [SerializeField]
    bool isMine = false;

    PipePuzzleGameManager pipePuzzleGameManager;
    public GameObject explosion;

    private void Awake()
    {
        pipePuzzleGameManager = GameObject.Find("GameManager").GetComponent<PipePuzzleGameManager>();
    }

    private void Start()
    {
        int rand = Random.Range(0, rotations.Length);
        transform.eulerAngles = new Vector3(0, 0, rotations[rand]);

        for (int i = 0; i < correctRotation.Length; i++) {
            if (transform.eulerAngles.z == correctRotation[i])
            {
                isPlaced = true;
                pipePuzzleGameManager.CorrectMove(gameObject.name.Substring(gameObject.name.Length - 2));
            }
        }
    }

    private void OnMouseDown()
    {
        if (isMine == true) {
            Debug.Log("This is Mine");

            int mineClicks = PlayerPrefs.GetInt(Constants.TOTAL_MINES_CLICKS);
            ++mineClicks;
            //Debug.Log(string.Format("Mines encountered!: {0}", mineClicks));  
            PlayerPrefs.SetInt(Constants.TOTAL_MINES_CLICKS, mineClicks);

            pipePuzzleGameManager.destroyPipes(gameObject.name.Substring(gameObject.name.Length - 2));
            GameObject expl = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
            Destroy(expl, 3);
        }

        transform.Rotate(new Vector3(0, 0, 90));
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Round(transform.eulerAngles.z));
        Debug.Log(transform.eulerAngles.z);


        // Increment mouse clicks on every angle rotation
        int mouseClicks = PlayerPrefs.GetInt(Constants.TOTAL_MOUSE_CLICKS);
        ++mouseClicks;
        PlayerPrefs.SetInt(Constants.TOTAL_MOUSE_CLICKS, mouseClicks);
        //Debug.Log(string.Format("Total Mouse Clicks so far: {0}", mouseClicks));   

        if (isPlaced == false){
            for (int i = 0; i < correctRotation.Length; i++)
            {
                if (transform.eulerAngles.z == correctRotation[i])
                {
                    isPlaced = true;
                    pipePuzzleGameManager.CorrectMove(gameObject.name.Substring(gameObject.name.Length - 2));
                }
            }
        }
        else if (isPlaced == true)
        {
            bool wrongFlag = false;
            for (int i = 0; i < correctRotation.Length; i++)
            {
                if (transform.eulerAngles.z == correctRotation[i])
                {
                    wrongFlag = true;
                }
            }

            if (wrongFlag == false)
            {
                isPlaced = false;
                pipePuzzleGameManager.WrongMove(gameObject.name.Substring(gameObject.name.Length - 2));
            }
            
        }
    }
}