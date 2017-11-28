using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ForceField.GameState.Models.Base;
using ForceField.GameState.Services.Base;
using ForceField.GameState.Services;
using ForceField.GameState.Interfaces;

namespace ForceField.GameState.Models
{
    public class MessageBoxScreen : GameScreen
    {
        public MessageBoxScreen(MessageBoxScreenServiceImpl service, string message) :
            this(service, message, true)
        {
            this.service = service;
        }

        public MessageBoxScreen(MessageBoxScreenServiceImpl service, string message, bool includeUsageText) :
            base(service)
        {
            const string usageText = "\nA button, Space, Enter = ok" +
                                     "\nB button, Esc = cancel";

            if (includeUsageText)
            {
                this.message = message + usageText;
            }
            else
            {
                this.message = message;
            }

            TransitionOnTime = TimeSpan.FromSeconds(0.2);
            TransitionOffTime = TimeSpan.FromSeconds(0.2);

            menuSelect = new InputAction(
                new Buttons[] { Buttons.A, Buttons.Start },
                new Keys[] { Keys.Space, Keys.Enter },
                null,
                true);
            menuCancel = new InputAction(
                new Buttons[] { Buttons.B, Buttons.Back },
                new Keys[] { Keys.Escape, Keys.Back },
                null,
                true);
        }

        public void OnAccepted(MessageBoxScreen screen, object sender, PlayerIndexEventArgs e)
        {
            if (screen.Accepted != null)
            {
                screen.Accepted.Invoke(sender, e);
            }
        }

        public void OnCancelled(MessageBoxScreen screen, object sender, PlayerIndexEventArgs e)
        {
            if (screen.Cancelled != null)
            {
                screen.Cancelled.Invoke(sender, e);
            }
        }

        #region Events

        public event EventHandler<PlayerIndexEventArgs> Accepted;
        public event EventHandler<PlayerIndexEventArgs> Cancelled;

        #endregion

        #region Properties

        public override GameScreenService GameScreenServiceImpl
        {
            get
            {
                return service;
            }
        }

        public string Message
        {
            get
            {
                return message;
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

        public Texture2D GradientTexture
        {
            get
            {
                return gradientTexture;
            }
            set
            {
                gradientTexture = value;
            }
        }

        #endregion

        #region private

        string message;
        Texture2D gradientTexture;

        InputAction menuSelect;
        InputAction menuCancel;

        MessageBoxScreenServiceImpl service;

        #endregion
    }
}
