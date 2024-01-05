using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XnaUtils.Input
{
    public class InputBundle
    {
        
        public KeyboardManager KeyboardManager { get; set; }

       // public GamepadManager MainGamepadManager { get; set; }
        public GamepadManager GamepadManager { get; set; }
        public MouseManager MouseManager { get; set; }

        public InputBundle()
        {
            KeyboardManager = new KeyboardManager();
            GamepadManager = new GamepadManager(Microsoft.Xna.Framework.PlayerIndex.One);
            MouseManager = new MouseManager();
        }

        
        

        public void Update()
        {
            
            KeyboardManager.Update();
            MouseManager.Update();
            GamepadManager.Update();          
        }        

        public bool IsGKeyDown(GKeys key)
        {
            bool res = MouseManager.IsMouseDown(key.MouseButton); 
            res |= KeyboardManager.IsKeyDown(key.Key);
            return res;
        }

        //public bool IsPreviousGKeyDown(GKeys key)
        //{
        //    bool res = MouseManager.IsMouseButtonsDown(lastMouseState, key.MouseButton);
        //    res |= LastKeyboardState.IsKeyDown(key.Key);
        //    return res;
        //}

        public bool IsGKeyPressed(GKeys key)
        {
            return MouseManager.IsMousePressed(key.MouseButton) || KeyboardManager.IsKeyPressed(key.Key);           
        }

        public GKeys GetPressedKey()
        {            
            //var mouseValues = ReflectionUtils.GetValues<MouseButtons>();
            //foreach (var mouseBut in mouseValues)
            //{
            //    if(IsGKeyPressed(new GKeys(mouseBut)))
            //    {
            //        return new GKeys(mouseBut);
            //    }
            //}

            //var values = ReflectionUtils.GetValues<Keys>();
            //foreach (var key in values)
            //{
            //    if (IsKeyPressed(key))
            //        return key;
            //}

            return Keys.None; 
        }

    


        //public void CalculateActiveControlType()
        //{
        //    PlayerControlType = PlayerControlTypes.MouseAndKeys;
        //    for (int i = 0; i < 4; i++)
        //    {
        //        if (CheckGamePadActivity(i))
        //        {
        //            activeIndex = i;
        //            PlayerControlType = PlayerControlTypes.XboxController;
        //        }
        //    }

        //}
    }
}
