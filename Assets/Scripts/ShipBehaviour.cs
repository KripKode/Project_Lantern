using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipBehaviour : MonoBehaviour
{
    public Rigidbody2D rb;
    public float accelerationTime = 2f;
    private Vector2 movement;
    private float timeLeft;

    public float shipSpeed;

    public GameObject bigPapa;
    public Transform[] children;

    private void Start()
    {
        bigPapa = GameObject.FindGameObjectWithTag("BigPapa");
        children = new Transform[bigPapa.transform.childCount];

        for (int i = 0; i < bigPapa.transform.childCount; i++)
        {
            children[i] = bigPapa.transform.GetChild(i);
        }
    }

    void Update()
    {
        if (children.Length == 0)
            return;

        timeLeft += Time.deltaTime;
        if (timeLeft >= 8)
        {
            movement = children[Random.Range(0, children.Length)].transform.position;
            timeLeft = 0;
        }

        transform.position = Vector3.MoveTowards(transform.position, movement, shipSpeed * Time.deltaTime);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}