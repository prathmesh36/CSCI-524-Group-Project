using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChange : MonoBehaviour
{
    public Animator transition;
    public float transitionTIme = 1f;

    [SerializeField] private string sceneName;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Spacecraft")
        {
            StartCoroutine(LoadScene(sceneName));
        }
    }

    IEnumerator LoadScene(string sceneName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTIme);

        SceneManager.LoadScene(sceneName);
    }
}
