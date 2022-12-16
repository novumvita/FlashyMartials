using BlueprintCore.Blueprints.Configurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using System;
using System.Collections.Generic;
using BlueprintCore.Utils.Types;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Actions.Builder;
using UnityEngine;
using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Actions.Builder.ContextEx;
using Kingmaker.RuleSystem;
using Kingmaker.Blueprints;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using static Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell;
using static Kingmaker.UnitLogic.Commands.Base.UnitCommand;
using Kingmaker.Designers.Mechanics.Facts;
using FlashyMartials.Utilities;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.Utility;
using Kingmaker.UnitLogic;
using Kingmaker.RuleSystem.Rules;
using static Kingmaker.UnitLogic.Abilities.Components.AbilityCustomMeleeAttack;
using Kingmaker.PubSubSystem;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.EntitySystem;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.Enums;
using Kingmaker.Blueprints.Facts;
using Kingmaker.UnitLogic.Parts;

namespace FlashyMartials.Classes
{
    internal class SheathedBladeClass
    {
        private const string ClassName = "SheathedBladeClass";
        private const string ClassDisplayName = "SheathedBladeClass.Name";
        private const string ClassGuid = "0C91E832-04B6-447C-A4B6-197E4A321576";
        private const string ClassDescription = "SheathedBladeClass.Description";
        private const string ClassProgression = "SheathedBladeClassProgression";
        private const string ClassProgressionGuid = "DB8B9584-5485-42B7-903D-1EE4AB1E0E39";

        private const string LethalFocusName = "LethalFocus";
        private const string LethalFocusDisplayName = "LethalFocus.Name";
        private const string LethalFocusGuid = "17C0776B-EFE7-47A2-B4AC-A22B96B96D18";
        private const string LethalFocusDescription = "LethalFocus.Description";

        private const string InsightName = "Insight";
        private const string InsightDisplayName = "Insight.Name";
        private const string InsightGuid = "05710AF1-059F-4E88-865D-71F965483DF4";
        private const string InsightDescription = "Insight.Description";

        private const string PName = "PerfectedStrike";
        private const string PDisplayName = "PerfectedStrike.Name";
        private const string PGuid = "FA44D3FA-5779-483F-9E6B-7557986807BF";
        private const string PDescription = "PerfectedStrike.Description";

        private const string QSName = "QuickStrike";
        private const string QSDescription = "QuickStrike.Description";
        private const string QSGuid = "47E81E4D-DDDF-4C4B-A339-65657D74D5D8";

        private const string QSDisplayName = "QuickStrike.Name";

        private const string QSAbilityName = "QuickStrikeAbility";
        private const string QSAbilityGuid = "DCC755A3-B8AF-4242-97A2-F79FBF0C4845";

        private const string QSQDisplayName = "QuickStrikeQuick.Name";

        private const string QSQAbilityName = "QuickStrikeQuick";
        private const string QSQAbilityGuid = "19147A50-48EC-407B-AF6C-1BADC749C0A0";

        private const string LSName = "LongStrike";
        private const string LSDescription = "LongStrike.Description";
        private const string LSGuid = "CB9AA586-506C-4E33-A01B-DA921064B805";

        private const string LSDisplayName = "LongStrike.Name";

        private const string LSAbilityName = "LongStrikeAbility";
        private const string LSAbilityGuid = "8777B714-44FA-4D8D-93B7-9695ED17AF38";

        private const string LSQDisplayName = "LongStrikeQuick.Name";

        private const string LSQAbilityName = "LongStrikeQuick";
        private const string LSQAbilityGuid = "06F56B0F-C374-47A7-8939-77534D2D3E35";

        private const string SBName = "SheathedBlade";
        private const string SBDisplayName = "SheathedBlade.Name";
        private const string SBDescription = "SheathedBlade.Description";
        private const string SBGuid = "7EE9F2BD-AFB7-4EC5-96C9-75143AEA901D";
        private const string SBuffName = "SheathedBladeBuff";
        private const string SBuffGuid = "E09A550B-042C-4875-9005-197119821857";

        private const string SAbilityName = "SheatheAbility";
        private const string SAbilityGuid = "2BEF32DC-7BC8-4301-AB2E-9C36E0AD8508";
        private const string SAbilityDisplayName = "SheatheAbility.Name";
        private const string SAbilityDescription = "SheatheAbility.Description";

        private const string SAQAbilityDisplayName = "SheatheAbilityQuick.Name";

