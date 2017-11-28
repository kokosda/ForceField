using Microsoft.Xna.Framework;
using ForceField.Core.Services;
using ForceField.Interfaces;
using System;
using ForceField.Domain.Renderer;
using System.Linq;
using ForceField.Domain.Renderer.Base;
namespace ForceField.Core.Managers
{
    public class ParticleSystemManager : IGameComponent
    {
        EmitterService EmitterService;
        MenuEmitterService menuParticleService;
        Game game;

        public ParticleSystemManager(Game game)
        {
            EmitterService = new EmitterService();
            menuParticleService = new MenuEmitterService();
            game.Services.AddService(typeof(IParticleEmitterService<Emitter>), EmitterService);
            game.Services.AddService(typeof(IParticleEmitterService<MenuEmitter>), menuParticleService);
            game.Components.Add(this);
            this.game = game;
        }

        public Emitter GetEmitter<T>(string name) where T : Emitter
        {
            EmitterService service = game.Services.GetService(typeof(IParticleEmitterService<T>)) as EmitterService;
            return service.Units.First(p => p.Name == name);
        }

        public void AddEmitter<T>(Emitter emitter) where T : Emitter
        {
            EmitterService service = game.Services.GetService(typeof(IParticleEmitterService<T>)) as EmitterService;
            for (int i = 0; i < emitter.Particles.Length; ++i)
            {
                Particle particle = new Particle();
                particle.Sprite = Sprite.Clone(emitter.Sprite);
                particle.Sprite.CanDraw = false;
                service.restartParticle(emitter, ref particle);
                emitter.Particles[i] = particle;
            }
            service.Units.Add(emitter);
        }

        public void Clear<T>() where T : Emitter
        {
            EmitterService service = game.Services.GetService(typeof(IParticleEmitterService<T>)) as EmitterService;
            service.Units.Clear();
        }

        public void RemoveEmitter<T>(string name) where T : Emitter
        {
            EmitterService service = game.Services.GetService(typeof(IParticleEmitterService<T>)) as EmitterService;
            service.Units.Remove(service.Units.First(p => p.Name == name));
        }

        public void Initialize()
        {
            menuParticleService.Initialize(game);
        }
    }
}
