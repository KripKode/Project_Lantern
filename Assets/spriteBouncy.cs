using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteBouncy : MonoBehaviour
{
    public float maxAmp, minAmp;
    Vector3 scale;

    void Update()
    {
        for (int i = 0; i < 3; ++i)
        {
            scale[i] = maxAmp + minAmp * Mathf.Sin(Time.time);
        }

        transform.localScale = scale;
    }
}
