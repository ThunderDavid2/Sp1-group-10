using UnityEngine;

public class PulsatingText : MonoBehaviour
{
    [SerializeField] private float minSize = 0.8f;
    // Minsta skalan
    [SerializeField] private float maxSize = 1.2f;
    // Högsta skalan
    [SerializeField] private float speed = 2f;
    // Hastigheten texten pulserar
    
    private void Update()
    {
        float t = (Mathf.Sin(Time.time * speed) + 1f) / 2f;
        // Skapar en Sin våg som ändrar vinkeln med tiden och hur snabbt den rör sig 
        // +1f gör så att värdet blir positivt 
        // /2f gör så att värdet går mellan 0 till 1. Gör så att t går mellan 0 - 1 - 0 i en loop
        // om värdet är negativt blir texten mindre än den minsta storleken skrivet i "minSize"
        float scale = Mathf.Lerp(minSize, maxSize, t);
        // Get ett värde mellan minSize och maxSize baserat på t 
        transform.localScale = new Vector2(scale, scale);
        // Gör så både x och y storleken ändras lika mycket 
    }

}
