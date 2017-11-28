using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForceField.GameState.Services;
using ForceField.GameState.Interfaces;
using ForceField.GameState.Services.Base;

namespace ForceField.GameState.Models
{
    public class OptionsMenuScreen : MenuScreen
    {
        public OptionsMenuScreen(OptionsMenuScreenServiceImpl service)
            : base(service, "Настройки", "Ungulate", "Language", "Frobnicate", "Elf", "Back")
        {
            SetMenuEntryText();

            this.service = service;
        }

        public void SetMenuEntryText()
        {
            UngulateMenuEntry.Text = "Preferred ungulate: " + CurrentUngulate;
            LanguageMenuEntry.Text = "Language: " + Languages[CurrentLanguage];
            FrobnicateMenuEntry.Text = "Frobnicate: " + (Frobnicate ? "on" : "off");
            ElfMenuEntry.Text = "elf: " + Elf;
        }

        #region Properties

        public override GameScreenService GameScreenServiceImpl
        {
            get
            {
                return service;
            }
        }

        public MenuEntry UngulateMenuEntry
        {
            get
            {
                if (ungulateMenuEntry == null)
                {
                    ungulateMenuEntry = MenuEntries[0];
                }
                return ungulateMenuEntry;
            }
        }

        public MenuEntry LanguageMenuEntry
        {
            get
            {
                if (languageMenuEntry == null)
                {
                    languageMenuEntry = MenuEntries[1];
                }

                return languageMenuEntry;
            }
        }

        public MenuEntry FrobnicateMenuEntry
        {
            get
            {
                if (frobnicateMenuEntry == null)
                {
                    frobnicateMenuEntry = MenuEntries[2];
                }

                return frobnicateMenuEntry;
            }
        }

        public MenuEntry ElfMenuEntry
        {
            get
            {
                if (elfMenuEntry == null)
                {
                    elfMenuEntry = MenuEntries[3];
                }

                return elfMenuEntry;
            }
        }

        public MenuEntry Back
        {
            get
            {
                return MenuEntries[4];
            }
        }

        #endregion

        #region Fields

        public enum Ungulate
        {
            BactrianCamel,
            Dromedary,
            Llama,
        }

        public static Ungulate CurrentUngulate = Ungulate.Dromedary;

        public static string[] Languages = { "C#", "French", "Deoxyribonucleic acid" };
        public static int CurrentLanguage = 0;

        public static bool Frobnicate = true;

        public static int Elf = 23;

        #endregion

        #region private

        #region members

        private MenuEntry ungulateMenuEntry;
        private MenuEntry languageMenuEntry;
        private MenuEntry frobnicateMenuEntry;
        private MenuEntry elfMenuEntry;

        private OptionsMenuScreenServiceImpl service;

        #endregion

        #endregion
    }
}
