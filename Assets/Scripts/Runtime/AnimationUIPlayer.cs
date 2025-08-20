using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace AnimationUISystem
{
    public class AnimationUIPlayer : MonoBehaviour
    {
        [SerializeField] private AnimationUI[] animationUI;
        [SerializeField] private bool isPlayOnEnable;

        private List<Tween> _tweens = new();

        private void OnEnable()
        {
            if (isPlayOnEnable)
                Play();
        }

        private void OnDisable()
        {
            if (isPlayOnEnable)
                ClearTweens();
        }

        public void Play()
        {
            foreach (var anim in animationUI)
            {
                if (anim.NullCheck())
                {
                    Debug.LogError("AnimationUI.NullCheck failed");
                    continue;
                }

                anim.AssignTarget();
                anim.SetTweenStart();
                _tweens.AddRange(anim.CreateTween());
            }
        }

        public void ClearTweens()
        {
            foreach (var tween in _tweens)
            {
                tween?.Kill();
            }

            _tweens.Clear();
        }

        public List<Tween> GetTweens() => _tweens;
    }
}