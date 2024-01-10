using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{

    private float detectionRange = 1f;
    public Transform playerTransform;

    public Color touchedColor = Color.yellow;
    private SpriteRenderer spriteRenderer;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float distance = CalculatedDistance(playerTransform.position, transform.position);

        if (distance < detectionRange)
        {
            IsSwitchTouched();
            Debug.Log("Player touch the tree");

        }
    }

    private float CalculatedDistance(Vector3 position1, Vector3 position2)
    {
        float deltaX = position1.x - position2.x;
        float deltaY = position1.y - position2.y;

        float distance = deltaX * deltaX + deltaY * deltaY;

        return distance;
    }

    private void IsSwitchTouched() 
    {
        spriteRenderer.color = touchedColor;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
