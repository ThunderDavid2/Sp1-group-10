using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField] private GameObject textPopup, interactTutorial, panel;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactTutorial.SetActive(true);
            textPopup.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactTutorial.SetActive(false);
            textPopup.SetActive(false);
            panel.SetActive(false);
        }
    }
}
