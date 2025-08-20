using System;

namespace AnimationUISystem
{
    public enum TweenOption
    {
        None,
        Image,
        RectTransform,
        CanvasGroup,
        Text
    }
    
    [Flags]
    public enum RectOption
    {
        AnchoredPosition = 1 << 0,
        LocalScale = 1 << 1,
        Rotate = 1 << 2,
    }
    
    [Flags]
    public enum ImageOption
    {
        Color = 1 << 0,
        FillAmount = 1 << 1,
    }
}