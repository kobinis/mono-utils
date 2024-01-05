using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SolarConflict.XnaUtils;
using XnaUtils;
using XnaUtils.Framework.Graphics;
using XnaUtils.Graphics;
using XnaUtils.Input;
using XnaUtils.SimpleGui;

namespace MonoUtils
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private GuiManager _gui;
        private InputManager _inputManager;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            GraphicsSettingsUtils.InitStatic(_graphics);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _inputManager = new InputManager();
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Canvas can = new Canvas(100, 100, _graphics.GraphicsDevice);
            can.Clear(Color.White);
            can.SetData();
            TextureBank.Inst.AddTexture("missing", can.GetTexture());

            _gui = new GuiManager();
            _gui._spriteBatch = _spriteBatch;
            var control = new GuiControl(Vector2.One * 100, new Vector2(150, 100));
            control.CursorOverColor = Color.Yellow;

            control.Sprite = new Sprite("missing");
            _gui.Root = control;
            
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            _inputManager.Update(gameTime, this);

            _gui.Update(_inputManager.InputState);
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _gui.Draw();
            base.Draw(gameTime);
        }
    }
}