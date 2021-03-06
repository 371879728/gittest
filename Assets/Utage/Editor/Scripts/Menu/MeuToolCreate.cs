//----------------------------------------------
// UTAGE: Unity Text Adventure Game Engine
// Copyright 2014 Ryohei Tokimura
//----------------------------------------------

using UnityEditor;
using UnityEngine;
using System.IO;

namespace Utage
{

	public class MeuToolCreate : ScriptableObject
	{
		/// <summary>
		/// 各種マネージャーを作成
		/// </summary>
		[MenuItem(MeuToolOpen.MeuToolRoot + "Create/Managers", priority = 10)]
		static void CreateManagers()
		{
			EditorWindow.GetWindow(typeof(CreateManagersWindow), false, "Mangagers");
		}

		/// <summary>
		/// ADVエンジンを作成
		/// </summary>
		[MenuItem(MeuToolOpen.MeuToolRoot + "Create/AdvEngile", priority = 11)]
		static void OpenCreateAdvEngineWindow()
		{
			EditorWindow.GetWindow(typeof(CreateAdvEngineWindow), false, "AdvEngine");
		}

		/// <summary>
		/// ADVエンジンを起動させる機能を作成
		/// </summary>
		[MenuItem(MeuToolOpen.MeuToolRoot + "Create/AdvEngine Starter",priority=12)]
		static void CreateAdvEngineStarter()
		{
			GameObject go = new GameObject("AdvEngine Starter");
			go.AddComponent<AdvEngineStarter>();
			Selection.activeGameObject = go;
			Undo.RegisterCreatedObjectUndo(go, "CreateAdvEngineStarter");
		}
	}
}