        private const string SASbilityName = "SheatheAbilitySwift";
        private const string SASbilityGuid = "A337F678-063B-4EE4-9768-7A7B406C7629";

        private const string SAFbilityName = "SheatheAbilityFree";
        private const string SAFbilityGuid = "495A26C2-4B7C-4F73-8EDE-DF2192E72181";

        private const string SAQbilityName = "SheatheAbilityQuick";
        private const string SAQbilityGuid = "E278BA0F-49E5-496B-91A2-DA93FDB55C79";

        private const string ResourceName = "Focus";
        private const string ResourceGuid = "1BFCD2BA-863E-4B2F-8D1F-294F874E2AFA";

        private const string strike1Icon = "assets/icons/strike1.png";
        private const string strike2Icon = "assets/icons/strike2.png";
        private const string focusIcon = "assets/icons/focus.png";

        internal static void Configure()
        {
            var SheathedBladeClass =
                CharacterClassConfigurator.New(ClassName, ClassGuid)
                .SetLocalizedName(ClassDisplayName)
                .SetLocalizedDescription(ClassDescription)
                .SetHitDie(DiceType.D10)
                .SetBaseAttackBonus(StatProgressionRefs.BABFull.Cast<BlueprintStatProgressionReference>())
                .SetFortitudeSave(StatProgressionRefs.SavesPrestigeLow.Cast<BlueprintStatProgressionReference>())
                .SetReflexSave(StatProgressionRefs.SavesPrestigeHigh.Cast<BlueprintStatProgressionReference>())
                .SetWillSave(StatProgressionRefs.SavesPrestigeHigh.Cast<BlueprintStatProgressionReference>())
                .SetPrestigeClass(true)
                .SetDifficulty(3)
                .SetRecommendedAttributes(StatType.Dexterity, StatType.Wisdom)
                .SetSpellbook(null)
                .SetDefaultBuild(null)
                .SetSkillPoints(3)
                .SetClassSkills(StatType.SkillMobility, StatType.SkillKnowledgeWorld, StatType.SkillPerception)
                .SetIsArcaneCaster(false)
                .SetIsDivineCaster(false)
                .AddPrerequisiteStatValue(StatType.Dexterity, 17)
                .AddPrerequisiteStatValue(StatType.BaseAttackBonus, 6)
                .AddPrerequisiteStatValue(StatType.SkillPerception, 4)
                .AddPrerequisiteFeature(FeatureRefs.LightningReflexes.Cast<BlueprintFeatureReference>())
                .Configure();

            var focusResource = AbilityResourceConfigurator.New(ResourceName, ResourceGuid)
                .SetMaxAmount(ResourceAmountBuilder.New(1)
                    .IncreaseByStat(StatType.Wisdom))
                .SetUseMax()
                .SetMax(20)
                .Configure();

            var lethalFocus = FeatureConfigurator.New(LethalFocusName, LethalFocusGuid)
                .SetDisplayName(LethalFocusDisplayName)
                .SetDescription(LethalFocusDescription)
                .AddComponent<LethalFocus>()
                .Configure();

            var sheathedBuff = BuffConfigurator.New(SBuffName, SBuffGuid)
                .SetDisplayName(SBDisplayName)
                .SetDescription(SBDescription)
                .AddRemoveBuffOnAttack()
                .AddComponent<SheathedBladeBuff>()
                .SetIcon(focusIcon)
                .Configure();

            var quickStrike = AbilityConfigurator.New(QSAbilityName, QSAbilityGuid)
              .SetDisplayName(QSDisplayName)
              .SetDescription(QSDescription)
              .SetRange(AbilityRange.Weapon)
              .AllowTargeting(enemies: true)
              .SetAnimation(CastAnimationStyle.Special)
              .SetIsFullRoundAction(true)
              .SetNeedEquipWeapons(true)
              .AddComponent<AbilityQuickStrike>()
              .AddAbilityCasterHasFacts(new() { sheathedBuff })
              .AddAbilityCasterMainWeaponIsMelee()
              .SetIcon(strike2Icon)
              .Configure();

            var quickstrikeQuick = AbilityConfigurator.New(QSQAbilityName, QSQAbilityGuid)
              .SetDisplayName(QSQDisplayName)
              .SetDescription(QSDescription)
              .SetRange(AbilityRange.Weapon)
              .AllowTargeting(enemies: true)
              .SetAnimation(CastAnimationStyle.Special)
              .SetActionType(CommandType.Standard)
              .SetNeedEquipWeapons(true)
              .AddComponent<AbilityQuickStrike>()
              .AddAbilityCasterHasFacts(new() { sheathedBuff })
              .AddAbilityCasterMainWeaponIsMelee()
              .AddAbilityResourceLogic(requiredResource: focusResource, amount: 2, isSpendResource: true)
              .SetIcon(strike2Icon)
              .Configure();


            var quickStrikeFeature = FeatureConfigurator.New(QSName, QSGuid)
                .SetDescription(QSDescription)
                .SetDisplayName(QSDisplayName)
                .AddFacts(new() { quickStrike, quickstrikeQuick })
                .SetIcon(strike2Icon)
                .Configure();

            var longStrike = AbilityConfigurator.New(LSAbilityName, LSAbilityGuid)
                .SetDisplayName(LSDisplayName)
                .SetDescription(LSDescription)
                .SetRange(AbilityRange.Close)
                .AllowTargeting(enemies: true)
                .SetAnimation(CastAnimationStyle.Special)
                .SetActionType(CommandType.Standard)
                .SetNeedEquipWeapons(true)
                .AddComponent<AbilityLongStrike>()
                .AddAbilityCasterMainWeaponIsMelee()
                .AddAbilityCasterHasFacts(new() { sheathedBuff })
                .SetIcon(strike1Icon)
                .Configure();

            var longStrikeQuick = AbilityConfigurator.New(LSQAbilityName, LSQAbilityGuid)
                .SetDisplayName(LSQDisplayName)
                .SetDescription(LSDescription)
                .SetRange(AbilityRange.Close)
                .AllowTargeting(enemies: true)
                .SetAnimation(CastAnimationStyle.Special)
                .SetActionType(CommandType.Swift)
                .SetNeedEquipWeapons(true)
                .AddComponent<AbilityLongStrike>()
                .AddAbilityCasterHasFacts(new() { sheathedBuff })
                .AddAbilityCasterMainWeaponIsMelee()
                .AddAbilityResourceLogic(requiredResource: focusResource, amount: 1, isSpendResource: true)
                .SetIcon(strike1Icon)
                .Configure();

            var longStrikeFeature = FeatureConfigurator.New(LSName, LSGuid)
                .SetDescription(LSDescription)
                .SetDisplayName(LSDisplayName)
                .AddFacts(new() { longStrike, longStrikeQuick })
                .SetIcon(strike1Icon)
                .Configure();

            var sheathedAbility = AbilityConfigurator.New(SAbilityName, SAbilityGuid)
                .SetDisplayName(SAbilityDisplayName)
                .SetDescription(SAbilityDescription)
                .SetActionType(CommandType.Move)
                .SetAnimation(CastAnimationStyle.Self)
                .SetNeedEquipWeapons(true)
                .AddAbilityEffectRunAction(ActionsBuilder.New()
                    .ApplyBuffPermanent(sheathedBuff))
                .AddAbilityCasterMainWeaponIsMelee()
                .SetRange(AbilityRange.Personal)
                .SetIcon(focusIcon)
                .Configure();

            var sheathedAbilitySwift = AbilityConfigurator.New(SASbilityName, SASbilityGuid)
                .SetDisplayName(SAQAbilityDisplayName)
                .SetDescription(SAbilityDescription)
                .SetActionType(CommandType.Swift)
                .SetAnimation(CastAnimationStyle.Self)
                .SetNeedEquipWeapons(true)
                .AddAbilityEffectRunAction(ActionsBuilder.New()
                    .ApplyBuffPermanent(sheathedBuff))
                .AddAbilityResourceLogic(requiredResource: focusResource, amount: 1, isSpendResource: true)
                .AddAbilityCasterMainWeaponIsMelee()
                .SetRange(AbilityRange.Personal)
                .SetIcon(focusIcon)
                .Configure();

            var sheathedAbilityFree = AbilityConfigurator.New(SAFbilityName, SAFbilityGuid)
                .SetDisplayName(SAQAbilityDisplayName)
                .SetDescription(SAbilityDescription)
                .SetActionType(CommandType.Free)
                .SetAnimation(CastAnimationStyle.Self)
                .SetNeedEquipWeapons(true)
                .AddAbilityEffectRunAction(ActionsBuilder.New()
                    .ApplyBuffPermanent(sheathedBuff))
                .AddAbilityResourceLogic(requiredResource: focusResource, amount: 2, isSpendResource: true)
                .AddAbilityCasterMainWeaponIsMelee()
                .SetRange(AbilityRange.Personal)
                .SetIcon(focusIcon)
                .Configure();

            var sheathedAbilityQuick = AbilityConfigurator.New(SAQbilityName, SAQbilityGuid)
                .SetDisplayName(SAQAbilityDisplayName)
                .SetDescription(SAbilityDescription)
                .AddAbilityVariants(new() { sheathedAbilitySwift, sheathedAbilityFree })
                .SetRange(AbilityRange.Personal)
                .AddAbilityCasterMainWeaponIsMelee()
                .SetIcon(focusIcon)
                .Configure();

            var sheathedFeature = FeatureConfigurator.New(SBName, SBGuid)
               .SetDisplayName(SBDisplayName)
               .SetDescription(SBDescription)
               .AddFacts(new() { sheathedAbility, sheathedAbilityQuick })
               .AddComponent<CombatStateTrigger>(c =>
               {
                   c.CombatStartActions = ActionsBuilder.New()
                       .ApplyBuffPermanent(sheathedBuff)
                       .Build();
               })
               .SetIcon(focusIcon)
               .Configure();

            var perfectedFeature = FeatureConfigurator.New(PName, PGuid)
               .SetDisplayName(PDisplayName)
               .SetDescription(PDescription)
               .SetIcon(strike2Icon)
               .Configure();

            var insightFeature = FeatureConfigurator.New(InsightName, InsightGuid)
               .SetDisplayName(InsightDisplayName)
               .SetDescription(InsightDescription)
               .AddAbilityResources(resource: focusResource, restoreAmount: true)
               .Configure();

            var lb = new LevelEntryBuilder();
            lb.AddEntry(1, FeatureRefs.ImprovedUnarmedStrike.Cast<BlueprintFeatureBaseReference>(), sheathedFeature, insightFeature);
            lb.AddEntry(4, longStrikeFeature);
            lb.AddEntry(6, FeatureRefs.PenetratingStrike.Cast<BlueprintFeatureBaseReference>());
            lb.AddEntry(7, quickStrikeFeature, lethalFocus);
            lb.AddEntry(9, FeatureRefs.GreaterPenetratingStrike.Cast<BlueprintFeatureBaseReference>());
            lb.AddEntry(10, perfectedFeature);

            var ui = new UIGroupBuilder();
            ui.AddGroup(new Blueprint<BlueprintFeatureBaseReference>[] { FeatureRefs.PenetratingStrike.Cast<BlueprintFeatureBaseReference>(), FeatureRefs.GreaterPenetratingStrike.Cast<BlueprintFeatureBaseReference>() });
            ui.AddGroup(new Blueprint<BlueprintFeatureBaseReference>[] { sheathedFeature, quickStrikeFeature, longStrikeFeature, perfectedFeature });

            var progression =
                ProgressionConfigurator.New(ClassProgression, ClassProgressionGuid)
                .AddToLevelEntries(lb.GetEntries())
                .SetUIGroups(ui.GetGroups())
                .AddToClasses(SheathedBladeClass.ToReference<BlueprintCharacterClassReference>())
                .Configure();

            SheathedBladeClass = CharacterClassConfigurator.For(SheathedBladeClass)
                .SetProgression(progression)
                .Configure();

            Helpers.RegisterClass(SheathedBladeClass);
        }


