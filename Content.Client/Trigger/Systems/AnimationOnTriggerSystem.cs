using Content.Client.Animations;
using Content.Shared.Trigger;
using Content.Shared.Trigger.Components.Effects;
using Robust.Client.Animations;
using Robust.Client.GameObjects;
//using Robust.Shared.Timing;
using Robust.Shared.Map;
using System.Numerics;

namespace Content.Client.Trigger.Systems;

public sealed class AnimationOnTriggerSystem : EntitySystem
{
    [Dependency] private readonly AnimationPlayerSystem _animation = default!;
    [Dependency] private readonly SpriteSystem _sprite = default!;

    private EntityQuery<TransformComponent> _xformQuery;

    private const string FadeAnimationKey = "trigger-fade";
    private const string WaveAnimationKey = "trigger-wave";
    private const string ShowAnimationKey = "trigger-show";

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<AnimationOnTriggerComponent, TriggerEvent>(PlayAnimation);
    }
    public void PlayAnimation(EntityUid uid, AnimationOnTriggerComponent component, ref TriggerEvent args)
    {
        /*if (!Timing.IsFirstTimePredicted)
            return;*/
        if (args.User == null)
            return;

        EntityUid user = args.User.Value;

        if (!_xformQuery.TryGetComponent(user, out var userXform) || userXform.MapID == MapId.Nullspace)
            return;

        var animationUid = Spawn("AnimationOnTrigger", userXform.Coordinates);

        if (!TryComp<SpriteComponent>(animationUid, out var sprite)
            || !TryComp<AnimationOnTriggerComponent>(animationUid, out var animationComponent))
        {
            return;
        }

        if (animationComponent.Animation != AnimationType.None)
        {
            if (user != uid
                && TryComp(uid, out SpriteComponent? itemSpriteComponent))
                _sprite.CopySprite((uid, itemSpriteComponent), (animationUid, sprite));
        }

        var xform = _xformQuery.GetComponent(animationUid);
        TrackUserComponent track;

        switch (animationComponent.Animation)
        {
            case AnimationType.Wave:
                track = EnsureComp<TrackUserComponent>(animationUid);
                track.User = user;
                _animation.Play(animationUid, GetWaveAnimation(sprite, 135.0f, 0.0f), WaveAnimationKey);
                if (animationComponent.Fadeout)
                    _animation.Play(animationUid, GetFadeAnimation(sprite, 0.065f, 0.065f + 0.05f), FadeAnimationKey);
                break;
            case AnimationType.Show:
                track = EnsureComp<TrackUserComponent>(animationUid);
                track.User = user;
                _animation.Play(animationUid, GetShowAnimation(sprite, 1.0f, 0.0f), ShowAnimationKey);
                if (animationComponent.Fadeout)
                    _animation.Play(animationUid, GetFadeAnimation(sprite, 0.05f, 0.15f), FadeAnimationKey);
                break;
            case AnimationType.None:
                break;
        }
    }

    private Animation GetWaveAnimation(SpriteComponent sprite, Angle arc, Angle spriteRotation)
    {
        const float waveStart = 0.03f;
        const float waveEnd = 0.065f;
        const float length = waveEnd + 0.05f;
        var startRotation = sprite.Rotation + arc / 2;
        var endRotation = sprite.Rotation - arc / 2;
        var startRotationOffset = startRotation.RotateVec(new Vector2(0f, -1f));
        var endRotationOffset = endRotation.RotateVec(new Vector2(0f, -1f));
        startRotation += spriteRotation;
        endRotation += spriteRotation;

        return new Animation()
        {
            Length = TimeSpan.FromSeconds(length),
            AnimationTracks =
            {
                new AnimationTrackComponentProperty()
                {
                    ComponentType = typeof(SpriteComponent),
                    Property = nameof(SpriteComponent.Rotation),
                    KeyFrames =
                    {
                        new AnimationTrackProperty.KeyFrame(startRotation, 0f),
                        new AnimationTrackProperty.KeyFrame(startRotation, waveStart),
                        new AnimationTrackProperty.KeyFrame(endRotation, waveEnd)
                    }
                },
                new AnimationTrackComponentProperty()
                {
                    ComponentType = typeof(SpriteComponent),
                    Property = nameof(SpriteComponent.Offset),
                    KeyFrames =
                    {
                        new AnimationTrackProperty.KeyFrame(startRotationOffset, 0f),
                        new AnimationTrackProperty.KeyFrame(startRotationOffset, waveStart),
                        new AnimationTrackProperty.KeyFrame(endRotationOffset, waveEnd)
                    }
                },
            }
        };
    }

    private Animation GetShowAnimation(SpriteComponent sprite, Angle arc, Angle spriteRotation)
    {
        return new Animation
        {

        };
    }

    private Animation GetFadeAnimation(SpriteComponent sprite, float start, float end)
    {
        return new Animation
        {
            Length = TimeSpan.FromSeconds(end),
            AnimationTracks =
            {
                new AnimationTrackComponentProperty()
                {
                    ComponentType = typeof(SpriteComponent),
                    Property = nameof(SpriteComponent.Color),
                    KeyFrames =
                    {
                        new AnimationTrackProperty.KeyFrame(sprite.Color, start),
                        new AnimationTrackProperty.KeyFrame(sprite.Color.WithAlpha(0f), end)
                    }
                }
            }
        };
    }
}
