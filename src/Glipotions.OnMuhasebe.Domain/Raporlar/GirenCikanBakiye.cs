namespace Glipotions.OnMuhasebe.Raporlar;

public class GirenCikanBakiye : IEntity
{
    public decimal Giren { get; set; }
    public decimal Cikan { get; set; }
    public decimal Bakiye { get; set; }

    public object[] GetKeys()
    {
        throw new NotImplementedException();
    }
}