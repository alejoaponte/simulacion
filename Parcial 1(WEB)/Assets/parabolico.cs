using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parabolico : MonoBehaviour
{
    //Parametros plano xz
    public float theta;
    //parametros plano uy
    float V = 10.0f; // Velocidad inicial
    public float angletiro ; // Direccion de tiro parabolicos
    Vector3 Vel = Vector3.zero;
    float a = -7.0f;// Gravedad
    //Variables globales
    float degtorad;
    //Metodo numericos
    public int op=1; //1 euler, 2 euler mejorado, 3 range kutta
    int forma = 1;



    void Start()
    {
        start1();

        

    }

    void start1()
    {
        //Forma 1
        degtorad = Mathf.PI / 180;
        theta = Random.Range(0, 360);
        angletiro = Random.Range(30, 80);
        Vel.y = V * Mathf.Sin(angletiro * degtorad);
        Vel.x = V * Mathf.Cos(angletiro * degtorad) * Mathf.Cos(theta * degtorad);
        Vel.z = V * Mathf.Cos(angletiro * degtorad) * Mathf.Sin(theta * degtorad);
    }

    
    // Update is called once per frame
    void Update()
    {
        forma1();
    }

    void forma1()
    {
        Vector3 pos = gameObject.transform.position;
        float dt = Time.deltaTime;
        if (op == 1)
        {
            //eje x y z
            pos.x = pos.x + Vel.x * dt;
            pos.z = pos.z + Vel.z * dt;
            //eje y
            pos.y = pos.y + Vel.y * dt;
            //Velocidad 
            Vel.y = Vel.y + a * dt;
        }
        else if (op == 2)
        {
            //x* y z*
            //float xa = pos.x + Vel.x * dt;
            //float za = pos.x + Vel.x * dt;
            //X y Z
            pos.x = pos.x + (dt / 2) * (Vel.x + Vel.x);
            pos.z = pos.z + (dt / 2) * (Vel.z + Vel.z);

            //posicion y
            float va = Vel.y + a * dt; // velocidad sig
            //float ya = pos.y + Vel.y * dt;
            pos.y = pos.y + (dt / 2) * (Vel.y + va);
            //Velocidad y

            Vel.y = Vel.y + a * dt;
        }
        else if (op == 3)
        {
            //X y Z
            pos.x = pos.x + (dt / 2) * (Vel.x + Vel.x);
            pos.z = pos.z + (dt / 2) * (Vel.z + Vel.z);

            //posicion y
            float k1 = dt * (Vel.y);
            float k2 = dt * (Vel.y + a * dt / 2);
            float k3 = dt * (Vel.y + a * dt / 2);
            float k4 = dt * (Vel.y + a * dt);


            pos.y = pos.y + (k1 + 2 * k2 + 2 * k3 + k4) / 6;
            //Velocidad y
            Vel.y = Vel.y + a * dt;
        }


        gameObject.transform.position = pos;
    }
    
    


}
