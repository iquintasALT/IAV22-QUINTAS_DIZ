using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Camera cam;
    [SerializeField] 
    private float speed = 5;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }
    public void MoveCamera(Vector3 inputVector)
    {
        Vector3 movement = Quaternion.Euler(0, 30, 0) * inputVector; //rotacion inicial
        cam.transform.position += movement * Time.deltaTime * speed;
    }
}