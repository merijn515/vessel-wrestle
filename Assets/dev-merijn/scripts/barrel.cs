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
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "ground" && isThrown == true)
        {
            Instantiate(oil, transform.position, Quaternion.identity);
        }
    }
}
