using UnityEngine;
using System.Collections;

public class ClampParticlesAlongZ : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[GetComponent<ParticleSystem>().particleCount];
        GetComponent<ParticleSystem>().GetParticles(particles);
        for (int i = 0; i < particles.Length; i++)
        {
            Vector3 pos = particles[i].position;
            pos.y = 0;
            particles[i].position = pos;
            particles[i].velocity.Scale( new Vector3(1, 1, 0) );
        }

        GetComponent<ParticleSystem>().SetParticles(particles, particles.Length);
	
	}
}
