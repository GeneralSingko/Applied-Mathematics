using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Transform target; // The target position for the enemy to move towards
    public float moveSpeed = 1f; // The speed at which the enemy moves
    private bool hasWaited = false; // A flag to track whether the enemy has waited for 1 second
    public Color gizmoColor = Color.red; // The color of the gizmo
    public float detectionDistance = 1f; // The distance at which the enemy detects bulletPrefab


    private void Start()
    {
        GameObject targetGameObject = GameObject.Find("EndPoint");
        if (targetGameObject != null)
        {
            target = targetGameObject.transform;
        }
        else
        {
            Debug.LogError("Target GameObject not found");
        }

        // Wait for 1 second before starting to move
        Invoke("StartMoving", 1f);
    }

    private void StartMoving()
    {
        hasWaited = true;
    }

    private void Update()
    {
        // Check if the enemy has waited for 1 second
        if (hasWaited)
        {

            // Calculate the direction to the target
            Vector2 direction = (target.position - transform.position).normalized;

            // Move the enemy towards the target using Lerp
            transform.position = Vector2.Lerp(transform.position, target.position, moveSpeed * 0.5f * Time.deltaTime);

            // Check if the enemy has reached the target position
            if (Vector2.Distance(transform.position, target.position) < 0.1f)
            {
                // Destroy the enemy when it reaches the target
                Destroy(gameObject);
            }

            // Check for the presence of another GameObject using a tag (you should set a tag for your bulletPrefab)
            GameObject[] bullets = GameObject.FindGameObjectsWithTag("bulletPrefab");

            foreach (GameObject bullet in bullets)
            {
                // Calculate the distance between the enemy and each bullet
                float distanceToBullet = Vector2.Distance(transform.position, bullet.transform.position);

                // If the distance is below the detectionDistance, destroy the enemy
                if (distanceToBullet < detectionDistance)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (target != null)
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawLine(transform.position, target.position);
        }
    }
}
