using Glipotions.OnMuhasebe.Consts;
using Glipotions.OnMuhasebe.Localization;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Glipotions.OnMuhasebe.Cariler;
/// <ÖZET>
/// (2/5) Kurs
/// Validation işlemi FluentValidation ile yapılır, Volo içinde var
/// Bu yüzden AbstractValidator kullanılır.
/// </summary>
public class UpdateCariDtoValidator:AbstractValidator<UpdateCariDto>
{
    /// <ÖZET>
    /// (2/5) Kurs
    /// FluentValidator da RuleFor ile kural verilir.
    /// 
    /// hataları localizer ile vermemiz gerekir dil problemi olmaması için
    /// her bir property için kural varsa teker teker girilir.
    /// Örneğin Kod için Boş olmaması gerekir ve Maximum uzunluğu geçmemelidir hataları verildi.
    /// 
    /// Enumda olmayan için IsInEnum komutu kullanılır ve enumdaki kullanılmıyorsa hatayı verir.
    /// 
    /// Must kendi yapımızı oluşturmak için kullanılır, kendi kuralımızı koyduktan sonra validation yapılır.
    /// </summary>
    /// <param name="localizer"></param>
    public UpdateCariDtoValidator(IStringLocalizer<OnMuhasebeResource> localizer)
    {
        RuleFor(x => x.Kod)
           .NotEmpty()
           .WithMessage(localizer[OnMuhasebeDomainErrorCodes.Required, localizer["Code"]])

           .MaximumLength(EntityConsts.MaxKodLength)
           .WithMessage(localizer[OnMuhasebeDomainErrorCodes.MaxLenght, localizer["Code"],
            EntityConsts.MaxKodLength]);

        RuleFor(x => x.Ad)
            .NotEmpty()
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.Required, localizer["Name"]])

            .MaximumLength(EntityConsts.MaxAdLength)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.MaxLenght, localizer["Name"],
             EntityConsts.MaxAdLength]);

        RuleFor(x => x.VergiDairesi)
            .MaximumLength(CariConsts.MaxVergiDairesiLength)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.MaxLenght,
             localizer["TaxAdministration"], CariConsts.MaxVergiDairesiLength]);

        RuleFor(x => x.VergiNo)
            .MaximumLength(CariConsts.MaxVergiNoLength)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.MaxLenght,
             localizer["TaxNumber"], CariConsts.MaxVergiNoLength]);

        RuleFor(x => x.Telefon)
            .MaximumLength(EntityConsts.MaxTelefonLength)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.MaxLenght,
             localizer["Telephone"], EntityConsts.MaxTelefonLength]);

        RuleFor(x => x.Adres)
            .MaximumLength(EntityConsts.MaxAdresLength)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.MaxLenght,
             localizer["Address"], EntityConsts.MaxAdresLength]);

        RuleFor(x => x.Aciklama)
            .MaximumLength(EntityConsts.MaxAciklamaLength)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.MaxLenght,
             localizer["Description"], EntityConsts.MaxAciklamaLength]);
    }
}