        public class AbilityQuickStrike : AbilityCustomLogic
        {
            public override IEnumerator<AbilityDeliveryTarget> Deliver(AbilityExecutionContext context, TargetWrapper targetUnit)
            {
                var caster = context.Caster;
                var target = targetUnit.Unit;
                if (target == null)
                {
                    yield break;
                }
                var threatHandMelee = caster.GetThreatHandMelee();
                if (threatHandMelee == null)
                {
                    yield break;
                }

                var rule = new RuleAttackWithWeapon(caster, target, caster.GetFirstWeapon(), 0);

                if (caster.HasFact(Helpers.GetBlueprint<BlueprintFact>(PGuid)))
                {
                    rule.ForceFlatFooted = true;
                }

                EventHandlers eventHandlers = new EventHandlers();
                eventHandlers.Add(new QuickStrikeEventHandler(caster, caster.Stats.BaseAttackBonus, context.Ability.Fact));

                if (caster.Stats.BaseAttackBonus > 5)
                {
                    using (eventHandlers.Activate())
                    {
                        context.TriggerRule(rule);
                    }
                }
                else
                {
                    context.TriggerRule(rule);
                }

                yield return new AbilityDeliveryTarget(targetUnit);
                yield break;
            }
            public override void Cleanup(AbilityExecutionContext context)
            {
            }
        }

