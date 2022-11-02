using System.Collections;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LightHouseCC : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI savedShipsTxt;

    [SerializeField]
    GameManager gm;

    public GameObject lightHouseObj, gameFinishObj;

    public Image startFillBar, quitFillBar;

    [Range(0.1f, 1)]
    public float shipMovementSpeed;

    [SerializeField, Range(50, 100)]
    public float rotationSpeed;

    public int savedShips;

    public float loadGameValue, exitGameValue;

    [SerializeField]
    Animator fadeObj;

    [SerializeField]
    bool isGame;

    bool started, gameFinish;

    private void Update()
    {
        if (isGame)
        {
            savedShipsTxt.text = "Ships Saved: " + savedShips + "/" + gm.amountToSpawnShips.ToString();

            if (savedShips == gm.amountToSpawnShips && !gameFinish)
            {
                gameFinishObj.SetActive(true);
                gameFinish = true;
            }
        }

        if (loadGameValue >= 1 && !started)
        {
            fadeObj.SetTrigger("fadeOut");
            StartCoroutine(loadScene(1));
            started = true;
        }

        if (exitGameValue >= 1)
        {
            Application.Quit();
        }

        //Debugging without Wiimote :3
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            lightHouseObj.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            lightHouseObj.transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
        }
    }

    IEnumerator loadScene(int scene)
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(scene);
    }
}
