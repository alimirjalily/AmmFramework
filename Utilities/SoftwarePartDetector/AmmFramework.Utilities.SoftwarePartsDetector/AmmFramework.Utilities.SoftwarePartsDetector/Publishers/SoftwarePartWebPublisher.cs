using System.Text;
using AmmFramework.Extensions.Serializers.Abstractions;
using AmmFramework.Utilities.SoftwarePartsDetector.Authentications;
using AmmFramework.Utilities.SoftwarePartsDetector.DataModel;
using AmmFramework.Utilities.SoftwarePartsDetector.Options;
using IdentityModel.Client;
using Microsoft.Extensions.Options;

namespace AmmFramework.Utilities.SoftwarePartsDetector.Publishers;

public class SoftwarePartWebPublisher : ISoftwarePartPublisher
{
    private readonly HttpClient _httpClient;
    private readonly SoftwarePartDetectorOptions _softwarePartDetectorOptions;
    private readonly ISoftwarePartAuthentication _softwarePartLogin;
    private readonly IJsonSerializer _jsonSerializer;

    public SoftwarePartWebPublisher(HttpClient httpClient,
        IOptions<SoftwarePartDetectorOptions> softwarePartDetectorOption,
        ISoftwarePartAuthentication softwarePartLogin,
        IJsonSerializer jsonSerializer)
    {
        _httpClient = httpClient;
        _softwarePartDetectorOptions = softwarePartDetectorOption.Value;
        _softwarePartLogin = softwarePartLogin;
        _jsonSerializer = jsonSerializer;
    }
    public async Task PublishAsync(SoftwarePart softwarePart)
    {
        _httpClient.BaseAddress = new Uri(_softwarePartDetectorOptions.DestinationServiceBaseAddress);

        if (_softwarePartDetectorOptions.OAuth.Enabled)
        {
            var token = await _softwarePartLogin.LoginAsync();

            _httpClient.SetBearerToken(token.AccessToken);
        }

        HttpContent httpContent = new StringContent(_jsonSerializer.Serialize(softwarePart),
            Encoding.UTF8,
            "application/json");

        await _httpClient.PostAsync(_softwarePartDetectorOptions.DestinationServicePath, httpContent);
    }
}