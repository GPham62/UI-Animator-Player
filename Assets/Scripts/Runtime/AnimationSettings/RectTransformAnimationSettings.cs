using System;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AnimationUISystem.AnimationSettings
{
    [Serializable]
    public class RectTransformAnimationSettings : IAnimationSettings
    {
        [EnumToggleButtons, SerializeField] private RectOption rectOption;

        [SerializeField, ShowIf("@rectOption.HasFlag(RectOption.AnchoredPosition)"), BoxGroup("Anchored Position"),
         HideLabel]
        private AnimationValue<Vector3> anchoredPosition = new();

        [SerializeField, ShowIf("@rectOption.HasFlag(RectOption.LocalScale)"), BoxGroup("Local Scale"), HideLabel]
        private AnimationValue<Vector3> localScale = new();

        [SerializeField, ShowIf("@rectOption.HasFlag(RectOption.Rotate)"), BoxGroup("Rotate"), HideLabel]
        private AnimationValue<Vector3> rotate = new();

        private RectTransform _targetRectTransform;

        public void AssignTarget(Component target)
        {
            _targetRectTransform = target as RectTransform;
        }

        public void SetTweenStart()
        {
            if (rectOption.HasFlag(RectOption.AnchoredPosition))
                _targetRectTransform.anchoredPosition = anchoredPosition.start;
            if (rectOption.HasFlag(RectOption.LocalScale))
                _targetRectTransform.localScale = localScale.start;
            if (rectOption.HasFlag(RectOption.Rotate))
                _targetRectTransform.eulerAngles = rotate.start;
        }

        public List<Tween> CreateTween()
        {
            var result = new List<Tween>();
            if (_targetRectTransform == null)
                return null;
            if (rectOption.HasFlag(RectOption.AnchoredPosition))
            {
                var tweenAnchoredPosition = _targetRectTransform
                    .DOAnchorPos(anchoredPosition.end, anchoredPosition.duration)
                    .SetDelay(anchoredPosition.delay);
                if (anchoredPosition.isCustomEase)
                    tweenAnchoredPosition.SetEase(anchoredPosition.customEase);
                else
                    tweenAnchoredPosition.SetEase(anchoredPosition.ease);
                if (anchoredPosition.ignoreTimeScale)
                    tweenAnchoredPosition.SetUpdate(true);
                result.Add(tweenAnchoredPosition);
            }

            if (rectOption.HasFlag(RectOption.LocalScale))
            {
                var tweenLocalScale = _targetRectTransform.DOScale(localScale.end, localScale.duration)
                    .SetDelay(localScale.delay);
                if (localScale.isCustomEase)
                    tweenLocalScale.SetEase(localScale.customEase);
                else
                    tweenLocalScale.SetEase(localScale.ease);
                if (localScale.ignoreTimeScale)
                    tweenLocalScale.SetUpdate(true);
                result.Add(tweenLocalScale);
            }

            if (rectOption.HasFlag(RectOption.Rotate))
            {
                var tweenRotate = _targetRectTransform.DORotate(rotate.end, rotate.duration).SetDelay(rotate.delay);
                if (rotate.isCustomEase)
                    tweenRotate.SetEase(rotate.customEase);
                else
                    tweenRotate.SetEase(rotate.ease);
                if (rotate.ignoreTimeScale)
                    tweenRotate.SetUpdate(true);
                result.Add(tweenRotate);
            }

            return result;
        }
    }
}