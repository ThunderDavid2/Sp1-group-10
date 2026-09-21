using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestChecker : MonoBehaviour
{
    [SerializeField] private GameObject panel, finishedText, unfinishedText, secretText, player;
    [SerializeField] private int levelIndex;
     
    private Animator anim;
    private PlayerMovement playerMovement;
    private void Start()
    {
        anim = GetComponent<Animator>();
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(other.GetComponent<PlayerQuest>().GetCoins() >= other.GetComponent<PlayerQuest>().GetHiddenCoinsToCollect())
                    {
                panel.SetActive(true);
                secretText.SetActive(true);
                anim.SetTrigger("Flag");
                Invoke(nameof(LoadNextLevel), 10f);
                playerMovement.unlocked = true;
                    }
            else 
            if(other.GetComponent<PlayerQuest>().GetCoins() >= other.GetComponent<PlayerQuest>().GetCoinsToCollect())
            {
                panel.SetActive(true);
                finishedText.SetActive(true);
                anim.SetTrigger("Flag");
                Invoke(nameof(LoadNextLevel), 3.0f);
                playerMovement.unlocked = true;
            }
            else
            {
                panel.SetActive(true);
                unfinishedText.SetActive(true);
            }
        }
        
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(levelIndex);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        panel.SetActive(false);
        finishedText.SetActive(false);
        unfinishedText.SetActive(false);
    }
}