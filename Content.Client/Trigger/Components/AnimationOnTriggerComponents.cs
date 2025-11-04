using Content.Client.Trigger.Systems;
using Robust.Client.Animations;
using Robust.Shared.Audio;

namespace Content.Client.Trigger.Components;

/// <summary>
/// Shows a quick animation. Has a fadeout by default.
/// </summary>
[RegisterComponent]
public sealed partial class AnimationOnTriggerComponent : Component
{
    [DataField("animation")]
    public AnimationType Animation = AnimationType.Show;

    [ViewVariables(VVAccess.ReadWrite), DataField("fadeOut")]
    public bool Fadeout = true;

    public enum AnimationType : byte
    {
        Show,
        Wave,
        Slash,
    }
}
