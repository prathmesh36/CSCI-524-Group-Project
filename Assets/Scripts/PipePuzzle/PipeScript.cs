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
            pipePuzzleGameManager.destroyPipes(gameObject.name.Substring(gameObject.name.Length - 2));
        }

        transform.Rotate(new Vector3(0, 0, 90));
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Round(transform.eulerAngles.z));
        Debug.Log(transform.eulerAngles.z);
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