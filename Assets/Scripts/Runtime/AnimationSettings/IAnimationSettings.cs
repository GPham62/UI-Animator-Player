using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace AnimationUISystem
{
    public interface IAnimationSettings
    {
        void AssignTarget(Component target);
        void SetTweenStart();
        List<Tween> CreateTween();
    }
}