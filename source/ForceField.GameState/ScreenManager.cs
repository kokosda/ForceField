using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Content;
using ForceField.GameState.Models.Base;
using ForceField.GameState.Services;
using ForceField.Interfaces;
using ForceField.Core.Managers;
using ForceField.Core.Service;
using ForceField.UI.UIElements;

namespace ForceField.GameState
{

    public class ScreenManager : DrawableGameComponent
    {

        public ScreenManager(Game game, string version)
            : base(game)
        {

            TouchPanel.EnabledGestures = GestureType.None;
            screenFactory = new ScreenFactory();

            Game.Components.Add(this);
            input = game.Services.GetService(typeof(IUserInputService)) as InputService;
            Version = version;



            ConstructScreenServices();
        }

        #region Properties
        
        /// <summary>
        /// A default SpriteBatch shared by all the screens. This saves
        /// each screen having to bother creating their own local instance.
        /// </summary>
        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
        }

        public string Version;

        /// <summary>
        /// A default font shared by all the screens. This saves
        /// each screen having to bother loading their own local copy.
        /// </summary>
        public SpriteFont Font
        {
            get { return font; }
        }
        
        /// <summary>
        /// If true, the manager prints out a list of all the screens
        /// each time it is updated. This can be useful for making sure
        /// everything is being added and removed at the right times.
        /// </summary>
        public bool TraceEnabled
        {
            get { return traceEnabled; }
            set { traceEnabled = value; }
        }
        
        #endregion

        #region Initialization
        
        /// <summary>
        /// Initializes the screen manager component.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            battleScreenService.SetGameplayServices();
            battleScreenService.SetGUIService();

            globalMapScreenService.SetGameplayServices();
            globalMapScreenService.SetGUIService();

            isInitialized = true;

