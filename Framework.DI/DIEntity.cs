using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraceLogs;

namespace Framework.DI
{
    public class DIEntity
    {
        /// <summary>
        /// 注入配置对象如果你要用这个配置，就
        /// 表示对象的配置信息都是从web.config上获取
        /// </summary>
        private static UnityConfigurationSection configuration;

        /// <summary>
        /// 注入容器
        /// </summary>
        private static UnityContainer container;

        /// <summary>
        /// 日志记录
        /// </summary>
        private static ILogger logger;

        /// <summary>
        /// 接口的缓存容器
        /// </summary>
        private static List<CacheItem> cacheContainer;

        /// <summary>
        /// 当前对象的实例
        /// </summary>
        private static DIEntity instance;

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static DIEntity()
        {
            configuration=ConfigurationManager.GetSection(UnityConfigurationSection.SectionName) as UnityConfigurationSection;

            //注入容器实例化
            container = new UnityContainer();

            //配置的文件注入容器当中
            configuration.Configure(container);

            //日志接口的实例化
            logger = LoggerManager.Instance.GetSLogger("DIEntity");

            //缓存容器的实例化
            cacheContainer = new List<CacheItem>();
        }

       
        /// <summary>
        /// 获取实例
        /// </summary>
        /// <returns></returns>
        public static DIEntity Instance
        {
            get
            {
                if (instance == null)
                    instance = new DIEntity();
                return instance;
            }
        }
 
        /// <summary>
        /// 获取接口的实现对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetImpl<T>()
        {
            try
            {
                var obj = cacheContainer.FirstOrDefault(m => m.Name == typeof(T).Name);
                if (obj != null)
                    return (T)obj.Item;
                else
                {
                    var Item= container.Resolve<T>();
                    cacheContainer.Add(new CacheItem()
                    {
                        Name = typeof(T).Name,
                        Item = Item
                    });
                    return Item;
                }
            }
            catch (Exception ex)
            {
                logger.Info(ex.Message);
                logger.Fatal(ex);
                return default(T);
            }
        }
    }
}
