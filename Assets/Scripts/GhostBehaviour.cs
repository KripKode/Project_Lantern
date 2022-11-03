using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBehaviour : MonoBehaviour
{
    [SerializeField]
    GameObject anim;

    [SerializeField]
    GameObject shipDeathPrefab;

    [SerializeField]
    float attackMulti;

    public float monsterSpeed;
    public float health, maxHealth;

    public int ghostState;

    private void Update()
    {
        GameObject closestEnemyTarget = FindClosestEnemyWithinRange();

        if (!closestEnemyTarget)
            return;

        if (Vector3.Distance(transform.position, closestEnemyTarget.transform.position) > 0.25f)
        {
            transform.position = Vector3.MoveTowards(transform.position, closestEnemyTarget.transform.position, monsterSpeed * Time.deltaTime);
        }

    }
    private GameObject FindClosestEnemyWithinRange()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Ship");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;

            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }

        if (health >= (maxHealth / 4) && health <= (maxHealth / 2))
            ghostState = 1;
        if (health >= (maxHealth / 2) && health <= maxHealth)
            ghostState = 2;

        switch (ghostState)
        {
            case 1:
                anim.SetActive(true);
                anim.GetComponent<Animator>().speed = 0.25f;
                break;
            case 2:
                anim.GetComponent<Animator>().speed = 2;
                break;
        }

        return closest;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ship"))
        {
            float health = collision.gameObject.GetComponent<ShipBehaviour>().health += Time.deltaTime * attackMulti;
            if (health >= collision.gameObject.GetComponent<ShipBehaviour>().maxHealth)
            {
                SOLH _obj = null;
                foreach (var obj in SOLH.Entities)
                {
                    _obj = obj;
                }
                _obj.GetComponent<LightHouseCC>().shipsLeft--;
                _obj.GetComponent<LightHouseCC>().lostShips++;

                var go = Instantiate(shipDeathPrefab, collision.gameObject.transform.position, Quaternion.identity);
                Destroy(go, 3);
                Destroy(collision.gameObject);
            }
        }
    }
}
