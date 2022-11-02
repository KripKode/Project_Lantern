using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCollision : MonoBehaviour
{
    [SerializeField]
    GameObject monsterDeathPrefab;

    [SerializeField]
    LightHouseCC lh;

    [SerializeField]
    float smallMonsterHealth;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ship"))
        {
            collision.transform.position = Vector3.MoveTowards(collision.transform.position, lh.lightHouseObj.transform.position, lh.shipMovementSpeed * Time.deltaTime);
            if (Vector3.Distance(collision.transform.position, lh.lightHouseObj.transform.position) < 0.5f)
            {
                lh.savedShips++;
                Destroy(collision.gameObject);
            }
        }

        if (collision.gameObject.CompareTag("Monster"))
        {
            float health = collision.gameObject.GetComponent<GhostBehaviour>().health += Time.deltaTime;
            if (health >= smallMonsterHealth)
            {
                Instantiate(monsterDeathPrefab, collision.gameObject.transform.position, Quaternion.identity);
                Destroy(collision.gameObject);
            }
        }

        if (collision.gameObject.CompareTag("StartBtn"))
        {
            lh.loadGameValue += Time.deltaTime / 2.5f;
            lh.startFillBar.fillAmount = lh.loadGameValue;
        }

        if (collision.gameObject.CompareTag("QuitBtn"))
        {
            lh.exitGameValue += Time.deltaTime / 2.5f;
            lh.quitFillBar.fillAmount = lh.exitGameValue;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
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
    }
}
