using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XnaUtils.SimpleGui;

namespace SolarConflict.XnaUtils.SimpleGui.Controllers
{
    /// <summary>
    /// A Gui Control that allows drawing only withing the control
    /// </summary>
    class ScissorControl:GuiControl
    {
        RasterizerState _rasterizerState = new RasterizerState() { ScissorTestEnable = true };



        public override void Draw(SpriteBatch sb, Color? color = null)
        {
            sb.End();
            sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend,
                      null, null, _rasterizerState);

            //Copy the current scissor rect so we can restore it after
            Rectangle currentRect = sb.GraphicsDevice.ScissorRectangle;

            //Set the current scissor rectangle
            sb.GraphicsDevice.ScissorRectangle = this.GetRectangle();

            //Draw the text at the top left of the scissor rectangle

            foreach (var guiControl in children)
            {
                guiControl.Draw(sb, color);
            }

            //End the spritebatch
            sb.End();
            sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
        }
    }
}
