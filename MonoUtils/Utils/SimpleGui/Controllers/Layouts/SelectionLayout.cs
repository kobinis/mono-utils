using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XnaUtils;
using XnaUtils.SimpleGui;

namespace XnaUtils.SimpleGui.Controllers.Layouts
{
    /// <summary>
    /// Only updates and draws one of the childeren
    /// </summary>
    class SelectionLayout:GuiControl
    {
        public int SelectedIndex;
        public RadioSelectionGroup RadioGroup;
        public override void Update(InputState inputState)
        {
            if (RadioGroup != null)
                SelectedIndex = RadioGroup.SelectedControlIndex;

            if (LogicFunction != null)
            {
                LogicFunction.Invoke(this, inputState);
            }
            if (Disable)
                return;

            if(SelectedIndex >=0 && SelectedIndex < children.Count)
            {
                children[SelectedIndex].Update(inputState);
                HalfSize = children[SelectedIndex].HalfSize;
            }
                        
            UpdateLogic(inputState);
        }

        public override void Draw(SpriteBatch sb, Color? color = null)
        {
            DrawLogic(sb, color);

            if (SelectedIndex >= 0 && SelectedIndex < children.Count)
            {
                children[SelectedIndex].Draw(sb, color);
                HalfSize = children[SelectedIndex].HalfSize;
            }            
        }

    }
}
