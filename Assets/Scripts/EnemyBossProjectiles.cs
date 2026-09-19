using System.Collections;
using UnityEngine;
using UnityEngine.U2D;

public class EnemyBossProjectiles : MonoBehaviour
{
    private Rigidbody2D rgbd;
    private float timer;

    private bool spawnImmunity = true;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileHeight;


    private void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
        StartCoroutine(disableSpawnImmunity());
  
    }


        
        
    private IEnumerator disableSpawnImmunity()
    {
        yield return new WaitForSeconds(0.2f);
        spawnImmunity = false;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > 20)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (spawnImmunity == true) return;
        if(other.gameObject.CompareTag("BossEnemy"))
        {
            Destroy(gameObject);
        }
    }

}
