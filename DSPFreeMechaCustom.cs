using BepInEx;
using BepInEx.Configuration;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Reflection.Emit;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System;
using System.IO;
using BepInEx.Logging;
using HarmonyLib;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using static UnityEngine.GUILayout;
using UnityEngine.Rendering;
using Steamworks;
using rail;
using System.Runtime.Remoting.Contexts;
using TranslationCommon.SimpleJSON;

[module: UnverifiableCode]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]

namespace DSPFreeMechaCustom
{

    [BepInPlugin("Appun.DSP.plugin.FreeMechaCustom", "DSPFreeMechaCustom", "0.0.1")]
    [BepInProcess("DSPGAME.exe")]


    [HarmonyPatch]
    public class DSPFreeMechaCustom : BaseUnityPlugin
    {

        //スタート
        public void Start()
        {
            LogManager.Logger = Logger;
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
        }

        [HarmonyPrefix, HarmonyPatch(typeof(UIMechaMatsGroup), "OnApplyClick")]
        public static bool UIMechaMatsGroup_OnApplyClick_prePatch(UIMechaMatsGroup __instance)
        {
            __instance.mechaEditor.mecha.diyAppearance.CopyTo(__instance.mechaEditor.mecha.appearance);
            __instance.mechaEditor.player.mechaArmorModel.RefreshAllPartObjects();
            __instance.mechaEditor.player.mechaArmorModel.RefreshAllBoneObjects();
            __instance.mechaEditor.mecha.appearance.NotifyAllEvents();
            __instance.mechaEditor.CalcMechaProperty();
            VFAudio.Create("ui-click-1", null, Vector3.zero, true, 0, -1, -1L);
            UIMessageBox.Show("应用机甲窗标题".Translate(), "应用机甲窗文字".Translate(), "确定".Translate(), 0);
            return false;
        }

        [HarmonyPostfix, HarmonyPatch(typeof(UIMechaMatsGroup), "_OnUpdate")]
        public static void UIMechaMatsGroup_OnUpdate_PostPatch(UIMechaMatsGroup __instance)
        {
            __instance.applyButtonText.text = "Apply Mecha Design for Free".Translate();

        }

    }

    public class LogManager
    {
        public static ManualLogSource Logger;
    }

}