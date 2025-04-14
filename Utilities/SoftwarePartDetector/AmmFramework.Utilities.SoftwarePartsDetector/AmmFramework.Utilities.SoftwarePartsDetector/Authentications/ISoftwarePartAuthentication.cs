using IdentityModel.Client;

namespace AmmFramework.Utilities.SoftwarePartsDetector.Authentications;

public interface ISoftwarePartAuthentication
{
    Task<TokenResponse> LoginAsync();
}