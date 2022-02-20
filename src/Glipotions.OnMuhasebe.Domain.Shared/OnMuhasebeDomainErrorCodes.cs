namespace Glipotions.OnMuhasebe;

public static class OnMuhasebeDomainErrorCodes
{
    /// <Özet>
    /// Alabileceğimiz hatalara bir kod verdik ve bu kodu json da localize işlemlerinde diğer diller
    /// için belirtilir.
    /// </summary>
    /* You can add your business exception error codes here, as constants */
    public const string DuplicateKod = "Exception:00001";
    public const string ConnotBeDeleted = "Exception:00002";
    public const string Required = "Exception:00003";
    public const string MaxLenght = "Exception:00004";
    public const string GreaterThanOrEqual = "Exception:00005";
    public const string IsNull = "Exception:00006";
}