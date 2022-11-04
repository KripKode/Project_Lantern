using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCollision : MonoBehaviour
{
    [SerializeField]
    GameObject monsterDeathPrefab;

    [SerializeField]
    AudioSource aS;

    [SerializeField]
    AudioClip aC;

    [SerializeField]
    LightHouseCC lh;

    float tutorial;

    bool spawnedOnce;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ship"))
        {
            collision.gameObject.GetComponent<ShipBehaviour>().harborLocation = lh.harborSpot;
            collision.gameObject.GetComponent<ShipBehaviour>().isDirected = true;
            collision.transform.position = Vector3.MoveTowards(collision.transform.position, lh.harborSpot.transform.position, lh.shipMovementSpeed * Time.deltaTime);

            if (lh.gm.isTutorial)
            {
                tutorial += Time.deltaTime;

                if (tutorial >= 3 && !spawnedOnce)
                {
                    lh.gm.fadeObj.SetTrigger("showEnemy");
                    lh.gm.SpawnMonster(1);
                    spawnedOnce = true;
                }
            }

            if (Vector3.Distance(collision.transform.position, lh.harborSpot.transform.position) < 0.5f)
            {
                lh.shipsLeft--;
                lh.savedShips++;
                aS.PlayOneShot(aC);
                Destroy(collision.gameObject);
            }
        }

        if (collision.gameObject.CompareTag("Monster"))
        {
            GetComponent<AudioSource>().enabled = true;

            float health = collision.gameObject.GetComponent<GhostBehaviour>().health += Time.deltaTime;
            if (health >= collision.gameObject.GetComponent<GhostBehaviour>().maxHealth)
            {
                StartCoroutine(lh.gm.voicelineKillDelay());
                var go = Instantiate(monsterDeathPrefab, collision.gameObject.transform.position, Quaternion.identity);
                Destroy(go, 3);
                Destroy(collision.gameObject);
            }
        }

        if (collision.gameObject.CompareTag("StartBtn"))
        {
            lh.loadGameValue += Time.deltaTime / 2;
            lh.startFillBar.fillAmount = lh.loadGameValue;
        }

        if (collision.gameObject.CompareTag("QuitBtn"))
        {
            lh.exitGameValue += Time.deltaTime / 2;
            lh.quitFillBar.fillAmount = lh.exitGameValue;
        }

        if (collision.gameObject.CompareTag("Continue"))
        {
            lh.loadGameValue += Time.deltaTime / 2;
            lh.startFillBar.fillAmount = lh.loadGameValue;
        }

        if (collision.gameObject.CompareTag("BackMenu"))
        {
            lh.exitGameValue += Time.deltaTime / 2;
            lh.quitFillBar.fillAmount = lh.exitGameValue;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            GetComponent<AudioSource>().enabled = false;
        }

        if (collision.gameObject.CompareTag("Ship"))
        {
            collision.gameObject.GetComponent<ShipBehaviour>().isDirected = false;
        }

        if (collision.gameObject.CompareTag("StartBtn"))
        {
            lh.loadGameValue = 0;
            lh.startFillBar.fillAmount = 0;
        }

        if (collision.gameObject.CompareTag("QuitBtn"))
        {
            lh.exitGameValue = 0;
            lh.quitFillBar.fillAmount = 0;
        }

        if (collision.gameObject.CompareTag("Continue"))
        {
            lh.loadGameValue = 0;
            lh.startFillBar.fillAmount = 0;
        }

        if (collision.gameObject.CompareTag("BackMenu"))
        {
            lh.exitGameValue = 0;
            lh.quitFillBar.fillAmount = 0;
        }
    }
}
