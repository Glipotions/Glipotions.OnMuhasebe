using System;
using Glipotions.OnMuhasebe.Consts;
using Glipotions.OnMuhasebe.Localization;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Glipotions.OnMuhasebe.BankaHesaplar;
/// <ÖZET>
/// (2/5) Kurs
/// Validation işlemi FluentValidation ile yapılır, Volo içinde var
/// Bu yüzden AbstractValidator kullanılır.
/// </summary>
public class UpdateBankaHesapDtoValidator : AbstractValidator<UpdateBankaHesapDto>
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
    public UpdateBankaHesapDtoValidator(IStringLocalizer<OnMuhasebeResource> localizer)
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

        RuleFor(x => x.HesapTuru)
            .IsInEnum()
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.Required,
             localizer["AccountType"]])

            .NotEmpty()
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.Required,
             localizer["AccountType"]]);

        RuleFor(x => x.BankaSubeId)
            .Must(x => x.HasValue && x.Value != Guid.Empty)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.Required,
             localizer["BankBranch"]]);

        RuleFor(x => x.HesapNo)
            .NotEmpty()
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.Required,
             localizer["AccountNumber"]])

            .MaximumLength(BankaHesapConsts.MaxHesapNoLength)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.MaxLenght,
             localizer["AccountNumber"], BankaHesapConsts.MaxHesapNoLength]);

        RuleFor(x => x.IbanNo)
            .MaximumLength(BankaHesapConsts.MaxIbanNoLength)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.MaxLenght,
             localizer["Iban"], BankaHesapConsts.MaxIbanNoLength]);

        RuleFor(x => x.Aciklama)
            .MaximumLength(EntityConsts.MaxAciklamaLength)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.MaxLenght,
             localizer["Description"], EntityConsts.MaxAciklamaLength]);
    }
}