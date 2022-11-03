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
        if (isGame)
        {
            if (shipsLeft <= 0 && !gm.gameFinished)
            {
                resultsText.text = "Ships Saved: " + savedShips + "\n" + "Lost Ships: " + lostShips;
                gameFinishObj.SetActive(true);
                colliderSpot.SetActive(true);
                gm.gameFinished = true;
            }
        }

        if (loadGameValue >= 1 && !started)
        {
            gm.fadeObj.SetTrigger("fadeOut");
            StartCoroutine(loadScene(levelToLoad));
            started = true;
        }

        if (exitGameValue >= 1)
        {
            Application.Quit();
        }

        //Debugging without Wiimote :3
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            lightHousePivot.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            lightHousePivot.transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
        }
    }

    IEnumerator loadScene(int scene)
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(scene);
    }
}
