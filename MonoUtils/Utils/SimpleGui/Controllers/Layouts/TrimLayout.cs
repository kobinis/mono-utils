//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using XnaUtils.SimpleGui;

//namespace SolarConflict.XnaUtils.SimpleGui.Controllers.Layouts
//{
//    class TrimLayout:GuiControl
//    {
//        private HolderControl _holderControl;
//        private Vector2 _offset;

//        public Vector2 Offset
//        {
//            get { return _holderControl.LocalPosition; }
//            set { _holderControl.LocalPosition = value; }
//        }

//        public TrimLayout():base()
//        {
//            _holderControl = new HolderControl();
//            children.Add(_holderControl);
//        }

//        public override void AddChild(GuiControl guiController)
//        {
//            _holderControl.AddChild(guiController);
//        }

//        public override void RemoveAllChildren()
//        {
//            _holderControl.RemoveAllChildren();
//        }

//        public override void RemoveChild(GuiControl guiController)
//        {
//            _holderControl.RemoveChild(guiController);
//        }



//        RasterizerState _rasterizerState = new RasterizerState() { ScissorTestEnable = true };
//        public override void Draw(SpriteBatch sb, Color? color = null)
//        {
//            sb.End();
//            sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend,
//                      null, null, _rasterizerState);
//            DrawLogic(sb, color);
//            foreach (var guiControl in children)
//            {
//                guiControl.Draw(sb, color);
//            }
//            sb.End();
//            sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
//        }
//    }
//}
