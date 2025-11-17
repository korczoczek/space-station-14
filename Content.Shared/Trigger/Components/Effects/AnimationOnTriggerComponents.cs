using Robust.Shared.GameStates;

namespace Content.Shared.Trigger.Components.Effects;

/// <summary>
/// Shows an item animation. Has a fadeout by default.
/// </summary>
[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
public sealed partial class AnimationOnTriggerComponent : Component
{
    [DataField("animation"), AutoNetworkedField]
    public AnimationType Animation = AnimationType.None;

    [ViewVariables(VVAccess.ReadWrite), DataField("fadeOut"), AutoNetworkedField]
    public bool Fadeout = true;

    /// <summary>
    /// Used for multi-state animations
    /// </summary>
    [DataField, AutoNetworkedField]
    public int State = 0;

    /// <summary>
    /// Data needed to perform an animation
    /// </summary>
    [DataField, AutoNetworkedField]
    public List<int> Data;
}

public enum AnimationType : byte
{
    None,
    Show,
    Wave,
}

