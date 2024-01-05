using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XnaUtils.Input
{

    public class ControllerActionBind : ActionBind
    {
        private Buttons _boutton;


        public ControllerActionBind(Buttons button)
        {
            _boutton = button;
        }


        public override bool CheckActivation(InputBundle input, int playerIndex = 0)
        {
            return input.GamepadManager.IsButtonDown(_boutton);
        }

        public override string GetBindTag()
        {
            return _boutton.ToString();
            //return _boutton.GetTag();
        }
    }
}
