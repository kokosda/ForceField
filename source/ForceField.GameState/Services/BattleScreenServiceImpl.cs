using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

using ForceField.Core.Managers;
using ForceField.Core.Managers.Services;
using ForceField.Interfaces;
using ForceField.Domain.GameLogic;
using ForceField.Domain.Renderer;
using ForceField.Domain.Renderer.Base;
using ForceField.GameState.Services;
using ForceField.GameState.Models;
using ForceField.GameState.Models.Base;
using ForceField.UI.UIElements;
using ForceField.UI.UIComponentServices;
using ForceField.UI.Enums;

namespace ForceField.GameState.Services
{
    public class BattleScreenServiceImpl : GameplayScreenServiceImpl
    {
        public BattleScreenServiceImpl(ScreenManager screenManager)
            : base(screenManager)
        {
            //camera = screenManager.Game.Services.GetService(typeof(ICamera2DService)) as ICamera2DService;
            //input = screenManager.Game.Services.GetService(typeof(IUserInputService)) as IUserInputService;
            //animationService = screenManager.Game.Services.GetService(typeof(IAnimationService)) as IAnimationService;
            //guiService = screenManager.Game.Services.GetService(typeof(IGUIService)) as IGUIService;
            //unitManager = screenManager.Game.Components.First(p => p.GetType() == typeof(UnitsManager)) as UnitsManager;
        }

        #region Properties
       
        #endregion

        #region Initialization

        public void SetGameplayServices()
        {
            tileMapService = screenManager.Game.Services.GetService(typeof(ITileMapService)) as ITileMapService;
            heroService = screenManager.Game.Services.GetService(typeof(IHeroService)) as IHeroService;
            battleService = screenManager.Game.Services.GetService(typeof(IBattleService)) as IBattleService;
            scriptService = screenManager.Game.Services.GetService(typeof(IScriptService)) as IScriptService;
        }

        public void SetGUIService()
        {
            guiService = screenManager.Game.Services.GetService(typeof(IGUIService)) as IGUIService;
        }

        public override void Activate(Models.Base.GameScreen screen, bool instancePreserved)
        {
            base.Activate(screen, instancePreserved);

            BattleScreen battleScreen = screen as BattleScreen;

            InitializeMap(battleScreen);
            InitializeHeroes(battleScreen);
            InitializeGUI(battleScreen);

            camera.SetPosition(0, 0);
        }

        public override void Deactivate(Models.Base.GameScreen screen)
        {
            base.Deactivate(screen);
        }

        public override void Unload(Models.Base.GameScreen screen)
        {
            base.Unload(screen);
        }

        #endregion

        #region Update and Draw

        public override void Update(GameScreen screen, GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(screen, gameTime, otherScreenHasFocus, false);

            tileMapService.Update(gameTime);
            heroService.UpdateInBattleMode(gameTime);
            guiService.Update(gameTime);
            scriptService.Update(gameTime);
        }

        public override void HandleInput(GameScreen screen, GameTime gameTime, Core.Service.InputService input)
        {
            base.HandleInput(screen, gameTime, input);

            if (input.IsKeyPressed(Keys.D))
            {
                //Vector2 tileMapPosition = tileMapService.CurrentTileMap.Position;
                //tileMapPosition.X += 100;
                //tileMapService.CurrentTileMap.Position = tileMapPosition;
                camera.MoveToRight();
            }
        }

        public override void Draw(GameScreen screen, GameTime gameTime)
        {
            base.Draw(screen, gameTime);

            var battleScreen = screen as BattleScreen;

            tileMapService.Draw(gameTime);
            heroService.Draw(gameTime);
            guiService.Draw(gameTime);
        }

        #endregion

        #region private

        private void InitializeMap(BattleScreen screen)
        {
            tileMapService.CurrentTileMap = battleService.CurrentBattle.Ground;
            tileMapService.CurrentTileMap.Position = new Vector2(0, 0);
        }

