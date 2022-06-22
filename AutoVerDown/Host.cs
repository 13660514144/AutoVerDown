using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace HelperTools
{
    public class HostReqModel
    {
        //private readonly IHttpContextAccessor httpContextAccessor;
        public List<DomainUserKey> ListUserKEY;
        
        /*认证中心存储  USER在不同域的身份 TOKEN在域通用*/
        public class IdentyCollection
        {
            /*0无1有*/
            public int IsUser { get; set; }
            public int IsDomain { get; set; }
            public int IsToken { get; set; }
        }
        public class DomainUserKey
        {
            public string User { get; set; }
            public string UserCn { get; set; }
            public string UnitCode { get; set; }
            public string UnitCn { get; set; }
            public string Role { get; set; }
            public string UserType { get; set; }
            public string Token { get; set; }
            public string Domain { get; set; }
        }
        public class HostUrl
        {
            public string Host { get; set; }
            public string Ip { get; set; }
            public string Port { get; set; }
            public string Path { get; set; }
            public string Scheme { get; set; }
            public string Header { get; set; }
            public string Domain { get; set; }
        }

        public DomainUserKey GetUserKey(JObject Req)
        {
            DomainUserKey _DomainUserKey = new DomainUserKey()
            {
                User = Req["User"].ToString(),
                UserCn = Req["UserCn"].ToString(),
                UnitCode = Req["UnitCode"].ToString(),
                UnitCn = Req["UnitCn"].ToString(),
                Role = Req["Role"].ToString(),
                UserType = Req["UserType"].ToString(),
                Token = Req["Token"].ToString(),
                Domain = Req["Domain"].ToString()
            };
            return _DomainUserKey;
        }

        /// <summary>
        /// 用户登录中心
        /// </summary>
        /// <param name="Host"></param>
        /// <param name="Url"></param>
        /// <param name="JsonUser"></param>
        /// <returns></returns>
        public string IdentyLogin(string Host, string Url, string JsonUser)
        {
            string Result = string.Empty;
            Dictionary<string, dynamic> o = new Dictionary<string, dynamic>()
            {
                { "ApiPara",JsonConvert.DeserializeObject(JsonUser)}
            };
            HttpClientFactory Req = new HttpClientFactory
            {
                RequestMothed = "JSON",
                Url = $"http://{Host}{Url}",
                PostData = JsonConvert.SerializeObject(o)
            };
            Result = Req.SendHttpRequest();
            return Result;
        }
        
        /// <summary>
        /// HttpWebRequerst 模式
        /// </summary>
        /// <param name="requestURI"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public string SendHttpRequest(string requestURI, string json)
        {
            string Result = string.Empty;
            /*Dictionary<string, string> o = new Dictionary<string, string>()
                {
                    { "ApiPara",json}
                };*/
            HttpClientFactory Req = new HttpClientFactory
            {
                RequestMothed = "POST",
                Url = requestURI,
                PostData = json//JsonConvert.SerializeObject(o)
            };
            Result = Req.SendHttpRequest();
            return Result;
        }
        public async Task<string> SendAsyncHttpRequest(string requestURI, string json)
        {
            string Result = string.Empty;
      
            HttpClientFactory Req = new HttpClientFactory
            {
                RequestMothed = "POST",
                Url = requestURI,
                PostData = json
            };
            Result = await Req.SendAsyncHttpRequest();
            return Result;
        }
       

    }
}
