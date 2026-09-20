using System.Data;
using UnityEditor.Analytics;
using UnityEngine;

public class BossDamagedProjectile : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform projectilePos;
    private float moveSpeed, radius;
    Vector2 startPoint;

    private void Start()
    {
        radius = 5f;
        moveSpeed = 10f;
    }


    public void SpawnProjectiles(int numberOfProjectiles)
    {
        float angleStep = (90f - 270f) / numberOfProjectiles;
        float angle = 90f;
        for (int i = 0; i <= numberOfProjectiles -1; i++)
        {
            float projectileDirXposition = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileDirYposition = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector2 projectileVector = new Vector2(projectileDirXposition, projectileDirYposition);
            Vector2 projectileMoveDirection = (projectileVector - startPoint).normalized * moveSpeed;


            var proj = Instantiate(projectile, projectilePos.position, Quaternion.identity);
            proj.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);
            angle += angleStep;
        }
    }

}
