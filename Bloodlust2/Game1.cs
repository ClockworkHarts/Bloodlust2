using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;

public enum EnemyState { Idle, Patrol, Action, Asleep }
public enum TileType { Dirt, Stone, Water };
public enum TreeType { Pine, Oak, Spruce, Cedar };
public enum PlayerWeapon { Unarmed, Dagger, Sword, Spear };
public enum WeaponType { Dagger, Sword, Spear };

namespace Bloodlust2
{
   
    public class Game1 : Game
    {
        public static Game1 current;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
         

        //camera
        public Camera2D camera = null;
        
        public Game1()
        {
            
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        
        protected override void Initialize()
        {
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 1080;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();

            base.Initialize();
        }

       
        protected override void LoadContent()
        {
            current = this;

            spriteBatch = new SpriteBatch(GraphicsDevice);

            AIE.StateManager.CreateState("SPLASH", new SplashState());
            AIE.StateManager.CreateState("GAME", new GameState());
            AIE.StateManager.CreateState("GAMEOVER", new GameOverState());

            AIE.StateManager.PushState("SPLASH");

            //camera
            var viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, ScreenWidth*2, ScreenHeight*2);
            camera = new Camera2D(viewportAdapter);
            camera.Position = new Vector2(ScreenWidth, ScreenHeight);
            

        }

        protected override void UnloadContent()
        {
            
        }

               
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            AIE.StateManager.Update(Content, gameTime);

            base.Update(gameTime);
        }

       
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            AIE.StateManager.Draw(spriteBatch);

            base.Draw(gameTime);
        }

        public int ScreenWidth
        {
            get { return graphics.GraphicsDevice.Viewport.Width; }
        }
        public int ScreenHeight
        {
            get { return graphics.GraphicsDevice.Viewport.Height; }
        }
    }
}
