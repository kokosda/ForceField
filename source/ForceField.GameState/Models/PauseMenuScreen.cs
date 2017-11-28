using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForceField.GameState.Services;
using ForceField.GameState.Interfaces;
using ForceField.GameState.Services.Base;

namespace ForceField.GameState.Models
{
    public class PauseMenuScreen : MenuScreen
    {
        public PauseMenuScreen(PauseMenuScreenServiceImpl service)
            : base(service, "Пауза", "Вернуться", "Выйти")
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

        public MenuEntry ResumeGameMenuEntry
        {
            get
            {
                return MenuEntries[0];
            }
        }

        public MenuEntry QuitGameMenuEntry
        {
            get
            {
                return MenuEntries[1];
            }
        }

        #endregion

        #region private

        PauseMenuScreenServiceImpl service;

        #endregion
    }
}