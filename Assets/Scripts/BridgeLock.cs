using TMPro;
using UnityEngine;

public class BridgeLock : MonoBehaviour
{
    [SerializeField] int keysToCollect = 1;
    [SerializeField] private TMP_Text keyText;
    private int keys = 0;

    private void Start()
    {
        keyText.text = "" + keys;
    }
    public void AddKeys()
    {
        keys++;
        keyText.text = "" + keys;
    }
    public void RemoveKeys()
    {
        keys--;
        keyText.text = "" + -keys;
    
    }
    
  

    public int GetKeys() { return keys; }
    public int GetkeysToCollect() { return keysToCollect; }

 
}

  
