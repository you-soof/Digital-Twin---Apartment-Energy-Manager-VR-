using System;
using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;
using UnityEngine.Serialization;

public class FanRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 720.0f; 
    [SerializeField] private ApplianceEnergyTracker _fanEnergyTracker;

    private Vector3 rotationAxis = Vector3.up;
    
    void Update()
    {
        if (_fanEnergyTracker.isOn)
        {
            transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
        }
    }
}
