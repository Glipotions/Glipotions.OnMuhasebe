using System;
using Glipotions.OnMuhasebe.Consts;
using Glipotions.OnMuhasebe.Localization;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Glipotions.OnMuhasebe.Masraflar;
/// <ÖZET>
/// (2/5) Kurs
/// Validation işlemi FluentValidation ile yapılır, Volo içinde var
/// Bu yüzden AbstractValidator kullanılır.
/// </summary>
public class UpdateMasrafDtoValidator : AbstractValidator<UpdateMasrafDto>
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
    public UpdateMasrafDtoValidator(IStringLocalizer<OnMuhasebeResource> localizer)
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

        RuleFor(x => x.KdvOrani)
            .NotNull()
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.Required,
             localizer["ValueAddedTaxRate"]])

            .GreaterThanOrEqualTo(0)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.GreaterThanOrEqual,
             localizer["ValueAddedTaxRate"], localizer["ToZero"], localizer["ThanZero"]]);

        RuleFor(x => x.BirimFiyat)
            .NotNull()
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.Required,
             localizer["UnitPrice"]])

            .GreaterThanOrEqualTo(0)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.GreaterThanOrEqual,
             localizer["UnitPrice"], localizer["ToZero"], localizer["ThanZero"]]);

        RuleFor(x => x.Barkod)
            .MaximumLength(EntityConsts.MaxBarkodLength)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.MaxLenght,
             localizer["BarCode"], EntityConsts.MaxBarkodLength]);

        RuleFor(x => x.BirimId)
            .Must(x => x.HasValue && x.Value != Guid.Empty)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.Required,
             localizer["Unit"]]);

        RuleFor(x => x.Aciklama)
            .MaximumLength(EntityConsts.MaxAciklamaLength)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.MaxLenght,
             localizer["Description"], EntityConsts.MaxAciklamaLength]);
    }
}