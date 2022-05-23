using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CameraManager camManager;

    public InputManager inputManager;

    private void Start()
    {
        //suscribirse al metodo de hacer click
        inputManager.OnMouseClick += HandleMouseClick;
    }

    private void HandleMouseClick(Vector3Int position)
    {
        Debug.Log(position);  //checkear que funcione
    }

    private void Update()
    {
        camManager.MoveCamera(new Vector3(inputManager.CamMovement.x, 0, inputManager.CamMovement.y));
    }
}
