using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHolesPool : MonoBehaviour
{
    public int maxBulletHoles = 30;
    public GameObject bulletHole;

    Queue<GameObject> bulletHoles;

    private void Start()
    {
        bulletHoles = new Queue<GameObject>();
    }

    public void OnBulletHoleSpawned (RaycastHit hit)
    {
        if (bulletHoles.Count < maxBulletHoles)
        {
            var newHole = Instantiate(bulletHole, hit.point + hit.normal * 0.001f, Quaternion.LookRotation(-hit.normal));
            bulletHoles.Enqueue(newHole);
        }
        else
        {
            var replaceHole = bulletHoles.Dequeue();
            Debug.Log("PRE POS: " + replaceHole.transform.position);
            replaceHole.transform.position = hit.point + hit.normal * 0.001f;
            replaceHole.transform.rotation = Quaternion.LookRotation(-hit.normal);
            bulletHoles.Enqueue(replaceHole);
            Debug.Log("POST POS: " + replaceHole.transform.position);
        }
    }
}
