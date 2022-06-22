using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;


namespace HelperTools
{
    public class Dispatch
    {
      
        public Dispatch()
        {
           
        }
        /// <summary>
        /// API请求通行权限
        /// </summary>
        /// <param name="CollerName"></param>
        /// <param name="Obj"></param>
        /// <returns></returns>
        public string ApiGetData(string CollerName, string JSON,string Uri,string Port)
        {
            string Url = $"http://{Uri}:{Port}/";
            string Result = string.Empty;
            try
            {
                HostReqModel HostReq = new HostReqModel();
                Result =  HostReq.SendHttpRequest($"{Url}{CollerName}", JSON);
            }
            catch (Exception ex)
            {
            
                dynamic JsonObj = new DynamicObj();
                JsonObj.code = "000";
                JsonObj.Flg = "0";
                JsonObj.Message = ex.Message.ToString();
                JsonObj.Data = new JArray();
                Result = JsonConvert.SerializeObject(JsonObj._values);
            }
            return Result;
        }        
    }
}
