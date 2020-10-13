using System.Collections;
using System;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;

public class AdManager : MonoBehaviour
{
	BannerView bannerAD;
	public static AdManager Instance;

	[SerializeField] private string banner_ID = "ca-app-pub-3940256099942544/6300978111";
	[SerializeField] private string Interstitial_AD_ID = "ca-app-pub-3940256099942544/1033173712";

	private InterstitialAd interstitial;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
		else
			Destroy(this.gameObject);
	}

	private void Start()
	{
		// Initialize the Google Mobile Ads SDK.
		MobileAds.Initialize(initStatus => { }); // For Publishing
		this.RequestBanner();
	}

	private void RequestBanner()
	{
		//For real ad
		//AdRequest adRequest = new AdRequest.Builder().Build();

		bannerAD = new BannerView(banner_ID, AdSize.Banner, AdPosition.Bottom);

		bannerAD.OnAdLoaded += HandleOnAdLoaded;
		bannerAD.OnAdFailedToLoad += HandleOnAdFailedToLoad;

		AdRequest adRequest = new AdRequest.Builder().Build();
		// For Testing ad

		bannerAD.LoadAd(adRequest);
	}

	private void Display_Banner()
	{
		bannerAD.Show();
	}

	public void HandleOnAdLoaded(object sender, EventArgs args)
	{
		Display_Banner();
	}

	public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		StartCoroutine(RequestNewBanner());
	}

	IEnumerator RequestNewBanner()
	{
		yield return new WaitForSeconds(5); //wait 5 sec
		RequestBanner();
	}

	public void RequestInterstitial()
	{
		// Initialize an InterstitialAd.
		this.interstitial = new InterstitialAd(Interstitial_AD_ID);

		this.interstitial.OnAdLoaded += HandleOnInterstitialLoaded;
		this.interstitial.OnAdFailedToLoad += HandleOnInterstitialFailedToLoad;
		this.interstitial.OnAdOpening += HandleOnInterstitialOpened;
		this.interstitial.OnAdClosed += HandleOnInterstitialClosed;
		this.interstitial.OnAdLeavingApplication += HandleOnInterstitialLeavingApplication;

		AdRequest request = new AdRequest.Builder().Build();
		this.interstitial.LoadAd(request);
	}


	public void ShowInterstitialAd()
	{
		if (this.interstitial.IsLoaded())
		{
			this.interstitial.Show();
		}
	}

	// FOR EVENTS AND DELEGATES FOR ADS
	public void HandleOnInterstitialLoaded(object sender, EventArgs args)
	{
		if (this.interstitial.IsLoaded())
		{
			this.interstitial.Show();
		}
	}

	public void HandleOnInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		MonoBehaviour.print("Ad Failed To Load");
	}

	public void HandleOnInterstitialOpened(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdOpened event received");
	}

	public void HandleOnInterstitialClosed(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdClosed event received");
	}

	public void HandleOnInterstitialLeavingApplication(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdLeavingApplication event received");
	}
}
