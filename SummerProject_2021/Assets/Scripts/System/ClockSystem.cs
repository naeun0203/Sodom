using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockSystem : MonoBehaviour
{
    #region Clock
    // ���� �÷��� �ϰ� �ִ� ��¥
    private int _day = 1;

	public int Day
	{
		get { return _day; }
		set
		{
			_day = value;
		}
	}

	/// <summary>
	/// �ð� ����� ���� �ð� Ÿ�� ����ü
	/// </summary>
	public struct Clock
	{
		public int hour;
		public int min;
		public int sec;

		public Clock(int _hour, int _min) : this()
		{
			hour = _hour;
			min = _min;
		}
	}

	public int Hour
	{
		get { return DayTime.hour; }
		set
		{
			DayTime.hour = value;
			if (DayTime.hour >= 24)
			{
				Day++;
				DayTime.hour = 0;
			}

			if (Day == 30 && DayTime.hour == 18)
			{
			}
		}
	}

	public int Min
	{
		get { return DayTime.min; }
		set
		{
			DayTime.min = value;

			while (DayTime.min >= 60)
			{
				Hour++;
				DayTime.min = DayTime.min - 60;
			}
		}
	}

	public int Sec
	{
		get { return DayTime.sec; }
		set
		{
			DayTime.sec = value;

			while (DayTime.sec >= 60)
			{
				Min++;
				DayTime.sec = DayTime.sec - 60;
			}
		}
	}

	#endregion

	#region Variables
	private GameManager GM;
	#endregion

	private void PropInit()
    {
		GM = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    private void Awake()
    {
		PropInit();
    }

    private void Start()
    {
		StartCoroutine(TimeControl());
    }

    /// <summary>
    /// �ð� ���
    /// </summary>
    /// <returns></returns>
    public Clock DayTime = new Clock(0, 0);

	private IEnumerator TimeControl()
	{
		while (!GM.isEnd)
		{
			yield return new WaitForSeconds(0.417f); // 60�ʴ� 144�� �߰� => 1�ʸ� ��ٸ��� 
			Min = Min + 1; // Default : 1, Day Test : 720
						   // DayTime.Sec = DayTime.Sec + 24;
		}
	}
}
