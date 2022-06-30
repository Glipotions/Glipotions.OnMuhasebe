namespace Glipotions.OnMuhasebe.Permissions;

public static class OnMuhasebePermissions
{
    public const string GroupName = "OnMuhasebe";


    public const string CreateConst = ".Create";
    public const string UpdateConst = ".Update";
    public const string DeleteConst = ".Delete";
    public const string PrintConst = ".Print";
    public const string TransactionConst = ".Transaction";

    public static class Banka_
    {
        public const string Default = $"{GroupName}.{nameof(Banka_)}";//OnMuhasebe.Banka
        public const string Create = Default + CreateConst;//OnMuhasebe.Banka.Create
        public const string Update = Default + UpdateConst;
        public const string Delete = Default + DeleteConst;
        public const string Transaction = Default + TransactionConst;
    }

    public static class BankaHesap
    {
        public const string Default = $"{GroupName}.{nameof(BankaHesap)}";
        public const string Create = Default + CreateConst;
        public const string Update = Default + UpdateConst;
        public const string Delete = Default + DeleteConst;
        public const string Transaction = Default + TransactionConst;
    }

    public static class BankaSube
    {
        public const string Default = $"{GroupName}.{nameof(BankaSube)}";
        public const string Create = Default + CreateConst;
        public const string Update = Default + UpdateConst;
        public const string Delete = Default + DeleteConst;
    }

    public static class Birim
    {
        public const string Default = $"{GroupName}.{nameof(Birim)}";
        public const string Create = Default + CreateConst;
        public const string Update = Default + UpdateConst;
        public const string Delete = Default + DeleteConst;
    }

    public static class Cari
    {
        public const string Default = $"{GroupName}.{nameof(Cari)}";
        public const string Create = Default + CreateConst;
        public const string Update = Default + UpdateConst;
        public const string Delete = Default + DeleteConst;
        public const string Transaction = Default + TransactionConst;
    }

    public static class Depo
    {
        public const string Default = $"{GroupName}.{nameof(Depo)}";
        public const string Create = Default + CreateConst;
        public const string Update = Default + UpdateConst;
        public const string Delete = Default + DeleteConst;
        public const string Transaction = Default + TransactionConst;
    }

    public static class Donem
    {
        public const string Default = $"{GroupName}.{nameof(Donem)}";
        public const string Create = Default + CreateConst;
        public const string Update = Default + UpdateConst;
        public const string Delete = Default + DeleteConst;
    }

    public static class Fatura
    {
        public const string Default = $"{GroupName}.{nameof(Fatura)}";
        public const string Create = Default + CreateConst;
        public const string Update = Default + UpdateConst;
        public const string Delete = Default + DeleteConst;
        public const string Print = Default + PrintConst;
    }

    public static class Hizmet
    {
        public const string Default = $"{GroupName}.{nameof(Hizmet)}";
        public const string Create = Default + CreateConst;
        public const string Update = Default + UpdateConst;
        public const string Delete = Default + DeleteConst;
        public const string Transaction = Default + TransactionConst;
    }

    public static class Kasa
    {
        public const string Default = $"{GroupName}.{nameof(Kasa)}";
        public const string Create = Default + CreateConst;
        public const string Update = Default + UpdateConst;
        public const string Delete = Default + DeleteConst;
        public const string Transaction = Default + TransactionConst;
    }

    public static class Makbuz
    {
        public const string Default = $"{GroupName}.{nameof(Makbuz)}";
        public const string Create = Default + CreateConst;
        public const string Update = Default + UpdateConst;
        public const string Delete = Default + DeleteConst;
        public const string Print = Default + PrintConst;
    }

    public static class Masraf
    {
        public const string Default = $"{GroupName}.{nameof(Masraf)}";
        public const string Create = Default + CreateConst;
        public const string Update = Default + UpdateConst;
        public const string Delete = Default + DeleteConst;
        public const string Transaction = Default + TransactionConst;
    }

    public static class OdemeBelgesi
    {
        public const string Default = $"{GroupName}.{nameof(OdemeBelgesi)}";
        //public const string Create = Default + CreateConst;
        //public const string Update = Default + UpdateConst;
        //public const string Delete = Default + DeleteConst;
    }

    public static class OzelKod
    {
        public const string Default = $"{GroupName}.{nameof(OzelKod)}";
        public const string Create = Default + CreateConst;
        public const string Update = Default + UpdateConst;
        public const string Delete = Default + DeleteConst;
    }

    public static class Stok
    {
        public const string Default = $"{GroupName}.{nameof(Stok)}";
        public const string Create = Default + CreateConst;
        public const string Update = Default + UpdateConst;
        public const string Delete = Default + DeleteConst;
        public const string Transaction = Default + TransactionConst;
    }

    public static class Sube
    {
        public const string Default = $"{GroupName}.{nameof(Sube)}";
        public const string Create = Default + CreateConst;
        public const string Update = Default + UpdateConst;
        public const string Delete = Default + DeleteConst;
    }
}
