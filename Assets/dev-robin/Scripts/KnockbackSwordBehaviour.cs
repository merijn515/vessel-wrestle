using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackSwordBehaviour : MonoBehaviour
{
    [SerializeField] float swordRadius;

    [SerializeField] int swordImpact;

    private PlayerActions playerActions;
    // Start is called before the first frame update
    void Start()
    {
        playerActions = FindAnyObjectByType<PlayerActions>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator KnockBackBehaviour()
    {
        yield return new WaitForSeconds(.4f);
        Collider[] punchCollider = Physics.OverlapSphere(gameObject.transform.position, swordRadius, playerActions.hitLayer);

        foreach (Collider collider in punchCollider)
        {
            // add damage to the enemy instead
            var rigid = collider.GetComponent<Rigidbody>();

            rigid.AddForce(collider.gameObject.transform.position * swordImpact);
        }

    }
}
