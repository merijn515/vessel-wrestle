using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Jump : MonoBehaviour
{
    [SerializeField] InputActionReference jump;
    private bool isOnGround = true;
    [SerializeField] float jumpForce;
    // Start is called before the first frame update

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
            isOnGround = false;
            gameObject.GetComponent<Rigidbody>().AddForce(0,jumpForce,0,ForceMode.Impulse);
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
