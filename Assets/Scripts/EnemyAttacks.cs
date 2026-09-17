using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttacks : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletPos;

    private float timer;

   

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer > 2)
        {
            timer = 0;
            shoot();
        }

    }

    void shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }

}
