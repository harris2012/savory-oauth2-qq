using Savory.RpcClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Savory.OAuth2.QQ
{
    /// <summary>
    /// Step2：通过Authorization Code获取Access Token
    /// </summary>
    public class AccessTokenManager
    {
        /// <summary>
        /// PC网站，GET
        /// </summary>
        public static readonly string API_AccessToken_PC = "https://graph.qq.com/oauth2.0/token";
        /// <summary>
        /// WAP网站，GET
        /// </summary>
        public static readonly string API_AccessToken_WAP = "https://graph.z.qq.com/moc2/token";

        private readonly QQConfig qqConfig;

        private readonly IRpcGetClient rpcGetClient;

        public AccessTokenManager(QQConfig qqConfig, IRpcGetClient rpcGetClient)
        {
            this.qqConfig = qqConfig;
            this.rpcGetClient = rpcGetClient;
        }

        public AccessTokenResponse AccessToken(string code)
        {
            string host = GetHost(code);

            var response = rpcGetClient.GetString(host);
            if (string.IsNullOrEmpty(response))
            {
                return null;
            }

            var items = HttpUtility.ParseQueryString(response);
            if (items == null)
            {
                return null;
            }

            AccessTokenResponse returnValue = new AccessTokenResponse();
            if (items.AllKeys.Contains("access_token"))
            {
                returnValue.AccessToken = items["access_token"];
            }

            if (items.AllKeys.Contains("refresh_token"))
            {
                returnValue.RefreshToken = items["refresh_token"];
            }

            return returnValue;
        }

        private string GetHost(string code)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(API_AccessToken_PC).Append("?");

            // 授权类型，在本步骤中，此值为“authorization_code”。
            builder.Append("grant_type=authorization_code");

            builder.Append("&client_id=").Append(qqConfig.APPID);

            builder.Append("&client_secret=").Append(qqConfig.APPKey);

            // 上一步返回的authorization code。
            // 如果用户成功登录并授权，则会跳转到指定的回调地址，并在URL中带上Authorization Code。
            // 例如，回调地址为www.qq.com/my.php，则跳转到：http://www.qq.com/my.php?code=520DD95263C1CFEA087******
            // 注意此code会在10分钟内过期。
            builder.Append("&code=").Append(code);

            builder.Append("&redirect_uri=").Append(qqConfig.EncodedRedirectUri);

            return builder.ToString();
        }
    }

    /// <summary>
    /// 返回说明：
    /// 如果成功返回，即可在返回包中获取到Access Token。 
    /// </summary>
    public class AccessTokenResponse
    {
        /// <summary>
        /// 授权令牌，Access_Token。
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// 该access token的有效期，单位为秒。
        /// </summary>
        public int ExpireIn { get; set; }

        /// <summary>
        /// 在授权自动续期步骤中，获取新的Access_Token时需要提供的参数。
        /// </summary>
        public string RefreshToken { get; set; }
    }
}
