using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyllinderSimBehavior : MonoBehaviour
{

    public Obi.ObiEmitter[] Emitters;
    public Obi.ObiEmitter RockEmitter;

    ~CyllinderSimBehavior()
    {
        // return scale
        Time.timeScale = 1f;
    }

    public void toggleEnergy(bool status)
    {
        Emitters[0].GetComponent<Obi.RockDropEnergyColor>().showEnergy = status;
        Emitters[1].GetComponent<Obi.RockDropEnergyColor>().showEnergy = status;

        Emitters[2].GetComponent<Obi.RockDropEnergyColor>().showEnergy = status;

    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void pourA()
    {
        Emitters[0].speed = 1f;
    }
    public void stopPourA()
    {
        Emitters[0].speed = 0f;

    }

    public void pourB()
    {
        Emitters[1].speed = 1f;

    }
    public void stopPourB()
    {
        Emitters[1].speed = 0f;

    }
    public void pourC()
    {
        Emitters[2].speed = 1f;

    }

    public void stopPourC()
    {
        Emitters[2].speed = 0f ;

    }

    public void DropRock()
    {
        RockEmitter.EmitParticle(0f, 0f);
        
    }

    public void Reset()
    {
        foreach(var a in Emitters)
        {
            a.KillAll();
        }

        RockEmitter.KillAll();
    }

    public void setTimeStep(float scale)
    {
        Time.timeScale = scale;
    }


    float getEmitterKe(Obi.ObiEmitter emitter)
    {
        float sum = 0f;
        for (int i = 0; i < emitter.solverIndices.Length; i++)
        {
            var k = emitter.solverIndices[i];
            var velocity = emitter.solver.velocities[k].magnitude;
            var mass = emitter.solver.restDensities[k];

            float ke = (1 / 2.0f) * mass * (velocity * velocity);
            sum += ke;
        }

        return sum;
    }

    float getSystemKe()
    {
        return getEmitterKe(Emitters[0]) + getEmitterKe(Emitters[1]) + getEmitterKe(Emitters[2])
            + getEmitterKe(RockEmitter);
    }

    void OnGUI()
    {
        GUI.skin.label.alignment = TextAnchor.UpperRight;
        GUILayout.Label("System KE: " + getSystemKe());
        
    }
}
