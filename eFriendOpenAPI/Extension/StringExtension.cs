using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eFriendOpenAPI.Extension;

public static class StringExtension
{
    public static decimal ToMoney(this string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return 0;
        }

        return decimal.Parse(value);
    }
}
