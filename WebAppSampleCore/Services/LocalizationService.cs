using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using WebAppSampleCore.Interfaces.Services;
using WebAppSampleCore.Resources;

namespace WebAppSampleCore.Services;

public class LocalizationService : ILocalizationService
{
    private readonly IStringLocalizer<SharedResources> _localizer;
    private readonly ILogger<LocalizationService> _logger;
    public LocalizationService(IStringLocalizer<SharedResources> localizer, ILogger<LocalizationService> logger)
    {
        _localizer = localizer;
        _logger = logger;
    }

    public LocalizedString this[string name]
        => FindString(name);

    public LocalizedString this[string name, params object[] arguments]
        => FindString(name, arguments);

    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
    {
        return _localizer.GetAllStrings(includeParentCultures);
    }



    LocalizedString FindString(string name, params object[]? arguments)
    {
        var noArgs = arguments == null || arguments.Length == 0;

        LocalizedString result;
        if (noArgs)
        {
            result = _localizer[name];
        }
        else
        {
            result = _localizer[name, arguments!];
        }

        if (result.ResourceNotFound)
        {
            _logger.LogWarning($"Resource '{name}' not found!");
        }

        return result;
    }
}
