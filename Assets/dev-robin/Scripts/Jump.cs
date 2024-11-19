using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Jump : MonoBehaviour
{
    //references
    [SerializeField] InputActionReference jump,groundPound,punch,dash;

    private Rigidbody rb;
    private GameObject punchArm;

    // variables
    [SerializeField] int groundPoundForce;
    [SerializeField] float groundPoundRadius;

    [SerializeField] float punchRadius;

    private bool isOnGround = true;

    private int jumpAmount = 2;

    [SerializeField] float jumpForce;

    [SerializeField] LayerMask hitLayer;


    private void OnEnable()
    {
        jump.action.performed += Jumping;
        groundPound.action.performed += GroundPound;
        punch.action.performed += Punching;
    }

    private void OnDisable()
    {
        jump.action.performed -= Jumping;
        groundPound.action.performed -= GroundPound;
        punch.action.performed -= Punching;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        punchArm = GameObject.FindGameObjectWithTag("Punch arm").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Punching(InputAction.CallbackContext context)
    {
        //Play punching animation
        StartCoroutine(PunchingEnum());
    }

    private void Jumping(InputAction.CallbackContext context)
    {
        if (jumpAmount > 0)
        {
            jumpAmount--;
            isOnGround = false;
            rb.AddForce(0,jumpForce,0,ForceMode.Impulse);
            rb.velocity = Vector3.zero;
        }
        
    }

    private void GroundPound(InputAction.CallbackContext context)
    {
        if(!isOnGround)
        {
            //ground pound animations still needs to be added
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
        rb.AddForce(0,-groundPoundForce,0,ForceMode.Impulse);

        yield return new WaitForSeconds(.25f);
        // play ground pound vfx
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, groundPoundRadius,hitLayer);
      
        foreach (Collider collider in hitColliders)
        {
            // add damage to the enemy instead
            collider.gameObject.SetActive(false);
        }
        
    }

    private IEnumerator PunchingEnum()
    {
        WaitForSeconds wait = new WaitForSeconds(.25f);

        yield return wait;

        Collider[] punchCollider = Physics.OverlapSphere(punchArm.transform.position, punchRadius, hitLayer);

        foreach (Collider collider in punchCollider)
        {
            // add damage to the enemy instead
            collider.gameObject.SetActive(false);
        }
    }
    private void OnDrawGizmos()
    {
        // the ground pound sphere for debugging - Gizmos.DrawWireSphere(gameObject.transform.position, groundPoundRadius);
       // punch sphere for debugging -  Gizmos.DrawWireSphere(punchArm.transform.position, punchRadius);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isOnGround = true;
            jumpAmount = 2;
        }
    }
}
