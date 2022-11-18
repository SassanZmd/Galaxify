using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TestGame.Content.obj;

namespace TestGame;

public class Game1 : Game
{
    private Ball _ball;
    
    private readonly GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        InitBall();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _ball.SetTexture(Content.Load<Texture2D>("ball-pixel"));
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed 
            || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        SetKeyboard(gameTime);
        SetBounds();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Tan);

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
    }

    private void SetBounds()
    {
        var ballPos = _ball.GetPosition();
        var ballTexture = _ball.GetTexture();
        var ballScale = _ball.GetScale();
        
        if (ballPos.X > _graphics.PreferredBackBufferWidth - ballTexture.Width * ballScale/5)
        {
            ballPos.X = _graphics.PreferredBackBufferWidth - ballTexture.Width * ballScale/5;
        }
        if (ballPos.X < (float)ballTexture.Width/5 * ballScale)
        {
            ballPos.X = (float)ballTexture.Width/5 * ballScale;
        }
        if (ballPos.Y > _graphics.PreferredBackBufferHeight - (float)ballTexture.Height/5 * ballScale + 5)
        {
            ballPos.Y = _graphics.PreferredBackBufferHeight - (float)ballTexture.Height/5 * ballScale + 5;
        }
        if (ballPos.Y < (float)ballTexture.Height/5 * ballScale + 5)
        {
            ballPos.Y = (float)ballTexture.Height/5 * ballScale + 5;
        }
    }

    private void InitBall()
    {
        var ballPos = new Vector2((float)_graphics.PreferredBackBufferWidth / 2,
            (float)_graphics.PreferredBackBufferHeight / 2);
        const float ballSpeed = 300f, ballScale = 0.1f;
        var ballDiagonalSpeed = (float)Math.Sqrt(Math.Pow(_ball.GetSpeed(), 2) * 2) / 2;
        
        _ball = new Ball(ballPos, ballSpeed, ballDiagonalSpeed, ballScale);
    }

    private void DrawBall()
    {
        var ballTexture = _ball.GetTexture();
        var origin = new Vector2((float)ballTexture.Width / 2, (float)ballTexture.Height / 2);

        _spriteBatch.Begin();
        _spriteBatch.Draw(ballTexture, _ball.GetPosition(), null, Color.White, 0f, 
            origin, Vector2.One*_ball.GetScale(), SpriteEffects.None, 0f);
        _spriteBatch.End();
    }
}
