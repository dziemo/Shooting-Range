using ScriptableObjectArchitecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    public IntGameEvent onShotEvent;
    public float radius = 1.7f;

    public void OnShot (Vector2 point)
    {
        int score = (int)((radius - Vector2.Distance(transform.position, point) / radius) * 10) * 10;
        onShotEvent.Raise(score);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, radius);
    }
}
