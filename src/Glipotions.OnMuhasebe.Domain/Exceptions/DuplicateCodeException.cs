using Volo.Abp;

namespace Glipotions.OnMuhasebe.Exceptions;

public class DuplicateCodeException : BusinessException
{
    /// <Özet>
    /// Gelen kod'u veritabanına gönderir var mı yok mu diye kontrol eder ve varsa geriye mesaj verir.
    /// 
    /// Farklı dillerde kullanım olabileceği için mesajı burada değil base de verdik.
    /// Domain.Shared Projesinde DomainErrorCodes.cs sınıfında belirtildi.
    /// base e göndermek json dosyalarıyla çalışmayı sağlar.
    /// <param name="kod"></param>
    public DuplicateCodeException(string kod) : base(OnMuhasebeDomainErrorCodes.DuplicateKod)
    {
        WithData("kod", kod);
    }
}