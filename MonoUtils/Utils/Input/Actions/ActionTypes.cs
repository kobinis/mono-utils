using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XnaUtils.Input
{
    public enum ActionTypes
    {
        None = 0,
        Action1,
        Action2,
        Action3,
        Action4,
        PlayerUp,
        PlayerDown,
        PlayerLeft,
        PlayerRight,
        PlayerBrake,
        PlayerAnalog1,
        PlayerAnalog2,
        QuickUse1,
        QuickUse2,
        QuickUse3,
        QuickUse4,
        MoveToCursor,
        KeyboardLockRotation,
        Use,
        PlayerSwapUp,
        PlayerCallHelp,
        PlayerAutoEquip,
        UiLoot,
        UiStash,

        Approve,
        Cancel,    
        Skip,
        UiUp,
        UiDown,
        UiLeft,
        UiRight,
        UiSwitchLeft, //Used to change categories
        UiSwitchRight, //Used to cahnge categories

        CursorLeft,
        CursorRight,
        CursorMiddle,

        EscapeMenu,
        MissionLog, 
        Inventory, 
        Hangar, 
        TacticalMap, 
        GalaxyMap,
        UiZoomIn,
        UiZoomOut,
        UiSort,        
        Exit,
    }

    //    Action1 = 1 << 0, //ControlSignals
    //    Action2 = 1 << 1,
    //    Action3 = 1 << 2,
    //    Action4 = 1 << 3,
    //    Up = 1 << 4, 
    //    Down = 1 << 5, 
    //    Left = 1 << 6,  
    //    Right = 1 << 7,     
    //    AlwaysOn = 1 << 8,

    //    QuickUse1 = 1 << 9,
    //    QuickUse2 = 1 << 10,
    //    QuickUse3 = 1 << 11,
    //    QuickUse4 = 1 << 12,
    //    MoveToCursor = 1 << 13,

}
