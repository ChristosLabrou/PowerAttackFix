using HarmonyLib;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.UnitLogic.ActivatableAbilities;

namespace PowerAttackFix
{
	class PowerAttackPatch
	{
		[HarmonyPatch(typeof(BlueprintsCache), "Init")]
		public static class BlueprintCache_Init_Patch
		{
			public static void Postfix()
			{
				PatchPowerAttack();
			}

			public static void PatchPowerAttack()
			{
				var PowerAttackToggle = Resources.GetBlueprint<BlueprintActivatableAbility>("a7b339e4f6ff93a4697df5d7a87ff619");
				PowerAttackToggle.DeactivateImmediately = true;
			}
		}
	}
}
