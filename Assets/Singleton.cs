using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
	#region  Properties

	protected virtual void Awake() {
		instance = gameObject.GetComponent<T>();
	}

	static bool doneOnce;

	/// <summary>
	/// Returns the instance of this singleton.
	/// </summary>
	public static T Instance
	{
		[System.Diagnostics.DebuggerStepThrough]
		get {
			if(instance == null) {
				instance = (T)GameObject.FindObjectOfType(typeof(T));

				if(instance == null && !doneOnce) {
					doneOnce = true;
				}
			}

			return instance;
		}
	}

	#endregion

	private static T instance;
}