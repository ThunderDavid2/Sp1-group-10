using UnityEngine;

public class BossShockwave : MonoBehaviour
{
    private Rigidbody2D rgbd;
    private SpriteRenderer rend;
    private float timer;

    [SerializeField] private float shockwaveSpeed;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        rgbd = GetComponent<Rigidbody2D>();
        rgbd.linearVelocity = new Vector2(shockwaveSpeed, 0f);
    }

    private void Update()
    {
        if(shockwaveSpeed <0)
        {
            rend.flipX = true;
        }
        else
        {
            rend.flipX = false;
        }
        timer += Time.deltaTime;
        if(timer >= 1)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(1);
            Destroy(gameObject);
        }
    }
}
