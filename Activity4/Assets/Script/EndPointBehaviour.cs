using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EndPointBehaviour : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float detectionRange = 0.3f;
    public int maxHealth = 10;

    public Color gizmoColor = Color.yellow; // The color of the gizmo

    private int currentHealth;
    private bool hasDetected = false; // Flag to track whether detection has occurred

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        // Check if the enemy prefab is within the detection range
        if (!hasDetected && IsEnemyInDetectionRange())
        {
            // Enemy is within range, deduct 1 health
            currentHealth--;

            // Set the flag to true to prevent further detections until reset
            hasDetected = true;


            // Check if health is zero, restart the scene if true
            if (currentHealth <= 0)
            {
                // Restart the scene (you can replace this with your own game over logic)
                UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
            }
        }
        else if (!IsEnemyInDetectionRange())
        {
            // Reset the flag if no enemy is in the detection range
            hasDetected = false;
        }

        Debug.Log(currentHealth.ToString());
    }

    bool IsEnemyInDetectionRange()
    {
        // Find all GameObjects with the specified enemy prefab tag
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyPrefab.tag);

        foreach (GameObject enemy in enemies)
        {
            // Calculate the distance between the owner and each enemy using the distance formula
            float distance = CalculateDistance(enemy.transform.position, transform.position);

            // Check if the enemy is within the detection range
            if (distance < detectionRange)
            {
                return true;
            }
        }

        return false;
    }

    float CalculateDistance(Vector3 position1, Vector3 position2)
    {
        // Distance formula: sqrt((x2 - x1)^2 + (y2 - y1)^2)
        float deltaX = position2.x - position1.x;
        float deltaY = position2.y - position1.y;
        return Mathf.Sqrt(deltaX * deltaX + deltaY * deltaY);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}