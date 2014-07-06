package mono.com.google.android.gms.games.leaderboard;


public class OnLeaderboardMetadataLoadedListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.android.gms.games.leaderboard.OnLeaderboardMetadataLoadedListener
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onLeaderboardMetadataLoaded:(ILcom/google/android/gms/games/leaderboard/LeaderboardBuffer;)V:GetOnLeaderboardMetadataLoaded_ILcom_google_android_gms_games_leaderboard_LeaderboardBuffer_Handler:Android.Gms.Games.LeaderBoard.IOnLeaderboardMetadataLoadedListenerInvoker, GooglePlayServicesLib\n" +
			"";
		mono.android.Runtime.register ("Android.Gms.Games.LeaderBoard.IOnLeaderboardMetadataLoadedListenerImplementor, GooglePlayServicesLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", OnLeaderboardMetadataLoadedListenerImplementor.class, __md_methods);
	}


	public OnLeaderboardMetadataLoadedListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == OnLeaderboardMetadataLoadedListenerImplementor.class)
			mono.android.TypeManager.Activate ("Android.Gms.Games.LeaderBoard.IOnLeaderboardMetadataLoadedListenerImplementor, GooglePlayServicesLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onLeaderboardMetadataLoaded (int p0, com.google.android.gms.games.leaderboard.LeaderboardBuffer p1)
	{
		n_onLeaderboardMetadataLoaded (p0, p1);
	}

	private native void n_onLeaderboardMetadataLoaded (int p0, com.google.android.gms.games.leaderboard.LeaderboardBuffer p1);

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
