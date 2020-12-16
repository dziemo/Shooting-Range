using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class WeaponController : MonoBehaviour
{
    public WeaponData weaponData;
    public int currAmmo;
    public ParticleSystem muzzleFlash;

    float timeBetweenShots = 0f;
    bool isReloading = false;
    Collider coll;
    Camera cam;

    private void Start()
    {
        coll = GetComponent<Collider>();
        currAmmo = weaponData.maxAmmo;
        cam = Camera.main;
        muzzleFlash.gameObject.GetComponent<Renderer>().material = weaponData.muzzleFlashMat;
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
        if (!isReloading)
        {
            isReloading = true;
            transform.DOPunchPosition(new Vector3(0, -0.25f, 0), weaponData.reloadSpeed, 0, 0);
            transform.DOPunchRotation(new Vector3(-45, 0, 0), weaponData.reloadSpeed, 0, 0).OnComplete(() => {
                currAmmo = weaponData.maxAmmo;
                isReloading = false;
                updateAmmoText();
            });
        }
    }

    public void OnPerformFire()
    {
        if (timeBetweenShots <= 0 && currAmmo > 0 && !isReloading)
        {
            muzzleFlash.Play();
            transform.DOPunchPosition(new Vector3(0, 0, weaponData.kickback), weaponData.fireRate * 0.9f);
            transform.DOPunchRotation(new Vector3(weaponData.shotRotation, 0, 0), weaponData.fireRate * 0.9f);
            
            //Play sound

            Ray camRay = new Ray(cam.transform.position, cam.transform.forward);
            RaycastHit rayHit;

            if (Physics.Raycast(camRay, out rayHit))
            {
                if (rayHit.collider.CompareTag("Target"))
                {
                    rayHit.collider.GetComponent<TargetController>().OnShot(rayHit.point);
                }
                else if (rayHit.collider.CompareTag("StartTarget"))
                {
                    rayHit.collider.GetComponent<StartTargetController>().OnShot();
                    currAmmo++;
                }
                else
                {
                    var bulletHole = Instantiate(weaponData.bulletHole, rayHit.point + rayHit.normal * 0.001f, Quaternion.LookRotation(-rayHit.normal));
                }
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
