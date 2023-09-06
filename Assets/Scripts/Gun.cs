using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public FirstPersonCamera fpsCam;
    public Transform bulletPrefab;
    Transform shotSpawn;

    float mass = 0f;

    int weapon = 0;
    public float impulseGun = 0f; //força de impulso sobre o projetil
    public float backSpinDrag = 0f; //controlhe de hop-up da BB

    Transform bullet;
    Vector3 positionBullet;

    public GameObject pistol, ak, shotgun;

    bool isFiring = false;
    float timeLastShoot = 0f;

    int qtdBBs = 0;

    public Text textQtdBBs;

    private void Awake() {

        //fpsCam = GetComponent<FirstPersonCamera>();
        shotSpawn  = transform.Find("shotSpawn");

    }
    // Start is called before the first frame update
    void Start()
    {
        SetWeaponType(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && weapon != 0 && isFiring == false && qtdBBs > 0)
        {
            //Função de tiro, chamada toda vez que o botão do mouse for pressionado
            ShootBullet();
        }
        timeLastShoot -= Time.deltaTime;
        if(timeLastShoot <= 0){isFiring = false;}
    }

    void WeaponType(int o){
        switch (o)
        {
            case 0:
                pistol.SetActive(false);
                ak.SetActive(false);
                shotgun.SetActive(false);
            break;
            case 1:
                pistol.SetActive(true);
                ak.SetActive(false);
                shotgun.SetActive(false);
            break;
            case 2:
                pistol.SetActive(false);
                ak.SetActive(true);
                shotgun.SetActive(false);
            break;
            case 3:
                pistol.SetActive(false);
                ak.SetActive(false);
                shotgun.SetActive(true);
            break;
        }
    }

    void ShootBullet(){
        isFiring = true;
        switch (weapon)
        {
            case 1:
                timeLastShoot = 0.7f;
            break;
            case 2:
                timeLastShoot = 0.1f;
            break;
            case 3:
                timeLastShoot = 1.5f;
            break;
        }
        //Instanciação do projetil, atravez da posição do ponto "shotSpawn" na arma
        Transform bulletObj = Instantiate(bulletPrefab, shotSpawn.position, shotSpawn.rotation);
        bullet = bulletObj;
        positionBullet = bullet.position;
        bulletObj.GetComponent<Rigidbody>().mass = mass;
        bulletObj.GetComponent<Bullet>().SetSpeed(impulseGun);
        bulletObj.GetComponent<Bullet>().SetBackSpinDrag(backSpinDrag);
        StartCoroutine(DisableTrail(bulletObj));

        //Destroi o projetil depois de x segundos
        Destroy(bulletObj.gameObject, 10f);

        //Calcula e define a direção do tiro caso haja colisão entre objetos com Raycast
        RaycastHit hitInfo;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.GetForwardDirection(), out hitInfo, Mathf.Infinity, LayerMask.GetMask("hittable"))){
            bulletObj.GetComponent<Bullet>().SetDirection((hitInfo.point - shotSpawn.position).normalized);
        }
        else{
            //Direção de tiro, a mesma que a camera aponta
            bulletObj.GetComponent<Bullet>().SetDirection(fpsCam.GetForwardDirection());
        }
        //Debug.Log(fpsCam.GetForwardDirection());
        --qtdBBs;
        textQtdBBs.text = qtdBBs.ToString();
    }

    public void SetMass(float m){
        mass = m;
    }

    public void SetWeaponType(int w){
        weapon = w;
        WeaponType(w);
    }

    public void SetQtdBBs(int q){
        qtdBBs = q;
        textQtdBBs.text = qtdBBs.ToString();
    }

    IEnumerator DisableTrail(Transform bu){
        yield return new WaitForSeconds(5f);
        bu.GetComponent<Bullet>().DisableEnableTrail(false);
    }
}
