using Microsoft.Xna.Framework;
using ForceField.Domain;

namespace ForceField.Interfaces
{
    public interface ICamera2DService
    {
        Matrix Matrix { get; set; }
        CameraData Data { get; set; }

        void MoveToLeft();
        void MoveToTop();
        void MoveToBottom();
        void MoveToRight();

        void SetRotation(float angle);

        void SetAngleRotation(float angle);
        void SetPosition(float x, float y);
        void SetSpeedOffset(float x, float y);
    }
}
