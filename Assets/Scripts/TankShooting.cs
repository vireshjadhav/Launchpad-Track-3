using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankShooting : MonoBehaviour
{
    [SerializeField] private ShellController shellPrefab;
    [SerializeField] private Transform fireTransform;
    [SerializeField] private Slider aimSlider;
    [SerializeField] private AudioSource shootingAudio;
    [SerializeField] private AudioClip chargingClip;
    [SerializeField] private AudioClip fireClip;

    private float minShellVelocity = 15.0f;
    private float maxShellVelocity = 30.0f;
    private float maxChargeTime = 0.75f;
    private float chargingVelocity;
    private float currentLaunchVelocity;

    private bool fired = false;

    private void OnEnable()
    {
        currentLaunchVelocity = minShellVelocity;
        aimSlider.value = minShellVelocity;
    }

    // Start is called before the first frame update
    void Start()
    {
        chargingVelocity = (maxShellVelocity - minShellVelocity) / maxChargeTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentLaunchVelocity >= maxShellVelocity && !fired)
        {
            currentLaunchVelocity = maxShellVelocity;
            Fire();
        }
        else if (Input.GetMouseButtonDown(0))
        {
            fired = false;
            currentLaunchVelocity = minShellVelocity;

            shootingAudio.clip = chargingClip;
            shootingAudio.Play();
        }
        else if (Input.GetMouseButton(0) && !fired)
        {
            currentLaunchVelocity += chargingVelocity * Time.deltaTime;
            aimSlider.value = currentLaunchVelocity;
        }
        else if(Input.GetMouseButtonUp(0) && !fired)
        {
            Fire(); 
        }

    }

    private void Fire()
    {
        fired = true;

        ShellController shellInstance = Instantiate(shellPrefab);
        shellInstance.SetShellProperties(fireTransform, currentLaunchVelocity);

        shootingAudio.clip = fireClip;
        shootingAudio.Play();

        currentLaunchVelocity = minShellVelocity;
        aimSlider.value = minShellVelocity;
    }
}
