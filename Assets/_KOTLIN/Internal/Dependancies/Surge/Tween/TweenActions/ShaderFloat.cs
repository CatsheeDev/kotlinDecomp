﻿/// <summary>
/// SURGE FRAMEWORK
/// Author: Bob Berkebile
/// Email: bobb@pixelplacement.com
/// </summary>

using System;
using UnityEngine;

namespace Pixelplacement.TweenSystem
{
    internal class ShaderFloat : TweenBase
    {
        //Public Properties:
        public float EndValue { get; private set; }

        //Private Variables:
        private Material _target;

        private float _start;
        private string _propertyName;

        //Constructor:
        public ShaderFloat(Material target, string propertyName, float endValue, float duration, float delay, bool obeyTimescale, AnimationCurve curve, Tween.LoopType loop, Action startCallback, Action completeCallback)
        {
            //set essential properties:
            SetEssentials(Tween.TweenType.ShaderFloat, target.GetInstanceID(), duration, delay, obeyTimescale, curve, loop, startCallback, completeCallback);

            //catalog custom properties:
            _target = target;
            _propertyName = propertyName;
            EndValue = endValue;
        }

        //Processes:
        protected override bool SetStartValue()
        {
            _start = _target.GetFloat(_propertyName);
            if (_target == null) return false;
            return true;
        }

        protected override void Operation(float percentage)
        {
            float calculatedValue = TweenUtilities.LinearInterpolate(_start, EndValue, percentage);
            _target.SetFloat(_propertyName, calculatedValue);
        }

        //Loops:
        public override void Loop()
        {
            ResetStartTime();
            _target.SetFloat(_propertyName, _start);
        }

        public override void PingPong()
        {
            ResetStartTime();
            _target.SetFloat(_propertyName, EndValue);
            EndValue = _start;
            _start = _target.GetFloat(_propertyName);
        }
    }
}