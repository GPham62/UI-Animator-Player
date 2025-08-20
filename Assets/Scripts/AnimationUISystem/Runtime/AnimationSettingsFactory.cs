using System.Collections.Generic;
using UnityEngine;

namespace AnimationUISystem
{
    public static class AnimationSettingsFactory
    {
        public static IAnimationSettings CreateSettings(TweenOption option)
        {
            return option switch
            {
                TweenOption.RectTransform => new RectTransformAnimationSettings(),
                TweenOption.Image => new ImageAnimationSettings(),
                TweenOption.CanvasGroup => new CanvasGroupAnimationSettings(),
                TweenOption.Text => new TextAnimationSettings(),
                _ => null,
            };
        }
    }
}