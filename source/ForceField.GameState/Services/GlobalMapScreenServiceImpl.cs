using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using ForceField.Interfaces;
using ForceField.Core.Service;
using ForceField.Domain.GameLogic;
using ForceField.GameState.Services.Base;
using ForceField.GameState.Models;
using ForceField.GameState.Models.Base;
using ForceField.UI.UIComponentServices;
using ForceField.UI.UIElements;
using Microsoft.Xna.Framework.Graphics;
using ForceField.Domain.Input;
using ForceField.Domain.Renderer;
using ForceField.Domain.Comparers;
using System.Threading;
using ForceField.Core.Managers.Services;

namespace ForceField.GameState.Services
{
    public class GlobalMapScreenServiceImpl : GameplayScreenServiceImpl
    {
        public GlobalMapScreenServiceImpl(ScreenManager manager) 
            : base(manager)
        {
            cursorService = manager.Game.Services.GetService(typeof(ICursorService)) as ICursorService;
        }

        public void SetGameplayServices()
        {
            tileMapService = screenManager.Game.Services.GetService(typeof(ITileMapService)) as ITileMapService;
            heroService = screenManager.Game.Services.GetService(typeof(IHeroService)) as IHeroService;
            battleService = screenManager.Game.Services.GetService(typeof(IBattleService)) as IBattleService;
            globalMapService = screenManager.Game.Services.GetService(typeof(IGlobalMapService)) as IGlobalMapService;
            cursorService = screenManager.Game.Services.GetService(typeof(ICursorService)) as ICursorService;
        }

        public void SetGUIService()
        {
           guiService = base.screenManager.Game.Services.GetService(typeof(IGUIService)) as IGUIService;
        }

        public override void Activate(GameScreen screen, bool instancePreserved)
        {
            base.Activate(screen, instancePreserved);
            

            UILabelService labelService = new UILabelService(screenManager.Game);
            Label label = new Label(spriteService.GetSpriteText("menu"));
            label.Text = screenManager.Version;
            label.SpriteText.Scale = 0.7f;
            Game game = screenManager.Game;
            label.Location = new Vector2(0, game.GraphicsDevice.Viewport.Height - (label.SpriteText.SpriteFont.MeasureString(screenManager.Version).Y / 2));

            labelService.AddComponent(label);
            guiService.AddComponentService(labelService);
        }
        List<Point> points = null;
        public override void HandleInput(GameScreen screen, GameTime gameTime, InputService input)
        {
            camera.Data.IsNewPosition = false;
            camera.Data.IsUpdate = false;
            base.HandleInput(screen, gameTime, input);

            Hero player = heroService.PlayerHero;

            player.SetLayer(tileMapService.CurrentTileMap);
            Point curPos = input.IsMousePosition();
            player.SetBound(tileMapService.CurrentTileMap);

            cursorService.CheckSelect(player);

            if (input.IsMouseButtonPress(MouseButton.LeftButton))
            {
                Point m = input.IsMousePosition();
                TileMap map = tileMapService.CurrentTileMap;
                IPathService ser = new PathService();
                points = ser.GetPath(map, player.LocationArea, player.CurrentTilePosition, map.GetTilePosition(new Vector2(m.X, m.Y)));
                for (int i = 0; i < points.Count; ++i)
                {
                    player.CurrentTilePosition = points[i];
                }
            }

            if (input.IsKeyPressed(Keys.B))
            {
                battleService.StartBattle(battleService.CreateTestBattle());
                screenManager.AddScreen(new BattleScreen(screenManager.BattleScreenService), null);
            }

            if (player.IsSelected)
            {
               player.Sprite.Color = Color.Black;
            }

            if (input.IsMouseButtonPress(MouseButton.RightButton))
            {
                player.Sprite.Color = Color.White;
            }

            if (input.IsKeyPressed(Keys.A))
            {
                camera.MoveToLeft();
            }

            if (input.IsKeyPressed(Keys.D))
            {
                camera.MoveToRight();
            }

            if (input.IsKeyPressed(Keys.W))
            {
                camera.MoveToTop();
            }

            if (input.IsKeyPressed(Keys.S))
            {
                camera.MoveToBottom();
            }

            if (!player.IsMove)
            {
                //if (input.IsKeyPressed(Keys.A))
                //{
                //    player.CurrentTilePosition.X += 1;
                //    //camera.MoveToLeft();
                //    player.IsMove = true;
                //}

                //if (input.IsKeyPressed(Keys.D))
                //{
                //    player.CurrentTilePosition.X -= 1;
                //    //camera.MoveToRight();
                //    player.IsMove = true;
                //}

                //if (input.IsKeyPressed(Keys.W))
                //{
                //    player.CurrentTilePosition.Y -= 1;
                //    //camera.MoveToTop();
                //    player.IsMove = true;
                //}

                //if (input.IsKeyPressed(Keys.S))
                //{

                //    player.CurrentTilePosition.Y += 1;
                //    //camera.MoveToBottom();
                //    player.IsMove = true;
                //}
            }
            else
            {
                //player.Sprite.Location += tileMapService.GetVectorToNewTile(player);
                player.IsMove = false;
            }

            List<Hero> heroes;
            if (IsBeginFight(out heroes) == true)
            {
                //Начинаем бой и переходим в окно ирвина
                Hero enemyHero;
                if (heroes[0] != heroService.PlayerHero)
                {
                    enemyHero = heroes[0];
                }
                else
                {
                    enemyHero = heroes[1];
                }

                battleService.StartBattle(heroService.PlayerHero, enemyHero);
                screenManager.AddScreen(new BattleScreen(screenManager.BattleScreenService), null);
            }

        }

