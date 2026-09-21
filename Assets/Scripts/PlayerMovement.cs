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
    [SerializeField] private InputActionReference blink;
  
    private float moveDirection;
    // Sparar input från spelaren
    [SerializeField] private float moveSpeed = 1f;
    // Hur snabbt spelaren rör sig på x axeln
    [SerializeField] private float jumpForce = 200f;
    // Kraften som skickas upp i ett hopp
    [SerializeField] private float runSpeed = 2f;
    [SerializeField] private float blinkForce = 5000f;
    
    
    [SerializeField] private Transform leftFoot, rightFoot;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float raycastDistance = 0.25f;
    [SerializeField] private float wallDistance = 0.25f;
    [SerializeField] private AudioClip[] jumpSounds;
    [SerializeField] private ParticleSystem jumpParticleSystem;
    [SerializeField] private float wallGlideSpeed = -5;

    private bool isRunning;
    private bool hasJumped;
    private bool onWall;
    bool running = true;
    public bool unlocked = false;

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
        blink.action.started += Blink;


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

        Glide();
        // rgbd.linearVelocity = new Vector2(moveDirection * moveSpeed * Time.deltaTime, rgbd.linearVelocity.y);
    }

    private void OnDisable()
    {
        jump.action.started -= Jump;
        blink.action.started -= Blink;
        
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
            PerformJump();
            hasJumped = true;
        }
        else if (hasJumped == true && unlocked == true)
        {
           PerformJump();
           hasJumped = false;
        }
        else if (isOnWall())
        {
            PerformJump();
           //hasJumped = true;
        }
    }

    private void PerformJump()
    {
        rgbd.linearVelocity = new Vector2(rgbd.linearVelocity.x, 0f);
        rgbd.AddForce(new Vector2(0, jumpForce));
        jumpParticleSystem.Play();
        int randomJumpSound = Random.Range(0, jumpSounds.Length);
        audioSource.PlayOneShot(jumpSounds[randomJumpSound]);

   
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

    private bool isOnWall()
    {
        if (unlocked == true){
            RaycastHit2D leftHit = Physics2D.Raycast(rgbd.worldCenterOfMass, Vector2.left, wallDistance, whatIsGround);
            RaycastHit2D rightHit = Physics2D.Raycast(rgbd.worldCenterOfMass, Vector2.right, wallDistance, whatIsGround);
            Debug.DrawRay(transform.position, Vector2.left * wallDistance, Color.cyan);
            Debug.DrawRay(transform.position, Vector2.right * wallDistance, Color.cyan);
            if ( leftHit || rightHit)
            {
                return true;
            }
            else
            {
                return false; 
            }
        }
        else
        {
            return false;
        }
    }

    private void Glide()
{
  
        if (isOnWall() && rgbd.linearVelocity.y < wallGlideSpeed)
        {
            rgbd.linearVelocity = new Vector2(rgbd.linearVelocity.x, wallGlideSpeed);
        }
    
    }
    
    private void Blink(InputAction.CallbackContext context)
    {   
        if (unlocked == true){
            if (rend.flipX)
            {
                rgbd.AddForce( new Vector2(-blinkForce, 0));
            }
            else
            {
                rgbd.AddForce( new Vector2(blinkForce, 0));
            }
        }

    }


}


    