using System;
using System.Collections.Generic;
using System.Text;
using Glipotions.OnMuhasebe.Services;

namespace Glipotions.OnMuhasebe.BankaSubeler;

public interface IBankaSubeAppService:ICrudAppService<SelectBankaSubeDto, ListBankaSubeDto, BankaSubeListParameterDto,
    CreateBankaSubeDto, UpdateBankaSubeDto, BankaSubeCodeParameterDto>
{
}
