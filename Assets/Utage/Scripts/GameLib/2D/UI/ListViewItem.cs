//----------------------------------------------
// UTAGE: Unity Text Adventure Game Engine
// Copyright 2014 Ryohei Tokimura
//----------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace Utage
{

	/// <summary>
	/// リストビュー用のアイテム
	/// </summary>
	[AddComponentMenu("Utage/Lib/UI/ListViewItem")]
	public class ListViewItem : Button
	{
		[SerializeField]
		ListView listView;

		[SerializeField]
		float dragLen = 0.2f;	/// スクロール判定に使う距離

		/// <summary>
		/// ボタンの機能のON・OFF
		/// </summary>
		public bool IsEnableButton
		{
			get { return isEnableButton; }
			set {
				isEnableButton = value;
				UiEffect.enabled = value;
			}
		}
		[SerializeField]
		bool isEnableButton = true;

		bool isScrolled = false;	//リストビューをスクロールしたフラグ

		void Start()
		{
			UiEffect.enabled = IsEnableButton;
		}

		/// <summary>
		/// アイテムの初期化（主にリストビューから呼び出すときに使う）
		/// </summary>
		/// <param name="listView">リストビュー</param>
		/// <param name="target">ボタンを押したときのメッセージの送り先</param>
		/// <param name="index">インデックス</param>
		public void InitListItem(ListView listView, GameObject target, int index)
		{
			this.listView = listView;
			this.Target = target;
			this.Index = index;
		}

		/// <summary>
		/// タッチしたとき
		/// </summary>
		/// <param name="touch">タッチ入力データ</param>
		void OnTouchDown(TouchData2D touch)
		{
			isScrolled = false;
		}

		/// <summary>
		/// タッチが離されたとき
		/// </summary>
		/// <param name="touch">タッチ入力データ</param>
		void OnTouchUp(TouchData2D touch)
		{
			if (isScrolled)
			{
				listView.ScrollEnd();
			}
		}

		/// <summary>
		/// ドラッグ中
		/// </summary>
		/// <param name="touch">タッチ入力データ</param>
		void OnDrag(TouchData2D touch)
		{
			if (!listView.IsScrollEnable()) return;

			if (!isScrolled)
			{
				Vector2 dist = touch.TouchPoint - touch.StartPoint;
				//ある程度の距離を動くと、スクロール判定
				if (dist.sqrMagnitude > dragLen * dragLen)
				{
					isScrolled = true;
					UiEffect.EffectUp();
				}
			}

			if (isScrolled)
			{
				Vector2 move = this.transform.position;
				move = touch.DragPoint - move;
				if (!listView.Scroll(move))
				{
					touch.Cancel();
				}
			}
		}

		/// <summary>
		/// クリック処理されたとき
		/// </summary>
		/// <param name="touch">タッチ入力データ</param>
		protected override void OnClick(TouchData2D touch)
		{
			if (!isScrolled)
			{
				if (IsEnableButton)
				{
					base.OnClick(touch);
				}
			}
		}
	}
}