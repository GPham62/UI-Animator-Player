using System;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace AnimationUISystem.AnimationSettings
{
    [Serializable]
    public class TextAnimationSettings : IAnimationSettings
    {
        [SerializeField, BoxGroup("Text"), HideLabel]
        private AnimationValue<string> textAnimValue = new();
        private TMP_Text _targetText;
        
        public void AssignTarget(Component target)
        {
            _targetText = target as TMP_Text;
        }

        public void SetTweenStart()
        {
            _targetText.text = textAnimValue.start;
        }

        public List<Tween> CreateTween()
        {
            var result = new List<Tween>();
            var tweenText = _targetText.DOText(textAnimValue.end, textAnimValue.duration).SetDelay(textAnimValue.delay);
            if (textAnimValue.isCustomEase)
                tweenText.SetEase(textAnimValue.customEase);
            else
                tweenText.SetEase(textAnimValue.ease);
            if (textAnimValue.ignoreTimeScale)
                tweenText.SetUpdate(true);
            result.Add(tweenText);
            return result;
        }
    }
}