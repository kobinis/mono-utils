using Microsoft.Xna.Framework;
using SolarConflict.XnaUtils.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XnaUtils.Input
{
    public class GKeyActionBind : ActionBind
    {
        public GKeys key;

        public GKeyActionBind(GKeys key)
        {
            this.key = key;
        }

        public override bool CheckActivation(InputBundle input, int playerIndex = 0)
        {
            return playerIndex == 0 && input.IsGKeyDown(key);
        }

        public override string GetBindTag()
        {
            return key.ToString();
            //return key.GetTag();
        }
    }

}
