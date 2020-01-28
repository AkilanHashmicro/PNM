using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SalesApp.OdooRpc;
using static SalesApp.models.CRMModel;

namespace SalesApp.models
{
    class Controller
    {
        private static Controller instance = null;

        private static readonly object padlock = new object();
        private OdooRPC odooConnector;

        private Controller()
        {
        }

        public string[] getDatabases(string url)
        {
            try
            {
                OdooRPC con = OdooRPC.InstanceCreation(url);
                return con.getDatabases();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            String[] st = new string[0];
            return st;
        }

        public void login(String url, String database, String username, String password)
        {
            try
            {
                odooConnector = OdooRPC.InstanceCreation(url);
                App.userid = odooConnector.login(database, username, password);
                //object[] domain = new object[] { "id", "=", App.userid };
                //JArray userData = odooConnector.odooSearchReadCall<JArray>("res.users", domain, new string[] { "name", "email", "partner_id","groups_id","image_medium" }, false);

                //string partnerName="", partnerRole="", partnerImage="";
                //int partnerId=0;
                //foreach (JObject obj in userData)
                //{                        
                //    partnerId = obj["partner_id"][0].ToObject<int>();
                //    partnerName = obj["name"].ToString();
                //    partnerRole = "Manager";
                //    partnerImage = obj["image_medium"].ToString();                    
                //}
                //Settings.PrefKeyIsLocked = "True";
                //UserAccount data = new UserAccount(url, database, App.userid,partnerId,partnerName,password,partnerImage,partnerRole);                
                //Settings.PrefKeyUserDetails = JsonConvert.SerializeObject(data);                

            }
            catch (Exception ea)
            {
                throw;
            }
        }


        public String SaleOrderConfirm(String modelname, string methodname, int saleorderid)
        {
            String res = odooConnector.odooMethodSaleOrderConfirm(modelname,methodname,saleorderid);

            return res;
        }

        public List<CRMLead> crmLeadData()
        {
            String[] colourcodes = new String[] { "#3498db", "#e67e22", "#c0392b", "#2ecc71", "#d35400", "#27ae60", " #e74c3c", "#2980b9" };
            try
            {

                //  JObject res = odooConnector.odooCrmLeadDataCall("sale.crm", "get_your_pipelines_all_orders");

                App.filterdict["range"] = "False";
                App.filterdict["days"] = "False";
                App.filterdict["month"] = "True";

                JObject res = odooConnector.odooFilterDataCall("sale.crm", "get_your_pipelines_all_orders");

                //   List<OrderLine> test = new List<OrderLine>();

                App.productList = res["Products"].ToObject<List<ProductsList>>();
                App.taxList = res["taxes"].ToObject<List<taxes>>();
                App.paytermList = res["payment_terms"].ToObject<List<paytermList>>();

                App.nextActivityList = res["next_activity"].ToObject<List<next_activity>>();

                App.crmList = res["crm_leads"].ToObject<List<CRMLead>>();

                App.crmOpprList = res["crm_quotations"].ToObject<List<CRMOpportunities>>();

                App.salesQuotList = res["sale_quotations"].ToObject<List<SalesQuotation>>();

                App.salesOrderList = res["sale_orders"].ToObject<List<SalesOrder>>();

                App.stageList = res["stages"].ToObject<List<stages>>();

                String colorCodeData = "";

                int cnt = 0;
                foreach (stages stateObj in res["stages"].ToObject<List<stages>>())
                {
                    try
                    {
                        colorCodeData = colorCodeData + "," + stateObj.name + "^" + colourcodes[cnt];
                    }
                    catch
                    {
                        cnt = 0;
                        continue;
                    }
                    cnt++;
                }

                Settings.StageColourCode = colorCodeData;
                App.cusdict = res["Customers"].ToObject<Dictionary<int, string>>();

                App.reasondict = res["lost_reason"].ToObject<Dictionary<int, string>>();

                App.salesteam = res["sales_team"].ToObject<Dictionary<int, string>>();
                App.salespersons = res["sales_persons"].ToObject<Dictionary<int, string>>();

                App.crmleadtags = res["crm_lead_tags"].ToObject<Dictionary<int, string>>();

                App.statedict = res["state_list"].ToObject<Dictionary<int, string>>();
                App.countrydict = res["country_list"].ToObject<Dictionary<int, string>>();

                App.partner_name = res["user_name"].ToString();
                App.partner_image = res["user_image_medium"].ToString();
                App.partner_id = res["partner_id"].ToObject<int>();
                App.partner_email = res["user_email"].ToString();


                return App.crmList;
            }
            catch (Exception ea)
            {
                System.Diagnostics.Debug.WriteLine("::::: CRM Warning Message ::::  " + ea.Message);
                return App.crmList;
            }
        }


        public List<CRMLead> crmFilterData()
        {
            String[] colourcodes = new String[] { "#3498db", "#e67e22", "#c0392b", "#2ecc71", "#d35400", "#27ae60", " #e74c3c", "#2980b9" };
            try
            {

                JObject res = odooConnector.odooFilterDataCall("sale.crm", "get_your_pipelines_all_orders");

                //   List<OrderLine> test = new List<OrderLine>();

                App.productList = res["Products"].ToObject<List<ProductsList>>();
                App.taxList = res["taxes"].ToObject<List<taxes>>();
                App.paytermList = res["payment_terms"].ToObject<List<paytermList>>();

                App.nextActivityList = res["next_activity"].ToObject<List<next_activity>>();

                App.crmList = res["crm_leads"].ToObject<List<CRMLead>>();

                App.crmOpprList = res["crm_quotations"].ToObject<List<CRMOpportunities>>();

                App.salesQuotList = res["sale_quotations"].ToObject<List<SalesQuotation>>();

                App.salesOrderList = res["sale_orders"].ToObject<List<SalesOrder>>();

                App.stageList = res["stages"].ToObject<List<stages>>();

                String colorCodeData = "";

                int cnt = 0;
                foreach (stages stateObj in res["stages"].ToObject<List<stages>>())
                {
                    try
                    {
                        colorCodeData = colorCodeData + "," + stateObj.name + "^" + colourcodes[cnt];
                    }
                    catch
                    {
                        cnt = 0;
                        continue;
                    }
                    cnt++;
                }

                Settings.StageColourCode = colorCodeData;
                App.cusdict = res["Customers"].ToObject<Dictionary<int, string>>();

                App.reasondict = res["lost_reason"].ToObject<Dictionary<int, string>>();

                App.salesteam = res["sales_team"].ToObject<Dictionary<int, string>>();
                App.salespersons = res["sales_persons"].ToObject<Dictionary<int, string>>();

                App.partner_name = res["user_name"].ToString();
                App.partner_image = res["user_image_medium"].ToString();
                App.partner_id = res["partner_id"].ToObject<int>();
                App.partner_email = res["user_email"].ToString();


                return App.crmList;
            }
            catch (Exception ea)
            {
                System.Diagnostics.Debug.WriteLine("::::: CRM Warning Message ::::  " + ea.Message);
                return App.crmList;
            }
        }

        public List<SalesModel> salesOrderData(string getState)
        {
            List<SalesModel> quotationList = new List<SalesModel>();
            try
            {
                JObject jsonObject = JObject.Parse(Settings.PrefKeyUserDetails.ToString());
                int saleUserId = jsonObject["userId"].ToObject<int>();

                object[] domain = new object[2];
                domain[0] = new object[] { "user_id", "=", saleUserId };
                domain[1] = new object[] { "state", "=", getState };

                quotationList = odooConnector.odooSearchReadCall1<dynamic>("sale.order", domain, new string[] { "name", "partner_id", "date_order", "state" }, true);

                return quotationList;
            }
            catch (Exception ea)
            {
                System.Diagnostics.Debug.WriteLine("::::: CRM Warning Message ::::  " + ea.Message);
                return quotationList;
            }
        }


        //public List<CustomersModel> GetCustomerData()
        //{
        //    List<CustomersModel> data = new List<CustomersModel>();

        //    object[] domain = new object[] { };

        //    data = odooConnector.odooSearchReadCall3<dynamic>("res.partner", domain, new string[] { "name", "street", "email", "image_small", "city", "country_id" }, true);

        //    return data;
        //}

        public List<CustomersModel> GetCustomerData()
        {
            List<CustomersModel> data = new List<CustomersModel>();
            JArray dt = odooConnector.odooCustomerDataCall<dynamic>("res.partner", "get_customers_app");
            data = dt.ToObject<List<CustomersModel>>();
            return data;
        }

        public JObject GetCustomerCreationData()
        {
            JObject dt = odooConnector.odooCustomerDataCall<dynamic>("res.partner", "get_create_customers_data");
            return dt;
        }


        public List<all_events> GetCalenderData(int month, int year)
        {
            List<all_events> calresList = new List<all_events>();

            JObject result = odooConnector.odooMethodCall_cal<JObject>("calendar.event", "get_calendar_event_app", App.userid, month, year);

            calresList = result["all_events"].ToObject<List<all_events>>();

            App.tagsDict = result["all_tags"].ToObject<Dictionary<int, string>>();

            return calresList;

        }


        public List<all_events> GetMeetingsData(int partnerid)
        {
            List<all_events> meetingList = new List<all_events>();

            JObject result = odooConnector.odooMethodCall_meeting<JObject>("calendar.event", "get_customers_meetings", partnerid);

            meetingList = result["all_events"].ToObject<List<all_events>>();

            //App.tagsDict = result["all_tags"].ToObject<Dictionary<int, string>>();

            return meetingList;

        }

        public List<CRMOpportunities> GetOppssData(int partnerid)
        {
            List<CRMOpportunities> oppoList = new List<CRMOpportunities>();
            JArray result = odooConnector.odooMethodCall_meeting<JArray>("res.partner", "get_customers_oppurtunities", partnerid);
            oppoList = result.ToObject<List<CRMOpportunities>>();
            return oppoList;
        }


        public List<SalesOrder> GetSalesData(int partnerid)
        {
            List<SalesOrder> oppoList = new List<SalesOrder>();
            JArray result = odooConnector.odooMethodCall_meeting<JArray>("res.partner", "get_customers_quotation", partnerid);
            oppoList = result.ToObject<List<SalesOrder>>();
            return oppoList;
        }

        public List<SalesModel> salesOrderData1()
        {
            List<SalesModel> data = new List<SalesModel>();
            try
            {
                object[] domain = new object[] { };
                data = odooConnector.odooSearchReadCall1<dynamic>("sale.order", domain, new string[] { "name", "partner_id", "state", "date_order" }, false);

                return data;
            }
            catch (Exception ea)
            {
                System.Diagnostics.Debug.WriteLine("::::: CRM Warning Message ::::  " + ea.Message);
                return data;
            }

        }

        public string UpdateCRMOpporData(string modelName, string methodName, Dictionary<string, dynamic> vals)
        {
            string flag = odooConnector.odooUpdatecrmOppMeeting(modelName, methodName, vals);
            return flag;
        }

        public string UpdateGPSData(string modelName, string methodName, float latitude, float longitude, int id)
        {
            string flag = odooConnector.odooUpdateGpsData(modelName, methodName, latitude, longitude, id);
            return flag;
        }

        public string clearGPSData(string modelName, string methodName, int id)
        {
            string flag = odooConnector.clearGpsData(modelName, methodName, id);
            return flag;
        }


        public string UpdateLeadCreationData(string modelName, string methodName, Dictionary<string, dynamic> vals)
        {
            string flag = odooConnector.odooUpdatecrmLeadCreation(modelName, methodName, vals);
            return flag;
        }


        public string UpdateLeadCreationConvertData(string modelName, string methodName, int id, Dictionary<string, dynamic> vals)
        {
            string flag = odooConnector.odooConvertrmLeadCreation(modelName, methodName, id, vals);
            return flag;
        }

        public string UpdateCRMOpporData1(string modelName, string methodName, int updateId, Dictionary<string, dynamic> vals)
        {
            string flag = odooConnector.odooUpdatecrmOppMeeting1(modelName, methodName, updateId, vals);
            return flag;
        }

        public bool UpdateMarkWon(int modelId)
        {
            try
            {
                odooConnector.odooUpdateUserData("crm.lead", "action_set_won", modelId);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool UpdateLost(int modelId, int lostId)
        {
            try
            {
                odooConnector.odooLostData("sale.crm", "mark_lost_app", modelId, lostId);
            }
            catch
            {
                return false;
            }
            return true;
        }



        //public List<ActivitiesModel> getActivitiesData()
        //{
        //    List<ActivitiesModel> activitiesList = new List<ActivitiesModel>


        //    {
        //        new ActivitiesModel(1,"Nestle","Henry","01/01/2018"),
        //          new ActivitiesModel(1,"Health Building","Agrolait","02/01/2018"),
        //          new ActivitiesModel(1,"Sample","Sample","02/01/2018"),
        //          new ActivitiesModel(1,"Sample","Sample","02/01/2018"),
        //          new ActivitiesModel(1,"Sample","Sample","02/01/2018"),
        //          new ActivitiesModel(1,"Sample","Sample","02/01/2018"),
        //          new ActivitiesModel(1,"Sample","Sample","02/01/2018"),
        //          new ActivitiesModel(1,"Sample","Sample","02/01/2018"),

        //    };

        //    return activitiesList;
        //}

        public static Controller InstanceCreation()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new Controller();
                }
                return instance;
            }
        }
    }
}
