using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AnimationUISystem
{
    [Serializable]
    public class AnimationValue<T>
    {
        [SerializeField] public T start;
        [SerializeField] public T end;
        public bool isCustomEase;
        [HideIf(nameof(isCustomEase))]
        public Ease ease = Ease.Linear;
        [ShowIf(nameof(isCustomEase))]
        public AnimationCurve customEase;
        public float duration = 0;
        public float delay = 0;
        public bool ignoreTimeScale;
    }
}