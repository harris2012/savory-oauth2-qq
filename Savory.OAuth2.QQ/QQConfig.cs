namespace Savory.OAuth2.QQ
{
    /// <summary>
    /// 
    /// </summary>
    public class QQConfig
    {
        /// <summary>
        /// 申请QQ登录成功后，分配给网站的appid。
        /// </summary>
        public string APPID { get; set; }

        /// <summary>
        /// 申请QQ登录成功后，分配给网站的appkey。
        /// </summary>
        public string APPKey { get; set; }

        /// <summary>
        /// 编码之后的回调
        /// </summary>
        public string EncodedRedirectUri { get; set; }

        /// <summary>
        /// 请求用户授权时向用户显示的可进行授权的列表。
        /// 可填写的值是API文档中列出的接口，以及一些动作型的授权（目前仅有：do_like），如果要填写多个接口名称，请用逗号隔开。
        /// 例如：scope=get_user_info,list_album,upload_pic,do_like
        /// 不传则默认请求对接口get_user_info进行授权。
        /// 建议控制授权项的数量，只传入必要的接口名称，因为授权项越多，用户越可能拒绝进行任何授权。
        /// </summary>
        public string Scope { get; set; }

        /// <summary>
        /// 仅PC网站接入时使用。
        /// 用于展示的样式。不传则默认展示为PC下的样式。
        /// 如果传入“mobile”，则展示为mobile端下的样式。
        /// </summary>
        public string Display { get; set; }

        /// <summary>
        /// 仅WAP网站接入时使用。
        /// QQ登录页面版本（1：wml版本； 2：xhtml版本），默认值为1。
        /// </summary>
        public int GUT { get; set; }
    }
}