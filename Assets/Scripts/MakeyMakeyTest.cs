using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeyMakeyTest : MonoBehaviour
{
    private void Update()
    {
        foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKey(vKey))
            {
                Debug.Log(vKey);
            }
        }
    }
}
