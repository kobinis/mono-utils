using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using XnaUtils;
using SolarConflict.Framework;
using System.IO;
using System.Reflection;
using SolarConflict.XnaUtils.Input;
using XnaUtils.Graphics;

using SolarConflict.XnaUtils;
using XnaUtils.Input;

namespace SolarConflict
{

    public class KeyBindingsData
    {
        public Dictionary<ActionTypes, GKeys> KeyBindings;
        public Dictionary<ActionTypes, Buttons> XboxBindings;

        public KeyBindingsData()
        {

            KeyBindings = new Dictionary<ActionTypes, GKeys>()
            {{ActionTypes.Action1,  new GKeys(MouseButtons.LeftButton)},
             {ActionTypes.Action2,  new GKeys(MouseButtons.RightButton)},
             {ActionTypes.Action3,  Keys.Q},
             {ActionTypes.Action4,  Keys.LeftShift},
             {ActionTypes.PlayerUp,  Keys.W},
             {ActionTypes.PlayerDown,  Keys.S},
             {ActionTypes.PlayerLeft,  Keys.A},
             {ActionTypes.PlayerRight,  Keys.D},
             {ActionTypes.PlayerBrake,  Keys.X},
             {ActionTypes.QuickUse1,  Keys.D1},
             {ActionTypes.QuickUse2,  Keys.D2},
             {ActionTypes.QuickUse3,  Keys.D3},
             {ActionTypes.QuickUse4,  Keys.D4},
             {ActionTypes.MoveToCursor,  new GKeys(MouseButtons.MiddleButton)},
             {ActionTypes.KeyboardLockRotation,  Keys.LeftControl},
             {ActionTypes.Use, Keys.F},
             {ActionTypes.PlayerSwapUp,  Keys.Tab},
             {ActionTypes.PlayerCallHelp,  Keys.R},
             {ActionTypes.PlayerAutoEquip,  Keys.T},
             {ActionTypes.UiLoot,  Keys.Q},
             {ActionTypes.UiStash,  Keys.E},
             {ActionTypes.UiSort,  Keys.S},
             {ActionTypes.EscapeMenu, Keys.Escape },
             {ActionTypes.MissionLog, Keys.J},
             {ActionTypes.Inventory, Keys.E},
             {ActionTypes.Hangar, Keys.H},
             {ActionTypes.TacticalMap, Keys.M},
             {ActionTypes.GalaxyMap, Keys.G},
             {ActionTypes.UiZoomIn, Keys.OemPlus},
             {ActionTypes.UiZoomOut, Keys.OemMinus},
             {ActionTypes.Skip, Keys.Space},
            };

            XboxBindings = new Dictionary<ActionTypes, Buttons>()
            {{ActionTypes.Action1,Buttons.RightTrigger},
             {ActionTypes.Action2,Buttons.LeftTrigger},
             {ActionTypes.Action3,  Buttons.X},
             {ActionTypes.Action4,  Buttons.Y},
             //{ActionTypes.PlayerUp,  Keys.W},
             //{ActionTypes.PlayerDown,  Keys.S},
             //{ActionTypes.PlayerLeft,  Keys.A},
             //{ActionTypes.PlayerRight,  Keys.D},
             //{ActionTypes.PlayerBrake,  Keys.X},
             {ActionTypes.QuickUse1,  Buttons.LeftShoulder},
             {ActionTypes.QuickUse2,  Buttons.LeftStick},
             {ActionTypes.QuickUse3,  Buttons.RightStick},
             //{ActionTypes.MissionLog,  Buttons.LeftStick},
             {ActionTypes.GalaxyMap,  Buttons.RightShoulder},
             //{ActionTypes.PlayerUse2,  Keys.D2},
             //{ActionTypes.PlayerUse3,  Keys.D3},
             //{ActionTypes.PlayerUse4,  Keys.D4},
             //{ActionTypes.PlayerMoveToCursor,  Keys.Space},
             //{ActionTypes.PlayerKeyboardLockRotation,  Keys.LeftShift},
             {ActionTypes.Use,  Buttons.A},
             {ActionTypes.PlayerSwapUp,  Buttons.DPadLeft},
             {ActionTypes.PlayerCallHelp,  Buttons.B},
             {ActionTypes.PlayerAutoEquip,  Buttons.DPadRight},
             {ActionTypes.UiLoot,  Buttons.X},
             {ActionTypes.UiStash,  Buttons.Y},

             { ActionTypes.EscapeMenu, Buttons.Back},
             //{ActionTypes.MissionLog, Keys.J},
             {ActionTypes.Inventory, Buttons.Start},
             //{ActionTypes.Hangar, Keys.H},
             //{ActionTypes.TacticalMap, Keys.M},
             //{ActionTypes.GalaxyMap, Keys.G},
             {ActionTypes.UiZoomIn, Buttons.DPadDown},
             {ActionTypes.UiZoomOut, Buttons.DPadUp},
             {ActionTypes. UiSort,  Buttons.DPadLeft},
             {ActionTypes.UiLeft, Buttons.LeftShoulder},
             {ActionTypes.UiRight,Buttons.RightShoulder},
             {ActionTypes.UiSwitchLeft, Buttons.LeftTrigger},
             {ActionTypes.UiSwitchRight,Buttons.RightTrigger},

             {ActionTypes.Approve,  Buttons.A},
             {ActionTypes.Cancel,  Buttons.B},
             {ActionTypes.CursorLeft,  Buttons.A},
             {ActionTypes.CursorRight,  Buttons.RightTrigger},
             { ActionTypes.Exit, Buttons.Back},
            };

            /*
        
        PlayerAnalog1,
        PlayerAnalog2,

        Approve,
        Cancel,        
        UiUp,
        UiDown,
        UiLeft,
        UiRight,

        CursorLeftClick,
        CursorRightClick,
        CursorShiftClick,       
            */
        }
    }

