using Microsoft.Xna.Framework.Input;
using SolarConflict.XnaUtils.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XnaUtils.Input
{
    public class ActionBindCollection
    {
        public string ID { get; set; }
        public Dictionary<ActionTypes, ActionBind> _binds;
        
        public ActionBindCollection(string id)
        {
            ID = id;
            _binds = new Dictionary<ActionTypes, ActionBind>();
        }

        public ActionBindCollection(string id, Dictionary<ActionTypes, GKeys> keyBinding) : this(id)
        {
            foreach (var keyValue in keyBinding)
            {
                _binds[keyValue.Key] = new GKeyActionBind(keyValue.Value);
            }
        }

        public ActionBindCollection(string id, Dictionary<ActionTypes, Buttons> keyBinding) : this(id)
        {
            foreach (var keyValue in keyBinding)
            {
                _binds[keyValue.Key] = new ControllerActionBind(keyValue.Value);
            }
        }

        public Dictionary<ActionTypes, ActionBind> GetBinds()
        {
            return _binds;
        }            

        //Change it to update a HashSet
        public void Update(InputBundle input, ActionState actionState)
        {
            foreach (var item in _binds)
            {
                if(item.Value.CheckActivation(input))
                {
                    actionState.CurrentActions.Add(item.Key);
                  //  ActivityManager.Inst.AddToast(item.Key.ToString(),10);
                }
            }
        }

        public Dictionary<ActionTypes, GKeys> GetGKeyBinding()
        {
            Dictionary<ActionTypes, GKeys> keyBinding = new Dictionary<ActionTypes, GKeys>();
            foreach (var item in _binds)
            {
                if(item.Value is GKeyActionBind)
                {
                    keyBinding[item.Key] = (item.Value as GKeyActionBind).key;
                }
            }
            return keyBinding;
        }
    }
}
