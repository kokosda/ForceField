using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForceField.Interfaces;
using ForceField.Domain;
using Microsoft.Xna.Framework;

namespace ForceField.Core.Managers.Services
{
    public class Camera2DService : ICamera2DService
    {
        public CameraData Data { get; set; }
        public Matrix Matrix { get; set; }

        public Camera2DService(CameraData Data)
        {
            this.Data = Data;
            Data.Translation = new Vector2(100, 100);
            Matrix = Matrix.Identity;
            Data.Angle = 0f;
        }


        public void MoveToLeft()
        {
            Data.IsUpdate = true;
            Data.Translation.X += Data.Offset.X;
        }

        public void MoveToTop()
        {
            Data.IsUpdate = true;
            Data.Translation.Y += Data.Offset.Y;
        }

        public void MoveToBottom()
        {
            Data.IsUpdate = true;
            Data.Translation.Y -= Data.Offset.Y;
        }

        public void MoveToRight()
        {
            Data.IsUpdate = true;
            Data.Translation.X -= Data.Offset.X;
        }

        public void SetRotation(float angle)
        {
            Data.Angle = angle;
        }

        public void SetAngleRotation(float angle)
        {
            Data.Angle = angle;
        }

        public void SetPosition(float x, float y)
        {
            Data.IsNewPosition = true;
            Data.OldTranslation = Data.Translation;
            Data.IsUpdate = true;
            Data.Translation.X = x;
            Data.Translation.Y = y;
        }

        public void SetSpeedOffset(float x, float y)
        {
            Data.Offset.X = x;
            Data.Offset.Y = y;
        }
    }
}
