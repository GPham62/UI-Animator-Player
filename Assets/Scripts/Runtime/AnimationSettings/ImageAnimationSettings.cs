using System;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace AnimationUISystem.AnimationSettings
{
    [Serializable]
    public class ImageAnimationSettings : IAnimationSettings
    {
        [EnumToggleButtons, SerializeField] private ImageOption imageOption;

        [SerializeField, ShowIf("@imageOption.HasFlag(ImageOption.Color)"), BoxGroup("Color"), HideLabel]
        private AnimationValue<Color> color = new();

        [SerializeField, ShowIf("@imageOption.HasFlag(ImageOption.FillAmount)"), BoxGroup("FillAmount"), HideLabel]
        private AnimationValue<float> fillAmount = new();

        private Image _targetImg;

        public void AssignTarget(Component target)
        {
            _targetImg = target as Image;
        }

        public void SetTweenStart()
        {
            if (imageOption.HasFlag(ImageOption.Color))
                _targetImg.color = color.start;

            if (imageOption.HasFlag(ImageOption.FillAmount))
                _targetImg.fillAmount = fillAmount.start;
        }

        public List<Tween> CreateTween()
        {
            var result = new List<Tween>();
            if (_targetImg == null)
                return null;
            if (imageOption.HasFlag(ImageOption.Color))
            {
                var tweenColor = _targetImg.DOColor(color.end, color.duration).SetDelay(color.delay);
                if (color.isCustomEase)
                    tweenColor.SetEase(color.customEase);
                else
                    tweenColor.SetEase(color.ease);
                if (color.ignoreTimeScale)
                    tweenColor.SetUpdate(true);
                result.Add(tweenColor);
            }

            if (imageOption.HasFlag(ImageOption.FillAmount))
            {
                var tweenFill = _targetImg.DOFillAmount(fillAmount.end, fillAmount.duration).SetDelay(fillAmount.delay);
                if (fillAmount.isCustomEase)
                    tweenFill.SetEase(fillAmount.customEase);
                else
                    tweenFill.SetEase(fillAmount.ease);
                if (fillAmount.ignoreTimeScale)
                    tweenFill.SetUpdate(true);
                result.Add(tweenFill);
            }

            return result;
        }
    }
}