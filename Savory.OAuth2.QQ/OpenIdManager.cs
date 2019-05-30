using Savory.RpcClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Savory.OAuth2.QQ
{
    /// <summary>
    /// Step3：在Step1中获取到的access token。
    /// Url：http://wiki.connect.qq.com/%E8%8E%B7%E5%8F%96%E7%94%A8%E6%88%B7openid_oauth2-0
    /// </summary>
    public class OpenIdManager
    {
        /// <summary>
        /// PC GET
        /// </summary>
        public string API_OpenID_PC = "https://graph.qq.com/oauth2.0/me";

        /// <summary>
        /// WAP GET
        /// </summary>
        public string API_OpenID_WAP = "https://graph.z.qq.com/moc2/me";

        private readonly IRpcGetClient rpcGetClient;

        public OpenIdManager(IRpcGetClient rpcGetClient)
        {
            this.rpcGetClient = rpcGetClient;
        }

        /// <summary>
        /// Step3：获取用户OpenId
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public OpenIdResponse OpenId(string accessToken)
        {
            var host = GetHost(accessToken);

            //callback( {"client_id":"xx12196xx","openid":"09196B48CA96A8C8ED4FFxxCBxx59Dxx"} );
            var response = rpcGetClient.GetString(host);
            if (string.IsNullOrEmpty(response))
            {
                return null;
            }

            var regex = new Regex("callback\\( \\{\"client_id\":\"(\\d+)\",\"openid\":\"([A-Z0-9]+)\"\\} \\);\n");
            var match = regex.Match(response);
            if (!match.Success)
            {
                return null;
            }

            //var clientId = match.Groups[1].Value;
            var openId = match.Groups[2].Value;

            var returnValue = new OpenIdResponse();
            returnValue.ClientId = match.Groups[1].Value;
            returnValue.OpenId = match.Groups[2].Value;

            return returnValue;
        }

        private string GetHost(string accessToken)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(API_OpenID_PC).Append("?");

            builder.Append("access_token=").Append(accessToken);

            return builder.ToString();
        }

    }

    public class OpenIdResponse
    {
        /// <summary>
        /// client id
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// openid是此网站上唯一对应用户身份的标识，
        /// 网站可将此ID进行存储便于用户下次登录时辨识其身份，
        /// 或将其与用户在网站上的原有账号进行绑定。
        /// </summary>
        public string OpenId { get; set; }
    }
}
