using System.Threading;

namespace TestGame.Content.obj;

public class Timer
{
    private double _elapsedTime;
    private bool _keepGoing;
    private readonly int _timerUpdateIntervalMs;
    private double _timerStartValue;
    
    public Timer(int timerUpdateIntervalMs, double timerStartValue)
    {
        _elapsedTime = double.IsNaN(_timerStartValue) ? 0 : _timerStartValue;
        _keepGoing = true;
        _timerUpdateIntervalMs = timerUpdateIntervalMs;
        _timerStartValue = timerStartValue;
    }

    public void Start()
    {
        var thread = new Thread(StartThread);
        thread.Start();
    }
    
    private void StartThread()
    {
        while (_keepGoing)
        {
            Thread.Sleep(_timerUpdateIntervalMs);
            _elapsedTime += (double)_timerUpdateIntervalMs/1000;
        }
    }

    public void Stop()
    {
        _keepGoing = false;
        _timerStartValue = _elapsedTime;
    }

    public int GetTimerUpdateInterval()
    {
        return _timerUpdateIntervalMs;
    }

    public double GetTimerStartValue()
    {
        return _timerStartValue;
    }
}