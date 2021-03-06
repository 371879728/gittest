/**
 *
 *  このソースはLive2D関連アプリの開発用途に限り
 *  自由に改変してご利用頂けます。
 *
 *  (c) CYBERNOIDS Co.,Ltd. All rights reserved.
 */
using System.Collections;
using System.Collections.Generic;
using live2d;

public class L2DBaseModel 
{
	//標準パラメータ
	protected const string PARAM_ANGLE_X="PARAM_ANGLE_X";
	protected const string PARAM_ANGLE_Y="PARAM_ANGLE_Y";
	protected const string PARAM_ANGLE_Z="PARAM_ANGLE_Z";
	protected const string PARAM_BODY_X="PARAM_BODY_ANGLE_X";
	protected const string PARAM_BREATH="PARAM_BREATH";
	protected const string PARAM_EYE_BALL_X="PARAM_EYE_BALL_X";
	protected const string PARAM_EYE_BALL_Y="PARAM_EYE_BALL_Y";
	protected const string PARAM_MOUTH_OPEN_Y="PARAM_MOUTH_OPEN_Y";

	// モデル関連
	protected Live2DModelUnity 		live2DModel=null;		// Live2Dモデルクラス
	protected L2DModelMatrix		modelMatrix=null;		// Live2Dモデラー上の座標系からワールド座標系へ変換するための行列

	// モーション・状態管理
	protected Dictionary<string,AMotion> 	expressions ;	// 表情モーションデータ
	protected Dictionary<string,AMotion> 	motions ;		// モーションデータ

	protected L2DMotionManager 		mainMotionManager;		// メインモーション
	protected L2DMotionManager 		expressionManager;		// 表情
	protected L2DEyeBlink 			eyeBlink;				// 自動目パチ
	protected L2DPhysics 			physics;				// 物理演算
	protected L2DPose 				pose;					// ポーズ。腕の切り替えなど。

	protected bool 					initialized = false;	// 初期化状態
	protected bool 					updating = false;		// 読み込み中ならtrue
	protected bool 					lipSync = false;		// リップシンクが有効かどうか
	protected float 				lipSyncValue;			// 基本は0～1

	//傾きの値。-1から1の範囲
	protected float 				accelX=0;
	protected float 				accelY=0;
	protected float 				accelZ=0;

	//向く方向の値。-1から1の範囲
	protected float 				dragX=0;
	protected float 				dragY=0;

	protected long 					startTimeMSec;
	
	
	public L2DBaseModel()
	{
		//モーションマネージャーを作成
		mainMotionManager = new L2DMotionManager();//MotionQueueManagerクラスからの継承なので、使い方は同一
		expressionManager = new L2DMotionManager();
		
		motions = new Dictionary<string, AMotion>();
		expressions = new Dictionary<string, AMotion>();
	}
	
	
	public L2DModelMatrix getModelMatrix()
	{
		return modelMatrix;
	}
	
	
	/**
	 * 初期化されている場合はtrue。
	 * 更新と描画可能になったときに初期化完了とみなす。
	 *
	 * @return
	 */
	public bool isInitialized() 
	{
		return initialized;
	}


	public void setInitialized(bool v)
	{
		initialized = v ;
	}
	
	
	/**
	 * モデルの読み込み中はtrue。
	 * 更新と描画可能になったときに読み込み完了とみなす。
	 *
	 * @return
	 */
	public bool isUpdating() {
		return updating;
	}


	public void setUpdating(bool v)
	{
		updating=v;
	}


	/**
	 * Live2Dモデルクラスを取得する。
	 * @return
	 */
	public ALive2DModel getLive2DModel() {
		return live2DModel;
	}


	public void setLipSync(bool v)
	{
		lipSync=v;
	}


	public void setLipSyncValue(float v)
	{
		lipSyncValue=v;
	}


	public void setAccel(float x,float y,float z)
	{
		accelX=x;
		accelY=y;
		accelZ=z;
	}


	public void setDrag(float x,float y)
	{
		dragX=x;
		dragY=y;
	}


	public MotionQueueManager getMainMotionManager()
	{
		return mainMotionManager;
	}


	public MotionQueueManager getExpressionManager()
	{
		return expressionManager;
	}
}