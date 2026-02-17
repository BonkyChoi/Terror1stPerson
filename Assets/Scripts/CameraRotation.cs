using System;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    //debe seguir al cursor
    [SerializeField]private float mouseSensitivite = 100f;
    [SerializeField] private Transform playerBody;

    private float xRotation = 0f; //guarda la rotacion acumulada

    public Vector3 lookPoint { get; set; }
    public bool lookPointIsNull { get; set; }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        //float yTo = Mathf.Lerp(transform.position.y, playerBody.position.y, 5*Time.deltaTime);

        //this.transform.position = new Vector3(playerBody.position.x, yTo, playerBody.position.z);
            
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivite * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivite * Time.deltaTime;

        xRotation -=
            mouseY; //restas lo que se movio el raton en y a la rotacion de arriba y abajo. Debe permitir que al mirar arriba la camara se ponga mas negativa y viceversa
        xRotation = Math.Clamp(xRotation, -90f, 90f); //evita que camara gire mas de 90º
        
        
        playerBody.Rotate(Vector3.up * mouseX); //mueve en "YRotation" el movimiento horizontal
        transform.Rotate(Vector3.up * mouseX); //mueve en "YRotation" el movimiento horizontal

        transform.localRotation = Quaternion.Euler(xRotation, this.transform.localEulerAngles.y, 0); //rotas sobre eje y
    }

    private void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(this.transform.position, transform.forward, out hit))
        {
            lookPoint = hit.point;
            Debug.DrawLine(this.transform.position, hit.point);
            lookPointIsNull = false;
        }
        else
        {
            lookPointIsNull = true;
        }
    }
}
