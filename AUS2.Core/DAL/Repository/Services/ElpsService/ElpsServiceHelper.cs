using AUS2.Core.DAL.IRepository;
using AUS2.Core.Helper.SerilogService.Account;
using AUS2.Core.Utilities;
using AUS2.Core.ViewModels;
using AUS2.Core.ViewModels.Account;
using AUS2.Core.ViewModels.AdminModel;
using AUS2.Core.ViewModels.CompanyModel;
using AUS2.Core.ViewModels.Dto.Response;
using AUS2.Core.ViewModels.Payment;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography;

namespace AUS2.Core.DAL.Repository.Services.ElpsService
{
    public class ElpsServiceHelper: IElpsService
    {
        private readonly AppSettings _appSettings;
        WebApiResponse webApiResponse = new WebApiResponse();
        private readonly GeneralLogger _generalLogger;
        private readonly string directory = "Nelps";
        public ElpsServiceHelper(IOptions<AppSettings> appSettings, GeneralLogger generalLogger)
        {
            _appSettings = appSettings.Value;
            _generalLogger = generalLogger ?? throw new ArgumentNullException(nameof(generalLogger));
        }

        public WebApiResponse GetCompanyDetailByEmail(string email)
        {
            try { 
            var encrpt = $"{_appSettings.AppEmail}{ _appSettings.SecreteKey}";
            var apiHash = MyUtils.GenerateSha512(encrpt);
            _generalLogger.LogRequest($"{"About to GetCompanyDetail On Elps with Email => "}{" "}{email}{" - "}{DateTime.Now}", false, directory);
             var request = new RestRequest("api/company/{compemail}/{email}/{apiHash}", Method.GET);
            request.AddUrlSegment("compemail", email);
            request.AddUrlSegment("email", _appSettings.AppEmail);
            request.AddUrlSegment("apiHash", apiHash);

            var client = new RestClient(_appSettings.ElpsUrl);
            IRestResponse response = client.Execute(request);
            _generalLogger.LogRequest($"{"Response Exception =>" + response.ErrorException + "\r\nResponse Status =>" + response.ResponseStatus + "\r\nStatus Code =>" + response.StatusCode}{" - "}{DateTime.Now}", false, directory);

            if (response.ErrorException != null)
            {
                webApiResponse.Message = response.ErrorMessage;
            }
            else if (response.ResponseStatus != ResponseStatus.Completed)
            {
                webApiResponse.Message = response.ResponseStatus.ToString();
            }
            else if (response.StatusCode != HttpStatusCode.OK)
            {
                webApiResponse.Message = response.StatusCode.ToString();
            }
            else
            {
                webApiResponse.Message = "SUCCESS";
                webApiResponse.Data = JsonConvert.DeserializeObject<CompanyDetail>(response.Content);
            }
            }
            catch (Exception ex)
            {
                _generalLogger.LogRequest($"{"Last Exception =>" + ex.Message}{" - "}{DateTime.Now}", true, directory);

                webApiResponse.Message = ex.Message;
            }
            finally
            {
                _generalLogger.LogRequest($"{"About to Return with Message => " + webApiResponse.Message}{" - "}{DateTime.Now}", false, directory);

            }
            return webApiResponse;
        }

