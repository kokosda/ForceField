using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForceField.GameState.Services.Base;
using ForceField.GameState.Models;
using ForceField.GameState.Models.Base;

namespace ForceField.GameState.Services
{
    public class OptionsMenuScreenServiceImpl : MenuScreenService
    {
        public OptionsMenuScreenServiceImpl(ScreenManager screenManager) :
            base(screenManager)
        {
        }

        #region Initialize

        public override void  InitializeScreen(GameScreen menuscreen)
        {
            var optionsMenuScreen = menuscreen as OptionsMenuScreen;
            BindEvents(optionsMenuScreen);
        }

        #endregion

        #region Handle Input

        /// <summary>
        /// Event handler for when the Ungulate menu entry is selected.
        /// </summary>
        void UngulateMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            OptionsMenuScreen.CurrentUngulate++;

            if (OptionsMenuScreen.CurrentUngulate > OptionsMenuScreen.Ungulate.Llama)
                OptionsMenuScreen.CurrentUngulate = 0;

            ((sender as MenuEntry).MenuScreen as OptionsMenuScreen).SetMenuEntryText();
        }

        /// <summary>
        /// Event handler for when the Language menu entry is selected.
        /// </summary>
        void LanguageMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            OptionsMenuScreen.CurrentLanguage = (OptionsMenuScreen.CurrentLanguage + 1) % OptionsMenuScreen.Languages.Length;
            ((sender as MenuEntry).MenuScreen as OptionsMenuScreen).SetMenuEntryText();
        }

        /// <summary>
        /// Event handler for when the Frobnicate menu entry is selected.
        /// </summary>
        void FrobnicateMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            OptionsMenuScreen.Frobnicate = !OptionsMenuScreen.Frobnicate;
            ((sender as MenuEntry).MenuScreen as OptionsMenuScreen).SetMenuEntryText();
        }

        /// <summary>
        /// Event handler for when the Elf menu entry is selected.
        /// </summary>
        void ElfMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            OptionsMenuScreen.Elf++;
            ((sender as MenuEntry).MenuScreen as OptionsMenuScreen).SetMenuEntryText();
        }

        #endregion

        #region private

        private void BindEvents(OptionsMenuScreen optionsMenuScreen)
        {
            optionsMenuScreen.UngulateMenuEntry.Selected += UngulateMenuEntrySelected;
            optionsMenuScreen.LanguageMenuEntry.Selected += LanguageMenuEntrySelected;
            optionsMenuScreen.FrobnicateMenuEntry.Selected += FrobnicateMenuEntrySelected;
            optionsMenuScreen.ElfMenuEntry.Selected += ElfMenuEntrySelected;
            optionsMenuScreen.Back.Selected += OnCancel;
        }

        #endregion
    }
}
