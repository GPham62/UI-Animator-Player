using System;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AnimationUISystem
{
    [Serializable]
    public class AnimationUI
    {
        [Tooltip("Please Assign Target Component (TMP_Text, Image, RectTransform, CanvasGroup)")]
        [SerializeField, OnValueChanged(nameof(OnTargetChanged)), PropertySpace(16)]
        private Component target;
        [ShowIfGroup("TweenSettings", Condition = nameof(hasTarget))] 
        [BoxGroup("TweenSettings/Settings")]
        [SerializeField, ValueDropdown(nameof(GetAvailableTweenOptions)), OnValueChanged(nameof(OnTweenOptionChanged))]
        private TweenOption tweenOption;
        [BoxGroup("TweenSettings/Settings"), SerializeReference] private IAnimationSettings animationSettings;

        [HideInInspector] public bool hasTarget = false;
        [HideInInspector] public bool hasImage;
        [HideInInspector] public bool hasRectTransform;
        [HideInInspector] public bool hasCanvasGroup;
        [HideInInspector] public bool hasText;
        private IEnumerable<TweenOption> GetAvailableTweenOptions()
        {
            if (!hasTarget) yield break;
            if (hasImage) yield return TweenOption.Image;
            if (hasRectTransform) yield return TweenOption.RectTransform;
            if (hasCanvasGroup) yield return TweenOption.CanvasGroup;
            if (hasText) yield return TweenOption.Text;
        }

        private void OnTweenOptionChanged()
        {
            animationSettings = AnimationSettingsFactory.CreateSettings(tweenOption);
        }

        private void OnTargetChanged()
        {
            hasTarget = target != null;
            if (!hasTarget)
            {
                animationSettings = null;
                return;
            }
            hasImage = target.GetComponent<Image>() != null;
            hasRectTransform = target.GetComponent<RectTransform>() != null;
            hasCanvasGroup = target.GetComponent<CanvasGroup>() != null;
            hasText = target.GetComponent<TMP_Text>() != null;
            animationSettings = null;
            ResetTween();
        }

        private void ResetTween()
        {
            tweenOption = TweenOption.None;
        }

        public bool NullCheck()
        {
            return !hasTarget || animationSettings == null;
        }

        public void AssignTarget()
        {
            Component targetComp = tweenOption switch
            {
                TweenOption.Image => target.GetComponent<Image>(),
                TweenOption.CanvasGroup => target.GetComponent<CanvasGroup>(),
                TweenOption.RectTransform => target.GetComponent<RectTransform>(),
                TweenOption.Text => target.GetComponent<TMP_Text>(),
                _ => null,
            };
            animationSettings.AssignTarget(targetComp);
        }

        public void SetTweenStart()
        {
            animationSettings.SetTweenStart();
        }

        public List<Tween> CreateTween()
        {
            return animationSettings.CreateTween();
        }
    }
}