        public WebApiResponse GetStaff(string email)
        {
            try
            {
                var encrpt = $"{_appSettings.AppEmail}{ _appSettings.SecreteKey}";
                var apiHash = MyUtils.GenerateSha512(encrpt); 
                var request = new RestRequest($"/api/Accounts/Staff/{email}/{_appSettings.AppEmail}/{apiHash}", Method.GET);
                ;

                var client = new RestClient(_appSettings.ElpsUrl);
                _generalLogger.LogRequest($"{"About to GetCompanyDetail On Elps with Email => "}{" "}{" - "}{DateTime.Now}", false, directory);
                IRestResponse response = client.Execute(request);
                _generalLogger.LogRequest($"{"Response Exception =>" + response.ErrorException + "\r\nResponse Status =>" + response.ResponseStatus + "\r\nStatus Code =>" + response.StatusCode}{" "}{" - "}{DateTime.Now}", false, directory);
                if (response.ErrorException != null)
                {
                    webApiResponse.Message = response.ErrorMessage;
                }

                else if (response.ResponseStatus != ResponseStatus.Completed)
                {
                    webApiResponse.Message = response.ResponseStatus.ToString();
                }

                else if (response.StatusCode != HttpStatusCode.OK)
                {
                    webApiResponse.Message = response.StatusCode.ToString();
                }
                else
                {
                    webApiResponse.Data = JsonConvert.DeserializeObject<StaffResponseDto>(response.Content);
                    webApiResponse.Message = "SUCCESS";
                }
            }
            catch (Exception ex)
            {
                _generalLogger.LogRequest($"{"Last Exception =>" + ex.ToString()}{" - "}{DateTime.Now}", true, directory);
                webApiResponse.Message = ex.Message;
            }
            finally
            {
                _generalLogger.LogRequest($"{"About to Return with Message => " + webApiResponse.Message}{" - "}{DateTime.Now}", true, directory);

            }

            return webApiResponse;
        } 
        
        public WebApiResponse GetAllStaff()
        {
            try
            {
                var encrpt = $"{_appSettings.AppEmail}{ _appSettings.SecreteKey}";
                var apiHash = MyUtils.GenerateSha512(encrpt); 
                var request = new RestRequest("api/Accounts/Staff/{email}/{apiHash}", Method.GET);
                request.AddUrlSegment("email", _appSettings.AppEmail);
                request.AddUrlSegment("apiHash", apiHash);

                var client = new RestClient(_appSettings.ElpsUrl);
                _generalLogger.LogRequest($"{"About to GetCompanyDetail On Elps with Email => "}{" "}{" - "}{DateTime.Now}", false, directory);
                IRestResponse response = client.Execute(request);
                _generalLogger.LogRequest($"{"Response Exception =>" + response.ErrorException + "\r\nResponse Status =>" + response.ResponseStatus + "\r\nStatus Code =>" + response.StatusCode}{" "}{" - "}{DateTime.Now}", false, directory);
                if (response.ErrorException != null)
                {
                    webApiResponse.Message = response.ErrorMessage;
                }

                else if (response.ResponseStatus != ResponseStatus.Completed)
                {
                    webApiResponse.Message = response.ResponseStatus.ToString();
                }

                else if (response.StatusCode != HttpStatusCode.OK)
                {
                    webApiResponse.Message = response.StatusCode.ToString();
                }
                else
                {
                    webApiResponse.Data = JsonConvert.DeserializeObject<List<StaffResponseDto>>(response.Content);
                    webApiResponse.Message = "SUCCESS";
                }
            }
            catch (Exception ex)
            {
                _generalLogger.LogRequest($"{"Last Exception =>" + ex.ToString()}{" - "}{DateTime.Now}", true, directory);
                webApiResponse.Message = ex.Message;
            }
            finally
            {
                _generalLogger.LogRequest($"{"About to Return with Message => " + webApiResponse.Message}{" - "}{DateTime.Now}", true, directory);

            }

            return webApiResponse;
        }

