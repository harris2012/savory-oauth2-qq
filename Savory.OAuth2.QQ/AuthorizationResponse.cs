namespace Savory.OAuth2.QQ
{
    /// <summary>
    /// 返回说明：
    /// 1. 如果用户成功登录并授权，则会跳转到指定的回调地址，并在redirect_uri地址后带上Authorization Code和原始的state值。
    ///    注意：此code会在10分钟内过期。
    /// 2. 如果用户在登录授权过程中取消登录流程，对于PC网站，登录页面直接关闭；
    ///    对于WAP网站，同样跳转回指定的回调地址，并在redirect_uri地址后带上usercancel参数和原始的state值，
    ///    其中usercancel值为非零
    /// </summary>
    public class AuthorizationResponse
    {

        /// <summary>
        /// 授权码
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string state { get; set; }
    }
}