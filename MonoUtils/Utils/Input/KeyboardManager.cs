using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XnaUtils.Input
{    
    public class KeyboardManager
    {
        public KeyboardState LastKeyboardState { get; set; }
        public KeyboardState CurKeyboardState { get; set; }

        public void Update()
        {            
            LastKeyboardState = CurKeyboardState;
            CurKeyboardState = Keyboard.GetState();
        }

        /// <summary>
        /// Returens true when the key is pressed (is down and was up)
        /// </summary>
        public bool IsKeyPressed(Keys key)
        {
            return (CurKeyboardState.IsKeyDown(key) && LastKeyboardState.IsKeyUp(key));
        }

        public bool IsKeyReleased(Keys key)
        {
            return (CurKeyboardState.IsKeyUp(key) && LastKeyboardState.IsKeyDown(key));
        }

        public bool IsKeyDown(Keys key)
        {            
            return CurKeyboardState.IsKeyDown(key);
        }

        public Keys[] GetPressedKeys()
        {
            return CurKeyboardState.GetPressedKeys();
        }             
    }
}