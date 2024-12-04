using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerActions : MonoBehaviour
{
    //references

    public LayerMask hitLayer;

    private Rigidbody rb;

   [SerializeField] private GameObject punchArm;

    private Animator animator;

    private playerMovement playerMovement;

    // variables
    [SerializeField] int groundPoundForce;
    [SerializeField] float groundPoundRadius;

    [SerializeField] float punchRadius;

    private bool isOnGround = true;

   [SerializeField] private int jumpAmount = 2;

    [SerializeField] float jumpForce;

    [SerializeField] int punchImpact;
   
   
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();

        playerMovement = GetComponent<playerMovement>();
       
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("IsJumping", !isOnGround);
        animator.SetFloat("JumpDir", Mathf.Clamp(rb.velocity.y,-1,1));
        AnimationMovementCheck();
        
    }

    public void Punching(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            animator.SetBool("Punch", true);
       
            StartCoroutine(PunchingEnum());
        }

       
    }
    public void Jumping(InputAction.CallbackContext context)
    {

        
            if (jumpAmount > 0 && context.performed)
            {
                jumpAmount--;
                isOnGround = false;

                playerMovement.isMoving = false;
                rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
               

            }
        
    }

    public void GroundPound(InputAction.CallbackContext context)
    {
        if(!isOnGround && context.performed)
        {
            //ground pound animations still needs to be added
            animator.SetBool("GroundPound", true);
            StartCoroutine(GroundPoundEnum());
        }
    }

    private IEnumerator GroundPoundEnum()
    {
        WaitForSeconds wait = new WaitForSeconds(.5f);
        rb.useGravity = false;
        rb.velocity = Vector3.zero;

        yield return wait;
        rb.useGravity = true;
        rb.AddForce(0,-40,0,ForceMode.Impulse);
    
        yield return new WaitForSeconds(.25f);
        // play ground pound vfx
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, groundPoundRadius,hitLayer);

        foreach (Collider collider in hitColliders)
        {
            if (collider.gameObject != gameObject)
            {
                // add damage to the enemy instead
                collider.GetComponentInChildren<HealthController>().playerHealth--;
                var rigid = collider.GetComponent<Rigidbody>();

                rigid.AddForce(collider.gameObject.transform.position * groundPoundForce, ForceMode.Impulse);
               
            }
        }
        
    }

    private IEnumerator PunchingEnum()
    {
        WaitForSeconds wait = new WaitForSeconds(.3f);

        yield return wait;

        // if its holding the sword it will do that amount of damage
        if (!gameObject.GetComponent<playerPickup>().holdingObject)
        {
            Collider[] punchCollider = Physics.OverlapSphere(punchArm.transform.position, punchRadius, hitLayer);

            foreach (Collider collider in punchCollider)
            {
                if (collider.gameObject != gameObject)
                {
                    
                    collider.GetComponentInChildren<HealthController>().playerHealth--;
                    var rigid = collider.GetComponent<Rigidbody>();

                    rigid.AddForce(collider.gameObject.transform.position * punchImpact,ForceMode.Impulse);
                    
                }
            }
        }

        animator.SetBool("Punch", false);
    }
    private void OnDrawGizmos()
    {
        // the ground pound sphere for debugging -
        // Gizmos.DrawWireSphere(gameObject.transform.position, groundPoundRadius);

        // melee sphere for debugging
         //Gizmos.DrawWireSphere(punchArm.transform.position, punchRadius);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isOnGround = true;
            animator.SetBool("GroundPound", false);
            jumpAmount = 2;
        }
    }

    private void AnimationMovementCheck()
    {
        if (animator.GetBool("GroundPound"))
        {
            playerMovement.isMoving = false;
        }
        else if (animator.GetBool("Punch"))
        {
            playerMovement.isMoving = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Death barier")
        {
            gameObject.GetComponentInChildren<HealthController>().playerHealth = 0;
            
            Debug.Log("Player died");
        }
    }
}
