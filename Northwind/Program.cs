using System.Configuration;

namespace Northwnd
{
    class Program
    {
        static void Main(string[] args)
        {
            NorthwndQueries queries = new NorthwndQueries(ConfigurationManager.AppSettings["connectionString"]);
            NorthwndConsoleUi ui = new NorthwndConsoleUi();
            ui.Add(queries.Q1, "Show all info about the employee with ID 8.");
            ui.Add(queries.Q2, "Show the list of first and last names of the employees from London.");
            ui.Add(queries.Q3, "Show the list of first and last names of the employees whose first name begins with letter A.");
            ui.Add(queries.Q5, "Calculate the count of employees from London.");
            ui.Add(queries.Q9, "Show the first and last name(s) of the eldest employee(s).");
            ui.Add(queries.Q10, "Show first, last names and ages of 3 eldest employees.");
            ui.Add(queries.Q11, "Show the list of all cities where the employees are from");
            ui.Add(queries.Q13, "Show first and last names of the employees who used to serve orders shipped to Madrid.");
            ui.Add(queries.Q14, "Show first and last names of the employees as well as the count of orders each of them have received during the year 1997 ");
            ui.Add(queries.Q15, "Show first and last names of the employees as well as the count of orders each of them have received during the year 1997");
            ui.Add(queries.Q17, "Show the count of orders made by each customer from France.");
            ui.Add(queries.Q19, "Show the list of french customers’ names who have made more than one order");
            ui.Add(queries.Q30, "Show the list of cities where employees and customers are from and where orders have been made to. Duplicates should be eliminated.");
            ui.Add(queries.Q33, "Change the City field in one of your records using the UPDATE statement.");
            ui.Add(queries.Q35, "Delete one of records");
            using (queries)
            {
                ui.Launch();
            }
        }
    }
}
