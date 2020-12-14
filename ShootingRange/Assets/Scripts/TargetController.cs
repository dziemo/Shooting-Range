using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    public Animator anim;

    bool isStanding = true;

    public void OnStandUp()
    {
        if (!isStanding)
        {
            anim.SetTrigger("ChangeStance");
            isStanding = true;
        }
    }

    public void OnCollapse()
    {
        if (isStanding)
        {
            anim.SetTrigger("ChangeStance");
            isStanding = false;
        }
    }

    public void OnShot (Vector2 point)
    {
        if (isStanding)
        {
            OnCollapse();
            Debug.Log("Distance to center: " + Vector2.Distance(transform.position, point));
        }
    }
}
