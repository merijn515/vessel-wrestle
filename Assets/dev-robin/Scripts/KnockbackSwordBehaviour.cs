using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackSwordBehaviour : MonoBehaviour
{
    [SerializeField] float swordRadius;

    [SerializeField] int swordImpact;

    private PlayerActions playerActions;
    public Animator animator;
    public bool canUse = false;
    // Start is called before the first frame update
    void Start()
    {
        playerActions = FindAnyObjectByType<PlayerActions>();
    }

    // Update is called once per frame
    void Update()
    {
        
        
            if (animator !=null&&animator.GetBool("Punch"))
            {
                KnockBackBehaviour();
            }
        
    }

    public void KnockBackBehaviour()
    {
        Collider[] punchCollider = Physics.OverlapSphere(gameObject.transform.position, swordRadius, playerActions.hitLayer);

        foreach (Collider collider in punchCollider)
        {
            if (gameObject != collider.gameObject.GetComponent<playerPickup>().objectHold) 
            {
                var rigid = collider.GetComponent<Rigidbody>();

                rigid.AddForce(collider.gameObject.transform.position * swordImpact);
                Destroy(gameObject);
            }
        }

    }
    private void OnDrawGizmos()
    {
        //debugging for sword knockback
        //Gizmos.DrawWireSphere(gameObject.transform.position, swordRadius);
    }
}
