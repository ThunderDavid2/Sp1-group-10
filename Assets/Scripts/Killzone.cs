using UnityEngine;
using UnityEngine.SceneManagement;

public class Killzone : MonoBehaviour
{
    [SerializeField] private Transform spawnPosition;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(3);
            // other.transform.position = spawnPosition.position;
            //  other.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        }
    }


}