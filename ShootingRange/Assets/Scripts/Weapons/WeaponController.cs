using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class WeaponController : MonoBehaviour
{
    public WeaponData weaponData;

    public int currAmmo;

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

    public void OnReload(Action updateAmmoText)
    {
        transform.DOPunchPosition(new Vector3(0, -1.5f, 0), weaponData.reloadSpeed, 0, 0);
        transform.DOPunchRotation(new Vector3(30, 0, 0), weaponData.reloadSpeed, 0, 0).OnComplete(() => { currAmmo = weaponData.maxAmmo; updateAmmoText(); });
    }

    public void OnPerformFire()
    {
        if (timeBetweenShots <= 0 && currAmmo > 0)
        {
            transform.DOPunchPosition(new Vector3(0, 0, 0.05f), weaponData.fireRate * 0.9f);
            transform.DOPunchRotation(new Vector3(15, 0, 0), weaponData.fireRate * 0.9f);

            Ray camRay = new Ray(cam.transform.position, cam.transform.forward);
            RaycastHit rayHit;

            if (Physics.Raycast(camRay, out rayHit))
            {
                if (rayHit.collider.CompareTag("Target"))
                {
                    rayHit.collider.GetComponent<TargetController>().OnShot(rayHit.point);
                }
                if (rayHit.collider.CompareTag("StartTarget"))
                {
                    rayHit.collider.GetComponent<StartTargetController>().OnShot();
                    currAmmo++;
                }

                //Leave decal
                //Play anim
                //Play sound
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
