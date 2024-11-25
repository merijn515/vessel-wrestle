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
    [SerializeField]
    private bool isMoving;

    [SerializeField]
    private bool isSlowed;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private SkinnedMeshRenderer meshRenderer;

/*    [SerializeField] InputActionReference movement;*/

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        meshRenderer = transform.GetChild(1).GetComponent<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.forward = playerRb.velocity;

        /*        if (isMoving == true)
                {
                playerRb.velocity = testMove * speed * Time.deltaTime;
                }
                if (testMove != Vector3.zero)
                {
                    Debug.Log(playerRb.velocity);
                    transform.right = playerRb.velocity + new Vector3(0f, 0f, 90f);
                }*/
        if (Input.GetKeyDown(KeyCode.K))
        {
            animator.SetTrigger("test death trigger");
            meshRenderer.material.color = new Color(0f, 0f, 0f, 1f);
        }
        if (isSlowed == false)
        {
            speed = 5f;
        }
        if (isSlowed == true)
        {
            speed = 2.5f;
        }
    }
    private void FixedUpdate()
    {
        if (isMoving == true)
        {
            playerRb.velocity = testMove * speed;
        }
        if (testMove != Vector3.zero)
        {
            //Debug.Log(playerRb.velocity);
            transform.right = testMove/* + new Vector3(0f, 0f, 90f)*/;
            animator.SetBool("test move trigger", true);
        }
        //else
        if(testMove == Vector3.zero)
        {
            animator.SetBool("test move trigger", false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("slownessOil"))
        {
            isSlowed = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("slownessOil"))
        {
            isSlowed = false;
        }
    }
    public void Move(InputAction.CallbackContext context)
    {
        testMove = context.action.ReadValue<Vector3>();
        //Debug.Log(testMove);
        if (context.performed)
        {
            //playerRb.velocity = testMove * speed;
            isMoving = true;
            //animator.SetBool("test move trigger", true);
            //transform.right = playerRb.velocity + new Vector3(0f, 0f, 90f);
            //testMove = context.action.ReadValue<Vector3>();
        }
        //else 
        if(context.canceled)
        {
            playerRb.velocity = Vector3.zero;
            isMoving = false;
            //animator.SetBool("test move trigger", false);
            //transform.right = playerRb.velocity + new Vector3(0f, 0f, 90f);
        }

        /*if (playerRb.velocity != Vector3.zero)
        {
            transform.right = playerRb.velocity + new Vector3(0f, 0f, 90f);
        }*/
        //Debug.Log(playerRb.velocity);
    }
}
