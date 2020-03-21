﻿//-----------------------------------------------------------------------------
// <copyright file="IncreaseStatRule.cs" company="WheelMUD Development Team">
//   Copyright (c) WheelMUD Development Team. See LICENSE.txt. This file is
//   subject to the Microsoft Permissive License. All other rights reserved.
// </copyright>
// <summary>
//   Rule to increase the value of a stat.
// </summary>
//-----------------------------------------------------------------------------

namespace WarriorRogueMage.Rules
{
    using System;
    using WheelMUD.Core;
    using WheelMUD.Interfaces;
    using WheelMUD.Rules;

    /// <summary>Rule to increase the value of a stat.</summary>
    public class IncreaseStatRule : GameRule
    {
        private static Thing parentThing;
        private static GameStat privateStat;
        private static int value;

        /// <summary>Gets the kind of the rule.</summary>
        public override string RuleKind
        {
            get { return "IncreaseStatRule"; }
        }

        /// <summary>Executes the rule on the specified player <see cref="Thing"/>.</summary>
        /// <param name="playerThing">The player thing.</param>
        /// <param name="statName">Name of the attribute.</param>
        /// <param name="valueToAdd">The value to add.</param>
        public override void Execute(IThing playerThing, string statName, int valueToAdd)
        {
            value = valueToAdd;
            parentThing = (Thing)playerThing;
            FindStat(statName);
            IncreaseStatValue();
        }

        private static void FindStat(string statNameToFind)
        {
            // @@@ THE CONVERSIONS HERE AND IN ATTRIBUTE RULES ARE SO BROKEN
            //   NEED TO AVOID PERMANENTLY CHANGING THE BASE STATS - SO DANGEROUS
            //   NEED TO HAVE A WAY TO ATTACH EFFECTS TO THE STATS, IN A WAY THAT ONLY OPTIONALLY ALLOWS DUPLICATES OF NAME (like stacking combat effects)
            //   Then it won't be as risky to ensure we have the relevant effect of a unique name applied, like 
            var stat = parentThing.FindGameStat(statNameToFind);

            privateStat = (GameStat)Convert.ChangeType(parentThing.FindGameStat(stat.Name), typeof(GameStat));
        }

        private static void IncreaseStatValue()
        {
            var stat = (WRMStat)Convert.ChangeType(privateStat, typeof(WRMStat));

            stat.Increase(value, parentThing);
        }
    }
}
