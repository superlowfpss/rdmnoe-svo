using Content.Server.Damage.Components;
using Content.Server.Popups;
using Content.Shared.Backmen.Surgery.Wounds;
using Content.Shared.Damage;
using Robust.Shared.Player;
using Robust.Shared.Random;

namespace Content.Server.Damage.Systems;

/// <summary>
/// Outputs a random pop-up from the strings list when an object receives damage
/// </summary>
public sealed class DamageRandomPopupSystem : EntitySystem
{
    [Dependency] private readonly PopupSystem _popupSystem = default!;
    [Dependency] private readonly IRobustRandom _random = default!;

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<DamageRandomPopupComponent, DamageChangedEvent>(OnDamageChange);
        SubscribeLocalEvent<DamageRandomPopupComponent, WoundsChangedEvent>(OnWoundsChange); // backmen Edit
    }

    private void OnDamageChange(EntityUid uid, DamageRandomPopupComponent component, DamageChangedEvent args)
    {
        _popupSystem.PopupEntity(Loc.GetString(_random.Pick(component.Popups)), uid);
    }

    // backmen edit start
    private void OnWoundsChange(EntityUid uid, DamageRandomPopupComponent component, WoundsChangedEvent args)
    {
        _popupSystem.PopupEntity(Loc.GetString(_random.Pick(component.Popups)), uid);
    }
    // backmen edit end
}
