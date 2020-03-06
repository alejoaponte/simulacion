using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changemethod : MonoBehaviour
{
    public GameObject esfera;
    public int option = 1;
    public void cambiometodo()
    {
        GameObject g = Instantiate(esfera,new Vector3(0,0.51f,0), Quaternion.identity);
        g.GetComponent<disparo>().op = option;
        Debug.Log("funciona" + option);
    }
}
