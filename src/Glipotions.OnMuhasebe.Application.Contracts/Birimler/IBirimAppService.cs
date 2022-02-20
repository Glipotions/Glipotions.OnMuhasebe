using System;
using System.Collections.Generic;
using System.Text;
using Glipotions.OnMuhasebe.CommonDtos;
using Glipotions.OnMuhasebe.Services;

namespace Glipotions.OnMuhasebe.Birimler;

public interface IBirimAppService:ICrudAppService<SelectBirimDto, ListBirimDto, BirimListParameterDto,
    CreateBirimDto, UpdateBirimDto, CodeParameterDto>
{
}
