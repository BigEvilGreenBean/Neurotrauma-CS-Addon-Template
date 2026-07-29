
namespace MyAddon
{

    public static class AddonAfflictions
    {
        public static void DefineAllAfflictions()
        {
            AddonAfflictionsToAdd AffsToAdd = new();
        }
    }

    public class  AddonAfflictionsToAdd : AfflictionsPackage
    {

        // Human Updates update functions have 
        // Param 1: NTHuman (The character we updating) [C]
        // Param 2: String (The affliction Identifier) [I]
        // Param 3: LimbType (The limb the aff is on) [L]
        // Param 4: AfflictionData (the affliction data of the aff) [AffData]

        Dictionary<string, NTNonLimbAffliction> AfflictionsToAdd =
                                new Dictionary<string, NTNonLimbAffliction>();
        Dictionary<string, NTLimbAffliction> LimbAfflictionsToAdd =
                                new Dictionary<string, NTLimbAffliction>();
        Dictionary<string, NTBloodAffliction> BloodAfflictionsToAdd =
                                new Dictionary<string, NTBloodAffliction>();
        Dictionary<string, NTSymptom> SymptomsToAdd =
                                new Dictionary<string, NTSymptom>();
        Dictionary<string, NTLimbSymptom> LimbSymptomsToAdd =
                                new Dictionary<string, NTLimbSymptom>();

        public AddonAfflictionsToAdd() // Initalize the afflictions.
        {
            AddAfflictions();
            AddLimbAfflictions();
            AddBloodAfflictions();
            AddSymptoms();
            AddLimbSymptoms();
        }

        private void AddAfflictions()
        {
            // Oxygen Low
            // Not constant; gets applied by other sources
            // Type: Non-Limb Specific, Vanilla Override
            // Caused By: Lack of Oxygen, Respiratory Arrest
            // Effects: Hypoxemia
            AfflictionsToAdd["example_aff"] = new("example_aff", 0, 200, 0, AfflictionPriority.HIGH);
            AfflictionsToAdd["example_aff"].UpdateAction =
                (HumanUpdate.NTHuman C, string ID, LimbType Limb, HumanUpdate.NTHumanNonLimbAffData AffData) =>
                {
                };

            foreach (KeyValuePair<string, NTNonLimbAffliction> Pair in AfflictionsToAdd)
            {
                NTAfflictions.RegisterAffliction(Pair.Key, Pair.Value);
            }
        }

        private void AddLimbAfflictions()
        {
            // Sutured Incision
            // Not constant; gets applied by other sources.
            // Type: Limb Specific, Surgical
            // Caused By: Stitching a Surgical Incision.
            // Effects: None.
            LimbAfflictionsToAdd["example_limb_aff"] = new("example_limb_aff", 0, 100, 0, AfflictionPriority.MEDIUM);
            LimbAfflictionsToAdd["example_limb_aff"].UpdateAction =
               (HumanUpdate.NTHuman C, string ID, LimbType Limb, HumanUpdate.NTHumanLimbAffData AffData) =>
               {
                   // Passive Decrease
                   // Originally had a maxstrength of 100, and reduced by 1 per second in XML.
                   // Adjusted, that became 4 per 4 seconds.
                   AffData.Strength[Limb] -= 4;
               };

            foreach (KeyValuePair<string, NTLimbAffliction> Pair in LimbAfflictionsToAdd)
            {
                NTAfflictions.RegisterAffliction(Pair.Key, Pair.Value);
            }
        }

        private void AddBloodAfflictions()
        {
            // Blood afflictions are literally the same to write as NonLimbAfflictions, they're just here for organization purposes.

            // Blood Pressure
            // Constant; too complicated otherwise.
            // Type: Vital Mechanic
            // Handles the entire blood pressure system and application of effects.
            BloodAfflictionsToAdd["example_blood_aff"] = new("example_blood_aff", 0, 200, 100, AfflictionPriority.HIGH);
            BloodAfflictionsToAdd["example_blood_aff"].UpdateAction =
                (HumanUpdate.NTHuman C, string ID, LimbType Limb, HumanUpdate.NTHumanBloodAffData AffData) =>
                {
                };

            foreach (KeyValuePair<string, NTBloodAffliction> Pair in BloodAfflictionsToAdd)
            {
                NTAfflictions.RegisterAffliction(Pair.Key, Pair.Value);
            }
        }

        private void AddSymptoms()
        {
            // Cough
            // Type: Symptom, Mental
            // Removes itself when conditions are NOT met. Applied by other afflictions. Removed when Unconscious.
            SymptomsToAdd["example_sym_aff"] = new("example_sym_aff", 0, 100, 0, AfflictionPriority.HIGH);
            SymptomsToAdd["example_sym_aff"].UpdateAction =
                (HumanUpdate.NTHuman C, string ID, LimbType Limb, HumanUpdate.NTHumanSymptomData AffData) =>
                {
                }; 

            foreach (KeyValuePair<string, NTSymptom> Pair in SymptomsToAdd)
            {
                NTAfflictions.RegisterAffliction(Pair.Key, Pair.Value);
            }
        }

        private void AddLimbSymptoms()
        {

            // Spasms
            // Type: Symptom
            // Caused By: Seizure
            // Effects: Makes character twitch on the ground via XML.
            LimbSymptomsToAdd["example_limbsym_aff"] = new("example_limbsym_aff", 0, 100, 0, AfflictionPriority.HIGH);
            LimbSymptomsToAdd["example_limbsym_aff"].UpdateAction =
                (HumanUpdate.NTHuman C, string ID, LimbType Limb, HumanUpdate.NTHumanLimbSymptomData AffData) =>
                {
                };

            foreach (KeyValuePair<string, NTLimbSymptom> Pair in LimbSymptomsToAdd)
            {
                NTAfflictions.RegisterAffliction(Pair.Key, Pair.Value);
            }
        }
    }
}