        public ElpsResponse GetElpAppUpdateStatus(string Appid, string ElpsID, string Status)
        {
            ElpsResponse elspResponse = new ElpsResponse();
            try
            {
                var encrpt = $"{_appSettings.AppEmail}{ _appSettings.SecreteKey}";
                var apiHash = MyUtils.GenerateSha512(encrpt);
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                var request = new RestRequest("/api/Application/ByOrderId/{orderId}/{email}/{apiHash}", Method.GET);
                request.AddUrlSegment("orderId", Appid);
                request.AddUrlSegment("email", _appSettings.AppEmail);
                request.AddUrlSegment("apiHash", apiHash);

                var client = new RestClient(_appSettings.ElpsUrl);
                IRestResponse response = client.Execute(request);

                var resp = JsonConvert.DeserializeObject<JObject>(response.Content);

                var values = new JObject();
                values.Add("orderId", Appid);
                values.Add("company_Id", Convert.ToInt32(ElpsID));
                values.Add("status", Status);
                values.Add("date", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fff"));
                values.Add("categoryName", (string)resp.SelectToken("categoryName"));
                values.Add("licenseName", (string)resp.SelectToken("licenseName"));
                values.Add("licenseId", (string)resp.SelectToken("licenseId"));
                values.Add("id", (string)resp.SelectToken("id"));



                request = new RestRequest("api/Application/{email}/{apiHash}", Method.PUT);
                request.AddParameter("application/json; charset=utf-8", JsonConvert.SerializeObject(values), ParameterType.RequestBody);
                request.RequestFormat = DataFormat.Json;
                request.AddUrlSegment("email", _appSettings.AppEmail);
                request.AddUrlSegment("apiHash", apiHash);
                response = client.Execute(request);


                if (response.ErrorException != null)
                {
                    elspResponse.message = response.ErrorMessage;
                }

                else if (response.ResponseStatus != ResponseStatus.Completed)
                {
                    elspResponse.message = response.ResponseStatus.ToString();
                }

                else if (response.StatusCode != HttpStatusCode.OK)
                {
                    elspResponse.message = response.StatusCode.ToString();
                }
                else
                {
                    elspResponse.value = JsonConvert.DeserializeObject<JObject>(response.Content);
                    elspResponse.message = "SUCCESS";
                }
            }
            catch (Exception ex)
            {
                _generalLogger.LogRequest($"{"GetElpAppUpdateStatus-- Last Exception =>" + ex.Message}{" "}{" - "}{DateTime.Now}", false, directory);

                elspResponse.message = ex.Message;
            }
            finally
            {
                _generalLogger.LogRequest($"{"GetElpAppUpdateStatus-- About to Return with Message =>" + elspResponse.message}{" "}{" - "}{DateTime.Now}", false, directory);

            }

            return elspResponse;
        }

        public WebApiResponse UpdateCompanyDetails(CompanyModel model, string email, bool update)
        {
            try
            {
                var encrpt = $"{_appSettings.AppEmail}{ _appSettings.SecreteKey}";
                var apiHash = MyUtils.GenerateSha512(encrpt);
                _generalLogger.LogRequest($"{"About to Update Company details On Elps => "}{" "}{JsonConvert.SerializeObject(model)}{" - "}{DateTime.Now}", false, directory);

                var request = new RestRequest("/api/Company/Edit/{email}/{apiHash}", Method.PUT);
                request.AddJsonBody(new
                {
                    company = model,
                    companyMedicals = (string)null,
                    companyExpatriateQuotas = (string)null,
                    companyNsitfs = (string)null,
                    companyProffessionals = (string)null,
                    companyTechnicalAgreements = (string)null
                });
                request.AddUrlSegment("email", _appSettings.AppEmail);
                request.AddUrlSegment("apiHash", apiHash);

                var client = new RestClient(_appSettings.ElpsUrl);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode == HttpStatusCode.OK && !string.IsNullOrEmpty(response.Content))
                {
                    var company = JsonConvert.DeserializeObject<CompanyModel>(JsonConvert.SerializeObject(model));
                    if (company != null && !string.IsNullOrEmpty(email))
                    {
                        var res = UpdateCompanyNameEmail(new
                        {
                            Name = company.name,
                            RC_Number = company.rC_Number,
                            Business_Type = company.business_Type,
                            emailChange = true,
                            CompanyId = company.id,
                            NewEmail = email
                        });
                    }
                }
                _generalLogger.LogRequest($"{"Response Exception =>" + response.ErrorException + "\r\nResponse Status =>" + response.ResponseStatus + "\r\nStatus Code =>" + response.StatusCode}{" - "}{DateTime.Now}", false, directory);

                if (response.ErrorException != null)
                {
                    webApiResponse.Message = response.ErrorMessage;
                }
                else if (response.ResponseStatus != ResponseStatus.Completed)
                {
                    webApiResponse.Message = response.ResponseStatus.ToString();
                }
                else if (response.StatusCode != HttpStatusCode.OK)
                {
                    webApiResponse.Message = response.StatusCode.ToString();
                }
                else
                {
                    webApiResponse.Message = "SUCCESS";
                    webApiResponse.Data = JsonConvert.DeserializeObject<CompanyModel>(response.Content);
                }
            }
            catch (Exception ex)
            {
                _generalLogger.LogRequest($"{"Last Exception =>" + ex.Message}{" - "}{DateTime.Now}", true, directory);

                webApiResponse.Message = ex.Message;
            }
            finally
            {
                _generalLogger.LogRequest($"{"About to Return with Message => " + webApiResponse.Message}{" - "}{DateTime.Now}", false, directory);

            }
            return webApiResponse;
        }

