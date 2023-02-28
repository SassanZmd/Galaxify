using System.Collections.Generic;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TestGame.Content.obj;
using TestGame.Content.obj.Config;
using Timer = TestGame.Content.obj.Timer;

namespace TestGame;

public class Game1 : Game
{
    private readonly Config _config;
    private readonly Graphics _graphicsConfig;
    private Timer _timer;
    private Ball _ball;
    private Science _science;
    private Politics _politics;

    private List<Texture2D> _loadingTextureList;
    private bool _waitForLoading;
    private int _frameCounter;

    private SpriteBatch _spriteBatch;
    private SpriteFont _font;
    private RenderTarget2D _target;

    public Game1()
    {
        _config = new Config();
        var graphics = new GraphicsDeviceManager(this);
        _graphicsConfig = _config.GetGraphics();
        (graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight) = _graphicsConfig.GetResolution();
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        if (!_config.FinishedLoading)
        {
            base.Initialize();
            return;
        }
        
        _timer = _config.GetTimer();
        _ball = _config.GetBall();
        _science = _config.GetScience();
        _politics = _config.GetPolitics();

        _timer.Start();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        if (!_config.FinishedLoading)
        {
            _loadingTextureList = new List<Texture2D>();
            var loadingTextureName = _config.GetLoadingTextureName();
            var loadingTextureFrames = _config.GetLoadingTextureFrames();
            for (var i = 1; i <= loadingTextureFrames; i++)
            {
                _loadingTextureList.Add(Content.Load<Texture2D>(loadingTextureName + "-" + i));
            }
            return;
        }

        var ballTextureName = _ball.GetTextureName();
        _ball.SetTexture(Content.Load<Texture2D>(ballTextureName));

        var scienceTextureName = _science.GetTextureName();
        _science.SetTexture(Content.Load<Texture2D>(scienceTextureName));
        _science.Spawn(_ball);

        var politicsTextureName = _politics.GetTextureName();
        _politics.SetTexture(Content.Load<Texture2D>(politicsTextureName));

        _font = Content.Load<SpriteFont>("font1");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed 
            || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Quit();

        if (!_config.FinishedLoading)
        {
            _waitForLoading = true;
            return;
        }

        if (_waitForLoading)
        {
            _waitForLoading = false;
            Initialize();
            LoadContent();
        }

        if (_science.IsDestroyed())
        {
            _science.DelayedSpawn(_ball);
        }
        
        SetKeyboard(gameTime);
        SetBounds();

        if (_ball.Collision(_science))
        {
            _science.SelfDestruct();
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        if (!_config.FinishedLoading)
        {
            GraphicsDevice.Clear(Color.Black);
            DrawLoading();
            
            base.Draw(gameTime);
            return;
        }
        
        GraphicsDevice.Clear(_graphicsConfig.GetColor());

        DrawText();
        DrawScience();
        DrawBall();

        base.Draw(gameTime);
    }

    private void SetKeyboard(GameTime gameTime)
    {
        var keyState = Keyboard.GetState();
        float speed;
        
        if (((keyState.IsKeyDown(Keys.Up) || keyState.IsKeyDown(Keys.W)) 
             && (keyState.IsKeyDown(Keys.Left) || keyState.IsKeyDown(Keys.A) || keyState.IsKeyDown(Keys.Right) 
                                               || keyState.IsKeyDown(Keys.D))) 
            || (keyState.IsKeyDown(Keys.Down) || keyState.IsKeyDown(Keys.S)) 
            && (keyState.IsKeyDown(Keys.Left) 
                || keyState.IsKeyDown(Keys.A) || keyState.IsKeyDown(Keys.Right) || keyState.IsKeyDown(Keys.D)) 
            && !((keyState.IsKeyDown(Keys.Right) || keyState.IsKeyDown(Keys.D)) 
                 && (keyState.IsKeyDown(Keys.Left) || keyState.IsKeyDown(Keys.A))))
        {
            speed = _ball.GetDiagonalSpeed();
        }
        else speed = _ball.GetSpeed();

        var ballPos = _ball.GetPosition();
        
        if (keyState.IsKeyDown(Keys.Up) || keyState.IsKeyDown(Keys.W))
        {
            ballPos.Y -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        if (keyState.IsKeyDown(Keys.Down) || keyState.IsKeyDown(Keys.S))
        {
            ballPos.Y += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        if (keyState.IsKeyDown(Keys.Right) || keyState.IsKeyDown(Keys.D))
        {
            ballPos.X += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        if (keyState.IsKeyDown(Keys.Left) || keyState.IsKeyDown(Keys.A))
        {
            ballPos.X -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        _ball.SetPosition(ballPos);
    }

    private void SetBounds()
    {
        var ballPos = _ball.GetPosition();
        var ballTexture = _ball.GetTexture();
        var ballScale = _ball.GetScale();
        var (resolutionWidth, resolutionHeight) = _graphicsConfig.GetResolution();
        
        if (ballPos.X > resolutionWidth - ballTexture.Width * ballScale/2)
        {
            ballPos.X = resolutionWidth - ballTexture.Width * ballScale/2;
        }
        if (ballPos.X < (float)ballTexture.Width/2 * ballScale)
        {
            ballPos.X = (float)ballTexture.Width/2 * ballScale;
        }
        if (ballPos.Y > resolutionHeight - (float)ballTexture.Height/2 * ballScale)
        {
            ballPos.Y = resolutionHeight - (float)ballTexture.Height/2 * ballScale;
        }
        if (ballPos.Y < (float)ballTexture.Height/2 * ballScale)
        {
            ballPos.Y = (float)ballTexture.Height/2 * ballScale;
        }
        
        _ball.SetPosition(ballPos);
    }

    private void Quit()
    {
        _timer.Stop();
        _config.SaveConfig();
        Exit();
    }

    private void DrawBall()
    {
        var ballTexture = _ball.GetTexture();
        var origin = new Vector2((float)ballTexture.Width / 2, (float)ballTexture.Height / 2);

        _spriteBatch.Begin();
        _spriteBatch.Draw(ballTexture, _ball.GetPosition(), null, Color.White, 0f, origin, 
            Vector2.One*_ball.GetScale(), SpriteEffects.None, 0f);
        _spriteBatch.End();
    }

    private void DrawScience()
    {
        var scienceTexture = _science.GetTexture();
        var origin = new Vector2((float)scienceTexture.Width / 2, (float)scienceTexture.Height / 2);
        
        _spriteBatch.Begin();
        _spriteBatch.Draw(scienceTexture, _science.GetPosition(), null, Color.White, 0f, origin, 
            Vector2.One*_science.GetScale(), SpriteEffects.None, 0f);
        _spriteBatch.End();
    }

    private void DrawLoading()
    {
        var (resolutionWidth, resolutionHeight) = _graphicsConfig.GetResolution();
        var origin = new Vector2((float)_loadingTextureList[0].Width / 2, (float)_loadingTextureList[0].Height / 2);

        if (_frameCounter > 3) 
            _frameCounter = 0;
        
        _spriteBatch.Begin();
        _spriteBatch.Draw(_loadingTextureList[_frameCounter], 
            new Vector2((float)resolutionWidth / 2, (float)resolutionHeight / 2), null, 
            Color.White, 0f, origin, Vector2.One*0.25f, SpriteEffects.None, 0f);
        _spriteBatch.End();

        _frameCounter++;
        Thread.Sleep(100);
    }

    private void DrawText()
    {
        _spriteBatch.Begin();
        // Finds the center of the string in coordinates inside the text rectangle
        var textMiddlePoint = _font.MeasureString("Score") / 2;
        // Places text in center of the screen
        var position = new Vector2(Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 2);
        _spriteBatch.DrawString(_font, "Score", position, Color.White, 0, textMiddlePoint,
            1.0f, SpriteEffects.None, 0.5f);
        _spriteBatch.End();
    }
}