        public override void Update(GameScreen screen, GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            tileMapService.Update(gameTime);
            guiService.Update(gameTime);
        }

        public override void Draw(GameScreen screen, GameTime gameTime)
        {
            tileMapService.Draw(gameTime);
            heroService.Draw(gameTime);
            guiService.Draw(gameTime);
        }

        private bool IsBeginFight(out List<Hero> outputHeroes)
        {
            /*
            IList<Hero> heroes = heroService.Units; 
            foreach (Hero hero in heroes)
            {
                foreach (Hero secondHero in heroes)
                {
                    Point heroLocation = hero.CurrentTilePosition;
                    Point secondHeroLocation = secondHero.CurrentTilePosition;
                    if (hero != secondHero)
                    {
                        if (heroLocation.X + 1 == secondHeroLocation.X && heroLocation.Y + 1 == secondHeroLocation.Y)
                        {
                            outputHeroes = new List<Hero>();
                            outputHeroes.Add(hero);
                            outputHeroes.Add(secondHero);
                            return true;
                        }

                        if (heroLocation.X - 1 == secondHeroLocation.X && heroLocation.Y - 1 == secondHeroLocation.Y)
                        {
                            outputHeroes = new List<Hero>();
                            outputHeroes.Add(hero);
                            outputHeroes.Add(secondHero);
                            return true;
                        }

                        if (heroLocation.X == secondHeroLocation.X && heroLocation.Y + 1 == secondHeroLocation.Y)
                        {
                            outputHeroes = new List<Hero>();
                            outputHeroes.Add(hero);
                            outputHeroes.Add(secondHero);
                            return true;
                        }

                        if (heroLocation.X + 1 == secondHeroLocation.X && heroLocation.Y == secondHeroLocation.Y)
                        {
                            outputHeroes = new List<Hero>();
                            outputHeroes.Add(hero);
                            outputHeroes.Add(secondHero);
                            return true;
                        }

                        if (heroLocation.X == secondHeroLocation.X && heroLocation.Y - 1 == secondHeroLocation.Y)
                        {
                            outputHeroes = new List<Hero>();
                            outputHeroes.Add(hero);
                            outputHeroes.Add(secondHero);
                            return true;
                        }

                        if (heroLocation.X - 1 == secondHeroLocation.X && heroLocation.Y == secondHeroLocation.Y)
                        {
                            outputHeroes = new List<Hero>();
                            outputHeroes.Add(hero);
                            outputHeroes.Add(secondHero);
                            return true;
                        }

                        if (heroLocation.X - 1 == secondHeroLocation.X && heroLocation.Y + 1 == secondHeroLocation.Y)
                        {
                            outputHeroes = new List<Hero>();
                            outputHeroes.Add(hero);
                            outputHeroes.Add(secondHero);
                            return true;
                        }

                        if (heroLocation.X + 1 == secondHeroLocation.X && heroLocation.Y - 1 == secondHeroLocation.Y)
                        {
                            outputHeroes = new List<Hero>();
                            outputHeroes.Add(hero);
                            outputHeroes.Add(secondHero);
                            return true;
                        }
                    }
                }
            }

            outputHeroes = null;
            return false;
             * */

            outputHeroes = null;
            return false;
        }

        #region private

        private ITileMapService tileMapService;
        private IHeroService heroService;
        private IBattleService battleService;
        private IGlobalMapService globalMapService;
        private IGUIService guiService;
        private ICursorService cursorService;
        #endregion
    }
}
