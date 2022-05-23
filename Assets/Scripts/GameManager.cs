using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CameraManager camManager;

    public InputManager inputManager;

    public GridManager gridManager;

    private void Start()
    {
        //suscribirse al metodo de hacer click
        inputManager.OnMouseClick += HandleMouseClick;
    }

    private void HandleMouseClick(Vector3Int pos)
    {
        Debug.Log(pos);  //checkear que funcione
        gridManager.checkNeighboursAndPlace(pos);
        gridManager.correctNeightbours(pos);
    }

    private void Update()
    {
        camManager.MoveCamera(new Vector3(inputManager.CamMovement.x, 0, inputManager.CamMovement.y));
    }
}
