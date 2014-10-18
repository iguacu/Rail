using UnityEngine;
using System.Collections;

public class GyroButton : MonoBehaviour {

	private GyroTest gyroScript;
	private Vector2 touchPos;
	private GUITexture gui;

	delegate void listener( ArrayList touches );

	event listener touchBegin, touchMove, touchEnd;

	void Start()
	{
		gyroScript = GameObject.Find ("Pivot").GetComponent<GyroTest> ();
		gui = GetComponent<GUITexture> ();

		touchBegin += ( touches ) =>
		{
			Debug.Log( "begin" );
			gyroScript.startGyro();
		};
		touchEnd += ( touches ) =>
		{
			Debug.Log( "end" );
			gyroScript.endGyro();
		};
		touchMove += ( touches ) =>
		{
			Debug.Log( "move" );
		};
		//터치하면 각각 begin, end, move 호출
	}

	void Update()
	{
		int count = Input.touchCount;

		if( count == 0 )
		{
			return;
		}

		//이벤트를 체크할 플래그
		bool begin = false;
		bool move = false;
		bool end = false;
		
		//인자로 보낼 ArrayList;
		ArrayList result = new ArrayList();
		
		for(int i = 0; i < count; i++)
		{
			Touch touch = Input.GetTouch(i);

			if (!gui.HitTest (touch.position))
			{
				return;
			}

			result.Add( touch ); //보낼 인자에 추가
			if(touch.phase==TouchPhase.Began && touchBegin!=null)
			{
				begin = true;
			}
			else if(touch.phase==TouchPhase.Moved && touchMove!=null)
			{
				move = true;
			}
			else if(touch.phase==TouchPhase.Ended && touchEnd!=null)
			{
				end = true;
			}
			else
			{

			}
		}
		//포인트중 하나라도 상태를 가졌다면..
		if( begin ) touchBegin( result);
		if( end ) touchEnd( result);
		if( move ) touchMove( result);
	}
}
