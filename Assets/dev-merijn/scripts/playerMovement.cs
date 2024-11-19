using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody playerRb;
    [SerializeField]
    private float speed;
    [SerializeField]
    private Vector3 testMove;
    [SerializeField] InputActionReference movement;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Move(InputAction.CallbackContext context)
    {
        testMove = context.action.ReadValue<Vector3>();
        //Debug.Log(testMove);
        if (context.performed)
        {
            playerRb.velocity = testMove * speed;
        }
        else
        {
            playerRb.velocity = Vector3.zero;
        }
        //Debug.Log(playerRb.velocity);
    }
}
