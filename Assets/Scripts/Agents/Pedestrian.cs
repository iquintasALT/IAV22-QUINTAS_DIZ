using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Pedestrian : MonoBehaviour
{
    Animator animator;
    [SerializeField]
    private float speed = 0.2f, rotationSpeed = 10f;


    List<Vector3> path = new List<Vector3>();
    bool moving = false;
    int index = 0;
    Vector3 endPosition;

    /// <summary>
    /// Establece un camino para ser usado por el peaton
    /// </summary>
    public void Initialize(List<Vector3> _path)
    {
        path = _path;
        index = 1;
        moving = true;
        endPosition = path[index];

        //animacion sencilla de caminar
        animator = GetComponent<Animator>();
        animator.SetTrigger("Walk");
    }

    private void Update()
    {
        if (moving)
            CheckMovement();
    }

    private void CheckMovement()
    {
        if (path.Count > index)
        {
            //si llegamos al siguiente punto
            if (Move() < 0.05f)
            {
                //vamos modificando la posición final hasta que terminamos de recorrer el camino
                index++;
                if (index >= path.Count)
                {
                    moving = false;
                    Destroy(gameObject);
                    return;
                }
                endPosition = path[index];
            }
        }
    }

    private float Move()
    {
        //corregimos la altura
        Vector3 newEndPosition = new Vector3(endPosition.x, transform.position.y, endPosition.z);
        transform.position = Vector3.MoveTowards(transform.position, newEndPosition, speed * Time.deltaTime);

        //rotacion suavizada del peaton
        var lookDirection = newEndPosition - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDirection), Time.deltaTime * rotationSpeed);
        return Vector3.Distance(transform.position, newEndPosition);
    }
}

