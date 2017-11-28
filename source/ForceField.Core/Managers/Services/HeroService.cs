using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ForceField.Interfaces;
using ForceField.Domain.GameLogic;
using ForceField.Domain.Renderer.Base;
using ForceField.Domain.Renderer;

namespace ForceField.Core.Managers.Services
{
    public class HeroService : BattleUnitService, IHeroService
    {
        public HeroService(IList<BattleUnit> heroList, UnitsManager manager) : base(heroList,manager)
        {
            Units = heroList;
            currentHeroes = new List<Hero>();
            playerHero = null;

            spriteBatchService = manager.Game.Services.GetService(typeof(ISpriteBatchService)) as ISpriteBatchService;
            spriteService = manager.Game.Services.GetService(typeof(ISpriteService)) as ISpriteService;
            animationService = manager.Game.Services.GetService(typeof(IAnimationService)) as IAnimationService;
            camera = manager.Game.Services.GetService(typeof(ICamera2DService)) as ICamera2DService;
            unitsManager = manager;
        }

        #region Properties

        //public IList<Hero> Units { get; set; }

        public Hero PlayerHero
        {
            get
            {
                return playerHero;
            }
            set 
            {
                if (!Units.Contains(value))
                {
                    AddHero(value);
                }

                playerHero = value; 
            }
        }

        #endregion

        public void SetSpriteService(ISpriteService spriteService)
        {
            if (spriteBatchService != null)
            {
                this.spriteService = spriteService;
            }
        }

        public Hero CreateDefaultHero(Point location, string name, string spriteName,Color color, TileMap map)
        {
            Hero hero = new Hero();
            hero.Sprite = Sprite.Clone(spriteService.GetByName(spriteName));
            hero.CurrentTilePosition = location;
            hero.Sprite.Location = map.Tiles[location.X + location.Y * map.Size.X].Sprite.Location;
            hero.Sprite.TranslationPosition = hero.Sprite.Location + camera.Data.Translation;
            hero.Sprite.CanDraw = true;
            hero.Sprite.Scale = 0.50f;
            hero.SetLayer(1);
            hero.Sprite.Unit = hero;
            hero.Sprite.Color = color;
            return hero;
        }

        public void CreateUnitsFromServiceByResourcePath(string templatePath)
        {
        }

        public Unit GetRandom()
        {
            throw new NotImplementedException();

            // todo
        }

        public Texture2D GetFirstTexture(Hero unit)
        {
            throw new NotImplementedException();

            // todo
        }

        public void SetGameplayServices()
        {
        }

        public void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void UpdateInGlobalMapMode(GameTime gameTime)
        {
        }

        public void UpdateInBattleMode(GameTime gameTime)
        {
        }

        public void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        /// <summary>
        /// Создает героя и добавляет его в список героев. todo: параметры метода.
        /// </summary>
        /// <returns>Созданный герой</returns>
        public Hero AddHero()
        {
            Hero hero = new Hero();

            // todo

            Units.Add(hero);

            return hero;
        }

        /// <summary>
        /// Добавляет уже созданного героя в список героев.
        /// </summary>
        /// <param name="hero">Добавляемый герой</param>
        public void AddHero(Hero hero)
        {
            if (!Units.Contains(hero))
            {
                Units.Add(hero);
            }
        }

        /// <summary>
        /// Создает героя, но не добавляет его в список героев. todo: параметры метода.
        /// </summary>
        /// <returns>Созданный герой</returns>
        public Hero CreateHero()
        {
            Hero hero = new Hero();

            // todo

            return hero;
        }

        /// <summary>
        /// Добавляет героя в список текущих героев. Герой должен присуствовать в списке героев.
        /// </summary>
        /// <param name="hero">Добавляемый герой</param>
        public void AddCurrentHero(Hero hero)
        {
            if (!Units.Contains(hero))
            {
                currentHeroes.Add(hero);
            }
        }

        /// <summary>
        /// Очистить список текущих героев.
        /// </summary>
        public void ClearCurrentHeroes()
        {
            currentHeroes.Clear();
        }

        public Hero CreateTestBattlePlayerHero()
        {
            Hero hero = new Hero();

            BattleUnitDescription heroDescription = new BattleUnitDescription();
            heroDescription.BaseMaxHp = 20;
            heroDescription.BaseStrength = 2;
            heroDescription.BaseActionPoints = 3;
            heroDescription.Size = BattleUnitSize.Normal;
            heroDescription.Color = new ColorDescription(NationColor.None);
            hero.Description = heroDescription;

            hero.IsPlayerHero = true;
            hero.MagicPower = 4;
            hero.Sprite = spriteService.GetByName("BattleHero");
            hero.Sprite.Unit = hero;
            
            // инициализация библиотеки героя

            return hero;
        }

        public Hero CreateTestBattleEnemyHero()
        {
            Hero hero = new Hero();

            BattleUnitDescription heroDescription = new BattleUnitDescription();
            heroDescription.BaseMaxHp = 20;
            heroDescription.BaseStrength = 2;
            heroDescription.BaseActionPoints = 3;
            heroDescription.Size = BattleUnitSize.Normal;
            heroDescription.Color = new ColorDescription(NationColor.Green);
            hero.Description = heroDescription;

            hero.IsPlayerHero = false;
            hero.MagicPower = 4;
            hero.Sprite = spriteService.GetByName("BattleHero");
            hero.Sprite.Unit = hero;

            // инициализация библиотеки

            return hero;
        }

        #region private

        private void UpdateBattleHero(Hero hero, GameTime gameTime)
        {

        }

        private void UpdateLocationHero(Hero hero, GameTime gameTime)
        {

        }

        private void UpdateGlobalMapHero(Hero hero, GameTime gameTime)
        {

        }

        private void DrawHero(Hero hero)
        {
            spriteBatchService.Draw(hero.Sprite);
        }
        
        #region fields

        private ICamera2DService camera;
        private ISpriteBatchService spriteBatchService;
        private ISpriteService spriteService;
        private IAnimationService animationService;
        private UnitsManager unitsManager;

        private List<Hero> currentHeroes;
        private Hero playerHero;

        #endregion

        #endregion
    }
}
