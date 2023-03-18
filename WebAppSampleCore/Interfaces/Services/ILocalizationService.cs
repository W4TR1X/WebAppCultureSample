using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppSampleCore.Resources;

namespace WebAppSampleCore.Interfaces.Services
{
    public interface ILocalizationService : IStringLocalizer<SharedResources>
    {
    }
}
