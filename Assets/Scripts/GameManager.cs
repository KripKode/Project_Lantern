using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    BoxCollider2D shipSpawnArea, monsterSpawnArea;

    [SerializeField]
    GameObject shipPrefab;

    [SerializeField, Range(1, 10)]
    float shipSpawnTimer;

    [Range(1, 50)]
    public int amountToSpawnShips;


    [SerializeField]
    GameObject monsterPrefab;

    [SerializeField, Range(1, 30)]
    float monsterSpawnTimer;

    [Range(1, 50)]
    public int amountToSpawnMonsters;

    void Start()
    {
        StartCoroutine(randomShipSpawn());
        StartCoroutine(randomMonsterSpawn());
    }

    public void SpawnShip()
    {
        Bounds colliderBounds = shipSpawnArea.bounds;
        Vector3 colliderCenter = colliderBounds.center;
        Vector3 spawnableItemLocalScale = shipPrefab.transform.localScale;

        float spawnableItemSizeX = spawnableItemLocalScale.x / 2;
        float spawnableItemSizeY = spawnableItemLocalScale.y / 2;

        float[] ranges = {
            (colliderCenter.x - colliderBounds.extents.x) + spawnableItemSizeX,
            (colliderCenter.x + colliderBounds.extents.x) - spawnableItemSizeX,
            (colliderCenter.y - colliderBounds.extents.y) + spawnableItemSizeY,
            (colliderCenter.y + colliderBounds.extents.y) - spawnableItemSizeY,
        };

        float randomX = Random.Range(ranges[0], ranges[1]);
        float randomY = Random.Range(ranges[2], ranges[3]);

        Vector2 randomPos = new Vector2(randomX, randomY);

        Instantiate(shipPrefab, randomPos, Quaternion.identity);
    }

    public void SpawnMonster()
    {
        Bounds colliderBounds = monsterSpawnArea.bounds;
        Vector3 colliderCenter = colliderBounds.center;
        Vector3 spawnableItemLocalScale = shipPrefab.transform.localScale;

        float spawnableItemSizeX = spawnableItemLocalScale.x / 2;
        float spawnableItemSizeY = spawnableItemLocalScale.y / 2;

        float[] ranges = {
            (colliderCenter.x - colliderBounds.extents.x) + spawnableItemSizeX,
            (colliderCenter.x + colliderBounds.extents.x) - spawnableItemSizeX,
            (colliderCenter.y - colliderBounds.extents.y) + spawnableItemSizeY,
            (colliderCenter.y + colliderBounds.extents.y) - spawnableItemSizeY,
        };

        float randomX = Random.Range(ranges[0], ranges[1]);
        float randomY = Random.Range(ranges[2], ranges[3]);

        Vector2 randomPos = new Vector2(randomX, randomY);

        Instantiate(monsterPrefab, randomPos, Quaternion.identity);
    }

    IEnumerator randomShipSpawn()
    {
        for (int i = 0; i < amountToSpawnShips; i++)
        {
            yield return new WaitForSeconds(shipSpawnTimer);
            SpawnShip();
        }
    }

    IEnumerator randomMonsterSpawn()
    {
        for (int i = 0; i < amountToSpawnShips; i++)
        {
            yield return new WaitForSeconds(monsterSpawnTimer);
            SpawnMonster();
        }
    }
}
