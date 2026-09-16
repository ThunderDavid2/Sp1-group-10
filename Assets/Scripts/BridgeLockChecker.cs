using UnityEngine;

public class BridgeLockChecker : MonoBehaviour
{
    [SerializeField] private GameObject bridgeLockChecker;
    private Animator anim;

    private bool hasTriggered = false;
    void Start()
    {
        anim = GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (hasTriggered) return;
        if (other.CompareTag("Player"))
        {
            
            if (other.GetComponent<BridgeLock>().GetKeys() >= other.GetComponent<BridgeLock>().GetkeysToCollect())
            {
                {
                    anim.SetTrigger("Move");
                    bridgeLockChecker.SetActive(false);
                    hasTriggered = true;
                }
                other.gameObject.GetComponent<BridgeLock>().RemoveKeys();
                
                
            }
            else
            {
                return;
            }
            
        }
    }
}






        


    
