using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XnaUtils.Input
{
    public class GamepadManager
    {
        
        public PlayerIndex Index { get; set; }
        public GamePadState LastGamepadState { get; set; }
        public GamePadState CurGamepadState { get; set; }

                
        public GamepadManager(PlayerIndex index) {
            Index = index;
        }

               
        public void Update()
        {
            
            LastGamepadState = CurGamepadState;
            CurGamepadState = GamePad.GetState(Index);
        }

        public bool IsButtonPressed(Buttons button)
        {
            return (CurGamepadState.IsButtonDown(button) && LastGamepadState.IsButtonUp(button));
        }

        public bool IsButtonReleased(Buttons button)
        {
            return (CurGamepadState.IsButtonUp(button) && LastGamepadState.IsButtonDown(button));
        }

        public bool IsButtonDown(Buttons button)
        {
            return CurGamepadState.IsButtonDown(button);
        }
        
        //Return first pressed Buttons
        public Buttons GetPressedButton()
        {
            foreach (var item in Enum.GetValues(typeof(Buttons)))
            {
                if (IsButtonPressed((Buttons)item))
                    return (Buttons)item;
            }

            return 0;
        }        

    }
}
