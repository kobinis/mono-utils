using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XnaUtils.Input
{
    public abstract class ActionBind
    {
        public abstract bool CheckActivation(InputBundle input, int playerIndex = 0);
        /// <summary>
        /// Used for displaying the key
        /// </summary>        
        public abstract string GetBindTag();
    }
}
