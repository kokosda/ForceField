using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Windows.Forms;
using System.Configuration;

namespace ForceField.Core
{
    public static class Depository
    {
        #region Properties

        public static GraphicsDeviceManager Graphics
        {
            get { return graphics; }
            set { graphics = value; }
        }

        public static Form MainForm
        {
            get { return mainForm; }
            set { mainForm = value; }
        }

        public static GraphicsAdapter Adapter
        {
            get { return adapter; }
            set { adapter = value; }
        }

        public static GraphicsDevice GD
        {
            get { return gd; }
            set { gd = value; }
        }

        public static ContentManager Content
        {
            get { return content; }
            set { content = value; }
        }

        public static MouseState MouseState
        {
            get { return mouseState; }
            set { mouseState = value; }
        }

        public static KeyboardState KeyboardState
        {
            get { return keyboardState; }
            set { keyboardState = value; }
        }

        public static MouseState PreviousMouseState
        {
            get { return previousMouseState; }
            set { previousMouseState = value; }
        }

        public static KeyboardState PreviousKeyboardState
        {
            get { return previousKeyboardState; }
            set { previousKeyboardState = value; }
        }

        public static bool FormActive
        {
            get { return formActive; }
            set { formActive = value; }
        }

        public static Point PreviousMouseGlobalPosition
        {
            get { return previousMouseGlobalPosition; }
            set { previousMouseGlobalPosition = value; }
        }

        public static Point MouseGlobalPosition
        {
            get { return mouseGlobalPosition; }
            set { mouseGlobalPosition = value; }
        }

        public static Rectangle DrawViewport
        {
            get { return drawViewport; }
            set { drawViewport = value; }
        }

        public static int ScreenHeight
        {
            get { return screenHeight; }
            set { screenHeight = value; }
        }

        public static int ScreenWidth
        {
            get { return screenWidth; }
            set { screenWidth = value; }
        }

        public static double Elapsed
        {
            get { return elapsed; }
            set { elapsed = value; }
        }

        public static bool CanSelect
        {
            get { return canSelect; }
            set { canSelect = value; }
        }

        public static float NowSelectedPlan
        {
            get { return nowSelectedPlan; }
            set { nowSelectedPlan = value; }
        }

        public static int UnitSelectedNow
        {
            get { return unitSelectedNow; }
            set { unitSelectedNow = value; }
        }

        public static int ExSelectedNow
        {
            get { return exSelectedNow; }
            set { exSelectedNow = value; }
        }

        public static int MouseWasX
        {
            get { return mouseWasX; }
            set { mouseWasX = value; }
        }

        public static int MouseWasY
        {
            get { return mouseWasY; }
            set { mouseWasY = value; }
        }


        public static string[] TexturesFolders = { ConfigurationManager.AppSettings.Get("TexturesFolders") };

        public static string[] SoundsFolders = { ConfigurationManager.AppSettings.Get("SoundsFolders") };

        public static string[] EffectsFolders = { ConfigurationManager.AppSettings.Get("EffectsFolders") };

        public static string[] ModelsFolders = { ConfigurationManager.AppSettings.Get("ModelsFolders") };

        public static string[] FontsFolders = { ConfigurationManager.AppSettings.Get("FontsFolders") };

        public static string[] BinariesFolders = { ConfigurationManager.AppSettings.Get("BinariesFolders") };

        #endregion

        #region private

        private static GraphicsDeviceManager graphics;
        private static Form mainForm;
        private static GraphicsAdapter adapter;
        private static GraphicsDevice gd;
        private static ContentManager content;
        private static MouseState mouseState;
        private static KeyboardState keyboardState;
        private static MouseState previousMouseState;
        private static KeyboardState previousKeyboardState;
        private static bool formActive = true;
        private static Point mouseGlobalPosition;
        private static Point previousMouseGlobalPosition;
        private static Rectangle drawViewport = new Rectangle(0, 0, 0, 0);
        private static int screenHeight;
        private static int screenWidth;
        private static double elapsed;
        private static bool canSelect = true;
        private static float nowSelectedPlan = 0.0f;
        private static int unitSelectedNow = -1;
        private static int exSelectedNow = -1;
        private static int mouseWasX;
        private static int mouseWasY;

        #endregion
    }
}