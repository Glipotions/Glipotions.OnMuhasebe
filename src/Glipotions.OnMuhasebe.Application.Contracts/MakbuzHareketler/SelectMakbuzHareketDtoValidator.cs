using Glipotions.OnMuhasebe.Consts;
using Glipotions.OnMuhasebe.Makbuzlar;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Glipotions.OnMuhasebe.MakbuzHareketler;

public class SelectMakbuzHareketDtoValidator : AbstractValidator<SelectMakbuzHareketDto>
{
    public SelectMakbuzHareketDtoValidator(IStringLocalizer localizer)
    {
        RuleFor(x => x.CekBankaId)
            .NotEmpty()
            .When(x => x.OdemeTuru == OdemeTuru.Cek)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.Required, localizer["Bank"]]);

        RuleFor(x => x.CekBankaSubeId)
            .NotEmpty()
            .When(x => x.OdemeTuru == OdemeTuru.Cek)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.Required, localizer["BankBranch"]]);

        RuleFor(x => x.CekHesapNo)
            .NotEmpty()
            .When(x => x.OdemeTuru == OdemeTuru.Cek)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.Required, localizer["CheckAccountNo"]])
            .MaximumLength(MakbuzHareketConsts.MaxCekHesapNoLength)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.MaxLenght,
                localizer["CheckAccountNo"], MakbuzHareketConsts.MaxCekHesapNoLength]);

        RuleFor(x => x.BelgeNo)
            .NotEmpty()
            .When(x => x.OdemeTuru == OdemeTuru.Cek)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.Required, localizer["CheckNo"]])
            .MaximumLength(MakbuzHareketConsts.MaxBelgeNoLength)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.MaxLenght,
                localizer["CheckNo"], MakbuzHareketConsts.MaxBelgeNoLength]);

        RuleFor(x => x.AsilBorclu)
            .NotEmpty()
            .When(x => x.OdemeTuru == OdemeTuru.Cek || x.OdemeTuru == OdemeTuru.Senet)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.Required, localizer["PrincipalDebtor"]])
            .MaximumLength(MakbuzHareketConsts.MaxAsilBorcluLength)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.MaxLenght,
                localizer["PrincipalDebtor"], MakbuzHareketConsts.MaxAsilBorcluLength]);

        RuleFor(x => x.AsilBorclu)
            .Empty()
            .When(x => x.OdemeTuru != OdemeTuru.Cek && x.OdemeTuru != OdemeTuru.Senet)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.IsNull, localizer["PrincipalDebtor"]]);

        RuleFor(x => x.Ciranta)
            .MaximumLength(MakbuzHareketConsts.MaxCirantaLength)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.MaxLenght,
                localizer["Endorser"], MakbuzHareketConsts.MaxCirantaLength]);

        RuleFor(x => x.Ciranta)
            .Empty()
            .When(x => x.OdemeTuru != OdemeTuru.Cek && x.OdemeTuru != OdemeTuru.Senet)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.IsNull, localizer["Endorser"]]);

        RuleFor(x => x.KasaId)
            .NotEmpty()
            .When(x => x.OdemeTuru == OdemeTuru.Nakit)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.Required, localizer["CashAccount"]]);

        RuleFor(x => x.BankaHesapId)
            .NotEmpty()
            .When(x => x.OdemeTuru == OdemeTuru.Banka || x.OdemeTuru == OdemeTuru.Pos)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.Required, localizer["BankAccount"]]);
        
        RuleFor(x => x.Tutar)
            .NotNull()
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.Required, localizer["Amount"]])

            .GreaterThanOrEqualTo(0)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.GreaterThanOrEqual,
                localizer["Amount"], localizer["ToZero"], localizer["ThanZero"]]);
        
        RuleFor(x => x.Aciklama)
            .MaximumLength(EntityConsts.MaxAciklamaLength)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.MaxLenght,
                localizer["Description"], EntityConsts.MaxAciklamaLength]);
    }
}