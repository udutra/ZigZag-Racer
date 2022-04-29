using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener {

    public static AdsManager instance;
    public string gameID = "4731112";

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    private void Start() {
        Advertisement.Initialize(gameID);
        Advertisement.AddListener(this);
    }

    public void ShowAd() {
        if (Advertisement.IsReady("Interstitial_Android")) {
            Advertisement.Show("Interstitial_Android");
        }
    }

    public void ShowRewaredAd() {
        if (Advertisement.IsReady("Rewarded_Android")) {
            Advertisement.Show("Rewarded_Android");
        }
    }

    public void OnUnityAdsReady(string placementId) {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidError(string message) {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidStart(string placementId) {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult) {

        if (showResult == ShowResult.Finished) {
            //rewward the user
        }

        GameManager.instance.ReloadLevel();
    }
}
