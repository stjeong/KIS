namespace eFriendOpenAPI.Packet;

public class 계좌번호
{
    public string CANO { get; private set; }
    public string ACNT_PRDT_CD { get; private set; }

    public 계좌번호(string account)
    {
        if (string.IsNullOrEmpty(account) || account.Length < 10)
        {
            throw new ArgumentException($"{nameof(account)}");
        }

        CANO = account[0..8];
        ACNT_PRDT_CD = account[8..];
    }

    public override string ToString()
    {
        return $"{CANO}-{ACNT_PRDT_CD}";
    }
}
