using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TestGame.Content.obj;
using static TestGame.Content.obj.Config;

namespace TestGame;

public class Game1 : Game
{
    private Timer _timer;
    private Ball _ball;
    private Science _science;
    private Politics _politics;

    private SpriteBatch _spriteBatch;

    public Game1()
    {
        var graphics = new GraphicsDeviceManager(this);
        graphics.PreferredBackBufferWidth = ResolutionWidth;
        graphics.PreferredBackBufferHeight = ResolutionHeight;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _timer = new Timer();
        _ball = new Ball();
        _science = new Science();
        _politics = new Politics();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _ball.SetTexture(Content.Load<Texture2D>(BallTexture));
        _science.SetTexture(Content.Load<Texture2D>(ScienceTexture));
        _science.Spawn(_ball);
        _politics.SetTexture(Content.Load<Texture2D>(PoliticsTexture));
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed 
            || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Quit();

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
        GraphicsDevice.Clear(BackgroundColor);

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
        
        if (ballPos.X > ResolutionWidth - ballTexture.Width * ballScale/2)
        {
            ballPos.X = ResolutionWidth - ballTexture.Width * ballScale/2;
        }
        if (ballPos.X < (float)ballTexture.Width/2 * ballScale)
        {
            ballPos.X = (float)ballTexture.Width/2 * ballScale;
        }
        if (ballPos.Y > ResolutionHeight - (float)ballTexture.Height/2 * ballScale)
        {
            ballPos.Y = ResolutionHeight - (float)ballTexture.Height/2 * ballScale;
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
}