            Game.Services.AddService(typeof(IScreenFactory), screenFactory);
        }
        
        /// <summary>
        /// Load your graphics content.
        /// </summary>
        protected override void LoadContent()
        {
            var resourceManager = Game.Components.Where(p => p.GetType() == typeof(ResourceManager)).First() as ResourceManager;
            ContentManager content = Game.Content;

            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = resourceManager.Fonts.GetByName("menu").Data;
            //blankTexture = resourceManager.Textures.GetByName("blank").Data;

            foreach (GameScreen screen in screens)
            {
                screen.GameScreenServiceImpl.Activate(screen, false);
                screen.GameScreenServiceImpl.InitializeScreen(screen);
            }
        }
        
        /// <summary>
        /// Unload your graphics content.
        /// </summary>
        protected override void UnloadContent()
        {
            // Tell each of the screens to unload their content.
            foreach (GameScreen screen in screens)
            {
                screen.GameScreenServiceImpl.Unload(screen);
            }
        }
        
        #endregion

        #region ScreenServices

        public BackgroundScreenServiceImpl BackgroundScreenService
        {
            get
            {
                return backgroundScreenService;
            }
        }

        public GlobalMapScreenServiceImpl GlobalMapScreenService
        {
            get
            {
                return globalMapScreenService;
            }
        }

        public MainMenuScreenServiceImpl MainMenuScreenService
        {
            get
            {
                return mainMenuScreenService;
            }
        }

        public MessageBoxScreenServiceImpl MessageBoxScreenService
        {
            get
            {
                return messageBoxScreenService;
            }
        }

        public OptionsMenuScreenServiceImpl OptionsMenuScreenService
        {
            get
            {
                return optionsMenuScreenService;
            }
        }

        public PauseMenuScreenServiceImpl PauseMenuScreenService
        {
            get
            {
                return pauseMenuScreenService;
            }
        }

        public LoadingScreenServiceImpl LoadingScreenService
        {
            get
            {
                return loadingScreenService;
            }
        }

        public BattleScreenServiceImpl BattleScreenService
        {
            get
            {
                return battleScreenService;
            }
        }
        
        #endregion

        #region Update and Draw

        /// <summary>
        /// Allows each screen to run logic.
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            // Read the keyboard and gamepad.
            input.Update();

            // Make a copy of the master screen list, to avoid confusion if
            // the process of updating one screen adds or removes others.
            tempScreensList.Clear();

            foreach (GameScreen screen in screens)
            {
                tempScreensList.Add(screen);
            }

            bool otherScreenHasFocus = !Game.IsActive;
            bool coveredByOtherScreen = false;

            // Loop as long as there are screens waiting to be updated.
            while (tempScreensList.Count > 0)
            {
                // Pop the topmost screen off the waiting list.
                GameScreen screen = tempScreensList[tempScreensList.Count - 1];

                tempScreensList.RemoveAt(tempScreensList.Count - 1);

                // Update the screen.
                screen.GameScreenServiceImpl.Update(screen, gameTime, otherScreenHasFocus, coveredByOtherScreen);

                if (screen.ScreenState == ScreenState.TransitionOn ||
                    screen.ScreenState == ScreenState.Active)
                {
                    // If this is the first active screen we came across,
                    // give it a chance to handle input.
                    if (!otherScreenHasFocus)
                    {
                        screen.GameScreenServiceImpl.HandleInput(screen, gameTime, input);

                        otherScreenHasFocus = true;
                    }

                    // If this is an active non-popup, inform any subsequent
                    // screens that they are covered by it.
                    if (!screen.IsPopup)
                    {
                        coveredByOtherScreen = true;
                    }
                }
                
            }

            // Print debug trace?
            if (traceEnabled)
            {
                TraceScreens();
            }
        }
        
        /// <summary>
        /// Prints a list of all the screens, for debugging.
        /// </summary>
        void TraceScreens()
        {
            List<string> screenNames = new List<string>();

            foreach (GameScreen screen in screens)
            {
                screenNames.Add(screen.GetType().Name);
            }

            Debug.WriteLine(string.Join(", ", screenNames.ToArray()));
        }
        
        /// <summary>
        /// Tells each screen to draw itself.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            foreach (GameScreen screen in screens)
            {
                if (screen.ScreenState == ScreenState.Hidden)
                {
                    continue;
                }

                screen.GameScreenServiceImpl.Draw(screen, gameTime);
            }
        }
        
        #endregion

        #region Public Methods
        
        /// <summary>
        /// Adds a new screen to the screen manager.
        /// </summary>
        public void AddScreen(GameScreen screen, PlayerIndex? controllingPlayer)
        {
            screen.ControllingPlayer = controllingPlayer;
            screen.IsExiting = false;

            // If we have a graphics device, tell the screen to load content.
            if (isInitialized)
            {
                screen.GameScreenServiceImpl.Activate(screen, false);
                screen.GameScreenServiceImpl.InitializeScreen(screen);
            }

            screens.Add(screen);

            // update the TouchPanel to respond to gestures this screen is interested in
            TouchPanel.EnabledGestures = screen.EnabledGestures;
        }
        
        /// <summary>
        /// Removes a screen from the screen manager. You should normally
        /// use GameScreen.ExitScreen instead of calling this directly, so
        /// the screen can gradually transition off rather than just being
        /// instantly removed.
        /// </summary>
        public void RemoveScreen(GameScreen screen)
        {
            // If we have a graphics device, tell the screen to unload content.
            if (isInitialized)
            {
                screen.GameScreenServiceImpl.Unload(screen);
            }

            screens.Remove(screen);
            tempScreensList.Remove(screen);

            // if there is a screen still in the manager, update TouchPanel
            // to respond to gestures that screen is interested in.
            if (screens.Count > 0)
            {
                TouchPanel.EnabledGestures = screens[screens.Count - 1].EnabledGestures;
            }
        }
        
        /// <summary>
        /// Expose an array holding all the screens. We return a copy rather
        /// than the real master list, because screens should only ever be added
        /// or removed using the AddScreen and RemoveScreen methods.
        /// </summary>
        public GameScreen[] GetScreens()
        {
            return screens.ToArray();
        }
        
        /// <summary>
        /// Helper draws a translucent black fullscreen sprite, used for fading
        /// screens in and out, and for darkening the background behind popups.
        /// </summary>
        public void FadeBackBufferToBlack(float alpha)
        {
            //spriteBatch.Begin();
            //spriteBatch.Draw(blankTexture, GraphicsDevice.Viewport.Bounds, Color.Black * alpha);
            //spriteBatch.End();
        }

        /// <summary>
        /// Informs the screen manager to serialize its state to disk.
        /// </summary>
        public void Deactivate()
        {
            return;
        }

        public bool Activate(bool instancePreserved)
        {
            
            return false;
        }

        #endregion

        #region private

        #region members

        private const string StateFilename = "ScreenManagerState.xml";

        List<GameScreen> screens = new List<GameScreen>();
        List<GameScreen> tempScreensList = new List<GameScreen>();

        public InputService input = new InputService();

        SpriteBatch spriteBatch;
        SpriteFont font;

        bool isInitialized;

        bool traceEnabled = true;

        BackgroundScreenServiceImpl backgroundScreenService;
        MainMenuScreenServiceImpl mainMenuScreenService;
        MessageBoxScreenServiceImpl messageBoxScreenService;
        OptionsMenuScreenServiceImpl optionsMenuScreenService;
        PauseMenuScreenServiceImpl pauseMenuScreenService;
        LoadingScreenServiceImpl loadingScreenService;
        BattleScreenServiceImpl battleScreenService;
        GlobalMapScreenServiceImpl globalMapScreenService;
        ScreenFactory screenFactory;

        #endregion

        private void ConstructScreenServices()
        {
            backgroundScreenService = new BackgroundScreenServiceImpl(this);
            mainMenuScreenService = new MainMenuScreenServiceImpl(this);
            messageBoxScreenService = new MessageBoxScreenServiceImpl(this);
            optionsMenuScreenService = new OptionsMenuScreenServiceImpl(this);
            pauseMenuScreenService = new PauseMenuScreenServiceImpl(this);
            loadingScreenService = new LoadingScreenServiceImpl(this);
            battleScreenService = new BattleScreenServiceImpl(this);
            globalMapScreenService = new GlobalMapScreenServiceImpl(this);
        }

        #endregion
    }
}
