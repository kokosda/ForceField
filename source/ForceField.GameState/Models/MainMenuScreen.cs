using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForceField.GameState.Models.Base;
using ForceField.GameState.Services;
using ForceField.GameState.Interfaces;
using ForceField.GameState.Services.Base;

namespace ForceField.GameState.Models
{
    public class MainMenuScreen : MenuScreen
    {
        public MainMenuScreen(MainMenuScreenServiceImpl service)
            : base(service, "Главное меню", "Начать?", "Настройки", "Выйти")
        {
            this.service = service;
        }

        #region Properties

        public override GameScreenService GameScreenServiceImpl
        {
            get
            {
                return service;
            }
        }

        public MenuEntry Play
        {
            get
            {
                return MenuEntries[0];
            }
        }

        public MenuEntry Settings
        {
            get
            {
                return MenuEntries[1];
            }
        }

        public MenuEntry Exit
        {
            get
            {
                return MenuEntries[2];
            }
        }

        public MenuEntry GetByName(string entryText)
        {
            return MenuEntries.FirstOrDefault(p => p.Text == entryText);
        }

        #endregion

        #region private

        MainMenuScreenServiceImpl service;

        #endregion
    }
}
