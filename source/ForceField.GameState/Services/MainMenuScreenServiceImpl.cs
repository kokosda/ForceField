using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ForceField.GameState.Services.Base;
using ForceField.GameState.Models;
using ForceField.GameState.Models.Base;
using ForceField.Core.Managers;
using ForceField.GameState.Interfaces;

namespace ForceField.GameState.Services
{
    public class MainMenuScreenServiceImpl : MenuScreenService, IGameScreenService
    {
        /// <summary>
        /// Constructor fills in the menu contents.
        /// </summary>
        public MainMenuScreenServiceImpl(ScreenManager screenManager)
            : base(screenManager)
        {
        }

        #region Intialize

        public override void InitializeScreen(GameScreen menuScreen)
        {
            var mainMenuScreen = menuScreen as MainMenuScreen;
            BindEvents(mainMenuScreen);
        }
        
        #endregion

        #region Handle Input

        /// <summary>
        /// Event handler for when the Play Game menu entry is selected.
        /// </summary>
        void PlayGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            var loadingScreen = new LoadingScreen(ScreenManager.LoadingScreenService, true, new GlobalMapScreen(ScreenManager.GlobalMapScreenService));
            LoadingScreenServiceImpl.Load(this, loadingScreen, e.PlayerIndex);
        }
        
        /// <summary>
        /// Event handler for when the Options menu entry is selected.
        /// </summary>
        void OptionsMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new OptionsMenuScreen(ScreenManager.OptionsMenuScreenService), e.PlayerIndex);
        }
        
        /// <summary>
        /// When the user cancels the main menu, ask if they want to exit the sample.
        /// </summary>
        protected override void OnCancel(MenuScreen menuScreen, PlayerIndex playerIndex)
        {
            const string message = "Вы уверены, что хотите выйти из игры?";

            MessageBoxScreen confirmExitMessageBox = new MessageBoxScreen(ScreenManager.MessageBoxScreenService ,message);
            confirmExitMessageBox.Accepted += ConfirmExitMessageBoxAccepted;
            ScreenManager.AddScreen(confirmExitMessageBox, playerIndex);
        }
        
        /// <summary>
        /// Event handler for when the user selects ok on the "are you sure
        /// you want to exit" message box.
        /// </summary>
        void ConfirmExitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.Game.Exit();
        }
        
        #endregion

        #region private

        private void BindEvents(MainMenuScreen mainMenuScreen)
        {
            mainMenuScreen.Play.Selected += PlayGameMenuEntrySelected;
            mainMenuScreen.Settings.Selected += OptionsMenuEntrySelected;
            mainMenuScreen.Exit.Selected += OnCancel;
        }

        #endregion
    }
}
