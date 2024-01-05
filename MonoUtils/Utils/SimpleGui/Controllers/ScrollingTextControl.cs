using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SolarConflict;
using SolarConflict.XnaUtils;
using System.Diagnostics;
using XnaUtils.Graphics;
using SolarConflict.XnaUtils.SimpleGui.Controllers;
using SolarConflict.XnaUtils.Input;

namespace XnaUtils.SimpleGui.Controllers
{
    
    /// <summary>
    /// A textbox that cuts the text inside
    /// </summary>
    [Serializable]
    public class ScrollingTextControl : GuiControl //needs to derived from abstract class or interface IGuiControl
    {
        //TODO: scorll with holding mouse over controller
        //Add a controller that holds this controll with a scroll bar similar to scrollable grid
        public TextShadow Shadow;

        private int _verticalSpacing = 20;
        private int _horizontalSpacing = 10;                        

        float _offset;
        public Color TextColor
        {
            get { return _parser.DefaultColor; }
            set { _parser.DefaultColor = value; }
        }
        public SpriteFont Font
        {
            get { return _parser.DefaultFont; }
            set { _parser.DefaultFont = value; }
        }

        public RichTextParser Parser
        {
            get { return _parser; }
        }

        public Color? TextHighlightColor { get; set; }

        public string Text
        {
            get { return _parser.Text; }
            set
            {
                _parser.TextParse = value;                
            }
        }
        public float MaxLineWidth
        {
            get
            {
                return _parser.MaxLineWidth;
            }
            set
            {
                _parser.MaxLineWidth = value;                
            }
        }

        public bool IsShowFrame { get; set; }
        public int ScorllingRange { get; private set; }

        RichTextParser _parser;

        public ScrollingTextControl(string text, Vector2 size, SpriteFont font = null, bool isShowFrame = false)
            : base(Vector2.Zero, Vector2.One)
        {
            if (font == null)
            {
                font = FontBank.DefaultFont;
            }
            this.IsShowFrame = isShowFrame;
            HalfSize = size * 0.5f;
            _parser = new RichTextParser(font);
            TextColor = Color.LightBlue;

            //  PressedControlColor = Color.Blue;
            // CursorOverColor = Color.Yellow;
            Text = text;
            
            Sprite = TextureBank.Inst.GetSprite("Small_ui");
            //_scrollBar = new VerticalSliderControl(Vector2.One);
            Update(InputState.EmptyState);
        }


        public override void Update(InputState inputState)
        {
           // _scrollBar.Update(inputState);
            base.Update(inputState);
        }

        public override void UpdateLogic(InputState inputState)
        {
            MaxLineWidth = this.Width - _horizontalSpacing * 2;

            //if (IsPositionOn(inputState.Cursor.Position) || _scrollBar.IsCursorOn)
            //    _scrollBar.Value -= Math.Sign(MouseUtils.Inst.GetDScroolWheel()); //TODO: use inputState
          
            float diff = -inputState.Cursor.GetDScroolWheel() * 0.5f;
            if (IsCursorOn && IsPositionOn(inputState.Cursor.FirstPosition) && inputState.Cursor.IsPressed)
            {
                diff = inputState.Cursor.PreviousPosition.Y - inputState.Cursor.Position.Y;
            }
            _offset += diff;
            ScorllingRange = (int)Math.Ceiling(Parser.Size.Y - this.Height + _verticalSpacing * 2);
            _offset = Math.Min(_offset, ScorllingRange);
            _offset = Math.Max(_offset, 0);
            ActivityManager.Inst.AddToast(_offset.ToString(), 5);

            //mat  Parser.Size.Y
            

           
        }        

        RasterizerState _rasterizerState = new RasterizerState() { ScissorTestEnable = true };        
        protected override void DrawLogic(SpriteBatch sb, Color? color = null)
        {
            

            if (IsShowFrame)
                base.DrawLogic(sb, color);

            if (Text == null)
                return;

            
            sb.End();
            sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend,
                      null, null, _rasterizerState);

            //Copy the current scissor rect so we can restore it after
            Rectangle currentRect = sb.GraphicsDevice.ScissorRectangle;

            //Set the current scissor rectangle
            Rectangle boarder = this.GetRectangle();
            boarder.Inflate(-_horizontalSpacing, -_verticalSpacing);
            sb.GraphicsDevice.ScissorRectangle = boarder;

            //Draw the text at the top left of the scissor rectangle

            


            Color backcolor = _parser.DefaultColor;
            if (TextHighlightColor.HasValue && this.IsCursorOn)
                _parser.DefaultColor = TextHighlightColor.Value;

            // Shadows
            //if (Shadow != null)
            //{
            //    // TEMP disregard color arg, override with shadow color field
            //    _parser.Draw(sb, Position - size * 0.5f - Vector2.UnitX * Shadow.Offset, Shadow.Color);
            //    _parser.Draw(sb, Position - size * 0.5f + Vector2.UnitX * Shadow.Offset, Shadow.Color);
            //    _parser.Draw(sb, Position - size * 0.5f - Vector2.UnitY * Shadow.Offset, Shadow.Color);
            //    _parser.Draw(sb, Position - size * 0.5f + Vector2.UnitY * Shadow.Offset, Shadow.Color);                
            //}

            // Text            
            var size = _parser.MeasureText();
            _parser.Draw(sb, Position  - HalfSize - Vector2.UnitY*_offset  + new Vector2(_horizontalSpacing, _verticalSpacing), color);
            _parser.DefaultColor = backcolor;

            //Reset scissor rectangle to the saved value
            sb.GraphicsDevice.ScissorRectangle = currentRect;

            //Draw more textboxes or whatever else using the scissor rectangles here

            //End the spritebatch
            sb.End();
            sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
        }

    }
}
