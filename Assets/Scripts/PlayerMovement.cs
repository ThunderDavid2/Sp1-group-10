using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using static Unity.Burst.Intrinsics.X86.Avx;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputActionReference move;
    // SerializeField gör att man kan skriva in den inuti editorn
    [SerializeField] private InputActionReference jump;

    [SerializeField] private InputActionReference run;
  
    private float moveDirection;
    // Sparar input från spelaren
    [SerializeField] private float moveSpeed = 1f;
    // Hur snabbt spelaren rör sig på x axeln
    [SerializeField] private float jumpForce = 200f;
    // Kraften som skickas upp i ett hopp
    [SerializeField] private float runSpeed = 2f;
    
    [SerializeField] private Transform leftFoot, rightFoot;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float raycastDistance = 0.25f;
    [SerializeField] private AudioClip[] jumpSounds;
    [SerializeField] private ParticleSystem jumpParticleSystem;

    private bool isRunning;
    bool running = true;


    private AudioSource audioSource;
    private Rigidbody2D rgbd;
    // Fysik
    private SpriteRenderer rend; 

  
    private Animator anim;
    // Animationer

     // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        jump.action.started += Jump;


    }   



public void PlayerDamage()
    {
        anim.SetTrigger("PlayerHit");
    }
    
       
    

    // Update is called once per frame
    void Update()
    {
        moveDirection = move.action.ReadValue<float>();
        // Läser spelarens input
        isRunning = run.action.IsPressed();

      
        anim.SetFloat("MoveSpeed",Mathf.Abs(rgbd.linearVelocity.x));
        anim.SetFloat("VerticalSpeed", (rgbd.linearVelocity.y));
        anim.SetBool("IsGrounded", CheckIsGrounded());
        // Skickar värden till Animator för att byta animationer
        if (moveDirection < 0f)
        {
            FlipSprite(true);
        }

        if (moveDirection > 0f)
        {
            FlipSprite(false);
        }
        // Vänder spriten baserat på vilket håll den går åt 
    }
    private void FixedUpdate()
    {
        if(!running)
        {
            return;
        }
        float currentSpeed = isRunning ? runSpeed : moveSpeed;
        rgbd.linearVelocity = new Vector2(moveDirection * currentSpeed, rgbd.linearVelocity.y);


        // rgbd.linearVelocity = new Vector2(moveDirection * moveSpeed * Time.deltaTime, rgbd.linearVelocity.y);
    }

    private void OnDisable()
    {
        jump.action.started -= Jump;
    }
    // Fix för att karaktären inte ska hoppa högre varje gång spelet startas

    private void FlipSprite(bool direction)
        // "void" betyder att metoden bara kör klart och inget annat händer 
        // "direction" kan heta vad som helst 
    {
        rend.flipX = direction;
    }
    private void Jump(InputAction.CallbackContext context)
    {
        if (CheckIsGrounded() == true)
        {
            rgbd.AddForce(new Vector2(0, jumpForce));
            jumpParticleSystem.Play();
            int randomJumpSound = Random.Range(0, jumpSounds.Length);
            audioSource.PlayOneShot(jumpSounds[randomJumpSound]);

        }
    }
    private bool CheckIsGrounded() 
    {
        RaycastHit2D leftHit = Physics2D.Raycast(leftFoot.position, Vector2.down, raycastDistance, whatIsGround);
        RaycastHit2D rightHit = Physics2D.Raycast(rightFoot.position, Vector2.down, raycastDistance, whatIsGround);
        // Debug.DrawRay(leftFoot.position, Vector2.down * raycastDistance, Color.purple, 0.25f);
        // Debug.DrawRay(rightFoot.position, Vector2.down * raycastDistance, Color.purple, 0.25f);
        // Gör så två strålar skickas ner från fötterna
        if (leftHit.collider != null && leftHit || rightHit.collider != null && rightHit)
        {
            return true;
        }
        else
        {
            return false; 
        }
        
    }

    
    public void TakeKnockback(float knockbackForce, float upwardsForce)
    {
        running = false;
        rgbd.AddForce(new Vector2(knockbackForce, upwardsForce));
        Invoke(nameof(CanMoveAgain), 0.25f);
    }

    private void CanMoveAgain()
    {
        running = true;
    }


}


    