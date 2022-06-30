using Glipotions.OnMuhasebe.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Glipotions.OnMuhasebe.Permissions;

public class OnMuhasebePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var localizePrefix = "Permission";
        var mainGroup = context.AddGroup(OnMuhasebePermissions.GroupName,
            L($"{localizePrefix}:{OnMuhasebePermissions.GroupName}"));

        //ayar
        //var ayar = mainGroup.AddPermission(OnMuhasebePermissions.Ayar.Default,
        //    L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Ayar)}"));

        //banka
        var banka = mainGroup.AddPermission(OnMuhasebePermissions.Banka_.Default,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Banka_)}"));

        banka.AddChild(OnMuhasebePermissions.Banka_.Create,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Banka_)}{OnMuhasebePermissions.CreateConst}"));//Permission:Banka_.Create
        banka.AddChild(OnMuhasebePermissions.Banka_.Update,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Banka_)}{OnMuhasebePermissions.UpdateConst}"));//Permission:Banka_.Update
        banka.AddChild(OnMuhasebePermissions.Banka_.Delete,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Banka_)}{OnMuhasebePermissions.DeleteConst}"));
        banka.AddChild(OnMuhasebePermissions.Banka_.Transaction,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Banka_)}{OnMuhasebePermissions.TransactionConst}"));

        //banka hesap
        var bankaHesap = mainGroup.AddPermission(OnMuhasebePermissions.BankaHesap.Default,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.BankaHesap)}"));

        bankaHesap.AddChild(OnMuhasebePermissions.BankaHesap.Create,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.BankaHesap)}{OnMuhasebePermissions.CreateConst}"));
        bankaHesap.AddChild(OnMuhasebePermissions.BankaHesap.Update,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.BankaHesap)}{OnMuhasebePermissions.UpdateConst}"));
        bankaHesap.AddChild(OnMuhasebePermissions.BankaHesap.Delete,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.BankaHesap)}{OnMuhasebePermissions.DeleteConst}"));
        bankaHesap.AddChild(OnMuhasebePermissions.BankaHesap.Transaction,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.BankaHesap)}{OnMuhasebePermissions.TransactionConst}"));

        //banka şube
        var bankaSube = mainGroup.AddPermission(OnMuhasebePermissions.BankaSube.Default,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.BankaSube)}"));

        bankaSube.AddChild(OnMuhasebePermissions.BankaSube.Create,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.BankaSube)}{OnMuhasebePermissions.CreateConst}"));
        bankaSube.AddChild(OnMuhasebePermissions.BankaSube.Update,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.BankaSube)}{OnMuhasebePermissions.UpdateConst}"));
        bankaSube.AddChild(OnMuhasebePermissions.BankaSube.Delete,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.BankaSube)}{OnMuhasebePermissions.DeleteConst}"));

        //birim
        var birim = mainGroup.AddPermission(OnMuhasebePermissions.Birim.Default,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Birim)}"));

        birim.AddChild(OnMuhasebePermissions.Birim.Create,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Birim)}{OnMuhasebePermissions.CreateConst}"));
        birim.AddChild(OnMuhasebePermissions.Birim.Update,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Birim)}{OnMuhasebePermissions.UpdateConst}"));
        birim.AddChild(OnMuhasebePermissions.Birim.Delete,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Birim)}{OnMuhasebePermissions.DeleteConst}"));

        //cari
        var cari = mainGroup.AddPermission(OnMuhasebePermissions.Cari.Default,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Cari)}"));

        cari.AddChild(OnMuhasebePermissions.Cari.Create,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Cari)}{OnMuhasebePermissions.CreateConst}"));
        cari.AddChild(OnMuhasebePermissions.Cari.Update,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Cari)}{OnMuhasebePermissions.UpdateConst}"));
        cari.AddChild(OnMuhasebePermissions.Cari.Delete,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Cari)}{OnMuhasebePermissions.DeleteConst}"));
        cari.AddChild(OnMuhasebePermissions.Cari.Transaction,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Cari)}{OnMuhasebePermissions.TransactionConst}"));

        //depo
        var depo = mainGroup.AddPermission(OnMuhasebePermissions.Depo.Default,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Depo)}"));

        depo.AddChild(OnMuhasebePermissions.Depo.Create,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Depo)}{OnMuhasebePermissions.CreateConst}"));
        depo.AddChild(OnMuhasebePermissions.Depo.Update,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Depo)}{OnMuhasebePermissions.UpdateConst}"));
        depo.AddChild(OnMuhasebePermissions.Depo.Delete,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Depo)}{OnMuhasebePermissions.DeleteConst}"));
        depo.AddChild(OnMuhasebePermissions.Depo.Transaction,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Depo)}{OnMuhasebePermissions.TransactionConst}"));

        //donem
        var donem = mainGroup.AddPermission(OnMuhasebePermissions.Donem.Default,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Donem)}"));

        donem.AddChild(OnMuhasebePermissions.Donem.Create,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Donem)}{OnMuhasebePermissions.CreateConst}"));
        donem.AddChild(OnMuhasebePermissions.Donem.Update,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Donem)}{OnMuhasebePermissions.UpdateConst}"));
        donem.AddChild(OnMuhasebePermissions.Donem.Delete,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Donem)}{OnMuhasebePermissions.DeleteConst}"));

        //fatura
        var fatura = mainGroup.AddPermission(OnMuhasebePermissions.Fatura.Default,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Fatura)}"));

        fatura.AddChild(OnMuhasebePermissions.Fatura.Create,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Fatura)}{OnMuhasebePermissions.CreateConst}"));
        fatura.AddChild(OnMuhasebePermissions.Fatura.Update,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Fatura)}{OnMuhasebePermissions.UpdateConst}"));
        fatura.AddChild(OnMuhasebePermissions.Fatura.Delete,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Fatura)}{OnMuhasebePermissions.DeleteConst}"));
        fatura.AddChild(OnMuhasebePermissions.Fatura.Print,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Fatura)}{OnMuhasebePermissions.PrintConst}"));

        //hizmet
        var hizmet = mainGroup.AddPermission(OnMuhasebePermissions.Hizmet.Default,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Hizmet)}"));

        hizmet.AddChild(OnMuhasebePermissions.Hizmet.Create,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Hizmet)}{OnMuhasebePermissions.CreateConst}"));
        hizmet.AddChild(OnMuhasebePermissions.Hizmet.Update,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Hizmet)}{OnMuhasebePermissions.UpdateConst}"));
        hizmet.AddChild(OnMuhasebePermissions.Hizmet.Delete,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Hizmet)}{OnMuhasebePermissions.DeleteConst}"));
        hizmet.AddChild(OnMuhasebePermissions.Hizmet.Transaction,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Hizmet)}{OnMuhasebePermissions.TransactionConst}"));

        //kasa
        var kasa = mainGroup.AddPermission(OnMuhasebePermissions.Kasa.Default,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Kasa)}"));

        kasa.AddChild(OnMuhasebePermissions.Kasa.Create,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Kasa)}{OnMuhasebePermissions.CreateConst}"));
        kasa.AddChild(OnMuhasebePermissions.Kasa.Update,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Kasa)}{OnMuhasebePermissions.UpdateConst}"));
        kasa.AddChild(OnMuhasebePermissions.Kasa.Delete,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Kasa)}{OnMuhasebePermissions.DeleteConst}"));
        kasa.AddChild(OnMuhasebePermissions.Kasa.Transaction,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Kasa)}{OnMuhasebePermissions.TransactionConst}"));

        //makbuz
        var makbuz = mainGroup.AddPermission(OnMuhasebePermissions.Makbuz.Default,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Makbuz)}"));

        makbuz.AddChild(OnMuhasebePermissions.Makbuz.Create,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Makbuz)}{OnMuhasebePermissions.CreateConst}"));
        makbuz.AddChild(OnMuhasebePermissions.Makbuz.Update,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Makbuz)}{OnMuhasebePermissions.UpdateConst}"));
        makbuz.AddChild(OnMuhasebePermissions.Makbuz.Delete,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Makbuz)}{OnMuhasebePermissions.DeleteConst}"));
        makbuz.AddChild(OnMuhasebePermissions.Makbuz.Print,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Makbuz)}{OnMuhasebePermissions.PrintConst}"));

        //masraf
        var masraf = mainGroup.AddPermission(OnMuhasebePermissions.Masraf.Default,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Masraf)}"));

        masraf.AddChild(OnMuhasebePermissions.Masraf.Create,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Masraf)}{OnMuhasebePermissions.CreateConst}"));
        masraf.AddChild(OnMuhasebePermissions.Masraf.Update,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Masraf)}{OnMuhasebePermissions.UpdateConst}"));
        masraf.AddChild(OnMuhasebePermissions.Masraf.Delete,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Masraf)}{OnMuhasebePermissions.DeleteConst}"));
        masraf.AddChild(OnMuhasebePermissions.Masraf.Transaction,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Masraf)}{OnMuhasebePermissions.TransactionConst}"));

        //odemebelgesi
        var odemeBelgesi = mainGroup.AddPermission(OnMuhasebePermissions.OdemeBelgesi.Default,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.OdemeBelgesi)}"));

        //odemeBelgesi.AddChild(OnMuhasebePermissions.OdemeBelgesi.Create,
        //    L($"{localizePrefix}:{nameof(OnMuhasebePermissions.OdemeBelgesi)}{OnMuhasebePermissions.CreateConst}"));
        //odemeBelgesi.AddChild(OnMuhasebePermissions.OdemeBelgesi.Update,
        //    L($"{localizePrefix}:{nameof(OnMuhasebePermissions.OdemeBelgesi)}{OnMuhasebePermissions.UpdateConst}"));
        //odemeBelgesi.AddChild(OnMuhasebePermissions.OdemeBelgesi.Delete,
        //    L($"{localizePrefix}:{nameof(OnMuhasebePermissions.OdemeBelgesi)}{OnMuhasebePermissions.DeleteConst}"));

        //ozelKod
        var ozelKod = mainGroup.AddPermission(OnMuhasebePermissions.OzelKod.Default,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.OzelKod)}"));

        ozelKod.AddChild(OnMuhasebePermissions.OzelKod.Create,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.OzelKod)}{OnMuhasebePermissions.CreateConst}"));
        ozelKod.AddChild(OnMuhasebePermissions.OzelKod.Update,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.OzelKod)}{OnMuhasebePermissions.UpdateConst}"));
        ozelKod.AddChild(OnMuhasebePermissions.OzelKod.Delete,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.OzelKod)}{OnMuhasebePermissions.DeleteConst}"));

        // stok
        var stok = mainGroup.AddPermission(OnMuhasebePermissions.Stok.Default,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Stok)}"));

        stok.AddChild(OnMuhasebePermissions.Stok.Create,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Stok)}{OnMuhasebePermissions.CreateConst}"));
        stok.AddChild(OnMuhasebePermissions.Stok.Update,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Stok)}{OnMuhasebePermissions.UpdateConst}"));
        stok.AddChild(OnMuhasebePermissions.Stok.Delete,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Stok)}{OnMuhasebePermissions.DeleteConst}"));
        stok.AddChild(OnMuhasebePermissions.Stok.Transaction,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Stok)}{OnMuhasebePermissions.TransactionConst}"));

        // sube
        var sube = mainGroup.AddPermission(OnMuhasebePermissions.Sube.Default,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Sube)}"));

        sube.AddChild(OnMuhasebePermissions.Sube.Create,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Sube)}{OnMuhasebePermissions.CreateConst}"));
        sube.AddChild(OnMuhasebePermissions.Sube.Update,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Sube)}{OnMuhasebePermissions.UpdateConst}"));
        sube.AddChild(OnMuhasebePermissions.Sube.Delete,
            L($"{localizePrefix}:{nameof(OnMuhasebePermissions.Sube)}{OnMuhasebePermissions.DeleteConst}"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<OnMuhasebeResource>(name);
    }
}
