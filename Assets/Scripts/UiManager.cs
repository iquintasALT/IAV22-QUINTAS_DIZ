using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UiManager : MonoBehaviour
{
    [SerializeField]
    Button roadButton, houseButton, pedestrianButton;

    bool roadSelected = true;
    public bool RoadSelected { get { return roadSelected; } }

private void Start()
    {
        roadButton.onClick.AddListener(() =>
        {
            roadSelected = true;
        });
        houseButton.onClick.AddListener(() =>
        {
            roadSelected = false;
        });
        pedestrianButton.onClick.AddListener(() =>
        {
            //instanciar peaton cuando funcione
        });
    }
}
