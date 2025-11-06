using Robust.Shared.GameStates;

namespace Content.Shared.Trigger.Components.Effects;

/// <summary>
/// Shows an item animation. Has a fadeout by default.
/// </summary>
[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
public sealed partial class AnimationOnTriggerComponent : BaseXOnTriggerComponent
{
    [DataField("animation"), AutoNetworkedField]
    public AnimationType Animation = AnimationType.Show;

    [ViewVariables(VVAccess.ReadWrite), DataField("fadeOut"), AutoNetworkedField]
    public bool Fadeout = true;

    [DataField, AutoNetworkedField]
    public int State = 0;

    public enum AnimationType : byte
    {
        Show,
        Wave,
    }
}
