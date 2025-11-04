using Robust.Client.Animations;
using Robust.Shared.Animations;

namespace Content.Client.Trigger.Systems;

public sealed class AnimationOnTriggerSystem : XOnTriggerSystem<AnimationOnTriggerComponent>
{
    [Dependency] private readonly AnimationPlayerSystem _animation = default!;

    private const string FadeAnimationKey = "trigger-fade";
    private const string SlashAnimationKey = "trigger-slash";
    private const string WaveAnimationKey = "trigger-wave";
    private const string ShowAnimationKey = "trigger-show";

    protected override void OnTrigger(Entity<AnimationOnTriggerComponent> ent, EntityUid target, ref TriggerEvent args)
    {

    }
}
