using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlador : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject particle;
    public int option=1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject g= Instantiate(particle, gameObject.transform.position,Quaternion.identity);
        g.GetComponent<parabolico>().op = option;
        
    }
}
