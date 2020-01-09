using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class HealthThreshold
{
	[Tooltip("The threshold at or below which the events will occur.")]
	public float healthThreshold;
	[Tooltip("An instantaneous event that occurs once health falls below the healthThreshold.")]
	public UnityEvent thresholdEvent;
	[Tooltip("An ongoing event that occurs while health is below the healthThreshold.")]
	public UnityEvent continuousthresholdEvent;
}

public class HealthSystem : MonoBehaviour
{
	[Header("Health")]
	[SerializeField] private float maxHealth = 10f;
	[SerializeField] private float minHealth = 0f;
	[SerializeField] private HealthThreshold[] thresholds;

	public UnityEvent DeathEvent;
	public UnityEvent HurtEvent;
	public UnityEvent HealEvent;

	private float health;

	#region Constructors
	/// <summary>
	/// Initializes a new instance of the <see cref="Character"/> class.
	/// </summary>
	/// <param name="hp">The <see cref="Character"/>'s initial hp.</param>
	HealthSystem(float hp)
	{
		maxHealth = hp;
		Init();
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="Character"/> class.
	/// </summary>
	/// <param name="minHP">The minimum hp.</param>
	/// <param name="maxHP">The maximum hp.</param>
	HealthSystem(float minHP, float maxHP)
	{
		maxHealth = maxHP;
		minHealth = minHP;
		Init();
	}
	#endregion

	// Start is called before the first frame update
	void Start()
	{
		Init();
	}

	private void Init()
	{
		health = maxHealth;
		if (DeathEvent == null) DeathEvent = new UnityEvent();
		if (HurtEvent == null) HurtEvent = new UnityEvent();
		if (HealEvent == null) HealEvent = new UnityEvent();
		foreach(HealthThreshold ht in thresholds)
		{
			if (ht.thresholdEvent == null) ht.thresholdEvent = new UnityEvent();
		}
	}

	// Update is called once per frame
	void Update()
	{

	}

	/// <summary>
	/// Adjusts the Characters health by the specified +/- value.
	/// </summary>
	/// <param name="value">The +/- value to change the character's health by.</param>
	public void Health(float value)
	{
		if(health + value <= minHealth)
		{
			health = minHealth;
			DeathEvent.Invoke();
			return;
		}
		if (health + value < maxHealth)
		{
			health += value;
		}

		if(value < 0)
			HurtEvent.Invoke();
		else if(value > 0)
			HealEvent.Invoke();
	}
}
