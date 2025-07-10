using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(HealthSystem))]
public class EnemyAI : MonoBehaviour
{

    private enum State
    {
        Idle,
        Chasing
    }

    [Header("AI Settings")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float detectionRange = 8f;
    [SerializeField] private float attackRange = 1.2f;
    [SerializeField] private int contactDamage = 10;

    [Header("Splitting")]
    [SerializeField] private GameObject smallSlimePrefab; 
    [SerializeField] private int splitCount = 2;
    [SerializeField] private bool isSmallSlime = false; 

    private State currentState = State.Idle;
    private Transform playerTransform;
    private Rigidbody2D rb;
    private HealthSystem healthSystem;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthSystem = GetComponent<HealthSystem>();

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
        healthSystem.OnDeath.AddListener(HandleDeath);
    }

    void Update()
    {
        if (playerTransform == null) return; 
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer < detectionRange)
        {
            currentState = State.Chasing;
        }
        else
        {
            currentState = State.Idle;
        }
    }

    private void FixedUpdate()
    {
        if (currentState == State.Chasing && playerTransform != null)
        {
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed;

            if (direction.x > 0)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (direction.x < 0)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
        else
        {
            rb.velocity = Vector2.zero; 
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthSystem playerHealth = collision.gameObject.GetComponent<HealthSystem>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(contactDamage);
            }
        }
    }
    private void HandleDeath()
    {
        if (!isSmallSlime && smallSlimePrefab != null)
        {
            for (int i = 0; i < splitCount; i++)
            {
                Vector2 spawnPosition = (Vector2)transform.position + Random.insideUnitCircle * 0.5f;
                Instantiate(smallSlimePrefab, spawnPosition, Quaternion.identity);
            }
        }
    }
}
