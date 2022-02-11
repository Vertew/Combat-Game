using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{

    [SerializeField] private float projectile_velocity;
    [SerializeField] private Rigidbody2D rb2d;

    private float delay;

    void Start()
    {
        rb2d.velocity = transform.up * projectile_velocity;
        // Destroy shot if it exists for too long
        Destroy(gameObject, 5f);
    }

    // Destroy shot if it hits enemy tank
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log(hitInfo.name);
        Destroy(gameObject);
    }

}
