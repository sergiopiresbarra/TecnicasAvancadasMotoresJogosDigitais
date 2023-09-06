using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    Vector3 velocity;

    float speed;

    //BoxCollider collider;
    SphereCollider sphereCollider;

    Rigidbody m_Rigidbody;
    float backSpinDrag;
    bool pin = false;

    TrailRenderer trailRenderer;

    // Start is called before the first frame update
    void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        //collider = GetComponent<BoxCollider>();
        sphereCollider = GetComponent<SphereCollider>();

        m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.AddForce(velocity, ForceMode.Impulse);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(pin == false){
            Debug.Log("velocidade inicial:"+m_Rigidbody.velocity.magnitude.ToString());
            //StartCoroutine(WaitForFunction());
            pin = true;
        }

        //transform.position += Mathf.Sqrt(m_Rigidbody.velocity.magnitude) * backSpinDrag * Vector3.up * Time.deltaTime;
        

        
        //Usando OverLapBox para calcular a Colisão entre projetil e algum objeto
        Collider[] colliders = Physics.OverlapBox(transform.position, sphereCollider.center / 2, transform.rotation, LayerMask.GetMask("hittable"));

        if(colliders.Length > 0){
            //Chama a Interface IShotHit para tratar a reação do objeto atingido
            IShotHit hitted = colliders[0].GetComponent<IShotHit>();
            if (hitted != null)
            {
                hitted.Hit(velocity.normalized);
            }
        }
    }

    void FixedUpdate()
    {

        //Debug.DrawRay(transform.position, m_Rigidbody.velocity.normalized, Color.green, 211, false);
        
        Vector3 magnusDirection = Vector3.Cross(m_Rigidbody.velocity, transform.right).normalized;

        //Debug.DrawRay(transform.position, transform.right, Color.blue, Mathf.Infinity);
        
        Vector3 magnusForce = Mathf.Sqrt(m_Rigidbody.velocity.magnitude) * magnusDirection * backSpinDrag * Time.fixedDeltaTime;
        
        //Debug.DrawRay(transform.position, magnusForce * 10000, Color.red, Mathf.Infinity);
        m_Rigidbody.AddForce(magnusForce);
    }

    //Direção da velocidade do projétil
    public void SetDirection(Vector3 direction){
        velocity = direction * speed;
    }

    public void SetSpeed(float s){
        speed = s;
    }

    public void SetBackSpinDrag(float b){
        backSpinDrag = b;
    }

    IEnumerator WaitForFunction()
    {
        yield return new WaitForSeconds(0.01f); //0.01f
        Debug.Log("velocidade inicial:"+m_Rigidbody.velocity.magnitude.ToString());
         
    }

    public float GetRigidbodyVelocityMagnitude(){
        try{
            return m_Rigidbody.velocity.magnitude;
        }
        catch{
            return 0f;
        }
    }

    public void DisableEnableTrail(bool b){
        trailRenderer.enabled = b;
    }

}
