using System.Windows.Forms;
using ForceField.Interfaces;
using Microsoft.Xna.Framework;
using ForceField.Core;
using ForceField.Core.Managers;
using ForceField.GameState;
using ForceField.GameState.Models;
using System.Reflection;
using System.Collections.Generic;

namespace ForceField.Game
{
    public class FFMain : Microsoft.Xna.Framework.Game
    {
        public FFMain()
        { 
            Content.RootDirectory = GameConfiguationManager.Read("ContentDir");
            Depository.Content = Content;
            Depository.Graphics = new GraphicsDeviceManager(this);
            inputManager = new InputManager(this);
            resourceManager = new ResourceManager(GameConfiguationManager.Read("ContentDir"), this);
            spriteManager = new SpriteManager(this);
            animationManager = new AnimationManager(this, GameConfiguationManager.Read("AnimationsDir"));
            spriteBatchManager = new SpriteBatchManager(this);
            particleSystemManager = new ParticleSystemManager(this);
            unitsManager = new UnitsManager(this);
            cameraManager = new Camera2DManager(this, new Vector2(10,10));
            screenManager = new ScreenManager(this, Assembly.GetExecutingAssembly().GetName().Version.ToString());
            levelManager = new LevelManager(this);
            guiManager = new GUIManager(this);
            battleManager = new BattleManager(this);
            cursorManager = new CursorManager(this);
            scriptManager = new ScriptManager(this,new List<Assembly>(), GameConfiguationManager.Read("CompileScript"));
            Depository.MainForm = Control.FromHandle(Window.Handle) as Form;
            screenManager.AddScreen(new BackgroundScreen(screenManager.BackgroundScreenService), null);
            screenManager.AddScreen(new MainMenuScreen(screenManager.MainMenuScreenService), null);          
        }

        #region protected

        protected override void Initialize()
        {
            log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo("App.config"));
            log4net.LogManager.GetLogger(GetType()).InfoFormat("***** Start log *****");
            Depository.Graphics.PreferredBackBufferWidth = 800;
            Depository.Graphics.PreferredBackBufferHeight = 600;
            IsMouseVisible = true;
            Depository.Graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatchService = Services.GetService(typeof(ISpriteBatchService)) as ISpriteBatchService;
            IsFixedTimeStep = false;

            Depository.Graphics.SynchronizeWithVerticalRetrace = true;
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            log4net.LogManager.GetLogger(GetType()).InfoFormat("***** End log *****");
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            spriteBatchService.DrawAll(gameTime);
        }

        #endregion

        #region private
        #region Services
        ISpriteBatchService spriteBatchService;
        #endregion
        private ResourceManager resourceManager;
        private UnitsManager unitsManager;
        private SpriteManager spriteManager;
        private AnimationManager animationManager;
        private ScreenManager screenManager;
        private ParticleSystemManager particleSystemManager;
        private LevelManager levelManager;
        private SpriteBatchManager spriteBatchManager;
        private InputManager inputManager;
        private GUIManager guiManager;
        private Camera2DManager cameraManager;
        private BattleManager battleManager;
        private CursorManager cursorManager;
        private ScriptManager scriptManager;
        #endregion
    }
}
