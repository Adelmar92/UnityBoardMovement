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

    public void SetSelected (bool selected)
    {
        GameObject hitTransform = this.gameObject.transform.GetChild(1).gameObject;
        GameObject hitTransform2 = hitTransform.transform.GetChild(0).gameObject;
        hitTransform2.SetActive(selected) ;
    }
}
