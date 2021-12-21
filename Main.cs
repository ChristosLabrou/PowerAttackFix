using HarmonyLib;
using UnityModManagerNet;

namespace PowerAttackFix
{
	static class Main
	{
		static bool Load(UnityModManager.ModEntry modEntry)
		{
			var harmony = new Harmony(modEntry.Info.Id);
			harmony.PatchAll();
			PostPatchInitializer.Initialize();
			return true;
		}
	}
}
