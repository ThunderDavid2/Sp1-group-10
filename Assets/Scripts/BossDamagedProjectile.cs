using System.Data;
using UnityEditor.Analytics;
using UnityEngine;

public class BossDamagedProjectile : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform projectilePos;

    private bool hasFired = false;



    public void Damaged()
    {
        Instantiate(projectile, projectilePos.position, Quaternion.identity);
    }
        
    


}