        public string UpdateCompanyNameEmail(object model)
        {
            try
            {
                var encrpt = $"{_appSettings.AppEmail}{ _appSettings.SecreteKey}";
                var apiHash = MyUtils.GenerateSha512(encrpt);
                _generalLogger.LogRequest($"{"About to Update Company name and email On Elps => "}{" "}{JsonConvert.SerializeObject(model)}{" - "}{DateTime.Now}", false, directory);

                var request = new RestRequest("/api/Accounts/ChangeEmail/{email}/{apiHash}", Method.POST);
                request.AddJsonBody(model);
                request.AddUrlSegment("email", _appSettings.AppEmail);
                request.AddUrlSegment("apiHash", apiHash);

                var client = new RestClient(_appSettings.ElpsUrl);
                IRestResponse response = client.Execute(request);

                if (response.StatusCode == HttpStatusCode.OK)
                    return response.Content;

            }
            catch (Exception ex)
            {
                _generalLogger.LogRequest($"{"Last Exception =>" + ex.Message}{" - "}{DateTime.Now}", true, directory);

            }
            finally
            {
                _generalLogger.LogRequest($"{"About to Return with Message => " + webApiResponse.Message}{" - "}{DateTime.Now}", false, directory);

            }
            return null;
        }

        public WebApiResponse AddCompanyRegAddress(List<RegisteredAddress> model, int companyId)
        {
            try
            {
                var encrpt = $"{_appSettings.AppEmail}{ _appSettings.SecreteKey}";
                var apiHash = MyUtils.GenerateSha512(encrpt);
                _generalLogger.LogRequest($"{"About to add Company address On Elps => "}{" "}{JsonConvert.SerializeObject(model)}{" - "}{DateTime.Now}", false, directory);

                var request = new RestRequest("/api/Address/{companyId}/{email}/{apiHash}", Method.POST);
                request.AddUrlSegment("companyid", companyId);
                request.AddUrlSegment("email", _appSettings.AppEmail);
                request.AddUrlSegment("apiHash", apiHash);

                var client = new RestClient(_appSettings.ElpsUrl);
                IRestResponse response = client.Execute(request);

                _generalLogger.LogRequest($"{"Response Exception =>" + response.ErrorException + "\r\nResponse Status =>" + response.ResponseStatus + "\r\nStatus Code =>" + response.StatusCode}{" - "}{DateTime.Now}", false, directory);

                if (response.ErrorException != null)
                {
                    webApiResponse.Message = response.ErrorMessage;
                }
                else if (response.ResponseStatus != ResponseStatus.Completed)
                {
                    webApiResponse.Message = response.ResponseStatus.ToString();
                }
                else if (response.StatusCode != HttpStatusCode.OK)
                {
                    webApiResponse.Message = response.StatusCode.ToString();
                }
                else
                {
                    webApiResponse.Message = "SUCCESS";
                    webApiResponse.Data = JsonConvert.DeserializeObject<List<RegisteredAddress>>(response.Content);
                }
            }
            catch (Exception ex)
            {
                _generalLogger.LogRequest($"{"Last Exception =>" + ex.Message}{" - "}{DateTime.Now}", true, directory);

                webApiResponse.Message = ex.Message;
            }
            finally
            {
                _generalLogger.LogRequest($"{"About to Return with Message => " + webApiResponse.Message}{" - "}{DateTime.Now}", false, directory);

            }
            return webApiResponse;
        }

