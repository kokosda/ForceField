using System.Collections.Generic;
using System.Linq;
using ForceField.Interfaces;
using Microsoft.Xna.Framework;
using ForceField.Domain.GameLogic;
using ForceField.Domain.Renderer.Base;
using Microsoft.Xna.Framework.Graphics;
using System;
using ForceField.Domain.Interface;
using ForceField.Domain.Renderer;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace ForceField.Core.Services
{
    public class EmitterService : IParticleEmitterService<Emitter>, IUnitsService<Emitter>
    {
        public EmitterService()
        {
            Units = new List<Emitter>();
            spriteService = null;
            restartParticle = new RestartParticle(LogicInitialize);
        }

        #region Properties

        public IList<Emitter> Units { get; set; }
        public delegate void RestartParticle(Emitter emitterInfo, ref Particle Particle);
        public RestartParticle restartParticle;

        #endregion

        public void SetSpriteService(ISpriteService spriteService)
        {
            this.spriteService = spriteService;
        }

        public void CreateUnitsFromServiceByResourcePath(string templatePath)
        {
            return;
        }

        public Unit GetRandom()
        {
            return Units[random.Next(0, Units.Count - 1)];
        }

        public Texture2D GetFirstTexture(Emitter unit)
        {
            if (spriteService != null)
            {
                return spriteService.GetTexture(unit.Sprite, 0);
            }
            else
            {
                return default(Texture2D);
            }
        }

        public override string ToString()
        {
            return string.Format("{0}s = {1}", typeof(Emitter).Name, Units != null ? Units.Count : 0);
        }

        #region Update and Draw

        public virtual void UpdateEmitter(Emitter emitter, GameTime gameTime)
        {
                Particle[] Particles = emitter.Particles;
                for (int i = 0; i < Particles.Length; ++i)
                {
                     BaseUpdateParticle(emitter, ref Particles[i]);
                }
        }

        public void Update(GameTime gameTime)
        {
                Parallel.ForEach(Units, p =>
                {
                    Emitter emitter = p;
                    if (emitter.IsActive)
                    {
                        UpdateEmitter(emitter, gameTime);
                    }
                });
        }

        public void Draw(GameTime gameTime)
        {
            int Count = Units.Count;
            
            for (int i = 0; i < Count; ++i)
            {
                Emitter emitter = Units[i];
                if (emitter.IsActive)
                {
                        for (int j = 0; j < emitter.Particles.Length; ++j)
                        {
                            Particle particle = emitter.Particles[j];
                            particle.Sprite.Color = new Color(particle.Color);
                            spriteBatchService.Draw(particle.Sprite);
                        }
                }
            }
        }

        public Vector4 ColorInterpolate(Vector4 MinColor, Vector4 MaxColor, float amount)
        {
            return new Vector4(MathHelper.Lerp(MinColor.X, MaxColor.X, amount),
                               MathHelper.Lerp(MinColor.Y, MaxColor.Y, amount),
                               MathHelper.Lerp(MinColor.Z, MaxColor.Z, amount),
                               MathHelper.Lerp(MinColor.W, MaxColor.W, amount));
        }

        public void LogicInitialize(Emitter Emitter, ref Particle Particle)
        {
            EmitterInfo EmitterInfo = Emitter.EmitterInfo;
            float seed = (float)random.NextDouble();
            #region Life Time and Angle Rotation
            Particle.Age = MathHelper.Lerp(EmitterInfo.MinLife, EmitterInfo.MaxLife, seed);
            Particle.Sprite.Angle = MathHelper.Lerp(EmitterInfo.MinRotation, EmitterInfo.MaxRotation, seed);
            #endregion
            #region Vector
            Vector2 vector = new Vector2();
            Particle.AngleVector = MathHelper.Lerp(EmitterInfo.MinSpread, EmitterInfo.MaxSpread, seed) / 180.0f * 3.14159f;
            Particle.Vector = vector;
            #endregion
            float ageDiff = Particle.Age / EmitterInfo.MinusTime;
            Particle.AgeDifference = ageDiff;
            #region Velocity Initialize
            Particle.Velocity = MathHelper.Lerp(EmitterInfo.MinStartVelocity, EmitterInfo.MaxStartVelocity, seed);
            float Velocity = MathHelper.Lerp(EmitterInfo.MinEndVelocity, EmitterInfo.MaxEndVelocity, seed);
            Particle.MinusVelocity = (Particle.Velocity-Velocity)/ageDiff;
            #endregion
            #region Initialize Color
            Vector4 Color = ColorInterpolate(EmitterInfo.MinEndColor, EmitterInfo.MaxEndColor, seed);
            Particle.Color = ColorInterpolate(EmitterInfo.MinStartColor, EmitterInfo.MaxStartColor, seed);
            Particle.MinusColor = new Vector4((Particle.Color.X - Color.X) / ageDiff, (Particle.Color.Y - Color.Y) / ageDiff, (Particle.Color.Z - Color.Z) / ageDiff, (Particle.Color.W - Color.W) / ageDiff);
            #endregion
            Particle.TimeSpawn = (int)MathHelper.Lerp(EmitterInfo.MinTimeSpawn, EmitterInfo.MaxTimeSpawn, seed);
            Particle.AngleVector = MathHelper.Lerp(EmitterInfo.MinStartOffsetVector, EmitterInfo.MaxStartOffsetVector, seed);
            Particle.OffsetVelocity = (Particle.AngleVector - MathHelper.Lerp(EmitterInfo.MinEndOffsetVector, EmitterInfo.MaxEndOffsetVector, seed)) / ageDiff;
            #region Initialize Scale
            float endScale = MathHelper.Lerp(EmitterInfo.MinEndScale, EmitterInfo.MaxEndScale, seed);
            Particle.Sprite.Scale = MathHelper.Lerp(EmitterInfo.MinStartScale, EmitterInfo.MaxStartScale, seed);
            Particle.MinusScale = (Particle.Sprite.Scale - endScale) / ageDiff;
            #endregion
            #region Get Location From Emitter Position
            Particle.Sprite.Location = Emitter.EmitterType.GetLocation(EmitterInfo.Location);
            #endregion
        }

        public void Initialize(Game game)
        {
            this.spriteBatchService = game.Services.GetService(typeof(ISpriteBatchService)) as SpriteBatchService;
            this.spriteService = game.Services.GetService(typeof(ISpriteService)) as SpriteService;
        }

        #endregion

        #region private

        private void BaseUpdateParticle(Emitter emitter,ref Particle particle)
        {
            if (particle.Sprite.CanDraw == true)
            {
                particle.Age -= emitter.EmitterInfo.MinusTime;
                particle.Velocity -= particle.MinusVelocity;
                particle.AngleVector += particle.OffsetVector;
                particle.Vector = new Vector2((float)Math.Cos(particle.AngleVector), (float)Math.Sin(particle.AngleVector));
                particle.Sprite.Location += particle.Vector * particle.Velocity;
                particle.Sprite.Scale -= particle.MinusScale;
                particle.Color -= particle.MinusColor;
                particle.OffsetVector += particle.OffsetVelocity;
                if (particle.Age < 0)
                {
                    particle.Sprite.CanDraw = false;
                }
            }
            else
            {
                particle.TimeSpawn -= emitter.EmitterInfo.MinusTime;
                if (particle.TimeSpawn < 0)
                {
                    particle.Sprite.CanDraw = true;
                    restartParticle(emitter, ref particle);
                }
            }
        }

        #region members
        private Random random = new Random();
        private ISpriteService spriteService;
        private ISpriteBatchService spriteBatchService;
        #endregion

        #endregion
    }
}
