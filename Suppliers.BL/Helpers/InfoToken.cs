using Common.API.Helpers.JwtHelpers;

namespace Suppliers.BL.Helpers;

public class InfoToken() : InfoTokenMod<uint>
{
    public string Token { get; private set; }
    public string Email { get; private set; }
    public uint Profile { get; private set; }

    override public void SetInfoToken(InfoToken<uint> infoToken)
    {
        if (infoToken == null)
        {
            return;
        }
        Token = infoToken.RawToken;
        UserId = uint.Parse(infoToken.GetInfo(0));
        Email = infoToken.GetInfo(1);
        Profile = uint.Parse(infoToken.GetInfo(2));
    }
}