#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace CoreUI
{
	public static class CoreUIInitializator
	{
		public static void SetCoreUIExecutionOrder()
		{
			var maxExecutionOrder = 0;
			var targetMonoScriptName = typeof(CoreUIPresentation).Name;
			var targetMonoScript = default(MonoScript);
			foreach (var monoScript in MonoImporter.GetAllRuntimeMonoScripts())
			{
				if (monoScript.name.Equals(targetMonoScriptName))
				{
					targetMonoScript = monoScript;
					continue;
				}
				var executionOrder = MonoImporter.GetExecutionOrder(monoScript);
				if (executionOrder > maxExecutionOrder) maxExecutionOrder = executionOrder;
			}
			MonoImporter.SetExecutionOrder(targetMonoScript, Mathf.Max(maxExecutionOrder+1, MonoImporter.GetExecutionOrder(targetMonoScript)));
		}	
	}
}
#endif