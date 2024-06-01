using HarmonyLib;
using UnityEngine;
using System.Collections.Generic;

namespace MoonCostMultiplier.Patches;

[HarmonyPatch(typeof(Terminal))]
public class TerminalPatch
{
    static List<int> multipliedMoons = new List<int>();

    [HarmonyPatch("LoadNewNodeIfAffordable")]
    [HarmonyPrefix]
    private static void LoadNewNodeIfAffordablePrefix(ref TerminalNode node)
    {
        if (node.buyRerouteToMoon != -1 && node.buyRerouteToMoon != -2 && !multipliedMoons.Contains(node.buyRerouteToMoon))
        {
            node.itemCost = (int)(node.itemCost * MoonCostMultiplier.MyConfig.Multiplier.Value);
            multipliedMoons.Add(node.buyRerouteToMoon);
        }
    }

    [HarmonyPatch("TextPostProcess")]
    [HarmonyPrefix]
    private static void TextPostProcessPrefix(ref string modifiedDisplayText, TerminalNode node)
    {
        if (node.buyRerouteToMoon == -2)
        {
            modifiedDisplayText = modifiedDisplayText.Replace("[totalCost]", "$" + node.itemCost * MoonCostMultiplier.MyConfig.Multiplier.Value);
        }
    }
}
