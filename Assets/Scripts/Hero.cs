using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public bool Deployed;
    public int Movement;

    public void Configure (bool deployed, int movement)
    {
        Deployed = deployed;
        Movement = movement;
    }
}
