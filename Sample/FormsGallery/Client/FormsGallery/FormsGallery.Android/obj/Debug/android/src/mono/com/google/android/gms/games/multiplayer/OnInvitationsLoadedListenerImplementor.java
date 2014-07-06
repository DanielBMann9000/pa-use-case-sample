package mono.com.google.android.gms.games.multiplayer;


public class OnInvitationsLoadedListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.android.gms.games.multiplayer.OnInvitationsLoadedListener
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onInvitationsLoaded:(ILcom/google/android/gms/games/multiplayer/InvitationBuffer;)V:GetOnInvitationsLoaded_ILcom_google_android_gms_games_multiplayer_InvitationBuffer_Handler:Android.Gms.Games.MultiPlayer.IOnInvitationsLoadedListenerInvoker, GooglePlayServicesLib\n" +
			"";
		mono.android.Runtime.register ("Android.Gms.Games.MultiPlayer.IOnInvitationsLoadedListenerImplementor, GooglePlayServicesLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", OnInvitationsLoadedListenerImplementor.class, __md_methods);
	}


	public OnInvitationsLoadedListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == OnInvitationsLoadedListenerImplementor.class)
			mono.android.TypeManager.Activate ("Android.Gms.Games.MultiPlayer.IOnInvitationsLoadedListenerImplementor, GooglePlayServicesLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onInvitationsLoaded (int p0, com.google.android.gms.games.multiplayer.InvitationBuffer p1)
	{
		n_onInvitationsLoaded (p0, p1);
	}

	private native void n_onInvitationsLoaded (int p0, com.google.android.gms.games.multiplayer.InvitationBuffer p1);

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
