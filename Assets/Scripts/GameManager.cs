using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CameraManager camManager;

    public InputManager inputManager;

    public GridManager gridManager;

    public UiManager uiManager;

    private void Start()
    {
        //suscribirse al metodo de hacer click
        inputManager.OnMouseClick += HandleMouseClick;
        //inputManager.OnMouseHold += HandleMouseClick;
        //inputManager.OnMouseUp += endPlacing;
    }

    private void HandleMouseClick(Vector3Int pos)
    {
        Debug.Log(pos);  //checkear que funcione
        if (uiManager.RoadSelected)
        {
            gridManager.checkNeighboursAndPlaceRoad(pos);
            gridManager.correctNeightbours(pos);
        }
        else
        {
            gridManager.placeHouse(pos);
        }
        
    }

    private void Update()
    {
        camManager.MoveCamera(new Vector3(inputManager.CamMovement.x, 0, inputManager.CamMovement.y));
    }
}
