using System;
using System.Collections.Generic;
using System.Text;
using Glipotions.OnMuhasebe.CommonDtos;
using Glipotions.OnMuhasebe.Services;

namespace Glipotions.OnMuhasebe.Bankalar;

public interface IBankaAppService : ICrudAppService<SelectBankaDto, ListBankaDto, BankaListParameterDto,
    CreateBankaDto, UpdateBankaDto, CodeParameterDto>
{
}
