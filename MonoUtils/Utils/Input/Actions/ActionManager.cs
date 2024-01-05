using Microsoft.Xna.Framework;
using SolarConflict.XnaUtils.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XnaUtils.Input
{
    public enum InputSetTypes { }
    class ActionManager
    {        
        public Dictionary<string, ActionBindCollection> BindCollections { get; private set; }
        
        ActionState actionState;

        public ActionManager()
        {
            BindCollections = new Dictionary<string, ActionBindCollection>();            
            actionState = new ActionState();
        }

        public ActionState Update(InputBundle input) //Change to return the action State
        {                        
            var temp = actionState.PreviousActions;
            actionState.PreviousActions = actionState.CurrentActions;
            actionState.CurrentActions = temp;
            actionState.CurrentActions.Clear();

            foreach (var item in BindCollections.Values)
            {
                item.Update(input, actionState);
            }

            //if (actionState.IsActionStart(ActionTypes.Exit))
            //    ActivityManager.Inst.AddToast("Exit", 11,Color.Red);
            return actionState;
        }
    }
}
