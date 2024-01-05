using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using XnaUtils.Input;
using XnaUtils.SimpleGui.Controllers;

namespace XnaUtils.SimpleGui {
    
    /// <summary>On invocation, pops up a confirmation, invokes the actual action if that's clicked</summary>
    public class ConfirmationControl : GuiControl 
    {
      
        public string ConfirmationText;

        GuiControl _popup;
        private GuiManager _gui;

        public object TextBank { get; private set; }

        public ConfirmationControl(GuiManager gui, Vector2 size, string confirmationText = "Are you sure?"):base(Vector2.Zero, size) 
        {
            _gui = gui;
            ConfirmationText = confirmationText;
        }

        public override void Draw(SpriteBatch sb, Color? color = null)
        {            
            base.Draw(sb, color);
            if (_popup != null)
                _popup.Draw(sb, color);
        }

        void ClosePopup() 
        {
            if (_popup == null)
                // Already closed
                return;

            _gui.RemoveControl(_popup);
            _popup = null;
        }

        /// <summary>Call the actual action (without bringing up a confirmation dialogue)</summary>
        public void ConfirmAction(CursorInfo cursorInfo) {
            ClosePopup();
            base.InvokeAction(cursorInfo);
        }

        public override void InvokeAction(CursorInfo cursorLocation) 
        {
            OpenPopup();
        }

        public static GuiControl MakeConfirmationMenu(string confirmationText, GuiManager gui)
        {
            // Create and add the popup
            var layout = new VerticalLayout(Vector2.Zero);
            layout.IsResizeChildrenHorizontally = true;
            layout.ControlColor = Color.White;
            layout.ShowFrame = true;
            layout.AddChild(new RichTextControl(confirmationText));

            var confirm = new RichTextControl("Yes",isShowFrame: true);
            confirm.CursorOverColor = Color.Yellow;
            confirm.Action += (source, cursorInfo) => {                
                (source.Parent.Parent as ConfirmationControl).ConfirmAction(cursorInfo);
            };
            layout.AddChild(confirm);

            var cancel = new RichTextControl("No",isShowFrame: true);
            cancel.CursorOverColor = Color.Yellow;
            cancel.Action += (source, cursorInfo) => {
                (source.Parent.Parent as ConfirmationControl).ClosePopup();
            };
            layout.AddChild(cancel);
            return layout;
        }

        void OpenPopup() {
            if (_popup != null)                
                return; // Already open
            _popup = MakeConfirmationMenu(ConfirmationText, _gui);
            _popup.Parent = this;            
            _gui.AddControl(_popup);
           // AddChild(_popup);
        }
    }
}
