using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ForceField.Interfaces;
using ForceField.Core.Managers.Services;
using ForceField.Domain;
using Microsoft.Xna.Framework.Graphics;

namespace ForceField.Core.Managers
{
    public class Camera2DManager : IGameComponent
    {
        public Game game;

        public Camera2DManager(Game game, Vector2 offset)
        {
            game.Components.Add(this);
            camera = new Camera2DService(new CameraData() { Offset = offset, Angle = 0f, Translation = new Vector2(0,0)  });
            game.Services.AddService(typeof(ICamera2DService), camera);
            this.game = game;
        }

        public void ChangeParameter(CameraData Data)
        {
            camera.Data = Data;
        }

        public void Initialize()
        {

        }

        public void SetGameArea(int width, int height, Viewport view)
        {
            float scale = Math.Min(view.Width / width, view.Height / height);
            camera.Matrix *= Matrix.CreateScale(scale, scale, 1f) * Matrix.CreateTranslation((view.Width - (width * scale)) / 2,
                                                                   (view.Height - (height * scale)) / 2,
                                                                   0f);
        }

        private ICamera2DService camera;
    }
}
