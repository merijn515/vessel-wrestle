using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrel : MonoBehaviour
{
    [SerializeField]
    private GameObject oil;
    [SerializeField]
    public bool isThrown;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
            Debug.DrawRay(gameObject.transform.position, Vector3.down * 2, Color.magenta);
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && isThrown == true)
        {


            RaycastHit hit;

            if (Physics.Raycast(gameObject.transform.position, Vector3.down, out hit, 3))
            {
                Debug.Log(hit.collider.name);
                Debug.Log(hit.point);
            Destroy(Instantiate(oil, hit.point, Quaternion.identity), 15f);
            }
            //Destroy(Instantiate(oil, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity), 15f);
        }
    }
}
