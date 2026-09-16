using JetBrains.Annotations;
using System.Data;
using System.Xml.Schema;
using Unity.VisualScripting;
using Unity.XR.Oculus.Input;
using UnityEngine;
using UnityEngine.Rendering;

public class KeyFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float followSpeed = 5f;

    [SerializeField] private float offsetX = 1f;
    [SerializeField] private float offsetY = 0f;

    [SerializeField] private bool isFollowing = false;
    [SerializeField] private bool hasBeenPickedUp = false;
  
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !hasBeenPickedUp)
        {
            hasBeenPickedUp = true;
            player = other.transform;
            isFollowing = true;
            other.gameObject.GetComponent<BridgeLock>().AddKeys();
        }
        
    }
    
    private void Update()
    {
        if(isFollowing == true)
        {
            float direction = player.GetComponent<SpriteRenderer>().flipX ? -1f : 1f;

            Vector2 targetPos = new Vector2(player.position.x - offsetX * direction, player.position.y + offsetY);
            transform.position = Vector2.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);
            int currentKeys = player.GetComponent<BridgeLock>().GetKeys();
            if (currentKeys == 0)
            {
                Destroy(gameObject);
            }
            }

           
    
            
    }
  
    
 
    
        
    

}