    //public 
    public static class KeysSettings
    {
        public const string LOCK_ROTATION = "LockRotation";
        public static Dictionary<GKeys, string> KeyIconIDs;
        private static string fullPath;


        public static KeyBindingsData Data;



        static KeysSettings()
        {
           // fullPath = Path.Combine(Consts.GAME_DATA_PATH, "KeyBindings.json");
            KeyIconIDs = new Dictionary<GKeys, string>() { { MouseButtons.LeftButton, "lmbV2" }, { MouseButtons.RightButton, "rmbV2" }, { MouseButtons.MiddleButton, "mousescroll" } }; //Add MMB


            Data = new KeyBindingsData();
            LoadOrDefaultBindings();
        }




        public static void LoadOrDefaultBindings()
        {
            try
            {
               // Data = SaveLoadManager.Inst.Load<KeyBindingsData>(fullPath);
            }
            catch (Exception)
            {
                SaveKeys();
            }
        }

        public static void SaveKeys()
        {
            //SaveLoadManager.Inst.Save(fullPath, Data);
        }

        public static string GetActionString(ActionTypes action)
        {
            //Maybe add if contains
            if (Data.KeyBindings.ContainsKey(action))
            {
                return Data.KeyBindings[action].ToString();
            }
            else
                return string.Empty;
        }

        public static string GetTag(GKeys key)
        {
            if (KeyIconIDs.ContainsKey(key))
            {
                return Sprite.Get(KeyIconIDs[key]).ToTag();
            }
            else
            {
                return key.ToString();
            }
        }

        public static string GetButtonTag(Buttons button)
        {
            return button.ToString();
            //return button.GetTag();
        }



        //public static string GetTag(ActionTypes action)
        //{
        //    if (KeysSettings.Data.KeyBindings.ContainsKey(action))
        //    {
        //        return GetTag(KeysSettings.Data.KeyBindings[action]);
        //    }
        //    return string.Empty;
        //}
    }
}