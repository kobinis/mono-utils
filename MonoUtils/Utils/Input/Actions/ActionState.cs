using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XnaUtils.Input
{
    /// <summary>
    /// represents the state of action preformed 
    /// </summary>    
    [Serializable]
    public class ActionState
    {
        public HashSet<ActionTypes> CurrentActions;
        public HashSet<ActionTypes> PreviousActions;
        public HashSet<ActionTypes> ConsumedActions;
        //public Dictionary<InputActions, TimeSpan> ActionStartTime;

        public ActionState()
        {
            CurrentActions = new HashSet<ActionTypes>();
            PreviousActions = new HashSet<ActionTypes>();
            ConsumedActions = new HashSet<ActionTypes>();
        }

        public void Consume(ActionTypes action)  //??
        {
            CurrentActions.Remove(action);
            PreviousActions.Remove(action);
            ConsumedActions.Add(action);
        }

        public bool IsAction(ActionTypes action)
        {
            return CurrentActions.Contains(action);
        }

        public bool IsPreviousAction(ActionTypes action)
        {
            return PreviousActions.Contains(action);
        }

        public bool IsActionStart(ActionTypes action)
        {
            return CurrentActions.Contains(action) && !PreviousActions.Contains(action);
        }

        public bool IsActionEnd(ActionTypes action)
        {
            return !CurrentActions.Contains(action) && PreviousActions.Contains(action);
        }

        /// <summary>
        /// Checks and consumes actionType
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool OnActionStart(ActionTypes action)
        {
            return CurrentActions.Contains(action) && !PreviousActions.Contains(action) && !ConsumedActions.Contains(action);            
        }
       
    }
}
