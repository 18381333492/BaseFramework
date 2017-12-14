using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Fleck;
using Framework.Utility.Tools;

namespace Framework.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            WebSocket();//开启服务
        }


        /// <summary>
        /// 启动WebSocket服务
        /// </summary>
        private void WebSocket()
        {
            int iPort =Convert.ToInt32(ConfigHelper.GetAppSetting("WebSocketPort"));//获取端口号
            var allSockets = new List<IWebSocketConnection>();
            var server = new WebSocketServer(string.Format("ws://127.0.0.1:{0}", iPort));
            server.Start(socket =>
            {
                //连接时候执行
                socket.OnOpen = () =>
                {
                    allSockets.Add(socket);
                    socket.Send(JsonHelper.ToJsonString(new
                    {
                        sSocketId = socket.ConnectionInfo.Id
                    }));
                };

                //连接关闭时执行
                socket.OnClose = () =>
                {
                    allSockets.Remove(socket);
                };

                //服务段收到消息时执行
                socket.OnMessage = message =>
                {
                    var receive = JsonHelper.Deserialize<dynamic>(message);//获取收到的消息
                    string sSocketId = Convert.ToString(receive.sSocketId);
                    if (!string.IsNullOrEmpty(sSocketId))
                    {
                        var sendSocket = allSockets.FirstOrDefault(m =>Convert.ToString(m.ConnectionInfo.Id)== sSocketId);
                        if (sendSocket != null)
                        {
                            sendSocket.Send(JsonHelper.ToJsonString(new
                            {
                                data = receive.data
                            }));
                        }
                    }
                };
            });
        }
    }
}