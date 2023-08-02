using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsShowListener, IUnityAdsLoadListener, IUnityAdsInitializationListener
{
	[SerializeField] private bool testMode = true;
	public static AdManager Instance;
	
#if UNITY_ANDROID
	private const string GameId = "5368030";
#elif UNITY_IOS
	private const string GameId = "5368031";
#endif

	private GameOverHandler _gameOverHandler;

	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
		}
		else
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);

			// Advertisement.AddListener(this);
			Advertisement.Initialize(GameId, testMode, this);
		}
	}

	public void ShowAdd(GameOverHandler gameOverHandler)
	{
		_gameOverHandler = gameOverHandler;
		Advertisement.Show("rewardedVideo", this);
	}

	public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
	{
		Debug.Log("AdsShowFailure");
	}

	public void OnUnityAdsShowStart(string placementId)
	{
		Debug.Log("AdsShowStart");
	}

	public void OnUnityAdsShowClick(string placementId)
	{
		Debug.Log("AdsShowClick");
	}

	public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
	{
		switch (showCompletionState)
		{
			case UnityAdsShowCompletionState.SKIPPED:
				// ad was skipped
				break;
			case UnityAdsShowCompletionState.COMPLETED:
				_gameOverHandler.ContinueGame();
				break;
			case UnityAdsShowCompletionState.UNKNOWN:
				Debug.LogWarning("Add Failed");
				break;
		}
		Debug.Log("AdsShowComplete");
	}

	public void OnUnityAdsAdLoaded(string placementId)
	{
		Debug.Log("AdsAdLoaded");
	}

	public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
	{
		Debug.Log("AdsFailedToLoad");
	}

	public void OnInitializationComplete()
	{
		Debug.Log("OnInitializationComplete");
	}

	public void OnInitializationFailed(UnityAdsInitializationError error, string message)
	{
		Debug.Log("OnInitializationFailed");
	}
}
