using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IMemberRepository
    {
        public int Login(string email, string password);
        public IEnumerable<Member> ReadAll();
        public List<Member> ReadKey(string key);
        public int EmailCount(string email);
        public void Create(Member member);
        public void Update(Member member);
        public void Delete(Member member);

        public Member GetByString(string o, string value);

    }
}
