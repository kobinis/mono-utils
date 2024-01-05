using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XnaUtils.Input;

namespace XnaUtils.SimpleGui
{
    /// <summary>
    /// Used to make a group of control act as a radio selection (only one can be selected)
    /// </summary>
    [Serializable]
    public class RadioSelectionGroup
    {
        int selectedControlIndex = 0;
        List<GuiControl> controls;


        public int SelectedControlIndex { get { return selectedControlIndex; } set { selectedControlIndex = value; Update(); } }
       
        public GuiControl SelectedControl {
            get { return controls[selectedControlIndex]; }
            set
            {
                for (int i = 0; i < controls.Count; i++)
                {
                    if (value == controls[i])
                    {
                        selectedControlIndex = i;
                        return;
                    }                    
                }                
            }
        }

        public GuiControl GetControl(int index)
        {
            return controls[index];
        }

        public RadioSelectionGroup()
        {
            controls = new List<GuiControl>();
        }

        public void AddControl(GuiControl control)
        {
            controls.Add(control);            
            control.Action += ControlSelectedHandler;
            Update();
        }

      

        public void AddControlCursorOn(GuiControl control)
        {
            controls.Add(control);
            control.CursorOn += ControlSelectedHandler;
            Update();
        }

        /// <summary>
        /// Optional update do control the menu with actions
        /// </summary>
        /// <param name="input"></param>
        public void InputUpdate(InputState input)
        {
            if(input.IsActionStart(ActionTypes.UiDown) && selectedControlIndex< controls.Count -1)
            {
                selectedControlIndex++;
                SelectedControl = controls[selectedControlIndex];
                Update();                
            }

            if (input.IsActionStart(ActionTypes.UiUp) && selectedControlIndex > 0)
            {
                selectedControlIndex--;
                SelectedControl = controls[selectedControlIndex];
                Update();
            }

            if (input.IsActionStart(ActionTypes.Approve))
            {
                SelectedControl.InvokeAction(new CursorInfo());                
            }

            if (SelectedControl != null)
            {
                var cur = new CursorInfo();
                cur.Position = SelectedControl.Position + SelectedControl.HalfSize;
                SelectedControl.IvokeCursorOn(cur);
            }
        }

        private void ControlSelectedHandler(GuiControl source, CursorInfo cursorLocation)
        {
            if (cursorLocation.Position != cursorLocation.PreviousPosition || cursorLocation.IsPressedLeft || cursorLocation.IsPressedRight)
            {
                selectedControlIndex = source.Index;
               
            }
            Update();
            //fire event
        }

        public void Update()
        {
            for (int i = 0; i < controls.Count; i++)
            {
                controls[i].Index = i; //???
                if(selectedControlIndex == i)
                {
                    controls[i].IsPressed = true;
                }
                else
                {
                    controls[i].IsPressed = false;
                }
            }
        }
        
    }
}
