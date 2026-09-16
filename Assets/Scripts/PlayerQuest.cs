using UnityEngine;
using TMPro;
using Unity.VisualScripting;
public class PlayerQuest : MonoBehaviour
{
    [SerializeField] int coinsToCollect = 10;
    [SerializeField] int hiddenCoinsToCollect = 20;
    [SerializeField] private TMP_Text coinText;
    [SerializeField] private AudioClip coinPickupSoundEffect;

 
  
    private int coins = 0;
    private AudioSource audioSource;

    private void Start()
    {
        coinText.text = "" + coins;
        audioSource = GetComponent<AudioSource>();
    }
    public void AddCoin()
    {
        coins++;
        coinText.text = "" + coins;
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.PlayOneShot(coinPickupSoundEffect);
    }

    public int GetCoins() { return coins; }
    public int GetCoinsToCollect() { return coinsToCollect; }

    public int GetHiddenCoinsToCollect() { return hiddenCoinsToCollect; }

}
