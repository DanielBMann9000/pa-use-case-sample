using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PreEmptive.Analytics.Common;
using Xamarin.Forms;

namespace PASample
{
    public class ExpenseRequestPage : BasePage
    {
        private const string SERVICE_URL = "http://josh-2012r2-2.preemptive.internal/sample/api/Expense/Approve";
        private const string EXPENSE_FEATURE = "Expense Request";
    
        public ExpenseRequestPage()
        {
            this.Title = "Expense Request";

            Label headerSlider = new Label
            {
                Text = "Reason",
                Font = Font.SystemFontOfSize(NamedSize.Large, FontAttributes.Bold),
              

            };
            Xamarin.Forms.Picker pick = new Picker()
            {
                
                Title="Pick Reason",
                Items=
                {
                    "Client Dinner","Purchase Airfare","Prepay Hotel","Register for Conference"

                }
                
            };

            Xamarin.Forms.InputView ammount = new Entry()
            {
                Placeholder="Dollar Amount"

                
            };

            var btn = new Button
            {
                Text = "Submit"
            };
            btn.Clicked += (o, e) =>
                {
                    var strAmt=(string)ammount.GetValue(Entry.TextProperty);
                    decimal amt;
                    if (!decimal.TryParse(strAmt,System.Globalization.NumberStyles.Currency,System.Globalization.CultureInfo.CurrentCulture, out amt))
                    {
                        PAClientFactory.FeatureTick("Invalid Amount");

                        ammount.SetValue(Entry.TextProperty, string.Empty);
                        DisplayAlert("Invalid Amount", "You must enter a valid currency","Ok");
                        return;
                    }

                    goButon_Clicked(amt,pick.Items[pick.SelectedIndex]);

                };
         

            this.Content = new StackLayout
            {
                Children ={
                    headerSlider,
                   pick,
                   new Label
                   {
                       Text="Amount",
                       Font = Font.SystemFontOfSize(NamedSize.Large, FontAttributes.Bold),
                   },
                   ammount,
                   btn

               },
               
            };

     
        }

        public override string Feature
        {
            get
            {
                return "Expense Request Page";
            }
        }

        async void goButon_Clicked(decimal ammount,string reason)
        {
            try
            {
                var keys = new ExtendedKeys();
                keys.Add("Amount", ammount);
                keys.Add("Reason", reason);

                PAClientFactory.StartFeature(EXPENSE_FEATURE,keys);

                var handler = new HttpClientHandler();

                /* Needed to use proxy so I could pass data through fiddler to debug
                 
                handler.UseProxy = true;
                handler.Proxy = new Proxy(new Uri("http://192.168.1.101:8888"));
                */

                var client = new System.Net.Http.HttpClient(handler);
                var content = new System.Net.Http.StringContent(new {
                    Amount = ammount,
                    Reason=reason,
                    LicenseKey = App.LicenseKey,
                    Id=Guid.NewGuid(),
                    Department=App.Department

                
                }.ToJson());

                content.Headers.ContentType.MediaType = "text/json";


                System.Net.Http.HttpRequestMessage rm = new System.Net.Http.HttpRequestMessage(HttpMethod.Post, SERVICE_URL);
                rm.Content=content;
                rm.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("text/json"));
                
                var response = await client.SendAsync(rm);
                if (!response.IsSuccessStatusCode)
                {
                    DisplayAlert("Error", "Unknown Error Processing Request","");

                    PAClientFactory.StopFeature(EXPENSE_FEATURE);
                    return;

                }
                var respStream=await response.Content.ReadAsStreamAsync();
                Newtonsoft.Json.JsonSerializer js=new Newtonsoft.Json.JsonSerializer();
                var expResponse = (ExpenseApprovalResponse)js.Deserialize(new Newtonsoft.Json.JsonTextReader(new System.IO.StreamReader(respStream)), typeof(ExpenseApprovalResponse));
                
                //var str = System.Text.Encoding.UTF8.GetString(result, 0, result.Length);
                var stopKeys = new ExtendedKeys();
                var msg = string.Empty;
                if (expResponse.Exception == null || string.IsNullOrEmpty(expResponse.Exception.Message))
                {
                    stopKeys.Add("Result", "Sucess");
                    msg = "Your request was processed.";

                }
                else
                {
                    stopKeys.Add("Result", expResponse.Exception.Message);
                    msg="Your request was rejected because - " + expResponse.Exception.Message;
                }
                PAClientFactory.StopFeature("Expense Request",stopKeys);
                await DisplayAlert("Result", msg, "");
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", "Error", "Ok");
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }
        public override string ContextMeansText
        {
            get
            {
                return "TBD - Sebastian Define Me";
            }
        }


    }

    public class ExpenseApprovalResponse
    {
        public ExceptionModel Exception { get; set; }
    }
    public class ExceptionModel
    {
        public string Message { get; set; }
    }
    public class Proxy : System.Net.IWebProxy
    {
        public System.Net.ICredentials Credentials
        {
            get;
            set;
        }

        private readonly Uri _proxyUri;

        public Proxy(Uri proxyUri)
        {
            _proxyUri = proxyUri;
        }

        public Uri GetProxy(Uri destination)
        {
            return _proxyUri;
        }

        public bool IsBypassed(Uri host)
        {
            return false;
        }
    }
    public static class Extensions
    {
        public static string ToJson(this object obj)
        {

            Newtonsoft.Json.JsonSerializer jserial = new Newtonsoft.Json.JsonSerializer();
            var sb = new StringBuilder();
            Newtonsoft.Json.JsonTextWriter jw = new Newtonsoft.Json.JsonTextWriter(new System.IO.StringWriter(sb));
            jserial.Serialize(jw, obj);

            return sb.ToString();

            
        }
    }
}
