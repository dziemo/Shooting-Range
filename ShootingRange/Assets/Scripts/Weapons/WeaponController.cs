using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public WeaponData weaponData;

    int currAmmo;
    float timeBetweenShots = 0f;

    Collider coll;
    Camera cam;

    private void Start()
    {
        coll = GetComponent<Collider>();
        currAmmo = weaponData.maxAmmo;
        cam = Camera.main;
    }

    public void OnPickup ()
    {
        coll.enabled = false;
    }

    public void OnThrow ()
    {
        coll.enabled = true;
    }

    public void OnReload()
    {
        currAmmo = weaponData.maxAmmo;
    }

    public void OnPerformFire()
    {
        if (timeBetweenShots <= 0 && currAmmo > 0)
        {
            Ray camRay = new Ray(cam.transform.position, cam.transform.forward);
            RaycastHit rayHit;

            if (Physics.Raycast(camRay, out rayHit))
            {
                //If target - calculate points

                //Leave decal

                Debug.Log("Pew pew");
            }

            currAmmo--;
            timeBetweenShots = weaponData.fireRate;   
        }
    }

    private void Update()
    {
        if (timeBetweenShots > 0)
        {
            timeBetweenShots -= Time.deltaTime;
        }
    }
}
