using HashidsNet;
using ResftulApiPlayground.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulApiPlayground.Infrastructure.Presentation
{
    public static class IdScrambler
    {
        private static IHashids _hashids = new Hashids("mudar depois");

        public static int Decode(string hashid)
        {
            if (string.IsNullOrEmpty(hashid))
                throw new InvalidIdArgumentError();

            int id;
            if (_hashids.TryDecodeSingle(hashid, out id))
                return id;
            else
                throw new InvalidIdArgumentError();
        }

        public static string Encode(int id)
        {
            if (id == 0)
                throw new InvalidIdArgumentError();

            return _hashids.Encode(id);
        }
    }
}
