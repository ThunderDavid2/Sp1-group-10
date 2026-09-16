using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
public class Interact : MonoBehaviour
{
    [SerializeField] private InputActionReference interactButton;

    [SerializeField] private GameObject panel, panelText, otherPanelClose, interactTutorial;

    private bool interact;
    private bool interaction = false;

    private bool playerInRange;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
    private void Update()
    {
        InteractPressed();
    }
    public void InteractPressed()
    {
        if (playerInRange == true)
        {
            interact = interactButton.action.triggered;
            if (interact)
            {
                interaction = !interaction;
            }
            Interacted();
        }
    }
    private void Interacted()
    {
        if (playerInRange == true)
        {
            if (interaction == true)
            {
                interactTutorial.SetActive(false);
                otherPanelClose.SetActive(false);
                panel.SetActive(true);
                panelText.SetActive(true);
            }
            else
            {
                panel.SetActive(false);
                panelText.SetActive(false);
            }
        }
    }
}
