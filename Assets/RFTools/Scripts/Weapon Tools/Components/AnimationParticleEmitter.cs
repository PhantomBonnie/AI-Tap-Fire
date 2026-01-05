using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationParticleEmitter : MonoBehaviour
{
    public ParticleSystem particleSystem;

    public void EmitParticles(int count)
    {
        particleSystem.Emit(count);
    }
}
