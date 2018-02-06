using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Framework.Utility.Tools
{
    /// <summary>
    /// .Net缓存帮助类
    /// </summary>
    public class CacheHelper
    {

        /// <summary>
        /// 缓存池对象
        /// </summary>
        private static ObjectCache MyCachePool;

        /// <summary>
        /// 缓存配置项容器
        /// </summary>
        private static List<CacheConfigItem> CacheConfigContainer;  
         
        /// <summary>
        /// 缓存项的配置文件路径
        /// </summary>
        private static string _configPath =AppDomain.CurrentDomain.BaseDirectory+ConfigHelper.GetAppSetting("CacheConfigPath");

        /// <summary>
        /// 初始化配置项
        /// </summary>
        private static void initCacheConfig()
        {
            CacheConfigContainer = new List<CacheConfigItem>();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(_configPath);
            XmlNodeList nodes = xmlDoc.SelectNodes("/CacheItems/Item");
            string CacheName;
            string TimeOut;
            string OverPolicy;
            foreach (XmlNode node in nodes)
            {
                CacheName = node.Attributes["CacheName"].Value;
                TimeOut = node.Attributes["TimeOut"].Value;
                OverPolicy = node.Attributes["OverPolicy"].Value;
                if (!string.IsNullOrEmpty(CacheName)&& !string.IsNullOrEmpty(TimeOut)&&!string.IsNullOrEmpty(OverPolicy))
                {
                    CacheConfigContainer.Add(new CacheConfigItem
                    {
                        CacheName = CacheName,
                        TimeOut = Convert.ToInt32(TimeOut),
                        OverPolicy = Convert.ToInt32(OverPolicy)
                    });
                }
                else
                {
                    throw new Exception("缓存配置信息出错,缓存配置名(CacheName,TimeOut,OverPolicy)不允许为空");
                }
            }
        }

        /// <summary>
        /// 静态构造函数完成配置文件的初始化
        /// </summary>
        static CacheHelper()
        {
            //初始化线程池
            MyCachePool = MemoryCache.Default;
            //初始化配置
            initCacheConfig();
        }
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="ChcheType"></param>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static bool SetCache(string CacheType, string Key, object Value)
        {
            var ConfigItem = CacheConfigContainer.FirstOrDefault(m => m.CacheName == CacheType);
            if (ConfigItem != null)
            {
                //缓存过期策略
                CacheItemPolicy Policy = new CacheItemPolicy();
                if (ConfigItem.OverPolicy == 1)
                    //它表示的是一个绝对时间过期，当超过设置时间后，Cache内容就会过期。
                    Policy.AbsoluteExpiration = DateTime.Now.AddSeconds(ConfigItem.TimeOut);
                else
                    //它表示按访问频度决定超期 它表示当对象3秒钟内没有得到访问时，就会过期。相对的，如果对象一直被访问，则不会过期。这两个策略并不能同时使用。
                    Policy.SlidingExpiration = TimeSpan.FromSeconds(ConfigItem.TimeOut);

                MyCachePool.Set(Key, Value, Policy, CacheType);
                return true;
            }
            else
                throw new Exception(string.Format("不存在{0}的缓存配置信息", CacheType));
        }

        /// <summary>
        /// 获取缓存的值
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="ChcheType"></param>
        /// <returns></returns>
        public static object GetValue(string Key, string CacheType)
        {
            return MyCachePool.Get(Key, CacheType);
        }

        /// <summary>
        /// 根据泛型获取缓存的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetValue<T>(string Key, string CacheType)
        {
           return JsonHelper.Deserialize<T>(JsonHelper.ToJsonString(MyCachePool.Get(Key, CacheType)));
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="CacheType"></param>
        /// <returns></returns>
        public static object Remove(string Key, string CacheType)
        {
           return MyCachePool.Remove(Key, CacheType);
        }
    }


    public class CacheConfigItem
    {
        /// <summary>
        /// 缓存类型
        /// </summary>
        public string CacheName
        {
            get;
            set;
        }

        /// <summary>
        /// 过期时间(单位秒)
        /// </summary>
        public int TimeOut
        {
            get;
            set;
        }

        /// <summary>
        /// 缓存过期策略(1绝对,2相对)
        /// </summary>
        public int OverPolicy
        {
            get;
            set;
        }
    }
}
