using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cinematicScript : MonoBehaviour
{
    [SerializeField]
    Animator fadeObj;

    [SerializeField]
    int sceneToLoad;

    [SerializeField]
    bool isResetThingy;

    public void loadScene()
    {
        if(isResetThingy)
            SceneManager.LoadScene(PlayerPrefs.GetInt("thatCurrentNight"));
        else
            SceneManager.LoadScene(sceneToLoad);
    }

    public void fadeOut()
    {
        fadeObj.SetTrigger("fadeOut");
    }
}
