using UnityEngine;

public class Level2Unlocks : MonoBehaviour
{
    [SerializeField] private GameObject textPopup;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            textPopup.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            textPopup.SetActive(false);
        }
    }
}


