using System;
using System.Collections.Generic;
using System.Text;
using XMLLoad.Files;
using System.Linq;

namespace XMLLoad.Processing
{
    public interface IPyramidService
    {
        void DispalyReport(MembersComaprer.ComparerType sortingType);
        void AnalyseMembers(Member member);
        void CalculateProvision(Payments transfers);
    }
    public class PyramidService : IPyramidService
    {

        public List<Member> PlainMembersList { get; set; }
        public Member Owner { get; set; }

        public void AnalyseMembers(Member rootMember)
        {
            Owner = rootMember;
            var plainCollection = new List<Member>();
            var totalChildless = ProcessRecursively(rootMember, 0, plainCollection);

            rootMember.ChildlessCnt = totalChildless;
            PlainMembersList = plainCollection;
        }

        public void CalculateProvision(Payments payments)
        {
            foreach (var payment in payments.Payment)
            {
                if (payment.From == Owner.Id)
                {
                    Owner.Cash += payment.Amount;
                }
                else
                {
                    var owner = new List<Member>() { Owner };

                    var membersPath = GetMembersPathRecursively(payment.From, owner);
                    var toShareMoney = membersPath.SkipLast(1).ToList();

                    DivideIncomingMoney(payment, toShareMoney);
                }
            }

        }

        public void DispalyReport(MembersComaprer.ComparerType sortingType)
        {
            Console.WriteLine($"Sorted by {sortingType.ToString()}");
            PlainMembersList.Sort(new MembersComaprer(sortingType));

            foreach (var m in PlainMembersList)
            {
                Console.WriteLine($"\n\n    {m.Id} {m.Level} {m.ChildlessCnt} {m.Cash}");
            }
            Console.WriteLine("----------------------------------------");
        }
        private int ProcessRecursively(Member member, int level, List<Member> flattenMembers)
        {
            member.Level = level++;
            flattenMembers.Add(member);

            if (member.Members.Count == 0)
            {
                return 1;
            }

            //level++;
            foreach (var m in member.Members)
            {
                member.ChildlessCnt += ProcessRecursively(m, level, flattenMembers);
            }
            return member.ChildlessCnt;



        }

        private void DivideIncomingMoney(Payment payment, List<Member> eligibleMembers)
        {
            var cash = payment.Amount;

            foreach (var member in eligibleMembers)
            {
                cash = PayDividendToMember(cash, member);
            }
            eligibleMembers.Last().Cash += cash;
        }

        private decimal PayDividendToMember(decimal input, Member member)
        {
            var dividenda = input / 2;
            var rest = input - dividenda;
            member.Cash += dividenda;
            return rest;
        }

        private List<Member> GetMembersPathRecursively(int from, List<Member> members, List<Member> membersPath = null)
        {
            foreach (var member in members)
            {
                if (member.Id == from)
                {
                    return new List<Member> { member };
                }

                var innerList = new List<Member> { member };
                var childMembers = GetMembersPathRecursively(from, member.Members, membersPath);
                if (childMembers != null)
                {
                    childMembers.Insert(0, member);
                    return childMembers;
                }
            }
            return null;
        }

    }
}
