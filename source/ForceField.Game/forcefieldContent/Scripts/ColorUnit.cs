using Forcefield.Core;
using Forcefield.Interface;
using Microsoft.Xna.Framework;
using Forcefield.

public class ColorUnit : IScript
{
	public ColorUnit()
	{
	
	}

	public void Activate(GameTime gameTime, double time, params object[] args)
	{
	
	}

	public void Update(params object[] args)
	{
		Unit unit = args[0] as Unit;
		
		unit.Sprite.Color = new Color(255,0,0,0);
	}
}