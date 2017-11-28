using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForceField.Core.Managers.Services;
using Microsoft.Xna.Framework;
using ForceField.Interfaces;

namespace ForceField.Core.Managers
{
    public class StateSaveManager : GameComponent
    {

        public StateSaveManager(Game game, float saveMillisecond)
            : base(game)
        {
            stateSave = new StateSaveService();
            game.Services.AddService(typeof(IStateSaveService), stateSave);
            millisecond = saveMillisecond;
            acumTime = 0f;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public void SetArrayData(object[] array)
        {

        }

        public override void Update(GameTime gameTime)
        {
            acumTime += gameTime.ElapsedGameTime.Milliseconds;

            if (acumTime > millisecond)
            {
                acumTime = 0f;
                if (!stateSave.Worker.IsBusy && stateSave.SaveGame)
                {
                    stateSave.SaveAll();
                }
            }

            base.Update(gameTime);
        }

        private float acumTime;
        private float millisecond;
        private StateSaveService stateSave;
        private Game game;
    }
}
