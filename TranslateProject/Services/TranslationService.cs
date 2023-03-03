using Newtonsoft.Json;
using RestSharp;
using TranslateProject.Models;

namespace TranslateProject.Services
{
    public class TranslationService : ITranslationService
    {
        private readonly TranslationApiKeys _translationApiKeys;

        public TranslationService(TranslationApiKeys translationApiKeys)
        {
            _translationApiKeys = translationApiKeys;
        }

        public async Task<string> TranslateAsync(Translation translation)
        {
            string apiKey = string.Empty;
            switch (translation.ProviderName)
            {
                case "0":
                    apiKey = _translationApiKeys.Nlp;
                    break;
                case "1":
                    apiKey = _translationApiKeys.DeepL;
                    break;
                case "2":
                    apiKey = _translationApiKeys.Microsoft;
                    break;
                default: throw new NotImplementedException("This provider is not implemented in the service");
            }

            string apiUrl = string.Empty;
            switch (translation.ProviderName)
            {
                case "0":
                    apiUrl = "https://nlp-translation.p.rapidapi.com/v1/translate";
                    break;
                case "1":
                    apiUrl = "https://api-free.deepl.com/v2/translate";
                    break;
                case "2":
                    apiUrl = $"https://api.cognitive.microsofttranslator.com/translate/?api-version=3.0&textType=html&from=en&to={translation.TargetLanguage}";
                    break;
                default: throw new NotImplementedException("This provider is not implemented in the service");
            }

            RestClient client = new RestClient(apiUrl);
            RestRequest request = new RestRequest(apiUrl, Method.Post);

            switch (translation.ProviderName)
            {
                case "0":
                    request.AddHeader("X-RapidAPI-Key", apiKey);
                    request.AddHeader("X-RapidAPI-Host", "nlp-translation.p.rapidapi.com");
                    request.AddParameter("text", translation.Text);
                    request.AddParameter("to", translation.TargetLanguage);
                    request.AddParameter("from", "en");
                    break;
                case "1":
                    request.AddHeader("Authorization", apiKey);
                    request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                    request.AddHeader("Accept", "application/json");
                    request.AddParameter("text", translation.Text);
                    request.AddParameter("target_lang", translation.TargetLanguage);
                    request.AddParameter("source_lang", "EN");
                    break;
                case "2":
                    request.AddHeader("Ocp-Apim-Subscription-Key", apiKey);
                    request.AddHeader("Content-Type", "application/json");
                    object[] body = new object[] { new { text = translation.Text } };
                    string requestBody = JsonConvert.SerializeObject(body);

                    // request.AddStringBody(requestBody, "application/json");
                    request.AddStringBody(requestBody, DataFormat.Json);
                    break;
                default: throw new NotImplementedException("This provider is not implemented in the service");
            }

            RestResponse response = await client.ExecuteAsync(request);



            switch (translation.ProviderName)
            {
                case "0":
                    {
                        NlpResponse? resultModel = JsonConvert.DeserializeObject<NlpResponse>(response.Content);
                        return resultModel.translated_text[translation.TargetLanguage];
                    }
                case "1":
                    {
                        DeepLResponse? resultModel = JsonConvert.DeserializeObject<DeepLResponse>(response.Content);
                        return resultModel.translations[0].text;
                    }
                case "2":
                    {
                        List<MicrosoftResponse>? resultModel = JsonConvert.DeserializeObject<List<MicrosoftResponse>>(response.Content);
                        return resultModel[0].translations[0].text;
                    }
                default: throw new NotImplementedException("This provider is not implemented in the service");
            }
        }
    }
}
