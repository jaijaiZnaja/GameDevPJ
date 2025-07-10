using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class WandProjectile : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] public float speed = 15f;
    [SerializeField] public int minDamage = 3;
    [SerializeField] public int maxDamage = 5;
    [SerializeField] public float lifetime = 3f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    public void Launch(Vector2 direction)
    {
        rb.velocity = direction.normalized * speed;
        transform.right = direction;
        Destroy(gameObject, lifetime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        HealthSystem healthSystem = other.GetComponent<HealthSystem>();
        if (healthSystem != null)
        {
            int randomDamage = Random.Range(minDamage, maxDamage + 1);
            healthSystem.TakeDamage(randomDamage);
        }

        if (!other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

}
