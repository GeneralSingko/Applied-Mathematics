using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float speed = 20f; // Adjust this to set the bullet speed
    private Vector2 direction;
    public float collisionDistanceThreshold = 0.5f; // Adjust this to set the collision threshold

    void Start()
    {
        // Destroy the bullet after 2 seconds
        Destroy(gameObject, 1f);
    }

    void Update()
    {
        MoveBullet();
    }

    public void SetDirection(Vector3 targetPosition, Quaternion turretRotation)
    {
        // Calculate the direction vector from bullet to target in 2D space
        Vector3 directionToTarget = targetPosition - transform.position;

        // Adjust the direction based on the turret's rotation
        directionToTarget = Quaternion.Inverse(turretRotation) * directionToTarget;

        direction = directionToTarget.normalized;
    }

    private void MoveBullet()
    {
        // Move the bullet in the specified direction
        transform.Translate(direction * speed * Time.deltaTime);

        // Check for collisions based on distance
        CheckForCollisions();
    }

    private void CheckForCollisions()
    {
        // Find all enemy objects in the scene (replace "enemyPrefab" with your actual tag)
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemyPrefab");

        foreach (GameObject enemy in enemies)
        {
            // Calculate the distance between the bullet and the enemy
            float distance = Vector2.Distance(transform.position, enemy.transform.position);

            // Check if the distance is below the collision threshold
            if (distance < collisionDistanceThreshold)
            {
                // Handle enemy hit
                HandleCollisionWithEnemy(enemy);
            }
        }
    }

    private void HandleCollisionWithEnemy(GameObject enemy)
    {
        // Implement your logic for handling the collision with the enemy
        Debug.Log("Bullet hit enemy: " + enemy.name);

        // Destroy the bullet
        Destroy(gameObject);
    }
}
