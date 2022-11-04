using System.Collections;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LightHouseCC : MonoBehaviour
{
    public GameManager gm;

    public GameObject harborSpot, lightHousePivot, gameFinishObj, colliderSpot;

    public Image startFillBar, quitFillBar;

    [Range(0.1f, 1)]
    public float shipMovementSpeed;

    [SerializeField, Range(50, 100)]
    public float rotationSpeed;

    public int savedShips, lostShips, initialShips, shipsLeft;

    public float loadGameValue, exitGameValue;

    [SerializeField]
    TextMeshProUGUI resultsText;

    [SerializeField]
    int levelToLoad;

    [SerializeField]
    bool isGame;

    bool started;
    private void Start()
    {
        initialShips = gm.amountToSpawnShips + gm.amountToSpawnMedShips + gm.amountToSpawnBigShips;
        shipsLeft = initialShips;
    }

    private void Update()
    {
        //Debugging without Wiimote :3
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            lightHousePivot.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            lightHousePivot.transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
        }

        if (isGame)
        {
            if (shipsLeft <= 0 && !gm.gameFinished)
            {
                if (savedShips <= 0)
                {
                    PlayerPrefs.SetInt("thatCurrentNight", SceneManager.GetActiveScene().buildIndex);
                    StartCoroutine(loadScene(10));
                }
                else
                {
                    PlayerPrefs.SetInt("savedShips", PlayerPrefs.GetInt("savedShips") + savedShips);

                    resultsText.text = "Ships Saved: " + savedShips + "\n" + "Lost Ships: " + lostShips;
                    gameFinishObj.SetActive(true);
                    colliderSpot.SetActive(true);

                    gm.gameFinished = true;
                }
            }
        }

        if (loadGameValue >= 1 && !started)
        {
            gm.fadeObj.SetTrigger("fadeOut");
            if (gm.isThirdNight)
            {
                if (PlayerPrefs.GetInt("savedShips") == 0)
                {
                    StartCoroutine(loadScene(0));
                }
                if (PlayerPrefs.GetInt("savedShips") >= 4 && PlayerPrefs.GetInt("savedShips") <= 10)
                {
                    StartCoroutine(loadScene(6));
                }
                if (PlayerPrefs.GetInt("savedShips") >= 11 && PlayerPrefs.GetInt("savedShips") <= 17)
                {
                    StartCoroutine(loadScene(7));
                }
                if (PlayerPrefs.GetInt("savedShips") >= 18 && PlayerPrefs.GetInt("savedShips") <= 23)
                {
                    StartCoroutine(loadScene(8));
                }
                if (PlayerPrefs.GetInt("savedShips") >= 24)
                {
                    StartCoroutine(loadScene(9));
                }
            }
            else
            {
                StartCoroutine(loadScene(levelToLoad));
            }
            started = true;
        }

        if (exitGameValue >= 1)
        {
            Application.Quit();
        }
    }

    IEnumerator loadScene(int scene)
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(scene);
    }
}
