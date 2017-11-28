using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using ForceField.Core.Managers;
using ForceField.Core;
using GameEditor.Core;
using ForceField.Interfaces;
using System.Reflection;
using System.Windows.Forms;
using GameEditor.Core.Manager;
using ForceField.Domain.Renderer;
namespace GameEditor
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        //GraphicsDeviceManager graphics;

        public Game1()
        {
            Content.RootDirectory = GameConfiguationManager.Read("ContentDir");
            Depository.Content = Content;
            Depository.Graphics = new GraphicsDeviceManager(this);
            inputManager = new InputManager(this);
            string content = GameConfiguationManager.Read("ContentDir");
            resourceManager = new ResourceManager(content, this);
            spriteManager = new SpriteManager(this);
            animationManager = new AnimationManager(this, GameConfiguationManager.Read("AnimationsDir"));
            spriteBatchManager = new SpriteBatchManager(this);
            particleSystemManager = new ParticleSystemManager(this);      
            cameraManager = new Camera2DManager(this, new Vector2(10f, 10f));
            unitsManager = new UnitsManager(this);
            levelManager = new LevelManager(this);
            guiManager = new GUIManager(this);
            battleManager = new BattleManager(this);
            cursorManager = new CursorManager(this);
            scriptManager = new ScriptManager(this, new List<Assembly>(), GameConfiguationManager.Read("CompileScript"));
            toolPanelManager = new ToolPanelManager(this);
            editorWindow = new EditorWindow(this);
            spriteBatchService = Services.GetService(typeof(ISpriteBatchService)) as ISpriteBatchService;
            SetEditorStyle();
        }

        #region protected

        protected override void Initialize()
        {
            Depository.Graphics.PreferredBackBufferWidth = 800;
            Depository.Graphics.PreferredBackBufferHeight = 600;
            IsMouseVisible = true;
            Depository.Graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            //spriteBatchService = Services.GetService(typeof(ISpriteBatchService)) as ISpriteBatchService;
            IsFixedTimeStep = false;

            Depository.Graphics.SynchronizeWithVerticalRetrace = true;
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
            spriteBatchService.DrawAll(gameTime);
        }

        #endregion

        ToolPanelManager toolPanelManager;

        #region private
        #region Services
        ISpriteBatchService spriteBatchService;
        #endregion
        private ResourceManager resourceManager;
        private UnitsManager unitsManager;
        private SpriteManager spriteManager;
        private AnimationManager animationManager;
        private ParticleSystemManager particleSystemManager;
        private LevelManager levelManager;
        private SpriteBatchManager spriteBatchManager;
        private InputManager inputManager;
        private GUIManager guiManager;
        private Camera2DManager cameraManager;
        private BattleManager battleManager;
        private CursorManager cursorManager;
        private ScriptManager scriptManager;
        private EditorWindow editorWindow;
        #endregion

        private void SetEditorStyle()
        {
            Form form = Control.FromHandle(Window.Handle) as Form;
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.MaximizeBox = false;
            form.MinimizeBox = false;
        }
    }
}
