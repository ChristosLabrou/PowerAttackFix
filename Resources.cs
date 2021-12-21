using Kingmaker.Blueprints;
using System;
using System.Collections.Generic;

namespace PowerAttackFix
{
	static class Resources
	{
		public static readonly Dictionary<BlueprintGuid, SimpleBlueprint> ModBlueprints = new Dictionary<BlueprintGuid, SimpleBlueprint>();

		internal static object GetBlueprint<T>()
		{
			throw new NotImplementedException();
		}
		public static T GetBlueprint<T>(string id) where T : SimpleBlueprint
		{
			var assetId = new BlueprintGuid(System.Guid.Parse(id));
			return GetBlueprint<T>(assetId);
		}
		public static T GetBlueprint<T>(BlueprintGuid id) where T : SimpleBlueprint
		{
			SimpleBlueprint asset = ResourcesLibrary.TryGetBlueprint(id);
			T value = asset as T;
			//if (value == null) { Main.Log($"COULD NOT LOAD: {id} - {typeof(T)}"); }
			return value;
		}
	}
}