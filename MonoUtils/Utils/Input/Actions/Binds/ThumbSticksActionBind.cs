using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XnaUtils.Input
{
    public class ThumbSticksActionBind : ActionBind
    {

        public Vector2 Direction;
        private float Threshold;
        private bool IsRight;

        public ThumbSticksActionBind(Vector2 direction, float threshold = 0.5f, bool isRight = false)
        {
            Direction = direction;
            Threshold = threshold;
            IsRight = isRight;
        }


        public override bool CheckActivation(InputBundle input, int playerIndex = 0)
        {
            Vector2 analog;
            if (IsRight)
                analog = input.GamepadManager.CurGamepadState.ThumbSticks.Right;
            else
                analog = input.GamepadManager.CurGamepadState.ThumbSticks.Left;

            return Vector2.Dot(analog, Direction) >= Threshold;
        }

        public override string GetBindTag()
        {
            if (IsRight)
                return "RightThumbStick";
            else
                return "LeftThumbStick";
        }
    }

}
