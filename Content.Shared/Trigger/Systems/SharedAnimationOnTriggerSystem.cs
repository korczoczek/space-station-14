
using Content.Shared.Trigger.Components.Effects;

namespace Content.Shared.Trigger.Systems;

public abstract class SharedAnimationOnTriggerSystem : XOnTriggerSystem<AnimationOnTriggerComponent>
{
    protected override void OnTrigger(Entity<AnimationOnTriggerComponent> ent, EntityUid target, ref TriggerEvent args)
    {
        PlayAnimation();
    }
}
