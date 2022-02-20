using Volo.Abp;

namespace Glipotions.OnMuhasebe.Exceptions;

public class ConnotBeDeletedException : BusinessException
{
    public ConnotBeDeletedException() : base(OnMuhasebeDomainErrorCodes.ConnotBeDeleted)
    {
    }
}