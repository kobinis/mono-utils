using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using SolarConflict.XnaUtils;
using SolarConflict.XnaUtils.Input;
using XnaUtils.Input;
using System.Linq;

namespace XnaUtils
{    
    /// <summary>
    /// Holds all inputs from user
    /// </summary>    
    [Serializable] // TEMP, should not be serialized
    public class InputState //change to struct?
    {
        static readonly InputState emptyState = new InputState();
        public static InputState EmptyState //Not Good, can be changed
        {
            get { return InputState.emptyState; }
        }

        public CursorInfo Cursor;
        public ActionState ActionState;

        public string TextBuffer;

        /// <summary>
        /// Constructs a new input state.
        /// </summary>
        public InputState()
        {
            ActionState = new ActionState();
            Cursor = new CursorInfo();
        }  
        

       public bool IsActionStart(ActionTypes action)
       {
            return ActionState.IsActionStart(action);
       }

        public bool IsAction(ActionTypes action)
        {
            return ActionState.IsAction(action);
        }

        public bool IsActionEnd(ActionTypes action)
        {
            return ActionState.IsActionEnd(action);
        }

    }

}
