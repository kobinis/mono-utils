using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SolarConflict;
using SolarConflict.XnaUtils.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XnaUtils.Input
{       
    public enum PlayerControlTypes { MouseAndKeys, XboxController}
    /// <summary>
    /// Manages input
    /// </summary>
    public class InputManager 
    {
        const int MaxGamePadNumber = 4;

        public PlayerControlTypes PlayerControlType { get; set; }        
        public InputState InputState { get; private set; }

        public bool IsInGame; //TODO: change

        public string TextBuffer;
                
        private ActionManager actionManager;
        public InputBundle InputBundle { get; private set; }



        public InputManager()
        {
            PlayerControlType = PlayerControlTypes.XboxController;
            InputBundle = new InputBundle();
            InputState = new InputState();

            actionManager = new ActionManager();
            actionManager.BindCollections["keys"] = new ActionBindCollection("keys", KeysSettings.Data.KeyBindings);
            actionManager.BindCollections["xbox"] = new ActionBindCollection("xbox", KeysSettings.Data.XboxBindings);

            var defaultKeys = new Dictionary<ActionTypes, GKeys> {
                {ActionTypes.Exit, Keys.Escape },
                {ActionTypes.CursorLeft, MouseButtons.LeftButton},
                {ActionTypes.CursorRight, MouseButtons.RightButton},
                {ActionTypes.UiUp, Keys.Up},
                {ActionTypes.UiDown,Keys.Down},
                {ActionTypes.UiLeft, Keys.Left},
                {ActionTypes.UiRight,Keys.Right},
                {ActionTypes.Approve,  Keys.Enter},
                {ActionTypes.Cancel,  Keys.Escape},
            };
            actionManager.BindCollections["default"] = new ActionBindCollection("default", defaultKeys);            
            actionManager.BindCollections["defaultXbox"] = new ActionBindCollection("defaultXbox");
            actionManager.BindCollections["defaultXbox"]._binds.Add(ActionTypes.UiUp, new ThumbSticksActionBind(Vector2.UnitY));
            actionManager.BindCollections["defaultXbox"]._binds.Add(ActionTypes.UiDown, new ThumbSticksActionBind(-Vector2.UnitY));

            //_actionManager.BindCollections["defaultXbox"]._binds.Add(ActionTypes.Exit, new ControllerActionBind(Buttons.Back)); //move to data
            //_actionManager.BindCollections["default"]._binds.Add(ActionTypes.UiDown, new GKeyActionBind(Keys.Down));
            //_binds = new Dictionary<ActionTypes, List<ActionBind>>();                        
            //_binds.Add(ActionTypes.Up, new List<ActionBind> { , new ThumbSticksActionBind(Vector2.UnitY) });
            //_binds.Add(ActionTypes.Down, new List<ActionBind> { new GKeyActionBind(Keys.Down), new ThumbSticksActionBind(-Vector2.UnitY) });
            //_binds.Add(ActionTypes.Approve, new List<ActionBind> { new GKeyActionBind(Keys.Enter), new ControllerActionBind(Buttons.A) }); ;
            //_binds.Add(ActionTypes.Back, new List<ActionBind> { new GKeyActionBind(Keys.Escape), new ControllerActionBind(Buttons.B) }); ;
            //_binds.Add(ActionTypes.Exit, new List<ActionBind> { new GKeyActionBind(Keys.Escape), new ControllerActionBind(Buttons.Back) }); ;
        }

        

        public void SetBindCollection(ActionBindCollection collection)
        {            
            actionManager.BindCollections[collection.ID] = collection;
        }

        public void UpdateInput()
        {
            //InputState = InputState.EmptyState;
            InputBundle.Update();
            //Action abstruction
            InputState.ActionState = actionManager.Update(InputBundle);
            //Cursor abstruction
            UpdateCursor();
        }

        public void Update(GameTime time, Game game)
        {
            //if (game != null && !game.IsActive) //Add option disable 
            //{
            //    //InputState = InputState.EmptyState;
            //    return;
            //}
            
            //Raw input
            InputBundle.Update();
            //Action abstruction
            InputState.ActionState = actionManager.Update(InputBundle);
            //Cursor abstruction
            UpdateCursor();                                    
        }




        private void UpdateCursor()
        {

            CursorInfo Cursor = InputState.Cursor;
            CursorInfo cl = new CursorInfo();
            if (Cursor.ActiveGuiControl != null)
                cl.ActiveGuiControl = Cursor.ActiveGuiControl;
            cl.IsActive = true;
            //cl.WasNotStartedOnGui = Cursor.WasNotStartedOnGui || (mouseState.LeftButton == ButtonState.Released && mouseState.RightButton == ButtonState.Released);
            cl.PreviousPosition = Cursor.Position;
            cl.LastScrollValue = Cursor.ScrollValue;
            cl.ScrollValue = InputBundle.MouseManager.CurMouseState.ScrollWheelValue;
            cl.IsPressedLeft = InputState.ActionState.IsAction(ActionTypes.CursorLeft);
            cl.IsLastPressedLeft = InputState.ActionState.IsPreviousAction(ActionTypes.CursorLeft);
            cl.IsPressedRight = InputState.ActionState.IsAction(ActionTypes.CursorRight);
            cl.IsLastPressedRight = InputState.ActionState.IsPreviousAction(ActionTypes.CursorRight);
            
            if (InputBundle.MouseManager.HasMouseStateChanged()) // Add timer
            {
                cl.Position = InputBundle.MouseManager.Position;
            }
            else
            {
                cl.Position = Cursor.Position;
                //if (PlayerControlType == PlayerControlTypes.XboxController)
                //{
                //    var gamePadState = InputBundle.GamepadManager.CurGamepadState;
                //    cl.Position = Cursor.Position;
                //    //if (gamePadState.ThumbSticks.Left.Length() > 0.5f)
                //    cl.Position = Cursor.Position + new Vector2(gamePadState.ThumbSticks.Left.X, -gamePadState.ThumbSticks.Left.Y) * 8f;
                  
                //}
                //IsInGame = false;
            }




            if (cl.OnPressLeft || cl.OnPressRight)
            {
                cl.FirstPosition = cl.Position;
            }
            else
            {
                cl.FirstPosition = Cursor.FirstPosition;
            }
            /*   cl.ScrollValue = mouseState.ScrollWheelValue;
               cl.LastScrollValue = lastMouseState.ScrollWheelValue;*/
            
            InputState.Cursor = cl;
        }

        private bool CheckGamePadActivity(int index)
        {
            GamePadState state = GamePad.GetState(index);
            if (state.IsConnected)
            {
                if (state.Buttons.A == ButtonState.Pressed ||
                    state.Buttons.B == ButtonState.Pressed ||
                    state.Buttons.X == ButtonState.Pressed ||
                    state.Buttons.Y == ButtonState.Pressed ||
                    state.Buttons.Back == ButtonState.Pressed ||
                    state.Buttons.Start == ButtonState.Pressed ||
                    state.Buttons.LeftShoulder == ButtonState.Pressed ||
                    state.Buttons.RightShoulder == ButtonState.Pressed ||
                    state.Buttons.LeftStick == ButtonState.Pressed ||
                    state.Buttons.RightStick == ButtonState.Pressed ||
                    state.DPad.Up == ButtonState.Pressed ||
                    state.DPad.Down == ButtonState.Pressed ||
                    state.DPad.Left == ButtonState.Pressed ||
                    state.DPad.Right == ButtonState.Pressed ||
                    state.Triggers.Left > 0.5f ||
                    state.Triggers.Right > 0.5f ||
                    state.ThumbSticks.Left.Length() > 0.5f ||
                    state.ThumbSticks.Right.Length() > 0.5f)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsGKeyPressed(GKeys key)
        {
            return InputBundle.IsGKeyPressed(key);
        }


        public string GetActionTag(ActionTypes actionType)
        {
            Dictionary<ActionTypes, ActionBind> binds;
            if (PlayerControlType == PlayerControlTypes.MouseAndKeys)
            {
                binds = actionManager.BindCollections["keys"].GetBinds();
            }
            else
            {
                binds = actionManager.BindCollections["xbox"].GetBinds();
            }

            if (binds.ContainsKey(actionType))
            {
                return binds[actionType].GetBindTag();
            }
            else
            {
                binds = actionManager.BindCollections["keys"].GetBinds();
                if (binds.ContainsKey(actionType))
                    return binds[actionType].GetBindTag();
            }

            return string.Empty;
        }


        public bool IsKeyDown(Keys key)
        {
            return InputBundle.KeyboardManager.IsKeyDown(key);
        }

        /// <summary>Returns true when the key is pressed (is down and was up)</summary>
        public bool IsKeyPressed(Keys key)
        {
            return InputBundle.KeyboardManager.IsKeyPressed(key);
        }
        
    }
}

