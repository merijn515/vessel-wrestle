using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackSwordBehaviour : MonoBehaviour
{
    [SerializeField] float swordRadius;

    [SerializeField] int swordImpact;

    private PlayerActions playerActions;
    private playerPickup playerPickup;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        //need to change the references when multiple players are added
        playerActions = FindAnyObjectByType<PlayerActions>();
        playerPickup = FindAnyObjectByType<playerPickup>();

        animator = playerPickup.rightHand.GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetBool("Punch") && playerPickup.holdingObject)
        {
           
            KnockBackBehaviour();

        }
    }

    public void KnockBackBehaviour()
    {
        Collider[] punchCollider = Physics.OverlapSphere(gameObject.transform.position, swordRadius, playerActions.hitLayer);

        foreach (Collider collider in punchCollider)
        {
            if (collider.gameObject != playerActions.gameObject)
            {
                // add damage to the enemy instead
                var rigid = collider.GetComponent<Rigidbody>();

                rigid.AddForce(collider.gameObject.transform.position * swordImpact);
                Destroy(gameObject);
                Debug.Log("Swordhit");
            }
        }

    }
    private void OnDrawGizmos()
    {
        //debugging for sword knockback
        //Gizmos.DrawWireSphere(gameObject.transform.position, swordRadius);
    }
}
