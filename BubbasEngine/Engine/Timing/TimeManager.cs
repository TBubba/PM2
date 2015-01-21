﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace BubbasEngine.Engine.Timing
{
    internal class TimeManager
    {
        // Private
        private Stopwatch _watch;
        private TimeKeeper _keeper;

        private double[] _frames;
        private int _currentFrame;
        private double _frameAverage;

        private bool _accumuate;

        // Constructor(s)
        internal TimeManager(TimeManagerArgs args)
        {
            _watch = new Stopwatch();
            _keeper = new TimeKeeper(args.StepsPerSecond);

            _frames = new double[args.StepsPerSecond];
            _accumuate = args.Accumulate;
        }

        // Frame
        internal void BeginFrame()
        {
            // Update keepers
            _keeper.Step(_frames[_currentFrame]);

            // Calculate the average frame time
            _frameAverage = _frames.Average();

            // Register the frame time
            _currentFrame++;
            if (_currentFrame >= _frames.Length)
                _currentFrame = 0;

            _frames[_currentFrame] = _watch.Elapsed.TotalMilliseconds;

            // Restart the watch
            _watch.Restart();
        }

        // Step
        internal int GetStepCount()
        {
            // Get step count
            int count = _keeper.GetStepCount();

            // Return count
            if (_accumuate)
                return count;
            return (count == 0) ? 0 : 1;
        }

        //
        internal double LastFrameTime()
        {
            return _frames[_currentFrame];
        }

        //
        internal double TimeSinceStep()
        {
            return _keeper.Accumulator;
        }

        //
        internal int GetGoalFPS()
        {
            return _keeper.StepsPerSec;
        }
    }
}
