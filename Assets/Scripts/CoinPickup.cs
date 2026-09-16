using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] private GameObject coinParticleSystem;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")){ 
            other.gameObject.GetComponent<PlayerQuest>().AddCoin();
            Instantiate(coinParticleSystem, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

}
