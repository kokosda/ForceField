using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForceField.Domain.Renderer;
using Microsoft.Xna.Framework;
using ForceField.Domain.Renderer.Base;

namespace ForceField.Interfaces
{
    public interface IParticleEmitterService<T> where T : Emitter
    {
        void LogicInitialize(Emitter emitter, ref Particle particle);
    }
}