        public class QuickStrikeEventHandler : IInitiatorRulebookHandler<RuleCalculateWeaponStats>, IRulebookHandler<RuleCalculateWeaponStats>
        {
            private readonly UnitEntityData m_Unit;
            private readonly EntityFact m_Fact;
            private readonly int m_AttackBonus;

            public QuickStrikeEventHandler(UnitEntityData unit, int attackBonus, EntityFact fact)
            {
                this.m_Unit = unit;
                this.m_AttackBonus = attackBonus;
                this.m_Fact = fact;
            }

            public UnitEntityData GetSubscribingUnit()
            {
                return this.m_Unit;
            }

            public void OnEventAboutToTrigger(RuleCalculateWeaponStats evt)
            {
            }

            public void OnEventDidTrigger(RuleCalculateWeaponStats evt)
            {
                var caster = this.m_Unit;
                var weapon = caster.GetFirstWeapon();
                var dice = new ModifiableDiceFormula(weapon.DamageDice);
                dice.Modify(new DiceFormula(dice.ModifiedValue.Rolls * ((this.m_AttackBonus - 1) / 5), dice.ModifiedValue.Dice), this.m_Fact);
                var dam = evt.DamageDescription.FirstItem();
                var dam2 = new DamageDescription();
                dam2.SetDice(dice);
                dam2.AddModifier(new Modifier(dam.Bonus * ((this.m_AttackBonus - 1) / 5), this.m_Fact, ModifierDescriptor.UntypedStackable));
                evt.DamageDescription.Insert(1, dam2);
            }

        }

