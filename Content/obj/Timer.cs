using System;
using System.Threading;
using static TestGame.Content.obj.Config;

namespace TestGame.Content.obj;

public class Timer
{
    private double _elapsedTime;
    private bool _keepGoing;
    
    public Timer()
    {
        _elapsedTime = double.IsNaN(TimerStartValue) ? 0 : TimerStartValue;
        _keepGoing = true;
        var thread = new Thread(Start);
        thread.Start();
    }

    private void Start()
    {
        while (_keepGoing)
        {
            Thread.Sleep(TimerUpdateIntervalMs);
            _elapsedTime += (double)TimerUpdateIntervalMs/1000;
        }
    }

    public void Stop()
    {
        _keepGoing = false;
        TimerStartValue = _elapsedTime;
    }
}