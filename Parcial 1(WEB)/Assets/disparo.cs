using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disparo : MonoBehaviour
{
    public float apiso=0;//Angulo de desviacion
    public float adisparo=70;//Angulo de elevacion
    public float V=10; //Velocidad inicial
    Vector3 Vel = Vector3.zero;
    private float g = -9.8f;

    public int op = 1;//metodo numerico a usar
    bool exploted = false;
    [SerializeField] GameObject explosion;

    Vector3 Fresistencia;
    public Vector3 Faire = new Vector3(0, 0, -1);//Direccion del aire
    public float Vaire = 100;
    Vector3 a;
    // Start is called before the first frame update
    void Start()
    {
        float degtorad = Mathf.PI / 180;
        Vel.y = V * Mathf.Sin(adisparo * degtorad);
        Vel.x = V * Mathf.Cos(adisparo * degtorad) * Mathf.Cos(apiso * degtorad);
        Vel.z = V * Mathf.Cos(adisparo * degtorad) * Mathf.Sin(apiso * degtorad);
        a = new Vector3(0, 9.8f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 pos = gameObject.transform.position;
        if(pos.y>0.5)
        {
            float dt = Time.deltaTime;
            if (op == 1)
            {
                //POSICIONES
                //eje x y z
                pos = pos + Vel * dt;
                //VELOCIDADES
                Vel = Vel + a * dt;

                //FUERZA Y ACELERACION
                Fresistencia = Vel.normalized;
                Fresistencia *= -0.5f * 1.23f * Vel.magnitude * Vel.magnitude * (3.1416f * 0.5f * 0.5f) * 0.1f;

                Faire = Faire.normalized;
                Faire *= 0.5f * 1.23f * Vaire * (3.1416f * 0.5f * 0.5f) * 0.1f;
                a.x = Fresistencia.x+Faire.x;
                a.z = Fresistencia.z+Faire.z;
                a.y = Fresistencia.y + g+Faire.y;
            }
            else if (op == 2)
            {
                //POSICIONES
                pos.x = pos.x + (dt / 2) * (Vel.x + Vel.x+dt);
                pos.z = pos.z + (dt / 2) * (Vel.z + Vel.z+dt);
                pos.y = pos.y + (dt / 2) * (Vel.y + Vel.y+dt);
                //VELOCIDADES
                Vel.x = Vel.x + (dt / 2) * (a.x + a.x + dt);
                Vel.y = Vel.y + (dt / 2) * (a.y + a.y + dt);
                Vel.z = Vel.z + (dt / 2) * (a.z + a.z + dt);
                //FUERZA Y ACELERACION
                Fresistencia = Vel.normalized;
                Fresistencia *= -0.5f * 1.23f * Vel.magnitude * Vel.magnitude * (3.1416f * 0.5f * 0.5f) * 0.1f;

                Faire = Faire.normalized;
                Faire *= 0.5f * 1.23f * Vaire * (3.1416f * 0.5f * 0.5f) * 0.1f;
                a.x = Fresistencia.x + Faire.x;
                a.z = Fresistencia.z + Faire.z;
                a.y = Fresistencia.y + g + Faire.y;
            }
            else if (op == 3)
            {
                //POSICIONES
                float kx1 = dt * (Vel.x);
                float kx2 = dt * (Vel.x + dt / 2);
                float kx3 = dt * (Vel.x + dt / 2);
                float kx4 = dt * (Vel.x + dt);

                float kz1 = dt * (Vel.z);
                float kz2 = dt * (Vel.z + dt / 2);
                float kz3 = dt * (Vel.z + dt / 2);
                float kz4 = dt * (Vel.z + dt);

                float ky1 = dt * (Vel.y);
                float ky2 = dt * (Vel.y + dt / 2);
                float ky3 = dt * (Vel.y + dt / 2);
                float ky4 = dt * (Vel.y + dt);

                pos.x = pos.x + (kx1 + 2 * kx2 + 2 * kx3 + kx4) / 6;
                pos.y = pos.y + (ky1 + 2 * ky2 + 2 * ky3 + ky4) / 6;
                pos.z = pos.z + (kz1 + 2 * kz2 + 2 * kz3 + kz4) / 6;
                //VELOCIDAD
                float kvx1 = dt * (a.x);
                float kvx2 = dt * (a.x + dt / 2);
                float kvx3 = dt * (a.x + dt / 2);
                float kvx4 = dt * (a.x + dt);

                float kvz1 = dt * (a.z);
                float kvz2 = dt * (a.z + dt / 2);
                float kvz3 = dt * (a.z + dt / 2);
                float kvz4 = dt * (a.z + dt);

                float kvy1 = dt * (a.y);
                float kvy2 = dt * (a.y + dt / 2);
                float kvy3 = dt * (a.y + dt / 2);
                float kvy4 = dt * (a.y + dt);

                Vel.x = Vel.x + (kvx1 + 2 * kvx2 + 2 * kvx3 + kvx4) / 6;
                Vel.y = Vel.y + (kvy1 + 2 * kvy2 + 2 * kvy3 + kvy4) / 6;
                Vel.z = Vel.z + (kvz1 + 2 * kvz2 + 2 * kvz3 + kvz4) / 6;
                //Fuerzas Y aceleracion

                Fresistencia = Vel.normalized;
                Fresistencia *= -0.5f * 1.23f * Vel.magnitude * Vel.magnitude * (3.1416f * 0.5f * 0.5f) * 0.1f;

                Faire = Faire.normalized;
                Faire *= 0.5f * 1.23f * Vaire * (3.1416f * 0.5f * 0.5f) * 0.1f;
                a.x = Fresistencia.x + Faire.x;
                a.z = Fresistencia.z + Faire.z;
                a.y = Fresistencia.y + g + Faire.y;
            }


            gameObject.transform.position = pos;
        }
        else
        {
            if(exploted==false)
            {
                GameObject g= Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
                exploted = true;
                Debug.Log("entre");
            }
        }
        
    }
}
