using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForceField.GameState.Services.Base;
using ForceField.GameState.Models;
using ForceField.GameState.Models.Base;

namespace ForceField.GameState.Services
{
    public class PauseMenuScreenServiceImpl : MenuScreenService
    {
        public PauseMenuScreenServiceImpl(ScreenManager screenManager) :
            base(screenManager)
        {
        }

        #region Initialize

        public override void InitializeScreen(GameScreen menuscreen)
        {
            var pauseMenuScreen = menuscreen as PauseMenuScreen;
            BindEvents(pauseMenuScreen);
        }

        #endregion

        #region Handle Input

        /// <summary>
        /// Event handler for when the Quit Game menu entry is selected.
        /// </summary>
        void QuitGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            const string message = "Закончить игру?";

            MessageBoxScreen confirmQuitMessageBox = new MessageBoxScreen(ScreenManager.MessageBoxScreenService, message);

            confirmQuitMessageBox.Accepted += ConfirmQuitMessageBoxAccepted;

            ScreenManager.AddScreen(confirmQuitMessageBox, confirmQuitMessageBox.ControllingPlayer);
        }


        /// <summary>
        /// Event handler for when the user selects ok on the "are you sure
        /// you want to quit" message box. This uses the loading screen to
        /// transition from the game back to the main menu screen.
        /// </summary>
        void ConfirmQuitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            var loadingScreen = new LoadingScreen(ScreenManager.LoadingScreenService, 
                                                    false, 
                                                    new BackgroundScreen(ScreenManager.BackgroundScreenService), 
                                                    new MainMenuScreen(ScreenManager.MainMenuScreenService));

            LoadingScreenServiceImpl.Load(this, loadingScreen, e.PlayerIndex);
        }
        
        #endregion
        
        #region private

        private void BindEvents(PauseMenuScreen pauseMenuScreen)
        {
            pauseMenuScreen.ResumeGameMenuEntry.Selected += OnCancel;
            pauseMenuScreen.QuitGameMenuEntry.Selected += QuitGameMenuEntrySelected;
        }

        #endregion
    }
}
