using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForceField.GameState.Models.Base;
using ForceField.GameState.Services;
using ForceField.GameState.Services.Base;

namespace ForceField.GameState.Models
{
    public class LoadingScreen : GameScreen
    {
        public LoadingScreen(GameScreenService service, bool loadingIsSlow, params GameScreen[] screensToLoad) :
            base(service)
        {
            this.loadingIsSlow = loadingIsSlow;
            this.screensToLoad = screensToLoad;

            TransitionOnTime = TimeSpan.FromSeconds(0.5);
        }

        #region Properties

        public bool LoadinIsSlow
        {
            get
            {
                return loadingIsSlow;
            }
        }

        public bool OtherScreensAreGone
        {
            get
            {
                return otherScreensAreGone;
            }
            set
            {
                otherScreensAreGone = value;
            }
        }

        public GameScreen[] ScreensToLoad
        {
            get
            {
                return screensToLoad;
            }
        }

        #endregion

        #region private

        bool loadingIsSlow;
        bool otherScreensAreGone;

        GameScreen[] screensToLoad;
        #endregion
    }
}
