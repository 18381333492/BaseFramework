//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Framework.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class ES_Client
    {
        public System.Guid ID { get; set; }
        public string sOpenId { get; set; }
        public string sNickName { get; set; }
        public string sHeadPicture { get; set; }
        public int iIntegral { get; set; }
        public int iState { get; set; }
        public int iIsSubscribe { get; set; }
        public System.DateTime dSubscribeTime { get; set; }
        public Nullable<System.DateTime> dUnSubscribeTime { get; set; }
        public Nullable<int> iSex { get; set; }
        public string sProvince { get; set; }
        public string sCountry { get; set; }
        public string sCity { get; set; }
    }
}
