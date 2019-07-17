using System;
using System.Collections.Generic;
using System.Linq;

namespace Millionaires
{
    class Program
    {
        static void Main (string[] args)
        {
            List<Customer> customers = new List<Customer> ()
            {
                new Customer () { Name = "Bob Lesman", Balance = 80345000.66, Bank = "FTB" },
                new Customer () { Name = "Joe Landy", Balance = 9284756.21, Bank = "WF" },
                new Customer () { Name = "Meg Ford", Balance = 487233.01, Bank = "BOA" },
                new Customer () { Name = "Peg Vale", Balance = 7001449.92, Bank = "BOA" },
                new Customer () { Name = "Mike Johnson", Balance = 790872.12, Bank = "WF" },
                new Customer () { Name = "Les Paul", Balance = 8374892.54, Bank = "WF" },
                new Customer () { Name = "Sid Crosby", Balance = 957436.39, Bank = "FTB" },
                new Customer () { Name = "Sarah Ng", Balance = 56562389.85, Bank = "FTB" },
                new Customer () { Name = "Tina Fey", Balance = 1000000.00, Bank = "CITI" },
                new Customer () { Name = "Sid Brown", Balance = 49582.68, Bank = "CITI" }
            };

            List<Bank> banks = new List<Bank> ()
            {
                new Bank () { Name = "First Tennessee", Symbol = "FTB" },
                new Bank () { Name = "Wells Fargo", Symbol = "WF" },
                new Bank () { Name = "Bank of America", Symbol = "BOA" },
                new Bank () { Name = "Citibank", Symbol = "CITI" },
            };

            Console.WriteLine ($"The customers:");
            customers.ForEach (customer => Console.WriteLine (customer.Name));

            // Build a collection of customers who are millionaires

            //Method syntax
            // IEnumerable<Customer> MillionairesClub = customers.Where(customer => customer.Balance >= 1000000);

            //Query syntax
            IEnumerable<Customer> MillionairesClub = from customer in customers
                where customer.Balance >= 1000000
                select customer;

            Console.WriteLine ("The Millionaires Club:");
            foreach (Customer c in MillionairesClub)
            {
                Console.WriteLine (c.Name);
            }

            /*
            Given the same customer set, display how many millionaires per bank.
            Ref: https://stackoverflow.com/questions/7325278/group-by-in-linq

            Example Output:
            WF 2
            BOA 1
            FTB 1
            CITI 1
            */
            
            //Method syntax
            // IEnumerable<IGrouping<string,Customer>> MillionairesPerBank = MillionairesClub.GroupBy(customer => customer.Bank);

            //Query syntax
            IEnumerable<IGrouping<string, Customer>> MillionairesPerBank = from millionaire in MillionairesClub
                group millionaire by millionaire.Bank into bankGroup
                select bankGroup;
            
            Console.WriteLine ("The Millionaires Per Bank:");
            foreach (IGrouping<string, Customer> m in MillionairesPerBank)
            {
                Console.WriteLine ($"{m.Key} {m.Count()}");

                foreach (Customer c in m)
                {
                    Console.WriteLine (c.Name);
                }
            }

            Console.WriteLine ("**************************");
            Console.WriteLine ("Testing the split method to see what it is splitting the string on:");
            Array ResultsOfSplit = customers.First().Name.Split();
            foreach (string item in ResultsOfSplit)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine ("**************************");

            /*
            TASK:
            As in the previous exercise, you're going to output the millionaires,
            but you will also display the full name of the bank. You also need
            to sort the millionaires' names, ascending by their LAST name.

            Example output:
                Tina Fey at Citibank
                Joe Landy at Wells Fargo
                Sarah Ng at First Tennessee
                Les Paul at Wells Fargo
                Peg Vale at Bank of America
            */

            // You will need to use the `Where()`
            // and `Select()` methods to generate
            // instances of the following class.
            

            //Query syntax
            List<ReportItem> millionaireReport = (from customer in customers
                join bank in banks on customer.Bank equals bank.Symbol
                where customer.Balance >= 1000000
                orderby customer.Name.Split()[1] ascending
                select new ReportItem {
                    CustomerName = customer.Name,
                    BankName = bank.Name
                }).ToList();

            Console.WriteLine ("The Millionaire Report:");
            foreach (var item in millionaireReport)
            {
                Console.WriteLine ($"{item.CustomerName} at {item.BankName}");
            }
        }
    }
}
