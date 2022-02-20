using System.Collections.Generic;
using System.Threading.Tasks;
using Glipotions.OnMuhasebe.MakbuzHareketler;

namespace Glipotions.OnMuhasebe.Raporlar;

public interface IFinansalDurumAppService
{
    Task<IList<FinansalHareketDto>> KasaSonOnHareketListAsync(MakbuzHareketListParameterDto input);
    Task<IList<FinansalHareketDto>> BankaSonOnHareketListAsync(MakbuzHareketListParameterDto input);
    Task<IList<FinansalDurumDto>> KasaDurumListAsync(MakbuzHareketListParameterDto input);
    Task<IList<FinansalDurumDto>> BankaDurumListAsync(MakbuzHareketListParameterDto input);
    Task<IList<FinansalDurumDto>> OdemeBelgeleriDagilimListAsync(MakbuzHareketListParameterDto input);
    Task<IList<OdemeBelgesiDto>> GecikenAlacaklarListAsync(MakbuzHareketListParameterDto input);
}