using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public bool Deployed;
    public int Movement;
    [SerializeField]
    public int Life;

    public void Configure (bool deployed, int movement, int life)
    {
        Deployed = deployed;
        Movement = movement;
        Life = life;
    }

    public void SetSelected (bool selected)
    {
        GameObject hitTransform = this.gameObject.transform.GetChild(1).gameObject;
        GameObject hitTransform2 = hitTransform.transform.GetChild(0).gameObject;
        hitTransform2.SetActive(selected) ;
    }

    public void takeDamage() {
        this.Life -= 1;
    }

    public bool isDead()
    {
        return this.Life <= 0;
    }

    public void Kill() { 
        this.gameObject.SetActive(false);
    }
}
