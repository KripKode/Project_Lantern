using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderSystem : MonoBehaviour
{
    [SerializeField]
    float thunderRatio;

    [SerializeField]
    GameObject lighningPrefab;

    [SerializeField]
    AudioSource aS;

    [SerializeField]
    AudioClip aC;

    void Start()
    {
        StartCoroutine(lightningSys());
    }

    IEnumerator lightningSys()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(thunderRatio - 5, thunderRatio + 10));
            aS.PlayOneShot(aC);
            var light = Instantiate(lighningPrefab);
            Destroy(light, 0.5f);
        }
    }
}
