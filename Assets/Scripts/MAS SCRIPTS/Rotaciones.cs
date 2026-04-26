using UnityEngine;
using System.Collections;

public class Rotaciones : MonoBehaviour
{
    public Transform laberinto;

    public Vector3 outerAxis = Vector3.up;
    public Vector3 innerAxis = Vector3.right;

    public float rotationStep = 90f;
    public float rotationSpeed = 2f;

    private bool isRotating = false;

    public void Interact()
    {
        if (!isRotating)
        {
            StartCoroutine(RotateBoth());
        }
    }
    IEnumerator RotateBoth()
    {
        isRotating = true;

        Quaternion startOuter = transform.rotation;
        Quaternion endOuter = transform.rotation * Quaternion.Euler(outerAxis * rotationStep);

        Quaternion startInner = laberinto.rotation;
        Quaternion endInner = laberinto.rotation * Quaternion.Euler(innerAxis * rotationStep);

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * rotationSpeed;

            transform.rotation = Quaternion.Slerp(startOuter, endOuter, t);

            if (laberinto != null)
            {
                laberinto.rotation = Quaternion.Slerp(startInner, endInner, t);
            }

            yield return null;
        }

        transform.rotation = endOuter;

        if (laberinto != null)
        {
            laberinto.rotation = endInner;
        }

        isRotating = false;
    }
}