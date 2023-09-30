using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PlatformDependantBehavior:BaseMonoBehavior
{
    public bool IsUnityStandaloneOsx;
    public bool IsUnityStandaloneWin;
    public bool IsUnityStandaloneLinux;
    public bool IsUnityWii;
    public bool IsUnityIos;
    public bool IsUnityIphone;
    public bool IsUnityAndroid;
    public bool IsUnityLumia;
    public bool IsUnityTizen;
    public bool IsUnityTvos;
    public bool IsUnityWsa;
    public bool IsUnityWsa100;
    public bool IsUnityWebgl;
    public bool IsUnityFacebook;

    public enum PlatformEnum
    {
        None,
        UNITY_STANDALONE_OSX,
        UNITY_STANDALONE_WIN,
        UNITY_STANDALONE_LINUX,
        UNITY_WII,
        UNITY_IOS,
        UNITY_IPHONE,
        UNITY_ANDROID,
        UNITY_LUMIN,
        UNITY_TIZEN,
        UNITY_TVOS,
        UNITY_WSA,
        UNITY_WSA_10_0,
        UNITY_WEBGL,
        UNITY_FACEBOOK,
    }
    protected void Start()
    {
        var platform = GetCurrentPlatform();
        if (IsUnityStandaloneOsx == false && platform==PlatformEnum.UNITY_STANDALONE_OSX)
        {
            DestroyImmediate(gameObject);
        }
        if (IsUnityStandaloneWin == false && platform == PlatformEnum.UNITY_STANDALONE_WIN)
        {
            DestroyImmediate(gameObject);
        }
        if (IsUnityStandaloneLinux == false && platform == PlatformEnum.UNITY_STANDALONE_LINUX)
        {
            DestroyImmediate(gameObject);
        }
        if (IsUnityWii == false && platform == PlatformEnum.UNITY_WII)
        {
            DestroyImmediate(gameObject);
        }
        if (IsUnityIos == false && platform == PlatformEnum.UNITY_IOS)
        {
            DestroyImmediate(gameObject);
        }
        if (IsUnityIphone == false && platform == PlatformEnum.UNITY_IPHONE)
        {
            DestroyImmediate(gameObject);
        }
        if (IsUnityAndroid == false && platform == PlatformEnum.UNITY_ANDROID)
        {
            DestroyImmediate(gameObject);
        }
        if (IsUnityLumia == false && platform == PlatformEnum.UNITY_LUMIN)
        {
            DestroyImmediate(gameObject);
        }
        if (IsUnityTizen == false && platform == PlatformEnum.UNITY_TIZEN)
        {
            DestroyImmediate(gameObject);
        }
        if (IsUnityTvos == false && platform == PlatformEnum.UNITY_TVOS)
        {
            DestroyImmediate(gameObject);
        }
        if (IsUnityWsa == false && platform == PlatformEnum.UNITY_WSA)
        {
            DestroyImmediate(gameObject);
        }
        if (IsUnityWsa100 == false && platform == PlatformEnum.UNITY_WSA_10_0)
        {
            DestroyImmediate(gameObject);
        }
        if (IsUnityWebgl == false && platform == PlatformEnum.UNITY_WEBGL)
        {
            DestroyImmediate(gameObject);
        }
        if (IsUnityFacebook == false && platform == PlatformEnum.UNITY_FACEBOOK)
        {
            DestroyImmediate(gameObject);
        }
    }

    protected PlatformEnum GetCurrentPlatform()
    {
        PlatformEnum ret=PlatformEnum.None;

#if UNITY_STANDALONE_OSX
        ret=PlatformEnum.UNITY_STANDALONE_OSX;
#endif
#if UNITY_STANDALONE_WIN
        ret = PlatformEnum.UNITY_STANDALONE_WIN;
#endif
#if UNITY_STANDALONE_LINUX
        ret=PlatformEnum.UNITY_STANDALONE_LINUX;
#endif
#if UNITY_WII
        ret=PlatformEnum.UNITY_WII;
#endif
#if UNITY_IOS
        ret=PlatformEnum.UNITY_IOS;
#endif
#if UNITY_IPHONE
        ret=PlatformEnum.UNITY_IPHONE;
#endif
#if UNITY_ANDROID
        ret=PlatformEnum.UNITY_ANDROID;
#endif
#if UNITY_LUMIN
        ret=PlatformEnum.UNITY_LUMIN;
#endif
#if UNITY_TIZEN
        ret=PlatformEnum.UNITY_TIZEN;
#endif
#if UNITY_TVOS
        ret=PlatformEnum.UNITY_TVOS;
#endif
#if UNITY_WSA
        ret=PlatformEnum.UNITY_WSA;
#endif
#if UNITY_WSA_10_0
        ret=PlatformEnum.UNITY_WSA_10_0;
#endif
#if UNITY_WEBGL
        ret=PlatformEnum.UNITY_WEBGL;
#endif
#if UNITY_FACEBOOK
        ret=PlatformEnum.UNITY_FACEBOOK;
#endif


        return ret;
    }


}