        public class AbilityLongStrike : AbilityCustomLogic
        {
            public override IEnumerator<AbilityDeliveryTarget> Deliver(AbilityExecutionContext context, TargetWrapper targetUnit)
            {
                var caster = context.Caster;
                var target = targetUnit.Unit;
                if (target == null)
                {
                    yield break;
                }
                var threatHandMelee = caster.GetThreatHandMelee();
                if (threatHandMelee == null)
                {
                    yield break;
                }

                var rule = new RuleAttackWithWeapon(caster, target, caster.GetFirstWeapon(), 0);

                if (caster.HasFact(Helpers.GetBlueprint<BlueprintFact>(PGuid)))
                {
                     rule.ForceFlatFooted = true;
                }

                context.TriggerRule(rule);

                yield return new AbilityDeliveryTarget(targetUnit);
                yield break;
            }
            public override void Cleanup(AbilityExecutionContext context)
            {
            }
        }

        public class SheathedBladeBuff :
      UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleCalculateAttackBonusWithoutTarget>
        {
            public void OnEventAboutToTrigger(RuleCalculateAttackBonusWithoutTarget evt)
            {
                var wisMod = (Owner.Stats.GetStat(StatType.Wisdom) as ModifiableValueAttributeStat).Bonus;
                evt.AddModifier(wisMod, Fact, ModifierDescriptor.Insight);
            }

            public void OnEventDidTrigger(RuleCalculateAttackBonusWithoutTarget evt)
            {
            }
        }

        public class LethalFocus : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleAttackWithWeapon>, IRulebookHandler<RuleAttackWithWeapon>, ISubscriber, IInitiatorRulebookSubscriber
        {
            public void OnEventAboutToTrigger(RuleAttackWithWeapon evt)
            {
                bool IsFlatFooted = evt.ForceFlatFooted || evt.Target.Descriptor.State.IsHelpless || evt.Target.Descriptor.State.HasCondition(UnitCondition.Stunned) || evt.Target.Descriptor.State.HasCondition(UnitCondition.LoseDexterityToAC) || ((evt.Target.Descriptor.State.HasCondition(UnitCondition.Shaken) || evt.Target.Descriptor.State.HasCondition(UnitCondition.Frightened)) && (bool)evt.Initiator.Descriptor.State.Features.ShatterDefenses);

                if (IsFlatFooted)
                {
                    evt.WeaponStats.AddDamageModifier(Math.Max(0, base.Owner.Stats.Wisdom.Bonus), base.Fact);
                }
            }

            public void OnEventDidTrigger(RuleAttackWithWeapon evt)
            {
            }
        }
    }
}