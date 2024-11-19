using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Jump : MonoBehaviour
{
    //references
    [SerializeField] InputActionReference jump,groundPound;
    private Rigidbody rb;

    // variables
    [SerializeField] int groundPoundForce;
    [SerializeField] float groundPoundRadius;
    private bool isOnGround = true;

    private int jumpAmount = 2;

    [SerializeField] float jumpForce;

    [SerializeField] LayerMask hitLayer;


    private void OnEnable()
    {
        jump.action.performed += Jumping;
        groundPound.action.performed += GroundPound;
    }

    private void OnDisable()
    {
        jump.action.performed -= Jumping;
        groundPound.action.performed -= GroundPound;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
            collider.gameObject.SetActive(false);
        }
        
    }
    private void OnDrawGizmos()
    {
       // the ground pound sphere for debugging Gizmos.DrawWireSphere(gameObject.transform.position, groundPoundRadius);
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
