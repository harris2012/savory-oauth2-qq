using Savory.RpcClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Savory.OAuth2.QQ
{
    /// <summary>
    /// Step1：获取Authorization Code
    /// Url：http://wiki.connect.qq.com/%E4%BD%BF%E7%94%A8authorization_code%E8%8E%B7%E5%8F%96access_token
    /// </summary>
    public class QQJump
    {
        /// <summary>
        /// PC网站，GET
        /// </summary>
        public string API_Authorization_PC = "https://graph.qq.com/oauth2.0/authorize";

        private readonly IRpcGetClient rpcGetClient;
        private readonly QQConfig qqConfig;

        public QQJump(IRpcGetClient rpcGetClient, QQConfig qqConfig)
        {
            this.rpcGetClient = rpcGetClient;
            this.qqConfig = qqConfig;
        }

        /// <summary>
        /// Step1：获取Authorization Code
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string AuthorizationHref(string state)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(API_Authorization_PC).Append("?");

            // 授权类型，此值固定为“code”。
            builder.Append("response_type=code");

            // 申请QQ登录成功后，分配给应用的appid。
            builder.Append("&client_id=").Append(qqConfig.APPID);

            // 成功授权后的回调地址，必须是注册appid时填写的主域名下的地址，
            // 建议设置为网站首页或网站的用户中心。
            // 注意需要将url进行URLEncode。
            builder.Append("&redirect_uri=").Append(qqConfig.EncodedRedirectUri);

            // client端的状态值。用于第三方应用防止CSRF攻击，成功授权后回调时会原样带回。
            // 请务必严格按照流程检查用户与state参数状态的绑定。
            builder.Append("&state=").Append(state);

            // 请求用户授权时向用户显示的可进行授权的列表。
            // 可填写的值是API文档中列出的接口，以及一些动作型的授权（目前仅有：do_like），如果要填写多个接口名称，请用逗号隔开。
            // 例如：scope=get_user_info,list_album,upload_pic,do_like
            // 不传则默认请求对接口get_user_info进行授权。
            // 建议控制授权项的数量，只传入必要的接口名称，因为授权项越多，用户越可能拒绝进行任何授权。
            if (!string.IsNullOrEmpty(qqConfig.Scope))
            {
                builder.Append("&scope=").Append(qqConfig.Scope);
            }

            // 仅PC网站接入时使用。
            // 用于展示的样式。不传则默认展示为PC下的样式。
            // 如果传入“mobile”，则展示为mobile端下的样式。
            if (!string.IsNullOrEmpty(qqConfig.Display))
            {
                builder.Append("&display=").Append(qqConfig.Display);
            }

            // 仅WAP网站接入时使用。
            // QQ登录页面版本（1：wml版本； 2：xhtml版本），默认值为1。
            if (qqConfig.GUT > 0)
            {
                builder.Append("&g_ut=").Append(qqConfig.GUT);
            }

            return builder.ToString();
        }
    }
}
