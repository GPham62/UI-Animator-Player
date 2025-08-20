using System.Threading.Tasks;
using DG.DOTweenEditor;
using DG.Tweening;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace AnimationUISystem.Editor
{
    [CustomEditor(typeof(AnimationUIPlayer))]
    public class AnimationUIPlayerEditor : OdinEditor
    {
        private bool _isPlaying;
        public override void OnInspectorGUI()
        {
            DrawHeader("Animation UI Player");
            EditorGUILayout.Space();
            AnimationUIPlayer tweeningComp = (AnimationUIPlayer)target;
            GUIStyle playTweenStyle = new GUIStyle(GUI.skin.button);
            playTweenStyle.fontSize = 16;
            playTweenStyle.fontStyle = FontStyle.Bold;
            playTweenStyle.alignment = TextAnchor.MiddleCenter;
            var textColor = Color.black;
            playTweenStyle.hover.textColor = textColor;
            playTweenStyle.active.textColor = textColor;
            playTweenStyle.normal.textColor = textColor;
            Texture2D greenTexture = new Texture2D(1, 1);
            greenTexture.SetPixel(0, 0, Color.green);
            greenTexture.Apply();
            playTweenStyle.normal.background = greenTexture;
            playTweenStyle.hover.background = greenTexture;
            playTweenStyle.active.background = greenTexture;
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUI.enabled = !_isPlaying;
            if (GUILayout.Button("Play Tween", playTweenStyle, GUILayout.Width(150), GUILayout.Height(40)))
            {
                tweeningComp.ClearTweens();
                tweeningComp.Play();
                var tweens = tweeningComp.GetTweens();
                float prevDuration = 0;
                foreach (var tween in tweens)
                {
                    DOTweenEditorPreview.PrepareTweenForPreview(tween);
                    DOTweenEditorPreview.Start();
                    float totalDuration = tween.Duration() + tween.Delay();
                    if (prevDuration < totalDuration)
                        prevDuration = totalDuration;
                }

                _isPlaying = true;
                StopPreviewAfterDelay(prevDuration);
            }
            GUI.enabled = true;
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            base.OnInspectorGUI();
        }

        private async void StopPreviewAfterDelay(float duration)
        {
            await Task.Delay((int)duration * 1000);
            DOTweenEditorPreview.Stop();
            _isPlaying = false;
        }

        private void DrawHeader(string text)
        {
            // Reserve a rectangular space
            Rect rect = GUILayoutUtility.GetRect(0, 30, GUILayout.ExpandWidth(true));

            // Draw background
            EditorGUI.DrawRect(rect, new Color(0.15f, 0.15f, 0.15f)); // Dark gray

            // Center label
            GUIStyle style = new GUIStyle(EditorStyles.boldLabel)
            {
                alignment = TextAnchor.MiddleCenter,
                fontSize = 14,
            };
            Color headerColor = new Color(1f, 0.8f, 0.85f);

            style.normal.textColor  = headerColor;
            style.hover.textColor   = headerColor;
            style.active.textColor  = headerColor;
            style.focused.textColor = headerColor;
            
            Rect labelRect = new Rect(rect.x, rect.y, rect.width, rect.height);
            GUI.Label(labelRect, text, style);
        }
    }
}
