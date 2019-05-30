using Savory.RpcClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Savory.OAuth2.QQ
{
    /// <summary>
    /// Step4：获取用户信息
    /// 
    /// OpenAPI调用说明_OAuth2.0
    /// 前提说明
    /// 1. 该appid已经开通了该OpenAPI的使用权限。
    ///    从API列表的接口列表中可以看到，有的接口是完全开放的，有的接口则需要提前提交申请，以获取访问权限。
    /// 2. 准备访问的资源是用户授权可访问的。
    ///    网站调用该OpenAPI读写某个openid（用户）的信息时，必须是该用户已经对你的appid进行了该OpenAPI的授权
    ///    （例如用户已经设置了相册不对外公开，则网站是无法读取照片信息的）。
    ///    用户可以进入QQ空间->设置->授权管理进行访问权限的设置。
    /// 3. 已经成功获取到Access Token，并且Access Token在有效期内。
    /// 
    /// 调用OpenAPI接口
    /// 
    /// QQ登录提供了用户信息/动态同步/日志/相册/微博等OpenAPI（详见API列表），
    /// 网站需要将请求发送到某个具体的OpenAPI接口，以访问或修改用户数据。
    /// </summary>
    public class UserInfoManager
    {
        /// <summary>
        /// GET
        /// </summary>
        public string API_Get_User_Info = "https://graph.qq.com/user/get_user_info";

        private readonly IRpcGetClient rpcGetClient;

        private readonly QQConfig qqConfig;

        public UserInfoManager(IRpcGetClient rpcGetClient, QQConfig qqConfig)
        {
            this.rpcGetClient = rpcGetClient;
            this.qqConfig = qqConfig;
        }

        /// <param name="entity"></param>
        /// <returns></returns>
        public OpenIdGetUserInfoResponse OpenId_Get_User_Info(string accessToken, string openId)
        {
            var host = GetHost(accessToken, openId);

            var response = rpcGetClient.GetItem<OpenIdGetUserInfoResponse>(host);

            return response;
        }

        private string GetHost(string accessToken, string openId)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(API_Get_User_Info).Append("?");

            /// 可通过使用Authorization_Code获取Access_Token 或来获取。 
            /// access_token有3个月有效期。
            builder.Append("access_token=").Append(accessToken);

            // 申请QQ登录成功后，分配给应用的appid
            builder.Append("&oauth_consumer_key=").Append(qqConfig.APPID);

            /// 用户的ID，与QQ号码一一对应。 
            /// 可通过调用https://graph.qq.com/oauth2.0/me?access_token=YOUR_ACCESS_TOKEN 来获取。
            builder.Append("&openid=").Append(openId);

            return builder.ToString();
        }
    }



    /// <summary>
    /// 获取登录用户的昵称、头像、性别
    /// Url：http://wiki.connect.qq.com/get_user_info
    /// </summary>
    public class OpenIdGetUserInfoResponse
    {
        /// <summary>
        /// 返回码
        /// </summary>
        public int ret { get; set; }
        /// <summary>
        /// 如果ret 小于 0，会有相应的错误信息提示，返回数据全部用UTF-8编码。
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 用户在QQ空间的昵称。
        /// </summary>
        public string nickname { get; set; }
        /// <summary>
        /// 大小为30×30像素的QQ空间头像URL。
        /// </summary>
        public string figureurl { get; set; }
        /// <summary>
        /// 大小为50×50像素的QQ空间头像URL。
        /// </summary>
        public string figureurl_1 { get; set; }
        /// <summary>
        /// 大小为100×100像素的QQ空间头像URL。
        /// </summary>
        public string figureurl_2 { get; set; }
        /// <summary>
        /// 大小为40×40像素的QQ头像URL。
        /// </summary>
        public string figureurl_qq_1 { get; set; }
        /// <summary>
        /// 大小为100×100像素的QQ头像URL。需要注意，不是所有的用户都拥有QQ的100x100的头像，但40x40像素则是一定会有。
        /// </summary>
        public string figureurl_qq_2 { get; set; }
        /// <summary>
        /// 性别。 如果获取不到则默认返回"男"
        /// </summary>
        public string gender { get; set; }
        /// <summary>
        /// 标识用户是否为黄钻用户（0：不是；1：是）。
        /// </summary>
        public string is_yellow_vip { get; set; }
        /// <summary>
        /// 标识用户是否为黄钻用户（0：不是；1：是）
        /// </summary>
        public string vip { get; set; }
        /// <summary>
        /// 黄钻等级
        /// </summary>
        public string yellow_vip_level { get; set; }
        /// <summary>
        /// 黄钻等级
        /// </summary>
        public string level { get; set; }
        /// <summary>
        /// 标识是否为年费黄钻用户（0：不是； 1：是）
        /// </summary>
        public string is_yellow_year_vip { get; set; }
    }
}
