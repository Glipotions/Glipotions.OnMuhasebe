using System;
using System.Collections.Generic;
using System.Text;
using Glipotions.OnMuhasebe.Services;

namespace Glipotions.OnMuhasebe.BankaHesaplar;

public interface IBankaHesapAppService:ICrudAppService<SelectBankaHesapDto, ListBankaHesapDto, BankaHesapListParameterDto,
    CreateBankaHesapDto, UpdateBankaHesapDto, BankaHesapCodeParameterDto>
{
}
