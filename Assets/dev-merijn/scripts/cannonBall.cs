using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannonBall : MonoBehaviour
{
    [SerializeField]
    public Coroutine cannonBallExplode;
    [SerializeField]
    private float explosionRadius;
    [SerializeField]
    private float explosionPower;

    [SerializeField]
    private GameObject explosionParticles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator BallExplode()
    {
        yield return new WaitForSeconds(2);

        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(explosionPower, explosionPos, explosionRadius);
            }

            if (hit.gameObject.layer == 12)
            {
                hit.gameObject.transform.GetChild(0).gameObject.GetComponent<HealthController>().playerHealth -= 1;
            }
        }

        yield return null;
        Destroy(Instantiate(explosionParticles, transform.position, Quaternion.identity), 3);
        Destroy(gameObject);
        StopCoroutine(cannonBallExplode);
    }
}
