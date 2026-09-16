using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class QuestLogBookText : MonoBehaviour
{
    public TextMeshProUGUI displayText;
    private PlayerQuest playerQuest;
    private int coinsToCollect;
    private int coins;

    private void Start()
    {
        playerQuest = GetComponent<PlayerQuest>();
    }
    public void QuestProgressText()
    {
        coins = playerQuest.GetCoins();
        coinsToCollect = playerQuest.GetCoinsToCollect();
        displayText.text = coins + " Coins out of " + coinsToCollect;
    }

}