        public WebApiResponse UpdateCompanyRegAddress(List<RegisteredAddress> model)
        {
            try
            {
                var encrpt = $"{_appSettings.AppEmail}{ _appSettings.SecreteKey}";
                var apiHash = MyUtils.GenerateSha512(encrpt);
                _generalLogger.LogRequest($"{"About to Update Company address On Elps => "}{" "}{JsonConvert.SerializeObject(model)}{" - "}{DateTime.Now}", false, directory);

                var request = new RestRequest("/api/Address/{email}/{apiHash}", Method.PUT);
                request.AddJsonBody(model);
                request.AddUrlSegment("email", _appSettings.AppEmail);
                request.AddUrlSegment("apiHash", apiHash);

                var client = new RestClient(_appSettings.ElpsUrl);
                IRestResponse response = client.Execute(request);

                _generalLogger.LogRequest($"{"Response Exception =>" + response.ErrorException + "\r\nResponse Status =>" + response.ResponseStatus + "\r\nStatus Code =>" + response.StatusCode}{" - "}{DateTime.Now}", false, directory);

                if (response.ErrorException != null)
                {
                    webApiResponse.Message = response.ErrorMessage;
                }
                else if (response.ResponseStatus != ResponseStatus.Completed)
                {
                    webApiResponse.Message = response.ResponseStatus.ToString();
                }
                else if (response.StatusCode != HttpStatusCode.OK)
                {
                    webApiResponse.Message = response.StatusCode.ToString();
                }
                else
                {
                    webApiResponse.Message = "SUCCESS";
                    webApiResponse.Data = JsonConvert.DeserializeObject<List<RegisteredAddress>>(response.Content);
                }
            }
            catch (Exception ex)
            {
                _generalLogger.LogRequest($"{"Last Exception =>" + ex.Message}{" - "}{DateTime.Now}", true, directory);

                webApiResponse.Message = ex.Message;
            }
            finally
            {
                _generalLogger.LogRequest($"{"About to Return with Message => " + webApiResponse.Message}{" - "}{DateTime.Now}", false, directory);

            }
            return webApiResponse;
        }

        public WebApiResponse UpdatePassword(PasswordModel model)
        {
            try
            {
                var encrpt = $"{_appSettings.AppEmail}{ _appSettings.SecreteKey}";
                var apiHash = MyUtils.GenerateSha512(encrpt);
                _generalLogger.LogRequest($"{"About to Update user password On Elps => "}{model.Email}{JsonConvert.SerializeObject(model)}{" - "}{DateTime.Now}", false, directory);

                var request = new RestRequest("/api/Accounts/ChangePassword/{useremail}/{email}/{apiHash}", Method.POST);
                request.AddJsonBody(new { model.OldPassword, model.NewPassword, model.ConfirmPassword });
                request.AddUrlSegment("useremail", model.Email);
                request.AddUrlSegment("email", _appSettings.AppEmail);
                request.AddUrlSegment("apiHash", apiHash);

                var client = new RestClient(_appSettings.ElpsUrl);
                IRestResponse response = client.Execute(request);

                _generalLogger.LogRequest($"{"Response Exception =>" + response.ErrorException + "\r\nResponse Status =>" + response.ResponseStatus + "\r\nStatus Code =>" + response.StatusCode}{" - "}{DateTime.Now}", false, directory);

                if (response.ErrorException != null)
                {
                    webApiResponse.Message = response.ErrorMessage;
                }
                else if (response.ResponseStatus != ResponseStatus.Completed)
                {
                    webApiResponse.Message = response.ResponseStatus.ToString();
                }
                else if (response.StatusCode != HttpStatusCode.OK)
                {
                    webApiResponse.Message = response.StatusCode.ToString();
                }
                else
                {
                    var dic = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content);
                    if (dic["code"].Equals("1"))
                        webApiResponse.Message = "SUCCESS";
                    else
                        webApiResponse.Message = "FAILED";

                    webApiResponse.Data = dic;
                }
            }
            catch (Exception ex)
            {
                _generalLogger.LogRequest($"{"Last Exception =>" + ex.Message}{" - "}{DateTime.Now}", true, directory);

                webApiResponse.Message = ex.Message;
            }
            finally
            {
                _generalLogger.LogRequest($"{"About to Return with Message => " + webApiResponse.Message}{" - "}{DateTime.Now}", false, directory);

            }
            return webApiResponse;
        }

