using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForceField.Interfaces;
using ForceField.Domain.Renderer;
using Microsoft.Xna.Framework;
using ForceField.Domain.Renderer.Base;

namespace ForceField.Core.Services
{
    public class MenuEmitterService : EmitterService, IParticleEmitterService<MenuEmitter>
    {
        public MenuEmitterService()
        {
            restartParticle = new RestartParticle(this.LogicInitialize);
        }

        public new void LogicInitialize(Emitter emitter, ref Particle particle)
        {
            MenuEmitter MenuEmitter = (MenuEmitter)emitter;
            if (MenuEmitter.Spawn == true)
            {
                base.LogicInitialize(emitter, ref particle);
            }
        }

        public override void UpdateEmitter(Emitter emitter, GameTime gameTime)
        {
            base.UpdateEmitter(emitter, gameTime);
        }

        public new void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
