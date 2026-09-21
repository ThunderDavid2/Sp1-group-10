using UnityEditor.Analytics;
using UnityEngine;

public class SlimePickup : MonoBehaviour
{

    [SerializeField] private GameObject slimeDropParticle;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerQuest>().AddCoin();
            Instantiate(slimeDropParticle, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }


}
