using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace XnaUtils.Input
{
    public enum MouseButtons
    {
        None,
        LeftButton,
        MiddleButton,
        RightButton,
        XButton1,
        XButton2,
    }

    public class MouseManager
    {       
        public MouseState LastMouseState { get; set; }
        public MouseState CurMouseState { get; set; }
        public Vector2 Position => new Vector2(CurMouseState.X, CurMouseState.Y);
        public MouseManager()
        {
            LastMouseState = Mouse.GetState();
            CurMouseState = Mouse.GetState();
        }

        public bool IsMousePressed(MouseButtons mouseButton)
        {
            return IsMouseButtonsDown(CurMouseState, mouseButton) && !IsMouseButtonsDown(LastMouseState, mouseButton);
        }

        public bool IsMouseReleased(MouseButtons mouseButton)
        {
            return !IsMouseButtonsDown(CurMouseState, mouseButton) && IsMouseButtonsDown(LastMouseState, mouseButton);
        }

        public bool IsMouseDown(MouseButtons mouseButton)
        {            
            return IsMouseButtonsDown(CurMouseState, mouseButton);
        }                
        
        public void Update()
        {
            LastMouseState = CurMouseState;
            CurMouseState =  Mouse.GetState();
        } 
       
        public int GetDScroolWheel()
        {
            return CurMouseState.ScrollWheelValue - LastMouseState.ScrollWheelValue;
        }

        public bool HasMouseStateChanged()
        {
            return CurMouseState != LastMouseState;
            //return true;
        }

        public static bool IsMouseButtonsDown(MouseState mouseState, MouseButtons mouseButton)
        {
            switch (mouseButton)
            {
                case MouseButtons.None:
                    return false;
                case MouseButtons.LeftButton:
                    return mouseState.LeftButton == ButtonState.Pressed;
                case MouseButtons.MiddleButton:
                    return mouseState.MiddleButton == ButtonState.Pressed;
                case MouseButtons.RightButton:
                    return mouseState.RightButton == ButtonState.Pressed;
                case MouseButtons.XButton1:
                    return mouseState.XButton1 == ButtonState.Pressed;
                case MouseButtons.XButton2:
                    return mouseState.XButton2 == ButtonState.Pressed;
            }
            return false;
        }

    }
}
