using AmmFramework.Utilities.SoftwarePartsDetector.DataModel;

namespace AmmFramework.Utilities.SoftwarePartsDetector.Publishers;

public interface ISoftwarePartPublisher
{
    Task PublishAsync(SoftwarePart softwarePart);
}