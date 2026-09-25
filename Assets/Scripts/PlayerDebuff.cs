using System.Collections;
using UnityEngine;
using UnityEngine.AdaptivePerformance;
using UnityEngine.UI;
public class PlayerDebuff : MonoBehaviour
{
    [SerializeField] private ParticleSystem slimeParticles;
    [SerializeField] private GameObject debuffImage;
    private bool isDebuffed;

    private Rigidbody2D rgbd;

    private void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
        debuffImage.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("BossProjectiles"))
        {
            if(!isDebuffed)
            {
                Destroy(other.gameObject);
            }
        }
    }
    public void SlimeEffect()
    {
        isDebuffed = true;
        CancelInvoke(nameof(RemoveSlimeDebuff));
        slimeParticles.Play();
        rgbd.linearDamping = 2f;
        debuffImage.SetActive(true);
        Invoke(nameof(RemoveSlimeDebuff), 5f);
    }

    public void RemoveSlimeDebuff()
    {
        isDebuffed = false;
        debuffImage.SetActive(false);
        rgbd.linearDamping = 0f;
    }
}
