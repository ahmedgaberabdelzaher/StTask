using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace StTask.Helper
{
    public static class HttpManage
    {
        public static async Task<Tuple<T, bool, string>> GetAsync<T>(string requestUrl) where T : class
        {
            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    var client = new System.Net.Http.HttpClient();
                    var response = await client.GetAsync(requestUrl);
                    if (response != null)
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            try
                            {
                                var responseJson = await response.Content.ReadAsStringAsync();
                                var JsonObject = JsonConvert.DeserializeObject<T>(responseJson);
                                return Tuple.Create(JsonObject, true, "");

                            }
                            catch (Exception ex)
                            { throw ex; }




                        }
                        else
                        {

                            return Tuple.Create((T)Activator.CreateInstance(typeof(T)), false, "Sever Error");
                        }
                    }
                    else
                    {
                        return Tuple.Create((T)Activator.CreateInstance(typeof(T)), false, "Error Occured");
                    }

                }
                else
                {
                    return Tuple.Create((T)Activator.CreateInstance(typeof(T)), false, "No Internet Connection");
                }

            }
            catch (System.Exception exp)
            {
                return Tuple.Create((T)Activator.CreateInstance(typeof(T)), false, exp.Message);
            }

        }
    }
}
