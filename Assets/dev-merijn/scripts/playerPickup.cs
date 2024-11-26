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
    private Animator animator;

    [SerializeField]
    private Coroutine throwRoutine;

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
            objectHold.transform.position = rightHand.transform.position;
            objectHold.transform.rotation = rightHand.transform.rotation * Quaternion.Euler(0f, 0f, 90f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("itemPickup") && objectHold == null)
        {
            /*other.gameObject.transform.position = rightHand.transform.position;*/
            objectHold = other.gameObject;
            //other.gameObject.layer = 6;
            holdingObject = true;
        }
    }

    public void ThrowObject(InputAction.CallbackContext context)
    {
        if (context.performed && objectHold != null)
        {
            animator.SetTrigger("test trigger");
            throwRoutine = StartCoroutine(ThrowRoutine());
        }
    }

    private IEnumerator ThrowRoutine()
    {
        yield return new WaitForSeconds(0.4f);
        holdingObject = false;
        objectHold.GetComponent<Rigidbody>().AddForce(new Vector3(0f, 250f, 0f) + transform.right * 300f/* + new Vector3(540f, 0f, 0f)*/);
        yield return new WaitForSeconds(0.2f);
        objectHold.layer = 0;
        if (objectHold.GetComponent<cannonBall>() != null)
        {
            objectHold.GetComponent<cannonBall>().cannonBallExplode = StartCoroutine(objectHold.GetComponent<cannonBall>().BallExplode());
        }
        objectHold = null;
        StopCoroutine(ThrowRoutine());
    }
}
