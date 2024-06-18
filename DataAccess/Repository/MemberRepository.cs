using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class MemberRepository : IMemberRepository
    {
        public void Create(Member member)
    => GenericDAO<Member>.Instance.AddNew(member);

        public void Delete(Member member) => GenericDAO<Member>.Instance.Remove(member);


        public int EmailCount(string email)
        {
            throw new NotImplementedException();
        }

        public Member GetById(int id) => GenericDAO<Member>.Instance.GetById(id);
        public Member GetByString(string o, string value) => GenericDAO<Member>.Instance.GetByString(o,value);

        public int Login(string email, string password)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Member> ReadAll() => GenericDAO<Member>.Instance.GetList();

        public List<Member> ReadKey(string key)
        {
            throw new NotImplementedException();
        }

        public void Update(Member member) => GenericDAO<Member>.Instance.Update(member);

    }
}
