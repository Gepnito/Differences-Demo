
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;
using System;

public class AppodealManager : MonoBehaviour
{
    
    private ListenerCallback listener = new ListenerCallback();

    private void Start()
    {
        Appodeal.setTesting(true);
        int adTypes = Appodeal.INTERSTITIAL;
        string appKey = "fee50c333ff3825fd6ad6d38cff78154de3025546d47a84f";
        Appodeal.setAutoCache(adTypes, true);
        Appodeal.initialize(appKey, adTypes);
    }

    public void onInitializationFinished(List<string> errors)
    {
        Appodeal.setInterstitialCallbacks(listener);
    }

    public void ShowInterstitial()
    {
        if (Appodeal.isLoaded(Appodeal.INTERSTITIAL) && Appodeal.canShow(Appodeal.INTERSTITIAL, "default") && !Appodeal.isPrecache(Appodeal.INTERSTITIAL))
        {
            Appodeal.show(Appodeal.INTERSTITIAL);
        }
        else if (!Appodeal.isAutoCacheEnabled(Appodeal.INTERSTITIAL))
        {
            Appodeal.cache(Appodeal.INTERSTITIAL);

        }
    }
    
}  

class ListenerCallback : IInterstitialAdListener
{
    #region Interstitial callback handlers

    // Called when interstitial was loaded (precache flag shows if the loaded ad is precache)
    public void onInterstitialLoaded(bool isPrecache)
    {
        Debug.Log("Interstitial loaded");
    }

    // Called when interstitial failed to load
    public void onInterstitialFailedToLoad()
    {
        Debug.Log("Interstitial failed to load");
    }

    // Called when interstitial was loaded, but cannot be shown (internal network errors, placement settings, etc.)
    public void onInterstitialShowFailed()
    {
        Debug.Log("Interstitial show failed");
    }

    // Called when interstitial is shown
    public void onInterstitialShown()
    {
        Debug.Log("Interstitial shown");
    }

    // Called when interstitial is closed
    public void onInterstitialClosed()
    {
        Debug.Log("Interstitial closed");
    }

    // Called when interstitial is clicked
    public void onInterstitialClicked()
    {
        Debug.Log("Interstitial clicked");
    }

    // Called when interstitial is expired and can not be shown
    public void onInterstitialExpired()
    {
        Debug.Log("Interstitial expired");
    }

    #endregion
}
