using Framework.API.Model;
using Framework.DB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace Framework.API
{
    public class APIController
    {
        private readonly string apiURL;
        private readonly string apiType;
        private readonly string apiKey;

        private Common common;
        private MySqlParamCollection paramList = new MySqlParamCollection();

        public APIController(string apiKey, string apiType, string apiURL, string dbConnectionString, int dbTimeOut)
        {
            this.apiKey = apiKey;
            this.apiType = apiType;
            this.apiURL = apiURL;

            common = new Common(dbConnectionString, dbTimeOut);
        }

        public async Task<string> UpdateAllSvar()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {

                    // Bad Request 방지
                    client.DefaultRequestHeaders.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

                    string targetURL = $"{ apiURL }/restinfo/hiwaySvarInfoList?key={ apiKey }&type={ apiType }";
                    HttpResponseMessage res = await client.GetAsync(targetURL);

                    // API Call Fail
                    if (!res.IsSuccessStatusCode)
                    {
                        return $"CALL API hiwaySvarInfoList RESULT: Fail({ res.StatusCode }, { res.ReasonPhrase })";
                    }

                    // Json Parsing
                    APIHighwaySvarInfoListModel model = JsonConvert.DeserializeObject<APIHighwaySvarInfoListModel>(await res.Content.ReadAsStringAsync());
                    APIHighwaySvarInfoListItems param = new APIHighwaySvarInfoListItems();

                    List<List<string>> hdqrList = new List<List<string>>();
                    List<List<string>> mtnofList = new List<List<string>>();
                    List<List<string>> routeList = new List<List<string>>();
                    List<List<string>> gudClssList = new List<List<string>>();

                    foreach (var items in model.list)
                    {
                        param.svarCd         += items.svarCd         + "*";
                        param.svarNm         += items.svarNm         + "*";
                        param.hdqrCd         += items.hdqrCd         + "*";
                        param.mtnofCd        += items.mtnofCd        + "*";
                        param.pstnoCd        += items.pstnoCd        + "*";
                        param.routeCd        += items.routeCd        + "*";
                        param.svarAddr       += items.svarAddr       + "*";
                        param.gudClssCd      += items.gudClssCd      + "*";
                        param.rprsTelNo      += items.rprsTelNo      + "*";
                        param.dspnPrkgTrcn   += items.dspnPrkgTrcn   + "*";
                        param.cocrPrkgTrcn   += items.cocrPrkgTrcn   + "*";
                        param.fscarPrkgTrcn  += items.fscarPrkgTrcn  + "*";
                        param.svarGsstClssCd += items.svarGsstClssCd + "*";

                        hdqrList.Add(new List<string> { items.hdqrCd, items.hdqrNm });
                        mtnofList.Add(new List<string> { items.mtnofCd, items.mtnofNm });
                        routeList.Add(new List<string> { items.routeCd, items.routeNm });
                        gudClssList.Add(new List<string> { items.gudClssCd, items.gudClssNm });
                    }


                    // param 
                    try
                    {
                        paramList.Clear();
                        paramList.Add("svarCd",         param.svarCd        );
                        paramList.Add("svarNm",         param.svarNm        );
                        paramList.Add("hdqrCd",         param.hdqrCd        );
                        paramList.Add("mtnofCd",        param.mtnofCd       );
                        paramList.Add("pstnoCd",        param.pstnoCd       );
                        paramList.Add("routeCd",        param.routeCd       );
                        paramList.Add("svarAddr",       param.svarAddr      );
                        paramList.Add("gudClssCd",      param.gudClssCd     );
                        paramList.Add("rprsTelNo",      param.rprsTelNo     );
                        paramList.Add("dspnPrkgTrcn",   param.dspnPrkgTrcn  );
                        paramList.Add("cocrPrkgTrcn",   param.cocrPrkgTrcn  );
                        paramList.Add("fscarPrkgTrcn",  param.fscarPrkgTrcn );
                        paramList.Add("svarGsstClssCd", param.svarGsstClssCd);

                        // DataSet ds = common.MdlList(paramList, "SET_RESTAREA");
                    } 
                    catch (Exception dbEx)
                    {
                        return $"CALL DB Procedure SET_RESTAREA, SET_RESTAREA_DEPENDENCY RESULT: Exception({ dbEx.Message } - { dbEx.InnerException })";
                    }

                }
            }
            catch (Exception ex)
            {
                return $"CALL API UpdateAllSvar RESULT: Exception({ ex.Message } - { ex.InnerException })";
            }
            
            return $"SUCCESS";
        }

        public async Task<string> UpdateAllSvarFood()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {

                    // Bad Request 방지
                    client.DefaultRequestHeaders.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

                    string targetURL = $"{ apiURL }/restinfo/restBestfoodList?key={ apiKey }&type={ apiType }&numOfRows=1000";
                    HttpResponseMessage res = await client.GetAsync(targetURL);



                    // API Call Fail
                    if (!res.IsSuccessStatusCode)
                    {
                        return $"CALL API hiwaySvarInfoList RESULT: Fail({ res.StatusCode } - { res.ReasonPhrase })";
                    }

                    Debug.WriteLine(await res.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
                return $"CALL API UpdateAllSvar RESULT: Exception({ ex.Message } - { ex.InnerException })";
            }

            return $"SUCCESS";
        }

        private string[] RemoveDuplicateRows(List<List<string>> list)
        {
            HashSet<string> seenRows = new HashSet<string>();
            string cdVal = string.Empty, nmVal = string.Empty;

            foreach (var row in list)
            {
                string rowString = string.Join(",", row);

                if (!seenRows.Contains(rowString))
                {
                    seenRows.Add(rowString);
                    cdVal += row[0] + "*";
                    nmVal += row[1] + "*";
                }
            }

            return new string[] { cdVal, nmVal };
        }

    }
}











using System.Collections.Generic;

namespace Framework.API.Model
{
    public class APIHighwaySvarInfoListModel
    {
        public int count { get; set; }
        public List<APIHighwaySvarInfoListItems> list { get; set; }
    }

    public class APIHighwaySvarInfoListItems
    {
        public string svarCd { get; set; } = string.Empty;

        public string svarNm { get; set; } = string.Empty;
        
        public string routeCd { get; set; } = string.Empty;

        public string routeNm { get; set; } = string.Empty;

        public string hdqrCd { get; set; } = string.Empty;

        public string hdqrNm { get; set; } = string.Empty;

        public string mtnofCd { get; set; } = string.Empty;

        public string mtnofNm { get; set; } = string.Empty;

        public string gudClssCd { get; set; } = string.Empty;

        public string gudClssNm { get; set; } = string.Empty;

        public string pstnoCd { get; set; } = string.Empty;


        public string svarAddr { get; set; } = string.Empty;

        public string rprsTelNo { get; set; } = string.Empty;

        public string dspnPrkgTrcn { get; set; } = string.Empty;

        public string cocrPrkgTrcn { get; set; } = string.Empty;

        public string fscarPrkgTrcn { get; set; } = string.Empty;

        public string svarGsstClssCd { get; set; } = string.Empty;

    }

}
