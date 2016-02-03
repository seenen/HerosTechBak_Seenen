using UnityEngine;
using System.Collections;

namespace AssetBundleEditor
{

    public class ABLoadMpq : ABLoad
    {
        public ABLoadMpq(int version, bool autorelease = true) 
            : base(version, autorelease)
        {
            mABDataType = ABDataType.Mpq;
        }

        public override bool Create(string url)
        {
            base.Create(url);

            int index = mUrl.IndexOf("/StreamingAssets/");

            if (index == -1)
            {
                Debug.LogError("/StreamingAssets/ == null" + " " + mUrl);

                return false;
            }

            mUrl = mUrl.Substring(index);

            {
#if UNITY_EDITOR_ZSN
                        try
                        {
                            string mpqurl = url.Substring(url.IndexOf("/StreamingAssets/"));

                            mFilePath = mUrl.Substring(mUrl.IndexOf(ProjectSystem.PrefixPlatform) + ProjectSystem.PrefixPlatform.Length);

                            if (mUrl.IndexOf("scene") != -1)
                            {
                                int a = 0;
                            }

                            lock (mFilePath)
                            {
                                if (!System.IO.File.Exists(mFilePath))
                                { 
                                    byte[] data = CommonUI_Unity3D.Impl.UnityDriver.UnityInstance.LoadData(CommonUI_Unity3D.Impl.UnityDriver.PREFIX_MPQ + mpqurl);

                                    if (data == null)
                                    {
                                        Debuger.LogError("CommonUI_Unity3D.Impl.UnityDriver.LoadData data == null" + mFilePath );
                                    }

                                    string dir = System.IO.Path.GetDirectoryName(mFilePath);

                                    if (!System.IO.Directory.Exists(dir))
                                        System.IO.Directory.CreateDirectory(dir);

                                    System.IO.File.WriteAllBytes(mFilePath, data);
                                }
                                else
                                {
                                    Debuger.Log("Exist " + mFilePath);
                                }
                            }

                            Ref--;


                            return Create(mUrl, ABDataType.Normal);
                        }
                        catch(System.Exception e)
                        {
                            Debug.LogException(e);

                            return false;
                        }
#else
                if (createrequest == null)
                {
                    //createrequest = CommonUI_Unity3D.Impl.UnityDriver.UnityInstance.LoadAssetBundle(CommonUI_Unity3D.Impl.UnityDriver.PREFIX_MPQ + mUrl, out mpqmemorysize);
                    totalmpqsize += mpqmemorysize;

                    if (createrequest == null)
                    {
                        ErrorMsg = "UnityDriver.UnityInstance.LoadAssetBundle createrequest == Null";
                        return false;
                    }

                    ErrorMsg = string.Empty;
                }
#endif
            }


            return true;

        }

        public override void Release(bool force = false)
        {
            base.Release(force);

            if (force) mAutoRelease = force;

            if (mAutoRelease && Ref == 0)
            {
                if (createrequest != null)
                {
                    if (createrequest.assetBundle != null)
                        createrequest.assetBundle.Unload(false);

                    createrequest = null;
                }
            }
        }

        public override IEnumerator Waiting()
        {
            {
                yield return null;
            }
        }

        public override bool IsWWW()
        {
            {
                if (createrequest == null)
                    return true;

                return createrequest.isDone;
            }
        }

        public override AssetBundle GetAb()
        {
            if (createrequest != null) return createrequest.assetBundle;
            else return null;
        }

        public override void Unload()
        {
            Clear();

            if (createrequest != null)
            {
                //if (!Error())
                {
                    if (createrequest.assetBundle != null)
                        createrequest.assetBundle.Unload(true);
                }

                createrequest = null;

                totalmpqsize -= mpqmemorysize;
                mpqmemorysize = 0;
            }

#if UNITY_EDITOR_ZSN
            try
            {
                if (!string.IsNullOrEmpty(mFilePath))
                    System.IO.File.Delete(mFilePath);
            }
            catch(System.Exception e)
            {
                Debug.LogException(e);
            }
#endif
        }

        public override string GetErrorMsg()
        {
            return string.Empty;
        }

    }
}
