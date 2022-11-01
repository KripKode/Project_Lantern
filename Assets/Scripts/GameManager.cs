using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    float shipSpawnRadius;

    [SerializeField]
    GameObject shipPrefab;

    [SerializeField, Range(1, 10)]
    float shipSpawnTimer;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(randomShipSpawn());
    }

    IEnumerator randomShipSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(shipSpawnTimer);
            Instantiate(shipPrefab, Random.insideUnitCircle.normalized * shipSpawnRadius, Quaternion.identity);
        }
    }
}
