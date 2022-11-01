using Unity.Collections;
using UnityEngine;

public class LightHouseCC : MonoBehaviour
{
    [SerializeField]
    GameObject lightHouseObj;

    [SerializeField, Range(0.1f, 1)]
    float shipMovementSpeed;

    [SerializeField, Range(0.1f, 2)]
    float rotationSpeed;

    [SerializeField]
    public int savedShips;

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            lightHouseObj.transform.Rotate(0, 0, rotationSpeed);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            lightHouseObj.transform.Rotate(0, 0, -rotationSpeed);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ship"))
        {
            collision.transform.position = Vector3.MoveTowards(collision.transform.position, lightHouseObj.transform.position, shipMovementSpeed * Time.deltaTime);
            if (Vector3.Distance(collision.transform.position, lightHouseObj.transform.position) < 0.5f)
            {
                savedShips++;
                Destroy(collision.gameObject);
            }
        }
    }
}