        public WebApiResponse GetCompanyAddressById(int id)
        {
            try
            {
                var encrpt = $"{_appSettings.AppEmail}{ _appSettings.SecreteKey}";
                var apiHash = MyUtils.GenerateSha512(encrpt);
                _generalLogger.LogRequest($"{"About to GetCompanyAddress On Elps with id => "}{" "}{id}{" - "}{DateTime.Now}", false, directory);

                var request = new RestRequest("/api/Address/ById/{id}/{email}/{apiHash}", Method.GET);
                request.AddUrlSegment("id", id);
                request.AddUrlSegment("email", _appSettings.AppEmail);
                request.AddUrlSegment("apiHash", apiHash);

                var client = new RestClient(_appSettings.ElpsUrl);
                IRestResponse response = client.Execute(request);
                _generalLogger.LogRequest($"{"Response Exception =>" + response.ErrorException + "\r\nResponse Status =>" + response.ResponseStatus + "\r\nStatus Code =>" + response.StatusCode}{" - "}{DateTime.Now}", false, directory);

                if (response.ErrorException != null)
                {
                    webApiResponse.Message = response.ErrorMessage;
                }
                else if (response.ResponseStatus != ResponseStatus.Completed)
                {
                    webApiResponse.Message = response.ResponseStatus.ToString();
                }
                else if (response.StatusCode != HttpStatusCode.OK)
                {
                    webApiResponse.Message = response.StatusCode.ToString();
                }
                else
                {
                    webApiResponse.Message = "SUCCESS";
                    webApiResponse.Data = JsonConvert.DeserializeObject<RegisteredAddress>(response.Content);
                }
            }
            catch (Exception ex)
            {
                _generalLogger.LogRequest($"{"Last Exception =>" + ex.Message}{" - "}{DateTime.Now}", true, directory);

                webApiResponse.Message = ex.Message;
            }
            finally
            {
                _generalLogger.LogRequest($"{"About to Return with Message => " + webApiResponse.Message}{" - "}{DateTime.Now}", false, directory);

            }
            return webApiResponse;
        }

        public WebApiResponse GetCompanyDocumentsById(int id)
        {
            try
            {
                var encrpt = $"{_appSettings.AppEmail}{ _appSettings.SecreteKey}";
                var apiHash = MyUtils.GenerateSha512(encrpt);
                _generalLogger.LogRequest($"{"About to get Company;s documents On Elps => "}{" "}{id}{" - "}{DateTime.Now}", false, directory);

                var request = new RestRequest("/api/CompanyDocuments/{id}/{email}/{paiHash}", Method.GET);
                request.AddUrlSegment("id", id);
                request.AddUrlSegment("email", _appSettings.AppEmail);
                request.AddUrlSegment("apiHash", apiHash);

                var client = new RestClient(_appSettings.ElpsUrl);
                IRestResponse response = client.Execute(request);

                _generalLogger.LogRequest($"{"Response Exception =>" + response.ErrorException + "\r\nResponse Status =>" + response.ResponseStatus + "\r\nStatus Code =>" + response.StatusCode}{" - "}{DateTime.Now}", false, directory);

                if (response.ErrorException != null)
                {
                    webApiResponse.Message = response.ErrorMessage;
                }
                else if (response.ResponseStatus != ResponseStatus.Completed)
                {
                    webApiResponse.Message = response.ResponseStatus.ToString();
                }
                else if (response.StatusCode != HttpStatusCode.OK)
                {
                    webApiResponse.Message = response.StatusCode.ToString();
                }
                else
                {
                    webApiResponse.Message = "SUCCESS";
                    webApiResponse.Data = JsonConvert.DeserializeObject<List<CompanyDocument>>(response.Content);
                }
            }
            catch (Exception ex)
            {
                _generalLogger.LogRequest($"{"Last Exception =>" + ex.Message}{" - "}{DateTime.Now}", true, directory);

                webApiResponse.Message = ex.Message;
            }
            finally
            {
                _generalLogger.LogRequest($"{"About to Return with Message => " + webApiResponse.Message}{" - "}{DateTime.Now}", false, directory);

            }
            return webApiResponse;
        }

