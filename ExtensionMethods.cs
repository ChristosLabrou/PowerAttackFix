using Kingmaker.Blueprints;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PowerAttackFix
{
	static class ExtensionMethods
	{
		public static void AddComponent(this BlueprintScriptableObject obj, BlueprintComponent component)
		{
			obj.SetComponents(obj.ComponentsArray.AppendToArray(component));
		}

		public static void AddComponent<T>(this BlueprintScriptableObject obj, Action<T> init = null) where T : BlueprintComponent, new()
		{
			obj.SetComponents(obj.ComponentsArray.AppendToArray(Create(init)));
		}


		public static void SetComponents(this BlueprintScriptableObject obj, params BlueprintComponent[] components)
		{
			// Fix names of components. Generally this doesn't matter, but if they have serialization state,
			// then their name needs to be unique.
			var names = new HashSet<string>();
			foreach (var c in components)
			{
				if (string.IsNullOrEmpty(c.name))
				{
					c.name = $"${c.GetType().Name}";
				}
				if (!names.Add(c.name))
				{
					String name;
					for (int i = 0; !names.Add(name = $"{c.name}${i}"); i++) ;
					c.name = name;
				}
			}
			obj.ComponentsArray = components;
			obj.OnEnable(); // To make sure components are fully initialized
		}

		public static void SetComponents(this BlueprintScriptableObject obj, IEnumerable<BlueprintComponent> components)
		{
			SetComponents(obj, components.ToArray());
		}

		public static T[] AppendToArray<T>(this T[] array, T value)
		{
			var len = array.Length;
			var result = new T[len + 1];
			Array.Copy(array, result, len);
			result[len] = value;
			return result;
		}

		public static T[] AppendToArray<T>(this T[] array, params T[] values)
		{
			var len = array.Length;
			var valueLen = values.Length;
			var result = new T[len + valueLen];
			Array.Copy(array, result, len);
			Array.Copy(values, 0, result, len, valueLen);
			return result;
		}

		public static T[] AppendToArray<T>(this T[] array, IEnumerable<T> values) => AppendToArray(array, values.ToArray());

		public static void RemoveComponent(this BlueprintScriptableObject obj, BlueprintComponent component)
		{
			obj.SetComponents(obj.ComponentsArray.RemoveFromArray(component));
		}

		public static void RemoveComponents<T>(this BlueprintScriptableObject obj) where T : BlueprintComponent
		{
			var compnents_to_remove = obj.GetComponents<T>().ToArray();
			foreach (var c in compnents_to_remove)
			{
				obj.SetComponents(obj.ComponentsArray.RemoveFromArray(c));
			}
		}

		public static void RemoveComponents<T>(this BlueprintScriptableObject obj, Predicate<T> predicate) where T : BlueprintComponent
		{
			var compnents_to_remove = obj.GetComponents<T>().ToArray();
			foreach (var c in compnents_to_remove)
			{
				if (predicate(c))
				{
					obj.SetComponents(obj.ComponentsArray.RemoveFromArray(c));
				}
			}
		}
		public static T[] RemoveFromArray<T>(this T[] array, T value)
		{
			var list = array.ToList();
			return list.Remove(value) ? list.ToArray() : array;
		}

		public static T Create<T>(Action<T> init = null) where T : new()
		{
			var result = new T();
			init?.Invoke(result);
			return result;
		}
	}

}