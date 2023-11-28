using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SceneChange : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    public int blackholePenalty = -40;
    public int asteroidPenalty = -20;
    public int planetPenalty = 50;
    public int infiniteSpacePenalty = -80;

    public string sceneName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision Out");
        Debug.Log(collision.gameObject.tag);
        if (transform.name.Contains("border")) {
            int countInfinity = PlayerPrefs.GetInt(Constants.COUNT_INFINITY);
            ++countInfinity;
            PlayerPrefs.SetInt(Constants.COUNT_INFINITY, countInfinity);

            // Analytics - Update Fuel level since spaceship reached infinity
            int currentFuel = PlayerPrefs.GetInt(Constants.TOTAL_FUEL);
            currentFuel = currentFuel + infiniteSpacePenalty;
            PlayerPrefs.SetInt(Constants.INFINITE_SPACE_IMPACT, currentFuel < 0 ? 0 : currentFuel);
            GameManager.lostCause = "Reached Infinity";
        }

        if (transform.name.Contains("Galaxy"))
        {
            int countBlackhole = PlayerPrefs.GetInt(Constants.COUNT_BLACKHOLE);
            ++countBlackhole;
            PlayerPrefs.SetInt(Constants.COUNT_BLACKHOLE, countBlackhole);

            // Analytics - Update Fuel level since spaceship hit blackhole
            int currentFuel = PlayerPrefs.GetInt(Constants.TOTAL_FUEL);
            currentFuel = currentFuel + blackholePenalty;
            PlayerPrefs.SetInt(Constants.BLACKHOLE_IMPACT, currentFuel < 0 ? 0 : currentFuel );
            GameManager.lostCause = "Block Hole Collision";
        }

        if (collision.gameObject.tag == "Spacecraft")
        {
            if (GameManager.fuel > 0)
            {
                if (GameManager.health > 0)
                {
                    Debug.Log("Collision In");
                    if (transform.name.StartsWith("Planet"))
                    {

                        // Make the ShootingArrow re-appear
                        // When moving straight, we hide the ShootingArrow asset
                        try{
                            //Debug.Log("Asset is currently not visible; Enabling it.");
                            VisibilityController visController = GetComponentInChildren<VisibilityController>();
                            visController.AppearAsset();
                        }
                        catch(Exception ex){        
                            Console.WriteLine("Unexpected Error enabling it: " + ex.Message);
                        } 

                        int countPlanets = PlayerPrefs.GetInt(Constants.COUNT_PLANETS);
                        ++countPlanets;
                        PlayerPrefs.SetInt(Constants.COUNT_PLANETS, countPlanets);

                        // Analytics - Update Fuel level since spaceship hit blackhole
                        int currentFuel = PlayerPrefs.GetInt(Constants.TOTAL_FUEL);
                        currentFuel = currentFuel + planetPenalty;
                        PlayerPrefs.SetInt(Constants.PLANET_IMPACT, currentFuel < 0 ? 0 : currentFuel );
                        
                        string[] parts = transform.name.Split(' ');
                
                        if (parts.Length == 2)
                        {
                            // Attempt to parse the second part as an integer
                            if (int.TryParse(parts[1], out int result))
                            {
                                GameManager.currentPlanet = result - 1;
                                GameManager.lastFuel=GameManager.fuel;
                                if (!GameManager.boolArray[result - 1])
                                {
                                    StartCoroutine(LoadScene(sceneName));
                                    GameManager.boolArray[result - 1] = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        if(sceneName=="RestartRespawn"){
                            GameManager.currentPlanet=GameManager.currentPlanetRespawn;
                        }
                        StartCoroutine(LoadScene(sceneName));
                    }
                }
                else {
                    Debug.Log("Health Over");
                    GameManager.lostCause = "Health Over";
                    StartCoroutine(LoadScene("HealthOver"));
                }
            }
            else {
                Debug.Log("Fuel Over");
                GameManager.lostCause = "Fuel Over";
                StartCoroutine(LoadScene("FuelOver"));
            }
        }
    }

    IEnumerator LoadScene(string sceneName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneName);
    }
}
