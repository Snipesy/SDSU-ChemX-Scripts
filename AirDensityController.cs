using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirDensityController : MonoBehaviour
{

    [Tooltip("Density in kg/m^3")]
    public float density;

    private float initialDensity;

    [Tooltip("Volume in m^3")]
    public float volume;

    private float initialVolume;

    [Tooltip("Density in kg/m^3")]
    public float ambient_density;

    [Tooltip("Gravity in m/s/s")]
    public float gravity;

    public bool isSimulated = true;

    public float temp = 303f;

    private Vector3 startPos;

    public Rigidbody rb;

    public void setInit()
    {
        rb = GetComponent<Rigidbody>();

        initialDensity = density;
        initialVolume = volume;

        rb.mass = (volume * density) + 0.3f;

    }

    // archimedes's equation
    // F = g * V * dp

    // Start is called before the first frame update
    void Start()
    {
        startPos = this.transform.position;

        rb = GetComponent<Rigidbody>();

        setTemp(303f);
        
    }

    public void resetPos()
    {
        this.transform.position = startPos;
        this.rb.velocity *= 0f;
        this.rb.angularVelocity *= 0f;
    }

    public void setTemp(float newTemp)
    {
        var newVolume = (initialVolume * newTemp) / 303f;
        if (newVolume == 0f)
        {
            newVolume = 0.0001f;
        }


        density = (initialVolume / newVolume) * initialDensity;


        var initialEnergy = (3f / 2f) * 303f;

        var newEnergy = (3f / 2f) * newTemp;

        float factor = newEnergy / initialEnergy;



        var ps = GetComponent<ParticleSystem>().velocityOverLifetime;

        ps.speedModifier = factor;

        float volumeScale = (newTemp / 303f) * 0.5f + 0.5f;


        this.transform.localScale = new Vector3(volumeScale, volumeScale, volumeScale);

    }

    // Update is called once per frame
    void Update()
    {
        if(!isSimulated)
        {
            return;
        }


        var effectiveDensity = density + 0.3f;
       

        var densityDifference = effectiveDensity - ambient_density;

        // allow max differnece of 0.01
        var force = gravity * volume * densityDifference * -1;


        rb.AddForce(transform.up * force);

    }
}
