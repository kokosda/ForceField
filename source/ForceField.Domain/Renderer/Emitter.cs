using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ForceField.Domain.Renderer.Base;
using System.Collections.Generic;
using ForceField.Domain.GameLogic;
using ForceField.Domain.Interface;
using ForceField.Domain.Renderer;

namespace ForceField.Domain.Renderer
{
    [Serializable]
    public class Emitter : Unit
    {
        public Particle[] Particles { get; set; }
        public EmitterType EmitterType { get; set; }
        public EmitterInfo EmitterInfo { get; set; }
        public bool IsActive { get; set; }
        public Emitter(Sprite sprite,int Count,string name)
        {
            Sprite = new Renderer.Sprite();
            EmitterInfo = new EmitterInfo();
            EmitterType = new PointEmitter();
            EmitterInfo.Location = new Vector2(0,0);
            EmitterInfo.MaxEndColor = new Vector4(1, 1, 1, 1);
            EmitterInfo.MinEndColor = new Vector4(1, 1,1,1);
            EmitterInfo.MinusTime = 100;
            EmitterInfo.MaxLife = 999;
            EmitterInfo.MinLife = 999;
            EmitterInfo.MaxSpread = 360;
            EmitterInfo.MinSpread = 0;
            EmitterInfo.MaxStartColor = new Vector4(1, 1, 1, 1);
            EmitterInfo.MinStartColor = new Vector4(1, 1, 1, 1);
            EmitterInfo.MaxTimeSpawn = 1;
            EmitterInfo.MinTimeSpawn = 0;
            EmitterInfo.MaxStartScale = 1f;
            EmitterInfo.MinStartScale = 1f;
            EmitterInfo.MaxEndScale = 1f;
            EmitterInfo.MinEndScale = 1f;
            EmitterInfo.MaxRotation = 0f;
            EmitterInfo.MinRotation = 0f;
            EmitterInfo.MaxEndVelocity = 1f;
            EmitterInfo.MinEndVelocity = 1f;
            EmitterInfo.MaxStartVelocity = 1f;
            EmitterInfo.MinStartVelocity = 1f;
            base.Sprite = sprite;
            IsActive = true;
            Name = name;
            Particles = new Particle[Count];
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
