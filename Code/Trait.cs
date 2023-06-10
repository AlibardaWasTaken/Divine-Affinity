using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using NCMS;
using NCMS.Utils;
using UnityEngine;
using ReflectionUtility;
using HarmonyLib;
using ai;
using System.Reflection;
using Newtonsoft.Json;

namespace DivineAffinity
{
    class Traits
    {
        private static Dictionary<string, float> ChanceDictionary = new Dictionary<string, float>();
        public static void init()
        {
            ChanceDictionary.Add("Zero", 0f);
            ChanceDictionary.Add("Low", 0.05f);
            ChanceDictionary.Add("Medium", 0.15f);
            ChanceDictionary.Add("High", 0.45f);

            loadedCustomTrait();
        }


        public static void loadedCustomTrait()
        {


            ActorTrait Assasin = new ActorTrait();
            Assasin.id = "Lethal Precision";
            Assasin.path_icon = "ui/icons/Assasin";
            Assasin.birth = ChanceDictionary["Low"];
            Assasin.inherit = ChanceDictionary["Zero"];
            Assasin.type = TraitType.Positive;
            Assasin.can_be_given = true;
            Assasin.group_id = "DivineAffinity";
            Assasin.base_stats[S.critical_chance] += 0.25f;
            Assasin.base_stats[S.attack_speed] -= 10f;
            AssetManager.traits.add(Assasin);
            addTraitToLocalizedLibrary(Assasin.id, "Every strike embodying the delicate balance between lethal precision and measured patience");
            PlayerConfig.unlockTrait(Assasin.id);



            ActorTrait Timeforged = new ActorTrait();
            Timeforged.id = "Timeforged Might";
            Timeforged.path_icon = "ui/icons/Timeforged";
            Timeforged.birth = ChanceDictionary["Low"];
            Timeforged.inherit = ChanceDictionary["Zero"];
            Timeforged.can_be_given = true;
            Timeforged.type = TraitType.Positive;
            Timeforged.can_be_removed = true;
            Timeforged.base_stats[S.diplomacy] += 2f;
            Timeforged.base_stats[S.intelligence] += 2f;
            Timeforged.base_stats[S.warfare] += 2f;
            Timeforged.base_stats[S.stewardship] += 2f;
            Timeforged.group_id = "DivineAffinity";
            Timeforged.base_stats[S.max_age] += 10f;
            Timeforged.action_attack_target = (AttackAction)Delegate.Combine(Timeforged.action_attack_target, new AttackAction(AgeAttack));
            AssetManager.traits.add(Timeforged);
            addTraitToLocalizedLibrary(Timeforged.id, "Chance to deal additional damage that grows with each passing year, bearing witness to the wisdom and experience that only age can bestow.");
            PlayerConfig.unlockTrait(Timeforged.id);



            ActorTrait ArmoredVitality = new ActorTrait();
            ArmoredVitality.id = "Armored Vitality";
            ArmoredVitality.path_icon = "ui/icons/ArmorRegen";
            ArmoredVitality.group_id = "DivineAffinity";
            ArmoredVitality.birth = ChanceDictionary["Low"];
            ArmoredVitality.inherit = ChanceDictionary["Zero"];
            ArmoredVitality.can_be_given = true;
            ArmoredVitality.type = TraitType.Positive;
            ArmoredVitality.can_be_removed = true;
            ArmoredVitality.base_stats[S.health] += 5.0f;
            ArmoredVitality.base_stats[S.armor] += 5.0f;
            ArmoredVitality.action_special_effect = (WorldAction)Delegate.Combine(ArmoredVitality.action_special_effect, new WorldAction(ArmorVitalRegen));
            ArmoredVitality.opposite = "Regeneration";
            AssetManager.traits.add(ArmoredVitality);
            addTraitToLocalizedLibrary(ArmoredVitality.id, "Allowing creature to fuel a regenerative force in direct proportion to its Armor stat");
            PlayerConfig.unlockTrait(ArmoredVitality.id);




            ActorTrait Ward = new ActorTrait();
            Ward.id = "Retaliating Ward";
            Ward.path_icon = "ui/icons/spiker";
            Ward.group_id = "DivineAffinity";
            Ward.birth = ChanceDictionary["Medium"];
            Ward.inherit = ChanceDictionary["Zero"];
            Ward.can_be_given = true;
            Ward.type = TraitType.Positive;
            Ward.can_be_removed = true;
            Ward.base_stats[S.health] += 10.0f;
            Ward.base_stats[S.armor] += 10.0f;
            Ward.action_get_hit = (GetHitAction)Delegate.Combine(Ward.action_get_hit, new GetHitAction(SpikeArmor));
            AssetManager.traits.add(Ward);
            addTraitToLocalizedLibrary(Ward.id, "Chance to convert the part of the creature Armor stat into a counterblow damage.");
            PlayerConfig.unlockTrait(Ward.id);



            ActorTrait VitalAwakening = new ActorTrait();
            VitalAwakening.id = "Vital Awakening";
            VitalAwakening.path_icon = "ui/icons/Regen";
            VitalAwakening.group_id = "DivineAffinity";
            VitalAwakening.type = TraitType.Positive;
            VitalAwakening.birth = ChanceDictionary["Low"];
            VitalAwakening.inherit = ChanceDictionary["Zero"];
            VitalAwakening.can_be_given = true;
            VitalAwakening.can_be_removed = true;
            VitalAwakening.opposite = "Regeneration";
            VitalAwakening.action_special_effect = (WorldAction)Delegate.Combine(VitalAwakening.action_special_effect, new WorldAction(VitalRegen));
            AssetManager.traits.add(VitalAwakening);
            addTraitToLocalizedLibrary(VitalAwakening.id, "Enhanced version of Regeneration Trait, that bestowed upon those who possess an exceptional connection to the life force.");
            PlayerConfig.unlockTrait(VitalAwakening.id);

           


            ActorTrait FuryGambit = new ActorTrait();
            FuryGambit.id = "Fury's Gambit";
            FuryGambit.path_icon = "ui/icons/Gambit";
            FuryGambit.group_id = "DivineAffinity";
            FuryGambit.type = TraitType.Other;
            FuryGambit.birth = ChanceDictionary["Low"];
            FuryGambit.inherit = ChanceDictionary["Zero"];
            FuryGambit.can_be_given = true;
            FuryGambit.can_be_removed = true;
            FuryGambit.action_get_hit = (GetHitAction)Delegate.Combine(FuryGambit.action_get_hit, new GetHitAction(DoubleTakeDamage));
            FuryGambit.action_attack_target = (AttackAction)Delegate.Combine(FuryGambit.action_attack_target, new AttackAction(DoubleAttack));
            AssetManager.traits.add(FuryGambit);
            addTraitToLocalizedLibrary(FuryGambit.id,  "This creature deals and takes double amount of damage");
            PlayerConfig.unlockTrait(FuryGambit.id);


            


            ActorTrait Feralstrike = new ActorTrait();
            Feralstrike.id = "Feral Strike";
            Feralstrike.path_icon = "ui/icons/FeralStrike";
            Feralstrike.group_id = "DivineAffinity";
            Feralstrike.birth = ChanceDictionary["Medium"];
            Feralstrike.inherit = ChanceDictionary["Zero"];
            Feralstrike.can_be_given = true;
            Feralstrike.can_be_removed = true;
            Feralstrike.type = TraitType.Positive;
            Feralstrike.base_stats[S.health] += 15.0f;
            Feralstrike.action_attack_target = (AttackAction)Delegate.Combine(Feralstrike.action_attack_target, new AttackAction(FeralStrikeAttack));
            AssetManager.traits.add(Feralstrike);
            addTraitToLocalizedLibrary(Feralstrike.id, "Each injury suffered fueling an rage that amplifying this creature attack damage.");
            PlayerConfig.unlockTrait(Feralstrike.id);


            ActorTrait TheWall = new ActorTrait();
            TheWall.id = "The Wall";
            TheWall.path_icon = "ui/icons/TheWall";
            TheWall.group_id = "DivineAffinity";
            TheWall.birth = ChanceDictionary["High"];
            TheWall.inherit = ChanceDictionary["Low"];
            TheWall.can_be_given = true;
            TheWall.type = TraitType.Positive;
            TheWall.can_be_removed = true;
            TheWall.base_stats[S.health] += 15.0f;
            TheWall.base_stats[S.knockback_reduction] += 1f;
            AssetManager.traits.add(TheWall);
            addTraitToLocalizedLibrary(TheWall.id, "This creature possess an strong spirit that anchors them against crushing blows.");
            PlayerConfig.unlockTrait(TheWall.id);



            ActorTrait Fear = new ActorTrait();
            Fear.id = "Fear of the unknown";
            Fear.path_icon = "ui/icons/Fear";
            Fear.group_id = "DivineAffinity";
            Fear.birth = ChanceDictionary["Medium"];
            Fear.inherit = ChanceDictionary["Low"];
            Fear.can_be_given = true;
            Fear.can_be_removed = true;
            Fear.type = TraitType.Positive;
            Fear.base_stats[S.diplomacy] -= 10.0f;
            Fear.base_stats[S.warfare] += 5.0f;
            Fear.action_attack_target = (AttackAction)Delegate.Combine(Fear.action_attack_target, new AttackAction(AttackOnOtherSpec));
            AssetManager.traits.add(Fear);
            addTraitToLocalizedLibrary(Fear.id, "This creature deals extra damage on hit if the target belongs to a different species.");
            PlayerConfig.unlockTrait(Fear.id);


            ActorTrait DarkHeart = new ActorTrait();
            DarkHeart.id = "Heart of Darkness";
            DarkHeart.path_icon = "ui/icons/DarkHeart";
            DarkHeart.birth = ChanceDictionary["Low"];
            DarkHeart.inherit = ChanceDictionary["Zero"];
            DarkHeart.can_be_given = true;
            DarkHeart.type = TraitType.Positive;
            DarkHeart.group_id = "DivineAffinity";
            DarkHeart.base_stats[S.armor] += 20f;
            DarkHeart.base_stats[S.damage] += 10f;
            DarkHeart.base_stats[S.health] += 100f;
            DarkHeart.base_stats[S.max_age] -= 20f;
            AssetManager.traits.add(DarkHeart);
            addTraitToLocalizedLibrary(DarkHeart.id, "A dark heart pulsating with forbidden power at the cost of a shortened existence.");
            PlayerConfig.unlockTrait(DarkHeart.id);




            ActorTrait Knowledge = new ActorTrait();
            Knowledge.id = "Ethereal Knowledge";
            Knowledge.path_icon = "ui/icons/Knowledge";
            Knowledge.group_id = "DivineAffinity";
            Knowledge.type = TraitType.Positive;
            Knowledge.birth = ChanceDictionary["Low"];
            Knowledge.inherit = ChanceDictionary["Zero"];
            Knowledge.can_be_given = true;
            Knowledge.can_be_removed = true;
            Knowledge.base_stats[S.intelligence] += 5f;
            Knowledge.action_attack_target = (AttackAction)Delegate.Combine(Knowledge.action_attack_target, new AttackAction(IntAttack));
            AssetManager.traits.add(Knowledge);
            addTraitToLocalizedLibrary(Knowledge.id, "Creature has a chance to create explosion of magical energy on hit, Intelligence stat increases the damage of explosion. ");
            PlayerConfig.unlockTrait(Knowledge.id);



            ActorTrait HeartDamage = new ActorTrait();
            HeartDamage.id = "Gift of the Life";
            HeartDamage.path_icon = "ui/icons/Heart";
            HeartDamage.group_id = "DivineAffinity";
            HeartDamage.type = TraitType.Positive;
            HeartDamage.birth = ChanceDictionary["Medium"];
            HeartDamage.inherit = ChanceDictionary["Zero"];
            HeartDamage.can_be_given = true;
            HeartDamage.can_be_removed = true;
            HeartDamage.action_attack_target = (AttackAction)Delegate.Combine(HeartDamage.action_attack_target, new AttackAction(HeathAttack));
            AssetManager.traits.add(HeartDamage);
            addTraitToLocalizedLibrary(HeartDamage.id, "The more remaining health percent a creature has, the more additional damage it deals.");
            PlayerConfig.unlockTrait(HeartDamage.id);



            ActorTrait Nutrition = new ActorTrait();
            Nutrition.id = "Pain Nutrition";
            Nutrition.path_icon = "ui/icons/Meat";
            Nutrition.group_id = "DivineAffinity";
            Nutrition.type = TraitType.Positive;
            Nutrition.birth = ChanceDictionary["Low"];
            Nutrition.inherit = ChanceDictionary["Zero"];
            Nutrition.can_be_given = true;
            Nutrition.can_be_removed = true;
            Nutrition.action_get_hit = (GetHitAction)Delegate.Combine(Nutrition.action_get_hit, new GetHitAction(NutritionRestore));
            AssetManager.traits.add(Nutrition);
            addTraitToLocalizedLibrary(Nutrition.id, "This creature feeds on pain, restores some hunger when it gets hit");
            PlayerConfig.unlockTrait(Nutrition.id);





        }

