﻿using UnityEngine;
using System.Collections;

public class FinishController : MonoBehaviour {

    public GameObject ui;
    private FinishLevel finishLevel;				//Reference to script on UI GameObject
    
    void Awake()
    {
        //Get a reference to ShowPanels attached to UI object
        finishLevel = ui.GetComponent<FinishLevel>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Voce entrou no BlackHole");
            finishLevel.FinishScreen();
        }
    }
}