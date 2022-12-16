using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Root;
using Kingmaker.UI.UnitSettings;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlashyMartials.Utilities
{
    public static class Helpers
    {

        //stuff stole from Bubbles vv
        public static T[] Arr<T>(params T[] val)
        {
            return val;
        }
        //stuff stole from Bubbles ^^
        public static void RegisterClass(BlueprintCharacterClass ClassToRegister)
        {
            ProgressionRoot progression = ResourcesLibrary.GetRoot().Progression;
            List<BlueprintCharacterClassReference> list = ((IEnumerable<BlueprintCharacterClassReference>)progression.m_CharacterClasses).ToList<BlueprintCharacterClassReference>();
            list.Add(ClassToRegister.ToReference<BlueprintCharacterClassReference>());
            list.Sort((Comparison<BlueprintCharacterClassReference>)((x, y) =>
            {
                BlueprintCharacterClass blueprint1 = ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>(x.guid);
                BlueprintCharacterClass blueprint2 = ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>(y.guid);
                return blueprint1 == null || blueprint2 == null ? 1 : (blueprint1.PrestigeClass == blueprint2.PrestigeClass ? blueprint1.NameSafe().CompareTo(blueprint2.NameSafe()) : (blueprint1.PrestigeClass ? 1 : -1));
            }));
            progression.m_CharacterClasses = list.ToArray();
            if (!ClassToRegister.IsArcaneCaster && !ClassToRegister.IsDivineCaster)
                return;
            BlueprintProgression.ClassWithLevel classWithLevel = Helpers.ClassToClassWithLevel(ClassToRegister);
            BlueprintProgression blueprint = ResourcesLibrary.TryGetBlueprint<BlueprintProgression>("fe9220cdc16e5f444a84d85d5fa8e3d5");
            blueprint.m_Classes = blueprint.m_Classes.AppendToArray<BlueprintProgression.ClassWithLevel>(new BlueprintProgression.ClassWithLevel[1]
            {
                   classWithLevel
            });
        }

        public static BlueprintProgression.ClassWithLevel ClassToClassWithLevel(
      BlueprintCharacterClass orig,
      int addLevel = 0)
        {
            return new BlueprintProgression.ClassWithLevel()
            {
                m_Class = orig.ToReference<BlueprintCharacterClassReference>(),
                AdditionalLevel = addLevel
            };
        }

        public static T[] AppendToArray<T>(this T[] array, IEnumerable<T> values) => AppendToArray(array, values.ToArray());

        public static T GetBlueprint<T>(string id) where T : SimpleBlueprint
        {
            var assetId = new BlueprintGuid(Guid.Parse(id));
            return GetBlueprint<T>(assetId);
        }
        public static T GetBlueprint<T>(BlueprintGuid id) where T : SimpleBlueprint
        {
            SimpleBlueprint asset = ResourcesLibrary.TryGetBlueprint(id);
            T value = asset as T;
            return value;
        }

    }

    internal class AlwaysAddToActionBar : UnitFactComponentDelegate
    {
        public override void OnTurnOn()
        {
            if (!Owner.UISettings.m_Slots.Any(
                  slot =>
                  {
                      if (slot is MechanicActionBarSlotAbility ability)
                          return ability.Ability.Fact == Fact;
                      if (slot is MechanicActionBarSlotActivableAbility activatable)
                          return activatable.ActivatableAbility.SourceFact == Fact;
                      return false;
                  }))
                {
                    Owner.UISettings.RemoveFromAlreadyAutomaticallyAdded((BlueprintAbility)Fact.Blueprint);
                }
                base.OnTurnOn();
        }
    }

}