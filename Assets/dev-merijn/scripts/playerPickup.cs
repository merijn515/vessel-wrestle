using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerPickup : MonoBehaviour
{
    [SerializeField]
    public GameObject rightHand;
    [SerializeField]
    public GameObject objectHold;
    [SerializeField]
    public bool holdingObject;

    [SerializeField]
    private GameObject testBarrelPos;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Coroutine throwRoutine;

    [SerializeField] LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if (objectHold != null)
        {
            if (holdingObject == true)
            {
                objectHold.GetComponent<Rigidbody>().velocity = Vector3.zero;
                if (objectHold.CompareTag("itemPickup"))
                {
                    objectHold.transform.position = rightHand.transform.position;
                    objectHold.transform.rotation = rightHand.transform.rotation * Quaternion.Euler(0f, 0f, 90f);
                }
                if (objectHold.CompareTag("barrel"))
                {
                    
                    objectHold.transform.position = testBarrelPos.transform.position; // /// // /// // // // /// // // /// // // // // testBarrelPos
                    objectHold.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(90f, 0f, 0);
                    /*objectHold.transform.forward = gameObject.transform.right;*/
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (objectHold == null && (other.gameObject.CompareTag("itemPickup") || other.gameObject.CompareTag("barrel")))
        {
           
            /*if (other.gameObject.CompareTag("itemPickup"))
            {
                objectHold = other.gameObject;
                other.gameObject.layer = 6;
                holdingObject = true;
            }*/
            

            
           
            if(other.gameObject.GetComponent<KnockbackSwordBehaviour>() != null)
            {
                other.gameObject.GetComponent<KnockbackSwordBehaviour>().canUse= true;
                other.gameObject.GetComponent<BoxCollider>().enabled = false;
                other.gameObject.GetComponent<CapsuleCollider>().enabled = false;
                other.gameObject.GetComponent<KnockbackSwordBehaviour>().animator = gameObject.GetComponentInChildren<Animator>();
                other.gameObject.GetComponent<KnockbackSwordBehaviour>().playerPickup = gameObject.GetComponent<playerPickup>();
                
                


            } else if (other.gameObject.GetComponent<cannonBall>() != null)
            {
                
                other.gameObject.GetComponent<SphereCollider>().enabled = false;
                other.gameObject.GetComponent<BoxCollider>().excludeLayers = layerMask;

            } else if (other.gameObject.GetComponent<barrel>() != null)
            {
                //objectHold = other.gameObject;
                //other.gameObject.layer = 6;
                //holdingObject = true;

                other.gameObject.GetComponent<CapsuleCollider>().enabled= false;
                other.gameObject.GetComponent<BoxCollider>().excludeLayers = layerMask;
                GetComponent<playerMovement>().animator.SetBool("test move trigger", false);
                animator.SetBool("test holdBarrel", true);

            } else if ( other.gameObject.GetComponent<Molotovbehaviour>() != null)
            {
                other.gameObject.GetComponent<CapsuleCollider>().enabled = false;
                other.gameObject.GetComponent<BoxCollider>().excludeLayers = layerMask;
            }

            /*other.gameObject.transform.position = rightHand.transform.position;*/
            objectHold = other.gameObject;
            other.gameObject.layer = 6;
            holdingObject = true;
        } 
    }

    public void ThrowObject(InputAction.CallbackContext context)
    {
        if (context.performed && objectHold != null)
        {
            if (objectHold.CompareTag("itemPickup"))
            {
                animator.SetTrigger("test trigger");
            }
            if (objectHold.CompareTag("barrel"))
            {
                animator.SetTrigger("test barrelThrow");
            }

            if (objectHold.gameObject.GetComponent<Molotovbehaviour>() != null)
            {
                objectHold.gameObject.GetComponent<Molotovbehaviour>().canBreak = true;
            }

                throwRoutine = StartCoroutine(ThrowRoutine());
        }
    }

    private IEnumerator ThrowRoutine()
    {
        yield return new WaitForSeconds(0.4f);
        holdingObject = false;
        objectHold.GetComponent<Rigidbody>().AddForce(new Vector3(0f, 300f, 0f) + transform.right * 800f/* + new Vector3(540f, 0f, 0f)*/);
        yield return new WaitForSeconds(0.2f);
        objectHold.layer = 0;
        if (objectHold.GetComponent<cannonBall>() != null)
        {
            objectHold.GetComponent<cannonBall>().cannonBallExplode = StartCoroutine(objectHold.GetComponent<cannonBall>().BallExplode());
        }
        if (objectHold.GetComponent<barrel>() != null)
        {
            objectHold.GetComponent<barrel>().isThrown = true;
            GetComponent<playerMovement>().animator.SetBool("test moveBarrel trigger", false);
            animator.SetBool("test holdBarrel", false);
        }
        objectHold = null;
        StopCoroutine(ThrowRoutine());
    }
}
