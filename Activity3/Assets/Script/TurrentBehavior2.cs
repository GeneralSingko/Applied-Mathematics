using System.Collections;
using UnityEngine;

public class TurretBehavior2 : MonoBehaviour
{
    public float detectionRange = 4f;
    public float rotationSpeed = 20f;
    public GameObject enemyPrefab;
    public GameObject bulletPrefab;
    public Transform firePoint;

    private Quaternion originalRotation;
    private Transform targetEnemy;
    private bool canFire = true;

    private void Start()
    {
        originalRotation = transform.rotation;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

    private void Update()
    {
        DetectEnemyInRange();

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
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemyPrefab");

        foreach (GameObject enemy in enemies)
        {
            if (IsEnemyInDetectionRange(enemy.transform.position))
            {
                targetEnemy = enemy.transform;
                return;
            }
        }
        
        targetEnemy = null;
    }

    private bool IsEnemyInDetectionRange(Vector3 enemyPosition)
    {
        float distance = CalculateDistance(transform.position, enemyPosition);
        return distance <= detectionRange;
    }

    private void RotateTowardsEnemy()
    {
        Vector3 directionToEnemy = targetEnemy.position - transform.position;
        directionToEnemy.z = 0f;

        float angle = Mathf.Atan2(directionToEnemy.y, directionToEnemy.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }


    private void CheckAndInstantiateBullet()
    {
        if (canFire)
        {
            // Instantiate bulletPrefab at the firePoint's position
            if (bulletPrefab != null)
            {
                GameObject bulletInstance = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

                // Set the direction for the bullet, considering the turret's rotation
                BulletBehavior bulletBehavior = bulletInstance.GetComponent<BulletBehavior>();
                if (bulletBehavior != null)
                {
                    bulletBehavior.SetDirection(targetEnemy.position, transform.rotation);
                }

                StartCoroutine(WaitForNextShot());
            }
            else
            {
                Debug.LogError("bulletPrefab is null. Assign a valid prefab in the Inspector.");
            }
        }
    }

    private float CalculateDistance(Vector3 point1, Vector3 point2)
    {
        float dx = point2.x - point1.x;
        float dy = point2.y - point1.y;
        return Mathf.Sqrt(dx * dx + dy * dy);
    }

    IEnumerator WaitForNextShot()//rate of fire
    {
        canFire = false;
        yield return new WaitForSeconds(.25f);
        canFire = true;
    }
}
