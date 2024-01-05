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
    public class GenActionState<T>
    {
        public HashSet<T> CurrentActions;
        public HashSet<T> PreviousActions;
        //public Dictionary<InputActions, TimeSpan> ActionStartTime;

        public GenActionState()
        {
            CurrentActions = new HashSet<T>();
            PreviousActions = new HashSet<T>();
        }

        public bool IsAction(T action)
        {
            return CurrentActions.Contains(action);
        }

        public bool IsActionStart(T action)
        {
            return CurrentActions.Contains(action) && !PreviousActions.Contains(action);
        }

        public bool IsActionEnd(T action)
        {
            return !CurrentActions.Contains(action) && PreviousActions.Contains(action);
        }
    }
}
