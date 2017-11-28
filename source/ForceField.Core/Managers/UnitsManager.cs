using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;

using ForceField.Interfaces;
using ForceField.Core.Services;
using ForceField.Domain.GameLogic;
using ForceField.Domain.Renderer;
using ForceField.Core.Managers.Services;
using System;
namespace ForceField.Core.Managers
{
    public class UnitsManager : DrawableGameComponent
    {
        public UnitsManager(Game game) : base (game)
        {
            creatureService = new CreatureService(new List<BattleUnit>(), this);
            heroService = new HeroService(new List<BattleUnit>(), this);
            tileMapService = new TileMapService(new List<TileMap>(), this);

            AddServices();

            inputService = game.Services.GetService(typeof(IUserInputService)) as IUserInputService;
            selectedUnits = new List<Unit>();
            
            Game.Components.Add(this);
        }

        public override void Initialize()
        {
            //Game.Services.AddService(typeof(IUnitsService<TileMap>), tileMapService);
            //Game.Services.AddService(typeof(ICreatureService), creatureService);
            //Game.Services.AddService(typeof(IHeroService), heroService);

            #region Initialize Particle and emitters

            particleService = Game.Services.GetService(typeof(IParticleEmitterService<Emitter>)) as EmitterService;
            menuParticleService = Game.Services.GetService(typeof(IParticleEmitterService<MenuEmitter>)) as MenuEmitterService;

            #endregion

            var spriteService = Game.Services.GetService(typeof(ISpriteService)) as ISpriteService;
            RegisterServices(spriteService);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            ConstructInitialUnits();
            TileMapService.ConstructInitialTileMaps();

           // heroService.PlayerHero = heroService.CreateDefaultHero(new Point(10, 3), "HeroPlayer", "Hero", Color.White, tileMapService.CurrentTileMap);
            //heroService.PlayerHero.IsPlayerHero = true;
            //heroService.AddHero(heroService.PlayerHero);

            //heroService.AddHero(heroService.CreateDefaultHero(new Point(40, 10), "EnemyHero", "Hero", Color.Red, tileMapService.CurrentTileMap));
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            //tileMapService.Draw(gameTime);
            base.Draw(gameTime);
        }

        public override string ToString()
        {
            var unitsServices = GetType().GetProperties().Where<PropertyInfo>(p => p.Name.Contains("Service")).Select(p => p.GetValue(this, null)).ToList();
            var unitsCount = 0;

            for (var i = 0; i < unitsServices.Count; i++ )
            {
                var serviceUnits = unitsServices[i];
                var propertyUnits = serviceUnits.GetType().GetProperties().Where(p => p.Name.StartsWith("Units")).FirstOrDefault().GetValue(serviceUnits, null).GetType();
                var methodCount = propertyUnits.GetMethods().Where(p => p.Name.Contains("Count")).ElementAt(0);
                unitsCount += (int)methodCount.Invoke(serviceUnits.GetType().GetProperties().ElementAt(0).GetValue(serviceUnits, null), null);
            }

            unitsCount += ParticleService.Units.Count;

            return string.Format("Units service = {0}, Units = {1}", GetType().GetProperties().Where(p => p.Name.Contains("Service")).Count(), unitsCount);
        }

        #region Properties

        public CreatureService CreatureService
        {
            get
            {
                return creatureService;
            }
        }

        public HeroService HeroService
        {
            get
            {
                return heroService;
            }
        }

        public TileMapService TileMapService
        {
            get
            {
                return tileMapService;
            }
        }

        public EmitterService ParticleService
        {
            get
            {
                return particleService;
            }
        }

        public bool IsGameplay
        {
            get
            {
                return isGamePlay;
            }
            set
            {
                isGamePlay = value;
            }
        }

        public bool IsMenu;

        #endregion

        #region private

        private void AddServices()
        {
            Game.Services.AddService(typeof(ITileMapService), TileMapService);
            Game.Services.AddService(typeof(ICreatureService), CreatureService);
            Game.Services.AddService(typeof(IHeroService), HeroService);
        }

        private void RegisterServices(ISpriteService spriteService)
        {
            particleService.SetSpriteService(spriteService);

            heroService.SetGameplayServices();
            tileMapService.SetGameplayServices();
        }

        private void ConstructInitialUnits()
        {
            tileMapService.CreateUnitsFromServiceByResourcePath("Tiles");
        }

        #region fields

        private IUserInputService inputService;
        private CreatureService creatureService;
        private HeroService heroService;
        private TileMapService tileMapService;
        private MenuEmitterService menuParticleService;
        private EmitterService particleService;
        
        private bool isGamePlay = false;

        private List<Unit> selectedUnits; 

        #endregion

        #endregion
    }
}