        #region Payments API
        public WebApiResponse GetExtraPayment_RRR(string companyELPSID ,string email, RemitaSplit rm)
        {
            try
            {
                var encrpt = $"{_appSettings.AppEmail}{ _appSettings.SecreteKey}";
                var apiHash = MyUtils.GenerateSha512(encrpt);
                _generalLogger.LogRequest($"{"Posting Extra Payment PayLoads To Elps For => "}{" Company: "}{email} {"; ApplicationType: "} {rm.CategoryName}{" - "}{DateTime.Now}", false, directory);
               
                var request = new RestRequest("/api/Payments/ExtraPayment/{CompId}/{email}/{apiHash}", Method.POST);
                request.AddUrlSegment("CompId", companyELPSID);
                request.AddUrlSegment("email", _appSettings.AppEmail);
                request.AddUrlSegment("apiHash", apiHash);
                if (rm != null)
                {
                 request.AddParameter("application/json; charset=utf-8", JsonConvert.SerializeObject(rm), ParameterType.RequestBody);
                }
                var client = new RestClient(_appSettings.ElpsUrl);
                IRestResponse response = client.Execute(request);
                _generalLogger.LogRequest($"{"Response Exception =>" + response.ErrorException + "\r\nResponse Status =>" + response.ResponseStatus + "\r\nStatus Code =>" + response.StatusCode}{" - "}{DateTime.Now}", false, directory);
                if (response.ErrorException != null)
                {
                    webApiResponse.Message = response.ErrorMessage;
                }
                else if (response.ResponseStatus != ResponseStatus.Completed)
                {
                    webApiResponse.Message = response.ResponseStatus.ToString();
                }
                else if (response.StatusCode != HttpStatusCode.OK)
                {
                    webApiResponse.Message = response.StatusCode.ToString();
                }
                else
                {
                    webApiResponse.Message = "SUCCESS";
                    webApiResponse.Data = JsonConvert.DeserializeObject<JObject>(response.Content);

                }
            }
            catch (Exception ex)
            {
                _generalLogger.LogRequest($"{"Last Exception =>" + ex.Message}{" - "}{DateTime.Now}", true, directory);

                webApiResponse.Message = ex.Message;
            }
            finally
            {
                _generalLogger.LogRequest($"{"Returning With Message => " + webApiResponse.Message}{" - "}{DateTime.Now}", false, directory);

            }
            return webApiResponse;
        }