        private void InitializeHeroes(BattleScreen screen)
        {
            Hero playerHero = battleService.CurrentBattle.PlayerHero;
            Hero enemyHero = battleService.CurrentBattle.EnemyHero;

            heroService.ClearCurrentHeroes();
            heroService.AddCurrentHero(playerHero);
            heroService.AddCurrentHero(enemyHero);

            playerHero.Sprite.CurrentAction = AnimatedAction.Idle;
            animationService.Reset(playerHero.Sprite.CurrentAnimation);
     
            enemyHero.Sprite.CurrentAction = AnimatedAction.Idle;
            animationService.Reset(enemyHero.Sprite.CurrentAnimation);
        }

        private void InitializeGUI(BattleScreen screen)
        {
            /*
            SpriteText font = spriteService.GetSpriteText("unitbar");
            Label testLabel = new Label(font);
            testLabel.Location = new Vector2(500, 100);
            testLabel.Text = "Test label";
            testLabel.TextColor = new Color(1.0f, 0.0f, 0.0f);

            UILabelService labelService = new UILabelService(spriteBatchService, input);
            labelService.AddComponent(testLabel);
            guiService.AddComponentService(labelService);
            
            Sprite sprite = spriteService.GetByName("button_image");
            font = spriteService.GetSpriteText("unitbar");
            Button testButton = new Button(sprite, font, UI.Enums.ButtonAlign.Center, 4);
            testButton.Text = "Button";
            testButton.Location = new Vector2(500, 200);

            UIButtonService buttonService = new UIButtonService(spriteBatchService, input);
            buttonService.AddComponent(testButton);
            guiService.AddComponentService(buttonService);
             */

            screen.LabelService = new UILabelService(screenManager.Game);
            screen.ButtonService = new UIButtonService(screenManager.Game);

            screen.TestLabel = screen.LabelService.CreateLabel("unitbar", new Vector2(500, 100), "Test label");
            screen.LabelService.AddComponent(screen.TestLabel);

            screen.TestButton = screen.ButtonService.CreateButton("button_image", "unitbar", ButtonAlign.Center,
                4, "Button", new Vector2(500, 200));
            screen.TestButton.ClickedEvent += ButtonClickedHandler;
            screen.SpellButton = screen.ButtonService.CreateButton("button_image", "unitbar", ButtonAlign.Center,
                4, "Spell", new Vector2(500, 400));
            screen.SpellButton.Id = "HeroSpell01";
            screen.TestButton.ClickedEvent += SpellButtonClickedHandler;
            screen.ButtonService.AddComponent(screen.TestButton);

            guiService.AddComponentService(screen.LabelService);
            guiService.AddComponentService(screen.ButtonService);
        }

        #region fields

        private BattleScreen GetCurrentBattleScreen()
        {
            GameScreen[] screens = screenManager.GetScreens();

            foreach (GameScreen screen in screens)
            {
                if (screen is BattleScreen)
                {
                    return screen as BattleScreen;
                }
            }
            
            return null;
        }

        private void ButtonClickedHandler(UIComponent component)
        {
            BattleScreen screen = GetCurrentBattleScreen();

            if (screen != null)
            {
                screen.TestLabel.Text = "Test button clicked";
            }
        }

        private void SpellButtonClickedHandler(UIComponent component)
        {
            if (component.Id == "HeroSpell01")
            {
                // todo: проверка условий (таких как, ход игрока, ести ли мана, и.т.д)
                if (true)
                {
                    Hero playerHero = battleService.CurrentBattle.PlayerHero;
                    string scriptId = playerHero.Library.Slots[0].Spell.ScriptPath + playerHero.Library.Slots[0].Spell.ScriptName;
                    IScript script = scriptService.GetScript(scriptId);
                    scriptService.AddActiveScript(script);
                }
            }
        }

        private void AbilityButtonClickedHandler(UIComponent component)
        {
        }

        private IBattleService battleService;
        private ITileMapService tileMapService;
        private IHeroService heroService;
        private IGUIService guiService;
        private IScriptService scriptService;

        #endregion

        #endregion
    }
}
