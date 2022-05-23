using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Gestion de input mediante acciones. Otros scripts podrán escuchar estas acciones para recibir información y llamar otros métodos.
/// </summary>
public class InputManager : MonoBehaviour
{
	public Action<Vector3Int> OnMouseClick, OnMouseHold;
	public Action OnMouseUp;

	private Vector2 camMovement;
	public Vector2 CamMovement { get { return camMovement; } }

	[SerializeField]
	private Camera mainCam;
	[SerializeField]
	private LayerMask layerGround;



	private void Update()
	{
		CheckAxix();
		CheckMouseInput();
	}

	/// <summary>
	/// Crea un raycast desde la cámara a un elemento de juego. Si colisiona con algún elemento en esa máscara, devuelve la posición. En caso contrario,
	/// devuelve null.
	/// </summary>
	/// <returns></returns>
	private Vector3Int? RaycastToGameObject()  // ? permite devolver null
	{
		Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerGround))
		{
			Vector3Int pos = new Vector3Int((int)hit.point.x, 0, (int)hit.point.z);
			return pos;
		}
		return null;
	}

	private void CheckAxix()
    {
		camMovement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
	}

	private void CheckMouseInput()
    {
		//Pulsar
		if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false) //comprobación extra de si se hace clic en un elemento de juego, no en un elemento de ui
		{
			Vector3Int? position = RaycastToGameObject();
			if (position.HasValue)  // igual que hacer un if de un puntero en c++
				OnMouseClick?.Invoke(position.Value);  //esto solo se llama si hay alguien "escuchando"

		}

		//Mantener
		else if (Input.GetMouseButton(0) && EventSystem.current.IsPointerOverGameObject() == false) 
		{
			Vector3Int? position = RaycastToGameObject();
			if (position.HasValue)  
				OnMouseHold?.Invoke(position.Value);  

		}

		//Soltar
		else if (Input.GetMouseButtonUp(0) && EventSystem.current.IsPointerOverGameObject() == false)
			OnMouseUp?.Invoke();
	}
	
}
