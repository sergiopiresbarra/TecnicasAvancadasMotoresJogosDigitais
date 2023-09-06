using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    //pegando as posições do corpo do Player
    public Transform characterHead;
    public Transform characterBody;

    float rotationX = 0;
    float rotationY = 0;

    //Limitador do angulo da Camera
    float angleYmin = -90;
    float angleYmax = 90;

    //Sensibilidade do Mouse
    public float sensitivityX = 4f;
    public float sensitivityY = 4f;

    public Vector3 GetForwardDirection() => transform.forward;
    // Start is called before the first frame update
    void Start()
    {
        //Ocultar e travar o ponteiro do mouse
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float verticalDelta = Input.GetAxisRaw("Mouse Y") * sensitivityY;
        float horizontalDelta = Input.GetAxisRaw("Mouse X") * sensitivityX;

        rotationX += horizontalDelta;
        rotationY += verticalDelta;

        rotationY = Mathf.Clamp(rotationY, angleYmin, angleYmax);

        //Passa a rotação do mouse para o Player
        characterBody.localEulerAngles = new Vector3(0, rotationX, 0);

        //Passa a rotação do mouse para a Camera
        transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
    }

    private void LateUpdate()
    {
        //Posição da Camera fixada na cabeça do Player
        transform.position = characterHead.position;
    }
}
