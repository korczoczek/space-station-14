using Content.Shared.Trigger.Components.Effects;
using Robust.Client.GameObjects;

namespace Content.Client.Trigger.Systems;

public sealed class AnimationOnTriggerSystem : EntitySystem
{
    [Dependency] private readonly AnimationPlayerSystem _animation = default!;

    private const string FadeAnimationKey = "trigger-fade";
    private const string WaveAnimationKey = "trigger-wave";
    private const string ShowAnimationKey = "trigger-show";

    public void PlayAnimation()
    {

    }
}
