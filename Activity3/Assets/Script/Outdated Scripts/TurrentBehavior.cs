using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurrentBehavior : MonoBehaviour
{
    public float detectionRange = 2.3f; // Adjust the range as needed
    public float rotationSpeed = 2f; // Adjust the rotation speed as needed
    public GameObject enemyPrefab;

    public GameObject bulletPrefab;
    public Transform firePoint; // Set this to the position where you want the bullets to spawn


    private Quaternion originalRotation;
    private Quaternion targetRotation;
    private Transform targetEnemy;

    //new
    private bool canFire = true; // Control whether the turret can fire

    private void Start()
    {
        originalRotation = transform.rotation;
        targetRotation = originalRotation;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // Draw the raycast visualization
        if (firePoint != null)
        {
            Gizmos.DrawLine(firePoint.position, firePoint.position + firePoint.right * detectionRange);
        }
    }

    private void Update()
    {
        DetectEnemyInRange();

        // Rotate towards the target enemy
        if (targetEnemy != null)
        {
            RotateTowardsEnemy();

            // Check for an obstacle in front of the turret using raycast and instantiate bullet if clear
            CheckAndInstantiateBullet();
        }
        else
        {
            // Slerp for smooth rotation back to original rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, originalRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void DetectEnemyInRange()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemyPrefab"); // Assuming enemies have the "enemyPrefab" tag

        foreach (GameObject enemy in enemies)
        {
            if (IsEnemyInDetectionRange(enemy.transform.position))
            {
                //Debug.Log("Enemy in reach");
                targetEnemy = enemy.transform;
                return;
            }
        }

        // Reset targetEnemy if no enemy is in range
        targetEnemy = null;
    }

    private bool IsEnemyInDetectionRange(Vector3 enemyPosition)
    {
        float distance = CalculateDistance(transform.position, enemyPosition);
        return distance <= detectionRange;
    }

    private void RotateTowardsEnemy()
    {
        // Calculate the direction to the target enemy
        Vector3 directionToEnemy = targetEnemy.position - transform.position;
        directionToEnemy.z = 0f; // Ignore the Z component for a 2D game

        // Calculate the angle in radians
        float angle = Mathf.Atan2(directionToEnemy.y, directionToEnemy.x) * Mathf.Rad2Deg;

        // Create a new Quaternion based on the angle
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);

        // Slerp for smooth rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void CheckAndInstantiateBullet()
    {
        if (canFire && targetEnemy != null)
        {
            float distanceToEnemy = Vector3.Distance(firePoint.position, targetEnemy.position);

            // Adjust this threshold based on your needs
            float distanceThreshold = 0.1f;

            if (distanceToEnemy < distanceThreshold)
            {
                // Log information about the hit object
                Debug.Log("Enemy in detection range: " + targetEnemy.name);

                // Instantiate bulletPrefab at the firePoint's position
                if (bulletPrefab != null)
                {
                    Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                    StartCoroutine(WaitForNextShot());
                }
                else
                {
                    Debug.LogError("bulletPrefab is null. Assign a valid prefab in the Inspector.");
                }
            }
        }
        // Calculate the direction to the target enemy
        /*Vector3 directionToEnemy = targetEnemy.position - transform.position;

        // Ignore the Z component for a 2D game
        directionToEnemy.z = 0f;

        // Calculate the distance to the target enemy
        float distanceToEnemy = directionToEnemy.magnitude;

        // Debugging statements
        Debug.DrawRay(firePoint.position, transform.right * detectionRange, Color.yellow); // Visualize the ray in the Scene view
        */

        /*if (distanceToEnemy <= detectionRange)
        {
            // Log information about the hit object
            Debug.Log("Enemy in detection range: " + targetEnemy.name);

            // Instantiate bulletPrefab at the firePoint's position
            if (bulletPrefab != null)
            {
                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            }
            else
            {
                Debug.LogError("bulletPrefab is null. Assign a valid prefab in the Inspector.");
            }
        }
        else
        {
            Debug.Log("Enemy is outside detection range.");
        }*/
    }

    private float CalculateDistance(Vector3 point1, Vector3 point2)
    {
        // Use Pythagorean theorem to calculate distance without vector functions
        float dx = point2.x - point1.x;
        float dy = point2.y - point1.y;
        float distance = Mathf.Sqrt(dx * dx + dy * dy);

        return distance;
    }

    IEnumerator WaitForNextShot()//new
    {
        canFire = false;
        yield return new WaitForSeconds(0.5f);
        canFire = true;
    }
}
