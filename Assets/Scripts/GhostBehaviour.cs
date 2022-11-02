using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBehaviour : MonoBehaviour
{
    public float monsterSpeed;
    public float health;

    private void Update()
    {
        if (!FindClosestEnemy())
            return;

        if (Vector3.Distance(transform.position, FindClosestEnemy().position) > 0.25f)
        {
            transform.position = Vector3.MoveTowards(transform.position, FindClosestEnemy().position, monsterSpeed * Time.deltaTime);
        }

    }

    public Transform FindClosestEnemy()
    {
        float distanceToClosestEnemy = Mathf.Infinity;
        ShipBehaviour closestEnemy = null;
        ShipBehaviour[] allEnemies = GameObject.FindObjectsOfType<ShipBehaviour>();

        foreach (ShipBehaviour currentEnemy in allEnemies)
        {
            float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
            if (distanceToEnemy < distanceToClosestEnemy)
            {
                distanceToClosestEnemy = distanceToEnemy;
                closestEnemy = currentEnemy;
                return closestEnemy.gameObject.transform;
            }
        }

        return null;
    }
}
