using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Universe
{
    public static float gravitationalConstant = 0.0000000000667430f; // Newtons gravitational constant
    public static float physicsTimeStep = 0.01f;
    public static float scaleFactor = 12756000f; // scaling the system to make earth approximately 1m in diameter
    public static float massScaleFactor = 1000000000000000000000000f;
}
