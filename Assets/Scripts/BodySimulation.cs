using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BodySimulation : MonoBehaviour
{
    public CelestialBody[] bodies;

    public CelestialBody sun;

    static BodySimulation instance;

    private void Awake()
    {
        Time.fixedDeltaTime = Universe.physicsTimeStep;
    }

    private void FixedUpdate()
    {
        // CALCULATE ACCELERATION FOR PLANETS AROUND THE SUN
        for (int i = 0; i < bodies.Length; i++)
        {
            if (bodies[i] == sun) continue;

            Vector3 acceleration = SolarAcceleration(bodies[i].Position, bodies[i]);
            bodies[i].UpdateVelocity(acceleration, Universe.physicsTimeStep);
        }

        for (int i = 0; i < bodies.Length; i++)
        {
            bodies[i].UpdatePosition(Universe.physicsTimeStep);
        }
    }


    //------------------------------ ACCELERATIONS ---------------------------------\\

    // SOLAR SYSTEM
    public Vector3 SolarAcceleration(Vector3 point, CelestialBody ignoreBody = null)
    {
        Vector3 acceleration = Vector3.zero;
        for (int i = 0; i < bodies.Length; i++)
        {
            if (bodies[i] == ignoreBody) continue;

            if (bodies[i] == sun)
            {
                float sqrDist = (bodies[i].Position - point).sqrMagnitude;
                Vector3 forceDir = (bodies[i].Position - point).normalized;
                acceleration += forceDir * Universe.gravitationalConstant * bodies[i].mass / sqrDist; 
            }
        }
        return acceleration;
    }

    //private bool Close(CelestialBody body, CelestialBody self)
    //{
    //    float dist = (body.Position - self.Position).magnitude;
    //    if (dist < 1000)
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}

    static BodySimulation Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BodySimulation>();
            }
            return instance;
        }
    }
}
