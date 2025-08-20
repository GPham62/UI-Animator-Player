using System;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AnimationUISystem
{
    [Serializable]
    public class CanvasGroupAnimationSettings : IAnimationSettings
    {
        [SerializeField, BoxGroup("Alpha"), HideLabel]
        private AnimationValue<float> canvasGroupAlpha = new();

        private CanvasGroup _targetCanvasGroup;

        public void AssignTarget(Component target)
        {
            _targetCanvasGroup = target as CanvasGroup;
        }

        public void SetTweenStart()
        {
            _targetCanvasGroup.alpha = canvasGroupAlpha.start;
        }

        public List<Tween> CreateTween()
        {
            var result = new List<Tween>();
            var tweenCanvasGroup = _targetCanvasGroup.DOFade(canvasGroupAlpha.end, canvasGroupAlpha.duration)
                .SetDelay(canvasGroupAlpha.delay);
            if (canvasGroupAlpha.isCustomEase)
                tweenCanvasGroup.SetEase(canvasGroupAlpha.customEase);
            else
                tweenCanvasGroup.SetEase(canvasGroupAlpha.ease);
            if (canvasGroupAlpha.ignoreTimeScale)
                tweenCanvasGroup.SetUpdate(true);
            result.Add(tweenCanvasGroup);
            return result;
        }
    }
}