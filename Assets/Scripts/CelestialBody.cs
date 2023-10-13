using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.Universal;
using UnityEngine;

public class CelestialBody : MonoBehaviour
{
    [Header("Planetary Properties")]
    [Tooltip("in Km^6")]
    public float orbitDistance;
    [Tooltip("kgm/m^3")]
    public float density;
    public float mass { get; private set; }
    [Tooltip("in Km")]
    public float radius;

    [Header("Orbital Properties")] 
    public Vector3 initialVelocity;
    public Vector3 currentVelocity { get; private set; }

    private Rigidbody rb;

    void Awake()
    {
        // Scale each aspect
        radius = (radius * 1000) / Universe.scaleFactor; // the 1000 is to turn from km to m
        float volume = (4 / 3) * Mathf.PI * (radius * radius * radius);
        mass = volume * density;
        orbitDistance = orbitDistance / Universe.scaleFactor;

        // Calculate the missing values for the body
        transform.localScale = Vector3.one * (radius * 2);
        transform.localPosition = new Vector3(orbitDistance, 0, 0);
        currentVelocity = initialVelocity;

        rb = gameObject.GetComponent<Rigidbody>();
        rb.mass = mass;
    }

    public void UpdateVelocity(Vector3 acceleration, float timeStep)
    {
        currentVelocity += acceleration * timeStep;
    }

    public void UpdatePosition(float timeStep)
    {
        rb.MovePosition(rb.position + currentVelocity * timeStep);
    }

    public Vector3 Position
    {
        get { return rb.position; }
    }
}