        private static bool NutritionRestore(BaseSimObject pSelf, BaseSimObject pAttackedBy, WorldTile pTile)
        {
            if (pAttackedBy != null)
            {
                int hungVal = pSelf.a.data.hunger + 5;
                hungVal = Mathf.Clamp(hungVal, 1, 100);
                pSelf.a.data.hunger = hungVal;
            }
            return true;
        }


        private static bool IntAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Toolbox.randomChance(0.2f))
            {
                Actor a = Reflection.GetField(pTarget.GetType(), pTarget, "a") as Actor;
                Actor s = Reflection.GetField(pSelf.GetType(), pTarget, "a") as Actor;
       
                a.getHit(1.5f * s.stats[S.intelligence], true, AttackType.Weapon, pSelf, false, false);
                EffectsLibrary.spawnExplosionWave(pTile.posV3, 0.05f, 6f);
                return true;
            }
            return false;
        }


        public static bool AgeAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (pTarget != null)
            {
                if (Toolbox.randomChance(0.4f))
                {
                    Actor a = Reflection.GetField(pTarget.GetType(), pTarget, "a") as Actor;
                    Actor s = Reflection.GetField(pSelf.GetType(), pTarget, "a") as Actor;
                    float damagecaped = Mathf.Clamp(s.getAge() / 2, 1, 400);
                    a.getHit(damagecaped, true, AttackType.Weapon, pSelf, false, false);
                    return true;
                }

            }
            return false;
        }

        public static bool DoubleAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (pTarget != null)
            {

                Actor a = Reflection.GetField(pTarget.GetType(), pTarget, "a") as Actor;
                Actor s = Reflection.GetField(pSelf.GetType(), pTarget, "a") as Actor;
                a.getHit(s.stats[S.damage], true, AttackType.Weapon, pSelf, false, false);

                return true;


            }
            return false;
        }



        public static bool AttackOnOtherSpec(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (pTarget != null && pTarget.a.race != pSelf.a.race)
            {

                Actor a = Reflection.GetField(pTarget.GetType(), pTarget, "a") as Actor;
                Actor s = Reflection.GetField(pSelf.GetType(), pTarget, "a") as Actor;
                a.getHit(s.stats[S.damage] * 0.5f, true, AttackType.Weapon, pSelf, false, false);
                return true;


            }
            return false;
        }



        public static bool FeralStrikeAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (pTarget != null)
            {


                float RestHp = (pSelf.a.getMaxHealth() - pSelf.a.data.health);
                float LostHpPercent = (RestHp / pSelf.a.getMaxHealth()) * 100f;
                
                pTarget.a.getHit(LostHpPercent, true, AttackType.Weapon, pSelf, false, false);
                return true;


            }
            return false;
        }




        public static bool HeathAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (pTarget != null)
            {

                float HpCalc = pSelf.a.data.health;
                float damageBasedOnHp = ((HpCalc / pSelf.a.getMaxHealth()) * 100f) / 2.5f;

                pTarget.a.getHit(damageBasedOnHp, true, AttackType.Weapon, pSelf, false, false);
                Debug.Log(damageBasedOnHp);
                return true;


            }
            return false;
        }





        public static bool DoubleTakeDamage(BaseSimObject pSelf, BaseSimObject pAttackedBy, WorldTile pTile)
        {
            if (pAttackedBy != null)
            {
             BaseSimObject enemy = pAttackedBy.a.attackedBy;
             Actor Self = Reflection.GetField(pSelf.GetType(), pSelf, "a") as Actor;
             Self.getHit(enemy.a.stats[S.damage], true, AttackType.Weapon, null, true, false);
    
            }
            return true;
        }




        public static bool SpikeArmor(BaseSimObject pSelf, BaseSimObject pAttackedBy, WorldTile pTile)
        {
            if (pAttackedBy != null)
            {
                
               if (Toolbox.randomChance(0.5f))
               {
                  
                  Actor Self = Reflection.GetField(pSelf.GetType(), pSelf, "a") as Actor;

                    pAttackedBy.a.getHit(Self.a.stats[S.armor] / 2f, true, AttackType.Weapon, null, true, false);
               }


                
            }
            return true;
        }


        public static bool ArmorVitalRegen(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.hasTrait("infected"))
            {
                return true;
            }
            bool flag = true;
            if (pTarget.a.asset.needFood)
            {
                flag = (pTarget.a.data.hunger > 0);
            }
            if (pTarget.a.data.health != pTarget.getMaxHealth() && flag && Toolbox.randomChance(0.4f))
            {

                pTarget.a.restoreHealth((int)Math.Round(pTarget.a.stats[S.armor] / 5f));
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }
            return true;
        }





        public static bool VitalRegen(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.hasTrait("infected"))
            {
                return true;
            }
            bool flag = true;
            if (pTarget.a.asset.needFood)
            {
                flag = (pTarget.a.data.hunger > 0);
            }
            if (pTarget.a.data.health != pTarget.getMaxHealth() && flag && Toolbox.randomChance(0.4f))
            {
                pTarget.a.restoreHealth(5);
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }
            return true;
        }





        public static void addTraitToLocalizedLibrary(string id, string description)
        {
            string language = Reflection.GetField(LocalizedTextManager.instance.GetType(), LocalizedTextManager.instance, "language") as string;
            Dictionary<string, string> localizedText = Reflection.GetField(LocalizedTextManager.instance.GetType(), LocalizedTextManager.instance, "localizedText") as Dictionary<string, string>;
            localizedText.Add("trait_" + id, id);
            localizedText.Add("trait_" + id + "_info", description);
        }


    }
}
