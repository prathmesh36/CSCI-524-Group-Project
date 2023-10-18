using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChange : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    public string sceneName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision Out");
        if (collision.gameObject.tag == "Spacecraft")
        {
            Debug.Log("Collision In");
            SceneManager.LoadScene(sceneName);
            //StartCoroutine(LoadScene(sceneName));
        }
    }

    //IEnumerator LoadScene(string sceneName)
    //{
    //    transition.SetTrigger("Start");

    //    yield return new WaitForSeconds(transitionTime);

    //    SceneManager.LoadScene(sceneName);
    //}
}
