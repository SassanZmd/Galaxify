using System;
using System.IO;
using System.Text.Json.Nodes;
using System.Threading;
using Microsoft.Xna.Framework;

namespace TestGame.Content.obj.Config;

public class Config
{
    public bool FinishedLoading;
    private JsonNode _configFileJson;
    
    // Config file defaults
    private const string ConfigFileAddress = "Config.json";

    // Graphics config
    private Graphics _graphicsConfig;

    // Timer Config
    private Timer _timerConfig;

    // Ball sprite config
    private Ball _ball;

    // Science sprite config
    private Science _science;
    
    // Politics sprite config
    private Politics _politics;

    public Config()
    {
        FinishedLoading = false;
        
        var json = JsonNode.Parse(File.ReadAllText(ConfigFileAddress));
        if (json is not JsonObject) throw new Exception("Invalid config file");

        // graphics setup
        if (json["graphics"]?["resolutionWidth"] == null || json["graphics"]["resolutionHeight"] == null 
                                                         || json["graphics"]["backgroundColor"] == null)
            throw new Exception("Invalid 'graphics' configuration");
        var resolutionWidth = json["graphics"]["resolutionWidth"].GetValue<int>();
        var resolutionHeight = json["graphics"]["resolutionHeight"].GetValue<int>();
        var color = System.Drawing.Color.FromName(json["graphics"]["backgroundColor"].GetValue<string>());
        var backgroundColor = new Color(color.R, color.G, color.B, color.A);
        _graphicsConfig = new Graphics(resolutionWidth, resolutionHeight, backgroundColor);
        
        var thread = new Thread(LoadConfig);
        thread.Start();
    }

    private void LoadConfig()
    {
        var json = JsonNode.Parse(File.ReadAllText(ConfigFileAddress));
        if (json is not JsonObject) throw new Exception("Invalid config file");

        var (resolutionWidth, resolutionHeight) = _graphicsConfig.GetResolution();

        // timer setup
        if (json["timer"]?["timerUpdateIntervalMs"] == null || json["timer"]["timerStartValue"] == null)
            throw new Exception("Invalid 'timer' configuration");
        var timerUpdateIntervalMs = json["timer"]["timerUpdateIntervalMs"].GetValue<int>();
        var timerStartValue = json["timer"]["timerStartValue"].GetValue<double>();
        _timerConfig = new Timer(timerUpdateIntervalMs, timerStartValue);

        // ball setup
        if (json["ball"]?["ballTexture"] == null || json["ball"]["ballSpeed"] == null
                                                 || json["ball"]["ballScale"] == null
                                                 || json["ball"]["ballCollisionOffset"] == null)
            throw new Exception("Invalid 'ball' configuration");
        var ballTextureName = json["ball"]["ballTexture"].GetValue<string>();
        var ballPosition = new Vector2((float)resolutionWidth / 2, (float)resolutionHeight / 2);
        var ballSpeed = json["ball"]["ballSpeed"].GetValue<float>();
        var ballDiagonalSpeed = (float)Math.Sqrt(Math.Pow(ballSpeed, 2) * 2) / 2;
        var ballScale = json["ball"]["ballScale"].GetValue<float>();
        var ballCollisionOffset = json["ball"]["ballCollisionOffset"].GetValue<float>();
        _ball = new Ball(ballTextureName, ballPosition, ballSpeed, ballScale, ballDiagonalSpeed, 
            ballCollisionOffset);
        
        // science setup
        if (json["science"]?["scienceTexture"] == null || json["science"]["scienceSpeed"] == null
                                                       || json["science"]["scienceScale"] == null
                                                       || json["science"]["scienceCollisionOffset"] == null
                                                       || json["science"]["scienceDelayMs"] == null)
            throw new Exception("Invalid 'science' configuration");
        var scienceTextureName = json["science"]["scienceTexture"].GetValue<string>();
        var scienceSpeed = json["science"]["scienceSpeed"].GetValue<float>();
        var scienceScale = json["science"]["scienceScale"].GetValue<float>();
        var scienceCollisionOffset = json["science"]["scienceCollisionOffset"].GetValue<float>();
        var scienceDelayMs = json["science"]["scienceDelayMs"].GetValue<int>();
        _science = new Science(scienceTextureName, scienceSpeed, scienceScale, scienceCollisionOffset, scienceDelayMs, 
            resolutionWidth, resolutionHeight);
        
        // politics setup
        if (json["politics"]?["politicsTexture"] == null || json["politics"]["politicsSpeed"] == null 
                                                         || json["politics"]["politicsScale"] == null 
                                                         || json["politics"]["politicsCollisionOffset"] == null 
                                                         || json["politics"]["politicsDelayMs"] == null) 
            throw new Exception("Invalid 'politics' configuration");
        var politicsTextureName = json["politics"]["politicsTexture"].GetValue<string>();
        var politicsSpeed = json["politics"]["politicsSpeed"].GetValue<float>();
        var politicsScale = json["politics"]["politicsScale"].GetValue<float>();
        var politicsCollisionOffset = json["politics"]["politicsCollisionOffset"].GetValue<float>();
        var politicsDelayMs = json["politics"]["politicsDelayMs"].GetValue<int>();
        _politics = new Politics(politicsTextureName, politicsSpeed, politicsScale, politicsCollisionOffset, 
            politicsDelayMs);

        _configFileJson = json;
        FinishedLoading = true;
    }

    public void SaveConfig()
    {
        var json = _configFileJson;

        (json["graphics"]!["resolutionWidth"], json["graphics"]["resolutionHeight"]) = _graphicsConfig.GetResolution();
        json["graphics"]["backgroundColor"] = _graphicsConfig.GetColorName();

        json["timer"]!["timerUpdateIntervalMs"] = _timerConfig.GetTimerUpdateInterval();
        json["timer"]["timerStartValue"] = _timerConfig.GetTimerStartValue();

        json["ball"]!["ballTexture"] = _ball.GetTextureName();
        json["ball"]["ballSpeed"] = _ball.GetSpeed();
        json["ball"]["ballScale"] = _ball.GetScale();
        json["ball"]["balCollisionOffset"] = _ball.GetCollisionOffset();

        json["science"]!["scienceTexture"] = _science.GetTextureName();
        json["science"]["scienceSpeed"] = _science.GetSpeed();
        json["science"]["scienceScale"] = _science.GetScale();
        json["science"]["scienceCollisionOffset"] = _science.GetCollisionOffset();
        json["science"]["scienceDelayMs"] = _science.GetDelayMs();

        json["politics"]!["politicsTexture"] = _politics.GetTextureName();
        json["politics"]["politicsSpeed"] = _politics.GetSpeed();
        json["politics"]["politicsScale"] = _politics.GetScale();
        json["politics"]["politicsCollisionOffset"] = _politics.GetCollisionOffset();
        json["politics"]["politicsDelayMs"] = _politics.GetDelayMs();

        File.WriteAllText("Config.json", json.ToJsonString());
    }

    public Graphics GetGraphics()
    {
        return _graphicsConfig;
    }

    public Timer GetTimer()
    {
        return _timerConfig;
    }

    public Ball GetBall()
    {
        return _ball;
    }

    public Science GetScience()
    {
        return _science;
    }

    public Politics GetPolitics()
    {
        return _politics;
    }
}