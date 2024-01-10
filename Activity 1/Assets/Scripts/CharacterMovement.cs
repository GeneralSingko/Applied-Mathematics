using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the new position based on user input
        float newX = transform.position.x + horizontalInput * moveSpeed * Time.deltaTime;
        float newY = transform.position.y + verticalInput * moveSpeed * Time.deltaTime;

        // Update the position
        transform.position = new Vector3(newX, newY, transform.position.z);
    }
}
