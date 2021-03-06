using UnityEngine;
using System;
using System.Collections;
using live2d;

public class main : MonoBehaviour 
{
	public TextAsset mocFile ;
	public Texture2D[] texture ;
	public TextAsset[] mtnFiles; // mtnファイル

	private Live2DModelUnity live2DModel;
	private Live2DMotion 		motion; // Live2Dモーションクラス
	private MotionQueueManager 	motionManager; // モーション管理クラス
	
	void Start () 
	{
		Live2D.init();
		
		live2DModel = Live2DModelUnity.loadModel(mocFile.bytes);
		for(int i = 0; i < texture.Length; i++)
		{
			live2DModel.setTexture( i, texture[i] ) ;
		}

		// 描画モードを指定
		live2DModel.setRenderMode( Live2D.L2D_RENDER_DRAW_MESH );
		
		// モーションのインスタンスの作成（mtnの読み込み）と設定
		for (int i = 0; i < mtnFiles.Length; i++)
		{
			motion = Live2DMotion.loadMotion (mtnFiles [i].bytes);
		}
		motion.setLoop( true );
		
		// モーション管理クラスのインスタンスの作成
		motionManager = new MotionQueueManager();
		// モーションの再生
		//motionManager.startMotion( motion, false );
	}
	
	
	void OnRenderObject()
	{
		Matrix4x4 m1=Matrix4x4.Ortho(0,1000.0f,1000.0f,0,-0.5f,0.5f);
		Matrix4x4 m2 = transform.localToWorldMatrix;
		Matrix4x4 m3 = m2*m1;
		live2DModel.setMatrix(m3);
		if( live2DModel == null ) return ;
		
		double t = (UtSystem.getUserTimeMSec()/1000.0) * 2 * Math.PI  ;
		live2DModel.setParamFloat( "PARAM_ANGLE_X" , (float) (30 * Math.Sin( t/3.0 )) ) ;

		/*// 再生中のモーションからモデルパラメータを更新
		motionManager.updateParam( live2DModel );
		
		// マウスの座標で顔の角度XYを動かす（加算）
		float targetX = 2 * Input.mousePosition.x / Screen.width - 1 ;
		float targetY = 2 * Input.mousePosition.y / Screen.height - 1 ;
		live2DModel.addToParamFloat ( "PARAM_ANGLE_X", 30 * targetX, 1 );
		live2DModel.addToParamFloat ( "PARAM_ANGLE_Y", 30 * targetY, 1 );
		*/
		
		live2DModel.update();
		live2DModel.draw();
	}
}
