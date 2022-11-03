using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipBehaviour : MonoBehaviour
{
    [SerializeField]
    GameObject anim;

    public int shipState;
    public float shipHealth;

    public Rigidbody2D rb;
    public float accelerationTime = 2f;
    private Transform positionToGo;
    private float timeLeft;

    public float shipSpeed, rotationSpeed;

    public GameObject bigPapa, harborLocation;
    public Transform[] children;

    public bool isDirected;

    public float health;
    public float maxHealth;

    private void Start()
    {
        bigPapa = GameObject.FindGameObjectWithTag("BigPapa");
        children = new Transform[bigPapa.transform.childCount];

        for (int i = 0; i < bigPapa.transform.childCount; i++)
        {
            children[i] = bigPapa.transform.GetChild(i);
        }

        positionToGo = children[Random.Range(0, children.Length)].transform;
    }

    void Update()
    {
        if (children.Length == 0)
            return;

        timeLeft += Time.deltaTime;
        if (timeLeft >= 3)
        {
            positionToGo = children[Random.Range(0, children.Length)].transform;
            timeLeft = 0;
        }

        if (!isDirected)
        {
            Vector2 dir = positionToGo.position - transform.position;
            dir.Normalize();
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
            transform.position += transform.up * Time.deltaTime * shipSpeed;
        }
        else
        {
            Vector2 dir = harborLocation.transform.position - transform.position;
            dir.Normalize();
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
            transform.position += transform.up * Time.deltaTime * shipSpeed;
        }

        if (health >= (maxHealth / 8) && health <= (maxHealth / 2))
            shipState = 1;
        if (health >= (maxHealth / 2) && health <= maxHealth)
            shipState = 2;

        switch (shipState)
        {
            case 1:
                anim.SetActive(true);
                anim.GetComponent<Animator>().speed = 0.25f;
                break;
            case 2:
                anim.GetComponent<Animator>().speed = 2;
                break;
        }
    }
}