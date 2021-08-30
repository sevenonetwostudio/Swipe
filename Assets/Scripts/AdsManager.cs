using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{

    public static AdsManager instance;

#if UNITY_IOS
    string gameID = "4266472";
#else
    string gameID = "4266473";
#endif

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Advertisement.Initialize(gameID);

        Advertisement.AddListener(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowAd()
    {
        if(Advertisement.IsReady("Interstitial_iOS"))
        {
            Advertisement.Show("Interstitial_iOS");
        }
    }

    public void ShowRewardedAd()
    {
        if (Advertisement.IsReady("Rewarded_iOS"))
        {
            Advertisement.Show("Rewarded_iOS");
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidError(string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        
        GameManager.instance.ReloadLevel();
    }
}
