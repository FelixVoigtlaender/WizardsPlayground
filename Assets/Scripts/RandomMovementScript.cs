using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovementScript : MonoBehaviour
{
    [Header("Randomness")]
    public float speedRange = 1.5f;
    public float rotationSpeedRange = 40;
    [Header("Information")]
    public Vector2 size;
    public Vector2 velocity;
    public float rotationSpeed;

    private void Start()
    {
        velocity = new Vector2(Random.Range(-speedRange, speedRange), Random.Range(-speedRange, speedRange));
        rotationSpeed = Random.Range(-rotationSpeedRange, rotationSpeedRange);
    }
    // Update is called once per frame
    void Update()
    {
        //Movement
        Vector2 position = transform.position;
        position += velocity * Time.deltaTime;
        transform.position = position;

        // Bounce of walls
        if (position.x > size.x / 2)
        {
            velocity.x = Mathf.Abs(velocity.x) * -1;
            rotationSpeed *= -1;
        }
        if (position.x < -size.x / 2)
        {
            velocity.x = Mathf.Abs(velocity.x);
            rotationSpeed *= -1;
        }
        if (position.y > size.y / 2)
        {
            velocity.y = Mathf.Abs(velocity.y) * -1;
            rotationSpeed *= -1;
        }
        if (position.y < -size.y / 2)
        {
            velocity.y = Mathf.Abs(velocity.y);
            rotationSpeed *= -1;
        }

        //Rotation
        Quaternion rotation = transform.rotation;
        rotation *= Quaternion.Euler(0, 0, rotationSpeed * Time.deltaTime);
        transform.rotation = rotation;

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(Vector2.zero, size);
    }
}
