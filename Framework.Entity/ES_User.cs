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
    
    public partial class ES_User
    {
        public System.Guid ID { get; set; }
        public string sLoginAccout { get; set; }
        public string sPassWord { get; set; }
        public string sPhone { get; set; }
        public string sName { get; set; }
        public string sHeadPicture { get; set; }
        public string sRoleId { get; set; }
        public System.DateTime dInsertTime { get; set; }
        public Nullable<System.DateTime> dLastLoginTime { get; set; }
        public int iState { get; set; }
        public bool bIsDeleted { get; set; }
        public string sNick { get; set; }
        public string sOpenId { get; set; }
        public bool bIsSuper { get; set; }
    }
}
