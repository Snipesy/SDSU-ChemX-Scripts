using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ParticleBehavior : MonoBehaviour
{

    ParticleSystem.Particle[] particles;

    ParticleSystem ps;

    public float localRadius;



    // Update is called once per frame
    private void LateUpdate()
    {
        if (ps == null || particles == null)
        {
            ps = GetComponent<ParticleSystem>();

            particles = new ParticleSystem.Particle[ps.main.maxParticles];

        }

        var numParticles = ps.GetParticles(particles);

        var radius = transform.localScale.x * localRadius;


        for (var i = 0; i < numParticles; i++)
        {
            var part = particles[i];


            if (particles[i].position.magnitude > radius)
            {
                // recalc norm to ensure in sphere
                var pn = part.position.normalized;

                var vn = part.velocity.normalized;

                var norm = pn;

                // reset position
                particles[i].position = pn * radius * 0.999f;

                var dot = Vector3.Dot(vn, norm);
                var vfactor = new Vector3(
                    vn.x - 2 * dot * norm.x,
                    vn.y - 2 * dot * norm.y,
                    vn.z - 2 * dot * norm.z);

                particles[i].velocity = vfactor * particles[i].velocity.magnitude;


            }


        }
        ps.SetParticles(particles);
    }
}