        public ElpsResponse PostFacility(Facilities facilityDetails)
        {
            ElpsResponse elspResponse = new ElpsResponse();

            try
            {
                var encrpt = $"{_appSettings.AppEmail}{ _appSettings.SecreteKey}";
                var apiHash = MyUtils.GenerateSha512(encrpt);
                _generalLogger.LogRequest($"{"PostFacility--About to Post Permit On Elps with Company ID => " + webApiResponse.Message}{" - "}{DateTime.Now}", false, directory);
                var request = new RestRequest("/api/Facility/Add/{email}/{apiHash}", Method.POST);
                request.AddUrlSegment("email", _appSettings.AppEmail);
                request.AddUrlSegment("apiHash", apiHash);
                request.RequestFormat = DataFormat.Json;
                request.AddParameter("application/json; charset=utf-8", JsonConvert.SerializeObject(facilityDetails), ParameterType.RequestBody);

                var client = new RestClient(_appSettings.ElpsUrl);
                IRestResponse response = client.Execute(request);
                _generalLogger.LogRequest($"{"PostFacility--Response Exception =>" + response.ErrorException + "\r\nResponse Status =>" + response.ResponseStatus + "\r\nStatus Code =>" + response.StatusCode}{" - "}{DateTime.Now}", false, directory);
                if (response.ErrorException != null)
                {
                    elspResponse.message = response.ErrorMessage;
                }

                else if (response.ResponseStatus != ResponseStatus.Completed)
                {
                    elspResponse.message = response.ResponseStatus.ToString();
                }

                else if (response.StatusCode != HttpStatusCode.OK)
                {
                    elspResponse.message = response.StatusCode.ToString();
                }
                else
                {
                    elspResponse.message = "SUCCESS";
                    elspResponse.value = JsonConvert.DeserializeObject<Facilities>(response.Content);
                }
            }
            catch (Exception ex)
            {
                _generalLogger.LogRequest($"{"PostFacility--Last Exception =>" + ex.Message}{" - "}{DateTime.Now}", false, directory);
                elspResponse.message = ex.Message;
            }
            finally
            {
                _generalLogger.LogRequest($"{"PostFacility--About to Return with Message => " + elspResponse.message}{" - "}{DateTime.Now}", false, directory);

            }

            return elspResponse;
        }

        public WebApiResponse GetAllDocs(string type)
        {
            try
            {
                var encrpt = $"{_appSettings.AppEmail}{_appSettings.SecreteKey}";
                var apiHash = MyUtils.GenerateSha512(encrpt);
                _generalLogger.LogRequest($"{"About to get all documents On Elps => "}{" - "}{DateTime.Now}", false, directory);

                var request = new RestRequest($"/api/Documents/Types/{_appSettings.AppEmail}/{apiHash}", Method.GET);
                if (!string.IsNullOrEmpty(type))
                    request = new RestRequest($"/api/Documents/Facility/{_appSettings.AppEmail}/{apiHash}/facility", Method.GET);

                var client = new RestClient(_appSettings.ElpsUrl);
                IRestResponse response = client.Execute(request);

                _generalLogger.LogRequest($"{"Response Exception =>" + response.ErrorException + "\r\nResponse Status =>" + response.ResponseStatus + "\r\nStatus Code =>" + response.StatusCode}{" - "}{DateTime.Now}", false, directory);

                if (response.ErrorException != null)
                {
                    webApiResponse.Message = response.ErrorMessage;
                }
                else if (response.ResponseStatus != ResponseStatus.Completed)
                {
                    webApiResponse.Message = response.ResponseStatus.ToString();
                }
                else if (response.StatusCode != HttpStatusCode.OK)
                {
                    webApiResponse.Message = response.StatusCode.ToString();
                }
                else
                {
                    webApiResponse.Message = "SUCCESS";
                    webApiResponse.Data = JsonConvert.DeserializeObject<List<DocumentType>>(response.Content);
                }
            }
            catch (Exception ex)
            {
                _generalLogger.LogRequest($"{"Last Exception =>" + ex.Message}{" - "}{DateTime.Now}", true, directory);

                webApiResponse.Message = ex.Message;
            }
            finally
            {
                _generalLogger.LogRequest($"{"About to Return with Message => " + webApiResponse.Message}{" - "}{DateTime.Now}", false, directory);
            }
            return webApiResponse;
        }

        public string ReferenceCode()
        {
            //generate 12 digit numbers
            var bytes = new byte[8];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            ulong random = BitConverter.ToUInt64(bytes, 0) % 1000000000000;
            return String.Format("{0:D12}", random);
        }
        #endregion
    }

}
