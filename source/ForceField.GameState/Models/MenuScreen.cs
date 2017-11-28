using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using ForceField.GameState.Models.Base;
using ForceField.GameState.Services.Base;
using ForceField.GameState.Interfaces;
using ForceField.Domain.Input;

namespace ForceField.GameState.Models
{
    public abstract class MenuScreen : GameScreen
    {
        public MenuScreen(MenuScreenService service, string menuTitle, params string[] entryTitles) :
            base(service)
        {
            menuEntries = new List<MenuEntry>();

            for (var i = 0; i < entryTitles.Length; i++)
            {
                MenuEntry entry = new MenuEntry(entryTitles[i], this);
                menuEntries.Add(entry);
            }

            this.menuTitle = menuTitle;

            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            menuUp = new InputAction(
                new Buttons[] { Buttons.DPadUp, Buttons.LeftThumbstickUp },
                new Keys[] { Keys.Up },
                new MouseScroll[] { MouseScroll.MouseScrollUp },
                true);
            menuDown = new InputAction(
                new Buttons[] { Buttons.DPadDown, Buttons.LeftThumbstickDown },
                new Keys[] { Keys.Down },
                new MouseScroll[] { MouseScroll.MouseScrollDown },
                true);
            menuSelect = new InputAction(
                new Buttons[] { Buttons.A, Buttons.Start },
                new Keys[] { Keys.Enter, Keys.Space },
                null,
                true);
            menuCancel = new InputAction(
                new Buttons[] { Buttons.B, Buttons.Back },
                new Keys[] { Keys.Escape },
                null,
                true);

            this.service = service;
        }

        public override GameScreenService GameScreenServiceImpl
        {
            get
            {
                return service;
            }
        }

        #region Properties

        /// <summary>
        /// Gets the list of menu entries, so derived classes can add
        /// or change the menu contents.
        /// </summary>
        public IList<MenuEntry> MenuEntries
        {
            get
            {
                return menuEntries;
            }
        }

        public string MenuTitle
        {
            get
            {
                return menuTitle;
            }
        }

        public InputAction MenuUp
        {
            get
            {
                return menuUp;
            }
        }

        public InputAction MenuDown
        {
            get
            {
                return menuDown;
            }
        }

        public InputAction MenuSelect
        {
            get
            {
                return menuSelect;
            }
        }

        public InputAction MenuCancel
        {
            get
            {
                return menuCancel;
            }
        }

        public int SelectedEntry
        {
            get
            {
                return selectedEntry;
            }
            set
            {
                selectedEntry = value;
            }
        }

        #endregion

        #region private

        IList<MenuEntry> menuEntries;

        int selectedEntry = 0;
        string menuTitle;

        InputAction menuUp;
        InputAction menuDown;
        InputAction menuSelect;
        InputAction menuCancel;

        MenuScreenService service;

        #endregion
    }
}
