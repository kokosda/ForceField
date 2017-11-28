using Microsoft.Xna.Framework;

namespace ForceField.Interfaces
{
    public interface IGUIService
    {
        void AddComponentService(IUIComponentService componentService);

        void DeleteComponentService(IUIComponentService componentService);

        void ClearComponentServices();

        void Update(GameTime gameTime);

        void Draw(GameTime gameTime);
    }
}
