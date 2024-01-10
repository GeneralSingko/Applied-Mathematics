using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    public float detectionRange = 4f;
    public Transform playerTransform;

    private void Update()
    {
        float distance = CalculatedDistance(playerTransform.position, transform.position);

        if (distance < detectionRange)
        {
            Debug.Log("Game Restarted!");
            RestartLevel();

        }
    }

    private float CalculatedDistance(Vector3 position1, Vector3 position2)
    {
        float deltaX = position1.x - position2.x;
        float deltaY = position1.y - position2.y;

        float distance = deltaX * deltaX + deltaY * deltaY;
        
        return distance;
    }

    private void RestartLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
