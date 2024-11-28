using System;
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
    
    public bool isMoving;

    [SerializeField]
    private bool isSlowed;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private SkinnedMeshRenderer meshRenderer;

    private float baseSpeed;
   

    /*    [SerializeField] InputActionReference movement;*/

    // Start is called before the first frame update

   
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        baseSpeed = speed;
       // meshRenderer = transform.GetChild(1).GetComponent<SkinnedMeshRenderer>();
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
            speed = baseSpeed;
        }
        if (isSlowed == true)
        {
            speed = baseSpeed -3f;
        }
    }
    private void FixedUpdate()
    {
        if (isMoving == true)
        {
            playerRb.velocity = testMove * speed + Physics.gravity * 0.3f;
        }
        /*        if (testMove != Vector3.zero)
                {
                    //Debug.Log(playerRb.velocity);
                    transform.right = testMove*//* + new Vector3(0f, 0f, 90f)*//*;
                    animator.SetBool("test move trigger", true);

                }
                //else
                if(testMove == Vector3.zero)
                {
                    animator.SetBool("test move trigger", false);

                }*/

        if (testMove != Vector3.zero)
        {
            transform.right = testMove;
            if (GetComponent<playerPickup>().objectHold != null && GetComponent<playerPickup>().objectHold.CompareTag("barrel"))
            {
                animator.SetBool("test moveBarrel trigger", true);
            } //else
            if (GetComponent<playerPickup>().objectHold == null || (GetComponent<playerPickup>().objectHold != null && !GetComponent<playerPickup>().objectHold.CompareTag("barrel")))
            {
                //Debug.Log(playerRb.velocity);
                transform.right = testMove/* + new Vector3(0f, 0f, 90f)*/;
                animator.SetBool("test move trigger", true);
            }

        }

        if (testMove == Vector3.zero)
            //else
            if (testMove == Vector3.zero)
            {
                //animator.SetBool("test move trigger", false);
                if (GetComponent<playerPickup>().objectHold != null && GetComponent<playerPickup>().objectHold.CompareTag("barrel"))
                {
                    animator.SetBool("test moveBarrel trigger", false);
                }
                if (GetComponent<playerPickup>().objectHold == null || (GetComponent<playerPickup>().objectHold != null && !GetComponent<playerPickup>().objectHold.CompareTag("barrel")))
                {
                    animator.SetBool("test move trigger", false);
                }
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
