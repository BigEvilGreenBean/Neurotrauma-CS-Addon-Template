using Barotrauma.Items.Components;
using FarseerPhysics.Dynamics;

namespace MyAddon;

public class OnDamaged
{
    public static readonly Dictionary<string, Action<Character, float, LimbType>> OnDamagedMethods = new();

    public static readonly List<Func<CharacterHealth, List<Affliction>, Limb, List<Affliction>>> ModifyingOnDamagedHooks = new();

    public static readonly List<Action<CharacterHealth, AttackResult, Limb>> OnDamagedHooks = new();

    private static bool HasLungs(Character C) => !(HF.HasAffliction(C, "lungremoved"));

    private static bool HasHeart(Character C) => !(HF.HasAffliction(C, "heartremoved"));

    /// <summary>
    /// Reduces Concussion amount based on worn armor.
    /// </summary>
    /// <param name="Armor">Item ID of worn armor.</param>
    /// <param name="Strength">Amount of Strength of the Concussion Affliction.</param>
    /// <returns></returns>
    public static float GetCalculatedConcussionReduction(Item Armor, float Strength)
    {
        if (Armor == null)
        {
            return 0f;
        }

        if (!Armor.HasTag("deepdiving") &&
            !Armor.HasTag("deepdivinglarge") &&
            !Armor.HasTag("smallitem"))
        {
            return 0f;
        }

        var wearable = Armor.GetComponent<Wearable>();
        if (wearable == null)
        {
            return 0f;
        }

        foreach (var modifier in wearable.DamageModifiers)
        {
            if (modifier.AfflictionIdentifiers.Contains("concussion"))
            {
                return Strength - (Strength * modifier.DamageMultiplier);
            }
        }

        return 0f;
    }

    public static void Override_DamageLimb(
    Character __instance,
    Vector2 worldPosition,
    Limb hitLimb,
    IEnumerable<Affliction> afflictions,
    float stun,
    bool playSound,
    Vector2 attackImpulse,
    Character ?attacker = null,
    float damageMultiplier = 1f,
    bool allowStacking = true,
    float penetration = 0f,
    bool shouldImplode = false,
    bool ignoreDamageOverlay = false,
    bool recalculateVitality = true)
    {
        // Confirm the attack data is valid.
        if (__instance == null || __instance.IsDead || !(__instance.IsHuman) ||
            afflictions == null ||
            hitLimb == null || hitLimb.IsSevered ||
            attacker == null ||
            !(NTConfig.Get("NT_Calculations", true)))
        {
            return;
        }

        // Pull the Evil Falldamage abusing creatures from config.
        var CreatureCategory = NTConfig.Get("NT_creatureNoFallDamage", Enumerable.Empty<string>());

        // If one of these critters caused the attack, counteract the additional damage.
        foreach (string Species in CreatureCategory)
        {
            if (attacker.SpeciesName == Species)
            {
                HF.AddAffliction(__instance, "stopcreatureabuse", 2f);
                break;
            }
        }
    }

    public static void Override_ApplyDamage(
        CharacterHealth __instance,
        Limb hitLimb,
        AttackResult attackResult,
        bool allowStacking = true,
        bool recalculateVitality = true)
    {
        // Confirm the attack data is valid.
        if (__instance == null || __instance.Character == null || __instance.Character.IsDead || !(__instance.Character.IsHuman) ||
            attackResult.Afflictions == null || !(attackResult.Afflictions.Any()) ||
            hitLimb == null || hitLimb.IsSevered ||
            !NTConfig.Get("NT_Calculations", true))
        {
            return;
        }

        // Check for Luabotomy.
        if (!HF.HasAffliction(__instance.Character, "luabotomy"))
        {
            HF.SetAffliction(__instance.Character, "luabotomy", 1f);
        }

        List<Affliction> Afflictions = attackResult.Afflictions;

        // NT Compatibility Modifying OnDamaged Hooks
        foreach (var hook in OnDamaged.ModifyingOnDamagedHooks)
        {
            Afflictions = hook(__instance, Afflictions, hitLimb);
        }

        // Run the method corresponding to the identifier (if it exists)
        foreach (Affliction affliction in Afflictions)
        {
            string Identifier = affliction.Prefab.Identifier.Value;

            if (OnDamaged.OnDamagedMethods.TryGetValue(Identifier, out var method))
            {
                float Resistance = HF.GetResistance(__instance.Character, Identifier, hitLimb.type);
                float Strength = ((float)HF.NormalizeDouble((double)affliction.Strength * (1f - Resistance)));
                method(__instance.Character, Strength, hitLimb.type);
            }
        }

        // NT Compatibility OnDamaged Hooks
        foreach (var hook in OnDamaged.OnDamagedHooks)
        {
            hook(__instance, attackResult, hitLimb);
        }
    }

    public static void InitializeOnDamagedMethods()
    {
        OnDamagedMethods["gunshotwound"] = GunshotWound;
        OnDamagedMethods["explosiondamage"] = ExplosionDamage;
        OnDamagedMethods["bitewounds"] = BiteWounds;
        OnDamagedMethods["lacerations"] = Lacerations;
        OnDamagedMethods["blunttrauma"] = BluntTrauma;
        OnDamagedMethods["internaldamage"] = InternalDamage;
    }

    public static void GunshotWound(Character Character, float Strength, LimbType LimbType)
    {
        // Insert your stuff
    }

    public static void ExplosionDamage(Character Character, float Strength, LimbType LimbType)
    {
        // Insert your stuff
    }

    public static void BiteWounds(Character Character, float Strength, LimbType LimbType)
    {
        // Insert your stuff
    }

    public static void Lacerations(Character Character, float Strength, LimbType LimbType)
    {
        // Insert your stuff
    }

    public static void BluntTrauma(Character Character, float Strength, LimbType LimbType)
    {
        // Insert your stuff
    }

    public static void InternalDamage(Character Character, float Strength, LimbType LimbType)
    {
        // Insert your stuff
    }
}