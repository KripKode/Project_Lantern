using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    AudioClip monsterSpawnSFX, monsterSpawnSFX2;

    [SerializeField]
    AudioClip[] monsterSpawnSFXA, monsterSpawnSFXA2;

    [SerializeField]
    BoxCollider2D shipSpawnArea;

    [SerializeField]
    PolygonCollider2D monsterSpawnArea;

    [SerializeField]
    GameObject smolShipPrefab, mediumShipPrefab, bigShipPrefab;

    [SerializeField, Range(1, 100)]
    float smolShipSpawnTimer, mediumShipSpawnTimer, bigShipSpawnTimer;

    [Range(0, 50)]
    public int amountToSpawnShips, amountToSpawnMedShips, amountToSpawnBigShips;

    [SerializeField]
    GameObject smolMonsterPrefab, mediumMonsterPrefab, bigMonsterPrefab;

    [SerializeField, Range(1, 100)]
    float smolMonsterSpawnTimer, mediumMonsterSpawnTimer, bigMonsterSpawnTimer;

    [SerializeField]
    bool isFirstNight, isSecondNight, isThirdNight;

    public Animator fadeObj;

    public bool isTutorial, gameFinished;

    void Start()
    {
        if (isTutorial)
        {
            fadeObj.SetTrigger("showShip");
            StartCoroutine(randomShipSpawn());
        }
        else
        {
            if (isFirstNight)
            {
                StartCoroutine(randomShipSpawn());
                StartCoroutine(randomMonsterSpawn());
            }
            else if (isSecondNight)
            {
                StartCoroutine(randomShipSpawn());
                StartCoroutine(randomMonsterSpawn());

                StartCoroutine(randomMediumShipSpawn());
                StartCoroutine(randomMediumMonsterSpawn());
            }
            else if (isThirdNight)
            {
                StartCoroutine(randomShipSpawn());
                StartCoroutine(randomMonsterSpawn());

                StartCoroutine(randomMediumShipSpawn());
                StartCoroutine(randomMediumMonsterSpawn());

                StartCoroutine(randomBigShipSpawn());
                StartCoroutine(randomBigMonsterSpawn());
            }
        }

    }

    Vector3 spawnableShipLocalScale;
    public void SpawnShip(int _shipPrefab)
    {
        Bounds colliderBounds = shipSpawnArea.bounds;
        Vector3 colliderCenter = colliderBounds.center;

        switch (_shipPrefab)
        {
            case 1:
                spawnableMonsterLocalScale = smolShipPrefab.transform.localScale;
                break;
            case 2:
                spawnableMonsterLocalScale = mediumShipPrefab.transform.localScale;
                break;
            case 3:
                spawnableMonsterLocalScale = bigShipPrefab.transform.localScale;
                break;
        }

        float spawnableItemSizeX = spawnableShipLocalScale.x / 2;
        float spawnableItemSizeY = spawnableShipLocalScale.y / 2;

        float[] ranges = {
            (colliderCenter.x - colliderBounds.extents.x) + spawnableItemSizeX,
            (colliderCenter.x + colliderBounds.extents.x) - spawnableItemSizeX,
            (colliderCenter.y - colliderBounds.extents.y) + spawnableItemSizeY,
            (colliderCenter.y + colliderBounds.extents.y) - spawnableItemSizeY,
        };

        float randomX = Random.Range(ranges[0], ranges[1]);
        float randomY = Random.Range(ranges[2], ranges[3]);

        Vector2 randomPos = new Vector2(randomX, randomY);

        switch (_shipPrefab)
        {
            case 1:
                Instantiate(smolShipPrefab, randomPos, Quaternion.identity);
                break;
            case 2:
                Instantiate(mediumShipPrefab, randomPos, Quaternion.identity);
                break;
            case 3:
                Instantiate(bigShipPrefab, randomPos, Quaternion.identity);
                break;
        }
    }

    Vector3 spawnableMonsterLocalScale;
    public void SpawnMonster(int _monsterPrefab)
    {
        StartCoroutine(voicelineDelay());

        Bounds colliderBounds = monsterSpawnArea.bounds;
        Vector3 colliderCenter = colliderBounds.center;

        switch (_monsterPrefab)
        {
            case 1:
                spawnableMonsterLocalScale = smolMonsterPrefab.transform.localScale;
                break;
            case 2:
                spawnableMonsterLocalScale = mediumMonsterPrefab.transform.localScale;
                break;
            case 3:
                spawnableMonsterLocalScale = bigMonsterPrefab.transform.localScale;
                break;
        }

        float spawnableItemSizeX = spawnableMonsterLocalScale.x / 2;
        float spawnableItemSizeY = spawnableMonsterLocalScale.y / 2;

        float[] ranges = {
            (colliderCenter.x - colliderBounds.extents.x) + spawnableItemSizeX,
            (colliderCenter.x + colliderBounds.extents.x) - spawnableItemSizeX,
            (colliderCenter.y - colliderBounds.extents.y) + spawnableItemSizeY,
            (colliderCenter.y + colliderBounds.extents.y) - spawnableItemSizeY,
        };

        float randomX = Random.Range(ranges[0], ranges[1]);
        float randomY = Random.Range(ranges[2], ranges[3]);

        Vector2 randomPos = new Vector2(randomX, randomY);

        switch (_monsterPrefab)
        {
            case 1:
                Instantiate(smolMonsterPrefab, randomPos, Quaternion.identity);
                break;
            case 2:
                Instantiate(mediumMonsterPrefab, randomPos, Quaternion.identity);
                break;
            case 3:
                Instantiate(bigMonsterPrefab, randomPos, Quaternion.identity);
                break;
        }
    }

    IEnumerator randomShipSpawn()
    {
        for (int i = 0; i < amountToSpawnShips; i++)
        {
            yield return new WaitForSeconds(smolShipSpawnTimer);
            if (gameFinished)
                yield break;
            SpawnShip(1);
        }
    }

    public IEnumerator randomMonsterSpawn()
    {
        while (!gameFinished)
        {
            yield return new WaitForSeconds(smolMonsterSpawnTimer);
            if (gameFinished)
                yield break;
            SpawnMonster(1);
        }

        yield break;
    }

    IEnumerator randomMediumShipSpawn()
    {
        for (int i = 0; i < amountToSpawnMedShips; i++)
        {
            yield return new WaitForSeconds(mediumShipSpawnTimer);
            if (gameFinished)
                yield break;
            SpawnShip(2);
        }
    }

    public IEnumerator randomMediumMonsterSpawn()
    {
        while (!gameFinished)
        {
            yield return new WaitForSeconds(mediumMonsterSpawnTimer);
            if (gameFinished)
                yield break;
            SpawnMonster(2);
        }
        yield break;
    }

    IEnumerator randomBigShipSpawn()
    {
        for (int i = 0; i < amountToSpawnBigShips; i++)
        {
            yield return new WaitForSeconds(bigShipSpawnTimer);
            if (gameFinished)
                yield break;
            SpawnShip(3);
        }
    }

    public IEnumerator randomBigMonsterSpawn()
    {
        while (!gameFinished)
        {
            yield return new WaitForSeconds(bigMonsterSpawnTimer);
            if (gameFinished)
                yield break;
            SpawnMonster(3);
        }
        yield break;
    }

    AudioClip[] FilterCurrentClip()
    {
        AudioClip[] rtrn = new AudioClip[monsterSpawnSFXA.Length - 1];
        int j = 0;
        for (int i = 0; i < monsterSpawnSFXA.Length; i++)
        {
            if (j >= rtrn.Length)
            {
                return monsterSpawnSFXA;
            }
            if (monsterSpawnSFXA[i] != audioSource.clip)
            {
                rtrn[j] = monsterSpawnSFXA[i];
                j++;
            }
        }
        return rtrn;
    }

    AudioClip[] FilterCurrentClip2()
    {
        AudioClip[] rtrn = new AudioClip[monsterSpawnSFXA2.Length - 1];
        int j = 0;
        for (int i = 0; i < monsterSpawnSFXA2.Length; i++)
        {
            if (j >= rtrn.Length)
            {
                return monsterSpawnSFXA2;
            }
            if (monsterSpawnSFXA2[i] != audioSource.clip)
            {
                rtrn[j] = monsterSpawnSFXA2[i];
                j++;
            }
        }
        return rtrn;
    }

    IEnumerator voicelineDelay()
    {
        yield return new WaitForSeconds(1.5f);
        var filteredSounds = FilterCurrentClip();
        if (!audioSource.isPlaying)
        {
            audioSource.clip = (filteredSounds[Random.Range(0, filteredSounds.Length)]);
            audioSource.Play();
        }
    }

    public IEnumerator voicelineKillDelay()
    {
        yield return new WaitForSeconds(0.25f);
        var filteredSounds = FilterCurrentClip2();
        if (!audioSource.isPlaying)
        {
            audioSource.clip = (filteredSounds[Random.Range(0, filteredSounds.Length)]);
            audioSource.Play();
        }
    }
}
