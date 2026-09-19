using UnityEngine;
using UnityEngine.U2D;

public class EnemyBossProjectiles : MonoBehaviour
{
    private Rigidbody2D rgbd;
    private SpriteRenderer rend;
    private float timer;
    private GameObject player;

    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileHeight;


    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        rgbd = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > 20)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("BossEnemy"))
        {
            Destroy(gameObject);
        }
    }
}
