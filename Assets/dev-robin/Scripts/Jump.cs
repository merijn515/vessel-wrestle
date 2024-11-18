using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Jump : MonoBehaviour
{
    //references
    [SerializeField] InputActionReference jump;

    // variables
    private bool isOnGround = true;
    private int jumpAmount = 2;
    [SerializeField] float jumpForce;

    private void OnEnable()
    {
        jump.action.performed += Jumping;
    }

    private void OnDisable()
    {
        jump.action.performed -= Jumping;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Jumping(InputAction.CallbackContext context)
    {
        if (isOnGround)
        {
            jumpAmount--;
            gameObject.GetComponent<Rigidbody>().AddForce(0,jumpForce,0,ForceMode.Impulse);
        }

        //this is when he double jumps
        if(jumpAmount == 0)
        {
            isOnGround = false;
            jumpAmount = 2;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isOnGround = true;
        }
    }
}
